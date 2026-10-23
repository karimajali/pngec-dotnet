-- =========================================================================================
-- PNGEC Phase 1 & 2: 07_Enhance_Sync_All_Demographics.sql
-- Purpose: Complete demographic and extended fields synchronization for Voter Roll
-- =========================================================================================

USE [pngec-core];
GO

-- 1. Ensure Queue activation is paused temporarily while updating sp_ProcessSyncInsert
ALTER QUEUE [dbo].[SyncInsertQueue] WITH ACTIVATION (STATUS = OFF);
GO

-- 2. Update sp_ProcessSyncInsert with all 40+ demographic columns
IF OBJECT_ID('dbo.sp_ProcessSyncInsert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ProcessSyncInsert;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE PROCEDURE dbo.sp_ProcessSyncInsert
AS
BEGIN
    SET NOCOUNT ON;
    SET QUOTED_IDENTIFIER ON;
    SET ANSI_NULLS ON;

    DECLARE @ConversationHandle UNIQUEIDENTIFIER;
    DECLARE @MessageTypeName sysname;
    DECLARE @MessageBody XML;

    WHILE (1 = 1)
    BEGIN
        BEGIN TRANSACTION;

        WAITFOR
        (
            RECEIVE TOP(1)
                @ConversationHandle = conversation_handle,
                @MessageTypeName = message_type_name,
                @MessageBody = CAST(message_body AS XML)
            FROM [dbo].[SyncInsertQueue]
        ), TIMEOUT 3000;

        -- If queue is empty, exit loop
        IF (@@ROWCOUNT = 0)
        BEGIN
            ROLLBACK TRANSACTION;
            BREAK;
        END

        IF (@MessageTypeName = '//PNGEC/Sync/InsertMessage')
        BEGIN
            BEGIN TRY
                -- Parse XML Payload
                DECLARE @UploadId NVARCHAR(100) = @MessageBody.value('(/Voter/UploadId)[1]', 'NVARCHAR(100)');
                DECLARE @FName NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/FName)[1]', 'NVARCHAR(100)'), '');
                DECLARE @LName NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/LName)[1]', 'NVARCHAR(100)'), '');
                DECLARE @LocationId INT = @MessageBody.value('(/Voter/LocationId)[1]', 'INT');
                DECLARE @WardId INT = @MessageBody.value('(/Voter/WardId)[1]', 'INT');
                DECLARE @DOB DATETIME = @MessageBody.value('(/Voter/DOB)[1]', 'DATETIME');
                DECLARE @Gender NVARCHAR(10) = @MessageBody.value('(/Voter/Gender)[1]', 'NVARCHAR(10)');
                DECLARE @ActionType NVARCHAR(50) = @MessageBody.value('(/Voter/ActionType)[1]', 'NVARCHAR(50)');
                DECLARE @UserId INT = @MessageBody.value('(/Voter/UserId)[1]', 'INT');
                DECLARE @DeviceId NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/DeviceId)[1]', 'NVARCHAR(100)'), '');
                DECLARE @SevisPassId NVARCHAR(50) = NULLIF(@MessageBody.value('(/Voter/SevisPassId)[1]', 'NVARCHAR(50)'), '');
                DECLARE @SevisPassStatus NVARCHAR(50) = NULLIF(@MessageBody.value('(/Voter/SevisPassStatus)[1]', 'NVARCHAR(50)'), '');

                -- Extended Demographic & Personal Fields
                DECLARE @FatherFName NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/FatherFName)[1]', 'NVARCHAR(100)'), '');
                DECLARE @FatherLName NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/FatherLName)[1]', 'NVARCHAR(100)'), '');
                DECLARE @MotherFName NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/MotherFName)[1]', 'NVARCHAR(100)'), '');
                DECLARE @MotherLName NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/MotherLName)[1]', 'NVARCHAR(100)'), '');
                DECLARE @ProvOfBirthFK INT = @MessageBody.value('(/Voter/ProvOfBirthFK)[1]', 'INT');
                DECLARE @PlaceOfBirth NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/PlaceOfBirth)[1]', 'NVARCHAR(100)'), '');
                DECLARE @OccupationFK INT = @MessageBody.value('(/Voter/OccupationFK)[1]', 'INT');
                DECLARE @ClanFK INT = @MessageBody.value('(/Voter/ClanFK)[1]', 'INT');
                DECLARE @Clan NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/Clan)[1]', 'NVARCHAR(100)'), '');
                DECLARE @SubClan NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/SubClan)[1]', 'NVARCHAR(100)'), '');
                DECLARE @Address NVARCHAR(250) = NULLIF(@MessageBody.value('(/Voter/Address)[1]', 'NVARCHAR(250)'), '');
                DECLARE @NatID NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/NatID)[1]', 'NVARCHAR(100)'), '');
                DECLARE @NatIDRecordNo NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/NatIDRecordNo)[1]', 'NVARCHAR(100)'), '');
                DECLARE @HouseFK INT = @MessageBody.value('(/Voter/HouseFK)[1]', 'INT');
                DECLARE @HouseSortOrder INT = @MessageBody.value('(/Voter/HouseSortOrder)[1]', 'INT');
                DECLARE @FamilyFK INT = @MessageBody.value('(/Voter/FamilyFK)[1]', 'INT');
                DECLARE @FamilySortOrder INT = @MessageBody.value('(/Voter/FamilySortOrder)[1]', 'INT');
                DECLARE @FamilyNumber NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/FamilyNumber)[1]', 'NVARCHAR(100)'), '');
                DECLARE @FamilyName NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/FamilyName)[1]', 'NVARCHAR(100)'), '');
                DECLARE @OpenElectorate NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/OpenElectorate)[1]', 'NVARCHAR(100)'), '');
                DECLARE @RestHouse NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/RestHouse)[1]', 'NVARCHAR(100)'), '');
                DECLARE @SectionNo NVARCHAR(50) = NULLIF(@MessageBody.value('(/Voter/SectionNo)[1]', 'NVARCHAR(50)'), '');
                DECLARE @LotNo NVARCHAR(50) = NULLIF(@MessageBody.value('(/Voter/LotNo)[1]', 'NVARCHAR(50)'), '');
                DECLARE @StreetName NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/StreetName)[1]', 'NVARCHAR(100)'), '');
                DECLARE @SuburbTownSettlement NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/SuburbTownSettlement)[1]', 'NVARCHAR(100)'), '');
                DECLARE @Comment NVARCHAR(MAX) = NULLIF(@MessageBody.value('(/Voter/Comment)[1]', 'NVARCHAR(MAX)'), '');
                DECLARE @Lat DECIMAL(18,6) = @MessageBody.value('(/Voter/Lat)[1]', 'DECIMAL(18,6)');
                DECLARE @Long DECIMAL(18,6) = @MessageBody.value('(/Voter/Long)[1]', 'DECIMAL(18,6)');
                DECLARE @Religion NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/Religion)[1]', 'NVARCHAR(100)'), '');
                DECLARE @ClanId INT = @MessageBody.value('(/Voter/ClanId)[1]', 'INT');
                DECLARE @EnrolmentFormNumber NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/EnrolmentFormNumber)[1]', 'NVARCHAR(100)'), '');
                DECLARE @EnrolmentFormType NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/EnrolmentFormType)[1]', 'NVARCHAR(100)'), '');
                DECLARE @TempElectorId INT = @MessageBody.value('(/Voter/TempElectorId)[1]', 'INT');
                DECLARE @isYOB BIT = @MessageBody.value('(/Voter/isYOB)[1]', 'BIT');
                DECLARE @isPWD BIT = @MessageBody.value('(/Voter/isPWD)[1]', 'BIT');
                DECLARE @dpo NVARCHAR(100) = NULLIF(@MessageBody.value('(/Voter/dpo)[1]', 'NVARCHAR(100)'), '');
                DECLARE @ElectorProfileImage NVARCHAR(250) = NULLIF(@MessageBody.value('(/Voter/ElectorProfileImage)[1]', 'NVARCHAR(250)'), '');
                DECLARE @ElectorFormImage NVARCHAR(250) = NULLIF(@MessageBody.value('(/Voter/ElectorFormImage)[1]', 'NVARCHAR(250)'), '');
                DECLARE @MKALocationId INT = @MessageBody.value('(/Voter/MKALocationId)[1]', 'INT');
                DECLARE @MKAWardId INT = @MessageBody.value('(/Voter/MKAWardId)[1]', 'INT');
                DECLARE @MKALLGId INT = @MessageBody.value('(/Voter/MKALLGId)[1]', 'INT');
                DECLARE @MKADistrictId INT = @MessageBody.value('(/Voter/MKADistrictId)[1]', 'INT');
                DECLARE @MKAClanId INT = @MessageBody.value('(/Voter/MKAClanId)[1]', 'INT');

                -- 1. Check idempotency: If already processed, acknowledge and commit
                IF EXISTS (SELECT 1 FROM [dbo].[tblSyncLog] WHERE [UploadId] = @UploadId AND [Status] = 'SUCCESS')
                BEGIN
                    END CONVERSATION @ConversationHandle;
                    COMMIT TRANSACTION;
                    CONTINUE;
                END

                -- 2. Obtain atomic next Voter Number from Sequence
                DECLARE @NewVoterNumber BIGINT = NEXT VALUE FOR [dbo].[Seq_VoterNumber];
                DECLARE @GenderChar VARCHAR(1) = CASE WHEN UPPER(LEFT(LTRIM(ISNULL(@Gender, 'M')), 1)) = 'F' THEN 'F' ELSE 'M' END;

                -- Lookup Geo Hierarchy for User and RO Geo Area Assignment visibility
                DECLARE @EffectiveWard INT = ISNULL(@WardId, @MKAWardId);
                DECLARE @LLGId INT = NULL, @DistrictId INT = NULL, @ProvinceId INT = NULL, @RegionId INT = NULL;
                SELECT TOP 1 
                    @LLGId = LLGId, 
                    @DistrictId = DistrictId, 
                    @ProvinceId = ProvinceId, 
                    @RegionId = RegionId
                FROM [pngec-core].[geo].[tblWards]
                WHERE WardId = @EffectiveWard;

                -- 3. Execute Insert into Staging tblroll with full columns
                INSERT INTO [pngec-staging].[dbo].[tblroll]
                (
                    [ElectorId], [RecordNo], [UploadId], [FName], [LName], [LocationId], [WardId],
                    [LLGId], [DistrictId], [ProvinceId], [RegionId],
                    [DOB], [Gender], [CreatedBy], [CreatedDate], [Status], [SevisPassId], [SevisPassStatus],
                    [FatherFName], [FatherLName], [MotherFName], [MotherLName],
                    [ProvOfBirthFK], [PlaceOfBirth], [OccupationFK], [ClanFK], [Clan], [SubClan],
                    [Address], [NatID], [NatIDRecordNo], [HouseFK], [HouseSortOrder],
                    [FamilyFK], [FamilySortOrder], [FamilyNumber], [FamilyName],
                    [OpenElectorate], [RestHouse], [SectionNo], [LotNo], [StreetName], [SuburbTownSettlement],
                    [Comment], [Lat], [Long], [Religion], [ClanId],
                    [EnrolmentFormNumber], [EnrolmentFormType], [TempElectorId],
                    [isYOB], [isPWD], [dpo], [ElectorProfileImage], [ElectorFormImage],
                    [MKALocationId], [MKAWardId], [MKALLGId], [MKADistrictId], [MKAClanId]
                )
                VALUES
                (
                    @NewVoterNumber, @NewVoterNumber, @UploadId, @FName, @LName, @LocationId, @WardId,
                    @LLGId, @DistrictId, @ProvinceId, @RegionId,
                    @DOB, @GenderChar, @UserId, GETDATE(), ISNULL(@ActionType, 'NEW'), @SevisPassId, @SevisPassStatus,
                    @FatherFName, @FatherLName, @MotherFName, @MotherLName,
                    @ProvOfBirthFK, @PlaceOfBirth, @OccupationFK, @ClanFK, @Clan, @SubClan,
                    @Address, @NatID, @NatIDRecordNo, @HouseFK, @HouseSortOrder,
                    @FamilyFK, @FamilySortOrder, @FamilyNumber, @FamilyName,
                    @OpenElectorate, @RestHouse, @SectionNo, @LotNo, @StreetName, @SuburbTownSettlement,
                    @Comment, @Lat, @Long, @Religion, @ClanId,
                    @EnrolmentFormNumber, @EnrolmentFormType, @TempElectorId,
                    ISNULL(@isYOB, 0), ISNULL(@isPWD, 0), @dpo, @ElectorProfileImage, @ElectorFormImage,
                    @MKALocationId, @MKAWardId, @MKALLGId, @MKADistrictId, @MKAClanId
                );

                -- 4. Record Success in tblSyncLog
                IF EXISTS (SELECT 1 FROM [dbo].[tblSyncLog] WHERE [UploadId] = @UploadId)
                BEGIN
                    UPDATE [dbo].[tblSyncLog]
                    SET [Status] = 'SUCCESS',
                        [ElectorId] = @NewVoterNumber,
                        [RecordNo] = @NewVoterNumber,
                        [ErrorMessage] = NULL
                    WHERE [UploadId] = @UploadId;
                END
                ELSE
                BEGIN
                    INSERT INTO [dbo].[tblSyncLog]
                        ([UploadId], [ElectorId], [RecordNo], [ActionType], [UserId], [DeviceId], [Status], [CreatedDate])
                    VALUES
                        (@UploadId, @NewVoterNumber, @NewVoterNumber, @ActionType, @UserId, @DeviceId, 'SUCCESS', GETUTCDATE());
                END

                END CONVERSATION @ConversationHandle;
                COMMIT TRANSACTION;
            END TRY
            BEGIN CATCH
                DECLARE @ErrMessage NVARCHAR(MAX) = ERROR_MESSAGE();
                DECLARE @ErrNum INT = ERROR_NUMBER();

                IF (XACT_STATE() <> 0)
                    ROLLBACK TRANSACTION;

                BEGIN TRY
                    INSERT INTO [dbo].[tblSyncInsertErrors]
                        ([ConversationHandle], [MessageTypeName], [MessageBody], [ErrorMessage], [ErrorNumber])
                    VALUES
                        (@ConversationHandle, @MessageTypeName, CAST(@MessageBody AS NVARCHAR(MAX)), @ErrMessage, @ErrNum);

                    UPDATE [dbo].[tblSyncLog]
                    SET [Status] = 'FAILED',
                        [ErrorMessage] = @ErrMessage
                    WHERE [UploadId] = @UploadId;
                END TRY
                BEGIN CATCH
                END CATCH
            END CATCH
        END
        ELSE
        BEGIN
            END CONVERSATION @ConversationHandle;
            COMMIT TRANSACTION;
        END
    END
END;
GO

-- 3. Re-enable Queue Activation
ALTER QUEUE [dbo].[SyncInsertQueue]
WITH STATUS = ON,
     ACTIVATION (
         STATUS = ON,
         PROCEDURE_NAME = [dbo].[sp_ProcessSyncInsert],
         MAX_QUEUE_READERS = 1,
         EXECUTE AS OWNER
     );
GO

-- 4. Update sp_Sync_New to receive all demographic parameters and pack into XML
IF OBJECT_ID('dbo.sp_Sync_New', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Sync_New;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE PROCEDURE dbo.sp_Sync_New
    @UploadId NVARCHAR(100),
    @FName NVARCHAR(100),
    @LName NVARCHAR(100),
    @LocationId INT = NULL,
    @WardId INT = NULL,
    @DOB DATETIME = NULL,
    @Gender NVARCHAR(10) = NULL,
    @UserId INT = NULL,
    @DeviceId NVARCHAR(100) = NULL,
    @SevisPassId NVARCHAR(50) = NULL,
    @SevisPassStatus NVARCHAR(50) = NULL,
    @FatherFName NVARCHAR(100) = NULL,
    @FatherLName NVARCHAR(100) = NULL,
    @MotherFName NVARCHAR(100) = NULL,
    @MotherLName NVARCHAR(100) = NULL,
    @ProvOfBirthFK INT = NULL,
    @PlaceOfBirth NVARCHAR(100) = NULL,
    @OccupationFK INT = NULL,
    @ClanFK INT = NULL,
    @Clan NVARCHAR(100) = NULL,
    @SubClan NVARCHAR(100) = NULL,
    @Address NVARCHAR(250) = NULL,
    @NatID NVARCHAR(100) = NULL,
    @NatIDRecordNo NVARCHAR(100) = NULL,
    @HouseFK INT = NULL,
    @HouseSortOrder INT = NULL,
    @FamilyFK INT = NULL,
    @FamilySortOrder INT = NULL,
    @FamilyNumber NVARCHAR(100) = NULL,
    @FamilyName NVARCHAR(100) = NULL,
    @OpenElectorate NVARCHAR(100) = NULL,
    @RestHouse NVARCHAR(100) = NULL,
    @SectionNo NVARCHAR(50) = NULL,
    @LotNo NVARCHAR(50) = NULL,
    @StreetName NVARCHAR(100) = NULL,
    @SuburbTownSettlement NVARCHAR(100) = NULL,
    @Comment NVARCHAR(MAX) = NULL,
    @Lat DECIMAL(18,6) = NULL,
    @Long DECIMAL(18,6) = NULL,
    @Religion NVARCHAR(100) = NULL,
    @ClanId INT = NULL,
    @EnrolmentFormNumber NVARCHAR(100) = NULL,
    @EnrolmentFormType NVARCHAR(100) = NULL,
    @TempElectorId INT = NULL,
    @isYOB BIT = NULL,
    @isPWD BIT = NULL,
    @dpo NVARCHAR(100) = NULL,
    @ElectorProfileImage NVARCHAR(250) = NULL,
    @ElectorFormImage NVARCHAR(250) = NULL,
    @MKALocationId INT = NULL,
    @MKAWardId INT = NULL,
    @MKALLGId INT = NULL,
    @MKADistrictId INT = NULL,
    @MKAClanId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- A. Idempotency Check
    DECLARE @ExistingRecordNo BIGINT, @ExistingElectorId BIGINT, @ExistingStatus NVARCHAR(50);
    SELECT TOP 1 
        @ExistingRecordNo = RecordNo, 
        @ExistingElectorId = ElectorId, 
        @ExistingStatus = Status
    FROM dbo.tblSyncLog 
    WHERE UploadId = @UploadId;

    IF (@ExistingStatus = 'SUCCESS')
    BEGIN
        SELECT 1 AS IsSuccess, @ExistingElectorId AS ElectorId, @ExistingRecordNo AS RecordNo, 'ALREADY_PROCESSED' AS Message;
        RETURN;
    END

    -- B. Send Message to Service Broker Queue for atomic serialized processing
    BEGIN TRY
        DECLARE @InitConversation UNIQUEIDENTIFIER;
        DECLARE @Msg XML;

        SET @Msg = (
            SELECT 
                @UploadId AS UploadId,
                @FName AS FName,
                @LName AS LName,
                @LocationId AS LocationId,
                @WardId AS WardId,
                @DOB AS DOB,
                @Gender AS Gender,
                'NEW' AS ActionType,
                @UserId AS UserId,
                @DeviceId AS DeviceId,
                @SevisPassId AS SevisPassId,
                @SevisPassStatus AS SevisPassStatus,
                @FatherFName AS FatherFName,
                @FatherLName AS FatherLName,
                @MotherFName AS MotherFName,
                @MotherLName AS MotherLName,
                @ProvOfBirthFK AS ProvOfBirthFK,
                @PlaceOfBirth AS PlaceOfBirth,
                @OccupationFK AS OccupationFK,
                @ClanFK AS ClanFK,
                @Clan AS Clan,
                @SubClan AS SubClan,
                @Address AS Address,
                @NatID AS NatID,
                @NatIDRecordNo AS NatIDRecordNo,
                @HouseFK AS HouseFK,
                @HouseSortOrder AS HouseSortOrder,
                @FamilyFK AS FamilyFK,
                @FamilySortOrder AS FamilySortOrder,
                @FamilyNumber AS FamilyNumber,
                @FamilyName AS FamilyName,
                @OpenElectorate AS OpenElectorate,
                @RestHouse AS RestHouse,
                @SectionNo AS SectionNo,
                @LotNo AS LotNo,
                @StreetName AS StreetName,
                @SuburbTownSettlement AS SuburbTownSettlement,
                @Comment AS Comment,
                @Lat AS Lat,
                @Long AS Long,
                @Religion AS Religion,
                @ClanId AS ClanId,
                @EnrolmentFormNumber AS EnrolmentFormNumber,
                @EnrolmentFormType AS EnrolmentFormType,
                @TempElectorId AS TempElectorId,
                @isYOB AS isYOB,
                @isPWD AS isPWD,
                @dpo AS dpo,
                @ElectorProfileImage AS ElectorProfileImage,
                @ElectorFormImage AS ElectorFormImage,
                @MKALocationId AS MKALocationId,
                @MKAWardId AS MKAWardId,
                @MKALLGId AS MKALLGId,
                @MKADistrictId AS MKADistrictId,
                @MKAClanId AS MKAClanId
            FOR XML PATH('Voter')
        );

        BEGIN DIALOG CONVERSATION @InitConversation
            FROM SERVICE [SyncInsertService]
            TO SERVICE 'SyncInsertService'
            ON CONTRACT [//PNGEC/Sync/InsertContract]
            WITH ENCRYPTION = OFF;

        SEND ON CONVERSATION @InitConversation
            MESSAGE TYPE [//PNGEC/Sync/InsertMessage] (@Msg);

        -- Record initial state in tblSyncLog
        IF NOT EXISTS (SELECT 1 FROM dbo.tblSyncLog WHERE UploadId = @UploadId)
        BEGIN
            INSERT INTO dbo.tblSyncLog (UploadId, ActionType, UserId, DeviceId, Status, CreatedDate)
            VALUES (@UploadId, 'NEW', @UserId, @DeviceId, 'PENDING_QUEUE', GETUTCDATE());
        END

        SELECT 1 AS IsSuccess, 0 AS ElectorId, 0 AS RecordNo, 'QUEUED' AS Message;
    END TRY
    BEGIN CATCH
        DECLARE @ErrMsg NVARCHAR(MAX) = ERROR_MESSAGE();
        SELECT 0 AS IsSuccess, -1 AS ElectorId, 0 AS RecordNo, @ErrMsg AS Message;
    END CATCH
END;
GO

-- 5. Update sp_Sync_Update to update staging tblroll with full fields
IF OBJECT_ID('dbo.sp_Sync_Update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Sync_Update;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE PROCEDURE dbo.sp_Sync_Update
    @UploadId NVARCHAR(100),
    @ElectorId BIGINT,
    @FName NVARCHAR(100) = NULL,
    @LName NVARCHAR(100) = NULL,
    @DOB DATETIME = NULL,
    @Gender NVARCHAR(10) = NULL,
    @LocationId INT = NULL,
    @UserId INT = NULL,
    @DeviceId NVARCHAR(100) = NULL,
    @SevisPassId NVARCHAR(50) = NULL,
    @SevisPassStatus NVARCHAR(50) = NULL,
    @FatherFName NVARCHAR(100) = NULL,
    @FatherLName NVARCHAR(100) = NULL,
    @MotherFName NVARCHAR(100) = NULL,
    @MotherLName NVARCHAR(100) = NULL,
    @ProvOfBirthFK INT = NULL,
    @PlaceOfBirth NVARCHAR(100) = NULL,
    @OccupationFK INT = NULL,
    @ClanFK INT = NULL,
    @Clan NVARCHAR(100) = NULL,
    @SubClan NVARCHAR(100) = NULL,
    @Address NVARCHAR(250) = NULL,
    @NatID NVARCHAR(100) = NULL,
    @NatIDRecordNo NVARCHAR(100) = NULL,
    @HouseFK INT = NULL,
    @HouseSortOrder INT = NULL,
    @FamilyFK INT = NULL,
    @FamilySortOrder INT = NULL,
    @FamilyNumber NVARCHAR(100) = NULL,
    @FamilyName NVARCHAR(100) = NULL,
    @OpenElectorate NVARCHAR(100) = NULL,
    @RestHouse NVARCHAR(100) = NULL,
    @SectionNo NVARCHAR(50) = NULL,
    @LotNo NVARCHAR(50) = NULL,
    @StreetName NVARCHAR(100) = NULL,
    @SuburbTownSettlement NVARCHAR(100) = NULL,
    @Comment NVARCHAR(MAX) = NULL,
    @Lat DECIMAL(18,6) = NULL,
    @Long DECIMAL(18,6) = NULL,
    @Religion NVARCHAR(100) = NULL,
    @ClanId INT = NULL,
    @EnrolmentFormNumber NVARCHAR(100) = NULL,
    @EnrolmentFormType NVARCHAR(100) = NULL,
    @TempElectorId INT = NULL,
    @isYOB BIT = NULL,
    @isPWD BIT = NULL,
    @dpo NVARCHAR(100) = NULL,
    @ElectorProfileImage NVARCHAR(250) = NULL,
    @ElectorFormImage NVARCHAR(250) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- A. Idempotency Check
    IF EXISTS (SELECT 1 FROM dbo.tblSyncLog WHERE UploadId = @UploadId AND Status = 'SUCCESS')
    BEGIN
        SELECT 1 AS IsSuccess, @ElectorId AS ElectorId, 'ALREADY_PROCESSED' AS Message;
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM [pngec-staging].[dbo].[tblroll] WHERE ElectorId = @ElectorId)
        BEGIN
            ROLLBACK TRANSACTION;
            SELECT 0 AS IsSuccess, -3 AS ElectorId, 'Elector not found in staging' AS Message;
            RETURN;
        END

        UPDATE [pngec-staging].[dbo].[tblroll]
        SET
            FName = ISNULL(@FName, FName),
            LName = ISNULL(@LName, LName),
            DOB = ISNULL(@DOB, DOB),
            Gender = CASE WHEN @Gender IS NOT NULL THEN (CASE WHEN UPPER(LEFT(LTRIM(@Gender), 1)) = 'F' THEN 'F' ELSE 'M' END) ELSE Gender END,
            LocationId = ISNULL(@LocationId, LocationId),
            ModifiedBy = @UserId,
            ModifiedDate = GETDATE(),
            SevisPassId = ISNULL(@SevisPassId, SevisPassId),
            SevisPassStatus = ISNULL(@SevisPassStatus, SevisPassStatus),
            FatherFName = ISNULL(@FatherFName, FatherFName),
            FatherLName = ISNULL(@FatherLName, FatherLName),
            MotherFName = ISNULL(@MotherFName, MotherFName),
            MotherLName = ISNULL(@MotherLName, MotherLName),
            ProvOfBirthFK = ISNULL(@ProvOfBirthFK, ProvOfBirthFK),
            PlaceOfBirth = ISNULL(@PlaceOfBirth, PlaceOfBirth),
            OccupationFK = ISNULL(@OccupationFK, OccupationFK),
            ClanFK = ISNULL(@ClanFK, ClanFK),
            Clan = ISNULL(@Clan, Clan),
            SubClan = ISNULL(@SubClan, SubClan),
            Address = ISNULL(@Address, Address),
            NatID = ISNULL(@NatID, NatID),
            NatIDRecordNo = ISNULL(@NatIDRecordNo, NatIDRecordNo),
            HouseFK = ISNULL(@HouseFK, HouseFK),
            HouseSortOrder = ISNULL(@HouseSortOrder, HouseSortOrder),
            FamilyFK = ISNULL(@FamilyFK, FamilyFK),
            FamilySortOrder = ISNULL(@FamilySortOrder, FamilySortOrder),
            FamilyNumber = ISNULL(@FamilyNumber, FamilyNumber),
            FamilyName = ISNULL(@FamilyName, FamilyName),
            OpenElectorate = ISNULL(@OpenElectorate, OpenElectorate),
            RestHouse = ISNULL(@RestHouse, RestHouse),
            SectionNo = ISNULL(@SectionNo, SectionNo),
            LotNo = ISNULL(@LotNo, LotNo),
            StreetName = ISNULL(@StreetName, StreetName),
            SuburbTownSettlement = ISNULL(@SuburbTownSettlement, SuburbTownSettlement),
            Comment = ISNULL(@Comment, Comment),
            Lat = ISNULL(@Lat, Lat),
            Long = ISNULL(@Long, Long),
            Religion = ISNULL(@Religion, Religion),
            ClanId = ISNULL(@ClanId, ClanId),
            EnrolmentFormNumber = ISNULL(@EnrolmentFormNumber, EnrolmentFormNumber),
            EnrolmentFormType = ISNULL(@EnrolmentFormType, EnrolmentFormType),
            TempElectorId = ISNULL(@TempElectorId, TempElectorId),
            isYOB = ISNULL(@isYOB, isYOB),
            isPWD = ISNULL(@isPWD, isPWD),
            dpo = ISNULL(@dpo, dpo),
            ElectorProfileImage = ISNULL(@ElectorProfileImage, ElectorProfileImage),
            ElectorFormImage = ISNULL(@ElectorFormImage, ElectorFormImage)
        WHERE ElectorId = @ElectorId;

        INSERT INTO dbo.tblSyncLog (UploadId, ElectorId, ActionType, UserId, DeviceId, Status, CreatedDate)
        VALUES (@UploadId, @ElectorId, 'UPDATE', @UserId, @DeviceId, 'SUCCESS', GETUTCDATE());

        COMMIT TRANSACTION;
        SELECT 1 AS IsSuccess, @ElectorId AS ElectorId, 'SUCCESS' AS Message;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        SELECT 0 AS IsSuccess, -5 AS ElectorId, ERROR_MESSAGE() AS Message;
    END CATCH
END;
GO

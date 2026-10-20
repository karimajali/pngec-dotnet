-- =========================================================================================
-- PNGEC Phase 1 Migration: 02_Create_ServiceBroker_Queue_and_Activation.sql
-- Purpose: Service Broker Queue, Service, Activation Procedure (MAX_QUEUE_READERS = 1)
-- =========================================================================================

USE [pngec-core];
GO

-- 1. Ensure Service Broker and Trustworthy are enabled on the database
IF (SELECT is_broker_enabled FROM sys.databases WHERE name = 'pngec-core') = 0
BEGIN
    ALTER DATABASE [pngec-core] SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE;
    PRINT 'Service Broker enabled on pngec-core.';
END
GO

ALTER DATABASE [pngec-core] SET TRUSTWORTHY ON;
GO

-- 2. Error / Poison message storage table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'tblSyncInsertErrors')
BEGIN
    CREATE TABLE [dbo].[tblSyncInsertErrors](
        [ErrorId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [ConversationHandle] UNIQUEIDENTIFIER NULL,
        [MessageTypeName] sysname NULL,
        [MessageBody] NVARCHAR(MAX) NULL,
        [ErrorMessage] NVARCHAR(MAX) NULL,
        [ErrorNumber] INT NULL,
        [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
    );
    PRINT 'tblSyncInsertErrors created.';
END
GO

-- 3. Message Type for Sync Inserts
IF NOT EXISTS (SELECT * FROM sys.service_message_types WHERE name = '//PNGEC/Sync/InsertMessage')
BEGIN
    CREATE MESSAGE TYPE [//PNGEC/Sync/InsertMessage]
    VALIDATION = WELL_FORMED_XML;
    PRINT 'Message type //PNGEC/Sync/InsertMessage created.';
END
GO

-- 4. Contract
IF NOT EXISTS (SELECT * FROM sys.service_contracts WHERE name = '//PNGEC/Sync/InsertContract')
BEGIN
    CREATE CONTRACT [//PNGEC/Sync/InsertContract]
    (
        [//PNGEC/Sync/InsertMessage] SENT BY INITIATOR
    );
    PRINT 'Contract //PNGEC/Sync/InsertContract created.';
END
GO

-- 5. Activation Stored Procedure for Serialized 1-at-a-time Processing
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID('dbo.sp_ProcessSyncInsert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ProcessSyncInsert;
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
                -- Parse XML Payload with QUOTED_IDENTIFIER ON
                DECLARE @UploadId NVARCHAR(100) = @MessageBody.value('(/Voter/UploadId)[1]', 'NVARCHAR(100)');
                DECLARE @FName NVARCHAR(100) = @MessageBody.value('(/Voter/FName)[1]', 'NVARCHAR(100)');
                DECLARE @LName NVARCHAR(100) = @MessageBody.value('(/Voter/LName)[1]', 'NVARCHAR(100)');
                DECLARE @LocationId INT = @MessageBody.value('(/Voter/LocationId)[1]', 'INT');
                DECLARE @WardId INT = @MessageBody.value('(/Voter/WardId)[1]', 'INT');
                DECLARE @DOB DATETIME = @MessageBody.value('(/Voter/DOB)[1]', 'DATETIME');
                DECLARE @Gender NVARCHAR(10) = @MessageBody.value('(/Voter/Gender)[1]', 'NVARCHAR(10)');
                DECLARE @ActionType NVARCHAR(50) = @MessageBody.value('(/Voter/ActionType)[1]', 'NVARCHAR(50)');
                DECLARE @UserId INT = @MessageBody.value('(/Voter/UserId)[1]', 'INT');
                DECLARE @DeviceId NVARCHAR(100) = @MessageBody.value('(/Voter/DeviceId)[1]', 'NVARCHAR(100)');
                DECLARE @SevisPassId NVARCHAR(50) = @MessageBody.value('(/Voter/SevisPassId)[1]', 'NVARCHAR(50)');
                DECLARE @SevisPassStatus NVARCHAR(50) = @MessageBody.value('(/Voter/SevisPassStatus)[1]', 'NVARCHAR(50)');

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
                DECLARE @LLGId INT = NULL, @DistrictId INT = NULL, @ProvinceId INT = NULL, @RegionId INT = NULL;
                SELECT TOP 1 
                    @LLGId = LLGId, 
                    @DistrictId = DistrictId, 
                    @ProvinceId = ProvinceId, 
                    @RegionId = RegionId
                FROM [pngec-core].[geo].[tblWards]
                WHERE WardId = @WardId;

                -- 3. Execute Insert into Staging tblroll
                INSERT INTO [pngec-staging].[dbo].[tblroll]
                (
                    [ElectorId], [RecordNo], [UploadId], [FName], [LName], [LocationId], [WardId],
                    [LLGId], [DistrictId], [ProvinceId], [RegionId],
                    [DOB], [Gender], [CreatedBy], [CreatedDate], [Status], [SevisPassId], [SevisPassStatus]
                )
                VALUES
                (
                    @NewVoterNumber, @NewVoterNumber, @UploadId, @FName, @LName, @LocationId, @WardId,
                    @LLGId, @DistrictId, @ProvinceId, @RegionId,
                    @DOB, @GenderChar, @UserId, GETDATE(), ISNULL(@ActionType, 'NEW'), @SevisPassId, @SevisPassStatus
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

-- 6. Create Queue with single serialized reader (MAX_QUEUE_READERS = 1)
IF NOT EXISTS (SELECT * FROM sys.service_queues WHERE name = 'SyncInsertQueue')
BEGIN
    CREATE QUEUE [dbo].[SyncInsertQueue]
    WITH STATUS = ON,
         RETENTION = OFF,
         ACTIVATION (
             STATUS = ON,
             PROCEDURE_NAME = [dbo].[sp_ProcessSyncInsert],
             MAX_QUEUE_READERS = 1,
             EXECUTE AS OWNER
         );
    PRINT 'Queue SyncInsertQueue created with MAX_QUEUE_READERS = 1.';
END
ELSE
BEGIN
    ALTER QUEUE [dbo].[SyncInsertQueue]
    WITH STATUS = ON,
         ACTIVATION (
             STATUS = ON,
             PROCEDURE_NAME = [dbo].[sp_ProcessSyncInsert],
             MAX_QUEUE_READERS = 1,
             EXECUTE AS OWNER
         );
    PRINT 'SyncInsertQueue activation updated.';
END
GO

-- 7. Create Service on Queue
IF NOT EXISTS (SELECT * FROM sys.services WHERE name = 'SyncInsertService')
BEGIN
    CREATE SERVICE [SyncInsertService]
    ON QUEUE [dbo].[SyncInsertQueue]
    ([//PNGEC/Sync/InsertContract]);
    PRINT 'Service SyncInsertService created.';
END
GO

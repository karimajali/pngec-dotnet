-- =========================================================================================
-- PNGEC Phase 1 Migration: 03_Status_Based_Sync_Procedures.sql
-- Purpose: Modular status-based stored procedures replacing monolithic sp_SyncElectors
-- =========================================================================================

USE [pngec-core];
GO

-- 1. sp_Sync_New: Dedicated for New Enrolments with atomic idempotency
IF OBJECT_ID('dbo.sp_Sync_New', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Sync_New;
GO

CREATE PROCEDURE dbo.sp_Sync_New
    @UploadId NVARCHAR(100),
    @FName NVARCHAR(100),
    @LName NVARCHAR(100),
    @LocationId INT,
    @WardId INT,
    @DOB DATETIME,
    @Gender NVARCHAR(10),
    @UserId INT,
    @DeviceId NVARCHAR(100) = NULL,
    @SevisPassId NVARCHAR(50) = NULL,
    @SevisPassStatus NVARCHAR(50) = NULL
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
                @SevisPassStatus AS SevisPassStatus
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

-- 2. sp_Sync_TransferIn: Dedicated for In-Ward Transfer Requests
IF OBJECT_ID('dbo.sp_Sync_TransferIn', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Sync_TransferIn;
GO

CREATE PROCEDURE dbo.sp_Sync_TransferIn
    @UploadId NVARCHAR(100),
    @ElectorId BIGINT,
    @FromWardId INT,
    @ToWardId INT,
    @ToLocationId INT,
    @UserId INT,
    @DeviceId NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verify Elector Exists
        IF NOT EXISTS (SELECT 1 FROM dbo.tblroll WHERE ElectorId = @ElectorId)
        BEGIN
            ROLLBACK TRANSACTION;
            SELECT 0 AS IsSuccess, -3 AS ElectorId, 'Elector not found' AS Message;
            RETURN;
        END

        UPDATE dbo.tblroll
        SET 
            WardId = @ToWardId,
            LocationId = @ToLocationId,
            ModifiedBy = @UserId,
            ModifiedDate = GETDATE(),
            Status = 'TRANSFERRED_IN'
        WHERE ElectorId = @ElectorId;

        -- Audit in tblSyncLog
        INSERT INTO dbo.tblSyncLog (UploadId, ElectorId, ActionType, UserId, DeviceId, Status, CreatedDate)
        VALUES (@UploadId, @ElectorId, 'TRANSFER_IN', @UserId, @DeviceId, 'SUCCESS', GETUTCDATE());

        COMMIT TRANSACTION;
        SELECT 1 AS IsSuccess, @ElectorId AS ElectorId, 'SUCCESS' AS Message;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        SELECT 0 AS IsSuccess, -5 AS ElectorId, ERROR_MESSAGE() AS Message;
    END CATCH
END;
GO

-- 3. sp_Sync_Update: Dedicated for Voter Demographic Corrections
IF OBJECT_ID('dbo.sp_Sync_Update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Sync_Update;
GO

CREATE PROCEDURE dbo.sp_Sync_Update
    @UploadId NVARCHAR(100),
    @ElectorId BIGINT,
    @FName NVARCHAR(100),
    @LName NVARCHAR(100),
    @DOB DATETIME,
    @Gender NVARCHAR(10),
    @LocationId INT,
    @UserId INT,
    @DeviceId NVARCHAR(100) = NULL,
    @SevisPassId NVARCHAR(50) = NULL,
    @SevisPassStatus NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM dbo.tblroll WHERE ElectorId = @ElectorId)
        BEGIN
            ROLLBACK TRANSACTION;
            SELECT 0 AS IsSuccess, -3 AS ElectorId, 'Elector not found' AS Message;
            RETURN;
        END

        UPDATE dbo.tblroll
        SET 
            FName = @FName,
            LName = @LName,
            DOB = @DOB,
            Gender = @Gender,
            LocationId = @LocationId,
            ModifiedBy = @UserId,
            ModifiedDate = GETDATE()
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

-- 4. sp_Sync_StatusChange: Dedicated for GHOST, DECEASED, DUPLICATE, UNDERAGE, TRANSFERRED OUT
IF OBJECT_ID('dbo.sp_Sync_StatusChange', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Sync_StatusChange;
GO

CREATE PROCEDURE dbo.sp_Sync_StatusChange
    @UploadId NVARCHAR(100),
    @ElectorId BIGINT,
    @NewStatus NVARCHAR(50),
    @Comment NVARCHAR(MAX) = NULL,
    @UserId INT,
    @DeviceId NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM dbo.tblroll WHERE ElectorId = @ElectorId)
        BEGIN
            ROLLBACK TRANSACTION;
            SELECT 0 AS IsSuccess, -3 AS ElectorId, 'Elector not found' AS Message;
            RETURN;
        END

        UPDATE dbo.tblroll
        SET 
            Status = @NewStatus,
            ModifiedBy = @UserId,
            ModifiedDate = GETDATE()
        WHERE ElectorId = @ElectorId;

        INSERT INTO dbo.tblSyncLog (UploadId, ElectorId, ActionType, UserId, DeviceId, Status, ErrorMessage, CreatedDate)
        VALUES (@UploadId, @ElectorId, 'STATUS_CHANGE', @UserId, @DeviceId, 'SUCCESS', @Comment, GETUTCDATE());

        COMMIT TRANSACTION;
        SELECT 1 AS IsSuccess, @ElectorId AS ElectorId, 'SUCCESS' AS Message;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        SELECT 0 AS IsSuccess, -5 AS ElectorId, ERROR_MESSAGE() AS Message;
    END CATCH
END;
GO

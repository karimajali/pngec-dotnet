-- =========================================================================================
-- PNGEC Phase 1 Migration: 01_Create_tblSyncLog_and_Sequences.sql
-- Purpose: Provides idempotency tracking table, atomic sequence generator, and constraints
-- =========================================================================================

USE [pngec-core];
GO

-- 1. Create tblSyncLog for client-generated UploadId tracking & safe retries
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'tblSyncLog')
BEGIN
    CREATE TABLE [dbo].[tblSyncLog](
        [SyncLogId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [UploadId] NVARCHAR(100) NOT NULL,
        [ElectorId] BIGINT NULL,
        [RecordNo] BIGINT NULL,
        [ActionType] NVARCHAR(50) NULL,           -- NEW, UPDATE, TRANSFER_IN, STATUS_CHANGE
        [UserId] INT NULL,
        [DeviceId] NVARCHAR(100) NULL,
        [Status] NVARCHAR(50) NOT NULL,           -- SUCCESS, FAILED, PENDING_QUEUE
        [ErrorMessage] NVARCHAR(MAX) NULL,
        [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
    );

    CREATE UNIQUE INDEX [IX_tblSyncLog_UploadId] ON [dbo].[tblSyncLog]([UploadId]);
    CREATE INDEX [IX_tblSyncLog_ElectorId] ON [dbo].[tblSyncLog]([ElectorId]);
    CREATE INDEX [IX_tblSyncLog_CreatedDate] ON [dbo].[tblSyncLog]([CreatedDate]);
    PRINT 'tblSyncLog created successfully with unique index on UploadId.';
END
ELSE
BEGIN
    PRINT 'tblSyncLog already exists.';
END
GO

-- 2. Ensure Synonym for [pngec-staging].[dbo].[tblroll] in [pngec-core]
IF OBJECT_ID('dbo.tblroll', 'U') IS NULL AND OBJECT_ID('dbo.tblroll', 'SN') IS NULL
BEGIN
    CREATE SYNONYM [dbo].[tblroll] FOR [pngec-staging].[dbo].[tblroll];
    PRINT 'Synonym dbo.tblroll created for [pngec-staging].[dbo].[tblroll].';
END
GO

-- 3. Create Sequence for atomic duplicate-proof Voter Numbers
IF NOT EXISTS (SELECT * FROM sys.sequences WHERE name = 'Seq_VoterNumber')
BEGIN
    DECLARE @MaxExistingNo BIGINT = 0;
    
    -- Pick up highest existing RecordNo from staging or start at safe offset
    BEGIN TRY
        SELECT @MaxExistingNo = ISNULL(MAX(RecordNo), 0) FROM [pngec-staging].[dbo].[tblroll];
    END TRY
    BEGIN CATCH
        SET @MaxExistingNo = 0;
    END CATCH
    
    IF @MaxExistingNo < 10000000
        SET @MaxExistingNo = 10000000;

    DECLARE @Sql NVARCHAR(MAX) = 'CREATE SEQUENCE [dbo].[Seq_VoterNumber] START WITH ' + CAST((@MaxExistingNo + 1) AS NVARCHAR(20)) + ' INCREMENT BY 1 NO CYCLE;';
    EXEC sp_executesql @Sql;
    PRINT 'Seq_VoterNumber sequence created starting at ' + CAST((@MaxExistingNo + 1) AS NVARCHAR(20));
END
ELSE
BEGIN
    PRINT 'Seq_VoterNumber sequence already exists.';
END
GO

-- 4. Add Unique Constraint on Voter Number in staging if not already present
IF NOT EXISTS (SELECT * FROM [pngec-staging].sys.indexes WHERE name = 'UQ_tblroll_RecordNo' AND object_id = OBJECT_ID('[pngec-staging].dbo.tblroll'))
BEGIN
    BEGIN TRY
        ALTER TABLE [pngec-staging].[dbo].[tblroll] ADD CONSTRAINT [UQ_tblroll_RecordNo] UNIQUE ([RecordNo]);
        PRINT 'Unique constraint UQ_tblroll_RecordNo added to staging tblroll.';
    END TRY
    BEGIN CATCH
        PRINT 'Could not add unique constraint UQ_tblroll_RecordNo (may have existing duplicates or non-unique records).';
    END CATCH
END
GO


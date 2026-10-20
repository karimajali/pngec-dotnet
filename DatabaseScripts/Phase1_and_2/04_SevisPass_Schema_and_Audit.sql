-- =========================================================================================
-- PNGEC Phase 2 Migration: 04_SevisPass_Schema_and_Audit.sql
-- Purpose: Schema alterations for SevisPass digital identity fields and audit tracking table
-- =========================================================================================

USE [pngec-core];
GO

-- 1. Add SevisPass fields to [pngec-staging].[dbo].[tblroll] if not exists
IF EXISTS (SELECT * FROM [pngec-staging].sys.tables WHERE name = 'tblroll')
BEGIN
    IF NOT EXISTS (SELECT * FROM [pngec-staging].sys.columns WHERE object_id = OBJECT_ID('[pngec-staging].dbo.tblroll') AND name = 'SevisPassId')
    BEGIN
        ALTER TABLE [pngec-staging].[dbo].[tblroll] ADD [SevisPassId] NVARCHAR(50) NULL;
        PRINT 'Column SevisPassId added to [pngec-staging].[dbo].[tblroll].';
    END

    IF NOT EXISTS (SELECT * FROM [pngec-staging].sys.columns WHERE object_id = OBJECT_ID('[pngec-staging].dbo.tblroll') AND name = 'SevisPassStatus')
    BEGIN
        ALTER TABLE [pngec-staging].[dbo].[tblroll] ADD [SevisPassStatus] NVARCHAR(50) NULL DEFAULT 'Pending'; -- Verified, Mismatch, NotFound, Pending
        PRINT 'Column SevisPassStatus added to [pngec-staging].[dbo].[tblroll].';
    END

    IF NOT EXISTS (SELECT * FROM [pngec-staging].sys.columns WHERE object_id = OBJECT_ID('[pngec-staging].dbo.tblroll') AND name = 'SevisPassVerifiedDate')
    BEGIN
        ALTER TABLE [pngec-staging].[dbo].[tblroll] ADD [SevisPassVerifiedDate] DATETIME NULL;
        PRINT 'Column SevisPassVerifiedDate added to [pngec-staging].[dbo].[tblroll].';
    END

    IF NOT EXISTS (SELECT * FROM [pngec-staging].sys.columns WHERE object_id = OBJECT_ID('[pngec-staging].dbo.tblroll') AND name = 'SevisPassDiscrepancy')
    BEGIN
        ALTER TABLE [pngec-staging].[dbo].[tblroll] ADD [SevisPassDiscrepancy] NVARCHAR(MAX) NULL;
        PRINT 'Column SevisPassDiscrepancy added to [pngec-staging].[dbo].[tblroll].';
    END
END
GO

-- 2. Create Audit Log for SevisPass Verifications
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'tblSevisPassAudit')
BEGIN
    CREATE TABLE [dbo].[tblSevisPassAudit](
        [AuditId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [ElectorId] BIGINT NULL,
        [SevisPassId] NVARCHAR(50) NOT NULL,
        [VoterFullName] NVARCHAR(200) NULL,
        [ConsentObtained] BIT NOT NULL DEFAULT 1,
        [VerificationStatus] NVARCHAR(50) NOT NULL, -- Verified, Mismatch, NotFound, Simulated
        [DiscrepancyDetails] NVARCHAR(MAX) NULL,
        [VerifiedByUserId] INT NULL,
        [DeviceId] NVARCHAR(100) NULL,
        [VerifiedTimestamp] DATETIME NOT NULL DEFAULT GETUTCDATE()
    );

    CREATE INDEX [IX_tblSevisPassAudit_SevisPassId] ON [dbo].[tblSevisPassAudit]([SevisPassId]);
    CREATE INDEX [IX_tblSevisPassAudit_ElectorId] ON [dbo].[tblSevisPassAudit]([ElectorId]);
    PRINT 'tblSevisPassAudit created successfully.';
END
GO

-- Add missing columns to pngec-staging.dbo.tblroll
USE [pngec-staging];
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.tblroll') AND name = 'SevisPassDiscrepancy')
    ALTER TABLE dbo.tblroll ADD SevisPassDiscrepancy NVARCHAR(500) NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.tblroll') AND name = 'ARLat')
    ALTER TABLE dbo.tblroll ADD ARLat FLOAT NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.tblroll') AND name = 'ARLong')
    ALTER TABLE dbo.tblroll ADD ARLong FLOAT NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.tblroll') AND name = 'RejectedReason')
    ALTER TABLE dbo.tblroll ADD RejectedReason NVARCHAR(500) NULL;
GO

-- Add missing columns to pngec-final.dbo.tblroll
USE [pngec-final];
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.tblroll') AND name = 'SevisPassDiscrepancy')
    ALTER TABLE dbo.tblroll ADD SevisPassDiscrepancy NVARCHAR(500) NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.tblroll') AND name = 'ARLat')
    ALTER TABLE dbo.tblroll ADD ARLat FLOAT NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.tblroll') AND name = 'ARLong')
    ALTER TABLE dbo.tblroll ADD ARLong FLOAT NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.tblroll') AND name = 'RejectedReason')
    ALTER TABLE dbo.tblroll ADD RejectedReason NVARCHAR(500) NULL;
GO

PRINT 'Successfully updated tables with all required columns!';

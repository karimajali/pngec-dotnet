USE [pngec-core];
GO

-- Ensure pngec-final also has SevisPass columns if it exists
IF EXISTS (SELECT 1 FROM sys.databases WHERE name = 'pngec-final')
BEGIN
    IF NOT EXISTS (SELECT 1 FROM [pngec-final].sys.columns WHERE object_id = OBJECT_ID('[pngec-final].dbo.tblroll') AND name = 'SevisPassId')
    BEGIN
        ALTER TABLE [pngec-final].dbo.tblroll ADD
            SevisPassId NVARCHAR(50) NULL,
            SevisPassStatus NVARCHAR(20) NULL,
            SevisPassVerifiedDate DATETIME NULL,
            SevisPassDiscrepancy NVARCHAR(500) NULL;
        PRINT 'Added SevisPass columns to [pngec-final].dbo.tblroll';
    END
END
GO

-- Alter sp_GetAllVoter to include SevisPass fields in voter roll query
CREATE OR ALTER PROCEDURE [dbo].[sp_GetAllVoter]
	@DatabaseName	VARCHAR(100),
	@PageNo			INT				= 1,
	@PageSize		INT				= 10,
	@KeyWord		VARCHAR(100)	= NULL,
	@SortField		VARCHAR(100)	= 'ElectorId',
	@SortOrder		VARCHAR(5)		= 'DESC',
	@FiledName		VARCHAR(100)	= NULL,
	@FiledValue		VARCHAR(100)	= NULL,
	@LName			VARCHAR(100)	= NULL,
	@FName			VARCHAR(100)	= NULL,
	@Gender			VARCHAR(100)	= NULL,
	@ClanId			INT				= NULL,
	@GeoClanId		INT				= NULL,
	@ApprovalStatus	VARCHAR(100)	= NULL,
	@GeoName		VARCHAR(25)		= NULL,
	@GeoId			INT				= NULL,
	@LoggedUserId	INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @strSQL NVARCHAR(MAX) 
	DECLARE @SelectQuery NVARCHAR(MAX)
	DECLARE @Database NVARCHAR(100)

	SET @Database = CASE
		WHEN @DatabaseName = 'Staging' THEN 'pngec-staging'
		WHEN @DatabaseName = 'Preliminary' THEN 'pngec-preliminary'
		WHEN @DatabaseName = 'Final' THEN 'pngec-final'
		ELSE 'pngec-staging'
	END

	DECLARE @RecordType NVARCHAR(1000);

	IF @DatabaseName = 'Staging' OR @DatabaseName = 'Preliminary'
		BEGIN
			SET @RecordType = 'CASE
			WHEN (ISNULL([IsApproved], 0) = 0 AND ISNULL([IsRejected], 0) = 0 AND ISNULL([IsRejectedByRD], 0) = 0) THEN ''NORMAL''
			WHEN (ISNULL([IsApproved], 0) = 1 AND ISNULL([IsRejected], 0) = 0 AND ISNULL([IsRejectedByRD], 0) = 0) THEN ''APPROVED''
			WHEN (ISNULL([IsApproved], 0) = 0 AND ISNULL([IsRejected], 0) = 1 AND ISNULL([IsRejectedByRD], 0) = 0) THEN ''REJECTED''
			WHEN (ISNULL([IsApproved], 0) = 0 AND ISNULL([IsRejected], 0) = 0 AND ISNULL([IsRejectedByRD], 0) = 1) THEN ''REJECTED BY RD''
			Else ''-''
			END AS RecordType,'
		END
	ELSE
		SET @RecordType = ''
		
	DECLARE @Condition NVARCHAR(500);
	SET @Condition = (SELECT dbo.GetUserAssignGeoArea(@LoggedUserId, @GeoName, @GeoId))

	DECLARE @SubCondition NVARCHAR(MAX);
	IF (@FiledValue = 'TRANSFER OUT with FORM 11')
	BEGIN
		IF @Condition <> ''
		BEGIN
			SET @SubCondition = CASE
				WHEN @FiledName = 'Status' THEN ' AND (Roll.[Status] IN (''TRANSFERRED OUT'')) AND ISNULL(Roll.TransferInLocationId,0) > 0'
				ELSE ''
			END
		END
		ELSE
		BEGIN
			SET @SubCondition = CASE
				WHEN @FiledName = 'Status' THEN ' (Roll.[Status] IN (''TRANSFERRED OUT'')) AND ISNULL(Roll.TransferInLocationId,0) > 0'
				ELSE ''
			END
		END		
	END
	ELSE IF (@FiledValue = 'TRANSFER OUT without FORM 11')
	BEGIN
		IF @Condition <> ''
		BEGIN
			SET @SubCondition = CASE
				WHEN @FiledName = 'Status' THEN ' AND (Roll.[Status] IN (''TRANSFERRED OUT'')) AND ISNULL(Roll.TransferInLocationId,0) = 0'
				ELSE ''
			END
		END
		ELSE
		BEGIN
			SET @SubCondition = CASE
				WHEN @FiledName = 'Status' THEN ' (Roll.[Status] IN (''TRANSFERRED OUT'')) AND ISNULL(Roll.TransferInLocationId,0) = 0'
				ELSE ''
			END
		END
	END
	ELSE IF (@FiledValue <> 'ALL' AND @FiledValue <> 'WARD ROLL' AND @FiledValue <> 'MKA WARD ROLL' AND @FiledValue <> 'DELETED')
		BEGIN
			-- Whitelist allowed search field names to prevent column identifier injection
			IF @FiledName = 'Status'
			BEGIN
				IF @Condition <> ''
					SET @SubCondition = ' AND Roll.[Status] = @pFiledValue'
				ELSE
					SET @SubCondition = ' Roll.[Status] = @pFiledValue'
			END
			ELSE IF @FiledName = 'SevisPass'
			BEGIN
				IF @Condition <> ''
					SET @SubCondition = ' AND Roll.[SevisPassStatus] = @pFiledValue'
				ELSE
					SET @SubCondition = ' Roll.[SevisPassStatus] = @pFiledValue'
			END
			ELSE
			BEGIN
				SET @SubCondition = ''
			END
		END
	ELSE IF (@FiledValue = 'WARD ROLL' OR @FiledValue = 'MKA WARD ROLL')
		BEGIN
			IF @Condition <> ''
				BEGIN
					SET @SubCondition = CASE
						WHEN @FiledName = 'Status' THEN ' AND (Roll.[Status] = ''OLD'' OR Roll.[Status] = ''NEW'' OR Roll.[Status] = ''UPDATED'' OR Roll.[Status] = ''TRANSFERRED IN'')'
						ELSE ''
					END
				END
			ELSE
				BEGIN
					SET @SubCondition = CASE
						WHEN @FiledName = 'Status' THEN ' (Roll.[Status] = ''OLD'' OR Roll.[Status] = ''NEW'' OR Roll.[Status] = ''UPDATED'' OR Roll.[Status] = ''TRANSFERRED IN'')'
						ELSE ''
					END
				END
		END
	ELSE IF (@FiledValue = 'DELETED')
		BEGIN
			IF @Condition <> ''
				BEGIN
					SET @SubCondition = CASE
						WHEN @FiledName = 'Status' THEN ' AND (Roll.[Status] = ''TRANSFERRED OUT'' OR Roll.[Status] = ''GHOST'' OR Roll.[Status] = ''DELETED'' OR Roll.[Status] = ''DUPLICATE'' OR Roll.[Status] = ''DECEASED'' OR Roll.[Status] = ''UNDER AGE'')'
						ELSE ''
					END
				END
			ELSE
				BEGIN
					SET @SubCondition = CASE
						WHEN @FiledName = 'Status' THEN ' (Roll.[Status] = ''TRANSFERRED OUT'' OR Roll.[Status] = ''GHOST'' OR Roll.[Status] = ''DELETED'' OR Roll.[Status] = ''DUPLICATE'' OR Roll.[Status] = ''DECEASED'' OR Roll.[Status] = ''UNDER AGE'')'
						ELSE ''
					END
				END
		END
	ELSE
		BEGIN
			SET @SubCondition = ''
		END

	DECLARE @GenderCondition VARCHAR(500);
	IF @Gender = 'M' OR @Gender = 'F'
	BEGIN
		SET @GenderCondition = ' AND Roll.[Gender] = @pGender'
	END
	ELSE IF @Gender = 'O'
	BEGIN
		SET @GenderCondition = ' AND (ISNULL(Roll.Gender,'''') <> ''M'' AND ISNULL(Roll.Gender,'''') <> ''F'')'
	END
	ELSE
	BEGIN
		SET @GenderCondition = ''
	END

	DECLARE @ClanCondition VARCHAR(500);
	IF @ClanId > 0
	BEGIN
		SET @ClanCondition = ' AND Roll.[MKAClanId] = @pClanId'
	END
	ELSE
	BEGIN
		SET @ClanCondition = ''
	END

	DECLARE @GeoClanCondition NVARCHAR(500);
	IF @GeoClanId > 0
	BEGIN
		SET @GeoClanCondition = ' AND Roll.[ClanId] = @pGeoClanId'
	END
	ELSE
	BEGIN
		SET @GeoClanCondition = ''
	END

	DECLARE @ApprovalCondition NVARCHAR(500);
	SET @ApprovalCondition = CASE
		WHEN @ApprovalStatus = 'APPROVED' THEN ' AND (ISNULL([IsApproved], 0) = 1 AND ISNULL([IsRejected], 0) = 0 AND ISNULL([IsRejectedByRD], 0) = 0)'
		WHEN @ApprovalStatus = 'REJECTED' THEN ' AND (ISNULL([IsApproved], 0) = 0 AND ISNULL([IsRejected], 0) = 1 AND ISNULL([IsRejectedByRD], 0) = 0)'
		WHEN @ApprovalStatus = 'YET TO BE APPROVED' THEN ' AND (ISNULL([IsApproved], 0) = 0 AND ISNULL([IsRejected], 0) = 0 AND ISNULL([IsRejectedByRD], 0) = 0)'
		ELSE ''
	END

	DECLARE @GeoClanConditionSelectFirst NVARCHAR(500);
	SET @GeoClanConditionSelectFirst = ' SELECT '

	SET @SelectQuery = @GeoClanConditionSelectFirst
	SET @SelectQuery = @SelectQuery + '
							Roll.[ElectorId],
							Roll.[OldElectorID],
							Roll.[LocationId],
							Roll.[WardId],
							Roll.[LLGId],
							Roll.[DistrictId],
							Roll.[ProvinceId],
							Roll.[RegionId],
							Roll.[MKALocationId],
							Roll.[MKAWardId],
							Roll.[MKALLGId],
							Roll.[MKADistrictId],
							Roll.[MKAClanId],
							Roll.[LName],
							Roll.[FName],
							Roll.[Gender],
							Roll.[DOB],
							Roll.[isYOB],
							Roll.[isPWD],
							Roll.[FatherLName],
							Roll.[FatherFName],
							Roll.[MotherLName],
							Roll.[MotherFName],
							Roll.[ProvOfBirthFK],
							Roll.[PlaceOfBirth],
							Roll.[OccupationFK],
							Roll.[ClanFK],
							Roll.[SubClan],
							Roll.[Address],
							Roll.[NatID],
							Roll.[NatIDRecordNo],
							Roll.[Status],
							Roll.[HouseFK],
							Roll.[HouseSortOrder],
							Roll.[FamilyFK],
							Roll.[FamilySortOrder],
							Roll.[changed],
							Roll.[dpo],
							Roll.[posted],
							Roll.[gps],
							Roll.[Lat],
							Roll.[Long],
							Roll.[FamilyNumber],
							Roll.[FamilyName],
							Roll.[OpenElectorate],
							Roll.[Clan],
							Roll.[RestHouse],
							Roll.[SectionNo],
							Roll.[LotNo],
							Roll.[StreetName],
							Roll.[SuburbTownSettlement],
							Roll.[Comment],
							Roll.[ElectorProfileImage],
							Roll.[ElectorFormImage],
							Roll.[EnrolmentFormNumber],
							Roll.[DuplicateId],
							Roll.[AuditDescription],
							Roll.[HasTransferInForm11],
							Roll.[TransferInRegionId],
							Roll.[TransferInProvinceId],
							Roll.[TransferInDistrictId],
							Roll.[TransferInLLGId],
							Roll.[TransferInWardId],
							Roll.[TransferInLocationId],
							Roll.[TransferOutRegionId],
							Roll.[TransferOutProvinceId],
							Roll.[TransferOutDistrictId],
							Roll.[TransferOutLLGId],
							Roll.[TransferOutWardId],
							Roll.[TransferOutLocationId],
							Roll.[Religion],
							ISNULL(Roll.[UploadId], '''') AS UploadId,
							ISNULL(Roll.[SevisPassId], '''') AS SevisPassId,
							ISNULL(Roll.[SevisPassStatus], '''') AS SevisPassStatus,
							Roll.[SevisPassVerifiedDate],
							ISNULL(Roll.[SevisPassDiscrepancy], '''') AS SevisPassDiscrepancy,
							O.[OccupationName] AS Occupation,
							P.[ProvinceName] AS ProvinceName,
							PBirth.[ProvinceName] AS ProvinceOfBirth,
							D.[DistrictName] AS DistrictName,
							LL.[LLGName] AS LLGName,
							CAST(ISNULL(W.[WardNumber],'''') AS VARCHAR(25)) + '' - '' + W.[WardName] AS WardName,
							L.[LocationName] AS LocationName,
							DM.[DistrictName] AS MKADistrictName,
							LLM.[LLGName] AS MKALLGName,
							WM.[WardName] AS MKAWardName,
							LM.[LocationName] AS MKALocationName,
							IIF(Roll.[IsRejected]=1,''Rejected'',IIF(Roll.[IsApproved]=1,''Approved'','''')) AS IsApproveOrReject,
							Roll.[ModifiedDate],
							U.[FirstName] + '' '' + U.[LastName] AS ModifiedByName,
							Roll.[ARLat],
							Roll.[ARLong],
							Roll.[RejectedReason],
							CL.ClanName,
							CL.ClanId AS GeoClanID,
							' + @RecordType + '
							ROW_NUMBER() OVER (ORDER BY Roll.[LName] ASC, Roll.[FName] ASC) AS [RowNo]
						FROM [' + @Database + '].[dbo].[tblRoll] AS Roll WITH (NOLOCK)
						LEFT JOIN [dbo].[tblOccupations] AS O WITH(NOLOCK) ON O.[OccupationPK] = Roll.[OccupationFK]
						LEFT JOIN [geo].[tblProvinces] AS P WITH(NOLOCK) ON P.[ProvinceId] = Roll.[ProvinceId]
						LEFT JOIN [geo].[tblProvinces] AS PBirth WITH(NOLOCK) ON PBirth.[ProvinceId] = Roll.[ProvOfBirthFK]
						LEFT JOIN [geo].[tblDistricts] AS D WITH(NOLOCK) ON D.[DistrictId] = Roll.[DistrictId]
						LEFT JOIN [geo].[tblLLGs] AS LL WITH(NOLOCK) ON LL.[LLGId] = Roll.[LLGId]
						LEFT JOIN [geo].[tblWards] AS W WITH(NOLOCK) ON W.[WardId] = Roll.[WardId]
						LEFT JOIN [geo].[tblLocations] AS L WITH(NOLOCK) ON L.[LocationId] = Roll.[LocationId]
						LEFT JOIN [geo].[tblMKADistrict] AS DM WITH(NOLOCK) ON DM.[DistrictId] = Roll.[MKADistrictId] 
						LEFT JOIN [geo].[tblMKALLGs] AS LLM WITH(NOLOCK) ON LLM.[LLGId] = Roll.[MKALLGId]
						LEFT JOIN [geo].[tblMKAWards] AS WM WITH(NOLOCK) ON WM.[WardId] = Roll.[MKAWardId]
						LEFT JOIN [geo].[tblMKALocations] AS LM WITH(NOLOCK) ON LM.[LocationId] = Roll.[MKALocationId]
						LEFT JOIN [geo].[tblMKAClan] AS C WITH(NOLOCK) ON C.[ClanId] = Roll.[MKAClanId]
						LEFT JOIN [pngec-core].[user].[Users] AS U WITH(NOLOCK) ON U.[UserId] = Roll.[ModifiedBy]
						LEFT JOIN [geo].[tblClan] AS CL WITH(NOLOCK) ON CL.[ClanId] = Roll.[ClanId]
						WHERE ' + @Condition + @SubCondition + @GenderCondition + @ApprovalCondition + @ClanCondition +  @GeoClanCondition

	-- Parameterized safe LIKE filters (prevent SQL injection)
	IF @KeyWord IS NOT NULL AND @KeyWord <> ''
	BEGIN
		SET @SelectQuery = @SelectQuery + ' AND (Roll.[FName] LIKE @pKeyWord + ''%'' OR Roll.[LName] LIKE @pKeyWord + ''%'' OR (Roll.[LName] + '' '' + Roll.[FName]) LIKE @pKeyWord + ''%'')'
	END

	IF (@FName IS NOT NULL AND @FName <> '') AND (@LName IS NOT NULL AND @LName <> '')
	BEGIN
		SET @SelectQuery = @SelectQuery + ' AND (Roll.[FName] LIKE @pFName + ''%'' AND Roll.[LName] LIKE @pLName + ''%'')'
	END
	ELSE IF @FName IS NOT NULL AND @FName <> ''
	BEGIN
		SET @SelectQuery = @SelectQuery + ' AND (Roll.[FName] LIKE @pFName + ''%'')'
	END
	ELSE IF @LName IS NOT NULL AND @LName <> ''
	BEGIN
		SET @SelectQuery = @SelectQuery + ' AND (Roll.[LName] LIKE @pLName + ''%'')'
	END
	
	SET @strSQL  =	'SELECT * FROM (' + @SelectQuery + ') Result'
	
	DECLARE @MinRow INT = (@PageNo - 1) * @PageSize;
	DECLARE @MaxRow INT = @PageNo * @PageSize + 1;
						
	IF (@PageSize > 0)
	BEGIN
		SET @strSQL  =	@strSQL  + ' WHERE [Result].[RowNo] > @pMinRow AND [Result].[RowNo] < @pMaxRow'
	END

	DECLARE @ParamDef NVARCHAR(MAX) = N'@pKeyWord VARCHAR(100), @pFName VARCHAR(100), @pLName VARCHAR(100), @pGender VARCHAR(100), @pClanId INT, @pGeoClanId INT, @pFiledValue VARCHAR(100), @pMinRow INT, @pMaxRow INT';

	EXEC sp_executesql @strSQL, @ParamDef,
		@pKeyWord = @KeyWord,
		@pFName = @FName,
		@pLName = @LName,
		@pGender = @Gender,
		@pClanId = @ClanId,
		@pGeoClanId = @GeoClanId,
		@pFiledValue = @FiledValue,
		@pMinRow = @MinRow,
		@pMaxRow = @MaxRow;
	
	SET @strSQL  =	'SELECT COUNT(1) AS TotalRecordCount FROM (' + @SelectQuery + ') Result'
	
	EXEC sp_executesql @strSQL, @ParamDef,
		@pKeyWord = @KeyWord,
		@pFName = @FName,
		@pLName = @LName,
		@pGender = @Gender,
		@pClanId = @ClanId,
		@pGeoClanId = @GeoClanId,
		@pFiledValue = @FiledValue,
		@pMinRow = @MinRow,
		@pMaxRow = @MaxRow;
END
GO

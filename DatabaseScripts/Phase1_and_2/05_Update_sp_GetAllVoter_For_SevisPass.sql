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

	DECLARE @strSQL VARCHAR(MAX) 
	DECLARE @SelectQuery VARCHAR(MAX)
	DECLARE @Database VARCHAR(100)

	SET @Database = CASE
		WHEN @DatabaseName = 'Staging' THEN 'pngec-staging'
		WHEN @DatabaseName = 'Preliminary' THEN 'pngec-preliminary'
		WHEN @DatabaseName = 'Final' THEN 'pngec-final'
		ELSE 'pngec-staging'
	END

	DECLARE @RecordType VARCHAR(1000);

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
		
	DECLARE @Condition VARCHAR(500);
	SET @Condition = (SELECT dbo.GetUserAssignGeoArea(@LoggedUserId, @GeoName, @GeoId))

	DECLARE @SubCondition VARCHAR(MAX);
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
			IF @Condition <> ''
				BEGIN
					SET @SubCondition = CASE
						WHEN @FiledName = 'Status' THEN ' AND Roll.[' + @FiledName + '] = ''' + @FiledValue + ''''
						WHEN @FiledName = 'SevisPass' THEN ' AND Roll.[SevisPassStatus] = ''' + @FiledValue + ''''
						ELSE ''
					END
				END
			ELSE
				BEGIN
					SET @SubCondition = CASE
						WHEN @FiledName = 'Status' THEN ' Roll.[' + @FiledName + '] = ''' + @FiledValue + ''''
						WHEN @FiledName = 'SevisPass' THEN ' Roll.[SevisPassStatus] = ''' + @FiledValue + ''''
						ELSE ''
					END
				END
		END
	ELSE IF (@FiledValue = 'WARD ROLL' OR @FiledValue = 'MKA WARD ROLL')
		BEGIN
			IF @Condition <> ''
				BEGIN
					SET @SubCondition = CASE
						WHEN @FiledName = 'Status' THEN ' AND (Roll.[' + @FiledName + '] = ''' + 'OLD' + ''' OR Roll.[' + @FiledName + '] = ''' + 'NEW' + ''' OR Roll.[' + @FiledName + '] = ''' + 'UPDATED' + ''' OR Roll.[' + @FiledName + '] = ''' + 'TRANSFERRED IN' + ''')'
						ELSE ''
					END
				END
			ELSE
				BEGIN
					SET @SubCondition = CASE
						WHEN @FiledName = 'Status' THEN ' (Roll.[' + @FiledName + '] = ''' + 'OLD' + ''' OR Roll.[' + @FiledName + '] = ''' + 'NEW' + ''' OR Roll.[' + @FiledName + '] = ''' + 'UPDATED' + ''' OR Roll.[' + @FiledName + '] = ''' + 'TRANSFERRED IN' + ''')'
						ELSE ''
					END
				END
		END
	ELSE IF (@FiledValue = 'DELETED')
		BEGIN
			IF @Condition <> ''
				BEGIN
					SET @SubCondition = CASE
						WHEN @FiledName = 'Status' THEN ' AND (Roll.[' + @FiledName + '] = ''' + 'TRANSFERRED OUT' + ''' OR Roll.[' + @FiledName + '] = ''' + 'GHOST' + ''' OR Roll.[' + @FiledName + '] = ''' + 'DELETED' + ''' OR Roll.[' + @FiledName + '] = ''' + 'DUPLICATE' + ''' OR Roll.[' + @FiledName + '] = ''' + 'DECEASED' + ''' OR Roll.[' + @FiledName + '] = ''' + 'UNDER AGE' + ''')'
						ELSE ''
					END
				END
			ELSE
				BEGIN
					SET @SubCondition = CASE
						WHEN @FiledName = 'Status' THEN ' (Roll.[' + @FiledName + '] = ''' + 'TRANSFERRED OUT' + ''' OR Roll.[' + @FiledName + '] = ''' + 'GHOST' + ''' OR Roll.[' + @FiledName + '] = ''' + 'DELETED' + ''' OR Roll.[' + @FiledName + '] = ''' + 'DUPLICATE' + ''' OR Roll.[' + @FiledName + '] = ''' + 'DECEASED' + ''' OR Roll.[' + @FiledName + '] = ''' + 'UNDER AGE' + ''')'
						ELSE ''
					END
				END
		END
	ELSE
		BEGIN
			SET @SubCondition = ''
		END

	DECLARE @GenderCondition VARCHAR(500);
	SET @GenderCondition = CASE
		WHEN @Gender = 'M' THEN ' AND Roll.[Gender] = ''' + @Gender + ''''
		WHEN @Gender = 'F' THEN ' AND Roll.[Gender] = ''' + @Gender + ''''
		WHEN @Gender = 'O' THEN ' AND (ISNULL(Roll.Gender,'''') <> ''M'' AND ISNULL(Roll.Gender,'''') <> ''F'')'
		ELSE ''
	END

	DECLARE @ClanCondition VARCHAR(500);
	SET @ClanCondition = CASE
		WHEN @ClanId > 0 THEN ' AND Roll.[MKAClanId] = ' + CAST(@ClanId as VARCHAR(9)) +  ''
		ELSE ''
	END

	DECLARE @GeoClanCondition VARCHAR(500);
	SET @GeoClanCondition = CASE
		WHEN @GeoClanId > 0 THEN ' AND Roll.[ClanId] = ' + CAST(@GeoClanId as VARCHAR(9)) +  ''
		ELSE ''
	END

	DECLARE @ApprovalCondition VARCHAR(500);
	SET @ApprovalCondition = CASE
		WHEN @ApprovalStatus = 'APPROVED' THEN ' AND (ISNULL([IsApproved], 0) = 1 AND ISNULL([IsRejected], 0) = 0 AND ISNULL([IsRejectedByRD], 0) = 0)'
		WHEN @ApprovalStatus = 'REJECTED' THEN ' AND (ISNULL([IsApproved], 0) = 0 AND ISNULL([IsRejected], 0) = 1 AND ISNULL([IsRejectedByRD], 0) = 0)'
		WHEN @ApprovalStatus = 'YET TO BE APPROVED' THEN ' AND (ISNULL([IsApproved], 0) = 0 AND ISNULL([IsRejected], 0) = 0 AND ISNULL([IsRejectedByRD], 0) = 0)'
		ELSE ''
	END

	DECLARE @GeoClanConditionSelectFirst VARCHAR(500);
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

	IF @KeyWord IS NOT NULL AND @KeyWord <> ''
	BEGIN
		SET @SelectQuery = @SelectQuery + ' AND (Roll.[FName] LIKE ''' + @KeyWord + '%''OR ' + 
				'Roll.[LName] LIKE ''' + @KeyWord + '%''OR ' + 
				CONCAT('Roll.[LName]', ' + ', ''' ''', ' + ', 'Roll.[FName]') + ' LIKE ''' + @KeyWord + '%'')'
	END

	IF (@FName IS NOT NULL AND @FName <> '') AND (@LName IS NOT NULL AND @LName <> '')
		BEGIN
			SET @SelectQuery = @SelectQuery + ' AND (Roll.[FName] LIKE ''' + @FName + '%''AND ' + 'Roll.[LName] LIKE ''' + @LName + '%'')'
		END
	ELSE IF @FName IS NOT NULL AND @FName <> ''
		BEGIN
			SET @SelectQuery = @SelectQuery + ' AND (Roll.[FName] LIKE ''' + @FName + '%'')'
		END
	ELSE IF @LName IS NOT NULL AND @LName <> ''
		BEGIN
			SET @SelectQuery = @SelectQuery + ' AND (Roll.[LName] LIKE ''' + @LName + '%'')'
		END
	
	SET @strSQL  =	'SELECT * FROM (' + @SelectQuery + ') Result'
						
	IF (@PageSize > 0)
	BEGIN
		SET @strSQL  =	@strSQL  + ' WHERE [Result].[RowNo] > ' + CAST((@PageNo-1)* @PageSize AS VARCHAR(10)) + '
									 AND [Result].[RowNo] < ' + CAST(@PageNo * @PageSize + 1 AS VARCHAR(10))
	END

	EXEC(@strSQL)	
	
	SET @strSQL  =	'	SELECT COUNT(1) AS TotalRecordCount FROM (' + @SelectQuery + ') Result'
	
	EXEC(@strSQL)	
END
GO

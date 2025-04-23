
ALTER PROCEDURE [dbo].[spDashboard] 
@Flag char = null
AS
BEGIN
	IF @Flag = 'D' -- admin dashboard counts
	BEGIN
		DECLARE @cols NVARCHAR(MAX),
        @pivotCols NVARCHAR(MAX),
        @query NVARCHAR(MAX);

		-- Get columns for GenerationType
		SELECT
			@cols = STRING_AGG('SUM(' + QUOTENAME(GenerationType) + ') AS ' + QUOTENAME('Generation' + CAST(GenerationType AS varchar(20))), ','),
			@pivotCols = STRING_AGG(QUOTENAME(GenerationType), ',')
		FROM (SELECT DISTINCT GenerationType FROM tblFamilyTreeSetup) AS Generations;

		SET @query = N'
		SELECT
			(SELECT
				COUNT(*) AS TotalFamilyMember,
				COUNT(CASE WHEN GENDER = 1 THEN 1 END) AS Male,
				COUNT(CASE WHEN GENDER = 2 THEN 1 END) AS Female,
				COUNT(CASE WHEN deathdate IS NULL THEN 1 END) AS Living,
				COUNT(CASE WHEN deathdate IS NOT NULL THEN 1 END) AS Death,
				AVG(CASE WHEN deathdate IS NOT NULL THEN DATEDIFF(year, dob, deathdate) END) AS AverageLifespan,
				(
					SELECT TOP 1 FirstName + '' '' + ISNULL(MiddleName + '' '', '''') + LastName + '' ('' + CAST(DATEDIFF(year, dob, deathdate) AS VARCHAR) + '' years)''
					FROM tblFamilyTreeSetup
					WHERE deathdate IS NOT NULL
					ORDER BY DATEDIFF(year, dob, deathdate) DESC
				) AS LongestLivingIndividual,
				' + ISNULL(@cols, '') + ',
				SUM(CASE WHEN Age BETWEEN 1 AND 10 THEN 1 ELSE 0 END) AS [10''s],
				SUM(CASE WHEN Age BETWEEN 11 AND 20 THEN 1 ELSE 0 END) AS [20''s],
				SUM(CASE WHEN Age BETWEEN 21 AND 30 THEN 1 ELSE 0 END) AS [30''s],
				SUM(CASE WHEN Age BETWEEN 31 AND 40 THEN 1 ELSE 0 END) AS [40''s],
				SUM(CASE WHEN Age BETWEEN 41 AND 50 THEN 1 ELSE 0 END) AS [50''s],
				SUM(CASE WHEN Age BETWEEN 51 AND 60 THEN 1 ELSE 0 END) AS [60''s],
				SUM(CASE WHEN Age BETWEEN 61 AND 70 THEN 1 ELSE 0 END) AS [70''s],
				SUM(CASE WHEN Age BETWEEN 71 AND 80 THEN 1 ELSE 0 END) AS [80''s],
				SUM(CASE WHEN Age BETWEEN 81 AND 90 THEN 1 ELSE 0 END) AS [90''s],
				SUM(CASE WHEN Age BETWEEN 91 AND 100 THEN 1 ELSE 0 END) AS [100''s],
				SUM(CASE WHEN MatrialStatus = 1 THEN 1 ELSE 0 END) AS MaritalSingle,
				SUM(CASE WHEN MatrialStatus = 2 THEN 1 ELSE 0 END) AS MaritalMarried,
				SUM(CASE WHEN MatrialStatus = 3 THEN 1 ELSE 0 END) AS MaritalDivorced,
				SUM(CASE WHEN MatrialStatus = 4 THEN 1 ELSE 0 END) AS MaritalWidowed,
				SUM(CASE WHEN MatrialStatus = 5 THEN 1 ELSE 0 END) AS MaritalIsolated
			FROM
				tblFamilyTreeSetup
			' + (CASE WHEN @pivotCols IS NOT NULL THEN
			N'PIVOT (
				COUNT(Id)
				FOR GenerationType IN (' + @pivotCols + ')
			) AS PivotTable' ELSE '' END) + '
			FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS DashBoardData;';

		EXEC sp_executesql @query;

	END

	IF @Flag = 'A' -- for upcomming birth or death anniversary
	BEGIN
			WITH UpcomingAnniversaries AS (
			SELECT Id,CONCAT(FirstName, ' ', COALESCE(MiddleName, ''), ' ', LastName) AS Name,ImagePath,DOB,DeathDate,
			CASE 
				WHEN DeathDate IS NOT NULL 
				THEN DATEFROMPARTS(YEAR(GETDATE()) + 
					CASE WHEN DATEFROMPARTS(YEAR(GETDATE()), MONTH(DeathDate), DAY(DeathDate)) < GETDATE() THEN 1 ELSE 0 END, 
					MONTH(DeathDate), DAY(DeathDate))
				WHEN DOB IS NOT NULL 
				THEN DATEFROMPARTS(YEAR(GETDATE()) + 
					CASE WHEN DATEFROMPARTS(YEAR(GETDATE()), MONTH(DOB), DAY(DOB)) < GETDATE() THEN 1 ELSE 0 END, 
					MONTH(DOB), DAY(DOB))
			END AS NextAnniversary FROM tblFamilyTreeSetup WITH (NOLOCK) WHERE DOB IS NOT NULL OR DeathDate IS NOT NULL
			)
			SELECT TOP 6
				Id,Name,ImagePath,DOB,DeathDate,DATEDIFF(DAY, GETDATE(), NextAnniversary) AS DaysLeft,
				CASE 
					WHEN DeathDate IS NOT NULL AND NextAnniversary IS NOT NULL THEN 'Death Anniversary'
					WHEN DOB IS NOT NULL AND NextAnniversary IS NOT NULL THEN 'Birth Anniversary'
					ELSE NULL
				END AS AnniversaryType FROM UpcomingAnniversaries WHERE NextAnniversary IS NOT NULL ORDER BY DaysLeft;
	END

	IF @Flag = 'B' -- for latest blogs post
	BEGIN
		SELECT TOP 5 Id,Title,ImagePath,CAST(CreatedDate AS DATE) AS CreatedDate,
		CASE
        WHEN DATEDIFF(SECOND, CreatedDate, GETDATE()) < 60
            THEN CAST(DATEDIFF(SECOND, CreatedDate, GETDATE()) AS VARCHAR(10)) + ' seconds ago'
        WHEN DATEDIFF(MINUTE, CreatedDate, GETDATE()) < 60
            THEN CAST(DATEDIFF(MINUTE, CreatedDate, GETDATE()) AS VARCHAR(10)) + ' minutes ago'
        WHEN DATEDIFF(HOUR, CreatedDate, GETDATE()) < 24
            THEN CAST(DATEDIFF(HOUR, CreatedDate, GETDATE()) AS VARCHAR(10)) + ' hours ago'
        ELSE CAST(DATEDIFF(DAY, CreatedDate, GETDATE()) AS VARCHAR(10)) + ' days ago'
		END AS TimeAgo
		FROM tblBlogsPost ORDER BY CreatedDate DESC;
	END

	IF @Flag = 'F' -- headers
	BEGIN
		select N'टन्डन - वंशावली' as FamilyTree ,N'टन्डन -परिवार सदस्य' as FamilyDictionary, N'महत्वपूर्ण घटनाहरू' as FamilyTimeLine, N'टण्डनको इतिहास' as FamilyHistory
	END
END

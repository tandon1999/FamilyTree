CREATE PROCEDURE [dbo].[SpFamilyTreeSetup]
@Flag char = null,
@Id int  = null,
@FirstName nvarchar(400) = null,
@MiddleName nvarchar(400) = null,
@LastName nvarchar(400) = null,
@DOB datetime = null,
@Age int = null,
@Gender int  = null,
@Occupation int  = null,
@FatherName int = null,
@MotherName int = null,
@Description nvarchar(max) = null,
@DeathDate datetime = null,
@MatrialStatus int = null,
@NumberOfChildren int = null,
@ImagePath nvarchar(max) = null,
@Address nvarchar(max) = null,
@WIfeId int =null,
@HusbandId int = null,
@GenerationType int = null,
@PhoneNumber varchar(15) = null
AS 
BEGIN
	IF @flag = 'C' -- CREATE  OR UPDATE A FAMILY MEMEBER DETAILS
	BEGIN
		IF @Id = 0
		BEGIN
			INSERT INTO tblFamilyTreeSetup(FirstName, MiddleName, LastName, DOB, Age, Gender, Occupation, FatherName, 
			MotherName, [Description], DeathDate, CreatedDate, MatrialStatus, NumberOfChildren,ImagePath,Address,WIfeId,HusbandId,GenerationType,PhoneNumber)
			values(@FirstName,@MiddleName,@LastName,@DOB,@Age,@Gender,@Occupation,@FatherName,@MotherName,@Description,
			@DeathDate,GETDATE(),@MatrialStatus,@NumberOfChildren,@ImagePath,@Address,@WIfeId,@HusbandId,@GenerationType,@PhoneNumber)
			SELECT 'Member Detail Added Successfully!' as Messages;
		END
		ELSE
		BEGIN
			UPDATE tblFamilyTreeSetup  
			SET  FirstName = @FirstName,  MiddleName = @MiddleName,  LastName = @LastName,  DOB = @DOB,  Age = @Age,  Gender = @Gender,  Occupation = @Occupation,  
			FatherName = @FatherName,  MotherName = @MotherName,  [Description] = @Description,  DeathDate = @DeathDate,  MatrialStatus = @MatrialStatus,PhoneNumber=@PhoneNumber  ,
			NumberOfChildren = @NumberOfChildren,  ModifiedDate = GETDATE(),Address=@Address, WIfeId=@WIfeId,HusbandId=@HusbandId,ImagePath=@ImagePath,GenerationType=@GenerationType 
			WHERE Id = @Id;  
			SELECT 'Member Detail Updated Successfully!' as Messages;
		END
	END

	IF @Flag = 'D' -- DELETE THE FAMILY MEMEBER DETAILS
	BEGIN
		BEGIN TRY
		BEGIN TRAN
			Delete from tblFamilyTreeSetup where Id =@Id
			SELECT 'Member Detail Deleted Successfully!' as Messages;
			COMMIT
		END TRY
		BEGIN CATCH
			ROLLBACK
			SELECT  ERROR_MESSAGE() AS MESSAGE , 0 AS SUCCEEDED;
		END CATCH
	END

	IF @Flag = 'I' -- GET A FAMILY MEMBER DETAIL BY ID
	BEGIN
		select Id,FirstName,MiddleName,LastName,DOB,Gender,Occupation,FatherName,MotherName,Description,DeathDate,MatrialStatus,NumberOfChildren,ImagePath,Address,WIfeId,PhoneNumber,
		HusbandId,CASE WHEN DeathDate IS NULL THEN DATEDIFF(YEAR,DOB,GETDATE()) ELSE DATEDIFF(YEAR,DOB,DeathDate) END as Age,GenerationType from tblFamilyTreeSetup with(nolock) where Id = @Id;
	END

	IF @flag = 'G' -- GET ALL FAMILY MEMBER DETAILS 
	BEGIN
		select FT.Id,CONCAT(FT.FirstName,' ',FT.MiddleName,' ',FT.LastName) as FullName,FT.FirstName,FT.MiddleName,FT.LastName,Cast(FT.DOB as date) as DOB,
		CASE WHEN FT.DeathDate IS NULL THEN DATEDIFF(year, FT.DOB, GETDATE()) ELSE DATEDIFF(year, FT.DOB, FT.DeathDate) END AS Age,G.GenderName,O.OccupationType,
		FT.Description,FT.DeathDate,Ms.MatrialStatus,CONCAT(FT1.FirstName,' ',FT1.MiddleName,' ',FT1.LastName) as FatherName,FT.GenerationType as GenerationId,gg.GenerationType,
		CONCAT(FT2.FirstName,' ',FT2.MiddleName,' ',FT2.LastName) as MotherName,FT.Address,FT.ImagePath,NepDOB.NepaliYear AS NepDOBYear,
		CASE WHEN NepDeathDate.NepaliYear IS NULL then 'Present' else CAST(NepDeathDate.NepaliYear AS VARCHAR(20)) end AS NepDeathYear,FT.PhoneNumber
		from tblFamilyTreeSetup FT with(nolock) 
		join tblGender G with(nolock) on G.Id= FT.Gender
		join tblOccupation O with(nolock) on O.Id= FT.Occupation
		join tblMatrialStatus MS with(nolock) on MS.Id= FT.MatrialStatus
		left join tblgeneration gg with(nolock) on gg.Id= FT.GenerationType
		left join tblFamilyTreeSetup FT1 with(nolock) on FT.FatherName= FT1.Id
		left join tblFamilyTreeSetup FT2 with(nolock) on FT.MotherName= FT2.Id
		CROSS APPLY dbo.EngToNepColumnWise(FT.DOB) AS NepDOB
		CROSS APPLY dbo.EngToNepColumnWise(FT.DeathDate) AS NepDeathDate
		where ft.GenerationType=IIF(@GenerationType=0,ft.GenerationType,@GenerationType)
		order by ft.DOB 
		
	END

	If @Flag = 'F' -- FOR THE IMAGE SOURCE
	Begin
		select 'Tandan' as LastName 
	end

	IF @Flag = 'T' -- GET DATA FOR THE TIMELINE
	BEGIN 
		--;with cte as
		--	(
		--	select Id,CONCAT(FirstName,' ',MiddleName,' ',LastName,' ', N'जन्मिएका थिए') as FullName,cast(DOB as Date) as Date,ImagePath,1 as Type from tblFamilyTreeSetup where Gender = 1 and DOB is not null
		--	union all
		--	select Id,CONCAT(FirstName,' ',MiddleName,' ',LastName,' ', N'जन्मियेकि थिन') as FullName,cast(DOB as Date) as Date,ImagePath,1 as Type from tblFamilyTreeSetup where Gender = 2 and DOB is not null
		--	union all
		--	select Id,CONCAT(FirstName,' ',MiddleName,' ',LastName,N'को',' ', N'मृत्यु भएको थियो') as FullName, cast(DeathDate as Date) as Date,ImagePath,2 as Type from tblFamilyTreeSetup  where DeathDate is not null 
		--	)
		--	select * from cte order by Date 
				;with cte as
			(
			select Id,CONCAT(FirstName,' ',MiddleName,' ',LastName,' ', N'was born') as FullName,cast(DOB as Date) as Date,ImagePath,1 as Type from tblFamilyTreeSetup where Gender = 1 and DOB is not null
			union all
			select Id,CONCAT(FirstName,' ',MiddleName,' ',LastName,' ', N'was born') as FullName,cast(DOB as Date) as Date,ImagePath,1 as Type from tblFamilyTreeSetup where Gender = 2 and DOB is not null
			union all
			select Id,CONCAT(FirstName,' ',MiddleName,' ',LastName,' ', N'had died') as FullName, cast(DeathDate as Date) as Date,ImagePath,2 as Type from tblFamilyTreeSetup  where DeathDate is not null 
			)
			select * from cte order by Date 
	END

	IF @flag = 'U' -- GET ALL FAMILY MEMBER DETAILS UI
	BEGIN
		DECLARE @TargetId INT;	
		IF (@Id = 0)
		BEGIN
		    DECLARE @oldestPersonId INT;
		    SELECT TOP 1 @oldestPersonId = Id
		    FROM tblFamilyTreeSetup
		    ORDER BY DOB ASC;
		    SET @TargetId = @oldestPersonId;
		END
		ELSE
		BEGIN
		    SET @TargetId = @Id;
		END
		
		SELECT @WifeId = WifeId
		FROM tblFamilyTreeSetup
		WHERE Id = @TargetId;
		
		IF (@WifeId IS NOT NULL AND @WifeId != 0)
		BEGIN
		    SELECT
		        FT.Id,
		        CONCAT(FT.FirstName, ' ', ISNULL(FT.MiddleName, ''), ' ', FT.LastName) AS FullName,
		        CAST(FT.DOB AS DATE) AS DOB,
		        FT.DeathDate,
		        FT.ImagePath,
		        FT.GenerationType AS GenerationId,
		        gg.GenerationType,
		        G.Id AS GenderId,
				 CASE when ft.FatherName =0 and ft.MotherName = 0 and ft.Gender =2 then 0 else 1 end as HasWife ,
		        CASE
		            WHEN FT.Id = @TargetId THEN 1 -- Self
		            WHEN FT.Id = @WifeId THEN 2 -- Wife
		            WHEN (FT.FatherName = @TargetId OR FT.MotherName = @TargetId) AND FT.Gender = 1 THEN 3 -- Son
		            WHEN (FT.FatherName = @TargetId OR FT.MotherName = @TargetId) AND FT.Gender = 2 THEN 4 -- Daughter
		            WHEN (FT.FatherName = @WifeId OR FT.MotherName = @WifeId) AND FT.Gender = 1 THEN 3 -- Son of wife
		            WHEN (FT.FatherName = @WifeId OR FT.MotherName = @WifeId) AND FT.Gender = 2 THEN 4 -- Daughter of wife
		            ELSE 0 -- other
		        END AS IdentificationID,
		        CASE
		            WHEN FT.Id = @WifeId THEN 'Wife'
		            WHEN (FT.FatherName = @TargetId OR FT.MotherName = @TargetId) AND FT.Gender = 1 THEN 'Son'
		            WHEN (FT.FatherName = @TargetId OR FT.MotherName = @TargetId) AND FT.Gender = 2 THEN 'Daughter'
		            WHEN (FT.FatherName = @WifeId OR FT.MotherName = @WifeId) AND FT.Gender = 1 THEN 'Son'
		            WHEN (FT.FatherName = @WifeId OR FT.MotherName = @WifeId) AND FT.Gender = 2 THEN 'Daughter'
		            ELSE 'Self'
		        END AS Identification
		    FROM
		        tblFamilyTreeSetup FT WITH (NOLOCK)
		    JOIN
		        tblGender G WITH (NOLOCK) ON G.Id = FT.Gender
		    LEFT JOIN
		        tblGeneration gg WITH (NOLOCK) ON gg.Id = FT.GenerationType
		    WHERE
		        FT.Id = @TargetId OR FT.FatherName = @TargetId OR FT.MotherName = @TargetId OR FT.WifeId = @TargetId OR FT.HusbandId = @TargetId OR FT.Id = @WifeId OR FT.FatherName = @WifeId OR FT.MotherName = @WifeId OR FT.WifeId = @WifeId OR FT.HusbandId = @WifeId
		    ORDER BY
		        DOB;
		END
		ELSE
		BEGIN
		    SELECT
		        FT.Id,
		        CONCAT(FT.FirstName, ' ', ISNULL(FT.MiddleName, ''), ' ', FT.LastName) AS FullName,
		        CAST(FT.DOB AS DATE) AS DOB,
		        FT.DeathDate,
		        FT.ImagePath,
		        FT.GenerationType AS GenerationId,
		        gg.GenerationType,
		        G.Id AS GenderId,
				 CASE when ft.FatherName =0 and ft.MotherName = 0 and ft.Gender =2 then 0 else 1 end as HasWife ,
		        CASE
		            WHEN FT.Id = @TargetId THEN 1 -- Self
		            WHEN (FT.FatherName = @TargetId OR FT.MotherName = @TargetId) AND FT.Gender = 1 THEN 3 -- Son
		            WHEN (FT.FatherName = @TargetId OR FT.MotherName = @TargetId) AND FT.Gender = 2 THEN 4 -- Daughter
		            ELSE 0 -- other
		        END AS IdentificationID,
		        CASE
		            WHEN FT.Id = @WifeId THEN 'Wife'
		            WHEN (FT.FatherName = @TargetId OR FT.MotherName = @TargetId) AND FT.Gender = 1 THEN 'Son'
		            WHEN (FT.FatherName = @TargetId OR FT.MotherName = @TargetId) AND FT.Gender = 2 THEN 'Daughter'
		            WHEN (FT.FatherName = @WifeId OR FT.MotherName = @WifeId) AND FT.Gender = 1 THEN 'Son'
		            WHEN (FT.FatherName = @WifeId OR FT.MotherName = @WifeId) AND FT.Gender = 2 THEN 'Daughter'
		            ELSE 'Self'
		        END AS Identification
		    FROM
		        tblFamilyTreeSetup FT WITH (NOLOCK)
		    JOIN
		        tblGender G WITH (NOLOCK) ON G.Id = FT.Gender
		    LEFT JOIN
		        tblGeneration gg WITH (NOLOCK) ON gg.Id = FT.GenerationType
		    WHERE
		        FT.Id = @TargetId OR FT.FatherName = @TargetId OR FT.MotherName = @TargetId OR FT.WifeId = @TargetId OR FT.HusbandId = @TargetId
		    ORDER BY
		        DOB;
		END;
	END

	IF @Flag = 'A' -- get data for family details by id
	BEGIN
		select FT.Id,CONCAT(FT.FirstName,' ',FT.MiddleName,' ',FT.LastName) as FullName,FT.FirstName,FT.MiddleName,FT.LastName,Cast(FT.DOB as date) as DOB,FT.GenerationType as GenerationId,gg.GenerationType,
		CASE WHEN FT.DeathDate IS NULL THEN DATEDIFF(year, FT.DOB, GETDATE()) ELSE DATEDIFF(year, FT.DOB, FT.DeathDate) END AS Age,g.Id as GenderId,ft.NumberOfChildren,FT.PhoneNumber,
		G.GenderName,O.OccupationType,FT.Description,FT.DeathDate,Ms.MatrialStatus,CONCAT(FT1.FirstName,' ',FT1.MiddleName,' ',FT1.LastName) as FatherName,FT1.Id as FatherId,
		CONCAT(FT2.FirstName,' ',FT2.MiddleName,' ',FT2.LastName) as MotherName,FT2.Id as MotherId,FT.Address,FT.ImagePath,NepDOB.NepaliYear AS NepDOBYear,NepDeathDate.NepaliYear AS NepDeathYear,
		CASE WHEN FT.DeathDate IS NOT NULL THEN 1 ELSE 0 END AS HasValue,    CONCAT(Wife.FirstName, ' ', Wife.MiddleName, ' ', Wife.LastName) AS WifeName,Wife.Id as WifeId,
		CONCAT(Husband.FirstName, ' ', Husband.MiddleName, ' ', Husband.LastName) AS HusbandName,Husband.Id as HusbandId,
		(SELECT STRING_AGG(CONCAT(Child.FirstName, ' ', ISNULL(Child.MiddleName, ''), ' ', Child.LastName), ', ')
		FROM tblFamilyTreeSetup Child
		WHERE (Child.FatherName = FT.Id OR Child.MotherName = FT.Id) AND Child.Gender = (SELECT Id from tblGender where Id = 1)) AS SonNames,
		(SELECT STRING_AGG(CONCAT(Child.FirstName, ' ', ISNULL(Child.MiddleName, ''), ' ', Child.LastName), ', ')
		FROM tblFamilyTreeSetup Child
		WHERE (Child.FatherName = FT.Id OR Child.MotherName = FT.Id) AND Child.Gender = (SELECT Id from tblGender where Id = 2)) AS DaughterNames,
		(SELECT STRING_AGG(CAST(Child.Id AS VARCHAR(10)), ', ')
		FROM tblFamilyTreeSetup Child
		WHERE (Child.FatherName = FT.Id OR Child.MotherName = FT.Id) AND Child.Gender = (SELECT Id from tblGender where Id = 1)) AS SonIds,
		(SELECT STRING_AGG(CAST(Child.Id AS VARCHAR(10)), ', ')
		FROM tblFamilyTreeSetup Child
		WHERE (Child.FatherName = FT.Id OR Child.MotherName = FT.Id) AND Child.Gender = (SELECT Id from tblGender where Id = 2)) AS DaughterIds
		from tblFamilyTreeSetup FT with(nolock) 
		join tblGender G with(nolock) on G.Id= FT.Gender
		join tblOccupation O with(nolock) on O.Id= FT.Occupation
		join tblMatrialStatus MS with(nolock) on MS.Id= FT.MatrialStatus
		left join tblgeneration gg with(nolock) on gg.Id= FT.GenerationType
		left join tblFamilyTreeSetup FT1 with(nolock) on FT.FatherName= FT1.Id
		left join tblFamilyTreeSetup FT2 with(nolock) on FT.MotherName= FT2.Id
		LEFT JOIN tblFamilyTreeSetup Wife WITH (NOLOCK) ON FT.WifeId = Wife.Id
		LEFT JOIN tblFamilyTreeSetup Husband WITH (NOLOCK) ON FT.HusbandId = Husband.Id
		CROSS APPLY dbo.EngToNepColumnWise(FT.DeathDate) AS NepDeathDate
		CROSS APPLY dbo.EngToNepColumnWise(FT.DOB) AS NepDOB
		where FT.Id=@Id 
	END
END



 

GO
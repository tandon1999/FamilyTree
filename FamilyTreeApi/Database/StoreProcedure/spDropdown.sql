CREATE   PROCEDURE [dbo].[spDropdown]
@ddlType nvarchar(200)=null,
@Filter1 nvarchar(max)=null,
@Filter2 nvarchar(max)=null
AS
BEGIN	
	IF @ddlType='tblgender'
	BEGIN		
		SELECT Id as Id ,GenderName as Value  FROM tblGender WITH(NOLOCK)
	END	
	
	ELSE IF @ddlType='tblMatrialStatus'
	BEGIN		
		SELECT Id as Id ,MatrialStatus as Value  FROM tblMatrialStatus WITH(NOLOCK)
	END

	ELSE IF @ddlType='tblOccupation'
	BEGIN		
		SELECT Id as Id ,OccupationType as Value  FROM tblOccupation WITH(NOLOCK)
	END
	ELSE IF @ddlType='tblfather'
	BEGIN		
		SELECT Id as Id ,CONCAT(FirstName,' ',MiddleName,' ',LastName) as Value  FROM tblFamilyTreeSetup WITH(NOLOCK)
		where Gender=1 order by DOB;
	END 

	ELSE IF @ddlType='tblMother'
	BEGIN		
		SELECT Id as Id ,CONCAT(FirstName,' ',MiddleName,' ',LastName) as Value  FROM tblFamilyTreeSetup WITH(NOLOCK)
		where Gender=2 order by DOB;
	END

	ELSE IF @ddlType='tblCategory'
	BEGIN		
		SELECT Id as Id ,Name as Value  FROM tblCategory WITH(NOLOCK)
	END

	ELSE IF @ddlType='tblhusband'
	BEGIN		
		SELECT Id as Id ,CONCAT(FirstName,' ',MiddleName,' ',LastName) as Value  FROM tblFamilyTreeSetup WITH(NOLOCK) where Gender= 1 order by DOB
	END

	ELSE IF @ddlType='tblwife'
	BEGIN		
		SELECT Id as Id ,CONCAT(FirstName,' ',MiddleName,' ',LastName) as Value  FROM tblFamilyTreeSetup WITH(NOLOCK) where Gender=2 order by DOB
	END
	ELSE IF @ddlType = 'tblgeneration'
	BEGIN
		select Id , generationType as Value from tblgeneration
	END

END
GO
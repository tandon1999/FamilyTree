CREATE   PROCEDURE [dbo].[spGallerySetup] 
@Flag char = null,
@Id int= null,
@allimagedetails nvarchar(max) = null
AS
BEGIN
	If @Flag='C'
	BEGIN
		SELECT Id,PhotoName, Category, Description, DateofPhoto,ImagePath
		INTO #temp1
		FROM OPENJSON(@allimagedetails)
		WITH (
			Id int '$.Id',
			PhotoName NVARCHAR(400) '$.PhotoName',
			Category INT '$.Category',
			Description NVARCHAR(MAX) '$.Description',
			DateofPhoto varchar(10) '$.DateofPhoto',
			ImagePath nvarchar(max) '$.ImagePath'
		);
		delete from tblGallerys where Id in (select Id from #temp1);
		INSERT INTO tblGallerys (PhotoName, Category, Description, DateofPhoto, CreatedDate,ImagePath)
		SELECT PhotoName, Category, Description, DateofPhoto, GETDATE(),ImagePath FROM #temp1;
		DROP TABLE #temp1;
		SELECT 'A new photo has been added to the gallery' AS Messages;
	END

	IF @Flag = 'G'
	BEGIN
		select g.Id,PhotoName,Description,Cast(DateofPhoto as Date) as DateofPhoto,ImagePath,c.Name as CategoryName from tblGallerys g
		join tblCategory c with(nolock) on c.Id= g.Category order by DateofPhoto
	END

	IF @Flag='D'
	BEGIN
		delete from tblGallerys where Id=@Id;
		SELECT 'Deleted Successfully!!' AS Messages;
	END

	IF @Flag= 'I'
	BEGIN
		select * from tblGallerys where Id= @Id;
	END
END

GO
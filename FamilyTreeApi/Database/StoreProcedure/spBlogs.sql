CREATE   PROCEDURE [dbo].[spBlogs]
    @Flag CHAR = NULL,
    @Id INT = NULL,
    @Title NVARCHAR(500) = NULL,
    @Content NVARCHAR(MAX) = NULL,
    @ImagePath NVARCHAR(MAX) = NULL,
    @ReadMoreLink NVARCHAR(200) = NULL,
	@CreatedDate datetime = null

AS
BEGIN
        IF @Flag = 'C' -- Create
        BEGIN
			IF @Id = 0
			BEGIN
				INSERT INTO tblBlogsPost (Title, Content, ImagePath, CreatedDate) VALUES (@Title, @Content, @ImagePath, ISNULL(@createdDate,GETDATE()));
				SELECT 'Blogs Post Added Successfully' AS Messages;
			END
			ELSE
				BEGIN
					UPDATE tblBlogsPost SET Title = @Title,Content = @Content,ImagePath = @ImagePath WHERE Id = @Id;
					SELECT 'Blogs Post Updated Successfully' AS Messages;
				END
		END

        IF @Flag = 'G' -- GetAll
        BEGIN
            SELECT Id, Title, Content, ImagePath, CAST(CreatedDate AS DATE) AS CreatedDate
            FROM tblBlogsPost WITH (NOLOCK) order by CreatedDate desc;
        END
        IF @Flag = 'I' -- Get by Id
        BEGIN
            SELECT Id, Title, Content, ImagePath, CreatedDate
            FROM tblBlogsPost
            WHERE Id = @Id;
        END
        IF @Flag = 'D' -- Delete
        BEGIN
            DELETE FROM tblBlogsPost
            WHERE Id = @Id;
            SELECT 'Blogs Post Deleted Successfully' AS Messages;
        END
END
GO
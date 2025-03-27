create   proc [dbo].[spHistory]
@Flag char = null
AS 
BEGIN
	IF @Flag = 'H'
	BEGIN 	
		select N'<b>टण्डन</b>  को इतिहास' as Title, N'नेपालमा <strong>टण्डन</strong> समुदायको विरासत र योगदानको अन्वेषण गर्दै।' as SubTitle, N'<strong>टण्डन</strong> कुलको उत्पत्ति' as Title3
	END
END 
GO
/****** Object:  StoredProcedure [dbo].[spLogin]    Script Date: 3/27/2025 11:50:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
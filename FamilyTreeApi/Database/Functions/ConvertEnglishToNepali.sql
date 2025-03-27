CREATE   FUNCTION [dbo].[ConvertEnglishToNepali]
(
    @engDate DATE
)
RETURNS TABLE
AS
RETURN
(
    WITH NepaliDate AS
    (
        SELECT
            CASE 
                WHEN YEAR(@engDate) >= 1700 THEN (YEAR(@engDate) + 57) -- Adjust year
                ELSE NULL
            END AS NepaliYear,
            CASE 
                WHEN MONTH(@engDate) = 1 THEN 9  -- Mapping January to Poush/Magh
                WHEN MONTH(@engDate) = 2 THEN 10 -- Mapping February to Magh/Falgun
                WHEN MONTH(@engDate) = 3 THEN 11 -- Mapping March to Falgun/Chaitra
                WHEN MONTH(@engDate) = 4 THEN 12 -- Mapping April to Chaitra/Baishakh
                WHEN MONTH(@engDate) = 5 THEN 1  -- Mapping May to Baishakh/Jestha
                WHEN MONTH(@engDate) = 6 THEN 2  -- Mapping June to Jestha/Asar
                WHEN MONTH(@engDate) = 7 THEN 3  -- Mapping July to Asar/Shrawan
                WHEN MONTH(@engDate) = 8 THEN 4  -- Mapping August to Shrawan/Bhadra
                WHEN MONTH(@engDate) = 9 THEN 5  -- Mapping September to Bhadra/Ashwin
                WHEN MONTH(@engDate) = 10 THEN 6 -- Mapping October to Ashwin/Kartik
                WHEN MONTH(@engDate) = 11 THEN 7 -- Mapping November to Kartik/Mangsir
                WHEN MONTH(@engDate) = 12 THEN 8 -- Mapping December to Mangsir/Poush
                ELSE NULL
            END AS NepaliMonth,
            DAY(@engDate) AS NepaliDay
    )
    SELECT CONCAT(NepaliYear,'-',NepaliMonth,'-',NepaliDay) as NepaliDate FROM NepaliDate
);
GO
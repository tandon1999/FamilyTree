CREATE   FUNCTION [dbo].[EngToNepColumnWise]
(
    @engDate DATE
)
RETURNS TABLE
AS
RETURN
(
    -- Adjust the logic for correct conversion from Gregorian to Bikram Sambat
    WITH NepaliDate AS
    (
        SELECT
            -- Adjust the year calculation by adding 57 years (or 56 depending on the year)
            CASE
                WHEN MONTH(@engDate) >= 4 -- From April to December
                    THEN (YEAR(@engDate) + 57) -- Bikram Sambat year +57 starts from mid-April
                ELSE (YEAR(@engDate) + 57) -- Add 56 for January to March
            END AS NepaliYear,

            -- Adjust month based on complex logic of Nepali calendar
            CASE
                WHEN MONTH(@engDate) = 1 THEN 9  -- January -> Poush/Magh
                WHEN MONTH(@engDate) = 2 THEN 10 -- February -> Magh/Falgun
                WHEN MONTH(@engDate) = 3 THEN 11 -- March -> Falgun/Chaitra
                WHEN MONTH(@engDate) = 4 THEN 12 -- April -> Chaitra/Baishakh
                WHEN MONTH(@engDate) = 5 THEN 1  -- May -> Baishakh/Jestha
                WHEN MONTH(@engDate) = 6 THEN 2  -- June -> Jestha/Asar
                WHEN MONTH(@engDate) = 7 THEN 3  -- July -> Asar/Shrawan
                WHEN MONTH(@engDate) = 8 THEN 4  -- August -> Shrawan/Bhadra
                WHEN MONTH(@engDate) = 9 THEN 5  -- September -> Bhadra/Ashwin
                WHEN MONTH(@engDate) = 10 THEN 6 -- October -> Ashwin/Kartik
                WHEN MONTH(@engDate) = 11 THEN 7 -- November -> Kartik/Mangsir
                WHEN MONTH(@engDate) = 12 THEN 8 -- December -> Mangsir/Poush
                ELSE NULL
            END AS NepaliMonth,

            -- Adjust day logic (Note: For simplicity, we just take the day part directly here)
            DAY(@engDate) AS NepaliDay
    )

    -- Return the calculated Nepali date values
    SELECT NepaliYear, NepaliMonth, NepaliDay
    FROM NepaliDate
);
GO
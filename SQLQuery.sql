SELECT Top 5
    g.GameID,
    g.Name AS GameName,
    g.ReleaseDate,
    g.Price AS BasePrice,
    g.MetacriticScore,

    -- Developer(s)
    (SELECT STRING_AGG(d.Name, ', ') 
     FROM GameDevelopers gd 
     JOIN Developers d ON gd.DeveloperID = d.DeveloperID
     WHERE gd.GameID = g.GameID) AS Developers,

    -- Publisher(s)
    (SELECT STRING_AGG(p.Name, ', ') 
     FROM GamePublishers gp 
     JOIN Publishers p ON gp.PublisherID = p.PublisherID
     WHERE gp.GameID = g.GameID) AS Publishers,

    -- Number of Available Packages
    (SELECT COUNT(*) FROM Packages pk WHERE pk.GameID = g.GameID) AS PackageCount,

    -- Minimum Package Price
    (SELECT MIN(pk.Price) FROM Packages pk WHERE pk.GameID = g.GameID) AS MinPackagePrice

FROM Games g

ORDER BY g.ReleaseDate DESC

select top 1 * from Publishers

insert into Publishers(PublisherID,Name) values (1, 'Activision')

Select *
from Games G 
Join Packages P on P.GameID = G.GameID


insert into Packages(GameID,Title,Price) 
Select 
ABS(
checksum(NewID())) % 90000 +1 As GameID,
    'Package ' + CAST(ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS NVARCHAR) AS Title,
    CAST(RAND(CHECKSUM(NEWID())) * 100 AS DECIMAL(10,2)) AS Price
    FROM (SELECT TOP 1000 1 AS X FROM master.dbo.spt_values) AS T


 SELECT 
            *,
            S.ScreenshotID, S.GameID, S.URL
        FROM 
            (SELECT TOP 3 * FROM Games ORDER BY GameID ASC) AS G
        LEFT JOIN Screenshots S ON S.GameID = G.GameID
        ORDER BY G.GameID ASC

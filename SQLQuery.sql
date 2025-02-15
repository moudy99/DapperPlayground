SELECT 
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
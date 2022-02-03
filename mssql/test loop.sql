DECLARE @nombase varchar(255)
CREATE TABLE #TYPEBASE
(
    nom varchar(50)
);
INSERT INTO #TYPEBASE
    (nom)
VALUES
    ('TEST')
INSERT INTO #TYPEBASE
    (nom)
VALUES
    ('PROD')

DECLARE moncurseur CURSOR FOR SELECT
    nom
FROM
    #TYPEBASE

OPEN moncurseur

FETCH moncurseur INTO @nombase

WHILE @@FETCH_STATUS = 0
BEGIN
PRINT @nombase
FETCH moncurseur INTO @nombase
    END





DROP TABLE #TYPEBASE

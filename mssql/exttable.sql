SELECT REPLACE(N_EXTTABLE, 'IWSTEST', 'IWSREPRISE'),* FROM suiteisilog.EXTTABLE
where N_EXTTABLE like 'Provider=SQLOLEDB.1%'

use iwsreprise
go
DECLARE @Racine_Matrice varchar(255)
SET @Racine_Matrice = (SELECT DB_NAME() AS [Current Database]  ) 
UPDATE suiteisilog.EXTTABLE 
SET N_EXTTABLE = REPLACE(N_EXTTABLE, 'TOTO', @Racine_Matrice)
where C_MODELE='DOSSIERS_DOTATION'


use iwsreprise
DECLARE @Racine_Matrice varchar(255)
SET @Racine_Matrice = (SELECT DB_NAME() AS [Current Database]  )  
SELECT @Racine_Matrice
go


SELECT C_MODELE,N_EXTTABLE from suiteisilog.EXTTABLE
where N_EXTTABLE like '%SQLOLEDB%'

SELECT * from suiteisilog.JMD

SELECT
    *
INTO JMD
FROM
    OPENQUERY (OCS, 'SELECT S.NAME ,S.VERSION , S.PUBLISHER,
                                  B.ID, S.ID AS TOTO, B.NAME AS NOM
                         FROM   softwares S
                                 INNER JOIN hardware B
                                         ON S.HARDWARE_ID = B.ID ' ) L 


DROP TABLE JMD

UPDATE suiteisilog.EXTTABLE 
SET N_EXTTABLE = REPLACE(N_EXTTABLE, '-T', '-P')
where N_EXTTABLE like '%-T%'
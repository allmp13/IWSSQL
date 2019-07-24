
use GENHABLGME
drop table if EXISTS #metier
drop table if EXISTS #tmp
DELETE FROM HABILITATION;
DELETE FROM HABILITATIONS_TYPE;
DELETE FROM HABILITATIONS_COMPLEMENTAIRES

delete GENHABLGME.dbo.UTILISATEURS

bulk insert GENHABLGME.dbo.UTILISATEURS from 'c:\temp\EXPORT_RH_UTILISATEURS.csv' WITH (FORMAT='CSV',FIELDTERMINATOR=';',FIRSTROW=2,CODEPAGE='ACP')

Create table #metier( 
 metier varchar(50)
 );

 insert into #metier (metier) values ('LGME - EQUIPE COMPETENTE GSIC.xlsx')
 insert into #metier (metier) values ('LGME - EQUIPE COMPETENTE GPAT.xlsx')
 insert into #metier (metier) values ('LGME - EQUIPE COMPETENTE GTL.xlsx')
 insert into #metier (metier) values ('LGME - EQUIPE COMPETENTE BIO.xlsx')
 insert into #metier (metier) values ('DONNEES V2.xlsx')

declare @metier varchar(50)
declare moncurseur cursor for select metier from #metier

open moncurseur

fetch moncurseur into @metier

while @@FETCH_STATUS = 0
begin
set @metier = '''C:\temp\' + @metier +''''
print @metier

if exists(select * from sys.servers where name='TESTJMD')
EXEC sp_dropserver
   @server = N'TESTJMD',
   @droplogins ='droplogins';

declare @query NVARCHAR(MAX) =  
    'sp_addlinkedserver
     @server = N''TESTJMD'',
     @srvproduct = N''jm'',
     @provider = N''Microsoft.ACE.OLEDB.12.0'',
     @datasrc = N' + @metier + ',
     @provstr = N''Excel 12.0'';'
EXECUTE(@query)

EXEC sp_addlinkedsrvlogin
   @rmtsrvname = N'TESTJMD',
   @useself ='false', 
   @locallogin = NULL,
   @rmtuser = NULL,
   @rmtpassword = NULL;



fetch moncurseur into @metier

 Create table #tmp
 (TABLE_QUALIFIER varchar(40),
  TABLE_OWNER varchar(20),
  TABLE_NAME varchar(40),
  COLUMN_NAME varchar(40),
  DATA_TYPE int,
  TYPE_NAME varchar(20),
  PREC int, LENGTH int,
  SCALE int, RADIX int,
  NULLABLE char(4),
  REMARKS varchar(128),
  COLUMN_DEF varchar(40),
  SQL_DATA_TYPE int,
  SQL_DATETIME_SUB int,
  CHAR_OCTET_LENGTH int,
  ORDINAL_POSITION int,
  IS_NULLABLE char(4),
  SS_DATA_TYPE int)

insert #tmp
EXEC sp_columns_ex 'TESTJMD','Matrice$'

DECLARE @cols AS NVARCHAR(MAX);

select @cols = STUFF((SELECT distinct  ',' +
                        QUOTENAME(column_name)
                      FROM #tmp
                      WHERE 
                        COLUMN_NAME <> 'initiale' 
                        AND COLUMN_NAME <> 'matricule'
						AND COLUMN_NAME <> 'nom'
						AND COLUMN_NAME <> 'prenom'
                      FOR XML PATH(''), TYPE
                     ).value('.', 'NVARCHAR(MAX)') 
                        , 1, 1, '')

SELECT @cols = SUBSTRING(@cols, 0, LEN(@cols)) 
 
declare @query2 NVARCHAR(MAX) =  
'SELECT matricule as Ligne,hab as Colonne,Valeur
FROM TESTJMD...matrice$
UNPIVOT(Valeur
      FOR hab IN (' + @cols + '])) As toto '

insert dbo.HABILITATION
EXECUTE(@query2) 
Drop table #tmp

insert INTO HABILITATIONS_TYPE (
	   [Libell� habilitation type]
	  ,[Code Equipe]
      ,[Libell� Equipe]
      ,[Code profil]
      ,[R�le dans l'�quipe]
      ,[Code Nature]
      ,[Lecture seule])

select [Libelle Habi Type]
      ,[Code Equipe]
      ,[Libelle Equipe]
      ,[Code Profil]
      ,[R�le dans l'�quipe]
      ,[Code Nature]
      ,[Lecture Seule]
from TESTJMD...Equipe$

end

INSERT INTO HABILITATIONS_COMPLEMENTAIRES
    ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO], [Code Fonction], [Libell� Fonction], [Code Grade], [Libell� grade], [Code Statut professionnel], [Libell� Statut professionnel], [Code statut d'activit�], [Libell� statut d'activit�], [Type de situation], [Libell� type de situation], [Date de d�but d'habilitation], [Date de fin d'habilitation] )
SELECT
    H.Ligne AS [Matricule RH utilisateur],
    H.Colonne AS [Code habilitation type],
    HT.[Code profil],
    U.[Site habilitation principale],
    U.[UO habilitation principale],
    U.[Code fonction habilitation principale],
    U.[Libell� fonction habilitation principale],
    U.[Code grade],
    U.[Libell� du grade],
    U.[Code statut professionnel habilitation principale],
    U.[Libell� statut professionnel habilitation principale],
    'S' AS [Code statut d'activit�],
    'Situation Secondaire' AS [Libell� statut d'activit�],
    'A' AS [Type de situation],
    'Actif' AS [Libell� type de situation],
    '01/01/2018' AS DateDebut,
    '31/12/2021' AS Datefin
FROM
    HABILITATIONS_TYPE HT
    INNER JOIN (UTILISATEURS U INNER JOIN HABILITATION H ON U.[Matricule RH] = H.Ligne) 
    ON HT.[Libell� habilitation type] = H.Colonne
WHERE H.Valeur IS NOT NULL 

close moncurseur
deallocate moncurseur
Drop table #metier


/*

Version 3

GENERATION DES HABILITATIONS COMPLEMENTAIRES METIERS A PARTIR DES MATRICES METIERS

MODE OPERATOIRE:

Copier les fichiers:
    \\winlgme-t\d$\ISILOG\Passerelle RH\UTILISATEURS.csv
    \\winlgme-t\d$\ISILOG\Passerelle RH\TEST_HABILITATIONS_COMPLEMENTAIRES.csv
dans \\srvmssql-f\c$\GENHABLGME

Lancer la requête SQL avec:
    SET @environnement='PROD'
puis relancer la requête SQL avec:
    SET @environnement='TEST'

Copier les fichiers:
    \\srvmssql-f\c$\GENHABLGME\PROD\HABILITATIONS_COMPLEMENTAIRES_METIER_SANS_GEST.csv
    \\srvmssql-f\c$\GENHABLGME\TEST\HABILITATIONS_COMPLEMENTAIRES_METIER_AVEC_GEST.csv
dans \\srvlgme-t\d$\ISILOG\Passerelle RH
*/

SET NOCOUNT ON
DECLARE @Racine_Matrice varchar(255)
DECLARE @sql varchar(8000)
DECLARE @metier varchar(255)
DECLARE @nomfichier varchar(255)
DECLARE @environnement varchar(255)

SET @environnement='TEST'
SET @Racine_Matrice='C:\GENHABLGME\'


PRINT 'DEBUT'

IF  OBJECT_ID('tempdb..#metier') IS NOT NULL DROP TABLE #metier
IF  OBJECT_ID('tempdb..#tmp') IS NOT NULL DROP TABLE #tmp

/* Version > 2016
DROP TABLE #metier
DROP TABLE #tmp
*/

DELETE FROM HABILITATION;
DELETE FROM HABILITATIONS_TYPE;
DELETE FROM HABILITATIONS_COMPLEMENTAIRES_SPV;
DELETE FROM HABILITATIONS_COMPLEMENTAIRES;
DELETE FROM HABILITATIONS_COMPLEMENTAIRES_EXPORT;

DELETE GENHABLGME.dbo.UTILISATEURS

/* Import Utilisateurs */

SET @nomfichier = '''' + @Racine_Matrice + 'UTILISATEURS.csv' + ''''
SET @nomfichier = 'BULK INSERT GENHABLGME.dbo.UTILISATEURS FROM ' + @nomfichier + ' WITH (FIELDTERMINATOR='';'',FIRSTROW=2,CODEPAGE=''ACP'')'
/*PRINT @nomfichier*/
EXECUTE( @nomfichier)

/*
BULK INSERT GENHABLGME.dbo.UTILISATEURS FROM 'c:\temp\UTILISATEURS.csv' WITH (/FIELDTERMINATOR=';',FIRSTROW=2,CODEPAGE='ACP')
c:\temp\EXPORT_RH_UTILISATEURS.csv 
*/


/* Import Habilitations Complémentaires SPV */

SET @nomfichier = '''' + @Racine_Matrice + 'TEST_HABILITATIONS_COMPLEMENTAIRES.csv' + ''''
SET @nomfichier = 'BULK INSERT GENHABLGME.dbo.HABILITATIONS_COMPLEMENTAIRES_SPV FROM '+ @nomfichier +' WITH (FIELDTERMINATOR='';'',FIRSTROW=2,CODEPAGE=''ACP'')'
/* PRINT @nomfichier */
EXECUTE( @nomfichier)

/*
BULK INSERT GENHABLGME.dbo.HABILITATIONS_COMPLEMENTAIRES_SPV FROM 'c:\temp\TEST_HABILITATIONS_COMPLEMENTAIRES.csv' WITH (/FIELDTERMINATOR=';',FIRSTROW=2,CODEPAGE='ACP')
*/

CREATE TABLE #metier
(
    metier varchar(50)
);

INSERT INTO #metier
    (metier)
VALUES
    ('LGME - EQUIPE COMPETENTE GSIC')
INSERT INTO #metier
    (metier)
VALUES
    ('LGME - EQUIPE COMPETENTE GPAT')
INSERT INTO #metier
    (metier)
VALUES
    ('LGME - EQUIPE COMPETENTE GTL')
INSERT INTO #metier
    (metier)
VALUES
    ('LGME - EQUIPE COMPETENTE BIO')
INSERT INTO #metier
    (metier)
VALUES
    ('LGME - EQUIPE COMPETENTE ARI')
INSERT INTO #metier
    (metier)
VALUES
    ('LGME - EQUIPE COMPETENTE MTI')
INSERT INTO #metier
    (metier)
VALUES
    ('LGME - EQUIPE COMPETENTE TRS')
INSERT INTO #metier
    (metier)
VALUES
    ('LGME - EQUIPE COMPETENTE HAB')
INSERT INTO #metier
    (metier)
VALUES
    ('LGME - EQUIPE COMPETENTE COMMUNE')

DECLARE moncurseur CURSOR FOR SELECT
    metier
FROM
    #metier

OPEN moncurseur

FETCH moncurseur INTO @metier

WHILE @@FETCH_STATUS = 0
BEGIN
    /*SET @metier = '''' + @Racine_Matrice + @metier +''''*/
    SET @metier = '''' + @Racine_Matrice + @environnement + '\' + @metier + '_' + @environnement + '.xlsx' + ''''
    PRINT @metier

    IF EXISTS(SELECT
        *
    FROM
        sys.servers
    WHERE name='TESTJMD')
EXEC sp_dropserver
   @server = N'TESTJMD',
   @droplogins ='droplogins';

    DECLARE @query NVARCHAR(MAX) =  
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

    FETCH moncurseur INTO @metier

    CREATE TABLE #tmp
    (
        TABLE_QUALIFIER   varchar(40),
        TABLE_OWNER       varchar(20),
        TABLE_NAME        varchar(40),
        COLUMN_NAME       varchar(50),
        DATA_TYPE         int,
        TYPE_NAME         varchar(20),
        PREC              int,
        LENGTH            int,
        SCALE             int,
        RADIX             int,
        NULLABLE          char(4),
        REMARKS           varchar(128),
        COLUMN_DEF        varchar(40),
        SQL_DATA_TYPE     int,
        SQL_DATETIME_SUB  int,
        CHAR_OCTET_LENGTH int,
        ORDINAL_POSITION  int,
        IS_NULLABLE       char(4),
        SS_DATA_TYPE      int
    )

    INSERT #tmp
    EXEC sp_columns_ex 'TESTJMD','Matrice$'

    DECLARE @cols AS NVARCHAR(MAX);

    SELECT
        @cols = STUFF((SELECT
            DISTINCT
            ',' +
                        QUOTENAME(column_name)
        FROM
            #tmp
        WHERE 
            COLUMN_NAME <> 'initiale'
            AND COLUMN_NAME <> 'matricule'
            AND COLUMN_NAME <> 'nom'
            AND COLUMN_NAME <> 'prenom'
        FOR XML PATH(''), TYPE
                     ).value('.', 'NVARCHAR(MAX)') 
                        , 1, 1, '')

    SELECT
        @cols = SUBSTRING(@cols, 0, LEN(@cols))

    DECLARE @query2 NVARCHAR(MAX) =  
'SELECT matricule as Ligne,hab as Colonne,Valeur
FROM TESTJMD...matrice$
UNPIVOT(Valeur
      FOR hab IN (' + @cols + '])) As toto '

    INSERT dbo.HABILITATION
    EXECUTE(@query2)
    DROP TABLE #tmp

    INSERT INTO HABILITATIONS_TYPE
        (
        [Libellé habilitation type]
        ,[Code Equipe]
        ,[Libellé Equipe]
        ,[Code profil]
        ,[Rôle dans l'équipe]
        ,[Code Nature]
        ,[Lecture seule]
        ,[Code Fonction]
        ,[Libellé Fonction]
        ,[Code Site])

    SELECT
        [Libelle Habi Type]
      ,
        [Code Equipe]
      ,
        [Libelle Equipe]
      ,
        [Code Profil]
      ,
        [Rôle dans l'équipe]
      ,
        [Code Nature]
      ,
        [Lecture Seule]
      ,
        [Code Fonction]
      ,
        [Libellé Fonction]
      ,
        [Code Site]
    FROM
        TESTJMD...Equipe$

PRINT '------------------------------------------------------'

END

/* MAJ GESTIONNAIRES*/

INSERT INTO HABILITATIONS_COMPLEMENTAIRES
    ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO], [Code Fonction], [Libellé Fonction],/*  [Code Grade], [Libellé grade], [Code Statut professionnel], [Libellé Statut professionnel],*/ [Code statut d'activité], [Libellé statut d'activité], [Type de situation], [Libellé type de situation], [Date de début d'habilitation], [Date de fin d'habilitation] )
SELECT
    H.Ligne AS [Matricule RH utilisateur],
    H.Colonne AS [Code habilitation type],
    HT.[Code profil],
    /*U.[Site habilitation principale],*/
    /*HT.[Code Site],*/
    ISNULL(HT.[Code Site],U.[Site habilitation principale]),
    U.[UO habilitation principale],
    HT.[Code fonction],
    HT.[Libellé fonction],
   /* U.[Code fonction habilitation principale],
    U.[Libellé fonction habilitation principale],
    U.[Code grade],
    U.[Libellé du grade],
    U.[Code statut professionnel habilitation principale],
    U.[Libellé statut professionnel habilitation principale],*/
    /*'S' AS [Code statut d'activité],
    'Situation Secondaire' AS [Libellé statut d'activité],*/
    U.[Code statut d activité] AS [Code statut d'activité],
    U.[Libellé statut d activité] AS [Libellé statut d'activité],
    'A' AS [Type de situation],
    'Actif' AS [Libellé type de situation],
    convert(varchar, getdate(), 103) AS DateDebut,
    convert(varchar,  DATEADD(year, 1,getdate()), 103) AS Datefin
    FROM
    (SELECT
        [Libellé habilitation type],
        [Code profil],
        [Code fonction],
        [Libellé fonction],
        [Code Site]
    FROM
        [dbo].[HABILITATIONS_TYPE]
    GROUP BY [Libellé habilitation type],[Code profil],[Code fonction],[Libellé fonction],[Code Site]) HT
    INNER JOIN (UTILISATEURS U INNER JOIN HABILITATION H ON U.[Matricule RH] = H.Ligne)
    ON HT.[Libellé habilitation type] = H.Colonne
WHERE H.Valeur IS NOT NULL


/* GENERATION DES FICHIERS CSV */

/*---------------------*/
/* HABILITATIONS TYPES */

/* AVEC GESTIONNAIRES */

/*select @sql = 'bcp GENHABLGME.dbo.HABILITATIONS_TYPE out c:\temp\HT_AVEC_GEST.csv -c -C 1252 -t";"  -T -S'+ @@servername*/
select @sql = 'bcp "select [Libellé habilitation type],[Code profil],[Code fonction],[Libellé fonction],[Code grade],[Libellé grade],[Code statut professionnel],[Libellé statut professionnel],[Code Statut d''activité],[Libellé statut d''activité],[Code Equipe],[Libellé Equipe],[Rôle dans l''équipe],[Code Nature],[Lecture seule] from GENHABLGME.dbo.HABILITATIONS_TYPE GROUP BY [Libellé habilitation type],[Code profil],[Code fonction],[Libellé fonction],[Code grade],[Libellé grade],[Code statut professionnel],[Libellé statut professionnel],[Code Statut d''activité],[Libellé statut d''activité],[Code Equipe],[Libellé Equipe],[Rôle dans l''équipe],[Code Nature],[Lecture seule]" queryout ' + @Racine_Matrice + @environnement + '\HT_AVEC_GEST.csv -c -C 1252 -t";"  -T -S'+ @@servername

exec master..xp_cmdshell @sql

select @sql = 'copy ' + @Racine_Matrice + 'ENTETE_HABTYP.csv + ' + @Racine_Matrice + @environnement + '\HT_AVEC_GEST.csv ' + @Racine_Matrice + @environnement + '\HABILITATIONS_TYPE.csv'
exec master..xp_cmdshell @sql


select @sql = 'del /F ' + @Racine_Matrice + @environnement + '\HT_AVEC_GEST.csv'
exec master..xp_cmdshell @sql




/*-------------------------------*/
/* HABILITATIONS COMPLEMENTAIRES */

/* AVEC GESTIONNAIRES */
/*
select @sql = 'bcp GENHABLGME.dbo.HABILITATIONS_COMPLEMENTAIRES out c:\temp\HC_AVEC_GEST.csv -c -C 1252 -t";"  -T -S'+ @@servername
exec master..xp_cmdshell @sql
*/
/*select @sql = 'copy c:\temp\TEST_HABILITATIONS_COMPLEMENTAIRES.csv + c:\temp\HC_AVEC_GEST.csv c:\temp\HABILITATIONS_COMPLEMENTAIRES_AVEC_GEST.csv'*/
/*
select @sql = 'copy c:\temp\HC_AVEC_GEST.csv c:\temp\HABILITATIONS_COMPLEMENTAIRES_METIER_AVEC_GEST.csv'
*/
select @sql = 'bcp GENHABLGME.dbo.HABILITATIONS_COMPLEMENTAIRES out ' + @Racine_Matrice + @environnement + '\HABILITATIONS_COMPLEMENTAIRES_METIER.csv -c -C 1252 -t";"  -T -S'+ @@servername
exec master..xp_cmdshell @sql


IF @environnement = 'TEST'
      select @sql = 'bcp GENHABLGME.dbo.HABILITATIONS_COMPLEMENTAIRES out ' + @Racine_Matrice + @environnement + '\HABILITATIONS_COMPLEMENTAIRES_METIER_AVEC_GEST.csv -c -C 1252 -t";"  -T -S'+ @@servername
ELSE 
      select @sql = 'bcp GENHABLGME.dbo.HABILITATIONS_COMPLEMENTAIRES out ' + @Racine_Matrice + @environnement + '\HABILITATIONS_COMPLEMENTAIRES_METIER_SANS_GEST.csv -c -C 1252 -t";"  -T -S'+ @@servername
exec master..xp_cmdshell @sql

select @sql = 'del /F ' + @Racine_Matrice + @environnement + '\HC_AVEC_GEST.csv'
exec master..xp_cmdshell @sql


/* Vidage Memoire*/

CLOSE moncurseur
DEALLOCATE moncurseur
DROP TABLE #metier
PRINT 'FIN'

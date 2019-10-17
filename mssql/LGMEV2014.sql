SET NOCOUNT ON
DECLARE @Racine_Matrice varchar(255)
/*Set @Racine_Matrice='\\srvlgme-t\d$\ISILOG\Passerelle RH\TESTJMD\'*/
SET @Racine_Matrice='C:\TEMP\'



PRINT 'DEBUT'
IF  OBJECT_ID('tempdb..#metier') IS NOT NULL DROP TABLE #metier
IF  OBJECT_ID('tempdb..#tmp') IS NOT NULL DROP TABLE #tmp
/* Version > 2016
DROP TABLE #metier
DROP TABLE #tmp
*/
DELETE FROM HABILITATION;
DELETE FROM HABILITATIONS_TYPE;
DELETE FROM HABILITATIONS_COMPLEMENTAIRES;
DELETE FROM HABILITATIONS_COMPLEMENTAIRES_EXPORT;

DELETE GENHABLGME.dbo.UTILISATEURS

/* Import Utilisateurs */

BULK INSERT GENHABLGME.dbo.UTILISATEURS FROM 'c:\temp\EXPORT_RH_UTILISATEURS.csv' WITH (/*FORMAT='CSV',*/FIELDTERMINATOR=';',FIRSTROW=2,CODEPAGE='ACP')


/* Import Habilitations Complémentaires */
/*
BULK INSERT GENHABLGME.dbo.HABILITATIONS_COMPLEMENTAIRES FROM 'c:\temp\EXPORT RH HABILITATIONS_COMPLEMENTAIRES.csv' WITH (/*FORMAT='CSV',*/FIELDTERMINATOR=';',FIRSTROW=2,CODEPAGE='ACP')
*/
PRINT 'PAS D''IMPORT HABILITATIONS COMPLEMENTAIRES'

CREATE TABLE #metier
(
    metier varchar(50)
);

INSERT INTO #metier
    (metier)
VALUES
    ('LGME - EQUIPE COMPETENTE GSIC.xlsx')
INSERT INTO #metier
    (metier)
VALUES
    ('LGME - EQUIPE COMPETENTE GPAT.xlsx')
INSERT INTO #metier
    (metier)
VALUES
    ('LGME - EQUIPE COMPETENTE GTL.xlsx')
INSERT INTO #metier
    (metier)
VALUES
    ('LGME - EQUIPE COMPETENTE BIO.xlsx')
INSERT INTO #metier
    (metier)
VALUES
    ('DONNEES V2.xlsx')

DECLARE @metier varchar(255)
DECLARE moncurseur CURSOR FOR SELECT
    metier
FROM
    #metier

OPEN moncurseur

FETCH moncurseur INTO @metier

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @metier = '''' + @Racine_Matrice + @metier +''''
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
        COLUMN_NAME       varchar(40),
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
        ,[Libellé Fonction])

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
    FROM
        TESTJMD...Equipe$
    /*WHERE [Code Profil] <>'GEST'
Print 'HABILITATIONS_TYPE Sans les Profils Gestionnaire'*/
PRINT '------------------------------------------------------'

END

/* MAJ GESTIONNAIRES*/

INSERT INTO HABILITATIONS_COMPLEMENTAIRES
    ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO],/* [Code Fonction], [Libellé Fonction], [Code Grade], [Libellé grade], [Code Statut professionnel], [Libellé Statut professionnel],*/ [Code statut d'activité], [Libellé statut d'activité], [Type de situation], [Libellé type de situation], [Date de début d'habilitation], [Date de fin d'habilitation] )
SELECT
    H.Ligne AS [Matricule RH utilisateur],
    H.Colonne AS [Code habilitation type],
    HT.[Code profil],
    U.[Site habilitation principale],
    U.[UO habilitation principale],
   /* U.[Code fonction habilitation principale],
    U.[Libellé fonction habilitation principale],
    U.[Code grade],
    U.[Libellé du grade],
    U.[Code statut professionnel habilitation principale],
    U.[Libellé statut professionnel habilitation principale],*/
    'S' AS [Code statut d'activité],
    'Situation Secondaire' AS [Libellé statut d'activité],
    'A' AS [Type de situation],
    'Actif' AS [Libellé type de situation],
    '01/01/2018' AS DateDebut,
    '31/12/2021' AS Datefin
FROM
    (SELECT
        [Libellé habilitation type],
        [Code profil]
    FROM
        [dbo].[HABILITATIONS_TYPE]
    GROUP BY [Libellé habilitation type],[Code profil]) HT
    INNER JOIN (UTILISATEURS U INNER JOIN HABILITATION H ON U.[Matricule RH] = H.Ligne)
    ON HT.[Libellé habilitation type] = H.Colonne
WHERE H.Valeur IS NOT NULL
/*AND HT.[Code profil] <>'GEST'
Print 'HABILITATIONS_COMPLEMENTAIRES Sans les Profils Gestionnaire'*/

/* MAJ RESPONSABLE HIERARCHIQUE NIVEAU 1*/

INSERT INTO HABILITATIONS_COMPLEMENTAIRES
    ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO],/* [Code Fonction], [Libellé Fonction], [Code Grade], [Libellé grade], [Code Statut professionnel], [Libellé Statut professionnel],*/ [Code statut d'activité], [Libellé statut d'activité], [Type de situation], [Libellé type de situation], [Date de début d'habilitation], [Date de fin d'habilitation] )
SELECT
    UTILISATEURS.[Matricule RH],
    HABILITATIONS_TYPE.[Libellé habilitation type],
    'RESP_TERRI' AS [Code profil],
    UTILISATEURS.[Site habilitation principale],
    UTILISATEURS.[UO habilitation principale],
    /*UTILISATEURS.[Code fonction habilitation principale],
    UTILISATEURS.[Libellé fonction habilitation principale],
    UTILISATEURS.[Code grade],
    UTILISATEURS.[Libellé du grade],
    UTILISATEURS.[Code statut professionnel habilitation principale],
    UTILISATEURS.[Libellé statut professionnel habilitation principale],*/
    'S' AS [Code statut d'activité],
    'Situation Secondaire' AS [Libellé statut d'activité],
    'A' AS [Type de situation],
    'Actif' AS [Libellé type de situation],
    '01/01/2018' AS DateDebut,
    '31/12/2021' AS Datefin
FROM
    HABILITATIONS_TYPE,
    UTILISATEURS
WHERE (((HABILITATIONS_TYPE.[Libellé habilitation type]) LIKE '%Responsable Hierarchique') AND ((UTILISATEURS.[Code fonction habilitation principale]) IN ('cds','cdc','cdcsp','cdst','CDSPRV')));

/* MAJ RESPONSABLE HIERARCHIQUE NIVEAU 2*/

INSERT INTO HABILITATIONS_COMPLEMENTAIRES
    ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO],/* [Code Fonction], [Libellé Fonction], [Code Grade], [Libellé grade], [Code Statut professionnel], [Libellé Statut professionnel],*/ [Code statut d'activité], [Libellé statut d'activité], [Type de situation], [Libellé type de situation], [Date de début d'habilitation], [Date de fin d'habilitation] )
SELECT
    UTILISATEURS.[Matricule RH],
    HABILITATIONS_TYPE.[Libellé habilitation type],
    'RESP_TERRI' AS [Code profil],
    UTILISATEURS.[Site habilitation principale],
    UTILISATEURS.[UO habilitation principale],
    /*UTILISATEURS.[Code fonction habilitation principale],
    UTILISATEURS.[Libellé fonction habilitation principale],
    UTILISATEURS.[Code grade],
    UTILISATEURS.[Libellé du grade],
    UTILISATEURS.[Code statut professionnel habilitation principale],
    UTILISATEURS.[Libellé statut professionnel habilitation principale],*/
    'S' AS [Code statut d'activité],
    'Situation Secondaire' AS [Libellé statut d'activité],
    'A' AS [Type de situation],
    'Actif' AS [Libellé type de situation],
    '01/01/2018' AS DateDebut,
    '31/12/2021' AS Datefin
FROM
    HABILITATIONS_TYPE,
    UTILISATEURS
WHERE (((HABILITATIONS_TYPE.[Libellé habilitation type]) LIKE '%Responsable Hierarchique NIV 2') AND ((UTILISATEURS.[Code fonction habilitation principale]) IN ('cdg','cdg2')));

/* HABILITATIONS COMPLEMENTAIRES METIERS POUR LES RESPONSABLES HIERARCHIQUES */

INSERT INTO HABILITATIONS_COMPLEMENTAIRES
    ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO],/* [Code Fonction], [Libellé Fonction], [Code Grade], [Libellé grade], [Code Statut professionnel], [Libellé Statut professionnel],*/ [Code statut d'activité], [Libellé statut d'activité], [Type de situation], [Libellé type de situation], [Date de début d'habilitation], [Date de fin d'habilitation] )
SELECT
    UTILISATEURS.[Matricule RH],
    [GROUPE] + ' ' + [LIBELLEREF] AS [Code habilitation type],
    'RESP_TERRI' AS [Code profil],
    UTILISATEURS.[Site habilitation principale],
    UTILISATEURS.[UO habilitation principale],
    /*FONCTION_REFERENT.CODEREF AS [Code fonction habilitation principale],
    FONCTION_REFERENT.LIBELLEREF,
    UTILISATEURS.[Code grade],
    UTILISATEURS.[Libellé du grade],
    UTILISATEURS.[Code statut professionnel habilitation principale],
    UTILISATEURS.[Libellé statut professionnel habilitation principale],*/
    'S' AS [Code statut d'activité],
    'Situation Secondaire' AS [Libellé statut d'activité],
    'A' AS [Type de situation],
    'Actif' AS [Libellé type de situation],
    '01/01/2018' AS DateDebut,
    '31/12/2021' AS Datefin
FROM
    UTILISATEURS,
    FONCTION_REFERENT
WHERE (((UTILISATEURS.[Code fonction habilitation principale])='CDC' OR (UTILISATEURS.[Code fonction habilitation principale])='CDCSP' OR (UTILISATEURS.[Code fonction habilitation principale])='CDG' OR (UTILISATEURS.[Code fonction habilitation principale])='CDG2' OR (UTILISATEURS.[Code fonction habilitation principale])='CDS' OR (UTILISATEURS.[Code fonction habilitation principale])='CDST' OR (UTILISATEURS.[Code fonction habilitation principale])='CDSPRV')) OR (((UTILISATEURS.[UO habilitation principale]) LIKE '030601??04') AND ((UTILISATEURS.[Code fonction habilitation principale])='CDST'));





/* Designation des Profils par défaut et Import dans TABLE Finale HABILITATIONS_COMPLEMENTAIRES_EXPORT*/

INSERT INTO HABILITATIONS_COMPLEMENTAIRES_EXPORT
SELECT
    DISTINCT
    /*E.*,*/
    E.[Matricule RH utilisateur],
    E.[Code habilitation type],
    E.[Code profil],
    E.[Code barre site],
    E.[Code UO],
    E.[Code Fonction],
    E.[Libellé Fonction],
    E.[Code Grade],
    E.[Libellé grade],
    E.[Code Statut professionnel],
    E.[Libellé Statut professionnel],
    E.[Code statut d'activité],
    E.[Libellé statut d'activité],
    E.[Type de situation],
    E.[Libellé type de situation],
    E.[Date de début d'habilitation],
    E.[Date de fin d'habilitation],
    F.Profil_def
FROM
    HABILITATIONS_COMPLEMENTAIRES E LEFT JOIN (
SELECT
        D.Profil,
        C.[Matricule RH utilisateur],
        '1' AS Profil_def
    FROM
        (SELECT
            Min(A.Position) AS 'Pos',
            A.[Matricule RH utilisateur]
        FROM
            HABILITATIONS_COMPLEMENTAIRES B,
            (
SELECT
                H.[Code profil],
                H.[Matricule RH utilisateur] ,
                P.Position
            FROM
                HABILITATIONS_COMPLEMENTAIRES H,
                PROFILS P
            WHERE H.[Code profil]<>'CLIENT' /*AND H.[Matricule RH utilisateur]='10611'*/ AND H.[Code profil]=P.Profil
            GROUP BY H.[Matricule RH utilisateur],H.[Code profil],P.position) A
        WHERE B.[Code profil]=A.[Code profil] AND B.[Matricule RH utilisateur]=A.[Matricule RH utilisateur]
        GROUP BY A.[Matricule RH utilisateur]
    ) C,
        Profils D
    WHERE D.Position=C.Pos) F
    ON (E.[Matricule RH utilisateur]=F.[Matricule RH utilisateur] AND E.[Code profil]=F.Profil)
ORDER BY E.[Matricule RH utilisateur]


declare @sql varchar(8000)
select @sql = 'bcp GENHABLGME.dbo.HABILITATIONS_COMPLEMENTAIRES_EXPORT out c:\temp\datafile.csv -c -C 1252 -t";"  -T -S'+ @@servername
exec master..xp_cmdshell @sql

select @sql = 'bcp GENHABLGME.dbo.HABILITATIONS_TYPE out c:\temp\HABILITATIONS_TYPE.csv -c -C 1252 -t";"  -T -S'+ @@servername
exec master..xp_cmdshell @sql

select @sql = 'del /F c:\temp\HABILITATIONS_COMPLEMENTAIRES.csv'
exec master..xp_cmdshell @sql

select @sql = 'copy c:\temp\EXPORT_RH_HABILITATIONS_COMPLEMENTAIRES.csv + c:\temp\datafile.csv c:\temp\HABILITATIONS_COMPLEMENTAIRES.csv'
exec master..xp_cmdshell @sql

/* Vidage Memoire*/

CLOSE moncurseur
DEALLOCATE moncurseur
DROP TABLE #metier
PRINT 'FIN'

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

BULK INSERT GENHABLGME.dbo.UTILISATEURS FROM 'c:\temp\TEST_UTILISATEURS.csv' WITH (/*FORMAT='CSV',*/FIELDTERMINATOR=';',FIRSTROW=2,CODEPAGE='ACP')
/*c:\temp\EXPORT_RH_UTILISATEURS.csv*/


/* Import Habilitations Complémentaires SPV */

BULK INSERT GENHABLGME.dbo.HABILITATIONS_COMPLEMENTAIRES_SPV FROM 'c:\temp\TEST_HABILITATIONS_COMPLEMENTAIRES.csv' WITH (/*FORMAT='CSV',*/FIELDTERMINATOR=';',FIRSTROW=2,CODEPAGE='ACP')
/*c:\temp\EXPORT_RH_HABILITATIONS_COMPLEMENTAIRES.csv*/

/*PRINT 'PAS D''IMPORT HABILITATIONS COMPLEMENTAIRES'*/

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
    ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO], [Code Fonction], [Libellé Fonction],/*  [Code Grade], [Libellé grade], [Code Statut professionnel], [Libellé Statut professionnel],*/ [Code statut d'activité], [Libellé statut d'activité], [Type de situation], [Libellé type de situation], [Date de début d'habilitation], [Date de fin d'habilitation] )
SELECT
    H.Ligne AS [Matricule RH utilisateur],
    H.Colonne AS [Code habilitation type],
    HT.[Code profil],
    U.[Site habilitation principale],
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
    '01/01/2018' AS DateDebut,
    '31/12/2021' AS Datefin
FROM
    (SELECT
        [Libellé habilitation type],
        [Code profil],
        [Code fonction],
        [Libellé fonction]
    FROM
        [dbo].[HABILITATIONS_TYPE]
    GROUP BY [Libellé habilitation type],[Code profil],[Code fonction],[Libellé fonction]) HT
    INNER JOIN (UTILISATEURS U INNER JOIN HABILITATION H ON U.[Matricule RH] = H.Ligne)
    ON HT.[Libellé habilitation type] = H.Colonne
WHERE H.Valeur IS NOT NULL
/*AND HT.[Code profil] <>'GEST'
Print 'HABILITATIONS_COMPLEMENTAIRES Sans les Profils Gestionnaire'*/

/* MAJ RESPONSABLE HIERARCHIQUE NIVEAU 1*/

INSERT INTO HABILITATIONS_COMPLEMENTAIRES
    ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO],[Code Fonction], [Libellé Fonction],/*  [Code Grade], [Libellé grade], [Code Statut professionnel], [Libellé Statut professionnel],*/ [Code statut d'activité], [Libellé statut d'activité], [Type de situation], [Libellé type de situation], [Date de début d'habilitation], [Date de fin d'habilitation] )
SELECT
    U.[Matricule RH],
    H.[Libellé habilitation type],
    /*'RESP_TERRI' AS [Code profil],*/
    H.[Code Profil] AS [Code profil],
    U.[Site habilitation principale],
    U.[UO habilitation principale],
    H.[Code Fonction],
    H.[Libellé Fonction],
    /*UTILISATEURS.[Code fonction habilitation principale],
    UTILISATEURS.[Libellé fonction habilitation principale],
    UTILISATEURS.[Code grade],
    UTILISATEURS.[Libellé du grade],
    UTILISATEURS.[Code statut professionnel habilitation principale],
    UTILISATEURS.[Libellé statut professionnel habilitation principale],*/
    /*'S' AS [Code statut d'activité],
    'Situation Secondaire' AS [Libellé statut d'activité],*/
    U.[Code statut d activité] AS [Code statut d'activité],
    U.[Libellé statut d activité] AS [Libellé statut d'activité],
    'A' AS [Type de situation],
    'Actif' AS [Libellé type de situation],
    '01/01/2018' AS DateDebut,
    '31/12/2021' AS Datefin
FROM
    HABILITATIONS_TYPE H,
    UTILISATEURS U
WHERE (((H.[Libellé habilitation type]) LIKE 'Approbateur Niv1%') AND ((U.[Code fonction habilitation principale]) IN ('cds','cdc','cdcsp','cdst','CDSPRV','50CHC','50CHS')));

/* MAJ RESPONSABLE HIERARCHIQUE NIVEAU 2*/

INSERT INTO HABILITATIONS_COMPLEMENTAIRES
    ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO], [Code Fonction], [Libellé Fonction],/* [Code Grade], [Libellé grade], [Code Statut professionnel], [Libellé Statut professionnel],*/ [Code statut d'activité], [Libellé statut d'activité], [Type de situation], [Libellé type de situation], [Date de début d'habilitation], [Date de fin d'habilitation] )
SELECT
    U.[Matricule RH],
    H.[Libellé habilitation type],
    /*'RESP_TERRI' AS [Code profil],*/
    H.[Code Profil] AS [Code profil],
    U.[Site habilitation principale],
    U.[UO habilitation principale],
    H.[Code Fonction],
    H.[Libellé Fonction],
    /*UTILISATEURS.[Code fonction habilitation principale],
    UTILISATEURS.[Libellé fonction habilitation principale],
    UTILISATEURS.[Code grade],
    UTILISATEURS.[Libellé du grade],
    UTILISATEURS.[Code statut professionnel habilitation principale],
    UTILISATEURS.[Libellé statut professionnel habilitation principale],*/
    /*'S' AS [Code statut d'activité],
    'Situation Secondaire' AS [Libellé statut d'activité],*/
    U.[Code statut d activité] AS [Code statut d'activité],
    U.[Libellé statut d activité] AS [Libellé statut d'activité],
    'A' AS [Type de situation],
    'Actif' AS [Libellé type de situation],
    '01/01/2018' AS DateDebut,
    '31/12/2021' AS Datefin
FROM
    HABILITATIONS_TYPE H,
    UTILISATEURS U
WHERE (((H.[Libellé habilitation type]) LIKE 'Approbateur Niv2%') AND ((U.[Code fonction habilitation principale]) IN ('cdg','cdg2','40CHG')));

/* HABILITATIONS COMPLEMENTAIRES REFERENTS METIERS POUR LES RESPONSABLES HIERARCHIQUES NIV1 */
/*
INSERT INTO HABILITATIONS_COMPLEMENTAIRES
    ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO], [Code Fonction], [Libellé Fonction], [Code statut d'activité], [Libellé statut d'activité], [Type de situation], [Libellé type de situation], [Date de début d'habilitation], [Date de fin d'habilitation] )
SELECT
    UTILISATEURS.[Matricule RH],
    [GROUPE] + ' ' + [LIBELLEREF] AS [Code habilitation type],
    'RESP_TERRI' AS [Code profil],
    UTILISATEURS.[Site habilitation principale],
    UTILISATEURS.[UO habilitation principale],
    FONCTION_REFERENT.CODEREF,
    FONCTION_REFERENT.LIBELLEREF,
    'S' AS [Code statut d'activité],
    'Situation Secondaire' AS [Libellé statut d'activité],
    'A' AS [Type de situation],
    'Actif' AS [Libellé type de situation],
    '01/01/2018' AS DateDebut,
    '31/12/2021' AS Datefin
FROM
    UTILISATEURS,
    FONCTION_REFERENT
WHERE (((UTILISATEURS.[Code fonction habilitation principale])='CDC' OR (UTILISATEURS.[Code fonction habilitation principale])='CDCSP' OR (UTILISATEURS.[Code fonction habilitation principale])='CDG' OR (UTILISATEURS.[Code fonction habilitation principale])='CDG2' OR (UTILISATEURS.[Code fonction habilitation principale])='CDS' OR (UTILISATEURS.[Code fonction habilitation principale])='CDST' OR (UTILISATEURS.[Code fonction habilitation principale])='CDSPRV')) OR (((UTILISATEURS.[UO habilitation principale]) LIKE '030601__04') AND ((UTILISATEURS.[Code fonction habilitation principale])='CDST'));
*/

INSERT INTO HABILITATIONS_COMPLEMENTAIRES
    ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO], [Code Fonction], [Libellé Fonction], [Code statut d'activité], [Libellé statut d'activité], [Type de situation], [Libellé type de situation], [Date de début d'habilitation], [Date de fin d'habilitation] )
SELECT
    U.[Matricule RH] AS [Matricule RH utilisateur],
    F.[HABTYPE] AS [Code habilitation type],
    F.PROFIL AS [Code profil],
    U.[Site habilitation principale] AS [Code barre site],
    U.[UO habilitation principale] AS [Code UO],
    F.CODEREF AS [Code Fonction],
    F.LIBELLEREF AS [Libellé Fonction],
    /*'S' AS [Code statut d'activité],
    'Situation Secondaire' AS [Libellé statut d'activité],*/
    U.[Code statut d activité] AS [Code statut d'activité],
    U.[Libellé statut d activité] AS [Libellé statut d'activité],
    'A' AS [Type de situation],
    'Actif' AS [Libellé type de situation],
    '01/01/2018' AS [Date de début d'habilitation],
    '31/12/2021' AS [Date de fin d'habilitation]
FROM
    UTILISATEURS U,
    FONCTION_REFERENT F
WHERE (F.PROFIL='RESP_HIER1' AND U.[Code fonction habilitation principale] IN ('CDC','CDCSP','CDS','CDST','CDSPRV','50CHC','50CHS'));

/* HABILITATIONS COMPLEMENTAIRES REFERENTS METIERS POUR LES RESPONSABLES HIERARCHIQUES NIV2 */

INSERT INTO HABILITATIONS_COMPLEMENTAIRES
    ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO], [Code Fonction], [Libellé Fonction], [Code statut d'activité], [Libellé statut d'activité], [Type de situation], [Libellé type de situation], [Date de début d'habilitation], [Date de fin d'habilitation] )
SELECT
    U.[Matricule RH] AS [Matricule RH utilisateur],
    F.[HABTYPE] AS [Code habilitation type],
    F.PROFIL AS [Code profil],
    U.[Site habilitation principale] AS [Code barre site],
    U.[UO habilitation principale] AS [Code UO],
    F.CODEREF AS [Code Fonction],
    F.LIBELLEREF AS [Libellé Fonction],
    /*'S' AS [Code statut d'activité],
    'Situation Secondaire' AS [Libellé statut d'activité],*/
    U.[Code statut d activité] AS [Code statut d'activité],
    U.[Libellé statut d activité] AS [Libellé statut d'activité],
    'A' AS [Type de situation],
    'Actif' AS [Libellé type de situation],
    '01/01/2018' AS [Date de début d'habilitation],
    '31/12/2021' AS [Date de fin d'habilitation]
FROM
    UTILISATEURS U,
    FONCTION_REFERENT F
WHERE (F.PROFIL='RESP_HIER2' AND U.[Code fonction habilitation principale] IN ('CDG','CDG2','40CHG'));

/* MAJ RESPONSABLE HIERARCHIQUE NIVEAU 1 DOUBLE STATUT*/

INSERT INTO HABILITATIONS_COMPLEMENTAIRES
    ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO],[Code Fonction], [Libellé Fonction],/*  [Code Grade], [Libellé grade], [Code Statut professionnel], [Libellé Statut professionnel],*/ [Code statut d'activité], [Libellé statut d'activité], [Type de situation], [Libellé type de situation], [Date de début d'habilitation], [Date de fin d'habilitation] )
SELECT
    U.[Matricule RH],
    H.[Libellé habilitation type],
    /*'RESP_TERRI' AS [Code profil],*/
    H.[Code Profil] AS [Code profil],
    U.[Site habilitation principale],
    U.[UO habilitation principale],
    H.[Code Fonction],
    H.[Libellé Fonction],
    /*UTILISATEURS.[Code fonction habilitation principale],
    UTILISATEURS.[Libellé fonction habilitation principale],
    UTILISATEURS.[Code grade],
    UTILISATEURS.[Libellé du grade],
    UTILISATEURS.[Code statut professionnel habilitation principale],
    UTILISATEURS.[Libellé statut professionnel habilitation principale],*/
    /*'S' AS [Code statut d'activité],
    'Situation Secondaire' AS [Libellé statut d'activité],*/
    U.[Code statut d'activité] AS [Code statut d'activité],
    U.[Libellé statut d'activité] AS [Libellé statut d'activité],
    'A' AS [Type de situation],
    'Actif' AS [Libellé type de situation],
    '01/01/2018' AS DateDebut,
    '31/12/2021' AS Datefin
FROM
    HABILITATIONS_TYPE H,
    (SELECT [Matricule RH utilisateur] AS [Matricule RH]
      ,[Code barre site] AS [Site habilitation principale]
      ,[Code UO] AS [UO habilitation principale]
      ,[Code Fonction] AS [Code fonction habilitation principale]
      ,[Code statut d'activité]
      ,[Libellé statut d'activité]

  FROM [GENHABLGME].[dbo].[HABILITATIONS_COMPLEMENTAIRES_SPV]
  WHERE [Code Fonction]='50CHC') U
WHERE (((H.[Libellé habilitation type]) LIKE 'Approbateur Niv1%') AND ((U.[Code fonction habilitation principale]) IN ('cds','cdc','cdcsp','cdst','CDSPRV','50CHC','50CHS')));

/* HABILITATIONS COMPLEMENTAIRES REFERENTS METIERS POUR LES RESPONSABLES HIERARCHIQUES NIV1 DOUBLE STATUT */

INSERT INTO HABILITATIONS_COMPLEMENTAIRES
    ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO], [Code Fonction], [Libellé Fonction], [Code statut d'activité], [Libellé statut d'activité], [Type de situation], [Libellé type de situation], [Date de début d'habilitation], [Date de fin d'habilitation] )
SELECT
    U.[Matricule RH] AS [Matricule RH utilisateur],
    F.[HABTYPE] AS [Code habilitation type],
    F.PROFIL AS [Code profil],
    U.[Site habilitation principale] AS [Code barre site],
    U.[UO habilitation principale] AS [Code UO],
    F.CODEREF AS [Code Fonction],
    F.LIBELLEREF AS [Libellé Fonction],
    /*'S' AS [Code statut d'activité],
    'Situation Secondaire' AS [Libellé statut d'activité],*/
    U.[Code statut d'activité] AS [Code statut d'activité],
    U.[Libellé statut d'activité] AS [Libellé statut d'activité],
    'A' AS [Type de situation],
    'Actif' AS [Libellé type de situation],
    '01/01/2018' AS [Date de début d'habilitation],
    '31/12/2021' AS [Date de fin d'habilitation]
FROM
    (SELECT [Matricule RH utilisateur] AS [Matricule RH]
      ,[Code barre site] AS [Site habilitation principale]
      ,[Code UO] AS [UO habilitation principale]
      ,[Code Fonction] AS [Code fonction habilitation principale]
      ,[Code statut d'activité]
      ,[Libellé statut d'activité]
  FROM [GENHABLGME].[dbo].[HABILITATIONS_COMPLEMENTAIRES_SPV]
  WHERE [Code Fonction]='50CHC') U,
    FONCTION_REFERENT F
WHERE (F.PROFIL='RESP_HIER1' AND U.[Code fonction habilitation principale] IN ('CDC','CDCSP','CDS','CDST','CDSPRV','50CHC','50CHS'));


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

select @sql = 'bcp GENHABLGME.dbo.HABILITATIONS_TYPE out c:\temp\HABILITATIONS_TYPE1.csv -c -C 1252 -t";"  -T -S'+ @@servername
exec master..xp_cmdshell @sql

select @sql = 'copy c:\temp\ENTETE_HABTYP.csv + c:\temp\HABILITATIONS_TYPE1.csv c:\temp\HABILITATIONS_TYPE.csv'
exec master..xp_cmdshell @sql

select @sql = 'del /F c:\temp\HABILITATIONS_COMPLEMENTAIRES.csv'
exec master..xp_cmdshell @sql

select @sql = 'copy c:\temp\TEST_HABILITATIONS_COMPLEMENTAIRES.csv + c:\temp\datafile.csv c:\temp\HABILITATIONS_COMPLEMENTAIRES.csv'
exec master..xp_cmdshell @sql

select @sql = 'del /F c:\temp\datafile.csv'
exec master..xp_cmdshell @sql

select @sql = 'del /F c:\temp\HABILITATIONS_TYPE1.csv'
exec master..xp_cmdshell @sql


/* Vidage Memoire*/

CLOSE moncurseur
DEALLOCATE moncurseur
DROP TABLE #metier
PRINT 'FIN'

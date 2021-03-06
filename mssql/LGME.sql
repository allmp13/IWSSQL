USE GENHABLGME

    DROP TABLE IF EXISTS #metier
    DROP TABLE IF EXISTS #tmp
    DELETE FROM HABILITATION;
    DELETE FROM HABILITATIONS_TYPE;
    DELETE FROM HABILITATIONS_COMPLEMENTAIRES;
    DELETE FROM HABILITATIONS_COMPLEMENTAIRES_EXPORT;

    DELETE GENHABLGME.dbo.UTILISATEURS

    BULK INSERT GENHABLGME.dbo.UTILISATEURS FROM 'c:\temp\EXPORT_RH_UTILISATEURS.csv' WITH (FORMAT='CSV',FIELDTERMINATOR=';',FIRSTROW=2,CODEPAGE='ACP')
    BULK INSERT GENHABLGME.dbo.HABILITATIONS_COMPLEMENTAIRES FROM 'c:\temp\EXPORT RH HABILITATIONS_COMPLEMENTAIRES.csv' WITH (FORMAT='CSV',FIELDTERMINATOR=';',FIRSTROW=2,CODEPAGE='ACP')

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

    DECLARE @metier varchar(50)
    DECLARE moncurseur CURSOR FOR SELECT
        metier
    FROM
        #metier

    OPEN moncurseur

    FETCH moncurseur INTO @metier

    WHILE @@FETCH_STATUS = 0
BEGIN
        SET @metier = '''C:\temp\' + @metier +''''
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
            [Libell� habilitation type]
            ,[Code Equipe]
            ,[Libell� Equipe]
            ,[Code profil]
            ,[R�le dans l'�quipe]
            ,[Code Nature]
            ,[Lecture seule]
            ,[Code Fonction]
            ,[Libell� Fonction])

        SELECT
            [Libelle Habi Type]
      ,
            [Code Equipe]
      ,
            [Libelle Equipe]
      ,
            [Code Profil]
      ,
            [R�le dans l'�quipe]
      ,
            [Code Nature]
      ,
            [Lecture Seule]
      ,
            [Code Fonction]
      ,
            [Libell� Fonction]
        FROM
            TESTJMD...Equipe$

    END

    /* MAJ GESTIONNAIRES*/

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
        (SELECT
            [Libell� habilitation type],
            [Code profil]
        FROM
            [dbo].[HABILITATIONS_TYPE]
        GROUP BY [Libell� habilitation type],[Code profil]) HT
        INNER JOIN (UTILISATEURS U INNER JOIN HABILITATION H ON U.[Matricule RH] = H.Ligne)
        ON HT.[Libell� habilitation type] = H.Colonne
    WHERE H.Valeur IS NOT NULL

    /* MAJ RESPONSABLE HIERARCHIQUE NIVEAU 1*/

    INSERT INTO HABILITATIONS_COMPLEMENTAIRES
        ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO], [Code Fonction], [Libell� Fonction], [Code Grade], [Libell� grade], [Code Statut professionnel], [Libell� Statut professionnel], [Code statut d'activit�], [Libell� statut d'activit�], [Type de situation], [Libell� type de situation], [Date de d�but d'habilitation], [Date de fin d'habilitation] )
    SELECT
        UTILISATEURS.[Matricule RH],
        HABILITATIONS_TYPE.[Libell� habilitation type],
        'RESP_TERRI' AS [Code profil],
        UTILISATEURS.[Site habilitation principale],
        UTILISATEURS.[UO habilitation principale],
        UTILISATEURS.[Code fonction habilitation principale],
        UTILISATEURS.[Libell� fonction habilitation principale],
        UTILISATEURS.[Code grade],
        UTILISATEURS.[Libell� du grade],
        UTILISATEURS.[Code statut professionnel habilitation principale],
        UTILISATEURS.[Libell� statut professionnel habilitation principale],
        'S' AS [Code statut d'activit�],
        'Situation Secondaire' AS [Libell� statut d'activit�],
        'A' AS [Type de situation],
        'Actif' AS [Libell� type de situation],
        '01/01/2018' AS DateDebut,
        '31/12/2021' AS Datefin
    FROM
        HABILITATIONS_TYPE,
        UTILISATEURS
    WHERE (((HABILITATIONS_TYPE.[Libell� habilitation type]) LIKE '%Responsable Hierarchique') AND ((UTILISATEURS.[Code fonction habilitation principale]) IN ('cds','cdc','cdcsp','cdst','CDSPRV')));

    /* MAJ RESPONSABLE HIERARCHIQUE NIVEAU 2*/

    INSERT INTO HABILITATIONS_COMPLEMENTAIRES
        ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO], [Code Fonction], [Libell� Fonction], [Code Grade], [Libell� grade], [Code Statut professionnel], [Libell� Statut professionnel], [Code statut d'activit�], [Libell� statut d'activit�], [Type de situation], [Libell� type de situation], [Date de d�but d'habilitation], [Date de fin d'habilitation] )
    SELECT
        UTILISATEURS.[Matricule RH],
        HABILITATIONS_TYPE.[Libell� habilitation type],
        'RESP_TERRI' AS [Code profil],
        UTILISATEURS.[Site habilitation principale],
        UTILISATEURS.[UO habilitation principale],
        UTILISATEURS.[Code fonction habilitation principale],
        UTILISATEURS.[Libell� fonction habilitation principale],
        UTILISATEURS.[Code grade],
        UTILISATEURS.[Libell� du grade],
        UTILISATEURS.[Code statut professionnel habilitation principale],
        UTILISATEURS.[Libell� statut professionnel habilitation principale],
        'S' AS [Code statut d'activit�],
        'Situation Secondaire' AS [Libell� statut d'activit�],
        'A' AS [Type de situation],
        'Actif' AS [Libell� type de situation],
        '01/01/2018' AS DateDebut,
        '31/12/2021' AS Datefin
    FROM
        HABILITATIONS_TYPE,
        UTILISATEURS
    WHERE (((HABILITATIONS_TYPE.[Libell� habilitation type]) LIKE '%Responsable Hierarchique NIV 2') AND ((UTILISATEURS.[Code fonction habilitation principale]) IN ('cdg','cdg2')));

/* Designation des Profils par d�faut et Import dans TABLE Finale HABILITATIONS_COMPLEMENTAIRES_EXPORT*/

INSERT INTO HABILITATIONS_COMPLEMENTAIRES_EXPORT
SELECT DISTINCT
    E.*,
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
    order by E.[Matricule RH utilisateur]


/* Vidage Memoire*/

    CLOSE moncurseur
    DEALLOCATE moncurseur
    DROP TABLE #metier


SET NOCOUNT ON
DECLARE @sql varchar(8000)

PRINT 'DEBUT'

DELETE FROM HABILITATION;
DELETE FROM HABILITATIONS_TYPE;
DELETE FROM HABILITATIONS_COMPLEMENTAIRES;
DELETE FROM HABILITATIONS_COMPLEMENTAIRES_EXPORT;

DELETE GENHABLGME.dbo.UTILISATEURS

/* Import Utilisateurs */

BULK INSERT GENHABLGME.dbo.UTILISATEURS FROM 'c:\temp\TEST_UTILISATEURS.csv' WITH (/*FORMAT='CSV',*/FIELDTERMINATOR=';',FIRSTROW=2,CODEPAGE='ACP')

/* Import Habilitations Complémentaires SPV */

BULK INSERT GENHABLGME.dbo.HABILITATIONS_COMPLEMENTAIRES_SPV FROM 'c:\temp\TEST_HABILITATIONS_COMPLEMENTAIRES.csv' WITH (/*FORMAT='CSV',*/FIELDTERMINATOR=';',FIRSTROW=2,CODEPAGE='ACP')

/* Import Habilitations TYPE */

BULK INSERT GENHABLGME.dbo.HABILITATIONS_TYPE FROM 'c:\temp\HABILITATIONS_TYPE_APPROBATION.csv' WITH (/*FORMAT='CSV',*/FIELDTERMINATOR=';',FIRSTROW=2,CODEPAGE='ACP')


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
    convert(varchar, getdate(), 103) AS DateDebut,
    convert(varchar,  DATEADD(year, 1,getdate()), 103) AS Datefin
FROM
    HABILITATIONS_TYPE H,
    UTILISATEURS U
WHERE (((H.[Libellé habilitation type]) LIKE 'Approbateur Niv1%') AND ((U.[Code fonction habilitation principale]) IN ('50CHC','50CHS')));

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
    convert(varchar, getdate(), 103) AS DateDebut,
    convert(varchar,  DATEADD(year, 1,getdate()), 103) AS Datefin
FROM
    HABILITATIONS_TYPE H,
    UTILISATEURS U
WHERE (((H.[Libellé habilitation type]) LIKE 'Approbateur Niv2%') AND ((U.[Code fonction habilitation principale]) IN ('40CHG')));

/* HABILITATIONS COMPLEMENTAIRES REFERENTS METIERS POUR LES RESPONSABLES HIERARCHIQUES NIV1 */

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
    convert(varchar, getdate(), 103) AS [Date de début d'habilitation],
    convert(varchar,  DATEADD(year, 1,getdate()), 103) AS [Date de fin d'habilitation]
FROM
    UTILISATEURS U,
    FONCTION_REFERENT F
WHERE (F.PROFIL='RESP_HIER1' AND U.[Code fonction habilitation principale] IN ('50CHC','50CHS'));

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
    convert(varchar, getdate(), 103) AS [Date de début d'habilitation],
    convert(varchar,  DATEADD(year, 1,getdate()), 103) AS [Date de fin d'habilitation]
FROM
    UTILISATEURS U,
    FONCTION_REFERENT F
WHERE (F.PROFIL='RESP_HIER2' AND U.[Code fonction habilitation principale] IN ('40CHG'));

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
    convert(varchar, getdate(), 103) AS DateDebut,
    convert(varchar,  DATEADD(year, 1,getdate()), 103) AS Datefin
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
WHERE (((H.[Libellé habilitation type]) LIKE 'Approbateur Niv1%') AND ((U.[Code fonction habilitation principale]) IN ('50CHC','50CHS')));

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
    convert(varchar, getdate(), 103) AS [Date de début d'habilitation],
    convert(varchar,  DATEADD(year, 1,getdate()), 103) AS [Date de fin d'habilitation]
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
WHERE (F.PROFIL='RESP_HIER1' AND U.[Code fonction habilitation principale] IN ('50CHC','50CHS'));


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

/* GENERATION DES FICHIERS CSV */



/*-------------------------------*/
/* HABILITATIONS COMPLEMENTAIRES */


select @sql = 'bcp GENHABLGME.dbo.HABILITATIONS_COMPLEMENTAIRES_EXPORT out c:\temp\HC.csv -c -C 1252 -t";"  -T -S'+ @@servername
exec master..xp_cmdshell @sql

select @sql = 'copy c:\temp\TEST_HABILITATIONS_COMPLEMENTAIRES.csv + c:\temp\HC.csv c:\temp\HABILITATIONS_COMPLEMENTAIRES.csv'
exec master..xp_cmdshell @sql

select @sql = 'del /F c:\temp\HC.csv'
exec master..xp_cmdshell @sql

PRINT 'FIN'

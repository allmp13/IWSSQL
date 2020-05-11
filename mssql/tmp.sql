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
    (SELECT DISTINCT [Libellé habilitation type],[Code Profil],[Code Fonction],[Libellé Fonction] FROM HABILITATIONS_TYPE) H,
    UTILISATEURS U
WHERE (((H.[Libellé habilitation type]) LIKE 'Approbateur Niv1%') AND ((U.[Code fonction habilitation principale]) IN ('50CHC','50CHS')))
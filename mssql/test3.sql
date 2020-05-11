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
WHERE (F.PROFIL='RESP_HIER1' AND U.[Code fonction habilitation principale] IN ('CDC','CDCSP','CDS','CDST','CDSPRV','50CHC','50CHS'))

SELECT * from FONCTION_REFERENT where CODEREF='COV'
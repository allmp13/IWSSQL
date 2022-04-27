SELECT
    U.[Matricule RH],
    G_T0.[Libell� habilitation type] ,
    G_T0.[Code profil] ,
    U.[Site habilitation principale],
    U.[UO habilitation principale],
    G_T0.[Code fonction] ,
    G_T0.[Libell� fonction],
    U.[Code statut d'activit�] AS [Code statut d'activit�],
    U.[Libell� statut d'activit�] AS [Libell� statut d'activit�],
    'A' AS [Type de situation],
    'Actif' AS [Libell� type de situation],
    CONVERT(varchar, getdate(), 103) AS DateDebut,
    CONVERT(varchar,  DATEADD(year, 1,getdate()), 103) AS Datefin

FROM
    (SELECT
        DISTINCT
        [Libell� habilitation type],
        [Code Profil],
        [Code Fonction],
        [Libell� fonction]
    FROM
        HABILITATIONS_TYPE ) G_T0,
    (SELECT
        [Matricule RH utilisateur] AS [Matricule RH]
      ,
        [Code barre site] AS [Site habilitation principale]
      ,
        [Code UO] AS [UO habilitation principale]
      ,
        [Code Fonction] AS [Code fonction habilitation principale]
      ,
        [Code statut d'activit�]
      ,
        [Libell� statut d'activit�]

    FROM
        HABILITATIONS_COMPLEMENTAIRES
    WHERE [Code Fonction]='50CHC') U

WHERE (((G_T0.[Libell� habilitation type]) LIKE 'Approbateur Niv1%') AND ((U.[Code fonction habilitation principale]) IN ('50CHC','50CHS')))  

/*SELECT * FROM HABILITATIONS_TYPE

SELECT * FROM HABILITATIONS_COMPLEMENTAIRES --WHERE [Code Fonction]='50CHC'
--SELECT * FROM UTIL*/

SELECT U.N_UTIL,H.* FROM UTILAFFECTHAB H INNER JOIN UTILISATEUR U ON H.C_UTIL=U.C_UTIL WHERE H.C_FONCTION='50CHC' AND H.C_LOV_STATUTPRO='SPV' AND H.D_ARCHIVE IS NULL --AND H.I_HAB_RH=0
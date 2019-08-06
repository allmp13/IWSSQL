    SELECT
        H.Ligne AS [Matricule RH utilisateur],
        H.Colonne AS [Code habilitation type],
        HT.[Code profil],
        U.[Site habilitation principale],
        U.[UO habilitation principale],
        U.[Code fonction habilitation principale],
        U.[Libellé fonction habilitation principale],
        U.[Code grade],
        U.[Libellé du grade],
        U.[Code statut professionnel habilitation principale],
        U.[Libellé statut professionnel habilitation principale],
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
    WHERE H.Valeur IS NOT NULL AND HT.[Code profil]='GEST'

    IF EXISTS 
        (SELECT 
             TABLE_NAME 
         FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '#metier') 
DROP TABLE TableToDrop

create table #metier (dummy int)
select OBJECT_ID('tempdb..#metier') 
 if  OBJECT_ID('tempdb..#metier') is not null drop table #metier 
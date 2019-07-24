USE GENHABLGME
INSERT INTO HABILITATIONS_COMPLEMENTAIRES
    ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO], [Code Fonction], [Libellé Fonction], [Code Grade], [Libellé grade], [Code Statut professionnel], [Libellé Statut professionnel], [Code statut d'activité], [Libellé statut d'activité], [Type de situation], [Libellé type de situation], [Date de début d'habilitation], [Date de fin d'habilitation] )
SELECT
    HABILITATION.Ligne AS [Matricule RH utilisateur],
    HABILITATION.Colonne AS [Code habilitation type],
    HABILITATION_TYPE.[Code profil],
    UTILISATEURS.[Site habilitation principale],
    UTILISATEURS.[UO habilitation principale],
    UTILISATEURS.[Code fonction habilitation principale],
    UTILISATEURS.[Libellé fonction habilitation principale],
    UTILISATEURS.[Code grade],
    UTILISATEURS.[Libellé du grade],
    UTILISATEURS.[Code statut professionnel habilitation principale],
    UTILISATEURS.[Libellé statut professionnel habilitation principale],
    'S' AS [Code statut d'activité],
    'Situation Secondaire' AS [Libellé statut d'activité],
    'A' AS [Type de situation],
    'Actif' AS [Libellé type de situation],
    '01/01/2018' AS DateDebut,
    '31/12/2021' AS Datefin
FROM
    HABILITATIONS_TYPE INNER JOIN (UTILISATEURS INNER JOIN HABILITATION ON UTILISATEURS.[Matricule RH] = HABILITATION.Ligne) ON HABILITATION_TYPE.[Libellé habilitation type] = HABILITATION.Colonne
WHERE (((HABILITATION.Valeur) IS NOT NULL));

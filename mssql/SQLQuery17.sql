USE GENHABLGME
INSERT INTO HABILITATIONS_COMPLEMENTAIRES
    ( [Matricule RH utilisateur], [Code habilitation type], [Code profil], [Code barre site], [Code UO], [Code Fonction], [Libell� Fonction], [Code Grade], [Libell� grade], [Code Statut professionnel], [Libell� Statut professionnel], [Code statut d'activit�], [Libell� statut d'activit�], [Type de situation], [Libell� type de situation], [Date de d�but d'habilitation], [Date de fin d'habilitation] )
SELECT
    HABILITATION.Ligne AS [Matricule RH utilisateur],
    HABILITATION.Colonne AS [Code habilitation type],
    HABILITATION_TYPE.[Code profil],
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
    HABILITATIONS_TYPE INNER JOIN (UTILISATEURS INNER JOIN HABILITATION ON UTILISATEURS.[Matricule RH] = HABILITATION.Ligne) ON HABILITATION_TYPE.[Libell� habilitation type] = HABILITATION.Colonne
WHERE (((HABILITATION.Valeur) IS NOT NULL));

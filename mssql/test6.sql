UPDATE H2
    SET H2.Profil_def=NULL
FROM
    HABILITATIONS_COMPLEMENTAIRES_EXPORT AS H2


SELECT U.Nom,H2.* 
FROM
    HABILITATIONS_COMPLEMENTAIRES_EXPORT AS H2, UTILISATEURS U
where H2.Profil_def is NULL and H2.[Matricule RH utilisateur]=u.[Matricule RH]

BULK INSERT GENHABLGME.dbo.HABILITATIONS_COMPLEMENTAIRES_EXPORT FROM 'c:\temp\LOGAN_HABILITATIONS_COMPLEMENTAIRES_EXPORT.csv' WITH (/*FORMAT='CSV',*/FIELDTERMINATOR=';',FIRSTROW=2,CODEPAGE='ACP')


SELECT U.Nom,H2.* 
FROM
    HABILITATIONS_COMPLEMENTAIRES_EXPORT AS H2, UTILISATEURS U
WHERE H2.[Matricule RH utilisateur]='14322'and H2.[Matricule RH utilisateur]=u.[Matricule RH]
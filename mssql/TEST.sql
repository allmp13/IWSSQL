USE GENHABLGME
/*EXEC LGME*/
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



/*


SELECT [HABILITATIONS_COMPLEMENTAIRES DISTINCTES].*, MATRICULE_PROFIL.PROFIL_DEF
FROM [HABILITATIONS_COMPLEMENTAIRES DISTINCTES] LEFT JOIN MATRICULE_PROFIL ON ([HABILITATIONS_COMPLEMENTAIRES DISTINCTES].[Matricule RH utilisateur] = MATRICULE_PROFIL.[Matricule RH utilisateur]) AND ([HABILITATIONS_COMPLEMENTAIRES DISTINCTES].[Code profil] = MATRICULE_PROFIL.Profil);


*/
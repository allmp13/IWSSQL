SELECT  H2.*
FROM
(
	SELECT  I.[Matricule RH utilisateur]
	       ,P2.Profil
	FROM
	(
		SELECT  MIN(P.[Position]) AS Pos
		       ,H.[Matricule RH utilisateur]
		FROM
		(
			SELECT  *
			FROM HABILITATIONS_COMPLEMENTAIRES_EXPORT
		) H, PROFILS P
		WHERE H.[Code profil] = P.Profil
		GROUP BY  H.[Matricule RH utilisateur]
	) I, PROFILS P2
	WHERE I.Pos = P2.[Position] 
) I2, HABILITATIONS_COMPLEMENTAIRES_EXPORT H2
WHERE I2.[Matricule RH utilisateur] = H2.[Matricule RH utilisateur]
AND I2.Profil = H2.[Code profil] 
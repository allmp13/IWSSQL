SELECT
    a.nom,
    a.prenom,
    S.[matricule]
      ,
    S.[code_collectivite]
      ,
    S.[nom_collectivite]

      ,
    S.[libellé_carrière]
      ,
    S.[date_debut]
      ,
    S.[date_fin]
 
      ,
    S.[code_service_princ]
      ,
    S.[libelle_service_princ]
      ,
    S.[sigle_service_princ]
 
      ,

    S.[code_motif]
      ,
    S.[libelle_motif]
      ,
    S.[code_fonction]
      ,
    S.[libelle_fonction]
      ,

    S.[code_sg]
      ,
    S.[libelle_sg]
FROM
    [ODS_PROD].[dbo].[BL_AGENT_SERVICE] S INNER JOIN [ODS_PROD].[dbo].[BL_AGENT] A ON A.matricule=S.matricule
WHERE  A.nom = 'CARBONI'--S.matricule = '021818' AND S.date_fin = '31/12/2099'
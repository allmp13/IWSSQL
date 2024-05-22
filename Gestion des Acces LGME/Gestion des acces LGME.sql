/*
Serveur:
           SRVMSSQL-M

*/

SELECT  [Libelle_Habi_Type]
      ,[Code_Profil]
      ,[Code_barre_Site]
      ,[Code_UO]
      ,[Code_Fonction]
      ,[Libelle_Fonction]
      ,[Code_Grade]
      ,[Libelle_Grade]
      ,[Code_Statut_professionnel]
      ,[Libelle_Statut_professionnel]
      ,[Code_statut_activite]
      ,[Libelle_statut_activite]
      ,[Environnement]
      ,[clef_unique]
  FROM [GSIC_LGME].[dbo].[HABILITATION]
  
  WHERE Environnement like 'HAB%'

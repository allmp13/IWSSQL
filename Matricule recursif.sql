
WITH [DirectReports] ([Code_de_l_UO_mère], [Code_de_l_UO], [Matricule_RH_du_responsable_1_de_l_UO], [MATB])
AS (
  -- Point de départ
SELECT e.[Code_de_l_UO_mère], e.[Code_de_l_UO], e.[Matricule_RH_du_responsable_1_de_l_UO], iif([Code_de_l_UO]='03','0',e.[Matricule_RH_du_responsable_1_de_l_UO])
FROM [UO] AS e
WHERE [Code_de_l_UO]='03'
UNION ALL
  -- Définition de la récursivité
SELECT e.[Code_de_l_UO_mère], e.[Code_de_l_UO], iif(e.[Matricule_RH_du_responsable_1_de_l_UO]<>'0',e.[Matricule_RH_du_responsable_1_de_l_UO],d.Matricule_RH_du_responsable_1_de_l_UO),d.Matricule_RH_du_responsable_1_de_l_UO
FROM [UO] AS e
INNER JOIN [DirectReports] AS d
ON e.[Code_de_l_UO_mère] = d.[Code_de_l_UO]
)
-- Table de sortie référençant la table CTE
SELECT e.[Code_de_l_UO_mère], e.[Code_de_l_UO], e.[Matricule_RH_du_responsable_1_de_l_UO],e.MATB As Supérieur 
FROM [DirectReports] e
ORDER BY [Code_de_l_UO];




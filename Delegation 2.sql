
SELECT
    T.L_TYPEHAB AS 'Libelle Habi Type',
    E.C_EQUIPE AS 'Code Equipe',
    E.L_EQUIPE AS 'Libelle Equipe',
    E.VA_COULEQUIPE AS 'Couleur',
    UH.C_PROFIL AS 'Code Profil',
    HE.ST_HELPDESK AS "Rôle dans l'équipe",
    N.C_NATURE AS 'Code Nature',
    STR(N.F_LECTURESEULE) AS 'Lecture Seule',
    IIF(F.L_FONCTION LIKE 'REFERENT%',UH.C_FONCTION,NULL )AS 'Code Fonction',
    IIF(F.L_FONCTION LIKE 'REFERENT%',F.L_FONCTION,NULL)AS 'Libellé Fonction',
    IIF (UH.C_PROFIL='GEST' AND S.C_SITEBARRE <> 'S047',S.C_SITEBARRE,NULL) AS 'Code Site'
FROM
    (((((UTILAFFECTHAB UH LEFT JOIN TYPEHAB T ON UH.C_TYPEHAB=T.C_TYPEHAB) LEFT JOIN SITE S ON S.C_SITE=UH.C_SITE) LEFT JOIN FONCTION F ON UH.C_FONCTION=F.C_FONCTION) LEFT JOIN TYPEHABEQUIPE HE ON HE.C_TYPEHAB=UH.C_TYPEHAB LEFT JOIN EQUIPE E ON HE.C_EQUIPE=E.C_EQUIPE) LEFT JOIN TYPEHABNATURE N ON N.C_TYPEHAB=UH.C_TYPEHAB)
WHERE 
/*T.L_TYPEHAB LIKE '%GSI%'*/
/*T.L_TYPEHAB LIKE '%PAT%'*/
/*T.L_TYPEHAB LIKE '%BIO%'*/
(T.L_TYPEHAB LIKE '%GTL%' OR T.L_TYPEHAB LIKE '%ARI%' OR T.L_TYPEHAB LIKE '%MTI%' OR T.L_TYPEHAB LIKE '%HAB%' OR T.L_TYPEHAB LIKE '%TRS%')
GROUP BY     
    T.L_TYPEHAB ,
    E.C_EQUIPE ,
    E.L_EQUIPE ,
    E.VA_COULEQUIPE ,
    UH.C_PROFIL ,
    HE.ST_HELPDESK ,
    N.C_NATURE ,
    N.F_LECTURESEULE ,
    UH.C_FONCTION ,
    F.L_FONCTION ,
    S.C_SITEBARRE 

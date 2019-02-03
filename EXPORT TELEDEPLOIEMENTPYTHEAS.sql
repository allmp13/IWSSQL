SELECT O.N_OBJET AS [Computername]
     , CAR1.VA_CARACOBJ AS [Adresse IP Principale]
     , CAR2.VA_CARACOBJ AS [MacAddresse principale]
     , 'Masque de sous réseau principal' AS [Masque de sous réseau principal]
     , O.I_OBJ_OCS_RESEAU_SDIS AS [Type de poste]
     , CASE 
          WHEN CHARINDEX('UC', C.C_TYPOBJ) > 0
               THEN 'PC'
          ELSE 'PORTABLE'
          END AS [Type]
     , 'Type de Portable' AS [Type de portable]
     , 'Operating system' AS [Operating system]
     , 'Domaine' AS [Domaine]
     , S.N_SERVICE AS [Orga niv1 intitulé]
     , S.C_SERVICE AS [Orga niv1 identifiant]
     , 'Orga nivN intitulé' AS [Orga nivN intitulé]
     , 'Orga nivN identifiant' AS [Orga nivN identifiant]
     , U.N_UTIL AS [Nom]
     , U.PRE_UTIL AS [Prénom]
     , U.I_UTI_MATRICULERH AS [Matricule]
      , SI.N_SITE as [Loca niv1 intitulé]
     , SI.C_SITEBARRE as [Loca niv1 identifiant]
     , 'Loca nivN intitulé' as [Loca nivN intitulé]
     , 'Loca nivN identifiant' as [Loca nivN identifiant]
FROM OBJET O
     LEFT JOIN CATALOGUE C
     ON C.C_REF = O.C_REF
     LEFT JOIN UTILISATEUR U
     ON U.C_UTIL = O.C_UTIL
     LEFT JOIN SERVICE S
     ON O.C_SERVICE = S.C_SERVICE
     LEFT JOIN SITE SI
     ON SI.C_SITE = O.C_SITE
     LEFT JOIN CARACOBJ CAR1
     ON CAR1.C_OBJET = O.C_OBJET
     LEFT JOIN CARACOBJ CAR2
     ON CAR2.C_OBJET = O.C_OBJET
WHERE O.C_NATURE = 'INF'
     AND C.C_FAMOBJ = 'IUNITE'
     AND CAR1.C_CARAC = 'UC-CPUTYP'
     AND CAR1.D_HISTCARACOBJ IS NULL
     AND CAR2.C_CARAC = 'UC-VIDEOTYP'
     AND CAR2.D_HISTCARACOBJ IS NULL

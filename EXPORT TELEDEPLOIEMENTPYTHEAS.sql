SELECT
     O.N_OBJET AS [Computername]
     ,
     --CAR3.VA_CARACOBJ AS [Adresse IP Principale]
     OCS.IPADDR AS [Adresse IP Principale]
     ,
     --CAR4.VA_CARACOBJ AS [MacAddresse principale]
     'MacAddresse principale' AS [MacAddresse principale]
     ,
     'Masque de sous réseau principal' AS [Masque de sous réseau principal]
     ,
     O.I_OBJ_OCS_RESEAU_SDIS AS [Type de poste]
     ,
     CASE 
          WHEN CHARINDEX('UC', C.C_TYPOBJ) > 0
               THEN 'PC'
          ELSE 'PORTABLE'
          END AS [Type]
     ,
     --'Type de Portable' AS [Type de portable]
     T.L_TYPOBJ AS [Type de portable]
     ,
     --'Operating system' AS [Operating system]
     OCS.OSNAME AS [Operating system]
     ,
     'Domaine' AS [Domaine]
     ,
     S.N_SERVICE AS [Orga niv1 intitulï¿½]
     ,
     S.C_SERVICE AS [Orga niv1 identifiant]
     ,
     'Orga nivN intitulé' AS [Orga nivN intitulï¿½]
     ,
     'Orga nivN identifiant' AS [Orga nivN identifiant]
     ,
     U.N_UTIL AS [Nom]
     ,
     U.PRE_UTIL AS [Prénom]
     ,
     U.I_UTI_MATRICULERH AS [Matricule]
      ,
     SI.N_SITE AS [Loca niv1 intitulï¿½]
     ,
     SI.C_SITEBARRE AS [Loca niv1 identifiant]
     ,
     'Loca nivN intitulé' AS [Loca nivN intitulï¿½]
     ,
     'Loca nivN identifiant' AS [Loca nivN identifiant]
     , C.C_TYPOBJ
FROM
     suiteisilog.OBJET O
     LEFT JOIN suiteisilog.CATALOGUE C
     ON C.C_REF = O.C_REF
     LEFT JOIN suiteisilog.TYPOBJ T 
     ON T.C_TYPOBJ=C.C_TYPOBJ
     LEFT JOIN suiteisilog.UTILISATEUR U
     ON U.C_UTIL = O.C_UTIL
     LEFT JOIN suiteisilog.SERVICE S
     ON O.C_SERVICE = S.C_SERVICE
     LEFT JOIN suiteisilog.SITE SI
     ON SI.C_SITE = O.C_SITE
     /*LEFT JOIN suiteisilog.CARACOBJ CAR1
     ON CAR1.C_OBJET = O.C_OBJET
     LEFT JOIN suiteisilog.CARACOBJ CAR2
     ON CAR2.C_OBJET = O.C_OBJET
     LEFT JOIN suiteisilog.CARACOBJ CAR3
     ON CAR3.C_OBJET = O.C_OBJET
     LEFT JOIN suiteisilog.CARACOBJ CAR4
     ON CAR4.C_OBJET = O.C_OBJET*/
     LEFT JOIN (SELECT
          NAME,
          IPADDR,
          OSNAME
     FROM
          OPENQUERY (OCS, 'SELECT MAX(LASTDATE),NAME,IPADDR,OSNAME FROM hardware H GROUP BY H.NAME ')) OCS ON O.N_OBJET = (OCS.NAME COLLATE French_CS_AS)
WHERE 
     O.D_ARCHIVE IS NULL
     AND O.C_NATURE = 'INF'
     AND C.C_FAMOBJ = 'IUNITE'
     /*AND CAR1.C_CARAC = 'UC-CPUTYP'
     AND CAR1.C_CARAC = 'UC-CPUTYP'
     AND CAR1.D_HISTCARACOBJ IS NULL
     AND CAR2.C_CARAC = 'UC-VIDEOTYP'
     AND CAR2.D_HISTCARACOBJ IS NULL
     AND CAR3.C_CARAC = 'UC-CRIP_1'
     AND CAR3.D_HISTCARACOBJ IS NULL
     AND CAR4.C_CARAC = 'UC-CRMAC_1'
     AND CAR4.D_HISTCARACOBJ IS NULL*/      


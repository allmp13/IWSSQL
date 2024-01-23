SELECT
    O.N_OBJET AS [Computername],
    OCS.IPADDR AS [Adresse IP Principale],
    --'MacAddresse principale' AS [MacAddresse principale]
    '' AS [MacAddresse principale],
    -- 'Masque de sous réseau principal' AS [Masque de sous réseau principal]
    '' AS [Masque de sous réseau principal],
    O.I_OBJ_OCS_RESEAU_SDIS AS [Type de poste],
    CASE
        WHEN CHARINDEX('PO', C.C_TYPOBJ) = 0 THEN 'PC'
        ELSE 'PORTABLE'
    END AS [Type],
    T.L_TYPOBJ AS [Type de portable],
    OCS.OSNAME AS [Operating system],
    -- 'Domaine' AS [Domaine]
    'D' AS [Domaine],
    S.N_SERVICE AS [Orga niv1 intitulé],
    S.C_SERVICE AS [Orga niv1 identifiant],
    --'Orga nivN intitulé' AS [Orga nivN intitulé]
    '' AS [Orga nivN intitulé],
    --'Orga nivN identifiant' AS [Orga nivN identifiant]
    '' AS [Orga nivN identifiant],
    U.N_UTIL AS [Nom],
    U.PRE_UTIL AS [Prénom],
    U.I_UTI_MATRICULERH AS [Matricule],
    SI.N_SITE AS [Loca niv1 intitulé],
    SI.C_SITEBARRE AS [Loca niv1 identifiant],
    --'Loca nivN intitulé' AS [Loca nivN intitulé]
    '' AS [Loca nivN intitulé],
    --'Loca nivN identifiant' AS [Loca nivN identifiant]
    '' AS [Loca nivN identifiant],
    C.C_TYPOBJ
FROM suiteisilog.OBJET O
    LEFT JOIN suiteisilog.CATALOGUE C ON C.C_REF = O.C_REF
    LEFT JOIN suiteisilog.TYPOBJ T ON T.C_TYPOBJ = C.C_TYPOBJ
    LEFT JOIN suiteisilog.UTILISATEUR U ON U.C_UTIL = O.C_UTIL
    LEFT JOIN suiteisilog.SERVICE S ON O.C_SERVICE = S.C_SERVICE
    LEFT JOIN suiteisilog.SITE SI ON SI.C_SITE = O.C_SITE
    LEFT JOIN (
        SELECT NAME, IPADDR, OSNAME
        FROM
            OPENQUERY (
                OCS,
                'SELECT MAX(LASTDATE),NAME,IPADDR,OSNAME FROM hardware H GROUP BY H.NAME '
            )
    ) OCS ON O.N_OBJET = (OCS.NAME COLLATE French_CS_AS)
WHERE
    O.D_ARCHIVE Is Null
    AND O.C_NATURE = 'INF'
    AND (
        C.C_FAMOBJ = 'IUNITE'
        OR C.C_FAMOBJ = 'ISERVE'
    )
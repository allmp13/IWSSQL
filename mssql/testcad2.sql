SELECT
    O.C_BARRE           AS CAE,
    O.N_OBJET           AS NSI,
    TYPEOPS.VA_CARACOBJ AS TYPE,
    O.C_STOBJ AS STATUT,
    UO.C_SERVICEBARRE AS CS,
    UO.C_SERVICE AS CODEUO,
    SIT.C_SITEBARRE AS SITE,
    LONG.VA_CARACOBJ    AS LONGUEUR,
    VOLU.VA_CARACOBJ    AS VOLUME,
    RFGI.C_BARRE        AS RFGI,
    ANTARES.VA_CARACOBJ AS ANTARES
FROM
    suiteisilog.OBJET O
    INNER JOIN suiteisilog.CATALOGUE CAT ON CAT.C_REF = O.C_REF
    LEFT JOIN suiteisilog.SERVICE UO ON UO.C_SERVICE = O.C_SERVICE
    LEFT JOIN suiteisilog.SITE SIT ON SIT.C_SITE = O.C_SITE 
    LEFT JOIN suiteisilog.CARACOBJ LONG ON LONG.C_OBJET = O.C_OBJET AND LONG.C_CARAC = 'LV' 
    LEFT JOIN suiteisilog.CARACOBJ VOLU ON LONG.C_OBJET = O.C_OBJET AND LONG.C_CARAC = 'SV' 
    LEFT JOIN suiteisilog.CARACOBJ TYPEOPS ON TYPEOPS.C_OBJET = O.C_OBJET AND TYPEOPS.C_CARAC = 'TVEHGES_TYPEO'
    INNER JOIN 
                (SELECT
                    OBJPF.C_OBJET,
                    OBJPF.C_OBJET_PERE,
                    X.C_BARRE
                FROM
                    suiteisilog.OBJPEREFILS OBJPF
                    INNER JOIN (SELECT
                        C_OBJET,
                        C_BARRE
                    FROM
                        suiteisilog.OBJET O2
                    WHERE  C_NATURE = 'OBJTRS' AND C_BARRE IS NOT NULL AND I_OBJ_VOPS = 1) X ON X.C_OBJET = OBJPF.C_OBJET AND X.C_BARRE IS NOT NULL AND OBJPF.D_HISTLIAISON IS NULL) RFGI ON RFGI.C_OBJET_PERE = O.C_OBJET 
    INNER JOIN (
                SELECT
                OBJPF.C_OBJET,
                OBJPF.C_OBJET_PERE,
                X.VA_CARACOBJ
            FROM
                suiteisilog.OBJPEREFILS OBJPF INNER JOIN (SELECT
                    O3.C_OBJET,
                    COBJ.VA_CARACOBJ
                FROM
                    suiteisilog.OBJET O3 INNER JOIN suiteisilog.CARACOBJ COBJ ON COBJ.C_OBJET = O3.C_OBJET AND COBJ.C_CARAC = 'TRFGI'
                WHERE  O3.C_NATURE = 'OBJTRS' AND O3.I_OBJ_VOPS = 1) X ON X.C_OBJET = OBJPF.C_OBJET) ANTARES ON ANTARES.C_OBJET_PERE = O.C_OBJET
WHERE  O.I_OBJ_VOPS = 1 AND O.C_NATURE = 'VEH' AND O.N_OBJET='CCFM-100' 



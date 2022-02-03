

SELECT
    O.C_BARRE,
    C.VA_CARACOBJ,
    U.N_UTIL
FROM
    OBJET O,
    CARACOBJ C,
    UTILISATEUR U
WHERE O.C_OBJET=C.C_OBJET AND C_CARAC='OCS_TPROFIL' AND CAST(VA_CARACOBJ AS INT) > 50000 AND O.C_UTIL=U.C_UTIL
ORDER BY CAST(VA_CARACOBJ AS INT) DESC 

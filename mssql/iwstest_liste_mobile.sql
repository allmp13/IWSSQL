SELECT
    O.NO_SERIE,
    S.C_SITEBARRE,
    O.C_SERVICE,
    U.I_UTI_MATRICULERH,
    U.N_UTIL,
    U.AD_UTILEMAIL,
    O.L_COMMENT
FROM
    OBJET O,
    SITE S,
    CATALOGUE C,
    UTILISATEUR U
WHERE  O.C_SITE=S.C_SITE AND C.C_REF=O.C_REF AND C.C_FAMOBJ='IABOMOB' AND U.C_UTIL=O.C_UTIL AND O.D_ARCHIVE IS NULL
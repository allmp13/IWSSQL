SELECT
    O.NO_SERIE,
    S.C_SITEBARRE,
    O.C_SERVICE,
    U.I_UTI_MATRICULERH,
    O.L_COMMENT
FROM
    UTILISATEUR U RIGHT JOIN OBJET O ON U.C_UTIL=O.C_UTIL,
    CATALOGUE C,
    SITE S
WHERE C.C_REF=O.C_REF AND C.C_FAMOBJ='IABOMOB' AND O.C_SITE=S.C_SITE AND O.D_ARCHIVE IS NULL

SELECT
    F.L_FAMOBJ,
    T.L_TYPOBJ,
    C.L_REF,
    O.C_BARRE,
    OP.C_BARRE,
    OP.D_INVENT,
    S.L_FULLNAMESITE,
    S.C_SITEBARRE,
    SVC.L_FULLNAMESERVICE,
    SVC.C_SERVICE,
    U.N_UTIL,
    U.I_UTI_MATRICULERH,
    U.D_SORTIEUTIL

FROM
    suiteisilog.OBJPEREFILS L INNER JOIN suiteisilog.OBJET O ON O.C_OBJET=L.C_OBJET INNER JOIN suiteisilog.OBJET OP ON L.C_OBJET_PERE=OP.C_OBJET LEFT JOIN suiteisilog.SITE S ON OP.C_SITE=S.C_SITE LEFT JOIN suiteisilog.SERVICE SVC ON SVC.C_SERVICE=OP.C_SERVICE LEFT JOIN suiteisilog.UTILISATEUR U ON OP.C_UTIL=U.C_UTIL INNER JOIN suiteisilog.CATALOGUE C ON O.C_REF=C.C_REF INNER JOIN suiteisilog.TYPOBJ T ON C.C_TYPOBJ=T.C_TYPOBJ INNER JOIN suiteisilog.FAMOBJ F ON C.C_FAMOBJ=F.C_FAMOBJ
WHERE F.C_FAMPERE='ILOG' --AND OP.C_BARRE='B13U200'
AND U.D_SORTIEUTIL < getdate()
AND C.L_REF='Licence Microsoft 365'
SELECT
    A.C_ACTION,
    A.NO_APPEL,
    A.D_CREATION,
    A.D_ARCHIVE,
    A.HM_ACTIONREAL,
    A.J_ACTIONREAL,
    A.D_MAJENREG,
    A.HM_ACTIONPREV,
    A.J_ACTIONPREV,
    A.D_ACTIONPREV,
    A.D_ACTIONREAL,
    A.DF_ACTIONPREV,
    A.DF_ACTIONREAL,
    A.L_COMMENT,
    A.C_STAPPELACT,
    A.DF_PREVACTIONREAL,
    A.F_ACTIONREAL,
    A.VA_ACTCOND,
    AP.IDT_APPEL,
    CASE WHEN CR.L_CIVILITE IS NULL 
THEN 
CASE WHEN UR.PRE_UTIL IS NULL 
THEN UR.N_UTIL 
ELSE UR.N_UTIL + ' ' + UR.PRE_UTIL 
END 
ELSE CR.L_CIVILITE + ' ' + 
CASE WHEN UR.PRE_UTIL IS NULL 
THEN UR.N_UTIL 
ELSE UR.N_UTIL + ' ' + UR.PRE_UTIL 
END 
END N_UTIL_RESP,
    E.L_EQUIPE,
    CASE WHEN CU.L_CIVILITE IS NULL 
THEN 
CASE WHEN UU.PRE_UTIL IS NULL 
THEN UU.N_UTIL 
ELSE UU.N_UTIL + ' ' + UU.PRE_UTIL 
END 
ELSE CU.L_CIVILITE + ' ' + 
CASE WHEN UU.PRE_UTIL IS NULL 
THEN UU.N_UTIL 
ELSE UU.N_UTIL + ' ' + UU.PRE_UTIL 
END 
END N_UTIL,
    SI.N_SITE,
    SI.L_FULLNAMESITE FULL_SITE,
    TA.L_TACTION,
    ST.L_STAPPEL,
    OPT_URL_MAIL.VA_PARAM MAILIWSPUBLICURL,
    CS.L_REF L_REFSERVICE,
    TPB.L_FULLNAMETYPEPB,
    TPB.L_TYPEPB,
    CASE WHEN CD.L_CIVILITE IS NULL 
THEN 
CASE WHEN UD.PRE_UTIL IS NULL 
THEN UD.N_UTIL 
ELSE UD.N_UTIL + ' ' + UD.PRE_UTIL 
END 
ELSE CD.L_CIVILITE + ' ' + 
CASE WHEN UD.PRE_UTIL IS NULL 
THEN UD.N_UTIL 
ELSE UD.N_UTIL + ' ' + UD.PRE_UTIL 
END 
END N_UTIL_DEM,
    SA.L_FULLNAMESITE,
    SE.L_FULLNAMESERVICE,
    AP.DE_SYMPAPPEL,
    AP.D_APPEL,
    AP.DE_SOLUTECH,
    AP.DE_SOLUAPPEL,
    AP.DE_CAUSEPB,
    AP.D_CLOTTECH,
    AP.D_FINPREVUE,
    AP.D_CLOTURE,
    AP.L_TITRENEWS,
    AP.D_REALSOUHAITRFC,
    CASE AP.C_NATURE WHEN 'INC' 
THEN 'HELP005_INC_SYS' WHEN 'RFC' 
THEN 'HELP005_RFC_SYS' WHEN 'FAQ' 
THEN 'HELP005_FAQ_SYS' WHEN 'NEWS' 
THEN 'HELP005_NEWS_SYS' WHEN 'PB' 
THEN 'HELP005_PB_SYS' 
ELSE 'HELP005_INC_SYS' 
END C_ECRANDEST,
    Y.L_OPERATIONREEL L_MOTIF ,
    suiteisilog.GetCommonFilesUrl('Images/CustomImages/LogoIWS.png') LOGO,
    suiteisilog.GetCommonFilesUrl('Images/CustomImages/cliquer.png') CLIQUEZICI
FROM
    Z_PARAM OPT_URL_MAIL,
    ( 
SELECT
        C_ACTIONPROC,
        C_INSTPROC,
        L_OPERATIONREEL
    FROM
        ACTIONS) Y
    RIGHT JOIN (SERVICE SE
    RIGHT JOIN (SITE SA
    RIGHT JOIN (CIVILITE CD
    RIGHT JOIN (UTILISATEUR UD
    RIGHT JOIN (TYPEPB TPB
    RIGHT JOIN (CATALOGUE CS
    RIGHT JOIN (OBJET OS
    RIGHT JOIN (APPEL AP
    RIGHT JOIN (CIVILITE CU
    RIGHT JOIN (UTILISATEUR UU
    RIGHT JOIN (CIVILITE CR
    RIGHT JOIN (UTILISATEUR UR
    RIGHT JOIN (STAPPEL ST
    RIGHT JOIN (SITE SI
    RIGHT JOIN (EQUIPE E
    RIGHT JOIN (TACTION TA
    INNER JOIN QRY_ACTIONPREDEC_ACT A
    ON TA.C_TACTION = A.C_TACTION)
    ON A.C_EQUIPE = E.C_EQUIPE)
    ON SI.C_SITE = A.C_SITE)
    ON A.C_STAPPELACT = ST.C_STAPPEL)
    ON A.C_UTIL_RESP = UR.C_UTIL)
    ON CR.C_CIVILITE = UR.C_CIVILITE)
    ON A.C_UTIL = UU.C_UTIL)
    ON CU.C_CIVILITE = UU.C_CIVILITE)
    ON A.NO_APPEL = AP.NO_APPEL)
    ON AP.C_OBJETSERVICE = OS.C_OBJET)
    ON OS.C_REF = CS.C_REF)
    ON AP.C_TYPEPB = TPB.C_TYPEPB)
    ON AP.C_UTIL_DEM = UD.C_UTIL)
    ON UD.C_CIVILITE = CD.C_CIVILITE)
    ON AP.C_SITE_DEM = SA.C_SITE)
    ON AP.C_SERVICE_DEM = SE.C_SERVICE)
    ON (A.ACTPROCPRED = Y.C_ACTIONPROC
        AND A.C_INSTPROC = Y.C_INSTPROC)
WHERE OPT_URL_MAIL.C_FAMPARAM='Serveur_ME'
    AND OPT_URL_MAIL.C_PARAM = 'MAILIWSPUBLICURL' AND AP.IDT_APPEL = 'DOS0026791'
SELECT
    H.L_TYPEHAB AS [Libell� habilitation type],
    H.C_PROFIL AS [Code profil],
    H.C_FONCTION AS [Code fonction],
    F.L_FONCTION AS [Libell� fonction],
    H.C_LOV_GRADE AS [Code grade],
    ZG.L_LOV_VALUE AS [Libell� grade],
    H.C_LOV_STATUTPRO AS [Code statut professionnel],
    ZP.L_LOV_VALUE AS [Libell� statut professionnel],
    H.C_LOV_STATUTACTIVITE AS [Code Statut d'activit�],
    ZS.L_LOV_VALUE AS [Libell� statut d'activit�],
    E.C_EQUIPE AS [Code Equipe],
    EL.L_EQUIPE AS [Libell� Equipe],
    E.ST_HELPDESK AS [R�le dans l'�quipe],
    N.C_NATURE AS [Code Nature] ,
    N.F_LECTURESEULE AS [Lecture seule]
FROM
    suiteisilog.TYPEHAB H LEFT JOIN suiteisilog.TYPEHABEQUIPE E ON H.C_TYPEHAB=E.C_TYPEHAB LEFT JOIN suiteisilog.TYPEHABNATURE N ON H.C_TYPEHAB = N.C_TYPEHAB LEFT JOIN suiteisilog.EQUIPE EL ON E.C_EQUIPE=EL.C_EQUIPE LEFT JOIN suiteisilog.FONCTION F ON H.C_FONCTION=F.C_FONCTION LEFT JOIN suiteisilog.Z_LOV_VALUE ZG ON H.C_LOV_GRADE=ZG.C_LOV_VALUE LEFT JOIN suiteisilog.Z_LOV_VALUE ZP ON H.C_LOV_STATUTPRO=ZP.C_LOV_VALUE LEFT JOIN suiteisilog.Z_LOV_VALUE ZS ON H.C_LOV_STATUTACTIVITE=ZS.C_LOV_VALUE
WHERE
 H.D_ARCHIVE IS NULL
    AND ( ZG.C_LOV='C_LOV_GRADE' OR ZG.C_LOV IS NULL ) AND (ZP.C_LOV='C_LOV_STATUTPRO' OR ZP.C_LOV IS NULL) AND (ZS.C_LOV='C_LOV_STATUTACTIVITE' OR ZS.C_LOV IS NULL)


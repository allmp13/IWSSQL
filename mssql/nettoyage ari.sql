--select O.C_BARRE,A.C_TACTION,A.L_COMMENT  from suiteisilog.ACTIONS A inner join suiteisilog.OBJET O On A.C_OBJET=O.C_OBJET Where O.C_NATURE='ELTARI'

SELECT C_ACTION FROM suiteisilog.ACTIONS WHERE C_ACTION in (select A.C_ACTION  from suiteisilog.ACTIONS A inner join suiteisilog.OBJET O On A.C_OBJET=O.C_OBJET Where O.C_NATURE='ELTARI')

SELECT A.IDT_APPEL,* FROM suiteisilog.APPEL A INNER JOIN suiteisilog.OBJET O ON A.C_OBJETSERVICE=O.C_OBJET WHERE O.C_REF='I0047' and A.C_STAPPEL not like 'C%' AND A.D_ARCHIVE is null


select * from suiteisilog.STOCKCONSO where C_REF like 'TAPR%'


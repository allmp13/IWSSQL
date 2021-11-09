select C_BARRE,
       NO_SERIE,
       O.C_REF,
       L_REF,
       suiteisilog.GetValCaracObj('IREFERENCE', O.C_OBJET)   AS "IREFERENCE",
       suiteisilog.GetValCaracObj('IPROFFONCGSM', O.C_OBJET) AS "IPROFFONCGSM",
       suiteisilog.GetValCaracObj('IPROFOPSGSM', O.C_OBJET)  AS "IPROFOPSGSM"
from   OBJET O
       inner join CATALOGUE C
               on C.C_REF = O.C_REF
where  O.NO_SERIE = '0632780093'

SELECT
  suiteisilog.OBJET.C_OBJET,
  suiteisilog.OBJET.C_BARRE,
  suiteisilog.OBJET.N_OBJET,
  suiteisilog.OBJET.C_SITE,
  suiteisilog.OBJET.C_SERVICE,
  suiteisilog.OBJET.T_SERVICE,
  suiteisilog.CATALOGUE.L_REF,
  /*count(*),*/
  suiteisilog.CATALOGUE.C_NATUREOBJ
FROM
  suiteisilog.CATALOGUE INNER JOIN suiteisilog.OBJET ON (suiteisilog.OBJET.C_REF=suiteisilog.CATALOGUE.C_REF)

WHERE
  (
   suiteisilog.OBJET.C_SERVICE  IS NULL
  OR
  suiteisilog.OBJET.C_SITE  IS NULL  
  )
  AND
  suiteisilog.CATALOGUE.C_NATUREOBJ = 'INF'
/*GROUP BY suiteisilog.CATALOGUE.C_NATUREOBJ*/
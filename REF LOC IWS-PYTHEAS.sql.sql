SELECT C_SITE
     , C_SITE_PERE
     , C_SITEBARRE
     , N_SITE
     , T_LOVSITE
FROM suiteisilog.SITE
WHERE C_ROLESITE = 'P'
     AND T_LOVSITE IS NOT NULL
     AND D_ARCHIVE IS NULL

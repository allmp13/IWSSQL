SELECT C_SERVICE
     , C_SERVICE_PERE
     , N_SERVICE
     , C_SERVICEBARRE
     , *
FROM suiteisilog.SERVICE
WHERE (
          C_SERVICE_PERE <> 'ASTRE'
          OR C_SERVICE_PERE IS NULL
          )
     AND C_SERVICE <> 'ASTRE'

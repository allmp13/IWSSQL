SELECT NO_CTR,C_OBJSERVICE,L_NIVEAU,*
  FROM [suiteisilog].[LIGNECTR] 
  where NO_CTR Like 'I%'
  order by 2,3

  SELECT *
  FROM [suiteisilog].[CONTRAT] 
  where NO_CTR Like 'I%'

  select *
  from EQUIPE
 
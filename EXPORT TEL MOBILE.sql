--select U.I_UTI_MATRICULERH ,U.N_UTIL,MAX(O.NO_SERIE) FROM OBJET O LEFT JOIN UTILISATEUR U ON O.C_UTIL=U.C_UTIL  WHERE  O.C_NATURE='INF' and O.NO_SERIE LIKE '0%' and O.C_UTIL IS NOT NULL 
--group by U.N_UTIL,U.I_UTI_MATRICULERH 

--select FORMAT(cast('0632780093' as int), '0# ## ## ## ##')
--SELECT FORMAT(123456789, '##-##-#####');
select U.I_UTI_MATRICULERH , U.N_UTIL, FORMAT(cast(max(O.NO_SERIE) as int), '0# ## ## ## ##')
FROM OBJET O LEFT JOIN UTILISATEUR U ON O.C_UTIL=U.C_UTIL
WHERE  O.C_NATURE='INF'and O.C_BARRE LIKE 'SIM%' and O.C_UTIL IS NOT NULL
group by U.N_UTIL,U.I_UTI_MATRICULERH
order by U.N_UTIL



    SELECT U.I_UTI_MATRICULERH , U.N_UTIL, FORMAT(CAST(MAX(O.NO_SERIE) AS INT), '0# ## ## ## ##') as TEL
      FROM OBJET O LEFT JOIN UTILISATEUR U ON O.C_UTIL=U.C_UTIL 
     WHERE O.C_NATURE='INF' AND O.C_BARRE LIKE 'SIM%' 
       AND O.C_UTIL IS NOT NULL
  GROUP BY U.N_UTIL,U.I_UTI_MATRICULERH 
SELECT *
FROM Z_HISTSCRIPT Z 
WHERE Z.VA_NOMHISTSCRIPT LIKE '%xn%'
--WHERE ST_HISTSCRIPT = 'FAILED'
ORDER BY D_HISTSCRIPT DESC
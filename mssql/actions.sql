select A.C_UTIL,count(A.C_UTIL) as tot ,U.N_UTIL,U.I_UTI_MATRICULERH
from ACTIONS A, UTILISATEUR U
where A.C_UTIL=U.C_UTIL
group by A.C_UTIL,U.N_UTIL,U.I_UTI_MATRICULERH
order by tot DESC


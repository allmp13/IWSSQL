SELECT U.N_UTIL,U.PRE_UTIL,U.C_LOV_GRADE,U.C_ISIPARC,U.I_UTI_MATRICULERH,U.C_SITE,U.C_FONCTION FROM UTILISATEUR U WHERE /*U.C_LOV_GRADE like 'P%'  AND*/ U.C_SITE <> 51781 AND U.D_ARCHIVE IS NULL /*AND U.C_LOV_GRADE  IN ('P56','P57','P58','P85','P86','P87','P90')*/ order by U.N_UTIL

select C_LOV_VALUE,L_LOV_VALUE from Z_LOV_VALUE where C_LOV='C_LOV_GRADE' and C_LOV_VALUE like 'P%'


select * from FONCTION

select U.N_UTIL,U.PRE_UTIL,U.C_LOV_GRADE,U.C_ISIPARC,U.I_UTI_MATRICULERH,U.C_SITE,H.C_FONCTION,H.C_LOV_STATUTPRO from UTILAFFECTHAB H, UTILISATEUR U WHERE U.D_ARCHIVE IS NULL AND H.D_ARCHIVE IS NULL AND H.T_UTILAFFECT='C' AND H.C_PROFIL='CLIENT' AND U.C_UTIL=H.C_UTIL AND H.C_FONCTION <> 'LGME' AND U.C_SITE <> 51781  ORDER BY U.N_UTIL

select * from UTILAFFECTHAB
select U.N_UTIL,H.C_FONCTION,S.N_SERVICE from UTILAFFECTHAB H,UTILISATEUR U, SERVICE S
where H.D_ARCHIVE IS NULL and H.C_FONCTION like '50%' and U.C_UTIL=H.C_UTIL and H.C_SERVICE=S.C_SERVICE 

select * from HABILITATIONS_TYPE

select * from suiteisilog.CARACOBJ


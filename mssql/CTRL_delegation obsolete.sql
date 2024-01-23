--Delegations recues
select U.N_UTIL,U.PRE_UTIL,U.D_SORTIEUTIL,count(U.N_UTIL) as NB  from suiteisilog.UTILISATEUR U inner join suiteisilog.UTILAFFECT H on /*U.C_UTIL=H.C_UTIL_DELEGANT or*/ U.C_UTIL=H.C_UTIL where U.D_SORTIEUTIL< GETDATE() AND H.T_UTILAFFECT='D' group by U.N_UTIL,U.PRE_UTIL,U.D_SORTIEUTIL order by U.D_SORTIEUTIL desc
--Delegations donnees
select U.N_UTIL,U.PRE_UTIL,U.D_SORTIEUTIL,count(U.N_UTIL) as NB  from suiteisilog.UTILISATEUR U inner join suiteisilog.UTILAFFECT H on U.C_UTIL=H.C_UTIL_DELEGANT /*or U.C_UTIL=H.C_UTIL*/ where U.D_SORTIEUTIL< DATEADD(month, + 3, GETDATE()) AND H.T_UTILAFFECT='D' group by U.N_UTIL,U.PRE_UTIL,U.D_SORTIEUTIL order by NB desc

SELECT  MAX(C_SITE) FROM suiteisilog.UTILISATEUR WHERE C_UTIL = '80524'

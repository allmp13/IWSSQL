select { fn WEEK(A.D_APPEL) } AS Week,UO.I_UO_LIBLONG,L.L_LIGNEDEM,L.VA_CARACVARIANTE,SUM(L.Q_DEMLIGNEDEM) from LIGNEDEM L, APPEL A, OBJET O, SERVICE UO
where L.NO_APPEL=A.NO_APPEL  and O.C_OBJET=A.C_OBJETSERVICE AND O.C_REF in ('PTV0013') AND A.ST_APPROBATION='V' AND A.C_SERVICE_DEM=UO.C_SERVICE
group by { fn WEEK(A.D_APPEL) } ,UO.I_UO_LIBLONG,L.L_LIGNEDEM,L.VA_CARACVARIANTE ORDER by UO.I_UO_LIBLONG

select A.D_APPEL ,UO.I_UO_LIBLONG,L.L_LIGNEDEM,L.VA_CARACVARIANTE,L.Q_DEMLIGNEDEM from LIGNEDEM L, APPEL A, OBJET O, SERVICE UO
where L.NO_APPEL=A.NO_APPEL  and O.C_OBJET=A.C_OBJETSERVICE AND O.C_REF in ('PTV0013') AND A.ST_APPROBATION='V' AND A.C_SERVICE_DEM=UO.C_SERVICE



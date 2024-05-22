SELECT S.C_SERVICE,S.L_FULLNAMESERVICE,count(O.C_BARRE) AS NB FROM suiteisilog.SERVICE S LEFT JOIN suiteisilog.OBJET O on S.C_SERVICE = O.C_SERVICE 
Where  S.L_FULLNAMESERVICE not like 'ASTRE%'
GROUP BY S.C_SERVICE,S.L_FULLNAMESERVICE HAVING count(O.C_BARRE) = 0
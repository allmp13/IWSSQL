SELECT DISTINCT N.NAME,
                N.SSN,
                N.[UC-CRIP],
                N.[UC-CRIPVAL],
                N.[UC-CRMAC],
                N.[UC-CRMACVAL]
FROM   suiteisilog.OBJET O
       INNER JOIN Openquery (OCS, '  SELECT H.NAME,
											B.SSN, 
											CONCAT(''UC-CRIP_'' , CAST((SELECT count(*) + 1 FROM networks X WHERE X.HARDWARE_ID = net.HARDWARE_ID AND X.ID > net.ID AND X.STATUS="UP") AS CHAR) ) AS ''UC-CRIP'', 
											net.IPADDRESS  AS ''UC-CRIPVAL'', 
											CONCAT(''UC-CRMAC_'' , CAST((SELECT count(*) + 1 FROM networks Y WHERE Y.HARDWARE_ID = net.HARDWARE_ID AND Y.ID > net.ID AND Y.STATUS="UP") AS CHAR) ) AS ''UC-CRMAC'', 
											net.MACADDR  AS ''UC-CRMACVAL'' 
										FROM  (SELECT * FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY NAME ORDER BY LASTDATE DESC) NUM from hardware ) V WHERE V.NUM=1) H
										INNER JOIN (SELECT Max(ID) MAX_ID,NAME FROM (SELECT * FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY NAME ORDER BY LASTDATE DESC) NUM from hardware ) V WHERE V.NUM=1) HD GROUP BY NAME) TH
										ON TH.NAME = H.NAME
										AND H.ID = TH.MAX_ID
										INNER JOIN networks net 
										ON net.HARDWARE_ID = H.ID and net.status="UP"   																		 
										INNER JOIN bios B
										ON B.HARDWARE_ID = H.ID
										WHERE H.NAME <> ''''
										AND H.NAME IS NOT NULL  AND H.NAME="B13u200"') N
               ON O.N_OBJET = ( N.NAME COLLATE French_CS_AS )


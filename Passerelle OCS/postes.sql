

SELECT DISTINCT TAB.SSN,
                TAB.NAME,
                TAB.SMANUFACTURER,
                TAB.SMODEL,
                TAB.DEVICEID,
                CONVERT(varchar, TAB.LASTDATE, 103) AS LASTDATE,
                RESEAU_SDIS,
                URL_OCS,
                VA_CARACOBJ,
                SITE_RESEAU
FROM   (SELECT DISTINCT T.SSN,
                        T.SMANUFACTURER,
                        T.SMODEL,
                        T.DEVICEID,
                        T.LASTDATE,
                        CASE
                          WHEN T.IPSRC IS NULL THEN 'Hors réseau'
                          ELSE
                            CASE
                              WHEN T.IPSRC LIKE '10.206%' THEN 'Opérationnel'
                              ELSE 'Administratif'
                            END
                        END                           RESEAU_SDIS,
                        T.NAME,
                        CASE
                          WHEN CO_URL.VA_CARACOBJ IS NOT NULL THEN 'http://srvocs.sdis13.fr/ocsreports/index.php?function=computer&head=1&systemid='
                                                                   + CO_URL.VA_CARACOBJ
                          ELSE NULL
                        END                           AS URL_OCS,
                        CO_URL.VA_CARACOBJ,
                        /*NID.NAME_NETID                AS SITE_RESEAU,*/
                        NID.NAME                AS SITE_RESEAU,
                        Row_number()
                          OVER(
                            PARTITION BY T.NAME
                            ORDER BY T.LASTDATE DESC) AS ROW
        FROM   OBJET O
               INNER JOIN Openquery (OCS, 'SELECT H.ID,
										   	      B.HARDWARE_ID,
										   	      B.SSN,
										   	      B.SMANUFACTURER,
										   	      B.SMODEL,
										   	      B.TYPE,
										   	      H.DEVICEID,
										   	      H.NAME,
										   	      H.IPSRC,
										   	      H.LASTDATE,
										   	      NT.IPADDRESS
										     FROM bios B
										   	   INNER JOIN hardware H
										   			   ON H.ID = B.HARDWARE_ID
										   	   LEFT JOIN networks NT
										   			  ON NT.HARDWARE_ID = H.ID
										   				 AND NT.STATUS = ''UP''
										   				 AND NT.TYPE = ''Ethernet''
										    WHERE B.SSN <> ''''
										      AND B.SSN IS NOT NULL ') T
                       ON O.N_OBJET = ( T.NAME COLLATE French_CS_AS )
               LEFT JOIN CARACOBJ CO_URL
                      ON CO_URL.C_OBJET = O.C_OBJET
                         AND CO_URL.C_CARAC = 'OCS_ID'
                         AND CO_URL.D_HISTCARACOBJ IS NULL
               LEFT JOIN  Openquery (OCS, 'SELECT subnet.NAME,hardware.ID
FROM (hardware INNER JOIN networks ON (hardware.IPADDR = networks.IPADDRESS) AND (hardware.ID = networks.HARDWARE_ID)) INNER JOIN subnet ON networks.IPSUBNET = subnet.NETID') NID
                      ON T.ID = NID.ID      
                      ) TAB
WHERE  TAB.ROW = 1


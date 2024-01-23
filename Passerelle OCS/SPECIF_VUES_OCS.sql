--------------------------------------------------------------------------------------------------------------
-- APPLICATION  : SDIS 13
-- PROPRIETAIRE : Passerlle OCS  
-- AUTEUR       : RBEN		
-- VERSION      : 1.0
--------------------------------------------------------------------------------------------------------------
--- Creer des caracteristique dans la base
--------------------------------------------------------------------------------------------------------------
BEGIN
    EXEC dbo.IsiScriptCompleted
      'SPECIF_VUES_OCS.SQL',
      '290 I0913'
END

GO

--------------------------------------------------------------------------------------------------------------
§ SPECIF_V_OCS_POSTES

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
                          WHEN T.IPSRC IS NULL THEN 'Hors reseau'
                          ELSE
                            CASE
                              WHEN T.IPSRC LIKE '10.206%' THEN 'Operationnel'
                              ELSE 'Administratif'
                            END
                        END                           RESEAU_SDIS,
                        T.NAME,
                        CASE
                          WHEN T.HARDWARE_ID IS NOT NULL THEN 'https://srvocs-adm.sdis13.fr/ocsreports/index.php?function=computer&head=1&systemid='
                                                                   + cast(T.HARDWARE_ID as varchar)
                          ELSE NULL
                        END                           AS URL_OCS,
                        CO_URL.VA_CARACOBJ,
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
										   	   INNER JOIN (SELECT * FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY NAME ORDER BY LASTDATE DESC) NUM from hardware ) V WHERE V.NUM=1) H
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
               LEFT JOIN Openquery (OCS, 'SELECT subnet.NAME,H.ID
											FROM ((SELECT * FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY NAME ORDER BY LASTDATE DESC) NUM from hardware ) V WHERE V.NUM=1) H
													INNER JOIN networks ON (H.IPADDR = networks.IPADDRESS) 
														  AND (H.ID = networks.HARDWARE_ID)) 
									  INNER JOIN subnet ON networks.IPSUBNET = subnet.NETID') NID
                      ON T.ID = NID.ID) TAB
				--Ancienne jointure
				--LEFT JOIN Openquery (OCS, 'SELECT NETID,NAME AS NAME_NETID  FROM subnet') NID
				--       ON NID.NETID = T.IPADDRESS) TAB
WHERE  TAB.ROW = 1

GO

--------------------------------------------------------------------------------------------------------------
--------------Creation de vue pour recuperer les caracs
--------------------------------------------------------------------------------------------------------------
§ SPECIF_V_OCS_CARAC

SELECT P.NAME,
       P.HARDWARE_ID,
       P.OSNAME,
       P.USERID,
       P.OSCOMMENTS,
       P.PROCESSORT,
       P.PROCESSORS,
       P.PROCESSORN,
       P.MAXSLOT,
       CAST(P.UC_RAM AS NUMERIC) UC_RAM,
       P.SSN,
       P.DESIGN_VID,
       P.VID_MEMORY,
       P.RESOLUTION,
       P.BVERSION,
       P.BIOS_TYPE,
       P.NAME_VIDEO,
       P.NB_SLOTS,
       P.SMODEL, 
	   P.RECUP
FROM   OBJET O
       INNER JOIN Openquery (OCS, 'SELECT DISTINCT H.NAME,
                                                   H.OSNAME,
                                                   H.USERID,
                                                   H.OSCOMMENTS,
                                                   H.PROCESSORT,
                                                   H.PROCESSORS,
                                                   H.PROCESSORN,
                                                   T.MAXSLOT,
                                                   T.SUMCAPCITY UC_RAM,
                                                   V.NAME       DESIGN_VID,
                                                   V.MEMORY     VID_MEMORY,
                                                   V.RESOLUTION,
                                                   B.BVERSION,
                                                   B.TYPE       BIOS_TYPE,
                                                   B.HARDWARE_ID,
                                                   B.SSN,
												   V.NAME AS NAME_VIDEO,
												   NBSLOTS.NB_SLOTS NB_SLOTS,
												   B.SMODEL,
												   PU.RECUP
                                            FROM   (SELECT * FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY NAME ORDER BY LASTDATE DESC) NUM from hardware ) V WHERE V.NUM=1) H
                                                   INNER JOIN (SELECT Max(ID) MAX_ID,
                                                                      NAME
                                                               FROM  (SELECT * FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY NAME ORDER BY LASTDATE DESC) NUM from hardware ) V WHERE V.NUM=1) HD
                                                               GROUP  BY NAME) TH
                                                           ON TH.NAME = H.NAME
                                                              AND H.ID = TH.MAX_ID
												   INNER JOIN registry RE
												   ON RE.HARDWARE_ID = H.ID
												   AND RE.NAME = ''Dernier Utilisateur Connecte''
												   LEFT JOIN profilsutilisateurs PU
														   ON PU.HARDWARE_ID = RE.HARDWARE_ID
														   AND PU.NOM = (SELECT SUBSTRING(RE.REGVALUE,LOCATE(''/'', RE.REGVALUE) + 1, LENGTH(RE.REGVALUE)))
                                                   INNER JOIN bios B
                                                           ON B.HARDWARE_ID = H.ID
                                                   INNER JOIN memories M
                                                           ON H.ID = M.HARDWARE_ID
                                                   INNER JOIN videos V
                                                           ON H.ID = V.HARDWARE_ID
                                                   INNER JOIN (SELECT Max(NUMSLOTS) MAXSLOT,
                                                                      Sum(CAPACITY) SUMCAPCITY,
                                                                      HARDWARE_ID
                                                               FROM   memories
                                                               GROUP  BY HARDWARE_ID) T
                                                           ON T.HARDWARE_ID = M.HARDWARE_ID 
												   LEFT JOIN (SELECT hardware_ID, count(hardware_ID) AS NB_SLOTS
																FROM memories
																GROUP BY hardware_ID) NBSLOTS
															ON NBSLOTS.hardware_ID = M.HARDWARE_ID
													WHERE  H.NAME IS NOT NULL
													 AND H.NAME <> ''''') P
               ON ( P.NAME COLLATE French_CS_AS ) = O.N_OBJET

GO

--------------------------------------------------------------------------------------------------------------
--------------Creation de vue pour recuperer les carac  : adresse IP(UC-CRIP), Adresse MAC UC-CRMAC
--------------------------------------------------------------------------------------------------------------========================================> OK
§ SPECIF_V_OCS_CARAC_NETWORK

SELECT DISTINCT N.NAME,
                N.SSN,
                N.[UC-CRIP],
                N.[UC-CRIPVAL],
                N.[UC-CRMAC],
                N.[UC-CRMACVAL]
FROM   OBJET O
       INNER JOIN Openquery (OCS, '  SELECT H.NAME,
											B.SSN, 
											CONCAT(''UC-CRIP_'' , CAST((SELECT count(*) + 1 FROM networks X WHERE X.HARDWARE_ID = networks.HARDWARE_ID AND X.ID > networks.ID) AS CHAR) ) AS ''UC-CRIP'', 
											networks.IPADDRESS  AS ''UC-CRIPVAL'', 
											CONCAT(''UC-CRMAC_'' , CAST((SELECT count(*) + 1 FROM networks Y WHERE Y.HARDWARE_ID = networks.HARDWARE_ID AND Y.ID > networks.ID) AS CHAR) ) AS ''UC-CRMAC'', 
											networks.MACADDR  AS ''UC-CRMACVAL'' 
										FROM  (SELECT * FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY NAME ORDER BY LASTDATE DESC) NUM from hardware ) V WHERE V.NUM=1) H
										INNER JOIN (SELECT Max(ID) MAX_ID,NAME FROM (SELECT * FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY NAME ORDER BY LASTDATE DESC) NUM from hardware ) V WHERE V.NUM=1) HD GROUP BY NAME) TH
										ON TH.NAME = H.NAME
										AND H.ID = TH.MAX_ID
										INNER JOIN networks
										ON networks.HARDWARE_ID = H.ID          																		 
										INNER JOIN bios B
										ON B.HARDWARE_ID = H.ID
										WHERE H.NAME <> ''''
										AND H.NAME IS NOT NULL ') N
               ON O.N_OBJET = ( N.NAME COLLATE French_CS_AS )

GO

--------------------------------------------------------------------------------------------------------------
--------------Creation de vue pour recuperer les carac  :  designation de partition(UC-DDPART), taille de partition (UC-DDMEM)
--------------------------------------------------------------------------------------------------------------------------------------------------------- A completer
§ SPECIF_V_OCS_CARAC_STORAGE

SELECT DISTINCT D.NAME,
                D.SSN,
                D.[DDDESIGNATIONTVAL],
                D.[UC-DDDESIGNATION],
                D.[UC-DDPART],
                D.DDPARTVAL,
                D.[UC-DDMEM],
                D.DDMEMVAL
FROM   OBJET O
       INNER JOIN Openquery (OCS, '  SELECT H.NAME, 
											B.SSN,		   
											CONCAT(''UC-DDDESIGNA_'' , CAST((SELECT count(*) + 1 FROM storages ST WHERE ST.HARDWARE_ID = S.HARDWARE_ID AND S.TYPE LIKE ''%hard DISK%'' AND ST.ID < S.ID) AS CHAR) ) AS ''UC-DDDESIGNATION'',
											CONCAT( CONCAT(CONCAT(S.DESCRIPTION , '' '') , CAST(Round((S.DISKSIZE/1024),2) AS CHAR)), '' GO '')  AS ''DDDESIGNATIONTVAL'',
                         					CONCAT(''UC-DDPART_'' , CAST((SELECT count(*) + 1 FROM storages ST WHERE ST.HARDWARE_ID = S.HARDWARE_ID AND S.TYPE LIKE ''%hard DISK%'' AND ST.ID < S.ID) AS CHAR) ) AS ''UC-DDPART'', S.NAME  AS ''DDPARTVAL'',
                                    		CONCAT(''UC-DDMEM_'' , CAST((SELECT count(*) + 1 FROM storages ST WHERE ST.HARDWARE_ID = S.HARDWARE_ID AND S.TYPE LIKE ''%hard DISK%'' AND ST.ID< S.ID) AS CHAR) ) AS ''UC-DDMEM'', S.DISKSIZE  AS ''DDMEMVAL''
									   FROM (SELECT * FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY NAME ORDER BY LASTDATE DESC) NUM from hardware ) V WHERE V.NUM=1) H
										INNER JOIN (SELECT Max(ID) MAX_ID,NAME FROM   (SELECT * FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY NAME ORDER BY LASTDATE DESC) NUM from hardware ) V WHERE V.NUM=1) HD GROUP  BY NAME) TH
										ON TH.NAME = H.NAME AND H.ID = TH.MAX_ID
										INNER JOIN storages S
                         				ON S.HARDWARE_ID = H.ID
                         				INNER JOIN bios B
                         				ON B.HARDWARE_ID = H.ID
                                    	WHERE S.TYPE LIKE ''%hard DISK%''
                                          AND H.NAME<> ''''
                         				  AND H.NAME IS NOT NULL ') D
               ON O.N_OBJET = ( D.NAME COLLATE French_CS_AS )

GO

---------------------------------------------------------------------------------------------------------------------------------------------------------
--------------Creation de vue pour recuperer les carac  :  type de disque , systeme de fichier, FREE
--------------------------------------------------------------------------------------------------------------------------------------------------------- A completer
§ SPECIF_V_OCS_CARAC_DRIVES

SELECT DISTINCT D.NAME,
                D.SSN,
                D.NOM_LECTEUR,
                D.NOM_LECTEURVAL,
                D.LECTEUR,
                D.LETTERVAL,
                D.TYPLECT,
                D.TYPLECTVAL,
                D.FSLECT,
                D.FILESYSTEMTVAL,
                D.TLTMB,
                D.TAILLELECTOTALVAL,
                D.TLLMB,
                D.TAILLELECLIBREVAL,
                D.OCCUPMB,
                D.OCCUPVAL,
                D.PUTIL,
                D.PUTILVAL,
                D.LECRESEAU,
                CASE
                  WHEN TYPE LIKE '%Network%' THEN 'Oui'
                  ELSE 'Non'
                END LECRESEAUVAL
FROM   OBJET O
       INNER JOIN Openquery (OCS, 'SELECT  H.NAME, B.SSN,
									CONCAT(''NOM_LECT_'', CAST((SELECT count(*) + 1 FROM drives ST WHERE ST.HARDWARE_ID = S.HARDWARE_ID AND ST.ID < S.ID) AS CHAR) )  AS ''NOM_LECTEUR'',
									CONCAT(CONCAT(S.LETTER , '' '') , CAST(VOLUMN  AS CHAR))   AS ''NOM_LECTEURVAL'',
									CONCAT(''LECTEUR_L'', CAST((SELECT count(*) + 1 FROM drives ST WHERE ST.HARDWARE_ID = S.HARDWARE_ID AND ST.ID < S.ID) AS CHAR) ) AS ''LECTEUR'', S.LETTER  AS ''LETTERVAL'',
									CONCAT(''TYPLECT_L'', CAST((SELECT count(*) + 1 FROM drives ST WHERE ST.HARDWARE_ID = S.HARDWARE_ID AND ST.ID < S.ID) AS CHAR) ) AS ''TYPLECT'', S.TYPE  AS ''TYPLECTVAL'',
									CONCAT(''FSLECT_L'' , CAST((SELECT count(*) + 1 FROM drives ST WHERE ST.HARDWARE_ID = S.HARDWARE_ID AND ST.ID < S.ID) AS CHAR) ) AS ''FSLECT'', S.FILESYSTEM  AS ''FILESYSTEMTVAL'',
									CONCAT(''TLTMB_L''  , CAST((SELECT count(*) + 1 FROM drives ST WHERE ST.HARDWARE_ID = S.HARDWARE_ID AND ST.ID < S.ID) AS CHAR) ) AS ''TLTMB'', S.TOTAL  AS ''TAILLELECTOTALVAL'',
									CONCAT(''TLLMB_L''  , CAST((SELECT count(*) + 1 FROM drives ST WHERE ST.HARDWARE_ID = S.HARDWARE_ID AND ST.ID < S.ID) AS CHAR) ) AS ''TLLMB'', S.FREE  AS ''TAILLELECLIBREVAL'',
									CONCAT(''OCCUP_L''  , CAST((SELECT count(*) + 1 FROM drives ST WHERE ST.HARDWARE_ID = S.HARDWARE_ID AND ST.ID<   S.ID) AS CHAR) ) AS ''OCCUPMB'', (S.TOTAL - S.FREE)  AS ''OCCUPVAL'',
									CONCAT(''PUTIL_L''  , CAST((SELECT count(*) + 1 FROM drives ST WHERE ST.HARDWARE_ID = S.HARDWARE_ID AND ST.ID< S.ID) AS CHAR) ) AS ''PUTIL'', Round((((S.TOTAL - S.FREE)*100)/S.TOTAL),2)  AS ''PUTILVAL'',
									CONCAT(''LECRES_L'' , CAST((SELECT count(*) + 1 FROM drives ST WHERE ST.HARDWARE_ID = S.HARDWARE_ID AND ST.ID <S.ID) AS CHAR) ) AS ''LECRESEAU'',
									S.TYPE
									FROM  (SELECT * FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY NAME ORDER BY LASTDATE DESC) NUM from hardware ) V WHERE V.NUM=1) H
									INNER JOIN (SELECT Max(ID) MAX_ID,NAME FROM   hardware HD GROUP  BY NAME) TH
										ON TH.NAME = H.NAME AND H.ID = TH.MAX_ID 
										INNER JOIN   drives S
										ON S.HARDWARE_ID = H.ID
										INNER JOIN bios B
										ON B.HARDWARE_ID = H.ID
									WHERE H.NAME<> ''''
									  AND H.NAME IS NOT NULL') D
               ON O.N_OBJET = ( D.NAME COLLATE French_CS_AS )

GO

--------------------------------------------------------------------------------------------------------------
--------------Creation de vue pour recuperer les carac  : registres
---------------- A confirmer avec CODR l'histoire de champ  N° de serie
--------------------------------------------------------------------------------------------------------------
§ SPECIF_V_OCS_REGISTRES

SELECT DISTINCT C.C_CARAC,
                O.N_OBJET,
                CAST(T.REGVALUE AS VARCHAR(MAX)) AS REGVALUE,
                T.NAME,
                T.NAME_REGISTRE
FROM   Openquery (OCS, 'SELECT  H.NAME, R.NAME NAME_REGISTRE,R.REGVALUE,R.HARDWARE_ID 
   					      FROM (SELECT * FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY NAME ORDER BY LASTDATE DESC) NUM from hardware ) V WHERE V.NUM=1) H
						   INNER JOIN registry R 
						   ON R.HARDWARE_ID=H.ID') T
       INNER JOIN OBJET O
               ON O.N_OBJET = CAST(( T.NAME COLLATE French_CS_AS ) AS VARCHAR)
       INNER JOIN CARAC C
               ON C.L_CARAC = ( T.NAME_REGISTRE COLLATE French_CS_AS )

GO

--------------------------------------------------------------------------------------------------------------
--------------Creation de vue pour recuperer les logiciels
--------------------------------------------------------------------------------------------------------------
-- § SPECIF_V_OCS_LOGICIEL
-- 
-- SELECT DISTINCT L.SSN,
--                 L.VERSION,
--                 L.NAME,
--                 CAT.I_CAT_LOGOCS,
-- 				CONCAT(L.VERSION, ' - ', L.ID) AS ID
-- FROM   Openquery (OCS, 'SELECT S.NAME ,S.VERSION,
--                                  B.SSN, S.ID
--                           FROM   softwares S
--                                  INNER JOIN bios B
--                                          ON S.HARDWARE_ID = B.HARDWARE_ID') L
--        INNER JOIN OBJET O
--                ON O.NO_SERIE = ( L.SSN COLLATE French_CS_AS )
--        INNER JOIN CATALOGUE CAT
--                ON CASE
--                     WHEN ( L.VERSION COLLATE French_CS_AS ) IS NOT NULL THEN ( L.NAME COLLATE French_CS_AS ) + ' ' + ( L.VERSION COLLATE French_CS_AS )
--                     ELSE ( L.NAME COLLATE French_CS_AS )
--                   END LIKE CAT.I_CAT_LOGOCS
--                   AND CAT.I_CAT_LOGOCS IS NOT NULL
-- 
-- GO

IF OBJECT_ID ('JMD') IS NOT NULL DROP TABLE JMD

SELECT L.NOM,L.VERSION,CASE
                    WHEN ( L.VERSION COLLATE French_CS_AS  IS NOT NULL) AND (CHARINDEX( L.VERSION COLLATE French_CS_AS,L.NAME COLLATE French_CS_AS  ) >0)  THEN UPPER( L.NAME COLLATE French_CS_AS )
                    ELSE UPPER( L.NAME COLLATE French_CS_AS  + ' ' +  L.VERSION COLLATE French_CS_AS )
                  END AS NAME, CAT.I_CAT_LOGOCS,L.ID
INTO JMD
FROM   Openquery (OCS, '
SELECT
    software_name.NAME,
    software_version.VERSION,
    software_publisher.PUBLISHER,
    H.ID,
    H.NAME  AS NOM
    
from (( (
            software
            INNER JOIN software_name ON software.NAME_ID = software_name.ID 
        )
        INNER JOIN software_version ON software.VERSION_ID = software_version.ID
    )
    INNER JOIN software_publisher ON software.PUBLISHER_ID = software_publisher.ID
    ) 
    inner join (
        SELECT
            MAX(ID) AS ID,
            NAME,
            LASTDATE
        FROM hardware
        WHERE
            LASTDATE > DATE_ADD(CURDATE(), INTERVAL -2 YEAR)
        GROUP BY
            NAME
    ) H ON software.HARDWARE_ID = H.ID
') L
INNER JOIN suiteisilog.OBJET O
               ON O.C_BARRE = ( L.NOM COLLATE French_CS_AS )
INNER JOIN suiteisilog.CATALOGUE CAT
               ON  UPPER( L.NAME COLLATE French_CS_AS )
                 LIKE CAT.I_CAT_LOGOCS

                  AND CAT.I_CAT_LOGOCS IS NOT NULL

GO

--------------------------------------------------------------------------------------------------------------
--------------Fonction pour recuperer l'url ocs 
--------------------------------------------------------------------------------------------------------------
IF EXISTS (SELECT 1
           FROM   sysobjects
           WHERE  NAME = 'SPECIF_FCT_GET_LIEN_OCS'
                  AND TYPE = 'FN')
  DROP FUNCTION SPECIF_FCT_GET_LIEN_OCS

GO

CREATE FUNCTION SPECIF_FCT_GET_LIEN_OCS(@I_OBJ_OCS_URL VARCHAR(100))
RETURNS VARCHAR(4000)
AS
  BEGIN
      DECLARE @Lien VARCHAR(4000)

      SELECT @Lien = '<a href="' + @I_OBJ_OCS_URL
                     + '" target="_blank">' + 'Lien OCS' + '</a>'

      RETURN @Lien
  END

GO 

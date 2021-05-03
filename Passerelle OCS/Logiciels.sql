SELECT
    CAT.I_CAT_LOGOCS,
    CASE
                    WHEN (( L.VERSION COLLATE French_CS_AS ) IS NOT NULL) AND (( L.NAME COLLATE French_CS_AS ) NOT LIKE '%[0-9]' ) THEN ( L.NAME COLLATE French_CS_AS ) + ' ' + ( L.VERSION COLLATE French_CS_AS )
                    ELSE L.NAME COLLATE French_CS_AS 
                  END,
    *
FROM
    OPENQUERY (OCS, 'SELECT S.NAME ,S.VERSION , S.PUBLISHER,
                                 B.SSN , B.HARDWARE_ID
                          FROM   softwares S
                                 INNER JOIN bios B
                                         ON S.HARDWARE_ID = B.HARDWARE_ID') L

    INNER JOIN OBJET O
    ON O.NO_SERIE = ( L.SSN COLLATE French_CS_AS )
    INNER JOIN CATALOGUE CAT
    ON CASE
                    WHEN (( L.VERSION COLLATE French_CS_AS ) IS NOT NULL) AND (( L.NAME COLLATE French_CS_AS )  NOT LIKE '%[0-9]' ) THEN UPPER(( L.NAME COLLATE French_CS_AS ) + ' ' + ( L.VERSION COLLATE French_CS_AS ))
                    ELSE UPPER( L.NAME COLLATE French_CS_AS )
                  END LIKE UPPER(CAT.I_CAT_LOGOCS)
        AND CAT.I_CAT_LOGOCS IS NOT NULL
ORDER BY L.SSN

select * from Openquery (OCS, 'select * from (SELECT *, ROW_NUMBER() OVER (PARTITION BY NAME ORDER BY LASTDATE DESC) NUM from hardware) V WHERE V.NAME like ''b13u200'' AND V.NUM = 1' ) 

select U.N_UTIL,U.PRE_UTIL,H.* from UTILAFFECTHAB H  inner join UTILISATEUR U on U.C_UTIL=H.C_UTIL where H.C_UTIL_DELEGANT='86051'

select N_UTIL,PRE_UTIL,* from UTILISATEUR where N_UTIL = 'ISILOG'
select * from UTILAFFECTHAB where C_UTIL_DELEGANT='78511'
select C_PROFIL,count(*) from UTILISATEUR group by C_PROFIL
select N_UTIL,PRE_UTIL,*  from UTILISATEUR where C_PROFIL is null
select * from APPEL where C_UTIL_CLOS='86233'

SELECT
  *
FROM
  TYPEHAB
WHERE D_ARCHIVE IS NOT NULL
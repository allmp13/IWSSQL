SELECT
    hardware.ipaddr AS ipaddr,
    (SELECT
        name
    FROM
        subnet
    WHERE NETID = networks.IPSUBNET) AS nom_reseau,
    hardware.ID,
    memory,
    CONVERT(CONVERT(hardware.name using binary
) using utf8) AS NAME, LASTDATE, CONVERT
(CONVERT
(hardware.userid using binary) using utf8) AS USERID,
 CONVERT
(CONVERT
(osname using binary) using utf8) AS OSNAME,CONVERT
(CONVERT
(oscomments using binary) using utf8) AS OSCOMMENTS,
 CONVERT
(CONVERT
(userdomain using binary) using utf8) AS USERDOMAIN, CONVERT
(CONVERT
(workgroup using binary) using utf8) AS WORKGROUP 
 FROM hardware, networks
 WHERE networks.HARDWARE_ID = hardware.ID AND hardware.ipaddr = networks.ipaddress
 AND hardware.lastdate > '" & GBdate(dateinv) & "'
 AND hardware.name NOT LIKE 'ddsis%' AND hardware.name NOT LIKE 'win%'
SELECT DISTINCT L.NAME, L.VERSION 
FROM   Openquery (OCS, 'SELECT S.NAME, S.VERSION
                          FROM   SOFTWARES S WHERE S.NAME <> "" AND S.NAME like "Microsoft Office Standard%"' ) L
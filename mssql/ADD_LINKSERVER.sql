IF EXISTS(SELECT
        *
    FROM
        sys.servers
    WHERE name='TESTJMD')
EXEC sp_dropserver
   @server = N'TESTJMD',
   @droplogins ='droplogins';



EXEC sp_addlinkedserver
     @server = N'TESTJMD',
     @srvproduct = N'jm',
     @provider = N'Microsoft.ACE.OLEDB.12.0',
     @datasrc = N'c:\GENHABLGME\PROD\LGME - EQUIPE COMPETENTE GSIC_PROD.xlsx',
     @provstr = N'Excel 12.0';

   EXEC sp_addlinkedsrvlogin
   @rmtsrvname = N'TESTJMD',
   @useself ='false', 
   @locallogin = NULL,
   @rmtuser = NULL,
   @rmtpassword = NULL;
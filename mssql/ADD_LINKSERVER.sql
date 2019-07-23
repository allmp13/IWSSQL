EXEC sp_addlinkedserver
     @server = N'TESTJMD',
     @srvproduct = N'jm',
     @provider = N'Microsoft.ACE.OLEDB.12.0',
     @datasrc = N'C:\temp\LGME - EQUIPE COMPETENTE GSIC.xlsx',
     @provstr = N'Excel 12.0';

   EXEC sp_addlinkedsrvlogin
   @rmtsrvname = N'TESTJMD',
   @useself ='false', 
   @locallogin = NULL,
   @rmtuser = NULL,
   @rmtpassword = NULL;
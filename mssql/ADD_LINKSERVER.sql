EXEC sp_addlinkedserver
     @server = N'EXCELDATA',
     @srvproduct = N'jm',
     @provider = N'Microsoft.ACE.OLEDB.12.0',
     @datasrc = N'C:\jmd.xlsx',
     @provstr = N'Excel 12.0';

   EXEC sp_addlinkedsrvlogin
   @rmtsrvname = N'EXCELDATA',
   @useself ='false', 
   @locallogin = NULL,
   @rmtuser = NULL,
   @rmtpassword = NULL;
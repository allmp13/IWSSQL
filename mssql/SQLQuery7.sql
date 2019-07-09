use test
SELECT *

FROM   OPENROWSET('Microsoft.ACE.OLEDB.12.0',

       'Excel 12.0 Xml;HDR=YES;Database=C:\temp\GSIC.xlsx',

       'SELECT * FROM [Matrice$]')
	   go
SELECT * FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0',
  'Excel 12.0;Database=C:\LGME - EQUIPE COMPETENTE GSIC.xlsx', [matrice$])
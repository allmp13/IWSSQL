
DECLARE @Racine_Matrice varchar(255)
DECLARE @nomfichier varchar(255)
SET @Racine_Matrice='C:\GENHABLGME\'

PRINT 'DEBUT'

SET @nomfichier = '''' + @Racine_Matrice + 'UTILISATEURS.csv' + ''''
SET @nomfichier = 'BULK INSERT GENHABLGME.dbo.UTILISATEURS FROM ' + @nomfichier + ' WITH (FIELDTERMINATOR='';'',FIRSTROW=2,CODEPAGE=''ACP'')'
PRINT @nomfichier
EXECUTE( @nomfichier)

SET @nomfichier = '''' + @Racine_Matrice + 'TEST_HABILITATIONS_COMPLEMENTAIRES.csv' + ''''
SET @nomfichier = 'BULK INSERT GENHABLGME.dbo.HABILITATIONS_COMPLEMENTAIRES_SPV FROM '+ @nomfichier +' WITH (FIELDTERMINATOR='';'',FIRSTROW=2,CODEPAGE=''ACP'')'
PRINT @nomfichier
EXECUTE( @nomfichier)

PRINT 'FIN'
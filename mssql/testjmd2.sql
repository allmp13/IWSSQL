DECLARE @Racine_Matrice varchar(255)
DECLARE @nomfichier varchar(255)
DECLARE @metier varchar(255)
DECLARE @environnement varchar(255)
DECLARE @sql varchar(8000)

SET @environnement='TEST'
SET @Racine_Matrice='C:\GENHABLGME\'
SET @metier = 'LGME - EQUIPE COMPETENTE GSIC'
/*
DECLARE @sql varchar(8000)
select @sql = 'copy c:\temp\HC_SANS_GEST.csv c:\temp\HABILITATIONS_COMPLEMENTAIRES_METIER_SANS_GEST.csv'
exec master..xp_cmdshell @sql*/

    SET @metier = '''' + @Racine_Matrice + @environnement + '\' + @metier + '_' + @environnement + '.xlsx' + ''''
    PRINT @metier
    set @sql = 'bcp GENHABLGME.dbo.HABILITATIONS_COMPLEMENTAIRES out ' + @Racine_Matrice + @environnement + '\' + 'HABILITATIONS_COMPLEMENTAIRES_METIER_'+ @environnement +'.csv -c -C 1252 -t";"  -T -S'+ @@servername
    PRINT @sql

exec master..xp_cmdshell 'dir \\srvetl-m\P11_RESSOURCES_HUMAINES'
exec master..xp_cmdshell 'set'
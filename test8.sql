DECLARE @sql varchar(8000)
/*select @sql = 'copy c:\temp\TEST_HABILITATIONS_COMPLEMENTAIRES.csv + c:\temp\HC_SANS_GEST.csv c:\temp\HABILITATIONS_COMPLEMENTAIRES_SANS_GEST.csv'*/
select @sql = 'copy c:\temp\LGME.sql \\SRVLGME-T\D$\HABILITATIONS_COMPLEMENTAIRES_METIER_SANS_GEST.csv'
exec master..xp_cmdshell @sql

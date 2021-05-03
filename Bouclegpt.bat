@Echo OFF
SET FICHE_CLOUD=\\b13u200\lgme

Echo Sauvegarde Base de Reprise
sqlcmd -S srvmssql-f -U srvr_admin -P SQLServer2005 -Q "BACKUP DATABASE IWSTEST TO  DISK = N'E:\BACKUP\IWS_TEST\FULL\BDDPARAM\IWSREPRISE.BAK' WITH NOFORMAT, INIT,  NAME = N'IWPROD-BAKCOMPLET', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"
for /f %%i In ('dir /b /ad "%FICHE_CLOUD%\IWSG*"') DO ( 
    IF EXIST %FICHE_CLOUD%\%%i\import.txt (
        Echo Restauration Base %%i
        SQLCMD -S SRVMSSQL-F -U srvr_admin -P SQLServer2005 -Q "USE master ALTER DATABASE %%i SET SINGLE_USER WITH ROLLBACK IMMEDIATE RESTORE DATABASE %%i FROM DISK='E:\BACKUP\IWS_TEST\FULL\BDDPARAM\IWSREPRISE.BAK' WITH REPLACE"
        SQLCMD -S SRVMSSQL-F -d %%i -U srvr_admin -P SQLServer2005 -Q "ALTER USER suiteisilog WITH LOGIN = suiteisilog"
        ECHO O | DEL %FICHE_CLOUD%\%%i\import.txt
    )
)
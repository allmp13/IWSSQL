@ECHO OFF
CLS
ECHO ******************************************************
ECHO ***           Reprise LGME PHASE 2 V3              ***
ECHO ***               JMD le 20/01/2020                ***
ECHO ***               JMD le 10/02/2020                ***
ECHO ***               JMD le 11/03/2022                ***
ECHO ******************************************************

SET STARTTIME=%TIME%

SET IWS_USER=DEMO
SET IWS_PWD=
SET IWSCN=IWSREPRISE

SET REP_IWSCOMMONFILES= CustomExtensions CustomScripts Models ReportMRS

SET PATH_IWS=D:\Applications\Isilog\Suite Isilog\
SET PATH_IWS_WS=D:\Applications\Isilog\IsilogWebSystem\
SET FICHE_SOURCE=D:\ISILOG\Passerelle RH\
SET PATH_SCRIPT=%FICHE_SOURCE%Scripts\
SET PATH_GB=%PATH_IWS%GestionBase\
SET PATH_ISINT=%PATH_IWS%Programmes\
SET FICHE_CLOUD=C:\Users\svclgme\CloudDrive\Partages_avec_Moi
SET PATH_REPR=D:\ISILOG\REPRISE\
SET PATH_X1=%PATH_IWS_WS%IWSCommonFiles\%IWSCN%\CustomScripts\X1\
SET SORTIECONFIG="%PATH_REPR%FINALISATION_REPRISE\CONFIGURATIONS_TYPES.csv"
SET SORTIEOBJPEREFILS="%PATH_REPR%FINALISATION_REPRISE\OBJETPEREFILS.csv"

SET FICHE_SOURCE="%FICHE_SOURCE%"
SET $madate=%date:~-4%_%date:~3,2%_%date:~0,2%_%time:~-0,2%_%time:~3,2%_%time:~6,2%
SET DESTINATAIRESMAIL=LGME-RefApplicatifMetier@sdis13.fr
SET DESTINATAIRESMAIL=jmdavid@sdis13.fr

SET SGBDPROD=winlgme-bdd-p
SET LOGINPROD=IWSAdmin
SET PWDPROD=2Fois4Egal8
SET PATH_BDDPROD_LOC=\\%SGBDPROD%\IWS_PROD

SET SGBDTEST=winlgme-bdd-t
SET LOGINTEST=IWSAdmin
SET PWDTEST=2Fois4Egal8
SET PATH_BDDTEST_LOC=\\%SGBDTEST%\IWS_TEST

SET MESSAGEMAIL=REPRISE DE DONNEES LGME PHASE 2 INFINITY BASE %IWSCN%

SET LOGFILE=%PATH_REPR%LOG\reprise.log

FOR %%r in (%REP_IWSCOMMONFILES%) DO (
XCOPY "\\winlgme-p\d$\Applications\Isilog\IsilogWebSystem\IWSCommonFiles\IWSPROD\%%r" "%PATH_IWS_WS%IWSCommonFiles\%IWSCN%\%%r" /E/S/I/Y/C
REM DIR "\\winlgme-p\d$\Applications\Isilog\IsilogWebSystem\IWSCommonFiles\IWSPROD\%%r"
 )


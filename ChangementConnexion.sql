--------------------------------------------------------------------------------------------------------------
-- APPLICATION  : IWS
-- PROPRIETAIRE : ISILOG
-- AUTEUR       : CDOR
-- VERSION      : 1.0
--------------------------------------------------------------------------------------------------------------
---Modification à apporter avant de lancer le script
--[USER_SQL]		: nom de l'utilisateur de la base IWS (Exemple : suiteisilog)
--[PWD_USER_SQL]	: mot de passe de l'utilisateur de la base IWS (Exemple : isilog)
--[BDD_IWS]			: nom de la base IWS
--[SRV_BDD]			: serveur BDD
--[VUE_SQL]			: vue SQL
--------------------------------------------------------------------------------------------------------------
--------------------------------  FICHIER  -----------  VERSION  ---------------------------------------------

BEGIN exec dbo.IsiScriptCompleted N'ChangementConnexion.sql',N'290' End 

GO
----------------------------------------------------------------------------------------------------------------
--Chanement des types de reprise csv vers Microsoft ADO
----------------------------------------------------------------------------------------------------------------
UPDATE EXTTABLE
SET    T_FICDELIMIT = NULL,
       VA_CARSEPAR = NULL,
       VA_CARDELIMIT = NULL,
       NO_LIGCHAMP = NULL,
       NO_LIGPREMIERE = NULL,
       T_ORIGFIC = NULL,
       T_NATEXTTABLE = 4 
WHERE C_MODELE = 'HABILITATIONS_COMPLEMENTAIRES' -- Code des modèles d'import concernés

GO
--------------------------------------------------------------------------------------------------------------
--Modification des chaines ADO avec informations de l'environnement sur lequel la passerelle est mise en place
--------------------------------------------------------------------------------------------------------------
UPDATE EXTTABLE
   SET N_EXTTABLE = 'Provider=SQLOLEDB.1;USER ID=srvr_admin ;Password=SQLServer2005;DATA SOURCE=SRVMSSQL-F;INITIAL CATALOG = GENHABLGME;TABLE=HABILITATIONS_COMPLEMENTAIRES_EXPORT'
   FROM EXTTABLE
 WHERE C_MODELE ='HABILITATIONS_COMPLEMENTAIRES' -- Code du modèle d'import
GO



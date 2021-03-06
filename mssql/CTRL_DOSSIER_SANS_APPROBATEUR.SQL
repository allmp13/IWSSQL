SELECT  [NO_APPEL],
 IDT_APPEL,DE_SYMPAPPEL,C_UTIL_DEM,A.C_SERVICE_DEM,A.C_OBJETSERVICE, O.C_PROCED,A.C_STAPPEL
  FROM [IWSPROD].[suiteisilog].[APPEL] A, [IWSPROD].[suiteisilog].[OBJET] O
  where  left(C_SERVICE_DEM,10) not in (select C_SERVICE from IWSPROD.suiteisilog.UTILISATEUR where C_FONCTION in ('CDC','CDCSP','CDS','CDST','CDSPREV')AND len(C_SERVICE)=10 )
  AND A.C_STAPPEL not in( 'C','CT')
  AND O.C_OBJET=A.C_OBJETSERVICE 
  AND O.C_PROCED in('7','8','9')

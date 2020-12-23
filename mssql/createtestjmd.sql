DECLARE @sql varchar(8000)
select @sql = 'bcp "select [Libellé habilitation type],[Code profil],[Code fonction],[Libellé fonction],[Code grade],[Libellé grade],[Code statut professionnel],[Libellé statut professionnel],[Code Statut d''activité],[Libellé statut d''activité],[Code Equipe],[Libellé Equipe],[Rôle dans l''équipe],[Code Nature],[Lecture seule] from GENHABLGME.dbo.HABILITATIONS_TYPE GROUP BY [Libellé habilitation type],[Code profil],[Code fonction],[Libellé fonction],[Code grade],[Libellé grade],[Code statut professionnel],[Libellé statut professionnel],[Code Statut d''activité],[Libellé statut d''activité],[Code Equipe],[Libellé Equipe],[Rôle dans l''équipe],[Code Nature],[Lecture seule]" queryout c:\temp\HT_AVEC_GEST.csv -c -C 1252 -t";"  -T -S'+ @@servername
print @sql
exec master..xp_cmdshell @sql



SELECT
        [Libellé habilitation type],
        [Code profil],
        [Code fonction],
        [Libellé fonction],
        [Code Site]
    FROM
        [dbo].[HABILITATIONS_TYPE]
    GROUP BY [Libellé habilitation type],[Code profil],[Code fonction],[Libellé fonction],[Code Site]

use GENHABLGME
drop table if EXISTS #metier
 drop table if EXISTS #tmp
DELETE FROM HABILITATION;

Create table #metier( 
 metier varchar(50)
 );

 insert into #metier (metier) values ('LGME - EQUIPE COMPETENTE GSIC.xlsx')
 insert into #metier (metier) values ('LGME - EQUIPE COMPETENTE GPAT.xlsx')
 insert into #metier (metier) values ('LGME - EQUIPE COMPETENTE GTL.xlsx')
 insert into #metier (metier) values ('LGME - EQUIPE COMPETENTE BIO.xlsx')
 insert into #metier (metier) values ('DONNEES V2.xlsx')

declare @metier varchar(50)
declare moncurseur cursor for select metier from #metier

open moncurseur

fetch moncurseur into @metier

while @@FETCH_STATUS = 0
begin
set @metier = '''C:\temp\' + @metier +''''
print @metier

EXEC sp_dropserver
   @server = N'TESTJMD',
   @droplogins ='droplogins';

declare @query NVARCHAR(MAX) =  
    'sp_addlinkedserver
     @server = N''TESTJMD'',
     @srvproduct = N''jm'',
     @provider = N''Microsoft.ACE.OLEDB.12.0'',
     @datasrc = N' + @metier + ',
     @provstr = N''Excel 12.0'';'
EXECUTE(@query)

EXEC sp_addlinkedsrvlogin
   @rmtsrvname = N'TESTJMD',
   @useself ='false', 
   @locallogin = NULL,
   @rmtuser = NULL,
   @rmtpassword = NULL;



fetch moncurseur into @metier

   /*select * from testjmd...matrice$*/

 Create table #tmp
 (TABLE_QUALIFIER varchar(40),
  TABLE_OWNER varchar(20),
  TABLE_NAME varchar(40),
  COLUMN_NAME varchar(40),
  DATA_TYPE int,
  TYPE_NAME varchar(20),
  PREC int, LENGTH int,
  SCALE int, RADIX int,
  NULLABLE char(4),
  REMARKS varchar(128),
  COLUMN_DEF varchar(40),
  SQL_DATA_TYPE int,
  SQL_DATETIME_SUB int,
  CHAR_OCTET_LENGTH int,
  ORDINAL_POSITION int,
  IS_NULLABLE char(4),
  SS_DATA_TYPE int)

insert #tmp
EXEC sp_columns_ex 'TESTJMD','Matrice$'

DECLARE @cols AS NVARCHAR(MAX);

select @cols = STUFF((SELECT distinct  ',' +
                        QUOTENAME(column_name)
                      FROM #tmp
                      WHERE 
                        COLUMN_NAME <> 'initiale' 
                        AND COLUMN_NAME <> 'matricule'
						AND COLUMN_NAME <> 'nom'
						AND COLUMN_NAME <> 'prenom'
                      FOR XML PATH(''), TYPE
                     ).value('.', 'NVARCHAR(MAX)') 
                        , 1, 1, '')

SELECT @cols = SUBSTRING(@cols, 0, LEN(@cols)) 
 
declare @query2 NVARCHAR(MAX) =  
'SELECT matricule as Ligne,hab as Colonne,Valeur
FROM TESTJMD...matrice$
UNPIVOT(Valeur
      FOR hab IN (' + @cols + '])) As toto '

insert dbo.HABILITATION
EXECUTE(@query2) 
Drop table #tmp



end
close moncurseur
deallocate moncurseur
Drop table #metier


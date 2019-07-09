use GENHABLGME

DELETE FROM HABILITATION;

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
                      FOR XML PATH(''), TYPE
                     ).value('.', 'NVARCHAR(MAX)') 
                        , 1, 1, '')

SELECT @cols = SUBSTRING(@cols, 0, LEN(@cols)) 
 
declare @query NVARCHAR(MAX) =  
'SELECT matricule as Ligne,hab as Colonne,Valeur
FROM TESTJMD...matrice$
UNPIVOT(Valeur
      FOR hab IN (' + @cols + '])) As toto '

/*EXECUTE(@query) WITH RESULT SETS 
((
Ligne VARCHAR(10),
Colonne VARCHAR(50),
Valeur VARCHAR(10)
))*/

insert dbo.HABILITATION
EXECUTE(@query) 

Drop table #tmp


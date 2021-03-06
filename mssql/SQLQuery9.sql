USE [TEST]
GO
/****** Object:  StoredProcedure [dbo].[JMD]    Script Date: 05/07/2019 23:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[JMD]
AS   

DECLARE @cols AS NVARCHAR(MAX);

select @cols = STUFF((SELECT distinct  ',' +
                        QUOTENAME(column_name)
                      FROM information_schema.columns
                      WHERE table_name = 'Matrice'
                        AND COLUMN_NAME <> 'initiale' 
                        AND COLUMN_NAME <> 'matricule'
						AND COLUMN_NAME <> 'nom'
                      FOR XML PATH(''), TYPE
                     ).value('.', 'NVARCHAR(MAX)') 
                        , 1, 1, '')

SELECT @cols = SUBSTRING(@cols, 0, LEN(@cols)) 
 
declare @query NVARCHAR(MAX) =  
'SELECT matricule as Ligne,hab as Colonne,Valeur
FROM matrice
UNPIVOT(Valeur
      FOR hab IN (' + @cols + '])) As toto '

EXECUTE(@query) WITH RESULT SETS 
((
Ligne VARCHAR(10),
Colonne VARCHAR(50),
Valeur VARCHAR(10)
))

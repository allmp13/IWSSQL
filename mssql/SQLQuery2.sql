DECLARE @cols AS NVARCHAR(MAX);

select @cols = STUFF((SELECT distinct ',' +
                        QUOTENAME(column_name)
                      FROM information_schema.columns
                      WHERE table_name = 'Matrice'
                        AND COLUMN_NAME <> 'initiale' 
                        AND COLUMN_NAME <> 'matricule'
						AND COLUMN_NAME <> 'nom'
                      FOR XML PATH(''), TYPE
                     ).value('.', 'NVARCHAR(MAX)') 
                        , 1, 1, '');
select @cols as hab
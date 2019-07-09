SELECT *
FROM 
    OPENROWSET(
    'SQLNCLI', 
    'Server=.;Trusted_Connection=yes;',
    'EXEC test.dbo.jmd')
	where Ligne = '7710'
select * from OBJPEREFILS where C_OBJET_PERE='1663658'
select C_OBJET,N_OBJET from OBJET  where N_OBJET like 'B13U200'
select O.NO_SERIE,O.C_OBJET from OBJET O INNER JOIN OBJPEREFILS OPF ON O.C_OBJET=OPF.C_OBJET where OPF.C_OBJET_PERE='1663658' order by O.NO_SERIE

SELECT O.N_OBJET,O.NB_PARTAGEFILSMAX, O.NB_PARTAGEMAX FROM OBJET O WHERE C_OBJET=1664014
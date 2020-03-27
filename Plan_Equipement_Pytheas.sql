Select D.otmodel,DP.otProperty2 from otobject D,Otdepartment DP where D.oidobjecttype='302' and D.oidObject=DP.oidobject

SELECT
    ob.oidobject,
    ob.otmodel ,
    otT.onlValue,
    otT.otProperty1,
    otT.otProperty2,
    otT.otType,
    0 AS value
FROM
    ovp21 ob,
    otTypology ott
WHERE ott.oidObject=ob.oidObject AND otType='Plan equipement'
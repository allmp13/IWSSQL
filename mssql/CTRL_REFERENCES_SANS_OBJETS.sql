SELECT
    C.C_REF,
    C.L_REF
FROM
    CATALOGUE C
WHERE C.C_NATUREOBJ='INF' AND C.C_NATURE='REF' AND C.C_REF NOT IN(

SELECT
        O.C_REF
    FROM
        OBJET O
    WHERE O.C_NATURE='INF'
    GROUP BY O.C_REF)


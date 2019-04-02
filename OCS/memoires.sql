SELECT
    ID,
    HARDWARE_ID,
    concat(count(id),'x',replace(CONVERT(CONVERT(type using binary
) using utf8),'<OUT OF SPEC>','Unknown'),'-',capacity,'Mo ') AS Label, sum
(capacity) AS Total FROM memories WHERE replace
(capacity,'No',0) > 0 GROUP BY hardware_id, type, capacity
SELECT  ID
       ,HARDWARE_ID
       ,LETTER
       ,CONVERT(CONVERT(volumn USING binary ) USING utf8 )     AS VOLUMN
       ,CONVERT (CONVERT (filesystem USING binary) USING utf8) AS FILESYSTEM
       ,TOTAL
       ,FREE
       ,CONVERT (CONVERT (type USING binary) USING utf8)       AS TYPE
FROM drives
WHERE ifnull (type, '') NOT LIKE '%removable%'
AND ifnull (type, '') NOT LIKE '%cd%rom%'
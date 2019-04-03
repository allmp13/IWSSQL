SELECT
    ID,
    HARDWARE_ID,
    LETTER,
    CONVERT(CONVERT(volumn using binary
) using utf8
) AS VOLUMN, CONVERT
(CONVERT
(filesystem using binary) using utf8) AS FILESYSTEM,
 TOTAL, FREE, CONVERT
(CONVERT
(type using binary) using utf8) AS TYPE FROM drives
 WHERE ifnull
(type,'') NOT LIKE '%removable%' AND ifnull
(type,'') NOT LIKE '%cd%rom%'
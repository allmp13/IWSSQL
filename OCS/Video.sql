SELECT
    ID,
    HARDWARE_ID,
    CONVERT(CONVERT(NAME using binary
) using utf8) AS NAME,
    CONVERT
(CONVERT
(CHIPSET using binary) using utf8) AS CHIPSET,
    MEMORY,
    CONVERT
(CONVERT
(RESOLUTION using binary) using utf8) AS RESOLUTION
FROM
    videos
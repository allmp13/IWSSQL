SELECT
    ID,
    status,
    HARDWARE_ID,
    CONVERT(CONVERT(description using binary
) using utf8) AS DESCRIPTION, MACADDR FROM networks
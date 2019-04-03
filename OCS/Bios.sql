SELECT
    type AS TYPEPOSTE,
    HARDWARE_ID,
    CONVERT(CONVERT(smanufacturer using binary
) using utf8) AS SMANUFACTURER, CONVERT
(CONVERT
(smodel using binary) using utf8) AS SMODEL, CONVERT
(CONVERT
(ssn using binary) using utf8) AS SSN, BDATE, CONVERT
(CONVERT
(bmanufacturer using binary) using utf8) AS BMANUFACTURER, CONVERT
(CONVERT
(bversion using binary) using utf8) AS BVERSION FROM bios
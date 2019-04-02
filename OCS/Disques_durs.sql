SELECT
    ID,
    HARDWARE_ID,
    CONVERT(CONVERT(manufacturer using binary
) using utf8) 
AS MANUFACTURER, CONVERT
(CONVERT
(model using binary) using utf8) AS MODEL, convert
(CONVERT
(name using binary) using utf8) AS NAME, DISKSIZE FROM storages WHERE 
ifnull
(disksize,0) > 0 AND concat_ws
('',manufacturer,name,description,type) NOT 
LIKE '%floppy%' AND concat_ws
('',manufacturer,name,description,type) NOT LIKE '%
disquette%' AND concat_ws
('',manufacturer,name,description,type) NOT LIKE '%cd%r
om%' AND concat_ws
('',manufacturer,name,description,type) NOT LIKE '%removable%'
 AND concat_ws
('',manufacturer,name,description,type) NOT LIKE '%usb%' AND concat_ws
('',manufacturer,name,description,type) NOT LIKE '%floppy%' AND concat_ws
(''
,manufacturer,name,description,type) NOT LIKE '%tape%'
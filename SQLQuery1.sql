DECLARE @datedebut DATETIME
DECLARE @datefin DATETIME
DECLARE @clientid int

SET @datedebut = '2022-10-5 00:00:00'
SET @datefin = '2022-10-7 00:00:00'
SET @clientid = 1

Select tp.jour,tp.tot,s.libelle from 
(SELECT CONVERT(DATE,e.e_date) as jour,e.service_id as id,SUM(e.poid) as tot 
FROM Etiquette e 
WHERE e.client_id = @clientid AND e.e_date BETWEEN @datedebut AND @datefin 
GROUP BY e.service_id,CONVERT(DATE,e.e_date))  tp 
right join C_Service s on s.id=tp.id;
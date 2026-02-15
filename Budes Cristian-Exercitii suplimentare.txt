
--17
DROP TABLE Salariati_S CASCADE CONSTRAINTS;

CREATE TABLE Salariati_S AS
SELECT 
    ANGAJAT_ID, 
    NUME, 
    PRENUME, 
    FUNCTIE, 
    DATA_ANGAJARII 
FROM Angajati;

--18
ALTER TABLE Salariati_S 
ADD Prima NUMBER(5,2);

--19
UPDATE Salariati_S 
SET Prima = 0.15;

COMMIT;

--20
ALTER TABLE Salariati_S 
ADD CONSTRAINT pk_salariati_s PRIMARY KEY (ANGAJAT_ID);
SELECT * FROM Salariati_S;

--21
ALTER TABLE Salariati_S 
MODIFY Prima CONSTRAINT nn_prima_s NOT NULL;

--22
ALTER TABLE Salariati_S 
DISABLE CONSTRAINT nn_prima_s;

ALTER TABLE Salariati_S 
SET UNUSED (Prima);

--23
ALTER TABLE Salariati_S 
DROP UNUSED COLUMNS;

--24
ALTER TABLE Salariati_S ADD Salariul NUMBER(10, 2);
UPDATE Salariati_S SET Salariul = 3000; -- Punem o valoare fictivă
COMMIT;

UPDATE Salariati_S 
SET Salariul = Salariul * 0.90 
WHERE Data_Angajarii < TO_DATE('01-06-2016', 'DD-MM-YYYY');

COMMIT;
SELECT * FROM Salariati_S;

--25
ALTER TABLE Salariati_S ADD ID_MANAGER NUMBER(6);
ALTER TABLE Salariati_S ADD ID_DEPARTAMENT NUMBER(4);

UPDATE Salariati_S SET ID_MANAGER = 120, ID_DEPARTAMENT = 50 WHERE ANGAJAT_ID = 1;
UPDATE Salariati_S SET ID_MANAGER = 100, ID_DEPARTAMENT = 50 WHERE ANGAJAT_ID = 2;
COMMIT;

UPDATE Salariati_S
SET Salariul = Salariul * 1.15
WHERE ID_MANAGER IN (120, 121, 122, 123, 124, 125)
  AND (FUNCTIE != 'ST_MAN' OR FUNCTIE IS NULL);
  
--26
UPDATE Salariati_S
SET (Salariul, Functie) = (
    SELECT Salariul, Functie 
    FROM Salariati_S 
    WHERE Salariul = (SELECT MAX(Salariul) FROM Salariati_S)
    AND ROWNUM = 1 -- Luăm doar unul în caz de egalitate
)
WHERE ID_DEPARTAMENT = 50 
  AND ID_MANAGER = 100;
  
--27
DELETE FROM Salariati_S
WHERE (SYSDATE - DATA_ANGAJARII) / 365 > 3;
ROLLBACK;
SELECT * FROM Salariati_S;

--1
DROP TABLE Ang2 CASCADE CONSTRAINTS;
DROP TABLE Ang CASCADE CONSTRAINTS;
DROP TABLE Dep CASCADE CONSTRAINTS;
DROP TABLE Salariati CASCADE CONSTRAINTS;

CREATE TABLE Dep (
    ID NUMBER(7) PRIMARY KEY,
    Denumire VARCHAR2(25)
);

--2
DESC Departamente

INSERT INTO Dep (ID, Denumire)
SELECT ID_DEPARTAMENT, DENUMIRE_DEPARTAMENT
FROM Departamente;

SELECT * FROM Dep;

--3
CREATE TABLE Ang (
    ID NUMBER(7) PRIMARY KEY,
    Prenume VARCHAR2(25),
    Nume VARCHAR2(25),
    Dep_ID NUMBER(7),
    CONSTRAINT fk_ang_dep FOREIGN KEY (Dep_ID) REFERENCES Dep(ID)
);

--4
ALTER TABLE Ang 
ADD Varsta NUMBER(2);

--5
ALTER TABLE Ang 
ADD CONSTRAINT Verifica_varsta CHECK (Varsta >= 18 AND Varsta <= 65);

--6
ALTER TABLE Ang 
DISABLE CONSTRAINT Verifica_varsta;

--7
ALTER TABLE Ang 
MODIFY Nume VARCHAR2(30);

--8
ALTER TABLE Ang 
RENAME TO Ang2;

--9
CREATE TABLE Salariati AS 
SELECT * FROM Angajati;

--10
DESC Salariati;

INSERT INTO Salariati (ANGAJAT_ID, PRENUME, NUME, FUNCTIE, DATA_ANGAJARII) 
VALUES (1, 'Steven', 'Kong', 'AD_PRES', TO_DATE('17-06-1987', 'DD-MM-YYYY'));

INSERT INTO Salariati (ANGAJAT_ID, PRENUME, NUME, FUNCTIE, DATA_ANGAJARII) 
VALUES (2, 'Neena', 'Koch', 'AD_VP', TO_DATE('21-09-1989', 'DD-MM-YYYY'));

INSERT INTO Salariati (ANGAJAT_ID, PRENUME, NUME, FUNCTIE, DATA_ANGAJARII) 
VALUES (3, 'Lex', 'Haan', 'AD_VP', TO_DATE('13-01-1993', 'DD-MM-YYYY'));

COMMIT;

SELECT * FROM Salariati WHERE ANGAJAT_ID IN (1, 2, 3);

--11
UPDATE Salariati 
SET Prenume = 'John' 
WHERE Angajat_ID = 3;


ALTER TABLE Salariati ADD Email VARCHAR2(25);
ALTER TABLE Salariati ADD Salariul NUMBER(8,2);
UPDATE Salariati SET Salariul = 17000;
SELECT * FROM Salariati;

--12
UPDATE Salariati 
SET Email = 'JHAAN' 
WHERE Angajat_ID = 3;

--13
UPDATE Salariati 
SET Salariul = Salariul * 1.1 
WHERE Salariul < 20000;

ALTER TABLE Salariati ADD Comision NUMBER(2,2);
UPDATE Salariati SET Comision = 0.20 WHERE Angajat_ID = 3;

--14
UPDATE Salariati 
SET Functie = 'AD_PRES' 
WHERE Angajat_ID = 2;

--15
UPDATE Salariati 
SET Comision = (SELECT Comision FROM Salariati WHERE Angajat_ID = 3) 
WHERE Angajat_ID = 2;

--16
DELETE FROM Salariati 
WHERE Angajat_ID = 1;

SELECT * FROM Salariati;

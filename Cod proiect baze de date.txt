DROP TABLE DETALII_FACTURA CASCADE CONSTRAINTS;
DROP TABLE FACTURI CASCADE CONSTRAINTS;
DROP TABLE PRODUSE CASCADE CONSTRAINTS;
DROP TABLE ANGAJATI CASCADE CONSTRAINTS;
DROP TABLE CLIENTI CASCADE CONSTRAINTS;
DROP TABLE FURNIZORI CASCADE CONSTRAINTS;
DROP TABLE CATEGORII_PRODUSE CASCADE CONSTRAINTS;

CREATE TABLE CATEGORII_PRODUSE (
    categorie_id    NUMBER(5)    CONSTRAINT PK_CATEGORII_PRODUSE PRIMARY KEY,
    denumire_categorie VARCHAR2(50)    CONSTRAINT NN_DENUMIRE_CATEG NOT NULL,
    parent_id      NUMBER(5)    NULL
);

CREATE TABLE FURNIZORI (
    furnizor_id   NUMBER(5)    CONSTRAINT PK_FURNIZORI PRIMARY KEY,
    nume_furnizor VARCHAR2(100) CONSTRAINT NN_NUME_FURN NOT NULL,
    cui           VARCHAR2(20)  CONSTRAINT NN_CUI_FURN NOT NULL,
    CONSTRAINT UQ_CUI_FURNIZOR UNIQUE(cui)   
);

CREATE TABLE PRODUSE (
    produs_id    NUMBER(5)    CONSTRAINT PK_PRODUSE PRIMARY KEY,
    nume_produs  VARCHAR2(100) CONSTRAINT NN_NUME_PROD NOT NULL,
    pret_lista   NUMBER(10,2)  CONSTRAINT NN_PRET_LISTA NOT NULL,
    categorie_id NUMBER(5)   
           CONSTRAINT NN_CAT_PROD NOT NULL,
    furnizor_id  NUMBER(5)    
           CONSTRAINT NN_FURN_PROD NOT NULL,
    CONSTRAINT CHK_PRET_LISTA_POS CHECK (pret_lista > 0)
);

CREATE TABLE CLIENTI (
    client_id    NUMBER(5)    CONSTRAINT PK_CLIENTI PRIMARY KEY,
    nume_client  VARCHAR2(100) CONSTRAINT NN_NUME_CLIENT NOT NULL,
    cif          VARCHAR2(20),
    adresa       VARCHAR2(100) CONSTRAINT NN_ADRESA_CLI NOT NULL,
    CONSTRAINT UQ_CIF_CLIENT UNIQUE(cif)  
);

CREATE TABLE ANGAJATI (
    angajat_id  NUMBER(5)    CONSTRAINT PK_ANGAJATI PRIMARY KEY,
    nume        VARCHAR2(50)  CONSTRAINT NN_NUME_ANG NOT NULL,
    prenume     VARCHAR2(50)  CONSTRAINT NN_PRENUME_ANG NOT NULL,
    functie     VARCHAR2(50)
);

CREATE TABLE FACTURI (
    factura_id   NUMBER(8)   CONSTRAINT PK_FACTURI PRIMARY KEY,
    client_id    NUMBER(5)   CONSTRAINT NN_CLI_FACT NOT NULL,
    angajat_id   NUMBER(5)   CONSTRAINT NN_ANG_FACT NOT NULL,
    data_facturii DATE       CONSTRAINT NN_DATA_FACT NOT NULL,
    total_factura NUMBER(12,2) CONSTRAINT NN_TOTAL_FACT NOT NULL,
    CONSTRAINT CHK_TOTAL_FACT_POS CHECK (total_factura >= 0)
);

CREATE TABLE DETALII_FACTURA (
    factura_id   NUMBER(8)   NOT NULL,
    produs_id    NUMBER(5)   NOT NULL,
    cantitate    NUMBER(10)  CONSTRAINT NN_CANT NOT NULL,
    pret_vanzare NUMBER(10,2) CONSTRAINT NN_PRET_VANZ NOT NULL,
    CONSTRAINT CHK_CANT_POS CHECK (cantitate > 0),
    CONSTRAINT CHK_PRET_VANZ_POS CHECK (pret_vanzare >= 0),
    CONSTRAINT PK_DETALII_FACTURA PRIMARY KEY(factura_id, produs_id)
);


DELETE FROM DETALII_FACTURA;
DELETE FROM FACTURI;
DELETE FROM PRODUSE;
DELETE FROM CATEGORII_PRODUSE;
DELETE FROM CLIENTI;
DELETE FROM ANGAJATI;
DELETE FROM FURNIZORI;
COMMIT;

INSERT ALL
  INTO CATEGORII_PRODUSE (categorie_id, denumire_categorie, parent_id) VALUES (1,  'Alimente',     NULL)
  INTO CATEGORII_PRODUSE (categorie_id, denumire_categorie, parent_id) VALUES (2,  'Lactate',      1)
  INTO CATEGORII_PRODUSE (categorie_id, denumire_categorie, parent_id) VALUES (3,  'Panificatie',  1)
  INTO CATEGORII_PRODUSE (categorie_id, denumire_categorie, parent_id) VALUES (4,  'Bauturi',      NULL)
  INTO CATEGORII_PRODUSE (categorie_id, denumire_categorie, parent_id) VALUES (5,  'Branzeturi',   2)
  INTO CATEGORII_PRODUSE (categorie_id, denumire_categorie, parent_id) VALUES (6,  'Iaurturi',     2)
  INTO CATEGORII_PRODUSE (categorie_id, denumire_categorie, parent_id) VALUES (7,  'Mezeluri',     1)
  INTO CATEGORII_PRODUSE (categorie_id, denumire_categorie, parent_id) VALUES (8,  'Dulciuri',     1)
  INTO CATEGORII_PRODUSE (categorie_id, denumire_categorie, parent_id) VALUES (9,  'Cafea',        4)
  INTO CATEGORII_PRODUSE (categorie_id, denumire_categorie, parent_id) VALUES (10, 'Apa',          4)
SELECT 1 FROM dual;


INSERT ALL
  INTO FURNIZORI (furnizor_id, nume_furnizor, cui) VALUES (1,  'Lactate SA',           'RO123456')
  INTO FURNIZORI (furnizor_id, nume_furnizor, cui) VALUES (2,  'Bauturi SRL',          'RO234567')
  INTO FURNIZORI (furnizor_id, nume_furnizor, cui) VALUES (3,  'Panifex SRL',          'RO345678')
  INTO FURNIZORI (furnizor_id, nume_furnizor, cui) VALUES (4,  'Delicatese SA',        'RO456789')
  INTO FURNIZORI (furnizor_id, nume_furnizor, cui) VALUES (5,  'Cafea Import SRL',     'RO567890')
  INTO FURNIZORI (furnizor_id, nume_furnizor, cui) VALUES (6,  'Apa Izvor SA',         'RO678901')
  INTO FURNIZORI (furnizor_id, nume_furnizor, cui) VALUES (7,  'Mezeluri Pro SRL',     'RO789012')
  INTO FURNIZORI (furnizor_id, nume_furnizor, cui) VALUES (8,  'Dulciuri Plus SRL',    'RO890123')
  INTO FURNIZORI (furnizor_id, nume_furnizor, cui) VALUES (9,  'Ferma Bunicii SRL',    'RO901234')
  INTO FURNIZORI (furnizor_id, nume_furnizor, cui) VALUES (10, 'Premium Drinks SRL',   'RO012345')
SELECT 1 FROM dual;


INSERT ALL
  INTO PRODUSE (produs_id, nume_produs, pret_lista, categorie_id, furnizor_id) VALUES (1,  'Lapte UHT 1L',          5.00,  2,  1)
  INTO PRODUSE (produs_id, nume_produs, pret_lista, categorie_id, furnizor_id) VALUES (2,  'Telemea vaca 1kg',      20.00, 5,  1)
  INTO PRODUSE (produs_id, nume_produs, pret_lista, categorie_id, furnizor_id) VALUES (3,  'Iaurt natural 150g',     3.50, 6,  9)
  INTO PRODUSE (produs_id, nume_produs, pret_lista, categorie_id, furnizor_id) VALUES (4,  'Paine graham 500g',      2.00, 3,  3)
  INTO PRODUSE (produs_id, nume_produs, pret_lista, categorie_id, furnizor_id) VALUES (5,  'Cozonac 500g',           18.00,8,  3)
  INTO PRODUSE (produs_id, nume_produs, pret_lista, categorie_id, furnizor_id) VALUES (6,  'Bere blonda 500ml',      7.00, 4, 10)
  INTO PRODUSE (produs_id, nume_produs, pret_lista, categorie_id, furnizor_id) VALUES (7,  'Vin rosu Merlot 750ml',  55.00,4,  2)
  INTO PRODUSE (produs_id, nume_produs, pret_lista, categorie_id, furnizor_id) VALUES (8,  'Cafea boabe 1kg',        75.00,9,  5)
  INTO PRODUSE (produs_id, nume_produs, pret_lista, categorie_id, furnizor_id) VALUES (9,  'Apa plata 2L',           4.00,10, 6)
  INTO PRODUSE (produs_id, nume_produs, pret_lista, categorie_id, furnizor_id) VALUES (10, 'Salam sasesc 400g',      18.00,7,  7)
SELECT 1 FROM dual;


INSERT ALL
  INTO CLIENTI (client_id, nume_client, cif, adresa) VALUES (1,  'SC Alba SRL',          'RO111111', 'Bucuresti, Str. Unirii 10')
  INTO CLIENTI (client_id, nume_client, cif, adresa) VALUES (2,  'MegaMarket SRL',       'RO222222', 'Cluj-Napoca, Str. Memorandumului 45')
  INTO CLIENTI (client_id, nume_client, cif, adresa) VALUES (3,  'Budeș Cristian',       NULL,       'Constanta, Bd. Mamaia 20') 
  INTO CLIENTI (client_id, nume_client, cif, adresa) VALUES (4,  'Distrib Vest SRL',     'RO333333', 'Timisoara, Str. Republicii 7')
  INTO CLIENTI (client_id, nume_client, cif, adresa) VALUES (5,  'Retail City SRL',      'RO444444', 'Iasi, Str. Palat 1')
  INTO CLIENTI (client_id, nume_client, cif, adresa) VALUES (6,  'Market Sud SRL',       'RO555555', 'Craiova, Bd. Olteniei 12')
  INTO CLIENTI (client_id, nume_client, cif, adresa) VALUES (7,  'Shop Express SRL',     'RO666666', 'Brasov, Str. Muresenilor 18')
  INTO CLIENTI (client_id, nume_client, cif, adresa) VALUES (8,  'HoReCa Group SRL',     'RO777777', 'Bucuresti, Calea Victoriei 100')
  INTO CLIENTI (client_id, nume_client, cif, adresa) VALUES (9,  'Minimarket Nord SRL',  'RO888888', 'Sibiu, Str. Cetatii 3')
  INTO CLIENTI (client_id, nume_client, cif, adresa) VALUES (10, 'Family Store SRL',     'RO999999', 'Oradea, Str. Independentei 9')
SELECT 1 FROM dual;


INSERT ALL
  INTO ANGAJATI (angajat_id, nume, prenume, functie) VALUES (1,  'Ionescu',   'Andrei', 'Agent Vanzari')
  INTO ANGAJATI (angajat_id, nume, prenume, functie) VALUES (2,  'Popescu',   'Maria',  NULL)
  INTO ANGAJATI (angajat_id, nume, prenume, functie) VALUES (3,  'Dumitru',   'Alex',   'Agent Vanzari')
  INTO ANGAJATI (angajat_id, nume, prenume, functie) VALUES (4,  'Stan',      'Ioana',  'Manager Vanzari')
  INTO ANGAJATI (angajat_id, nume, prenume, functie) VALUES (5,  'Radu',      'Mihai',  'Agent Vanzari')
  INTO ANGAJATI (angajat_id, nume, prenume, functie) VALUES (6,  'Georgescu', 'Elena',  'BackOffice')
  INTO ANGAJATI (angajat_id, nume, prenume, functie) VALUES (7,  'Marin',     'Vlad',   NULL)
  INTO ANGAJATI (angajat_id, nume, prenume, functie) VALUES (8,  'Petrescu',  'Ana',    'Agent Vanzari')
  INTO ANGAJATI (angajat_id, nume, prenume, functie) VALUES (9,  'Ilie',      'Cosmin', 'Agent Vanzari')
  INTO ANGAJATI (angajat_id, nume, prenume, functie) VALUES (10, 'Nistor',    'Daria',  'Agent Vanzari')
SELECT 1 FROM dual;


INSERT ALL
  INTO FACTURI (factura_id, client_id, angajat_id, data_facturii, total_factura) VALUES (100, 1, 1,  DATE '2025-01-10',  70.00)
  INTO FACTURI (factura_id, client_id, angajat_id, data_facturii, total_factura) VALUES (101, 2, 2,  DATE '2025-02-10', 110.00)
  INTO FACTURI (factura_id, client_id, angajat_id, data_facturii, total_factura) VALUES (102, 1, 2,  DATE '2025-03-20', 100.00)
  INTO FACTURI (factura_id, client_id, angajat_id, data_facturii, total_factura) VALUES (103, 4, 3,  DATE '2025-04-05',  55.00)
  INTO FACTURI (factura_id, client_id, angajat_id, data_facturii, total_factura) VALUES (104, 5, 5,  DATE '2025-05-18', 150.00)
  INTO FACTURI (factura_id, client_id, angajat_id, data_facturii, total_factura) VALUES (105, 6, 8,  DATE '2025-06-22',  40.00)
  INTO FACTURI (factura_id, client_id, angajat_id, data_facturii, total_factura) VALUES (106, 7, 9,  DATE '2025-07-30',  85.00)
  INTO FACTURI (factura_id, client_id, angajat_id, data_facturii, total_factura) VALUES (107, 8, 4,  DATE '2025-08-12', 200.00)
  INTO FACTURI (factura_id, client_id, angajat_id, data_facturii, total_factura) VALUES (108, 9, 10, DATE '2025-09-02',  64.00)
  INTO FACTURI (factura_id, client_id, angajat_id, data_facturii, total_factura) VALUES (109, 10, 6, DATE '2025-10-14',  92.00)
SELECT 1 FROM dual;


INSERT ALL
  INTO DETALII_FACTURA (factura_id, produs_id, cantitate, pret_vanzare) VALUES (100, 1, 10, 5.00)
  INTO DETALII_FACTURA (factura_id, produs_id, cantitate, pret_vanzare) VALUES (100, 4, 10, 2.00)
  INTO DETALII_FACTURA (factura_id, produs_id, cantitate, pret_vanzare) VALUES (101, 2, 5, 20.00)
  INTO DETALII_FACTURA (factura_id, produs_id, cantitate, pret_vanzare) VALUES (101, 4, 5, 2.00)
  INTO DETALII_FACTURA (factura_id, produs_id, cantitate, pret_vanzare) VALUES (102, 7, 2, 50.00)
  INTO DETALII_FACTURA (factura_id, produs_id, cantitate, pret_vanzare) VALUES (103, 7, 1, 55.00)
  INTO DETALII_FACTURA (factura_id, produs_id, cantitate, pret_vanzare) VALUES (104, 8, 2, 75.00)
  INTO DETALII_FACTURA (factura_id, produs_id, cantitate, pret_vanzare) VALUES (105, 9, 10, 4.00)
  INTO DETALII_FACTURA (factura_id, produs_id, cantitate, pret_vanzare) VALUES (106, 5, 10, 6.50)
  INTO DETALII_FACTURA (factura_id, produs_id, cantitate, pret_vanzare) VALUES (106, 3, 5, 3.50)
  INTO DETALII_FACTURA (factura_id, produs_id, cantitate, pret_vanzare) VALUES (107, 10, 10, 18.00)
  INTO DETALII_FACTURA (factura_id, produs_id, cantitate, pret_vanzare) VALUES (107, 1, 4, 5.00)
  INTO DETALII_FACTURA (factura_id, produs_id, cantitate, pret_vanzare) VALUES (108, 6, 8, 7.00)
  INTO DETALII_FACTURA (factura_id, produs_id, cantitate, pret_vanzare) VALUES (108, 9, 2, 4.00)
  INTO DETALII_FACTURA (factura_id, produs_id, cantitate, pret_vanzare) VALUES (109, 2, 4, 20.00)
  INTO DETALII_FACTURA (factura_id, produs_id, cantitate, pret_vanzare) VALUES (109, 4, 6, 2.00)
SELECT 1 FROM dual;

COMMIT;


SELECT client_id, nume_client, adresa
FROM CLIENTI
WHERE nume_client = 'Budeș Cristian';


SELECT 'CATEGORII_PRODUSE' tabela, COUNT(*) nr FROM CATEGORII_PRODUSE
UNION ALL SELECT 'FURNIZORI', COUNT(*) FROM FURNIZORI
UNION ALL SELECT 'PRODUSE', COUNT(*) FROM PRODUSE
UNION ALL SELECT 'CLIENTI', COUNT(*) FROM CLIENTI
UNION ALL SELECT 'ANGAJATI', COUNT(*) FROM ANGAJATI
UNION ALL SELECT 'FACTURI', COUNT(*) FROM FACTURI
UNION ALL SELECT 'DETALII_FACTURA', COUNT(*) FROM DETALII_FACTURA;

CREATE SEQUENCE SEQ_CLIENTI START WITH 1000 INCREMENT BY 1 NOCACHE NOCYCLE;
CREATE SEQUENCE SEQ_FACTURI START WITH 5000 INCREMENT BY 1 NOCACHE NOCYCLE;

CREATE INDEX IDX_PRODUSE_NUME ON PRODUSE(nume_produs);

CREATE OR REPLACE VIEW V_RAPORT_FACTURI AS
SELECT
  f.factura_id,
  f.data_facturii,
  c.nume_client,
  a.nume || ' ' || a.prenume AS nume_angajat,
  p.nume_produs,
  d.cantitate,
  d.pret_vanzare,
  d.cantitate * d.pret_vanzare AS valoare_linie
FROM FACTURI f
JOIN CLIENTI c ON c.client_id = f.client_id
JOIN ANGAJATI a ON a.angajat_id = f.angajat_id
JOIN DETALII_FACTURA d ON d.factura_id = f.factura_id
JOIN PRODUSE p ON p.produs_id = d.produs_id;

CREATE OR REPLACE SYNONYM SIN_CLIENTI FOR CLIENTI;
CREATE OR REPLACE SYNONYM SIN_RAPORT FOR V_RAPORT_FACTURI;

UPDATE CLIENTI
SET adresa = 'Constanta, Str. Tomis nr. 50'
WHERE nume_client = 'Budeș Cristian';

SELECT client_id, nume_client, adresa
FROM CLIENTI
WHERE nume_client = 'Budeș Cristian';

UPDATE ANGAJATI
SET functie = 'Agent Vanzari'
WHERE functie IS NULL;

SELECT angajat_id,
       nume || ' ' || prenume AS angajat,
       NVL(functie,'Nespecificata') AS functie_afisata
FROM ANGAJATI;

SELECT COUNT(*) AS nr_produse_initial
FROM PRODUSE;

DROP TABLE PRODUSE_BACKUP CASCADE CONSTRAINTS;

CREATE TABLE PRODUSE_BACKUP AS
SELECT * FROM PRODUSE;

SELECT COUNT(*) AS nr_backup
FROM PRODUSE_BACKUP;

DROP TABLE PRODUSE CASCADE CONSTRAINTS;

CREATE TABLE PRODUSE (
    produs_id    NUMBER(5)     CONSTRAINT PK_PRODUSE PRIMARY KEY,
    nume_produs  VARCHAR2(100) CONSTRAINT NN_NUME_PROD NOT NULL,
    pret_lista   NUMBER(10,2)  CONSTRAINT NN_PRET_LISTA NOT NULL,
    categorie_id NUMBER(5)     CONSTRAINT NN_CAT_PROD NOT NULL,
    furnizor_id  NUMBER(5)     CONSTRAINT NN_FURN_PROD NOT NULL,
    CONSTRAINT CHK_PRET_LISTA_POS CHECK (pret_lista > 0)
);

ALTER TABLE PRODUSE
  ADD CONSTRAINT FK_PROD_CATEG FOREIGN KEY (categorie_id)
  REFERENCES CATEGORII_PRODUSE(categorie_id);

ALTER TABLE PRODUSE
  ADD CONSTRAINT FK_PROD_FURN FOREIGN KEY (furnizor_id)
  REFERENCES FURNIZORI(furnizor_id);

INSERT INTO PRODUSE (produs_id, nume_produs, pret_lista, categorie_id, furnizor_id)
SELECT produs_id, nume_produs, pret_lista, categorie_id, furnizor_id
FROM PRODUSE_BACKUP;

SELECT COUNT(*) AS nr_produse_recuperate
FROM PRODUSE;



--Q1
SELECT produs_id, nume_produs, pret_lista
FROM PRODUSE
WHERE pret_lista > 50;

--Q2
SELECT produs_id, nume_produs, pret_lista
FROM PRODUSE
WHERE pret_lista < 5;

---Q3
SELECT produs_id, nume_produs, pret_lista
FROM PRODUSE
WHERE pret_lista >= 20;

--Q4
SELECT produs_id, nume_produs, pret_lista
FROM PRODUSE
WHERE pret_lista <= 10;

--Q5 
SELECT produs_id, nume_produs, pret_lista
FROM PRODUSE
WHERE pret_lista != 20;

--Q6
SELECT angajat_id, nume, prenume, functie
FROM ANGAJATI
WHERE functie IS NULL;

--Q7
SELECT client_id, nume_client
FROM CLIENTI
WHERE nume_client LIKE '%SRL%';

--Q8
SELECT factura_id, client_id, total_factura
FROM FACTURI
WHERE client_id IN (1,2,3);

-- Q9 
SELECT factura_id, data_facturii, total_factura
FROM FACTURI
WHERE data_facturii BETWEEN TO_DATE('2025-01-01','YYYY-MM-DD') AND TO_DATE('2025-12-31','YYYY-MM-DD');

-- Q10 
SELECT f.factura_id, c.nume_client, f.total_factura
FROM FACTURI f
JOIN CLIENTI c ON c.client_id = f.client_id;

-- Q11 
SELECT f.factura_id, c.nume_client, a.nume || ' ' || a.prenume AS angajat, f.total_factura
FROM FACTURI f
JOIN CLIENTI c ON c.client_id = f.client_id
JOIN ANGAJATI a ON a.angajat_id = f.angajat_id;

-- Q12 
SELECT c.nume_client, COUNT(f.factura_id) AS nr_facturi
FROM CLIENTI c
LEFT JOIN FACTURI f ON f.client_id = c.client_id
GROUP BY c.nume_client;

-- Q13 
SELECT c.nume_client, SUM(f.total_factura) AS total_vanzari
FROM CLIENTI c
JOIN FACTURI f ON f.client_id = c.client_id
GROUP BY c.nume_client;

-- Q14
SELECT c.nume_client, SUM(f.total_factura) AS total_vanzari
FROM CLIENTI c
JOIN FACTURI f ON f.client_id = c.client_id
GROUP BY c.nume_client
HAVING SUM(f.total_factura) >= 200;

-- Q15 
SELECT factura_id, TO_CHAR(data_facturii,'DD-Mon-YYYY') AS data_formatata
FROM FACTURI;

-- Q16 
SELECT factura_id,
       EXTRACT(YEAR FROM data_facturii) AS an,
       EXTRACT(MONTH FROM data_facturii) AS luna
FROM FACTURI;

-- Q17 
SELECT nume_produs, SUBSTR(nume_produs,1,4) AS prefix
FROM PRODUSE;

-- Q18 
SELECT factura_id, ROUND(SYSDATE - data_facturii) AS zile_trecute
FROM FACTURI;

-- Q19 
SELECT p.nume_produs,
       DECODE(p.categorie_id, 1,'Alimente',2,'Lactate',3,'Panificatie',4,'Bauturi','Alta') AS categorie_text
FROM PRODUSE p;

-- Q20 
SELECT nume_produs, pret_lista,
       CASE
         WHEN pret_lista > 50 THEN 'Scump'
         WHEN pret_lista BETWEEN 20 AND 50 THEN 'Mediu'
         ELSE 'Ieftin'
       END AS clasa_pret
FROM PRODUSE;

-- Q21 
SELECT nume || ' ' || prenume AS angajat,
       NVL(functie,'Nespecificata') AS functie_afisata
FROM ANGAJATI;

-- Q22 
SELECT nume_client AS denumire FROM CLIENTI
UNION
SELECT nume_furnizor FROM FURNIZORI;

-- Q23 
SELECT nume_produs, pret_lista
FROM PRODUSE
WHERE pret_lista > (SELECT AVG(pret_lista) FROM PRODUSE);

-- Q24 verificare: subcerere corelata - produse peste media categoriei lor
SELECT p1.nume_produs, p1.pret_lista, p1.categorie_id
FROM PRODUSE p1
WHERE p1.pret_lista > (
  SELECT AVG(p2.pret_lista)
  FROM PRODUSE p2
  WHERE p2.categorie_id = p1.categorie_id
);


-- Q25 
SELECT LEVEL AS nivel,
       LPAD(' ', LEVEL*2, ' ') || denumire_categorie AS categorie_indentata,
       SYS_CONNECT_BY_PATH(denumire_categorie, ' -> ') AS cale
FROM CATEGORII_PRODUSE
START WITH parent_id IS NULL
CONNECT BY PRIOR categorie_id = parent_id;


CREATE TABLE FACTURI_MARI (
  factura_id NUMBER(8),
  client_id NUMBER(5),
  total_factura NUMBER(12,2)
);

INSERT INTO FACTURI_MARI (factura_id, client_id, total_factura)
SELECT factura_id, client_id, total_factura
FROM FACTURI
WHERE total_factura > 100;


ALTER TABLE CLIENTI
ADD email VARCHAR2(100);

UPDATE CLIENTI
SET email = 'budes.cristian@student.ro'
WHERE nume_client = 'Budeș Cristian';

SELECT client_id, nume_client, email
FROM CLIENTI
WHERE nume_client = 'Budeș Cristian';


ALTER TABLE FACTURI
ADD CONSTRAINT CHK_TOTAL_FACTURA_POZ
CHECK (total_factura >= 0);

DROP TABLE DETALII_FACTURA CASCADE CONSTRAINTS PURGE;
DROP TABLE FACTURI CASCADE CONSTRAINTS PURGE;
DROP TABLE PRODUSE CASCADE CONSTRAINTS PURGE;
DROP TABLE ANGAJATI CASCADE CONSTRAINTS PURGE;
DROP TABLE CLIENTI CASCADE CONSTRAINTS PURGE;
DROP TABLE FURNIZORI CASCADE CONSTRAINTS PURGE;
DROP TABLE CATEGORII_PRODUSE CASCADE CONSTRAINTS PURGE;
DROP TABLE PRODUSE_BACKUP CASCADE CONSTRAINTS PURGE;
DROP TABLE FACTURI_MARI CASCADE CONSTRAINTS PURGE;
DROP VIEW V_RAPORT_FACTURI;
DROP SYNONYM SIN_CLIENTI;
DROP SYNONYM SIN_RAPORT;
DROP INDEX IDX_PRODUSE_NUME;
DROP SEQUENCE SEQ_CLIENTI;
DROP SEQUENCE SEQ_FACTURI;
COMMIT;
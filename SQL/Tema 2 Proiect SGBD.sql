SET SERVEROUTPUT ON;
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

-- Blocul 1
-- Auditarea si corectarea totalurilor facturilor pe baza liniilor din DETALII_FACTURA.
DECLARE
    CURSOR c_facturi IS
        SELECT factura_id, total_factura
        FROM facturi
        ORDER BY factura_id;

    CURSOR c_linii(p_factura_id facturi.factura_id%TYPE) IS
        SELECT cantitate, pret_vanzare
        FROM detalii_factura
        WHERE factura_id = p_factura_id;

    v_total_calculat facturi.total_factura%TYPE;
    v_nr_corectate   NUMBER := 0;
BEGIN
    FOR r_factura IN c_facturi LOOP
        v_total_calculat := 0;

        FOR r_linie IN c_linii(r_factura.factura_id) LOOP
            v_total_calculat := v_total_calculat + r_linie.cantitate * r_linie.pret_vanzare;
        END LOOP;

        IF ABS(v_total_calculat - r_factura.total_factura) > 0.01 THEN
            UPDATE facturi
            SET total_factura = v_total_calculat
            WHERE factura_id = r_factura.factura_id;

            IF SQL%ROWCOUNT = 1 THEN
                v_nr_corectate := v_nr_corectate + 1;
                DBMS_OUTPUT.PUT_LINE(
                    'Factura ' || r_factura.factura_id ||
                    ' a fost corectata de la ' || TO_CHAR(r_factura.total_factura, 'FM9990D00') ||
                    ' la ' || TO_CHAR(v_total_calculat, 'FM9990D00')
                );
            END IF;
        ELSE
            DBMS_OUTPUT.PUT_LINE('Factura ' || r_factura.factura_id || ' este deja corecta.');
        END IF;
    END LOOP;

    DBMS_OUTPUT.PUT_LINE('Numar total de facturi corectate: ' || v_nr_corectate);
END;
/

-- Blocul 5
-- Clasificarea produselor dupa valoarea vanzarilor obtinute.
DECLARE
    CURSOR c_produse_vandute IS
        SELECT p.produs_id,
               p.nume_produs,
               SUM(d.cantitate) AS cantitate_totala,
               SUM(d.cantitate * d.pret_vanzare) AS valoare_totala
        FROM produse p
        JOIN detalii_factura d
          ON d.produs_id = p.produs_id
        GROUP BY p.produs_id, p.nume_produs
        ORDER BY valoare_totala DESC, cantitate_totala DESC;

    r_produs        c_produse_vandute%ROWTYPE;
    v_medie_valoare NUMBER(12, 2);
    v_total_produse NUMBER;
    v_clasa_produs  VARCHAR2(40);
BEGIN
    SELECT COUNT(DISTINCT produs_id)
    INTO v_total_produse
    FROM detalii_factura;

    SELECT AVG(valoare_produs)
    INTO v_medie_valoare
    FROM (
        SELECT SUM(cantitate * pret_vanzare) AS valoare_produs
        FROM detalii_factura
        GROUP BY produs_id
    );

    DBMS_OUTPUT.PUT_LINE('Numar produse vandute: ' || v_total_produse);
    DBMS_OUTPUT.PUT_LINE('Media valorii vandute per produs: ' || TO_CHAR(v_medie_valoare, 'FM9990D00'));

    OPEN c_produse_vandute;
    LOOP
        FETCH c_produse_vandute INTO r_produs;
        EXIT WHEN c_produse_vandute%NOTFOUND;

        v_clasa_produs :=
            CASE
                WHEN r_produs.valoare_totala >= v_medie_valoare * 1.50 THEN 'Produs vedeta'
                WHEN r_produs.valoare_totala >= v_medie_valoare THEN 'Produs performant'
                ELSE 'Produs cu vanzari reduse'
            END;

        DBMS_OUTPUT.PUT_LINE(
            r_produs.nume_produs ||
            ' | cantitate: ' || r_produs.cantitate_totala ||
            ' | valoare: ' || TO_CHAR(r_produs.valoare_totala, 'FM9990D00') ||
            ' | clasa: ' || v_clasa_produs
        );
    END LOOP;
    CLOSE c_produse_vandute;
END;
/

-- Blocul 6
-- Evaluarea performantelor angajatilor pe baza facturilor emise.
DECLARE
    CURSOR c_performanta_angajati IS
        SELECT a.angajat_id,
               a.nume,
               a.prenume,
               COUNT(f.factura_id) AS nr_facturi,
               NVL(SUM(f.total_factura), 0) AS total_generat
        FROM angajati a
        LEFT JOIN facturi f
          ON f.angajat_id = a.angajat_id
        GROUP BY a.angajat_id, a.nume, a.prenume
        ORDER BY total_generat DESC, nr_facturi DESC;

    v_media_pe_angajat NUMBER(12, 2);
    v_clasificare      VARCHAR2(40);
BEGIN
    SELECT AVG(total_generat)
    INTO v_media_pe_angajat
    FROM (
        SELECT SUM(total_factura) AS total_generat
        FROM facturi
        GROUP BY angajat_id
    );

    DBMS_OUTPUT.PUT_LINE('Media vanzarilor pe angajat activ: ' || TO_CHAR(v_media_pe_angajat, 'FM9990D00'));

    FOR r_angajat IN c_performanta_angajati LOOP
        IF r_angajat.nr_facturi = 0 THEN
            v_clasificare := 'Fara activitate';
        ELSIF r_angajat.total_generat >= v_media_pe_angajat * 1.50 THEN
            v_clasificare := 'Performanta excelenta';
        ELSIF r_angajat.total_generat >= v_media_pe_angajat THEN
            v_clasificare := 'Performanta buna';
        ELSE
            v_clasificare := 'Performanta medie';
        END IF;

        DBMS_OUTPUT.PUT_LINE(
            r_angajat.nume || ' ' || r_angajat.prenume ||
            ' | facturi: ' || r_angajat.nr_facturi ||
            ' | total: ' || TO_CHAR(r_angajat.total_generat, 'FM9990D00') ||
            ' | status: ' || v_clasificare
        );
    END LOOP;
END;
/

-- Blocul 7
-- Raport ierarhic pe categorii si produse, folosind cursoare explicite imbricate.
DECLARE
    CURSOR c_radacini IS
        SELECT categorie_id, denumire_categorie
        FROM categorii_produse
        WHERE parent_id IS NULL
        ORDER BY categorie_id;

    CURSOR c_arbore(p_categorie_id categorii_produse.categorie_id%TYPE) IS
        SELECT categorie_id,
               denumire_categorie,
               LEVEL AS nivel
        FROM categorii_produse
        START WITH categorie_id = p_categorie_id
        CONNECT BY PRIOR categorie_id = parent_id
        ORDER SIBLINGS BY denumire_categorie;

    CURSOR c_produse_pe_categorie(p_categorie_id categorii_produse.categorie_id%TYPE) IS
        SELECT nume_produs, pret_lista
        FROM produse
        WHERE categorie_id = p_categorie_id
        ORDER BY nume_produs;

    v_nr_produse_arbore NUMBER;
    v_indentare         VARCHAR2(30);
    v_eticheta_pret     VARCHAR2(20);
BEGIN
    FOR r_radacina IN c_radacini LOOP
        SELECT COUNT(*)
        INTO v_nr_produse_arbore
        FROM produse
        WHERE categorie_id IN (
            SELECT categorie_id
            FROM categorii_produse
            START WITH categorie_id = r_radacina.categorie_id
            CONNECT BY PRIOR categorie_id = parent_id
        );

        DBMS_OUTPUT.PUT_LINE(
            'Categoria principala: ' || r_radacina.denumire_categorie ||
            ' | produse in arbore: ' || v_nr_produse_arbore
        );

        FOR r_nod IN c_arbore(r_radacina.categorie_id) LOOP
            v_indentare := LPAD(' ', (r_nod.nivel - 1) * 4, ' ');
            DBMS_OUTPUT.PUT_LINE(v_indentare || 'Subcategorie: ' || r_nod.denumire_categorie);

            FOR r_produs IN c_produse_pe_categorie(r_nod.categorie_id) LOOP
                IF r_produs.pret_lista >= 50 THEN
                    v_eticheta_pret := 'premium';
                ELSIF r_produs.pret_lista >= 15 THEN
                    v_eticheta_pret := 'mediu';
                ELSE
                    v_eticheta_pret := 'economic';
                END IF;

                DBMS_OUTPUT.PUT_LINE(
                    v_indentare || '  - ' || r_produs.nume_produs ||
                    ' | pret: ' || TO_CHAR(r_produs.pret_lista, 'FM9990D00') ||
                    ' | clasa: ' || v_eticheta_pret
                );
            END LOOP;
        END LOOP;
    END LOOP;
END;
/

-- Blocul 8
-- Analiza furnizorilor in functie de portofoliu si de valoarea vanzarilor generate.
DECLARE
    CURSOR c_furnizori IS
        SELECT furnizor_id, nume_furnizor
        FROM furnizori
        ORDER BY furnizor_id;

    CURSOR c_produse_furnizor(p_furnizor_id furnizori.furnizor_id%TYPE) IS
        SELECT nume_produs, pret_lista
        FROM produse
        WHERE furnizor_id = p_furnizor_id
        ORDER BY nume_produs;

    v_nr_produse      NUMBER;
    v_valoare_vanduta NUMBER(12, 2);
    v_status          VARCHAR2(40);
    v_tip_produs      VARCHAR2(20);
BEGIN
    FOR r_furnizor IN c_furnizori LOOP
        SELECT COUNT(DISTINCT p.produs_id),
               NVL(SUM(d.cantitate * d.pret_vanzare), 0)
        INTO v_nr_produse, v_valoare_vanduta
        FROM produse p
        LEFT JOIN detalii_factura d
          ON d.produs_id = p.produs_id
        WHERE p.furnizor_id = r_furnizor.furnizor_id;

        IF v_nr_produse = 0 THEN
            v_status := 'Furnizor fara portofoliu';
        ELSIF v_valoare_vanduta >= 150 THEN
            v_status := 'Furnizor strategic';
        ELSIF v_valoare_vanduta >= 50 THEN
            v_status := 'Furnizor important';
        ELSE
            v_status := 'Furnizor secundar';
        END IF;

        DBMS_OUTPUT.PUT_LINE(
            r_furnizor.nume_furnizor ||
            ' | produse: ' || v_nr_produse ||
            ' | vanzari: ' || TO_CHAR(v_valoare_vanduta, 'FM9990D00') ||
            ' | status: ' || v_status
        );

        FOR r_produs IN c_produse_furnizor(r_furnizor.furnizor_id) LOOP
            IF r_produs.pret_lista >= 50 THEN
                v_tip_produs := 'premium';
            ELSIF r_produs.pret_lista >= 15 THEN
                v_tip_produs := 'mediu';
            ELSE
                v_tip_produs := 'economic';
            END IF;

            DBMS_OUTPUT.PUT_LINE(
                '   -> ' || r_produs.nume_produs ||
                ' | pret lista: ' || TO_CHAR(r_produs.pret_lista, 'FM9990D00') ||
                ' | tip: ' || v_tip_produs
            );
        END LOOP;
    END LOOP;
END;
/

-- Blocul 2
-- Analiza unui client: numar facturi, total vanzari, valoarea maxima si incadrare comerciala.
DECLARE
    v_client_id    clienti.client_id%TYPE := 1;
    v_nume_client  clienti.nume_client%TYPE;
    v_nr_facturi   NUMBER;
    v_total_client NUMBER(12, 2);
    v_max_factura  NUMBER(12, 2);

    CURSOR c_facturi_client(p_client_id clienti.client_id%TYPE) IS
        SELECT factura_id, data_facturii, total_factura
        FROM facturi
        WHERE client_id = p_client_id
        ORDER BY data_facturii;
BEGIN
    SELECT nume_client
    INTO v_nume_client
    FROM clienti
    WHERE client_id = v_client_id;

    SELECT COUNT(*),
           NVL(SUM(total_factura), 0),
           NVL(MAX(total_factura), 0)
    INTO v_nr_facturi, v_total_client, v_max_factura
    FROM facturi
    WHERE client_id = v_client_id;

    DBMS_OUTPUT.PUT_LINE('Client analizat: ' || v_nume_client);

    IF v_nr_facturi = 0 THEN
        DBMS_OUTPUT.PUT_LINE('Clientul nu are facturi emise.');
    ELSE
        FOR r_factura IN c_facturi_client(v_client_id) LOOP
            DBMS_OUTPUT.PUT_LINE(
                'Factura ' || r_factura.factura_id ||
                ' | data: ' || TO_CHAR(r_factura.data_facturii, 'DD-MON-YYYY') ||
                ' | total: ' || TO_CHAR(r_factura.total_factura, 'FM9990D00')
            );
        END LOOP;

        IF v_total_client >= 200 THEN
            DBMS_OUTPUT.PUT_LINE('Client premium.');
        ELSIF v_total_client >= 100 THEN
            DBMS_OUTPUT.PUT_LINE('Client standard.');
        ELSE
            DBMS_OUTPUT.PUT_LINE('Client ocazional.');
        END IF;

        DBMS_OUTPUT.PUT_LINE('Numar facturi: ' || v_nr_facturi);
        DBMS_OUTPUT.PUT_LINE('Total vanzari catre client: ' || TO_CHAR(v_total_client, 'FM9990D00'));
        DBMS_OUTPUT.PUT_LINE('Valoarea maxima a unei facturi: ' || TO_CHAR(v_max_factura, 'FM9990D00'));
    END IF;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('Nu exista un client cu id-ul ' || v_client_id || '.');
END;
/

-- Blocul 3
-- Verificarea discounturilor acordate pe o factura fata de pretul de lista al produselor.
DECLARE
    v_factura_id  facturi.factura_id%TYPE := 106;
    v_nume_client clienti.nume_client%TYPE;
    v_nr_abateri  NUMBER := 0;
    v_discount    NUMBER(6, 2);

    CURSOR c_linii_factura(p_factura_id facturi.factura_id%TYPE) IS
        SELECT p.nume_produs,
               p.pret_lista,
               d.pret_vanzare,
               d.cantitate
        FROM detalii_factura d
        JOIN produse p
          ON p.produs_id = d.produs_id
        WHERE d.factura_id = p_factura_id;

    r_linie c_linii_factura%ROWTYPE;
BEGIN
    SELECT c.nume_client
    INTO v_nume_client
    FROM facturi f
    JOIN clienti c
      ON c.client_id = f.client_id
    WHERE f.factura_id = v_factura_id;

    DBMS_OUTPUT.PUT_LINE('Factura analizata: ' || v_factura_id || ' | client: ' || v_nume_client);

    OPEN c_linii_factura(v_factura_id);
    LOOP
        FETCH c_linii_factura INTO r_linie;
        EXIT WHEN c_linii_factura%NOTFOUND;

        v_discount := ROUND((1 - r_linie.pret_vanzare / r_linie.pret_lista) * 100, 2);

        IF r_linie.pret_vanzare < r_linie.pret_lista * 0.70 THEN
            v_nr_abateri := v_nr_abateri + 1;
            DBMS_OUTPUT.PUT_LINE(
                'Reducere critica pentru ' || r_linie.nume_produs ||
                ' | discount: ' || TO_CHAR(v_discount, 'FM990D00') || '%'
            );
        ELSIF r_linie.pret_vanzare < r_linie.pret_lista THEN
            DBMS_OUTPUT.PUT_LINE(
                'Reducere acceptata pentru ' || r_linie.nume_produs ||
                ' | discount: ' || TO_CHAR(v_discount, 'FM990D00') || '%'
            );
        ELSIF r_linie.pret_vanzare = r_linie.pret_lista THEN
            DBMS_OUTPUT.PUT_LINE('Produs vandut la pretul de lista: ' || r_linie.nume_produs);
        ELSE
            DBMS_OUTPUT.PUT_LINE('Produs vandut peste pretul de lista: ' || r_linie.nume_produs);
        END IF;
    END LOOP;
    CLOSE c_linii_factura;

    IF v_nr_abateri = 0 THEN
        DBMS_OUTPUT.PUT_LINE('Nu au fost identificate abateri majore de discount.');
    ELSE
        DBMS_OUTPUT.PUT_LINE('Numar abateri majore identificate: ' || v_nr_abateri);
    END IF;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('Factura ' || v_factura_id || ' nu exista in baza de date.');
END;
/

-- Blocul 4
-- Completarea functiilor lipsa pentru angajati folosind cursor explicit FOR UPDATE.
DECLARE
    CURSOR c_angajati_fara_functie IS
        SELECT angajat_id, nume, prenume
        FROM angajati
        WHERE functie IS NULL
        FOR UPDATE OF functie;

    v_functie_noua angajati.functie%TYPE;
    v_actualizati  NUMBER := 0;
BEGIN
    FOR r_angajat IN c_angajati_fara_functie LOOP
        IF UPPER(r_angajat.prenume) IN ('MARIA', 'ANA', 'ELENA', 'DARIA', 'IOANA') THEN
            v_functie_noua := 'Consultant Vanzari';
        ELSE
            v_functie_noua := 'Agent Vanzari';
        END IF;

        UPDATE angajati
        SET functie = v_functie_noua
        WHERE CURRENT OF c_angajati_fara_functie;

        IF SQL%ROWCOUNT = 1 THEN
            v_actualizati := v_actualizati + 1;
            DBMS_OUTPUT.PUT_LINE(
                'Angajat actualizat: ' || r_angajat.nume || ' ' || r_angajat.prenume ||
                ' -> ' || v_functie_noua
            );
        END IF;
    END LOOP;

    IF v_actualizati = 0 THEN
        DBMS_OUTPUT.PUT_LINE('Nu exista angajati fara functie sau acestia au fost deja actualizati.');
    ELSE
        DBMS_OUTPUT.PUT_LINE('Total angajati actualizati: ' || v_actualizati);
    END IF;
END;
/


--TEMA 2      
--Blocul 1



DECLARE
    v_client_id    clienti.client_id%TYPE := 1;
    v_prag_minim   NUMBER(12, 2) := 200;
    v_nume_client  clienti.nume_client%TYPE;
    v_total_client NUMBER(12, 2);
    v_nr_facturi   NUMBER;

    e_client_sub_prag EXCEPTION;

    CURSOR c_facturi_client(p_client_id clienti.client_id%TYPE) IS
        SELECT factura_id, data_facturii, total_factura
        FROM facturi
        WHERE client_id = p_client_id
        ORDER BY data_facturii;
BEGIN
    SELECT nume_client
    INTO v_nume_client
    FROM clienti
    WHERE client_id = v_client_id;

    SELECT COUNT(*), NVL(SUM(total_factura), 0)
    INTO v_nr_facturi, v_total_client
    FROM facturi
    WHERE client_id = v_client_id;

    DBMS_OUTPUT.PUT_LINE('Client analizat: ' || v_nume_client);

    FOR r_factura IN c_facturi_client(v_client_id) LOOP
        DBMS_OUTPUT.PUT_LINE(
            'Factura ' || r_factura.factura_id ||
            ' | data: ' || TO_CHAR(r_factura.data_facturii, 'DD-MON-YYYY') ||
            ' | total: ' || TO_CHAR(r_factura.total_factura, 'FM9990D00')
        );
    END LOOP;

    IF v_nr_facturi = 0 OR v_total_client < v_prag_minim THEN
        RAISE e_client_sub_prag;
    ELSE
        DBMS_OUTPUT.PUT_LINE('Clientul depaseste pragul minim de vanzari.');
    END IF;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('Exceptie de sistem: nu exista clientul cu id-ul ' || v_client_id || '.');
    WHEN e_client_sub_prag THEN
        DBMS_OUTPUT.PUT_LINE(
            'Exceptie definita de utilizator: clientul are totalul ' ||
            TO_CHAR(v_total_client, 'FM9990D00') ||
            ', sub pragul minim de ' || TO_CHAR(v_prag_minim, 'FM9990D00') || '.'
        );
END;
/

-- Blocul 2
-- Simularea emiterii unei facturi si tratarea exceptiilor DUP_VAL_ON_INDEX si linie_invalida.
DECLARE
    v_factura_id   facturi.factura_id%TYPE := 100;
    v_client_id    clienti.client_id%TYPE := 1;
    v_angajat_id   angajati.angajat_id%TYPE := 1;
    v_total_factura facturi.total_factura%TYPE := 0;

    e_linie_invalida EXCEPTION;

    CURSOR c_produse_factura IS
        SELECT produs_id, nume_produs, pret_lista,
               CASE produs_id
                   WHEN 1 THEN 3
                   WHEN 4 THEN 5
               END AS cantitate
        FROM produse
        WHERE produs_id IN (1, 4)
        ORDER BY produs_id;
BEGIN
    SAVEPOINT sp_factura_noua;

    FOR r_produs IN c_produse_factura LOOP
        IF r_produs.cantitate IS NULL OR r_produs.cantitate <= 0 THEN
            RAISE e_linie_invalida;
        END IF;

        v_total_factura := v_total_factura + r_produs.cantitate * r_produs.pret_lista;
    END LOOP;

    INSERT INTO facturi(factura_id, client_id, angajat_id, data_facturii, total_factura)
    VALUES (v_factura_id, v_client_id, v_angajat_id, SYSDATE, v_total_factura);

    FOR r_produs IN c_produse_factura LOOP
        INSERT INTO detalii_factura(factura_id, produs_id, cantitate, pret_vanzare)
        VALUES (v_factura_id, r_produs.produs_id, r_produs.cantitate, r_produs.pret_lista);
    END LOOP;

    DBMS_OUTPUT.PUT_LINE('Factura ' || v_factura_id || ' a fost emisa cu totalul ' || TO_CHAR(v_total_factura, 'FM9990D00') || '.');
    DBMS_OUTPUT.PUT_LINE('Randuri inserate in ultima instructiune: ' || SQL%ROWCOUNT);

    ROLLBACK TO SAVEPOINT sp_factura_noua;
    DBMS_OUTPUT.PUT_LINE('Simularea a fost anulata prin ROLLBACK TO SAVEPOINT.');
EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN
        ROLLBACK TO SAVEPOINT sp_factura_noua;
        DBMS_OUTPUT.PUT_LINE('Exceptie de sistem: exista deja factura cu id-ul ' || v_factura_id || '.');
    WHEN e_linie_invalida THEN
        ROLLBACK TO SAVEPOINT sp_factura_noua;
        DBMS_OUTPUT.PUT_LINE('Exceptie definita de utilizator: exista o linie de factura cu o cantitate invalida.');
    WHEN OTHERS THEN
        ROLLBACK TO SAVEPOINT sp_factura_noua;
        DBMS_OUTPUT.PUT_LINE('Alta exceptie: ' || SQLERRM);
END;
/

-- Blocul 3
-- Auditarea discounturilor si tratarea exceptiilor NO_DATA_FOUND si discount_critic.
DECLARE
    v_factura_id   facturi.factura_id%TYPE := 106;
    v_nume_client  clienti.nume_client%TYPE;
    v_discount     NUMBER(6, 2);
    v_produs_alerta produse.nume_produs%TYPE;

    e_discount_critic EXCEPTION;

    CURSOR c_linii_factura(p_factura_id facturi.factura_id%TYPE) IS
        SELECT p.nume_produs, p.pret_lista, d.pret_vanzare, d.cantitate
        FROM detalii_factura d
        JOIN produse p ON p.produs_id = d.produs_id
        WHERE d.factura_id = p_factura_id;
BEGIN
    SELECT c.nume_client
    INTO v_nume_client
    FROM facturi f
    JOIN clienti c ON c.client_id = f.client_id
    WHERE f.factura_id = v_factura_id;

    DBMS_OUTPUT.PUT_LINE('Factura analizata: ' || v_factura_id || ' | client: ' || v_nume_client);

    FOR r_linie IN c_linii_factura(v_factura_id) LOOP
        v_discount := ROUND((1 - r_linie.pret_vanzare / r_linie.pret_lista) * 100, 2);

        DBMS_OUTPUT.PUT_LINE(
            r_linie.nume_produs ||
            ' | pret lista: ' || TO_CHAR(r_linie.pret_lista, 'FM9990D00') ||
            ' | pret vanzare: ' || TO_CHAR(r_linie.pret_vanzare, 'FM9990D00') ||
            ' | discount: ' || TO_CHAR(v_discount, 'FM990D00') || '%'
        );

        IF r_linie.pret_vanzare < r_linie.pret_lista * 0.70 THEN
            v_produs_alerta := r_linie.nume_produs;
            RAISE e_discount_critic;
        END IF;
    END LOOP;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('Exceptie de sistem: factura ' || v_factura_id || ' nu exista.');
    WHEN e_discount_critic THEN
        DBMS_OUTPUT.PUT_LINE('Exceptie definita de utilizator: discount critic pentru produsul ' || v_produs_alerta || '.');
END;
/

-- Blocul 4
-- Cautarea produselor dupa fragment si tratarea exceptiilor TOO_MANY_ROWS si fragment_invalid.
DECLARE
    v_fragment    VARCHAR2(20) := 'a';
    v_produs_id   produse.produs_id%TYPE;
    v_nume_produs produse.nume_produs%TYPE;
    v_pret_lista  produse.pret_lista%TYPE;

    e_fragment_invalid EXCEPTION;

    CURSOR c_produse_gasite(p_fragment VARCHAR2) IS
        SELECT produs_id, nume_produs, pret_lista
        FROM produse
        WHERE LOWER(nume_produs) LIKE '%' || LOWER(p_fragment) || '%'
        ORDER BY nume_produs;
BEGIN
    IF v_fragment IS NULL OR LENGTH(TRIM(v_fragment)) = 0 THEN
        RAISE e_fragment_invalid;
    END IF;

    SELECT produs_id, nume_produs, pret_lista
    INTO v_produs_id, v_nume_produs, v_pret_lista
    FROM produse
    WHERE LOWER(nume_produs) LIKE '%' || LOWER(v_fragment) || '%';

    DBMS_OUTPUT.PUT_LINE('Produs gasit: ' || v_nume_produs || ' | pret: ' || TO_CHAR(v_pret_lista, 'FM9990D00'));
EXCEPTION
    WHEN TOO_MANY_ROWS THEN
        DBMS_OUTPUT.PUT_LINE('Exceptie de sistem: fragmentul "' || v_fragment || '" intoarce mai multe produse.');
        DBMS_OUTPUT.PUT_LINE('Lista produselor gasite:');

        FOR r_produs IN c_produse_gasite(v_fragment) LOOP
            DBMS_OUTPUT.PUT_LINE(
                '- ' || r_produs.nume_produs ||
                ' | id: ' || r_produs.produs_id ||
                ' | pret: ' || TO_CHAR(r_produs.pret_lista, 'FM9990D00')
            );
        END LOOP;
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('Exceptie de sistem: nu exista produse care contin fragmentul "' || v_fragment || '".');
    WHEN e_fragment_invalid THEN
        DBMS_OUTPUT.PUT_LINE('Exceptie definita de utilizator: fragmentul de cautare nu poate fi gol.');
END;
/

-- Blocul 5
-- Calcularea unui bonus pentru angajat si tratarea exceptiilor NO_DATA_FOUND si bonus_neeligibil.
DECLARE
    v_angajat_id    angajati.angajat_id%TYPE := 4;
    v_prag_bonus    NUMBER(12, 2) := 250;
    v_procent_bonus NUMBER(5, 2) := 5;
    v_angajat       VARCHAR2(120);
    v_total_vanzari NUMBER(12, 2);
    v_bonus         NUMBER(12, 2);

    e_bonus_neeligibil EXCEPTION;
    e_procent_invalid  EXCEPTION;

    CURSOR c_facturi_angajat(p_angajat_id angajati.angajat_id%TYPE) IS
        SELECT factura_id, data_facturii, total_factura
        FROM facturi
        WHERE angajat_id = p_angajat_id
        ORDER BY data_facturii;
BEGIN
    IF v_procent_bonus <= 0 THEN
        RAISE e_procent_invalid;
    END IF;

    SELECT nume || ' ' || prenume
    INTO v_angajat
    FROM angajati
    WHERE angajat_id = v_angajat_id;

    SELECT NVL(SUM(total_factura), 0)
    INTO v_total_vanzari
    FROM facturi
    WHERE angajat_id = v_angajat_id;

    DBMS_OUTPUT.PUT_LINE('Angajat analizat: ' || v_angajat);

    FOR r_factura IN c_facturi_angajat(v_angajat_id) LOOP
        DBMS_OUTPUT.PUT_LINE(
            'Factura ' || r_factura.factura_id ||
            ' | data: ' || TO_CHAR(r_factura.data_facturii, 'DD-MON-YYYY') ||
            ' | total: ' || TO_CHAR(r_factura.total_factura, 'FM9990D00')
        );
    END LOOP;

    IF v_total_vanzari < v_prag_bonus THEN
        RAISE e_bonus_neeligibil;
    END IF;

    v_bonus := v_total_vanzari * v_procent_bonus / 100;
    DBMS_OUTPUT.PUT_LINE('Bonus calculat: ' || TO_CHAR(v_bonus, 'FM9990D00'));
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('Exceptie de sistem: nu exista angajatul cu id-ul ' || v_angajat_id || '.');
    WHEN e_bonus_neeligibil THEN
        DBMS_OUTPUT.PUT_LINE(
            'Exceptie definita de utilizator: totalul vanzarilor este ' ||
            TO_CHAR(v_total_vanzari, 'FM9990D00') ||
            ', sub pragul de bonus ' || TO_CHAR(v_prag_bonus, 'FM9990D00') || '.'
        );
    WHEN e_procent_invalid THEN
        DBMS_OUTPUT.PUT_LINE('Exceptie definita de utilizator: procentul de bonus trebuie sa fie pozitiv.');
END;
/

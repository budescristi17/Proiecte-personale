-- Functia 1: f_total_factura
CREATE OR REPLACE FUNCTION f_total_factura(p_factura_id IN facturi.factura_id%TYPE) 
RETURN NUMBER IS
CURSOR c_linii_factura IS
SELECT cantitate, pret_vanzare
FROM detalii_factura
WHERE factura_id = p_factura_id;
v_total NUMBER(12, 2) := 0;
v_nr_linii NUMBER := 0;
v_exista facturi.factura_id%TYPE;

e_factura_fara_linii EXCEPTION;
BEGIN
SELECT factura_id
INTO v_exista
FROM facturi
WHERE factura_id = p_factura_id;

FOR r_linie IN c_linii_factura LOOP
v_total := v_total + r_linie.cantitate * r_linie.pret_vanzare;
v_nr_linii := v_nr_linii + 1;
END LOOP;

IF v_nr_linii = 0 THEN
RAISE e_factura_fara_linii;
END IF;

RETURN ROUND(v_total, 2);
EXCEPTION
WHEN NO_DATA_FOUND THEN
RAISE_APPLICATION_ERROR(-20001, 'Factura ' || p_factura_id || ' nu exista.');
WHEN e_factura_fara_linii THEN
RAISE_APPLICATION_ERROR(-20002, 'Factura ' || p_factura_id || ' nu are linii in DETALII_FACTURA.');
END f_total_factura;
/
SHOW ERRORS FUNCTION f_total_factura;

SELECT f_total_factura(106) FROM dual;

-- Functia 2: f_total_client
CREATE OR REPLACE FUNCTION f_total_client(p_client_id IN clienti.client_id%TYPE) 
RETURN NUMBER IS
v_exista clienti.client_id%TYPE;
v_total_client NUMBER(12, 2);
BEGIN
SELECT client_id
INTO v_exista
FROM clienti
WHERE client_id = p_client_id;
SELECT NVL(SUM(total_factura), 0)
INTO v_total_client
FROM facturi
WHERE client_id = p_client_id;
RETURN ROUND(v_total_client, 2);

EXCEPTION
WHEN NO_DATA_FOUND THEN
RAISE_APPLICATION_ERROR(-20003, 'Clientul ' || p_client_id || ' nu exista.');
END f_total_client;
/
SHOW ERRORS FUNCTION f_total_client;

SELECT f_total_client(1) FROM dual;

-- Functia 3: f_categorie_client
CREATE OR REPLACE FUNCTION f_categorie_client(p_client_id IN clienti.client_id%TYPE) 
RETURN VARCHAR2
IS
v_total_client NUMBER(12, 2);
v_nr_facturi   NUMBER;
BEGIN
v_total_client := f_total_client(p_client_id);

SELECT COUNT(*)
INTO v_nr_facturi
FROM facturi
WHERE client_id = p_client_id;

IF v_nr_facturi = 0 THEN
RETURN 'Fara activitate';
ELSIF v_total_client >= 250 THEN
RETURN 'Client premium';
ELSIF v_total_client >= 100 THEN
RETURN 'Client standard';
ELSE
RETURN 'Client ocazional';
END IF;
END f_categorie_client;
/
SHOW ERRORS FUNCTION f_categorie_client;

SELECT f_categorie_client(1) FROM dual;

-- Functia 4: f_valoare_vanduta_produs
CREATE OR REPLACE FUNCTION f_valoare_vanduta_produs(p_produs_id IN produse.produs_id%TYPE) 
RETURN NUMBER IS
v_exista produse.produs_id%TYPE;
v_valoare_vanduta NUMBER(12, 2);
BEGIN
SELECT produs_id
INTO v_exista
FROM produse
WHERE produs_id = p_produs_id;
SELECT NVL(SUM(cantitate * pret_vanzare), 0)
INTO v_valoare_vanduta
FROM detalii_factura
WHERE produs_id = p_produs_id;
RETURN ROUND(v_valoare_vanduta, 2);

EXCEPTION
WHEN NO_DATA_FOUND THEN
RAISE_APPLICATION_ERROR(-20004, 'Produsul ' || p_produs_id || ' nu exista.');
END f_valoare_vanduta_produs;
/
SHOW ERRORS FUNCTION f_valoare_vanduta_produs;

SELECT f_valoare_vanduta_produs(2) FROM dual;

-- Functia 5: f_produs_vedeta_furnizor
CREATE OR REPLACE FUNCTION f_produs_vedeta_furnizor(p_furnizor_id IN furnizori.furnizor_id%TYPE) 
RETURN VARCHAR2 IS
CURSOR c_top_produs IS
SELECT p.nume_produs, NVL(SUM(d.cantitate * d.pret_vanzare), 0) AS valoare_vanduta
FROM produse p
LEFT JOIN detalii_factura d
ON d.produs_id = p.produs_id
WHERE p.furnizor_id = p_furnizor_id
GROUP BY p.produs_id, p.nume_produs
ORDER BY valoare_vanduta DESC, p.nume_produs;

v_exista furnizori.furnizor_id%TYPE;
v_nume_produs produse.nume_produs%TYPE;
v_valoare NUMBER(12, 2);
BEGIN
SELECT furnizor_id
INTO v_exista
FROM furnizori
WHERE furnizor_id = p_furnizor_id;
OPEN c_top_produs;
FETCH c_top_produs INTO v_nume_produs, v_valoare;

IF c_top_produs%NOTFOUND THEN
CLOSE c_top_produs;
RETURN 'Fara produse';
END IF;
CLOSE c_top_produs;
RETURN v_nume_produs || ' (' || TO_CHAR(v_valoare, 'FM9990D00') || ')';

EXCEPTION
WHEN NO_DATA_FOUND THEN
RAISE_APPLICATION_ERROR(-20005, 'Furnizorul ' || p_furnizor_id || ' nu exista.');
END f_produs_vedeta_furnizor;
/
SHOW ERRORS FUNCTION f_produs_vedeta_furnizor;

SELECT f_produs_vedeta_furnizor(1) FROM dual;

-- Procedura 1: p_recalculeaza_factura
CREATE OR REPLACE PROCEDURE p_recalculeaza_factura(p_factura_id IN facturi.factura_id%TYPE) IS
v_total_vechi facturi.total_factura%TYPE;
v_total_nou   NUMBER(12, 2);
BEGIN
SELECT total_factura
INTO v_total_vechi
FROM facturi
WHERE factura_id = p_factura_id;
v_total_nou := f_total_factura(p_factura_id);
    
IF ABS(v_total_vechi - v_total_nou) > 0.01 THEN
UPDATE facturi
SET total_factura = v_total_nou
WHERE factura_id = p_factura_id;

DBMS_OUTPUT.PUT_LINE('Factura ' || p_factura_id ||' a fost actualizata de la ' || TO_CHAR(v_total_vechi, 'FM9990D00') ||' la ' || TO_CHAR(v_total_nou, 'FM9990D00') ||'. Randuri modificate: ' || SQL%ROWCOUNT);
ELSE
DBMS_OUTPUT.PUT_LINE('Factura ' || p_factura_id || ' are deja totalul corect.');
END IF;
EXCEPTION
WHEN NO_DATA_FOUND THEN
RAISE_APPLICATION_ERROR(-20006, 'Factura ' || p_factura_id || ' nu exista.');
END p_recalculeaza_factura;
/
SHOW ERRORS PROCEDURE p_recalculeaza_factura;

EXEC p_recalculeaza_factura(106);

-- Procedura 2: p_raport_client 
CREATE OR REPLACE PROCEDURE p_raport_client(p_client_id IN clienti.client_id%TYPE) IS
TYPE t_factura_rec IS RECORD (factura_id    facturi.factura_id%TYPE, data_facturii facturi.data_facturii%TYPE, total_factura facturi.total_factura%TYPE);
TYPE t_facturi_tab IS TABLE OF t_factura_rec INDEX BY PLS_INTEGER;
CURSOR c_facturi_client IS
SELECT factura_id, data_facturii, total_factura
FROM facturi
WHERE client_id = p_client_id
ORDER BY data_facturii, factura_id;
v_facturi t_facturi_tab;
v_index PLS_INTEGER := 0;
v_nume_client clienti.nume_client%TYPE;
v_total_client NUMBER(12, 2);
v_categorie VARCHAR2(30);
BEGIN
SELECT nume_client
INTO v_nume_client
FROM clienti
WHERE client_id = p_client_id;
v_total_client := f_total_client(p_client_id);
v_categorie := f_categorie_client(p_client_id);

FOR r_factura IN c_facturi_client LOOP
v_index := v_index + 1;
v_facturi(v_index).factura_id := r_factura.factura_id;
v_facturi(v_index).data_facturii := r_factura.data_facturii;
v_facturi(v_index).total_factura := r_factura.total_factura;
END LOOP;

DBMS_OUTPUT.PUT_LINE('--- RAPORT CLIENT ---');
DBMS_OUTPUT.PUT_LINE('Client: ' || v_nume_client);
DBMS_OUTPUT.PUT_LINE('Categorie: ' || v_categorie);
DBMS_OUTPUT.PUT_LINE('Total facturat: ' || TO_CHAR(v_total_client, 'FM9990D00'));
DBMS_OUTPUT.PUT_LINE('Numar facturi: ' || v_facturi.COUNT);

IF v_facturi.COUNT = 0 THEN
DBMS_OUTPUT.PUT_LINE('Clientul nu are facturi emise.');
ELSE
FOR i IN 1 .. v_facturi.COUNT LOOP
DBMS_OUTPUT.PUT_LINE('Factura ' || v_facturi(i).factura_id || ' | data: ' || TO_CHAR(v_facturi(i).data_facturii, 'DD-MON-YYYY') || ' | total: ' || TO_CHAR(v_facturi(i).total_factura, 'FM9990D00'));
END LOOP;
END IF;

EXCEPTION
WHEN NO_DATA_FOUND THEN
RAISE_APPLICATION_ERROR(-20007, 'Clientul ' || p_client_id || ' nu exista.');
END p_raport_client;
/
SHOW ERRORS PROCEDURE p_raport_client;

EXEC p_raport_client(1);

-- Procedura 3: p_discount_factura
CREATE OR REPLACE PROCEDURE p_discount_factura(p_factura_id IN facturi.factura_id%TYPE) IS
CURSOR c_linii_factura IS
SELECT p.nume_produs, p.pret_lista, d.pret_vanzare, d.cantitate
FROM detalii_factura d
JOIN produse p
ON p.produs_id = d.produs_id
WHERE d.factura_id = p_factura_id
ORDER BY p.nume_produs;
v_client clienti.nume_client%TYPE;
v_discount NUMBER(6, 2);
v_nr_linii NUMBER := 0;
v_nr_critice NUMBER := 0;
v_mesaj VARCHAR2(40);

e_discount_critic EXCEPTION;
BEGIN
SELECT c.nume_client
INTO v_client
FROM facturi f
JOIN clienti c
ON c.client_id = f.client_id
WHERE f.factura_id = p_factura_id;

DBMS_OUTPUT.PUT_LINE('---DISCOUNT  ' || p_factura_id || ' ---');
DBMS_OUTPUT.PUT_LINE('Client: ' || v_client);

FOR r_linie IN c_linii_factura LOOP
v_nr_linii := v_nr_linii + 1;
IF r_linie.pret_lista = 0 
THEN v_discount := 0;
ELSE
v_discount := ROUND((1 - r_linie.pret_vanzare / r_linie.pret_lista) * 100, 2);
END IF;
IF r_linie.pret_vanzare < r_linie.pret_lista * 0.70 
THEN v_nr_critice := v_nr_critice + 1;
v_mesaj := 'discount critic';
ELSIF r_linie.pret_vanzare < r_linie.pret_lista THEN
v_mesaj := 'discount acceptat';
ELSIF r_linie.pret_vanzare = r_linie.pret_lista THEN
v_mesaj := 'pret de lista';
ELSE
v_mesaj := 'peste pret lista';
END IF;
DBMS_OUTPUT.PUT_LINE(r_linie.nume_produs || ' | cantitate: ' || r_linie.cantitate || ' | pret lista: ' || TO_CHAR(r_linie.pret_lista, 'FM9990D00') || ' | pret vanzare: ' || TO_CHAR(r_linie.pret_vanzare, 'FM9990D00') || ' | discount: ' || TO_CHAR(v_discount, 'FM990D00') || '%' || ' | status: ' || v_mesaj);
END LOOP;

IF v_nr_linii = 0 
THEN
DBMS_OUTPUT.PUT_LINE('Factura nu are linii de detaliu.');
ELSIF v_nr_critice > 0 
THEN
RAISE e_discount_critic;
ELSE
DBMS_OUTPUT.PUT_LINE('Nu exista discounturi critice.');
END IF;

EXCEPTION
WHEN NO_DATA_FOUND 
THEN
RAISE_APPLICATION_ERROR(-20008, 'Factura ' || p_factura_id || ' nu exista.');
WHEN e_discount_critic 
THEN
DBMS_OUTPUT.PUT_LINE('Atentie: au fost gasite ' || v_nr_critice || ' discounturi critice.');
END p_discount_factura;
/
SHOW ERRORS PROCEDURE p_audit_discount_factura;

EXEC p_discount_factura(106);

-- Procedura 4: p_raport_furnizor
CREATE OR REPLACE PROCEDURE p_raport_furnizor(p_furnizor_id IN furnizori.furnizor_id%TYPE)
IS
TYPE t_produs_rec IS RECORD (produs_id produse.produs_id%TYPE, nume_produs produse.nume_produs%TYPE, pret_lista produse.pret_lista%TYPE, valoare_vanduta NUMBER(12, 2));
TYPE t_produse_tab IS TABLE OF t_produs_rec INDEX BY PLS_INTEGER;
CURSOR c_produse_furnizor IS
SELECT produs_id, nume_produs, pret_lista
FROM produse
WHERE furnizor_id = p_furnizor_id
ORDER BY nume_produs;
v_produse t_produse_tab;
v_index  PLS_INTEGER := 0;
v_nume_furnizor  furnizori.nume_furnizor%TYPE;
v_total_furnizor NUMBER(12, 2) := 0;
v_clasa_pret VARCHAR2(20);
BEGIN
SELECT nume_furnizor
INTO v_nume_furnizor
FROM furnizori
WHERE furnizor_id = p_furnizor_id;

FOR r_produs IN c_produse_furnizor LOOP
v_index := v_index + 1;
v_produse(v_index).produs_id := r_produs.produs_id;
v_produse(v_index).nume_produs := r_produs.nume_produs;
v_produse(v_index).pret_lista := r_produs.pret_lista;
v_produse(v_index).valoare_vanduta := f_valoare_vanduta_produs(r_produs.produs_id);
v_total_furnizor := v_total_furnizor + v_produse(v_index).valoare_vanduta;
END LOOP;

DBMS_OUTPUT.PUT_LINE('--- RAPORT FURNIZOR ---');
DBMS_OUTPUT.PUT_LINE('Furnizor: ' || v_nume_furnizor);
DBMS_OUTPUT.PUT_LINE('Produs vedeta: ' || f_produs_vedeta_furnizor(p_furnizor_id));
DBMS_OUTPUT.PUT_LINE('Valoare totala vanduta: ' || TO_CHAR(v_total_furnizor, 'FM9990D00'));

IF v_produse.COUNT = 0 THEN
DBMS_OUTPUT.PUT_LINE('Furnizorul nu are produse.');
ELSE
FOR i IN 1 .. v_produse.COUNT LOOP
CASE
WHEN v_produse(i).pret_lista >= 50 
THEN v_clasa_pret := 'premium';
WHEN v_produse(i).pret_lista >= 15 
THEN v_clasa_pret := 'mediu';
ELSE
v_clasa_pret := 'economic';
END CASE;
DBMS_OUTPUT.PUT_LINE(v_produse(i).nume_produs || ' | pret lista: ' || TO_CHAR(v_produse(i).pret_lista, 'FM9990D00') || ' | clasa: ' || v_clasa_pret || ' | valoare vanduta: ' || TO_CHAR(v_produse(i).valoare_vanduta, 'FM9990D00'));
END LOOP;
END IF;

EXCEPTION
WHEN NO_DATA_FOUND THEN
RAISE_APPLICATION_ERROR(-20009, 'Furnizorul ' || p_furnizor_id || ' nu exista.');
END p_raport_furnizor;
/
SHOW ERRORS PROCEDURE p_raport_furnizor;

EXEC p_raport_furnizor(1);

-- Procedura 5: p_ajustare_preturi_categorie
CREATE OR REPLACE PROCEDURE p_ajustare_preturi_categorie(p_categorie_id IN categorii_produse.categorie_id%TYPE, p_procent IN NUMBER, p_nr_modificate OUT NUMBER)
IS
CURSOR c_produse_categorie IS
SELECT produs_id, nume_produs, pret_lista
FROM produse
WHERE categorie_id = p_categorie_id
FOR UPDATE OF pret_lista;
v_exista       categorii_produse.categorie_id%TYPE;
v_pret_nou     produse.pret_lista%TYPE;

e_procent_invalid EXCEPTION;
BEGIN
p_nr_modificate := 0;
IF p_procent = 0 OR ABS(p_procent) > 30 THEN RAISE e_procent_invalid;
END IF;

SELECT categorie_id
INTO v_exista
FROM categorii_produse
WHERE categorie_id = p_categorie_id;

FOR r_produs IN c_produse_categorie LOOP
v_pret_nou := ROUND(r_produs.pret_lista * (1 + p_procent / 100), 2);
UPDATE produse
SET pret_lista = v_pret_nou
WHERE CURRENT OF c_produse_categorie;
IF SQL%ROWCOUNT = 1 THEN
p_nr_modificate := p_nr_modificate + 1;
DBMS_OUTPUT.PUT_LINE('Produs actualizat: ' || r_produs.nume_produs || ' | pret vechi: ' || TO_CHAR(r_produs.pret_lista, 'FM9990D00') || ' | pret nou: ' || TO_CHAR(v_pret_nou, 'FM9990D00'));
END IF;
END LOOP;

DBMS_OUTPUT.PUT_LINE('Total produse modificate: ' || p_nr_modificate);
EXCEPTION
WHEN NO_DATA_FOUND THEN
RAISE_APPLICATION_ERROR(-20010, 'Categoria ' || p_categorie_id || ' nu exista.');
WHEN e_procent_invalid THEN
RAISE_APPLICATION_ERROR(-20011, 'Procentul trebuie sa fie diferit de 0 si intre -30 si 30.');
END p_ajustare_preturi_categorie;
/
SHOW ERRORS PROCEDURE p_ajustare_preturi_categorie;

DECLARE
v_nr NUMBER;
BEGIN
p_ajustare_preturi_categorie(2, 10, v_nr);
DBMS_OUTPUT.PUT_LINE(v_nr);
END;
/

-- Pachetul 1: pkg_facturare
CREATE OR REPLACE PACKAGE pkg_facturare AS
TYPE t_rezumat_client_rec IS RECORD (client_id clienti.client_id%TYPE,nume_client clienti.nume_client%TYPE,nr_facturi NUMBER,total_client NUMBER(12, 2),categorie VARCHAR2(30));

FUNCTION rezumat_client(p_client_id IN clienti.client_id%TYPE) 
RETURN t_rezumat_client_rec;

FUNCTION status_factura(p_factura_id IN facturi.factura_id%TYPE) RETURN VARCHAR2;

PROCEDURE afiseaza_rezumat_client(p_client_id IN clienti.client_id%TYPE);
PROCEDURE corecteaza_si_auditeaza_factura(p_factura_id IN facturi.factura_id%TYPE);
PROCEDURE raport(p_client_id IN clienti.client_id%TYPE);
PROCEDURE raport(p_factura_id IN facturi.factura_id%TYPE,p_mod IN VARCHAR2);
END pkg_facturare;
/
SHOW ERRORS PACKAGE pkg_facturare;


CREATE OR REPLACE PACKAGE BODY pkg_facturare AS
g_prag_factura_mare CONSTANT NUMBER := 150;

FUNCTION exista_factura(p_factura_id IN facturi.factura_id%TYPE) 
RETURN BOOLEAN
IS
v_unu NUMBER;
BEGIN
SELECT 1
INTO v_unu
FROM facturi
WHERE factura_id = p_factura_id;
RETURN TRUE;

EXCEPTION
WHEN NO_DATA_FOUND THEN
RETURN FALSE;
END exista_factura;

FUNCTION rezumat_client(p_client_id IN clienti.client_id%TYPE) 
RETURN t_rezumat_client_rec
IS
v_rezumat t_rezumat_client_rec;
BEGIN
SELECT client_id, nume_client
INTO v_rezumat.client_id, v_rezumat.nume_client
FROM clienti
WHERE client_id = p_client_id;

SELECT COUNT(*)
INTO v_rezumat.nr_facturi
FROM facturi
WHERE client_id = p_client_id;
v_rezumat.total_client := f_total_client(p_client_id);
v_rezumat.categorie := f_categorie_client(p_client_id);
RETURN v_rezumat;

EXCEPTION
WHEN NO_DATA_FOUND THEN
RAISE_APPLICATION_ERROR(-20012, 'Clientul ' || p_client_id || ' nu exista.');
END rezumat_client;

FUNCTION status_factura(p_factura_id IN facturi.factura_id%TYPE) 
RETURN VARCHAR2
IS
v_total_stocat facturi.total_factura%TYPE;
v_total_calculat NUMBER(12, 2);
BEGIN
IF NOT exista_factura(p_factura_id) 
THEN RAISE_APPLICATION_ERROR(-20013, 'Factura ' || p_factura_id || ' nu exista.');
END IF;

SELECT total_factura
INTO v_total_stocat
FROM facturi
WHERE factura_id = p_factura_id;
v_total_calculat := f_total_factura(p_factura_id);
IF ABS(v_total_stocat - v_total_calculat) > 0.01 THEN
RETURN 'Necesita recalculare';
ELSIF v_total_stocat >= g_prag_factura_mare THEN
RETURN 'Factura mare';
ELSE
RETURN 'Factura obisnuita';
END IF;
END status_factura;

PROCEDURE afiseaza_rezumat_client(p_client_id IN clienti.client_id%TYPE) IS
v_rezumat t_rezumat_client_rec;
BEGIN
v_rezumat := rezumat_client(p_client_id);

DBMS_OUTPUT.PUT_LINE('--- REZUMAT CLIENT DIN PACHET ---');
DBMS_OUTPUT.PUT_LINE('Client: ' || v_rezumat.nume_client);
DBMS_OUTPUT.PUT_LINE('Nr. facturi: ' || v_rezumat.nr_facturi);
DBMS_OUTPUT.PUT_LINE('Total: ' || TO_CHAR(v_rezumat.total_client, 'FM9990D00'));
DBMS_OUTPUT.PUT_LINE('Categorie: ' || v_rezumat.categorie);
END afiseaza_rezumat_client;

PROCEDURE corecteaza_si_auditeaza_factura(p_factura_id IN facturi.factura_id%TYPE)
IS
BEGIN
DBMS_OUTPUT.PUT_LINE('Status initial factura ' || p_factura_id || ': ' || status_factura(p_factura_id));
p_recalculeaza_factura(p_factura_id);
DBMS_OUTPUT.PUT_LINE('Status dupa recalculare: ' || status_factura(p_factura_id));
p_audit_discount_factura(p_factura_id);
END corecteaza_si_auditeaza_factura;

PROCEDURE raport(p_client_id IN clienti.client_id%TYPE)
IS
BEGIN
p_raport_client(p_client_id);
END raport;

PROCEDURE raport(p_factura_id IN facturi.factura_id%TYPE, p_mod IN VARCHAR2)
IS
BEGIN
IF UPPER(p_mod) = 'DISCOUNT' THEN
p_audit_discount_factura(p_factura_id);
ELSE
DBMS_OUTPUT.PUT_LINE('Status factura: ' || status_factura(p_factura_id));
END IF;
END raport;

END pkg_facturare;
/
SHOW ERRORS PACKAGE BODY pkg_facturare;

--verificare
SELECT object_name, object_type, status
FROM user_objects
WHERE object_name = 'PKG_FACTURARE';

SET SERVEROUTPUT ON;
BEGIN
DBMS_OUTPUT.PUT_LINE('--- APEL 1: REZUMAT CLIENT ---');
pkg_facturare.afiseaza_rezumat_client(1);
DBMS_OUTPUT.PUT_LINE('--- APEL 2: STATUS FACTURA ---');
DBMS_OUTPUT.PUT_LINE('Status factura 106: ' || pkg_facturare.status_factura(106));
DBMS_OUTPUT.PUT_LINE('--- APEL 3: RAPORT CLIENT ---');
pkg_facturare.raport(1);
DBMS_OUTPUT.PUT_LINE('--- APEL 4: RAPORT FACTURA DISCOUNT ---');
pkg_facturare.raport(106, 'DISCOUNT');
END;
/


-- Pachetul 2: pkg_portofoliu
CREATE OR REPLACE PACKAGE pkg_portofoliu AS

TYPE t_produs_portofoliu_rec IS RECORD (produs_id produse.produs_id%TYPE,nume_produs produse.nume_produs%TYPE,pret_lista produse.pret_lista%TYPE,valoare_vanduta NUMBER(12, 2),clasa VARCHAR2(20));
TYPE t_produse_portofoliu_tab IS TABLE OF t_produs_portofoliu_rec INDEX BY PLS_INTEGER;

FUNCTION pret_mediu_categorie(p_categorie_id IN categorii_produse.categorie_id%TYPE) 
RETURN NUMBER;

FUNCTION clasa_produs(p_produs_id IN produse.produs_id%TYPE) 
RETURN VARCHAR2;

PROCEDURE afiseaza_furnizor(p_furnizor_id IN furnizori.furnizor_id%TYPE);

PROCEDURE ajusteaza_categorie(p_categorie_id IN categorii_produse.categorie_id%TYPE,p_procent IN NUMBER);
END pkg_portofoliu;
/
SHOW ERRORS PACKAGE pkg_portofoliu;


CREATE OR REPLACE PACKAGE BODY pkg_portofoliu AS
FUNCTION exista_categorie(p_categorie_id IN categorii_produse.categorie_id%TYPE) 
RETURN BOOLEAN IS
v_unu NUMBER;
BEGIN
SELECT 1
INTO v_unu
FROM categorii_produse
WHERE categorie_id = p_categorie_id;
RETURN TRUE;

EXCEPTION
WHEN NO_DATA_FOUND THEN
RETURN FALSE;
END exista_categorie;

FUNCTION pret_mediu_categorie(p_categorie_id IN categorii_produse.categorie_id%TYPE) 
RETURN NUMBER
IS
v_pret_mediu NUMBER(12, 2);
BEGIN
IF NOT exista_categorie(p_categorie_id) THEN
RAISE_APPLICATION_ERROR(-20014, 'Categoria ' || p_categorie_id || ' nu exista.');
END IF;

SELECT NVL(AVG(pret_lista), 0)
INTO v_pret_mediu
FROM produse
WHERE categorie_id = p_categorie_id;
RETURN ROUND(v_pret_mediu, 2);
END pret_mediu_categorie;

FUNCTION clasa_produs(p_produs_id IN produse.produs_id%TYPE) 
RETURN VARCHAR2
IS
v_pret produse.pret_lista%TYPE;
v_vanzari NUMBER(12, 2);
BEGIN
SELECT pret_lista
INTO v_pret
FROM produse
WHERE produs_id = p_produs_id;
v_vanzari := f_valoare_vanduta_produs(p_produs_id);
IF v_vanzari >= 150 THEN
RETURN 'vedeta';
ELSIF v_pret >= 50 THEN
RETURN 'premium';
ELSIF v_pret >= 15 THEN
RETURN 'standard';
ELSE
RETURN 'economic';
END IF;

EXCEPTION
WHEN NO_DATA_FOUND THEN
RAISE_APPLICATION_ERROR(-20015, 'Produsul ' || p_produs_id || ' nu exista.');
END clasa_produs;

PROCEDURE afiseaza_furnizor(p_furnizor_id IN furnizori.furnizor_id%TYPE)
IS
CURSOR c_produse IS
SELECT produs_id, nume_produs, pret_lista
FROM produse
WHERE furnizor_id = p_furnizor_id
ORDER BY nume_produs;
v_nume_furnizor furnizori.nume_furnizor%TYPE;
v_produse t_produse_portofoliu_tab;
v_index PLS_INTEGER := 0;
BEGIN
SELECT nume_furnizor
INTO v_nume_furnizor
FROM furnizori
WHERE furnizor_id = p_furnizor_id;

FOR r_produs IN c_produse LOOP
v_index := v_index + 1;
v_produse(v_index).produs_id := r_produs.produs_id;
v_produse(v_index).nume_produs := r_produs.nume_produs;
v_produse(v_index).pret_lista := r_produs.pret_lista;
v_produse(v_index).valoare_vanduta := f_valoare_vanduta_produs(r_produs.produs_id);
v_produse(v_index).clasa := clasa_produs(r_produs.produs_id);
END LOOP;

DBMS_OUTPUT.PUT_LINE('--- PORTOFOLIU FURNIZOR DIN PACHET ---');
DBMS_OUTPUT.PUT_LINE('Furnizor: ' || v_nume_furnizor);
DBMS_OUTPUT.PUT_LINE('Produs vedeta: ' || f_produs_vedeta_furnizor(p_furnizor_id));

IF v_produse.COUNT = 0 
THEN
DBMS_OUTPUT.PUT_LINE('Furnizorul nu are produse.');
ELSE
FOR i IN 1 .. v_produse.COUNT LOOP
DBMS_OUTPUT.PUT_LINE(v_produse(i).nume_produs || ' | pret: ' || TO_CHAR(v_produse(i).pret_lista, 'FM9990D00') || ' | vanzari: ' || TO_CHAR(v_produse(i).valoare_vanduta, 'FM9990D00') || ' | clasa: ' || v_produse(i).clasa);
END LOOP;
END IF;

EXCEPTION
WHEN NO_DATA_FOUND THEN
RAISE_APPLICATION_ERROR(-20016, 'Furnizorul ' || p_furnizor_id || ' nu exista.');
END afiseaza_furnizor;

PROCEDURE ajusteaza_categorie(p_categorie_id IN categorii_produse.categorie_id%TYPE,p_procent IN NUMBER)
IS
v_nr_modificate NUMBER;
BEGIN
p_ajustare_preturi_categorie(p_categorie_id, p_procent, v_nr_modificate);
DBMS_OUTPUT.PUT_LINE('Pachetul a modificat ' || v_nr_modificate || ' produse.');
END ajusteaza_categorie;
END pkg_portofoliu;
/
SHOW ERRORS PACKAGE BODY pkg_portofoliu;

-- apel 1
SET SERVEROUTPUT ON;
BEGIN
DBMS_OUTPUT.PUT_LINE('--- APEL 1: PRET MEDIU CATEGORIE ---');
DBMS_OUTPUT.PUT_LINE('Pret mediu categoria 2: ' || TO_CHAR(pkg_portofoliu.pret_mediu_categorie(2), 'FM9990D00'));
DBMS_OUTPUT.PUT_LINE('--- APEL 2: CLASA PRODUS ---');
DBMS_OUTPUT.PUT_LINE('Clasa produsului 2: ' || pkg_portofoliu.clasa_produs(2));
DBMS_OUTPUT.PUT_LINE('--- APEL 3: AFISARE FURNIZOR ---');
pkg_portofoliu.afiseaza_furnizor(1);
END;
/

-- apel 2
DECLARE
v_pret_mediu_dupa NUMBER(12, 2);
BEGIN
SAVEPOINT sp_pkg_portofoliu;
pkg_portofoliu.ajusteaza_categorie(2, 5);
v_pret_mediu_dupa := pkg_portofoliu.pret_mediu_categorie(2);
DBMS_OUTPUT.PUT_LINE('Pret mediu dupa ajustare demo: ' || TO_CHAR(v_pret_mediu_dupa, 'FM9990D00'));
ROLLBACK TO SAVEPOINT sp_pkg_portofoliu;
DBMS_OUTPUT.PUT_LINE('Modificarile au fost anulate.');
END;
/






SET SERVEROUTPUT ON;

-- Exercițiul 1 – Identificarea unui client și afișarea adresei
DECLARE
    v_client_id   CLIENTI.client_id%TYPE;
    v_nume        CLIENTI.nume_client%TYPE;
    v_adresa      CLIENTI.adresa%TYPE;
BEGIN
    SELECT client_id, nume_client, adresa
    INTO v_client_id, v_nume, v_adresa
    FROM CLIENTI
    WHERE nume_client = 'Budeș Cristian';

    IF SQL%FOUND THEN
        DBMS_OUTPUT.PUT_LINE('Client găsit: ' || v_client_id || ' | ' || v_nume || ' | ' || v_adresa);
    END IF;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('Nu există clientul Budeș Cristian în tabelul CLIENTI.');
    WHEN TOO_MANY_ROWS THEN
        DBMS_OUTPUT.PUT_LINE('Există mai mulți clienți cu același nume.');
END;
/

-- Exercițiul 2 – Raportul facturilor pentru un client dat
DECLARE
    CURSOR c_facturi (p_client_id CLIENTI.client_id%TYPE) IS
        SELECT factura_id, data_facturii, total_factura
        FROM FACTURI
        WHERE client_id = p_client_id
        ORDER BY data_facturii;

    v_nr_facturi NUMBER := 0;
    v_total      NUMBER(12,2) := 0;
BEGIN
    FOR rec IN c_facturi(1) LOOP
        v_nr_facturi := v_nr_facturi + 1;
        v_total := v_total + rec.total_factura;

        DBMS_OUTPUT.PUT_LINE('Factura ' || rec.factura_id ||
                             ' | Data: ' || TO_CHAR(rec.data_facturii, 'DD.MM.YYYY') ||
                             ' | Total: ' || rec.total_factura);
    END LOOP;

    IF v_nr_facturi = 0 THEN
        DBMS_OUTPUT.PUT_LINE('Clientul nu are facturi.');
    ELSIF v_total >= 150 THEN
        DBMS_OUTPUT.PUT_LINE('Client important. Total cumpărături = ' || v_total);
    ELSE
        DBMS_OUTPUT.PUT_LINE('Client standard. Total cumpărături = ' || v_total);
    END IF;
END;
/

-- Exercițiul 3 – Majorarea controlată a prețurilor pentru anumite categorii
DECLARE
    CURSOR c_preturi IS
        SELECT p.produs_id, p.nume_produs, p.pret_lista, c.denumire_categorie
        FROM PRODUSE p
        JOIN CATEGORII_PRODUSE c ON c.categorie_id = p.categorie_id
        WHERE p.categorie_id IN (8, 9)
        FOR UPDATE OF p.pret_lista;

    v_coeficient NUMBER(4,2);
    v_modificate NUMBER := 0;
BEGIN
    FOR rec IN c_preturi LOOP
        IF rec.pret_lista < 20 THEN
            v_coeficient := 1.10;
        ELSE
            v_coeficient := 1.05;
        END IF;

        UPDATE PRODUSE
        SET pret_lista = ROUND(rec.pret_lista * v_coeficient, 2)
        WHERE CURRENT OF c_preturi;

        IF SQL%ROWCOUNT = 1 THEN
            v_modificate := v_modificate + 1;
            DBMS_OUTPUT.PUT_LINE('Produs actualizat: ' || rec.nume_produs ||
                                 ' | categorie: ' || rec.denumire_categorie ||
                                 ' | preț vechi: ' || rec.pret_lista ||
                                 ' | preț nou: ' || ROUND(rec.pret_lista * v_coeficient, 2));
        END IF;
    END LOOP;

    DBMS_OUTPUT.PUT_LINE('Număr produse modificate: ' || v_modificate);
    ROLLBACK;
END;
/

-- Exercițiul 4 – Inserarea condiționată a unui client nou
DECLARE
    v_exista    NUMBER;
    v_client_id NUMBER;
BEGIN
    SELECT COUNT(*)
    INTO v_exista
    FROM CLIENTI
    WHERE UPPER(nume_client) = UPPER('Test Client PL_SQL');

    IF v_exista = 0 THEN
        v_client_id := SEQ_CLIENTI.NEXTVAL;

        INSERT INTO CLIENTI (client_id, nume_client, cif, adresa)
        VALUES (v_client_id, 'Test Client PL_SQL', 'ROPLSQL01', 'Constanta, Str. Test 1');

        IF SQL%ROWCOUNT = 1 THEN
            DBMS_OUTPUT.PUT_LINE('Client inserat cu succes. ID nou = ' || v_client_id);
        END IF;
    ELSE
        DBMS_OUTPUT.PUT_LINE('Clientul există deja și nu va fi reinserat.');
    END IF;

    ROLLBACK;
END;
/

-- Exercițiul 5 – Verificarea și corectarea totalului facturilor
DECLARE
    CURSOR c_verificare IS
        SELECT f.factura_id,
               f.total_factura AS total_antet,
               NVL(SUM(d.cantitate * d.pret_vanzare), 0) AS total_detalii
        FROM FACTURI f
        LEFT JOIN DETALII_FACTURA d ON d.factura_id = f.factura_id
        GROUP BY f.factura_id, f.total_factura
        ORDER BY f.factura_id;

    v_corectate NUMBER := 0;
BEGIN
    FOR rec IN c_verificare LOOP
        IF rec.total_antet <> rec.total_detalii THEN
            UPDATE FACTURI
            SET total_factura = rec.total_detalii
            WHERE factura_id = rec.factura_id;

            IF SQL%ROWCOUNT = 1 THEN
                v_corectate := v_corectate + 1;
                DBMS_OUTPUT.PUT_LINE('Factura ' || rec.factura_id ||
                                     ' corectată de la ' || rec.total_antet ||
                                     ' la ' || rec.total_detalii);
            END IF;
        ELSE
            DBMS_OUTPUT.PUT_LINE('Factura ' || rec.factura_id || ' este corectă.');
        END IF;
    END LOOP;

    DBMS_OUTPUT.PUT_LINE('Număr facturi corectate: ' || v_corectate);
    ROLLBACK;
END;
/

-- Exercițiul 6 – Clasificarea angajaților după performanța la vânzare
DECLARE
    CURSOR c_angajati IS
        SELECT a.angajat_id,
               a.nume || ' ' || a.prenume AS nume_angajat,
               COUNT(f.factura_id) AS nr_facturi,
               NVL(SUM(f.total_factura), 0) AS total_vanzari
        FROM ANGAJATI a
        LEFT JOIN FACTURI f ON f.angajat_id = a.angajat_id
        GROUP BY a.angajat_id, a.nume, a.prenume
        ORDER BY total_vanzari DESC;

    v_clasa VARCHAR2(20);
BEGIN
    FOR rec IN c_angajati LOOP
        CASE
            WHEN rec.total_vanzari >= 180 THEN v_clasa := 'Excelent';
            WHEN rec.total_vanzari >= 80 THEN v_clasa := 'Bun';
            WHEN rec.total_vanzari > 0 THEN v_clasa := 'Mediu';
            ELSE v_clasa := 'Fără vânzări';
        END CASE;

        DBMS_OUTPUT.PUT_LINE(RPAD(rec.nume_angajat, 25) ||
                             ' | facturi: ' || rec.nr_facturi ||
                             ' | total: ' || rec.total_vanzari ||
                             ' | clasă: ' || v_clasa);
    END LOOP;
END;
/

-- Exercițiul 7 – Listarea produselor unui furnizor și calculul mediei de preț
DECLARE
    v_furnizor_id FURNIZORI.furnizor_id%TYPE;
    v_nr_produse  NUMBER := 0;
    v_suma_pret   NUMBER(12,2) := 0;

    CURSOR c_produse_furnizor (p_furnizor_id FURNIZORI.furnizor_id%TYPE) IS
        SELECT produs_id, nume_produs, pret_lista
        FROM PRODUSE
        WHERE furnizor_id = p_furnizor_id
        ORDER BY pret_lista DESC;
BEGIN
    SELECT furnizor_id
    INTO v_furnizor_id
    FROM FURNIZORI
    WHERE nume_furnizor = 'Lactate SA';

    FOR rec IN c_produse_furnizor(v_furnizor_id) LOOP
        v_nr_produse := v_nr_produse + 1;
        v_suma_pret := v_suma_pret + rec.pret_lista;

        DBMS_OUTPUT.PUT_LINE('Produs: ' || rec.nume_produs || ' | preț: ' || rec.pret_lista);
    END LOOP;

    IF v_nr_produse > 0 THEN
        DBMS_OUTPUT.PUT_LINE('Preț mediu furnizor = ' || ROUND(v_suma_pret / v_nr_produse, 2));
    ELSE
        DBMS_OUTPUT.PUT_LINE('Furnizorul nu are produse asociate.');
    END IF;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('Furnizorul cerut nu există în tabelul FURNIZORI.');
END;
/

-- Exercițiul 8 – Parcurgerea ierarhiei de categorii și numărarea produselor
DECLARE
    CURSOR c_categorii IS
        SELECT LEVEL AS nivel,
               categorie_id,
               denumire_categorie,
               SYS_CONNECT_BY_PATH(denumire_categorie, ' -> ') AS cale
        FROM CATEGORII_PRODUSE
        START WITH parent_id IS NULL
        CONNECT BY PRIOR categorie_id = parent_id
        ORDER SIBLINGS BY denumire_categorie;

    v_nr_produse NUMBER;
BEGIN
    FOR rec IN c_categorii LOOP
        SELECT COUNT(*)
        INTO v_nr_produse
        FROM PRODUSE
        WHERE categorie_id = rec.categorie_id;

        IF v_nr_produse = 0 THEN
            DBMS_OUTPUT.PUT_LINE(LPAD(' ', rec.nivel * 2) || rec.denumire_categorie ||
                                 ' | fără produse directe');
        ELSE
            DBMS_OUTPUT.PUT_LINE(LPAD(' ', rec.nivel * 2) || rec.denumire_categorie ||
                                 ' | nr. produse directe = ' || v_nr_produse);
        END IF;
    END LOOP;
END;
/

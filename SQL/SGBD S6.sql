SET SERVEROUTPUT ON;

--1
ACCEPT p_an PROMPT 'Introduceti anul: '

DECLARE
    v_an            NUMBER := &p_an;
    v_nume_complet  VARCHAR2(100);
    v_functie       VARCHAR2(100);
    v_data_ang      DATE;
BEGIN
    SELECT a.nume || ' ' || a.prenume,
           f.denumire_functie,
           a.data_angajare
    INTO   v_nume_complet,
           v_functie,
           v_data_ang
    FROM   angajati a
           JOIN functii f ON a.id_functie = f.id_functie
    WHERE  EXTRACT(YEAR FROM a.data_angajare) = v_an;

    DBMS_OUTPUT.PUT_LINE('Angajat: ' || v_nume_complet);
    DBMS_OUTPUT.PUT_LINE('Functia: ' || v_functie);
    DBMS_OUTPUT.PUT_LINE('Data angajarii: ' || TO_CHAR(v_data_ang, 'DD-MM-YYYY'));

EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('In anul ' || v_an || ' nu a fost angajat personal nou');

    WHEN TOO_MANY_ROWS THEN
        DBMS_OUTPUT.PUT_LINE('In anul ' || v_an || ' au fost angajate multiple persoane');

        FOR r IN (
            SELECT a.nume || ' ' || a.prenume AS nume_complet,
                   f.denumire_functie,
                   a.data_angajare
            FROM   angajati a
                   JOIN functii f ON a.id_functie = f.id_functie
            WHERE  EXTRACT(YEAR FROM a.data_angajare) = v_an
            ORDER BY a.data_angajare
        ) LOOP
            DBMS_OUTPUT.PUT_LINE(
                r.nume_complet || ' | ' ||
                r.denumire_functie || ' | ' ||
                TO_CHAR(r.data_angajare, 'DD-MM-YYYY')
            );
        END LOOP;

    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('A aparut o alta problema');
END;
/

--2
DECLARE
    e_dept_fara_denumire EXCEPTION;
    PRAGMA EXCEPTION_INIT(e_dept_fara_denumire, -2290);
BEGIN
    INSERT INTO departamente (id_departament)
    VALUES (300);

    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Departamentul a fost adaugat.');
EXCEPTION
    WHEN e_dept_fara_denumire THEN
        DBMS_OUTPUT.PUT_LINE('Eroare: nu se poate adauga departamentul 300 fara denumire.');
        DBMS_OUTPUT.PUT_LINE('Mesaj Oracle: ' || SQLERRM);

    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('A aparut o alta problema.');
        DBMS_OUTPUT.PUT_LINE('Mesaj Oracle: ' || SQLERRM);
END;
/

--3
ACCEPT p_id_angajat PROMPT 'Introduceti id-ul angajatului: '

DECLARE
    v_id_angajat   NUMBER := &p_id_angajat;
    v_nume         VARCHAR2(100);
BEGIN
    UPDATE angajati
    SET    salariul = salariul * 1.30
    WHERE  id_angajat = v_id_angajat;

    IF SQL%ROWCOUNT = 0 THEN
        RAISE NO_DATA_FOUND;
    END IF;

    SELECT nume || ' ' || prenume
    INTO   v_nume
    FROM   angajati
    WHERE  id_angajat = v_id_angajat;

    COMMIT;

    DBMS_OUTPUT.PUT_LINE('Salariul angajatului ' || v_nume || ' a fost marit cu 30%.');

EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('Nu exista niciun angajat cu id-ul ' || v_id_angajat || '.');

    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('A aparut o alta problema.');
END;
/

--4
ACCEPT p_id_emp PROMPT 'Introduceti id-ul angajatului: '

DECLARE
    v_id_angajat     NUMBER := &p_id_emp;
    v_nume_complet   VARCHAR2(100);
    v_nr_comenzi     NUMBER;
    e_fara_comenzi   EXCEPTION;
BEGIN
    SELECT nume || ' ' || prenume
    INTO   v_nume_complet
    FROM   angajati
    WHERE  id_angajat = v_id_angajat;

    SELECT COUNT(*)
    INTO   v_nr_comenzi
    FROM   comenzi
    WHERE  id_angajat = v_id_angajat;

    DBMS_OUTPUT.PUT_LINE('Angajat: ' || v_nume_complet);

    IF v_nr_comenzi = 0 THEN
        RAISE e_fara_comenzi;
    ELSE
        DBMS_OUTPUT.PUT_LINE('Numar comenzi gestionate: ' || v_nr_comenzi);
    END IF;

EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('Angajatul cu id-ul ' || v_id_angajat || ' nu exista.');

    WHEN e_fara_comenzi THEN
        DBMS_OUTPUT.PUT_LINE('Angajatul exista, dar nu s-a ocupat de nicio comanda.');

    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('A aparut o alta problema.');
END;
/
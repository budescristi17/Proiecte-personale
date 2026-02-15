#include <iostream>

using namespace std;

void valueParameter(int a)
{
    cout << "Subprogram - valoare inainte de modificare: " << a << "; ";
    cout << "adresa parametrului a: " << &a << endl;
    a = a * 2;
    cout << "Subprogram - valoare dupa modificare: " << a << "; ";
    cout << "adresa parametrului a: " << &a << endl;
}

void referenceParameter(int& a)
{
    cout << "Subprogram - valoare inainte de modificare: " << a << "; ";
    cout << "adresa parametrului a: " << &a << endl;
    a = a * 2;
    cout << "Subprogram - valoare dupa modificare: " << a << "; ";
    cout << "adresa parametrului a: " << &a << endl;
}

void pointerParameter(int* a)
{
    cout << "Subprogram - valoare inainte de modificare: " << *a << "; ";
    cout << "adresa pointerului a: " << &a << "; ";
    cout << "adresa retinuta in pointerul a: "<< a << endl;
    *a = *a * 2;
    cout << "Subprogram - valoare dupa modificare: " << *a << "; ";
    cout << "adresa pointerului a: " << &a << "; ";
    cout << "adresa retinuta in pointerul a: " << a << endl;
}

int main()
{    
    cout << "SUBPROGRAM CU PARAMETRU TRANSMIS PRIN VALOARE" << endl;
    cout << "=============================================" << endl;
    int x = 7;
    cout << "Main - valoare inainte de apel: " << x << "; ";
    cout << "adresa variabilei x: " << &x << endl;
    valueParameter(x);
    cout << "Main - valoare dupa apel: " << x << "; ";
    cout << "adresa variabilei x: " << &x << endl;
    cout << "=============================================" << endl << endl;

    cout << "SUBPROGRAM CU PARAMETRU TRANSMIS PRIN REFERINTA" << endl;
    cout << "===============================================" << endl;
    int y = 7;
    cout << "Main - valoare inainte de apel: " << y << "; ";
    cout << "adresa variabilei y: " << &y << endl;
    referenceParameter(y);
    cout << "Main - valoare dupa apel: " << y << "; ";
    cout << "adresa variabilei y: " << &y << endl;
    cout << "===============================================" << endl << endl;

    cout << "SUBPROGRAM CU PARAMETRU TRANSMIS PRIN POINTER" << endl;
    cout << "=============================================" << endl;
    int z = 7;
    int* p = &z;
    cout << "Main - valoare inainte de apel: " << *p << "; ";
    cout << "adresa pointerului p: " << &p << "; ";
    cout << "adresa retinuta in pointerul p: " << p << endl;
    pointerParameter(p);
    cout << "Main - valoare dupa apel: " << *p << "; ";
    cout << "adresa pointerului p: " << &p << "; ";
    cout << "adresa retinuta in pointerul p: " << p << endl;
    cout << "=============================================" << endl;

}

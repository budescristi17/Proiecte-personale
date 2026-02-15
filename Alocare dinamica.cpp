#include <iostream>

using namespace std;

int main()
{
    int lungime;
    cout << "Dati lungimea: "; cin >> lungime;
    int *v = new int[lungime];

    for (int i = 0; i < lungime; i++)
    {
        v[i] = i;
    }

    cout << "Adresa din stack la care se afla alocat pointerul v: " << &v << endl;
    cout << "Adresa din heap spre care indica pointerul v: " << v << endl;
    cout << "Elementele vectorului spre care indica pointerul v: ";
    for (int i = 0; i < lungime; i++)
    {
        cout << v[i] << " ";
    }

    cout << endl << endl;
    
    int *temp = new int[lungime + 1];
    for (int i = 0; i < lungime; i++)
    {
        temp[i] = v[i];
    }
    temp[lungime] = lungime;
    delete[] v;
    v = temp;
    lungime++;

    cout << "Adresa din stack la care se afla alocat pointerul v: " << &v << endl; //observam ca nu s-a schimbat
    cout << "Adresa din heap spre care indica pointerul v: " << v << endl; //observam ca s-a schimbat
    cout << "Elementele vectorului spre care indica pointerul v: ";
    for (int i = 0; i < lungime; i++)
    {
        cout << v[i] << " ";
    }   
}

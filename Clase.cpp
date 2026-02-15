#include <iostream>

using namespace std;

class Animal
{
public:
    char nume[50];
    char specie[50];
    unsigned short varsta;
    bool areMicrocip;

    int calculeazaPretConsultatie()
    {
        int pret = 0;
        if (strcmp(specie, "pisica") == 0)
        {
            pret = 50;
        }
        else
        {
            if (strcmp(specie, "caine") == 0)
            {
                pret = 100;
            }
            else
            {
                pret = 200;
            }
        }

        if (areMicrocip == true)
        {
            pret = pret * 0.9;
        }

        return pret;
    }
};

int main()
{
    char a = 'a';
    char* c = &a;
    cout << c << " " << *c << " " << &c << endl;
    *c = 'b';
    cout << c << " " << *c << " " << &c << endl;

    /*Animal pufi;
    strcpy_s(pufi.nume, 5, "Pufi");
    strcpy_s(pufi.specie, 6, "caine");
    pufi.varsta = 7;
    pufi.areMicrocip = true;

    cout << "Pentru " << pufi.nume << ", consultatia costa " << pufi.calculeazaPretConsultatie() << endl;

    Animal missy;
    strcpy_s(missy.nume, 6, "Missy");
    strcpy_s(missy.specie, 7, "pisica");
    missy.varsta = 2;
    pufi.areMicrocip = false;

    cout << "Pentru " << missy.nume << ", consultatia costa " << missy.calculeazaPretConsultatie() << endl;

    Animal luther;
    strcpy_s(luther.nume, 7, "Luther");
    strcpy_s(luther.specie, 8, "hamster");
    missy.varsta = 1;
    pufi.areMicrocip = false;

    cout << "Pentru " << luther.nume << ", consultatia costa " << luther.calculeazaPretConsultatie() << endl;*/

}
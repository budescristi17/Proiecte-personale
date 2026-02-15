#include <iostream>
#include <vector>

using namespace std;

class Elev {
private:
    string nume;
    int varsta;

public:
    Elev(string nume, int varsta) : nume(nume), varsta(varsta) {}

    friend ostream& operator<<(ostream& os, const Elev& e) {
        os << "Nume: " << e.nume << endl;
        os << "Varsta: " << e.varsta << endl;
        return os;
    }
};

class Masina {
private:
    string marca;
    string model;

public:
    Masina(string marca, string model) : marca(marca), model(model) {}

    friend ostream& operator<<(ostream& os, const Masina& m) {
        os << "Marca: " << m.marca << endl;
        os << "Model: " << m.model << endl;
        return os;
    }
};

template <typename T>
class Catalog {
private:
    vector<T*> elemente;

public:
    void Adauga(T* e) {
        this->elemente.push_back(e);
    }

    void Sterge(T* e) {
        for (auto it = this->elemente.begin(); it != this->elemente.end(); it++)
        {
            if (*it == e) {
                this->elemente.erase(it);
                return;
            }
        }
    }

    friend ostream& operator<<(ostream& os, const Catalog<T>& catalog) {
        for (auto it = catalog.elemente.begin(); it != catalog.elemente.end(); it++)
        {
            os << **it << endl;
        }
        return os;
    }
};

int main()
{
    Elev* e1 = new Elev("Ion", 20);
    Elev* e2 = new Elev("Maria", 19);
    Elev* e3 = new Elev("Gigel", 21);

    Catalog<Elev> catalogElevi;
    catalogElevi.Adauga(e1);
    catalogElevi.Adauga(e2);
    catalogElevi.Adauga(e3);

    cout << "Catalog elevi complet:" << endl;
    cout << catalogElevi;

    cout << "Catalog dupa stergerea Mariei:" << endl;
    catalogElevi.Sterge(e2);
    cout << catalogElevi;

    cout << endl << "===========================" << endl << endl;

    Masina* m1 = new Masina("BMW", "X5");
    Masina* m2 = new Masina("Audi", "A4");
    Masina* m3 = new Masina("Dacia", "Logan");

    Catalog<Masina> catalogMasini;
    catalogMasini.Adauga(m1);
    catalogMasini.Adauga(m2);
    catalogMasini.Adauga(m3);

    cout << "Catalog masini complet: " << endl;
    cout << catalogMasini;

    cout << "Catalog dupa stergerea BMW-ului: " << endl;
    catalogMasini.Sterge(m1);
    cout << catalogMasini;

    delete e1;
    delete e2;
    delete e3;

    delete m1;
    delete m2;
    delete m3;

    return 0;
}

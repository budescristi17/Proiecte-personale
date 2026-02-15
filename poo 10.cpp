#include <iostream>

using namespace std;

class Companie {
private:
    char* nume;
    char* tara;
    int anFiscal;
    double venituri;

public:
    Companie() : nume(nullptr), tara(nullptr), anFiscal(0), venituri(0) {}

    Companie(const Companie& other) {
        anFiscal = other.anFiscal;
        venituri = other.venituri;

        if (other.nume) {
            nume = new char[strlen(other.nume) + 1];
            strcpy_s(nume, strlen(other.nume) + 1, other.nume);
        }
        else {
            nume = nullptr;
        }

        if (other.tara) {
            tara = new char[strlen(other.tara) + 1];
            strcpy_s(tara, strlen(other.tara) + 1, other.tara);
        }
        else {
            tara = nullptr;
        }
    }

    // Copy assignment operator (Rule of Three)
    Companie& operator=(const Companie& other) {
        if (this != &other) {
            delete[] nume;

            anFiscal = other.anFiscal;
            venituri = other.venituri;

            if (other.nume) {
                nume = new char[strlen(other.nume) + 1];
                strcpy_s(nume, strlen(other.nume) + 1, other.nume);
            }
            else {
                nume = nullptr;
            }
            if (other.tara) {
                tara = new char[strlen(other.tara) + 1];
                strcpy_s(tara, strlen(other.tara) + 1, other.tara);
            }
            else {
                tara = nullptr;
            }
        }
        return *this;
    }

    // Destructor (Rule of Three)
    ~Companie() {
        delete[] nume;
        delete[] tara;
    }

    // OUTPUT <<
    friend ostream& operator<<(ostream& os, const Companie& c) {
        os << "Companie: " << (c.nume ? c.nume : "N/A") << endl;
		os << "Tara: " << (c.tara ? c.tara : "N/A") << endl;
        os << "An fiscal: " << c.anFiscal << endl;
        os << "Venituri: " << c.venituri << endl;
        return os;
    }

    friend istream& operator>>(istream& is, Companie& c) {
        char buffer[256];

		cout << "Numele companiei: ";
		// Pentru a citi un sir cu spatii folosim getline, nu operatorul >> 
		// (acesta se opreste la primul spatiu). In cazul folosirii lui >>, al doilea cuvant
		// va ramane in buffer-ul de intrare si va fi citit la urmatoarea operatie de citire.
		// Incercati sa inlocuiti getline cu operatorul >> si vedeti diferenta pentru compania cu numele
        // "Tata Motors"
        
        //is >> buffer;
        is.getline(buffer, 256);

        delete[] c.nume;
        c.nume = new char[strlen(buffer) + 1];
        strcpy_s(c.nume, strlen(buffer) + 1, buffer);

		cout << "Tara: ";
        is.getline(buffer, 256);

        delete[] c.tara;
        c.tara = new char[strlen(buffer) + 1];
		strcpy_s(c.tara, strlen(buffer) + 1, buffer);

		cout << "Anul fiscal: ";
        is >> c.anFiscal;
        
        cout << "Venituri: ";
        is >> c.venituri;        

        return is;
    }
};

int main() {
    Companie c;
        
    cin >> c;

	cout << "--- Afisare companie ---" <<endl;
    cout << c << endl;

    Companie copy = c;
    cout << "--- Copiere companie ---" << endl;
    cout << copy << endl;

	Companie assigned;
    assigned = c;
    cout << "--- Atribuire companie ---" << endl;
    cout << assigned << endl;
	return 0;
}

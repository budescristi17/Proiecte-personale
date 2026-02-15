#include <iostream>

using namespace std;

class CarnetDeNote {
private:
    int* note;
    int lungime;

public:
    CarnetDeNote() {
        this->lungime = 0;
        this->note = new int[0];
    }

    ~CarnetDeNote() {
        delete[] this->note;
    }

    CarnetDeNote(const CarnetDeNote& other) {
        this->lungime = other.lungime;
        this->note = new int[this->lungime];
        for (int i = 0; i < this->lungime; i++) {
            this->note[i] = other.note[i];
        }
	}

    CarnetDeNote& operator=(const CarnetDeNote& other) {
        if (this != &other) {
            delete[] this->note;
            this->lungime = other.lungime;
            this->note = new int[this->lungime];
            for (int i = 0; i < this->lungime; i++) {
                this->note[i] = other.note[i];
            }
        }
        return *this;
    }

    int& operator[](int index) {
        if (index < 0 || index >= this->lungime)
            throw out_of_range("Indexul este invalid");

        return this->note[index];
    }

    void adaugaNota(int nota) {
        int* temp = new int[this->lungime + 1];
        for (int i = 0; i < this->lungime; i++) {
            temp[i] = this->note[i];
        }
        temp[this->lungime] = nota;
        delete[] this->note;
        this->note = temp;
        this->lungime++;
	}    

    friend ostream& operator<<(ostream& out, const CarnetDeNote& carnet) {
        for (int i = 0; i < carnet.lungime; i++) {
            out << carnet.note[i] << " ";
        }
        return out;
	}
};

int main() {
    CarnetDeNote carnet;
    carnet.adaugaNota(9);
    carnet.adaugaNota(10);
    carnet.adaugaNota(8);
    cout << "Carnetul de note: " << carnet << endl;
    try {
        cout << "Nota la indexul 1: " << carnet[1] << endl;
        carnet[1] = 7; // Modificam nota de la indexul 1
        cout << "Carnetul de note dupa modificare: " << carnet << endl;
        cout << "Incercare de accesare a unui index invalid:" << endl;
        cout << carnet[5] << endl; // Aceasta linie va arunca o exceptie
    } catch (const out_of_range& e) {
        cerr << e.what() << endl;
    }
    return 0;
}
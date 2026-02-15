#include <iostream>

using namespace std;

class ContBancar
{
private:
    double sold;

public:
    ContBancar(double soldInitial)
    {
		this->sold = soldInitial;
    }

    ContBancar& operator+=(double suma) {
        sold += suma;
        return *this;
    }

    ContBancar& operator-=(double suma) {
        sold -= suma;
        return *this;
    }

    ContBancar& operator*=(double factor) {
        sold *= factor;
        return *this;
    }

    ContBancar& operator/=(double divizor) {
        if (divizor == 0) {
            cerr << "Eroare: Impartire la zero!" << endl;
            return *this;
		}
        sold /= divizor;
        return *this;
    }

    friend ostream& operator<<(ostream& os, const ContBancar& cont) {
        os << "Fonduri curente: " << cont.sold << endl;
        return os;
	}
};

int main() {
    ContBancar cont(1000);

    cout << "Sold initial" << endl <<cont <<endl;
    
    cont += 200;   // depozit
	cout << "Dupa depozit: " << endl << cont <<endl;

    cont -= 150;   // retragere
	cout << "Dupa retragere: " << endl << cont << endl;

    cont *= 1.1;   // calcul dobanda
	cout << "Dupa calcul dobanda: " << endl << cont << endl;

    cont /= 5.05;     // conversie valutara
	cout << "Dupa conversie valutara: " << endl << cont << endl;
}

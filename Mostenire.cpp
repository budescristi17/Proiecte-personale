#include <iostream>

using namespace std;

class Bilet {
private:
    static int counter;
protected:
    const int id;
    string beneficiar;

public:
    Bilet() : id(counter++) {}

    Bilet(string beneficiar) : id(counter++)
    {
		this->beneficiar = beneficiar;
    }

    Bilet(const Bilet& b) : id(counter++) {
        this->beneficiar = b.beneficiar;
	}

    friend ostream& operator<<(ostream& os, const Bilet& t) {
        os << "ID: " << t.id << endl;
        os << "Beneficiar: " << t.beneficiar << endl;
        return os;
    }
};

class BiletUrban : public Bilet {
protected:
    string oras;
public:
    BiletUrban() : Bilet() {}

    BiletUrban(string beneficiar, string oras)
        : Bilet(beneficiar) {
		this->oras = oras;
    }

    BiletUrban(const BiletUrban& b) : Bilet(b) {
        this->oras = b.oras;
	}

    void validate() {
        cout << "Biletul urban este valid in " << oras << endl;
    }

    friend ostream& operator<<(ostream& os, const BiletUrban& b) {
        os << (const Bilet&)b;
        os << "Oras: " << b.oras << endl;
        return os;
    }
};

class BiletMetrou : public BiletUrban {
protected:
    string tip;
public:
    BiletMetrou() : BiletUrban() {}

    BiletMetrou(string beneficiar, string oras, string tip)
        : BiletUrban(beneficiar, oras)
    {
        this->tip = tip;
    }

    BiletMetrou(const BiletMetrou& b) : BiletUrban(b) {
        this->tip = b.tip;
    }

    void validate() {
		BiletUrban::validate();
        cout << "Biletul de metrou este de tipul: " << this->tip << endl;
    }

    friend ostream& operator<<(ostream& os, const BiletMetrou& b) {
        os << (const BiletUrban&)b;
        os << "Tip metrou: " << b.tip << endl;
        return os;
    }
};

class BiletSTB : public BiletUrban {
protected:
    int statii;
public:
	BiletSTB() : BiletUrban() {}

    BiletSTB(string beneficiar, string oras, int statii)
        : BiletUrban(beneficiar, oras)
    {
        this->statii = statii;
    }

    BiletSTB(const BiletSTB& b) : BiletUrban(b) {
        this->statii = b.statii;
    }

    void validate() {
        BiletUrban::validate();
        cout << "Biletul de tramvai este valabil " << this->statii << " statii" << endl;
    }

    friend ostream& operator<<(ostream& os, const BiletSTB& b) {
        os << (const BiletUrban&)b;
        os << "Statii: " << b.statii << endl;
        return os;
    }
};

class BiletUnic : public BiletSTB, public BiletMetrou {
protected:
    int durata;
public:
	BiletUnic() : BiletSTB(), BiletMetrou() {}

    BiletUnic(string beneficiar, string oras, int statii, string tip, int durata)
        : BiletSTB(beneficiar, oras, statii), BiletMetrou(beneficiar, oras, tip)
    {
        this->durata = durata;
    }

    BiletUnic(const BiletUnic& b)
        : BiletSTB(b), BiletMetrou(b)
    {
        this->durata = b.durata;
	}

    void validate() {
        BiletSTB::validate();
        BiletMetrou::validate();
        cout << "Biletul unic este valabil pentru " << durata << " minute" << endl;
    }

    friend ostream& operator<<(ostream& os, const BiletUnic& b) {
        os << (const BiletSTB&)b;
        os << "Tip bilet de metrou: " << b.tip << endl;
        os << "Durata: " << b.durata << " minute" << endl;
        return os;
    }
};

class BiletTren : public Bilet {
protected:
    string ruta;
public:
	BiletTren() : Bilet() {}

    BiletTren(string beneficiar, string ruta)
        : Bilet(beneficiar)
    {
		this->ruta = ruta;
    }

    BiletTren(const BiletTren& t) : Bilet(t) {
        this->ruta = t.ruta;
    }

    void validate() {
        cout << "Biletul de tren este valabil pe ruta " << this->ruta << endl;
    }

    friend ostream& operator<<(ostream& os, const BiletTren& t) {
        os << (const Bilet&)t;
        os << "Ruta: " << t.ruta << endl;
        return os;
    }
};

int Bilet::counter = 1;

int main() {

	cout << "==== Bilet ====" << endl;
	Bilet bilet("Ion Popescu");
	cout << bilet << endl;
	cout << "================" << endl << endl;

	cout << "==== Bilet Urban ====" << endl;
	BiletUrban biletUrban("Maria Ionescu", "Bucuresti");
	cout << biletUrban << endl;
	biletUrban.validate();
    cout << "================" << endl << endl;

	cout << "==== Bilet Metrou ====" << endl;
	BiletMetrou biletMetrou("Andrei Vasilescu", "Bucuresti", "Abonament");
	cout << biletMetrou << endl;
	biletMetrou.validate();
    cout << "================" << endl << endl;

	cout << "==== Bilet STB ====" << endl;
	BiletSTB biletSTB("Elena Georgescu", "Bucuresti", 5);
	cout << biletSTB << endl;
	biletSTB.validate();
    cout << "================" << endl << endl;

	cout << "==== Bilet Unic ====" << endl;
	BiletUnic biletUnic("Cristina Dumitrescu", "Bucuresti", 10, "Bilet comun metrou + STB", 120);
	cout << biletUnic << endl;
	biletUnic.validate();
    cout << "================" << endl << endl;

	cout << "==== Bilet Tren ====" << endl;
	BiletTren biletTren("Vasile Marin", "Bucuresti - Cluj");
	cout << biletTren << endl;
	biletTren.validate();
    cout << "================" << endl << endl;

    return 0;
}
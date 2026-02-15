#include <iostream>

using namespace std;

class Paralelipiped
{
private:
	int lungime;
	int latime;
	int inaltime;
public:
	Paralelipiped() : lungime(0), latime(0), inaltime(0)
	{
	}

	Paralelipiped(int lungime, int latime, int inaltime) : lungime(lungime), latime(latime), inaltime(inaltime)
	{
	}

	Paralelipiped(const Paralelipiped& p) : lungime(p.lungime), latime(p.latime), inaltime(p.inaltime)
	{
	}

	Paralelipiped& operator=(const Paralelipiped& p)
	{
		if (this != &p)
		{
			this->lungime = p.lungime;
			this->latime = p.latime;
			this->inaltime = p.inaltime;
		}
		return *this;
	}

	friend ostream& operator<<(ostream& out, const Paralelipiped& p)
	{
		out << "Lungime: " << p.lungime << " Latime: " << p.latime << " Inaltime: " << p.inaltime << endl;
		return out;
	}

	friend istream& operator>>(istream& in, Paralelipiped& p)
	{
		cout << "Lungime: ";
		in >> p.lungime;
		cout << "Latime: ";
		in >> p.latime;
		cout << "Inaltime: ";
		in >> p.inaltime;
		return in;
	}

	Paralelipiped operator+(const Paralelipiped& p)
	{
		Paralelipiped rezultat;
		rezultat.lungime = this->lungime + p.lungime;
		rezultat.latime = this->latime + p.latime;
		rezultat.inaltime = this->inaltime + p.inaltime;
		return rezultat;
	}

	int operator-(const Paralelipiped& p)
	{
		int volum1 = this->volum();
		int volum2 = p.volum();
		return abs(volum1 - volum2);
	}

	Paralelipiped operator*(const Paralelipiped& p)
	{
		Paralelipiped rezultat;
		int volum = p.volum();
		rezultat.lungime = this->lungime * volum;
		rezultat.latime = this->latime * volum;
		rezultat.inaltime = this->inaltime * volum;
		return rezultat;
	}

	bool operator/(const Paralelipiped& p)
	{
		return (this->lungime == 2 * p.lungime) && (this->latime == 2 * p.latime) && (this->inaltime == 2 * p.inaltime);
	}

private:
	int volum() const
	{
		return this->lungime * this->latime * this->inaltime;
	}
};

int main()
{
	Paralelipiped p1;
	cin >> p1;
	Paralelipiped p2(5, 6, 7);
	cout << "Paralelipiped 1: " << p1;
	cout << "Paralelipiped 2: " << p2;
	Paralelipiped p3 = p1 + p2;
	cout << "Paralelipiped 3 (p1 + p2): " << p3;
	int diff = p1 - p2;
	cout << "Diferenta volumelor p1 si p2: " << diff << endl;
	Paralelipiped p4 = p1 * p2;
	cout << "Paralelipiped 4 (p1 * volum p2): " << p4;
	bool isDouble = p4 / p1;
	cout << "Paralelipiped 4 are dimensiunile duble fata de Paralelipiped 1: " << (isDouble ? "Da" : "Nu") << endl;
	
	return 0;
}
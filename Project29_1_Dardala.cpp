#define _CRT_SECURE_NO_WARNINGS

#include <iostream>
#include <string>
#include <vector>
#include <fstream>
#include <stdexcept>

using namespace std;

class Observatie
{
protected:
	double valoare;
	int frecventa;
public:
	Observatie() : valoare(0) , frecventa(0){}
	Observatie(double valoare, int frecventa) : valoare(valoare), frecventa(frecventa){}
	Observatie(const Observatie& o) : valoare(o.valoare), frecventa(o.frecventa){}
	~Observatie(){} //destructor
	double getValoare()
	{
		return this->valoare;
	}
	int getFrecventa()
	{
		return this->frecventa;
	}
	void setValoare(double valoareNoua)
	{
		this->valoare = valoareNoua;
	}
	void setFrecventa(int frecventaNoua)
	{
		this->frecventa = frecventaNoua;
	}
	friend ostream& operator<<(ostream& os, const Observatie& o)
	{
		os << "Valoare: " << o.valoare << endl;
		os << "Frecventa: " << o.frecventa << endl;
		return os;
	}
	friend istream& operator>>(istream& is,  Observatie& o)
	{
		cout << "Valoare: " << endl; is >> o.valoare;
		cout << "Frecventa: " << endl; is >> o.frecventa;
		return is;
	}
	friend ofstream& operator<<(ofstream& of, const Observatie& o)
	{
		of << o.valoare << " " << o.frecventa << endl;
		return of;
	}
};

class Colectivitate
{
private:
	vector<Observatie> obs; //has-a
	int counter;
public:
	Colectivitate(): counter(0){}
	Colectivitate(vector<Observatie> obs ,int counter) : obs(obs), counter(counter){}
	Colectivitate(const Colectivitate& c) : obs(c.obs), counter(c.counter){}
	~Colectivitate(){}
	friend ostream& operator<<(ostream& os, const Colectivitate& c)
	{
		for (auto m : c.obs)
			os << m;
		os << "Counter: " << c.counter << endl;
		return os;
	}
	friend istream& operator>>(istream& is, Colectivitate& c)
	{
		cout << "Introduceti numarul de indexari: ";
		is >> c.counter;

		c.obs.clear();
		for (int i = 0; i < c.counter; i++)
		{
			Observatie o;
			is >> o;
			c.obs.push_back(o);
		}
		return is;
	}
	friend ofstream& operator<<(ofstream& of, const  Colectivitate& c)
	{
		for (auto m : c.obs)
			of << m;
		of << c.counter;
		return of;
	}
	Observatie& operator[](int index)
	{
		if (index < 0 || index >= counter)
			throw out_of_range("Indexul este invalid");
		return this->obs[index];
	}
	explicit operator double()
	{
		double medie = 0;
		for (auto m : obs)
			medie += m.getFrecventa() * m.getValoare();
		medie /= counter;
		return medie;
	}
	bool operator!()
	{
		return obs.size() == 0;
	}
};



int main()
{
	vector<Observatie> v = { {2,3} ,{6,7} };
	Colectivitate c1(v, v.size());
	cout << c1;
	cin >> c1;
	cout << c1;
	ofstream fout;
	fout.open("Tataie.txt", ofstream::out);
	if (fout.is_open())
	{
		fout << c1;
		fout.close();
	}
	cout << c1[1] << endl;
	cout << !c1 << endl;
	cout << true << endl;
	cout << (double)c1;
	return 0;
}
#define _CRT_SECURE_NO_WARNINGS

#include <iostream>
#include <string>
#include <cstring>
#include <vector>
#include <fstream>

using namespace std;
enum Stare
{
	disponibil,
	utilizat,
	sters
};
class Fisier
{
protected:
	string nume;
	double dimensiune;
	string extensie;
	string numeComplet;
	Stare stare;
public:
	Fisier() : nume("Unknown"), dimensiune(0), extensie("Unknown"), numeComplet("Unknown"), stare(sters){}
	Fisier(string nume, double dimensiune, string extensie, Stare stare) : nume(nume), dimensiune(dimensiune), extensie(extensie), stare(stare), numeComplet("")
	{
		this->numeComplet = nume + "." + extensie;
	}
	Fisier(const Fisier& f) : nume(f.nume), dimensiune(f.dimensiune), extensie(f.extensie), stare(f.stare), numeComplet("")
	{
		this->numeComplet = f.nume + "." + f.extensie;
	}
	void setNumeComplet(string fnume, string fextensie)
	{
		if (fnume == "" && fextensie == "") return;
		this->numeComplet.clear();
		this->numeComplet = fnume + "." + fextensie;
	}
	void clearNume()
	{
		this->nume.clear();
	}
	void clearExtensie()
	{
		this->extensie.clear();
	}
	void clearNumeComplet()
	{
		this->numeComplet.clear();
	}
	void setStare(int num)
	{
		stare = (Stare)num;
	}
	~Fisier(){}
	Stare getStare()
	{
		return this->stare;
	}
	friend ostream& operator<<(ostream& os, const Fisier& f)
	{
		os <<"Nume: " << f.nume << "\nDimensiune:  " << f.dimensiune << "\nExtensie: " << f.extensie << "\nNume complet: " << f.numeComplet << "\nStare: " << f.stare << endl;
		return os;
	}
	friend istream& operator>>(istream& is, Fisier& f)
	{
		cout << "Nume: "; is >> f.nume;
		cout << "Dimensiune: "; is >> f.dimensiune;
		cout << "Extensie: "; is >> f.extensie;
		int numar;
		cout << "Stare: (disponibil 0/utilizat 1/sters 2): "; is >> numar;
		f.setStare(numar);
		return is;
	}
	friend ofstream& operator<<(ofstream& of, const Fisier& f)
	{
		of <<f.nume << " " << f.dimensiune << " " << f.extensie << " "<< f.stare << endl;
		return of;
	}
	friend ifstream& operator>>(ifstream& is, Fisier& f)
	{
		is >> f.nume >> f.dimensiune >> f.extensie;
		int valoare;
		is >> valoare;
		f.setStare(valoare);
		return is;
	}
};
class Folder
{
private:
	string nume;
	vector<Fisier> fisiere;
public:
	Folder() : nume("Unknown"){}
	Folder(string nume, vector<Fisier> fisiere) : nume(nume), fisiere(fisiere){}
	Folder(const Folder& f) : nume(f.nume), fisiere(f.fisiere){}
	~Folder(){}
	friend ostream& operator<<(ostream& os, const Folder& f)
	{
		os << "\nNume Folder: " << f.nume << endl;
		for (auto m : f.fisiere)
			os << m;
		return os;
	}
	friend ofstream& operator<<(ofstream& of, const Folder& f)
	{
		of <<f.nume;
		for (auto m : f.fisiere)
			of << m;
		return of;
	}
	friend istream& operator>>(istream& is, Folder& f)
	{
		cout << "Nume Folder: "; is >> f.nume;
		Fisier f1;
		is >> f1;
		f.fisiere.push_back(f1);
		return is;
	}
	friend ifstream& operator>>(ifstream& ifs, Folder& f)
	{
		ifs >> f.nume;
		for (auto m : f.fisiere)
			ifs >> m;
		return ifs;
	}
	vector<Fisier> operator()(Stare s)
	{
		vector<Fisier> fisiereNoi;
		for (auto m : fisiere)
			if (m.getStare() == s)
				fisiereNoi.push_back(m);
		return fisiereNoi;
	}
	Folder& operator~()
	{
		Fisier::clearExtensie;
		Fisier::clearNume;
		Fisier::clearNumeComplet;
		return *this;
	}
};


//string nume, double dimensiune, string extensie, Stare stare
int main()
{
	vector<Fisier> v1 = { {"Tataia", 67.67, "muistul", disponibil} };
	Folder f1, f2("Tataia", v1);
	cout << f2 << endl;
	//cin >> f2;
	//cout << f2 << endl;
	//vector<Fisier> v2 = f2(disponibil);
	//for (auto m : v2)
	//	cout << m;
	ofstream fout;
	fout.open("Tataia.txt", ofstream::out);
	if (fout.is_open())
	{
		fout << f2;
		fout.close();
	}
	~(f1);
	ifstream fin;
	fin.open("Tataia.txt", ifstream::in);
	if (fin.is_open())
	{
		fin >> f1;
		fin.close();
	}
	cout << f1;
	return 0;
}
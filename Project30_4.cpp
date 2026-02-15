#define _CRT_SECURE_NO_WARNINGS

#include <iostream>
#include <string>
#include <cstring>
#include <vector>
#include <map>
#include <stdexcept>

using namespace std;


class Produs
{
protected:
	string cod;
	string nume;
	double pret;
	map<string, int> stocPeMarimi;
public:
	Produs() : cod("n/a"), nume("n/a"), pret(0.0){}
	Produs(string cod, string nume, double pret, map<string, int> stocPeMarimi) : cod(cod), nume(nume), pret(pret), stocPeMarimi(stocPeMarimi){}
	Produs(const Produs& p) : cod(p.cod), nume(p.nume), pret(p.pret), stocPeMarimi(p.stocPeMarimi){}
	friend ostream& operator<<(ostream& os, const Produs& p)
	{
		os << "Cod: " << p.cod << endl;
		os << "Nume: " << p.nume << endl;
		os << "Pret: " << p.pret << endl;
		for (auto m : p.stocPeMarimi)
			os << m.first << "------" << m.second << endl;
		return os;
	}
	virtual string getCategorie() = 0;
	virtual double getPretFinal()
	{
		return this->pret;
	}
	virtual ~Produs(){}
	virtual string getCod()
	{
		return this->cod;
	}
	int getStocTotal() 
	{
		int stoc = 0;
		for (auto m : stocPeMarimi)
			stoc += m.second;
		return stoc;
	}
	int getStocMarime(string key) const
	{
		auto it = stocPeMarimi.find(key);
		if (it != stocPeMarimi.end())
			return it->second;
	}
	void setStocMarime(string key, int num)
	{
		int valoare;
		auto it = stocPeMarimi.find(key);
		if (it != stocPeMarimi.end())
			it->second -= num;
	}
	map<string, int>& getStocPeMarimi()
	{
		return this->stocPeMarimi;
	}
};

class Tricou : public Produs
{
protected:
	string producator;
public:
	Tricou() : Produs(), producator("n/a"){}
	Tricou(string cod, string nume, double pret, map<string, int> stocPeMarimi, string producator) : Produs(cod,nume,pret, stocPeMarimi) , producator(producator){}
	Tricou(const Tricou& t) : Produs(t), producator(t.producator){}
	~Tricou(){}
	friend ostream& operator<<(ostream& os, const Tricou& p)
	{
		os << "Cod: " << p.cod << endl;
		os << "Nume: " << p.nume << endl;
		os << "Pret: " << p.pret << endl;
		for (auto m : p.stocPeMarimi)
			os << m.first << "------" << m.second << endl;
		os << "Producator: " << p.producator << endl;
		return os;
	}
	string getCategorie() override
	{
		return "Tricou";
	}
	double getPretFinal()
	{
		int stoc = 0;
		for (auto m : stocPeMarimi)
		{
			stoc += m.second;
		}
		if (stoc > 100)
			return this->pret *= 0.9;
		return this->pret;
	}
};

class Pantaloni : public Produs
{
protected:
	string culoare;
public:
	Pantaloni() : Produs(), culoare("n/a"){}
	Pantaloni(string cod, string nume, double pret, map<string, int> stocPeMarimi, string culoare) : Produs(cod, nume, pret, stocPeMarimi), culoare(culoare){}
	Pantaloni(const Pantaloni& p) : Produs(p), culoare(p.culoare){}
	friend ostream& operator<<(ostream& os, const Pantaloni& p)
	{
		os << "Cod: " << p.cod << endl;
		os << "Nume: " << p.nume << endl;
		os << "Pret: " << p.pret << endl;
		for (auto m : p.stocPeMarimi)
			os << m.first << "------" << m.second << endl;
		os << "Producator: " << p.culoare << endl;
		return os;
	}
	~Pantaloni(){}
	string getCategorie() override
	{
		return "Pantaloni";
	}
	double getPretFinal()
	{
		return this->pret;
	}
};

class Jacheta : public Produs
{
public:
	Jacheta() : Produs(){}
	Jacheta(string cod, string nume, double pret, map<string, int> stocPeMarimi) : Produs(cod, nume, pret, stocPeMarimi) {}
	Jacheta(const Jacheta& j) : Produs(j){}
	~Jacheta(){}
	string getCategorie() override
	{
		return "Jacheta";
	}
	double getPretFinal()
	{
		if (this->pret > 1200)
			return this->pret *= 1.15;
		return this->pret;
	}
};

class Magazin
{
protected:
	vector<Produs*> produse;
	map<string, int> vanzariMarimi;
public:
	Magazin() {}
	Magazin(vector<Produs*> produse, map<string, int> vanzariMarimi) : produse(produse), vanzariMarimi(vanzariMarimi){}
	Magazin(const Magazin& m) : produse(m.produse), vanzariMarimi(m.vanzariMarimi){}
	~Magazin(){}
	friend ostream& operator<<(ostream& os, const Magazin& m)
	{
		for (auto& mi : m.produse)
			os << *mi << endl;
			
		for (auto &mi : m.vanzariMarimi)
			os << mi.first << "-------" << mi.second << endl;
		return os;
	}
	void adaugaProdus(Produs* p)
	{
		for (auto& m : produse)
			if (m->getCod() == p->getCod())
				throw out_of_range("Codurile de produs sunt identice!");
		this->produse.push_back(p);
	}
	map<string, string> genereazaRaportEchilibru()
	{
		int stoc;
		vector<string> marimi = { "S", "M", "L", "XL" };
		map<string, string> temp;
		for (auto* m1 : produse)
		{
			int max = 0;
			string tempMarime = "";
			stoc = m1->getStocTotal();
			for (auto m2 : marimi)
				if (max < m1->getStocMarime(m2))
				{
					max = m1->getStocMarime(m2);
					tempMarime = m2;
				}
			if (max > stoc * 0.5)
				temp[m1->getCod()] = tempMarime;
		}
		return temp;
	}
	void operator()(string cod, string marime, int cantitate)
	{
		Produs* produs = nullptr;
		for (auto& p : this->produse)
		{
			if (p->getCod() == cod)
				produs = p;
		}
		if (produs == nullptr)
			return ;
		map<string, int>& stoc = produs->getStocPeMarimi();
		if (stoc[marime] < cantitate)
		{
			cout << "Cantitatea pentru masura " << marime << " din produsul " << cod << " este insuficienta!" << endl;
			return ;
		}
		stoc[marime] -= cantitate;
		this->vanzariMarimi[marime] += cantitate * produs->getPretFinal();
	}
	void reaprovizionare(string cod, string marime, int cantitate)
	{
		Produs* produs = nullptr;
		for (auto& p : produse)
			if (cod == p->getCod())
				produs = p;
		map<string, int>& stoc = produs->getStocPeMarimi();
		stoc[marime] += cantitate;
	}
};

//string cod, string nume, double pret, map<string, int> stocPeMarimi
int main()
{
	map<string, int> mStoc1 = {
		{"S", 14},
		{"M", 200},
		{"L", 56},
		{"XL", 14}
	};
	map<string, int> mStoc2 = {
		{"S", 10},
		{"M", 20},
		{"L", 500},
		{"XL", 40}
	};
	map<string, int> mStoc3 = {
		{"S", 45},
		{"M", 500},
		{"L", 22},
		{"XL", 13}
	};
	map<string, int> mVanzari1 = {
		{"S", 0},
		{"M", 0},
		{"L", 0},
		{"XL", 0}
	};
	Tricou* t1 = new Tricou("TATAIE1", "BOSOROGUL1", 14.87, mStoc1, "TATAIEEEE");
	Jacheta* j1 = new Jacheta("TATAIE2", "BOSOROGUL2", 1250, mStoc2);
	Pantaloni* p1 = new Pantaloni("TATAIE3", "BOSOROGUL3", 180, mStoc1, "TATAIEEEE");
	vector<Produs*> vProduse = {t1,j1};
	Magazin m1(vProduse, mVanzari1);
	//cout << m1 << endl;
	//cout << j1.getCategorie() << endl;
	//cout << j1.getPretFinal() << endl;
	//cout << t1.getPretFinal();
	//try {
	//	m1.adaugaProdus(p1);
	//}
	//catch (const out_of_range& e)
	//{
	//	cout << e.what() << endl;
	//}
	//cout << m1;
	//map<string, string> raport;
	//raport = m1.genereazaRaportEchilibru();
	//for (auto& m : raport)
		//cout << m.first << "--------" <<m.second<< endl;
	m1("TATAIE1", "M", 80);
	cout << m1;
	m1.reaprovizionare("TATAIE1", "M", 1000);
	cout << m1;
	delete t1, j1, p1;
	return 0;
}
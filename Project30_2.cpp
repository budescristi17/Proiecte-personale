#define _CRT_SECURE_NO_WARNINGS

#include <iostream>
#include <string>
#include <cstring>
#include <vector>

using namespace std;
enum TipCarte
{
	HORROR,
	POLITIST,
	DRAMA
};
class Carte
{
protected:
	string numeAutor;
	char* titluCarte;
	TipCarte tip;
	int nrExemplare;
public:
	Carte() : numeAutor(""), titluCarte(nullptr), tip(HORROR), nrExemplare(0){}
	Carte(string numeAutor, const char* ftitluCarte, TipCarte tip, int nrExemplare) : numeAutor(numeAutor), titluCarte(nullptr), tip(tip), nrExemplare(nrExemplare)
	{
		if (ftitluCarte)
		{
			titluCarte = new char[strlen(ftitluCarte) + 1];
			strcpy(titluCarte, ftitluCarte);
		}
	}
	Carte(const Carte& c) : numeAutor(c.numeAutor), titluCarte(nullptr), tip(c.tip), nrExemplare(c.nrExemplare)
	{
		if (c.titluCarte)
		{
			titluCarte = new char[strlen(c.titluCarte) + 1];
			strcpy(titluCarte, c.titluCarte);
		}
	}
	Carte& operator=(const Carte& c)
	{
		if (this == &c) return *this;
		this->numeAutor = c.numeAutor;
		this->tip = c.tip;
		this->nrExemplare = c.nrExemplare;
		this->titluCarte = nullptr;
		if (c.titluCarte != nullptr)
		{
			this->titluCarte = new char[strlen(c.titluCarte) + 1];
			strcpy(this->titluCarte, c.titluCarte);
		}
		return *this;
	}
	void setNrExemplare(int num)
	{
		this->nrExemplare = num;
	}
	int getNrExemplare()
	{
		return this->nrExemplare;
	}
	void setTipCarte(int num)
	{
		this->tip = (TipCarte)num;
	}
	~Carte()
	{
		delete[] titluCarte;
	}
	explicit operator int()
	{
		return this->nrExemplare;
	}
	friend ostream& operator<<(ostream& os, const Carte& c)
	{
		os << "Nume autor: " << c.numeAutor << endl;
		os << "Titlu carte: " << (c.titluCarte ? c.titluCarte : "(NULL)") << endl;
		os << "Tip carte: " << c.tip << endl;
		os << "Numar exemplare: " << c.nrExemplare << endl;
		return os;
	}
	static Carte& carteExemplar(vector<Carte>& v)
	{
		int max = 0;
		int pozitie = 0;
		for(int i = 0; i < v.size(); i++)
			if (max < v[i].getNrExemplare())
			{
				max = v[i].getNrExemplare();
				pozitie = i;
			}
		return v[pozitie];
	}
	virtual void inchiriareaCarte()
	{
		if (getNrExemplare() != 0)
		{
			cout << "Multumim pentru inchiriere!! :DD ";
			this->nrExemplare -= 1;
			return;
		}
		cout << "Nu mai sunt exemplare pe stoc, ne pare rau !!! :((((((";
		return;

	}
	friend istream& operator>>(istream& is, Carte& c)
	{
		cout << "Nume autor: "; is >> c.numeAutor;

		cout << "Titlu carte: ";
		char buffer[30];
		is >> buffer;
		delete[] c.titluCarte;
		c.titluCarte = new char[strlen(buffer) + 1];
		strcpy(c.titluCarte, buffer);

		cout << "Tip carte: ";
		int num;
		is >> num;
		c.setTipCarte(num);

		cout << "Numar exemplare: "; is >> c.nrExemplare;
		return is;
	}
};
class CarteElectronica : public Carte
{
private:
	string formatFisier;
public:
	CarteElectronica() : Carte(), formatFisier("Unknown"){}
	CarteElectronica(string numeAutor, const char* titluCarte, TipCarte tip, int nrExemplare, string formatFisier) : Carte(numeAutor,titluCarte,tip,nrExemplare) , formatFisier(formatFisier){}
	void inchiriareaCarte() override
	{
		cout << "Cartea a fost inchiriata cu succes <3333333333333333333333333333333333333333333333 !";
	}
	friend ostream& operator<<(ostream& os, const CarteElectronica& c)
	{
		os << "Nume autor: " << c.numeAutor << endl;
		os << "Titlu carte: " << (c.titluCarte ? c.titluCarte : "(NULL)") << endl;
		os << "Tip carte: " << c.tip << endl;
		os << "Numar exemplare: " << c.nrExemplare << endl;
		os << "Format fisier: " << c.formatFisier << endl;
		return os;
	}
	friend istream& operator>>(istream& is, CarteElectronica& c)
	{
		cout << "Nume autor: "; is >> c.numeAutor;

		cout << "Titlu carte: ";
		char buffer[30];
		is >> buffer;
		delete[] c.titluCarte;
		c.titluCarte = new char[strlen(buffer) + 1];
		strcpy(c.titluCarte, buffer);

		cout << "Tip carte: ";
		int num;
		is >> num;
		c.setTipCarte(num);

		cout << "Numar exemplare: "; is >> c.nrExemplare;
		cout << "Format fisier: "; is >> c.formatFisier;

		return is;
	}
};
//string numeAutor, const char* ftitluCarte, TipCarte tip, int nrExemplare
int main()
{
	vector<Carte> v1 = { {"Tataiel", "Buci de fier", DRAMA, 50}, {"Bunicul", "Pula de fier", HORROR, 110}};
	Carte c1, c2("Tataie", "Bucile de fier", HORROR, 100);
	cout << c2 << endl;
	cout << (int)c2 << endl;
	cout << c2.carteExemplar(v1);
	cin >> c1;
	cout << c1;
	return 0;
}
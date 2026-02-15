#include <iostream>

using namespace std;

class Pasare
{
private:
	int inaltime;
	bool canta;
public:
	Pasare() {
		this->canta = false;
		this->inaltime = 0;
	}
	Pasare(int inaltime, bool canta)
		:inaltime(inaltime)
	{
		this->canta = canta;
	}

	Pasare(const Pasare& sursa) 
	{
		this->inaltime = sursa.inaltime;
		this->canta = sursa.canta;
	}

	Pasare& operator =(const Pasare& sursa)
	{
		if (this != &sursa)
		{
			this->inaltime = sursa.inaltime;
			this->canta = sursa.canta;
		}
		return *this;
	}

	void set_canta(bool val)
	{
		this->canta = val;
	}

	bool get_canta()
	{
		return this->canta;
	}

	void set_inaltime(int val)
	{
		this->inaltime = val;
	}

	int get_inaltime()
	{
		return this->inaltime;
	}

	void display_canta()
	{
		cout << "Pasarea canta " << this->canta << " si are inaltimea: " << this->inaltime << " Inaltimea este stocata la adresa: " << &inaltime << endl;
	}
};
int main()
{
	Pasare p1;
	p1.set_inaltime(15);
	Pasare p2(20, true);
	Pasare p3 = p2;
	Pasare p4;
	p4 = p2;
	p2.set_inaltime(55);
	p1.display_canta();
	p2.display_canta();
	p3.display_canta();
	p4.display_canta();	
}

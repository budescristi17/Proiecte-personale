#include <iostream>

using namespace std;

class Triunghi
{
private:
	int l1, l2, l3;
	static int instante;
public:
	/* La apelarea oricarui constructor, numarul de instante alocate in memorie trebuie sa creasca */
	Triunghi()
	{
		this->l1 = 3;
		this->l2 = 4;
		this->l3 = 5;
		/* membrul static NU este membru de instanta, deci nu poate fi accesat prin pointerul this */
		Triunghi::instante++;
	}

	Triunghi(int l1, int l2, int l3)
	{
		this->l1 = l1;
		this->l2 = l2;
		this->l3 = l3;
		/* membrul static NU este membru de instanta, deci nu poate fi accesat prin pointerul this */
		Triunghi::instante++;
	}

	int getL1()
	{
		return this->l1;
	}

	void setL1(int l1)
	{
		this->l1 = l1;
	}

	int getL2()
	{
		return this->l2;
	}

	void setL2(int l2)
	{
		this->l2 = l2;
	}

	int getL3()
	{
		return this->l3;
	}

	void setL3(int l3)
	{
		this->l3 = l3;
	}

	/* getter static, deoarece modificatorul de acces al membrului "instante" este private. Retinem, acesta
	nu este membru de instanta, ci membru de clasa */
	static int getInstante()
	{
		return Triunghi::instante;
	}

	int calculeazaPerimetru()
	{
		return this->l1 + this->l2 + this->l3;
	}

	float calculeazaArie()
	{
		/* Formula lui Heron */
		int s = this->calculeazaPerimetru() / 2;
		return sqrt(s * (s - l1) * (s - l2) * (s - l3));
	}

	/* La apelarea destructorului, numarul de instante alocate in memorie trebuie sa scada */
	~Triunghi()
	{
		
		Triunghi::instante--;
	}
};

/* initializarea membrului static */
int Triunghi::instante = 0;

int main()
{
	Triunghi* t1 = new Triunghi(7, 8, 12);
	cout << "Latura 1 pentru T1: " << t1->getL1() << endl;
	cout << "Latura 2 pentru T1: " << t1->getL2() << endl;
	cout << "Perimetrul lui T1: " << t1->calculeazaPerimetru() << endl;
	cout << "Aria lui T1: " << t1->calculeazaArie() << endl;

	/* exercitiu: implementati o validare pornind de la inegalitatea triunghiului, astfel incat sa verificati 
	faptul ca cele trei valori pot constitui laturile unui triunghi */
	Triunghi t2(1, 3, 5);
	t2.setL1(3); 
	t2.setL2(5);
	cout << "Perimetrul lui T2: " << t2.calculeazaPerimetru() << endl;
	cout << "Aria lui T2: " << t2.calculeazaArie() << endl;
	cout << "Numar de instante in memorie: " << Triunghi::getInstante() << endl;
	cout << "Stergem instanta T1..." << endl;
	delete t1;
	cout << "Numar de instante in memorie: " << Triunghi::getInstante() << endl;
}
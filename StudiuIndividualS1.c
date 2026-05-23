#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

/*
  Studiu individual S1
  Tema: structura simpla cu alocare dinamica.

  Cerinte:
  1. Creati o structura Produs cu id, stoc, pret, denumire si categorie.
  2. Implementati functii pentru initializare, afisare, modificare denumire.
  3. Calculati valoarea totala din stoc: stoc * pret.
  4. Dezalocati memoria alocata dinamic.
*/

typedef struct Produs Produs;
struct Produs {
	int id;
	int stoc;
	float pret;
	char* denumire;
	char categorie;
};

Produs initializareProdus(int id, int stoc, float pret, const char* denumire, char categorie) {
	Produs produs;
	produs.id = id;
	produs.stoc = stoc;
	produs.pret = pret;
	produs.denumire = (char*)malloc(strlen(denumire) + 1);
	strcpy(produs.denumire, denumire);
	produs.categorie = categorie;
	return produs;
}

void afisareProdus(Produs produs) {
	printf("ID: %d | Stoc: %d | Pret: %.2f | Denumire: %s | Categorie: %c\n",
		produs.id, produs.stoc, produs.pret, produs.denumire, produs.categorie);
}

void modificaDenumire(Produs* produs, const char* denumireNoua) {
	if (produs->denumire) {
		free(produs->denumire);
	}
	produs->denumire = (char*)malloc(strlen(denumireNoua) + 1);
	strcpy(produs->denumire, denumireNoua);
}

float calculeazaValoareStoc(Produs produs) {
	return produs.stoc * produs.pret;
}

void dezalocareProdus(Produs* produs) {
	if (produs->denumire) {
		free(produs->denumire);
		produs->denumire = NULL;
	}
}

int main() {
	Produs produs = initializareProdus(1, 15, 129.99f, "Mouse", 'A');
	afisareProdus(produs);

	modificaDenumire(&produs, "Mouse wireless");
	afisareProdus(produs);

	printf("Valoare stoc: %.2f\n", calculeazaValoareStoc(produs));
	dezalocareProdus(&produs);
	return 0;
}

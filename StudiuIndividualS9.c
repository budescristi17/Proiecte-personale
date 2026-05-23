#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

/*
  Studiu individual S9
  Tema: tabela de dispersie cu chaining.

  Folositi fisierul produse_si.txt.
  Cerinte:
  1. Inserati produsele intr-o tabela hash dupa id.
  2. Afisati clusterele.
  3. Cautati un produs dupa id.
  4. Calculati pretul mediu pentru fiecare cluster nevid.
*/

typedef struct Produs Produs;
typedef struct Nod Nod;
typedef struct HashTable HashTable;

struct Produs {
	int id;
	int stoc;
	float pret;
	char* denumire;
	char categorie;
};

struct Nod {
	Produs info;
	Nod* next;
};

struct HashTable {
	int dimensiune;
	Nod** vector;
};

int hash(int id, int dimensiune) {
	return id % dimensiune;
}

HashTable initializareTabela(int dimensiune) {
	HashTable tabela;
	tabela.dimensiune = dimensiune;
	tabela.vector = (Nod**)malloc(sizeof(Nod*) * dimensiune);
	for (int i = 0; i < dimensiune; i++) {
		tabela.vector[i] = NULL;
	}
	return tabela;
}

void inserare(HashTable tabela, Produs produs) {
	int pozitie = hash(produs.id, tabela.dimensiune);
	Nod* nou = (Nod*)malloc(sizeof(Nod));
	nou->info = produs;
	nou->next = tabela.vector[pozitie];
	tabela.vector[pozitie] = nou;
}

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

Produs cautaDupaId(HashTable tabela, int id) {
	int pozitie = hash(id, tabela.dimensiune);
	Nod* p = tabela.vector[pozitie];
	while (p) {
		if (p->info.id == id) {
			return p->info;
		}
		p = p->next;
	}
	Produs produs;
	produs.id = -1;
	produs.denumire = NULL;
	return produs;
}

void afisareTabela(HashTable tabela) {
	for (int i = 0; i < tabela.dimensiune; i++) {
		printf("Cluster %d:\n", i);
		Nod* p = tabela.vector[i];
		while (p) {
			printf("  %d | %s | %.2f\n", p->info.id, p->info.denumire, p->info.pret);
			p = p->next;
		}
	}
}

float calculeazaPretMediuCluster(HashTable tabela, int cluster) {
	float suma = 0;
	int nr = 0;
	Nod* p = tabela.vector[cluster];
	while (p) {
		suma += p->info.pret;
		nr++;
		p = p->next;
	}
	return nr ? suma / nr : 0;
}

void dezalocare(HashTable* tabela) {
	for (int i = 0; i < tabela->dimensiune; i++) {
		Nod* p = tabela->vector[i];
		while (p) {
			Nod* temp = p;
			p = p->next;
			free(temp->info.denumire);
			free(temp);
		}
	}
	free(tabela->vector);
	tabela->vector = NULL;
	tabela->dimensiune = 0;
}

int main() {
	HashTable tabela = initializareTabela(5);
	inserare(tabela, initializareProdus(1, 12, 149.99f, "Tastatura", 'A'));
	inserare(tabela, initializareProdus(6, 5, 2300.00f, "Laptop", 'A'));
	inserare(tabela, initializareProdus(3, 8, 499.99f, "Casti", 'B'));
	inserare(tabela, initializareProdus(8, 10, 799.99f, "Monitor", 'C'));

	afisareTabela(tabela);
	printf("Pret mediu cluster 1: %.2f\n", calculeazaPretMediuCluster(tabela, 1));

	Produs gasit = cautaDupaId(tabela, 6);
	if (gasit.id != -1) {
		printf("Gasit: %s\n", gasit.denumire);
	}

	dezalocare(&tabela);
	return 0;
}

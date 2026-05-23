#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

/*
  Studiu individual S11
  Tema: graf reprezentat prin lista de liste.

  Folositi fisierele produse_si.txt si legaturi_produse_si.txt.
  Cerinte:
  1. Cititi nodurile grafului din fisierul cu produse.
  2. Cititi muchiile dupa id.
  3. Afisati vecinii unui produs.
  4. Determinati gradul unui nod.
*/

typedef struct Produs Produs;
typedef struct NodPrincipal NodPrincipal;
typedef struct NodSecundar NodSecundar;

struct Produs {
	int id;
	int stoc;
	float pret;
	char* denumire;
	char categorie;
};

struct NodPrincipal {
	Produs info;
	NodPrincipal* next;
	NodSecundar* vecini;
};

struct NodSecundar {
	NodPrincipal* info;
	NodSecundar* next;
};

Produs creareProdus(int id, int stoc, float pret, const char* denumire, char categorie) {
	Produs produs;
	produs.id = id;
	produs.stoc = stoc;
	produs.pret = pret;
	produs.denumire = (char*)malloc(strlen(denumire) + 1);
	strcpy(produs.denumire, denumire);
	produs.categorie = categorie;
	return produs;
}

void inserareNod(NodPrincipal** graf, Produs produs) {
	NodPrincipal* nou = (NodPrincipal*)malloc(sizeof(NodPrincipal));
	nou->info = produs;
	nou->next = NULL;
	nou->vecini = NULL;

	if (!*graf) {
		*graf = nou;
	}
	else {
		NodPrincipal* p = *graf;
		while (p->next) {
			p = p->next;
		}
		p->next = nou;
	}
}

NodPrincipal* cautaNod(NodPrincipal* graf, int id) {
	while (graf) {
		if (graf->info.id == id) {
			return graf;
		}
		graf = graf->next;
	}
	return NULL;
}

void inserareVecin(NodSecundar** lista, NodPrincipal* vecin) {
	NodSecundar* nou = (NodSecundar*)malloc(sizeof(NodSecundar));
	nou->info = vecin;
	nou->next = *lista;
	*lista = nou;
}

void inserareMuchie(NodPrincipal* graf, int idStart, int idStop) {
	NodPrincipal* start = cautaNod(graf, idStart);
	NodPrincipal* stop = cautaNod(graf, idStop);
	if (start && stop) {
		inserareVecin(&start->vecini, stop);
		inserareVecin(&stop->vecini, start);
	}
}

void afisareVecini(NodPrincipal* graf, int id) {
	NodPrincipal* nod = cautaNod(graf, id);
	if (!nod) {
		return;
	}
	printf("Vecinii produsului %s:\n", nod->info.denumire);
	NodSecundar* p = nod->vecini;
	while (p) {
		printf("  %d | %s\n", p->info->info.id, p->info->info.denumire);
		p = p->next;
	}
}

int determinaGrad(NodPrincipal* graf, int id) {
	NodPrincipal* nod = cautaNod(graf, id);
	int grad = 0;
	if (nod) {
		NodSecundar* p = nod->vecini;
		while (p) {
			grad++;
			p = p->next;
		}
	}
	return grad;
}

void dezalocareGraf(NodPrincipal** graf) {
	while (*graf) {
		NodPrincipal* temp = *graf;
		*graf = (*graf)->next;

		while (temp->vecini) {
			NodSecundar* vecin = temp->vecini;
			temp->vecini = temp->vecini->next;
			free(vecin);
		}

		free(temp->info.denumire);
		free(temp);
	}
}

int main() {
	NodPrincipal* graf = NULL;
	inserareNod(&graf, creareProdus(1, 12, 149.99f, "Tastatura", 'A'));
	inserareNod(&graf, creareProdus(2, 20, 89.50f, "Mouse", 'B'));
	inserareNod(&graf, creareProdus(3, 5, 2300.00f, "Laptop", 'A'));
	inserareNod(&graf, creareProdus(4, 7, 799.99f, "Monitor", 'C'));

	inserareMuchie(graf, 1, 2);
	inserareMuchie(graf, 1, 3);
	inserareMuchie(graf, 3, 4);

	afisareVecini(graf, 1);
	printf("Grad produs 1: %d\n", determinaGrad(graf, 1));

	dezalocareGraf(&graf);
	return 0;
}

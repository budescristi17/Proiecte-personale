#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

/*
  Studiu individual S7
  Tema: lista dublu inlantuita.

  Cerinte:
  1. Creati o lista dublu inlantuita de comenzi.
  2. Afisati lista de la inceput la final si invers.
  3. Stergeti o comanda dupa id.
  4. Calculati valoarea totala a comenzilor active.
*/

typedef struct Comanda Comanda;
typedef struct Nod Nod;
typedef struct ListaDubla ListaDubla;

struct Comanda {
	int id;
	float valoare;
	char* client;
	char status;
};

struct Nod {
	Comanda info;
	Nod* next;
	Nod* prev;
};

struct ListaDubla {
	Nod* prim;
	Nod* ultim;
};

Comanda initializareComanda(int id, float valoare, const char* client, char status) {
	Comanda comanda;
	comanda.id = id;
	comanda.valoare = valoare;
	comanda.client = (char*)malloc(strlen(client) + 1);
	strcpy(comanda.client, client);
	comanda.status = status;
	return comanda;
}

void inserareFinal(ListaDubla* lista, Comanda comanda) {
	Nod* nou = (Nod*)malloc(sizeof(Nod));
	nou->info = comanda;
	nou->next = NULL;
	nou->prev = lista->ultim;

	if (lista->ultim) {
		lista->ultim->next = nou;
	}
	else {
		lista->prim = nou;
	}
	lista->ultim = nou;
}

void afisareComanda(Comanda comanda) {
	printf("%d | %s | %.2f | %c\n", comanda.id, comanda.client, comanda.valoare, comanda.status);
}

void afisareDirect(ListaDubla lista) {
	Nod* p = lista.prim;
	while (p) {
		afisareComanda(p->info);
		p = p->next;
	}
}

void afisareInvers(ListaDubla lista) {
	Nod* p = lista.ultim;
	while (p) {
		afisareComanda(p->info);
		p = p->prev;
	}
}

float calculeazaValoareActive(ListaDubla lista) {
	float suma = 0;
	Nod* p = lista.prim;
	while (p) {
		if (p->info.status == 'A') {
			suma += p->info.valoare;
		}
		p = p->next;
	}
	return suma;
}

void stergeDupaId(ListaDubla* lista, int id) {
	Nod* p = lista->prim;
	while (p && p->info.id != id) {
		p = p->next;
	}
	if (!p) {
		return;
	}
	if (p->prev) {
		p->prev->next = p->next;
	}
	else {
		lista->prim = p->next;
	}
	if (p->next) {
		p->next->prev = p->prev;
	}
	else {
		lista->ultim = p->prev;
	}
	free(p->info.client);
	free(p);
}

void dezalocare(ListaDubla* lista) {
	while (lista->prim) {
		Nod* temp = lista->prim;
		lista->prim = lista->prim->next;
		free(temp->info.client);
		free(temp);
	}
	lista->ultim = NULL;
}

int main() {
	ListaDubla lista;
	lista.prim = NULL;
	lista.ultim = NULL;

	inserareFinal(&lista, initializareComanda(1, 240.50f, "Ana", 'A'));
	inserareFinal(&lista, initializareComanda(2, 110.00f, "Mara", 'I'));
	inserareFinal(&lista, initializareComanda(3, 520.75f, "Radu", 'A'));

	printf("Direct:\n");
	afisareDirect(lista);
	printf("\nInvers:\n");
	afisareInvers(lista);

	printf("\nValoare active: %.2f\n", calculeazaValoareActive(lista));
	stergeDupaId(&lista, 2);
	printf("\nDupa stergere:\n");
	afisareDirect(lista);

	dezalocare(&lista);
	return 0;
}

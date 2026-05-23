#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

/*
  Studiu individual S6
  Tema: lista simplu inlantuita.

  Folositi fisierul produse_si.txt.
  Cerinte:
  1. Cititi produsele intr-o lista simplu inlantuita.
  2. Inserati un produs la final.
  3. Calculati pretul mediu al produselor.
  4. Stergeti produsele dintr-o categorie data.
*/

typedef struct Produs Produs;
typedef struct Nod Nod;

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

Produs citireProdusDinFisier(FILE* fisier) {
	char linie[128];
	char sep[3] = ",\n";
	Produs produs = { -1, 0, 0, NULL, '-' };

	if (!fgets(linie, 128, fisier)) {
		return produs;
	}

	char* token = strtok(linie, sep);
	if (!token) {
		return produs;
	}

	produs.id = atoi(token);
	produs.stoc = atoi(strtok(NULL, sep));
	produs.pret = (float)atof(strtok(NULL, sep));
	token = strtok(NULL, sep);
	produs.denumire = (char*)malloc(strlen(token) + 1);
	strcpy(produs.denumire, token);
	produs.categorie = strtok(NULL, sep)[0];
	return produs;
}

void inserareLaFinal(Nod** cap, Produs produs) {
	Nod* nou = (Nod*)malloc(sizeof(Nod));
	nou->info = produs;
	nou->next = NULL;

	if (!*cap) {
		*cap = nou;
	}
	else {
		Nod* p = *cap;
		while (p->next) {
			p = p->next;
		}
		p->next = nou;
	}
}

Nod* citireLista(const char* numeFisier) {
	FILE* fisier = fopen(numeFisier, "r");
	Nod* cap = NULL;
	if (!fisier) {
		return NULL;
	}

	while (!feof(fisier)) {
		Produs produs = citireProdusDinFisier(fisier);
		if (produs.id != -1) {
			inserareLaFinal(&cap, produs);
		}
	}
	fclose(fisier);
	return cap;
}

void afisareLista(Nod* cap) {
	while (cap) {
		printf("%d | %s | %.2f | %c\n",
			cap->info.id, cap->info.denumire, cap->info.pret, cap->info.categorie);
		cap = cap->next;
	}
}

float calculeazaPretMediu(Nod* cap) {
	float suma = 0;
	int nr = 0;
	while (cap) {
		suma += cap->info.pret;
		nr++;
		cap = cap->next;
	}
	return nr ? suma / nr : 0;
}

void stergeCategorie(Nod** cap, char categorie) {
	while (*cap && (*cap)->info.categorie == categorie) {
		Nod* temp = *cap;
		*cap = (*cap)->next;
		free(temp->info.denumire);
		free(temp);
	}

	Nod* p = *cap;
	while (p && p->next) {
		if (p->next->info.categorie == categorie) {
			Nod* temp = p->next;
			p->next = temp->next;
			free(temp->info.denumire);
			free(temp);
		}
		else {
			p = p->next;
		}
	}
}

void dezalocareLista(Nod** cap) {
	while (*cap) {
		Nod* temp = *cap;
		*cap = (*cap)->next;
		free(temp->info.denumire);
		free(temp);
	}
}

int main() {
	Nod* lista = citireLista("produse_si.txt");
	afisareLista(lista);
	printf("Pret mediu: %.2f\n", calculeazaPretMediu(lista));

	printf("\nDupa stergerea categoriei A:\n");
	stergeCategorie(&lista, 'A');
	afisareLista(lista);

	dezalocareLista(&lista);
	return 0;
}

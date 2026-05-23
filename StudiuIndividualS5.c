#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

/*
  Studiu individual S5
  Tema: citire din fisier text intr-un vector dinamic.

  Folositi fisierul produse_si.txt.
  Cerinte:
  1. Cititi toate produsele din fisier.
  2. Afisati vectorul citit.
  3. Calculati valoarea totala a stocului.
  4. Adaugati un produs nou in vector.
  5. Dezalocati corect memoria.
*/

typedef struct Produs Produs;
struct Produs {
	int id;
	int stoc;
	float pret;
	char* denumire;
	char categorie;
};

Produs citireProdusDinFisier(FILE* fisier) {
	char buffer[128];
	char sep[3] = ",\n";
	Produs produs;
	produs.id = -1;
	produs.denumire = NULL;

	if (!fgets(buffer, 128, fisier)) {
		return produs;
	}

	char* token = strtok(buffer, sep);
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

void adaugaProdusInVector(Produs** vector, int* nrProduse, Produs produsNou) {
	Produs* temp = (Produs*)malloc(sizeof(Produs) * ((*nrProduse) + 1));
	for (int i = 0; i < *nrProduse; i++) {
		temp[i] = (*vector)[i];
	}
	temp[*nrProduse] = produsNou;
	free(*vector);
	*vector = temp;
	(*nrProduse)++;
}

Produs* citireVectorProduse(const char* numeFisier, int* nrProduse) {
	FILE* fisier = fopen(numeFisier, "r");
	Produs* produse = NULL;
	*nrProduse = 0;

	if (!fisier) {
		return NULL;
	}

	while (!feof(fisier)) {
		Produs produs = citireProdusDinFisier(fisier);
		if (produs.id != -1) {
			adaugaProdusInVector(&produse, nrProduse, produs);
		}
	}
	fclose(fisier);
	return produse;
}

void afisareProdus(Produs produs) {
	printf("%d | %s | stoc %d | pret %.2f | categorie %c\n",
		produs.id, produs.denumire, produs.stoc, produs.pret, produs.categorie);
}

void afisareVector(Produs* produse, int nrProduse) {
	for (int i = 0; i < nrProduse; i++) {
		afisareProdus(produse[i]);
	}
}

float calculeazaValoareTotala(Produs* produse, int nrProduse) {
	float suma = 0;
	for (int i = 0; i < nrProduse; i++) {
		suma += produse[i].pret * produse[i].stoc;
	}
	return suma;
}

void dezalocareVector(Produs** produse, int* nrProduse) {
	for (int i = 0; i < *nrProduse; i++) {
		free((*produse)[i].denumire);
	}
	free(*produse);
	*produse = NULL;
	*nrProduse = 0;
}

int main() {
	int nrProduse = 0;
	Produs* produse = citireVectorProduse("produse_si.txt", &nrProduse);

	afisareVector(produse, nrProduse);
	printf("Valoare totala stoc: %.2f\n", calculeazaValoareTotala(produse, nrProduse));

	Produs nou = { 99, 3, 349.99f, NULL, 'N' };
	nou.denumire = (char*)malloc(strlen("DockingStation") + 1);
	strcpy(nou.denumire, "DockingStation");
	adaugaProdusInVector(&produse, &nrProduse, nou);

	printf("\nDupa adaugare:\n");
	afisareVector(produse, nrProduse);

	dezalocareVector(&produse, &nrProduse);
	return 0;
}

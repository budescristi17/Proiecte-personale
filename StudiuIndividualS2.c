#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

/*
  Studiu individual S2
  Tema: vector dinamic de structuri cu deep copy.

  Cerinte:
  1. Creati un vector dinamic de produse.
  2. Copiati primele N elemente intr-un vector nou.
  3. Copiati produsele cu pret mai mare decat un prag.
  4. Cautati primul produs dintr-o anumita categorie.
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

Produs copiazaProdus(Produs produs) {
	return initializareProdus(produs.id, produs.stoc, produs.pret, produs.denumire, produs.categorie);
}

void afisareProdus(Produs produs) {
	printf("%d | %s | stoc %d | %.2f | %c\n",
		produs.id, produs.denumire, produs.stoc, produs.pret, produs.categorie);
}

void afisareVector(Produs* vector, int nrProduse) {
	for (int i = 0; i < nrProduse; i++) {
		afisareProdus(vector[i]);
	}
}

Produs* copiazaPrimeleN(Produs* vector, int nrProduse, int n) {
	if (n > nrProduse) {
		n = nrProduse;
	}
	Produs* copie = (Produs*)malloc(sizeof(Produs) * n);
	for (int i = 0; i < n; i++) {
		copie[i] = copiazaProdus(vector[i]);
	}
	return copie;
}

Produs* filtreazaProduseScumpe(Produs* vector, int nrProduse, float prag, int* nrFiltrate) {
	*nrFiltrate = 0;
	for (int i = 0; i < nrProduse; i++) {
		if (vector[i].pret > prag) {
			(*nrFiltrate)++;
		}
	}

	Produs* filtrate = (Produs*)malloc(sizeof(Produs) * (*nrFiltrate));
	int pozitie = 0;
	for (int i = 0; i < nrProduse; i++) {
		if (vector[i].pret > prag) {
			filtrate[pozitie++] = copiazaProdus(vector[i]);
		}
	}
	return filtrate;
}

Produs cautaPrimulDinCategorie(Produs* vector, int nrProduse, char categorie) {
	for (int i = 0; i < nrProduse; i++) {
		if (vector[i].categorie == categorie) {
			return copiazaProdus(vector[i]);
		}
	}
	return initializareProdus(-1, 0, 0, "Inexistent", '-');
}

void dezalocareVector(Produs** vector, int* nrProduse) {
	for (int i = 0; i < *nrProduse; i++) {
		free((*vector)[i].denumire);
	}
	free(*vector);
	*vector = NULL;
	*nrProduse = 0;
}

int main() {
	int nrProduse = 4;
	Produs* produse = (Produs*)malloc(sizeof(Produs) * nrProduse);
	produse[0] = initializareProdus(1, 12, 149.99f, "Tastatura", 'A');
	produse[1] = initializareProdus(2, 20, 89.50f, "Mouse", 'B');
	produse[2] = initializareProdus(3, 5, 2300.00f, "Laptop", 'A');
	produse[3] = initializareProdus(4, 7, 799.99f, "Monitor", 'C');

	afisareVector(produse, nrProduse);

	int nrCopiate = 2;
	Produs* primele = copiazaPrimeleN(produse, nrProduse, nrCopiate);
	printf("\nPrimele doua produse:\n");
	afisareVector(primele, nrCopiate);

	int nrFiltrate = 0;
	Produs* scumpe = filtreazaProduseScumpe(produse, nrProduse, 500, &nrFiltrate);
	printf("\nProduse cu pret peste prag:\n");
	afisareVector(scumpe, nrFiltrate);

	Produs gasit = cautaPrimulDinCategorie(produse, nrProduse, 'A');
	printf("\nPrimul produs din categoria A:\n");
	afisareProdus(gasit);
	free(gasit.denumire);

	dezalocareVector(&primele, &nrCopiate);
	dezalocareVector(&scumpe, &nrFiltrate);
	dezalocareVector(&produse, &nrProduse);
	return 0;
}

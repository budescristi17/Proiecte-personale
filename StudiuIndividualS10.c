#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

/*
  Studiu individual S10
  Tema: heap de produse.

  Cerinte:
  1. Creati un max-heap dupa pret.
  2. Inserati produse in heap.
  3. Extrageti produsul cu pret maxim.
  4. Afisati elementele vizibile si elementele extrase.
*/

typedef struct Produs Produs;
typedef struct Heap Heap;

struct Produs {
	int id;
	float pret;
	char* denumire;
};

struct Heap {
	Produs* vector;
	int lungime;
	int nrElemente;
};

Produs initializareProdus(int id, float pret, const char* denumire) {
	Produs produs;
	produs.id = id;
	produs.pret = pret;
	produs.denumire = (char*)malloc(strlen(denumire) + 1);
	strcpy(produs.denumire, denumire);
	return produs;
}

Heap initializareHeap(int lungime) {
	Heap heap;
	heap.lungime = lungime;
	heap.nrElemente = 0;
	heap.vector = (Produs*)malloc(sizeof(Produs) * lungime);
	return heap;
}

void interschimba(Produs* a, Produs* b) {
	Produs aux = *a;
	*a = *b;
	*b = aux;
}

void filtreaza(Heap heap, int pozitie) {
	int stanga = 2 * pozitie + 1;
	int dreapta = 2 * pozitie + 2;
	int maxim = pozitie;

	if (stanga < heap.nrElemente && heap.vector[stanga].pret > heap.vector[maxim].pret) {
		maxim = stanga;
	}
	if (dreapta < heap.nrElemente && heap.vector[dreapta].pret > heap.vector[maxim].pret) {
		maxim = dreapta;
	}
	if (maxim != pozitie) {
		interschimba(&heap.vector[pozitie], &heap.vector[maxim]);
		filtreaza(heap, maxim);
	}
}

void inserare(Heap* heap, Produs produs) {
	if (heap->nrElemente < heap->lungime) {
		heap->vector[heap->nrElemente++] = produs;
		for (int i = (heap->nrElemente - 2) / 2; i >= 0; i--) {
			filtreaza(*heap, i);
		}
	}
}

Produs extrageMaxim(Heap* heap) {
	Produs produs = { -1, 0, NULL };
	if (heap->nrElemente > 0) {
		produs = heap->vector[0];
		heap->vector[0] = heap->vector[heap->nrElemente - 1];
		heap->nrElemente--;
		filtreaza(*heap, 0);
	}
	return produs;
}

void afisareHeap(Heap heap) {
	for (int i = 0; i < heap.nrElemente; i++) {
		printf("%d | %s | %.2f\n", heap.vector[i].id, heap.vector[i].denumire, heap.vector[i].pret);
	}
}

void dezalocareHeap(Heap* heap) {
	for (int i = 0; i < heap->nrElemente; i++) {
		free(heap->vector[i].denumire);
	}
	free(heap->vector);
	heap->vector = NULL;
	heap->lungime = 0;
	heap->nrElemente = 0;
}

int main() {
	Heap heap = initializareHeap(6);
	inserare(&heap, initializareProdus(1, 149.99f, "Tastatura"));
	inserare(&heap, initializareProdus(2, 2300.00f, "Laptop"));
	inserare(&heap, initializareProdus(3, 799.99f, "Monitor"));
	inserare(&heap, initializareProdus(4, 89.50f, "Mouse"));

	printf("Heap initial:\n");
	afisareHeap(heap);

	Produs maxim = extrageMaxim(&heap);
	printf("\nProdus extras: %s %.2f\n", maxim.denumire, maxim.pret);
	free(maxim.denumire);

	printf("\nHeap dupa extragere:\n");
	afisareHeap(heap);

	dezalocareHeap(&heap);
	return 0;
}

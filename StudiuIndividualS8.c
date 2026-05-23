#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

/*
  Studiu individual S8
  Tema: stiva si coada.

  Cerinte:
  1. Implementati o stiva de documente folosind lista simplu inlantuita.
  2. Implementati o coada de documente folosind lista simplu inlantuita.
  3. Mutati documentele urgente din coada in stiva.
  4. Afisati ordinea de procesare.
*/

typedef struct Document Document;
typedef struct Nod Nod;

struct Document {
	int id;
	char* titlu;
	int prioritate;
};

struct Nod {
	Document info;
	Nod* next;
};

Document initializareDocument(int id, const char* titlu, int prioritate) {
	Document document;
	document.id = id;
	document.titlu = (char*)malloc(strlen(titlu) + 1);
	strcpy(document.titlu, titlu);
	document.prioritate = prioritate;
	return document;
}

void push(Nod** stiva, Document document) {
	Nod* nou = (Nod*)malloc(sizeof(Nod));
	nou->info = document;
	nou->next = *stiva;
	*stiva = nou;
}

Document pop(Nod** stiva) {
	Document document = { -1, NULL, 0 };
	if (*stiva) {
		Nod* temp = *stiva;
		document = temp->info;
		*stiva = temp->next;
		free(temp);
	}
	return document;
}

void enqueue(Nod** prim, Nod** ultim, Document document) {
	Nod* nou = (Nod*)malloc(sizeof(Nod));
	nou->info = document;
	nou->next = NULL;
	if (*ultim) {
		(*ultim)->next = nou;
	}
	else {
		*prim = nou;
	}
	*ultim = nou;
}

Document dequeue(Nod** prim, Nod** ultim) {
	Document document = { -1, NULL, 0 };
	if (*prim) {
		Nod* temp = *prim;
		document = temp->info;
		*prim = temp->next;
		if (!*prim) {
			*ultim = NULL;
		}
		free(temp);
	}
	return document;
}

void afisareDocument(Document document) {
	if (document.titlu) {
		printf("%d | %s | prioritate %d\n", document.id, document.titlu, document.prioritate);
	}
}

void dezalocareDocument(Document* document) {
	free(document->titlu);
	document->titlu = NULL;
}

int main() {
	Nod* coadaPrim = NULL;
	Nod* coadaUltim = NULL;
	Nod* urgente = NULL;

	enqueue(&coadaPrim, &coadaUltim, initializareDocument(1, "Contract", 2));
	enqueue(&coadaPrim, &coadaUltim, initializareDocument(2, "Factura", 5));
	enqueue(&coadaPrim, &coadaUltim, initializareDocument(3, "Cerere", 1));

	while (coadaPrim) {
		Document document = dequeue(&coadaPrim, &coadaUltim);
		if (document.prioritate >= 4) {
			push(&urgente, document);
		}
		else {
			afisareDocument(document);
			dezalocareDocument(&document);
		}
	}

	printf("\nDocumente urgente:\n");
	while (urgente) {
		Document document = pop(&urgente);
		afisareDocument(document);
		dezalocareDocument(&document);
	}
	return 0;
}

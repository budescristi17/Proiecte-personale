from __future__ import annotations

from dataclasses import dataclass
from random import Random


@dataclass
class RezultatGA:
    asezare: list[int]
    conflicte_vecine: int
    generatie_gasita: int
    evolutie_best: list[int]


def eticheta_oras(index: int) -> str:
    return f"O{index + 1}"


def construieste_matrice_conflict(n: int, muchii: list[tuple[int, int]]) -> list[list[int]]:
    matrice = [[0 for _ in range(n)] for _ in range(n)]

    for a, b in muchii:
        if not (0 <= a < n and 0 <= b < n):
            raise ValueError("Indicii oraselor trebuie sa fie intre 0 si n - 1.")
        if a == b:
            continue
        matrice[a][b] = 1
        matrice[b][a] = 1

    return matrice


def numara_conflicte_vecine(asezare: list[int], conflict: list[list[int]]) -> int:
    n = len(asezare)
    total = 0

    for i in range(n):
        oras_curent = asezare[i]
        oras_urmator = asezare[(i + 1) % n]
        if conflict[oras_curent][oras_urmator] == 1 or conflict[oras_urmator][oras_curent] == 1:
            total += 1

    return total


def fitness(asezare: list[int], conflict: list[list[int]]) -> int:
    return -numara_conflicte_vecine(asezare, conflict)


def selectie_turneu(
    populatie: list[list[int]],
    conflict: list[list[int]],
    aleator: Random,
    dimensiune_turneu: int,
) -> list[int]:
    candidati = aleator.sample(populatie, dimensiune_turneu)
    return max(candidati, key=lambda individ: fitness(individ, conflict))[:]


def crossover_ordonat(parinte1: list[int], parinte2: list[int], aleator: Random) -> list[int]:
    n = len(parinte1)
    stanga, dreapta = sorted(aleator.sample(range(n), 2))
    copil = [-1] * n
    copil[stanga : dreapta + 1] = parinte1[stanga : dreapta + 1]

    pozitie = (dreapta + 1) % n
    index = (dreapta + 1) % n

    while -1 in copil:
        gena = parinte2[index]
        if gena not in copil:
            copil[pozitie] = gena
            pozitie = (pozitie + 1) % n
        index = (index + 1) % n

    return copil


def mutatie_interschimbare(individ: list[int], aleator: Random) -> None:
    i, j = aleator.sample(range(len(individ)), 2)
    individ[i], individ[j] = individ[j], individ[i]


def valideaza_matrice(conflict: list[list[int]]) -> None:
    n = len(conflict)
    if n == 0:
        raise ValueError("Matricea de conflict nu poate fi vida.")

    for i, rand in enumerate(conflict):
        if len(rand) != n:
            raise ValueError("Matricea de conflict trebuie sa fie patratica.")
        if rand[i] != 0:
            raise ValueError("Elementele de pe diagonala principala trebuie sa fie 0.")


def algoritm_genetic_asezare(
    conflict: list[list[int]],
    dimensiune_populatie: int = 120,
    generatii_maxime: int = 1000,
    probabilitate_crossover: float = 0.9,
    probabilitate_mutatie: float = 0.2,
    dimensiune_turneu: int = 3,
    elitism: int = 2,
    seed: int = 7,
) -> RezultatGA:
    valideaza_matrice(conflict)
    aleator = Random(seed)
    n = len(conflict)
    baza = list(range(n))

    populatie = []
    for _ in range(dimensiune_populatie):
        individ = baza[:]
        aleator.shuffle(individ)
        populatie.append(individ)

    evolutie_best: list[int] = []
    cel_mai_bun = min(populatie, key=lambda individ: numara_conflicte_vecine(individ, conflict))[:]
    conflict_minim = numara_conflicte_vecine(cel_mai_bun, conflict)
    generatie_gasita = 0

    for generatie in range(generatii_maxime + 1):
        populatie.sort(key=lambda individ: numara_conflicte_vecine(individ, conflict))
        cel_mai_bun_curent = populatie[0]
        conflict_curent = numara_conflicte_vecine(cel_mai_bun_curent, conflict)
        evolutie_best.append(conflict_curent)

        if conflict_curent < conflict_minim:
            cel_mai_bun = cel_mai_bun_curent[:]
            conflict_minim = conflict_curent
            generatie_gasita = generatie

        if conflict_curent == 0:
            return RezultatGA(
                asezare=cel_mai_bun_curent[:],
                conflicte_vecine=0,
                generatie_gasita=generatie,
                evolutie_best=evolutie_best,
            )

        noua_populatie = [individ[:] for individ in populatie[:elitism]]

        while len(noua_populatie) < dimensiune_populatie:
            parinte1 = selectie_turneu(populatie, conflict, aleator, dimensiune_turneu)
            parinte2 = selectie_turneu(populatie, conflict, aleator, dimensiune_turneu)

            if aleator.random() < probabilitate_crossover:
                copil = crossover_ordonat(parinte1, parinte2, aleator)
            else:
                copil = parinte1[:]

            if aleator.random() < probabilitate_mutatie:
                mutatie_interschimbare(copil, aleator)

            noua_populatie.append(copil)

        populatie = noua_populatie

    return RezultatGA(
        asezare=cel_mai_bun,
        conflicte_vecine=conflict_minim,
        generatie_gasita=generatie_gasita,
        evolutie_best=evolutie_best,
    )


def afiseaza_asezare(asezare: list[int], conflict: list[list[int]]) -> None:
    n = len(asezare)
    print(
        "Asezare circulara gasita:",
        " -> ".join(eticheta_oras(oras) for oras in asezare),
        "->",
        eticheta_oras(asezare[0]),
    )
    print("Vecinatati la masa:")
    for i in range(n):
        a = asezare[i]
        b = asezare[(i + 1) % n]
        stare = "CONFLICT" if conflict[a][b] == 1 or conflict[b][a] == 1 else "OK"
        print(f"{eticheta_oras(a)} langa {eticheta_oras(b)}: {stare}")


def exemplu() -> None:
    n = 8
    rivalitati = [
        (0, 1),
        (1, 2),
        (2, 3),
        (3, 4),
        (4, 5),
        (5, 6),
        (6, 7),
        (0, 4),
        (1, 5),
        (2, 6),
    ]
    conflict = construieste_matrice_conflict(n, rivalitati)

    rezultat = algoritm_genetic_asezare(conflict, dimensiune_populatie=20, seed=7)

    print("Numar orase:", n)
    print("Conflicte intre orase:", [(eticheta_oras(a), eticheta_oras(b)) for a, b in rivalitati])
    print("Generatia in care s-a gasit cel mai bun individ:", rezultat.generatie_gasita)
    print("Numar conflicte intre vecini:", rezultat.conflicte_vecine)
    afiseaza_asezare(rezultat.asezare, conflict)
    print("Evolutia celui mai bun scor pe generatii:", rezultat.evolutie_best)


if __name__ == "__main__":
    exemplu()
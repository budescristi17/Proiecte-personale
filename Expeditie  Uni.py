from __future__ import annotations

from dataclasses import dataclass
from pathlib import Path
from random import Random


FISIER_DISTANTE = "matrice_distante_arhipelag.txt"
NUMAR_INSULE_DE_VIZITAT = 4
DISTANTA_MAXIMA = 120
EPS = 1e-9


@dataclass
class RezultatGA:
    permutare: list[int]
    insule_selectate: list[int]
    distanta_traseu: float
    scor_departare: float
    generatie_gasita: int
    evolutie_best: list[float]


def citeste_matrice(cale_fisier: str) -> list[list[float]]:
    linii = Path(cale_fisier).read_text(encoding="utf-8").splitlines()
    matrice = []
    for linie in linii:
        linie = linie.strip()
        if not linie:
            continue
        matrice.append([float(token) for token in linie.replace(",", " ").split()])

    if not matrice:
        raise ValueError("Fisierul cu distanta este gol.")
    dimensiune = len(matrice)
    if any(len(rand) != dimensiune for rand in matrice):
        raise ValueError("Matricea distantelor trebuie sa fie patratica.")
    return matrice


def distanta_traseu(permutare: list[int], matrice: list[list[float]], numar_insule: int) -> float:
    alese = permutare[:numar_insule]
    drum = [0, *alese, 0]
    return sum(matrice[a][b] for a, b in zip(drum, drum[1:]))


def scor_departare(permutare: list[int], matrice: list[list[float]], numar_insule: int) -> float:
    return sum(matrice[0][insula] for insula in permutare[:numar_insule])


def evalueaza(permutare: list[int], matrice: list[list[float]], numar_insule: int, distanta_maxima: float) -> tuple[float, float, float]:
    distanta = distanta_traseu(permutare, matrice, numar_insule)
    departare = scor_departare(permutare, matrice, numar_insule)

    if distanta <= distanta_maxima + EPS:
        scor = departare * 1000 - distanta
    else:
        scor = -(1_000_000 + (distanta - distanta_maxima) * 10_000 - departare)

    return scor, distanta, departare


def genereaza_individ_aleator(matrice: list[list[float]], aleator: Random) -> list[int]:
    insule = list(range(1, len(matrice)))
    if aleator.random() < 0.5:
        insule.sort(key=lambda index: matrice[0][index], reverse=True)
        for _ in range(3):
            i = aleator.randrange(len(insule))
            j = aleator.randrange(len(insule))
            insule[i], insule[j] = insule[j], insule[i]
        return insule

    aleator.shuffle(insule)
    return insule


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


def selectie_turneu(
    populatie: list[list[int]],
    matrice: list[list[float]],
    numar_insule: int,
    distanta_maxima: float,
    aleator: Random,
    dimensiune_turneu: int,
) -> list[int]:
    candidati = aleator.sample(populatie, dimensiune_turneu)
    return max(candidati, key=lambda individ: evalueaza(individ, matrice, numar_insule, distanta_maxima)[0])[:]


def imbunatateste_permutare(permutare: list[int], matrice: list[list[float]], numar_insule: int, distanta_maxima: float) -> None:
    while True:
        scor_curent, _, _ = evalueaza(permutare, matrice, numar_insule, distanta_maxima)
        cel_mai_bun = None
        scor_maxim = scor_curent

        for i in range(numar_insule):
            for j in range(i + 1, numar_insule):
                candidat = permutare[:]
                candidat[i], candidat[j] = candidat[j], candidat[i]
                scor_candidat, _, _ = evalueaza(candidat, matrice, numar_insule, distanta_maxima)
                if scor_candidat > scor_maxim + EPS:
                    cel_mai_bun = candidat
                    scor_maxim = scor_candidat

        for i in range(numar_insule):
            for j in range(numar_insule, len(permutare)):
                candidat = permutare[:]
                candidat[i], candidat[j] = candidat[j], candidat[i]
                scor_candidat, _, _ = evalueaza(candidat, matrice, numar_insule, distanta_maxima)
                if scor_candidat > scor_maxim + EPS:
                    cel_mai_bun = candidat
                    scor_maxim = scor_candidat

        if cel_mai_bun is None:
            break

        permutare[:] = cel_mai_bun


def mutatie(permutare: list[int], aleator: Random) -> None:
    numar_mutatii = aleator.randint(1, 3)
    for _ in range(numar_mutatii):
        i, j = aleator.sample(range(len(permutare)), 2)
        permutare[i], permutare[j] = permutare[j], permutare[i]


def algoritm_genetic_expeditie(
    matrice: list[list[float]],
    numar_insule: int = NUMAR_INSULE_DE_VIZITAT,
    distanta_maxima: float = DISTANTA_MAXIMA,
    dimensiune_populatie: int = 100,
    generatii_maxime: int = 300,
    probabilitate_crossover: float = 0.9,
    probabilitate_mutatie: float = 0.25,
    dimensiune_turneu: int = 3,
    elitism: int = 4,
    prag_stagnare: int = 35,
    seed: int = 47,
) -> RezultatGA:
    aleator = Random(seed)
    populatie = [genereaza_individ_aleator(matrice, aleator) for _ in range(dimensiune_populatie)]
    for individ in populatie:
        imbunatateste_permutare(individ, matrice, numar_insule, distanta_maxima)

    cel_mai_bun = max(populatie, key=lambda individ: evalueaza(individ, matrice, numar_insule, distanta_maxima)[0])[:]
    scor_best, distanta_best, departare_best = evalueaza(cel_mai_bun, matrice, numar_insule, distanta_maxima)
    generatie_gasita = 0
    evolutie_best: list[float] = []
    stagnare = 0

    for generatie in range(generatii_maxime + 1):
        populatie.sort(key=lambda individ: evalueaza(individ, matrice, numar_insule, distanta_maxima)[0], reverse=True)
        _, _, departare_curenta = evalueaza(populatie[0], matrice, numar_insule, distanta_maxima)
        evolutie_best.append(departare_curenta)

        scor_curent, _, _ = evalueaza(populatie[0], matrice, numar_insule, distanta_maxima)
        if scor_curent > scor_best + EPS:
            cel_mai_bun = populatie[0][:]
            scor_best, distanta_best, departare_best = evalueaza(cel_mai_bun, matrice, numar_insule, distanta_maxima)
            generatie_gasita = generatie
            stagnare = 0
        else:
            stagnare += 1

        noua_populatie = [individ[:] for individ in populatie[:elitism]]

        while len(noua_populatie) < dimensiune_populatie:
            parinte1 = selectie_turneu(populatie, matrice, numar_insule, distanta_maxima, aleator, dimensiune_turneu)
            parinte2 = selectie_turneu(populatie, matrice, numar_insule, distanta_maxima, aleator, dimensiune_turneu)

            if aleator.random() < probabilitate_crossover:
                copil = crossover_ordonat(parinte1, parinte2, aleator)
            else:
                copil = parinte1[:]

            if aleator.random() < probabilitate_mutatie:
                mutatie(copil, aleator)

            imbunatateste_permutare(copil, matrice, numar_insule, distanta_maxima)
            noua_populatie.append(copil)

        populatie = noua_populatie

        if stagnare >= prag_stagnare:
            break

    return RezultatGA(
        permutare=cel_mai_bun,
        insule_selectate=cel_mai_bun[:numar_insule],
        distanta_traseu=distanta_best,
        scor_departare=departare_best,
        generatie_gasita=generatie_gasita,
        evolutie_best=evolutie_best,
    )


def afiseaza_rezultat(rezultat: RezultatGA) -> None:
    traseu = ["Port"] + [f"I{insula}" for insula in rezultat.insule_selectate] + ["Port"]
    print("Traseu:", " -> ".join(traseu))
    print("Insule selectate:", [f"I{insula}" for insula in rezultat.insule_selectate])


def exemplu() -> None:
    matrice = citeste_matrice(FISIER_DISTANTE)
    rezultat = algoritm_genetic_expeditie(matrice)

    print("=== EXPEDITIE INSULE CU ALGORITM GENETIC ===")
    print("Numar total insule candidate:", len(matrice) - 1)
    print("Numar insule de vizitat:", NUMAR_INSULE_DE_VIZITAT)
    print("Distanta maxima admisa:", DISTANTA_MAXIMA)
    print("Scor de departare obtinut:", f"{rezultat.scor_departare:.2f}")
    print("Distanta totala a traseului:", f"{rezultat.distanta_traseu:.2f}")
    print("Generatia in care s-a fixat cel mai bun individ:", rezultat.generatie_gasita)
    afiseaza_rezultat(rezultat)
    print("Primele valori din evolutie:", [round(valoare, 2) for valoare in rezultat.evolutie_best[:20]])


if __name__ == "__main__":
    exemplu()
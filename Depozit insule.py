from __future__ import annotations

from dataclasses import dataclass
from pathlib import Path
from random import Random


FISIER_DISTANTE = "matrice_distante_statii.txt"
NUMAR_DEPOZITE = 3
EPS = 1e-9


@dataclass
class RezultatGA:
    depozite: list[int]
    distanta_totala: float
    repartizare: dict[int, list[int]]
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
        raise ValueError("Fisierul cu distante este gol.")
    dimensiune = len(matrice)
    if any(len(rand) != dimensiune for rand in matrice):
        raise ValueError("Matricea distantelor trebuie sa fie patratica.")
    return matrice


def distanta_totala(depozite: list[int], matrice: list[list[float]]) -> float:
    total = 0.0
    for insula in range(len(matrice)):
        total += min(matrice[insula][depozit] for depozit in depozite)
    return total


def repartizare_insule(depozite: list[int], matrice: list[list[float]]) -> dict[int, list[int]]:
    repartizare = {depozit: [] for depozit in depozite}
    for insula in range(len(matrice)):
        depozit = min(depozite, key=lambda candidat: matrice[insula][candidat])
        repartizare[depozit].append(insula)
    return repartizare


def evalueaza(depozite: list[int], matrice: list[list[float]]) -> tuple[float, float]:
    total = distanta_totala(depozite, matrice)
    return -total, total


def genereaza_individ_aleator(matrice: list[list[float]], aleator: Random) -> list[int]:
    insule = list(range(len(matrice)))
    if aleator.random() < 0.5:
        suma_distante = [(sum(matrice[i][j] for j in range(len(matrice))), i) for i in range(len(matrice))]
        suma_distante.sort()
        alese = [index for _, index in suma_distante[:NUMAR_DEPOZITE]]
        aleator.shuffle(alese)
        return sorted(alese)

    aleator.shuffle(insule)
    return sorted(insule[:NUMAR_DEPOZITE])


def selectie_turneu(
    populatie: list[list[int]],
    matrice: list[list[float]],
    aleator: Random,
    dimensiune_turneu: int,
) -> list[int]:
    candidati = aleator.sample(populatie, dimensiune_turneu)
    return max(candidati, key=lambda individ: evalueaza(individ, matrice)[0])[:]


def crossover_seturi(parinte1: list[int], parinte2: list[int], aleator: Random, numar_insule: int) -> list[int]:
    copil = []
    for gena in parinte1:
        if gena in parinte2 and gena not in copil:
            copil.append(gena)

    reuniune = [gena for gena in parinte1 + parinte2 if gena not in copil]
    aleator.shuffle(reuniune)
    for gena in reuniune:
        if len(copil) == NUMAR_DEPOZITE:
            break
        copil.append(gena)

    rest = [insula for insula in range(numar_insule) if insula not in copil]
    aleator.shuffle(rest)
    while len(copil) < NUMAR_DEPOZITE:
        copil.append(rest.pop())

    return sorted(copil)


def imbunatateste_depozite(depozite: list[int], matrice: list[list[float]]) -> None:
    while True:
        scor_curent, _ = evalueaza(depozite, matrice)
        cel_mai_bun = None
        scor_maxim = scor_curent
        nealese = [insula for insula in range(len(matrice)) if insula not in depozite]

        for pozitie in range(NUMAR_DEPOZITE):
            for candidat in nealese:
                nou = depozite[:]
                nou[pozitie] = candidat
                nou.sort()
                scor_nou, _ = evalueaza(nou, matrice)
                if scor_nou > scor_maxim + EPS:
                    cel_mai_bun = nou
                    scor_maxim = scor_nou

        if cel_mai_bun is None:
            break

        depozite[:] = cel_mai_bun


def mutatie(depozite: list[int], numar_insule: int, aleator: Random) -> None:
    pozitie = aleator.randrange(NUMAR_DEPOZITE)
    candidati = [insula for insula in range(numar_insule) if insula not in depozite]
    depozite[pozitie] = aleator.choice(candidati)
    depozite.sort()


def algoritm_genetic_depozite(
    matrice: list[list[float]],
    dimensiune_populatie: int = 80,
    generatii_maxime: int = 250,
    probabilitate_crossover: float = 0.9,
    probabilitate_mutatie: float = 0.25,
    dimensiune_turneu: int = 3,
    elitism: int = 4,
    prag_stagnare: int = 35,
    seed: int = 53,
) -> RezultatGA:
    aleator = Random(seed)
    populatie = [genereaza_individ_aleator(matrice, aleator) for _ in range(dimensiune_populatie)]
    for individ in populatie:
        imbunatateste_depozite(individ, matrice)

    cel_mai_bun = max(populatie, key=lambda individ: evalueaza(individ, matrice)[0])[:]
    scor_best, total_best = evalueaza(cel_mai_bun, matrice)
    generatie_gasita = 0
    evolutie_best: list[float] = []
    stagnare = 0

    for generatie in range(generatii_maxime + 1):
        populatie.sort(key=lambda individ: evalueaza(individ, matrice)[0], reverse=True)
        _, total_curent = evalueaza(populatie[0], matrice)
        evolutie_best.append(total_curent)

        scor_curent, _ = evalueaza(populatie[0], matrice)
        if scor_curent > scor_best + EPS:
            cel_mai_bun = populatie[0][:]
            scor_best, total_best = evalueaza(cel_mai_bun, matrice)
            generatie_gasita = generatie
            stagnare = 0
        else:
            stagnare += 1

        noua_populatie = [individ[:] for individ in populatie[:elitism]]

        while len(noua_populatie) < dimensiune_populatie:
            parinte1 = selectie_turneu(populatie, matrice, aleator, dimensiune_turneu)
            parinte2 = selectie_turneu(populatie, matrice, aleator, dimensiune_turneu)

            if aleator.random() < probabilitate_crossover:
                copil = crossover_seturi(parinte1, parinte2, aleator, len(matrice))
            else:
                copil = parinte1[:]

            if aleator.random() < probabilitate_mutatie:
                mutatie(copil, len(matrice), aleator)

            imbunatateste_depozite(copil, matrice)
            noua_populatie.append(copil)

        populatie = noua_populatie

        if stagnare >= prag_stagnare:
            break

    return RezultatGA(
        depozite=cel_mai_bun,
        distanta_totala=total_best,
        repartizare=repartizare_insule(cel_mai_bun, matrice),
        generatie_gasita=generatie_gasita,
        evolutie_best=evolutie_best,
    )


def exemplu() -> None:
    matrice = citeste_matrice(FISIER_DISTANTE)
    rezultat = algoritm_genetic_depozite(matrice)

    print("=== ALEGEREA DEPOZITELOR PE INSULE CU ALGORITM GENETIC ===")
    print("Depozite alese:", [f"I{insula + 1}" for insula in rezultat.depozite])
    print("Distanta totala minima gasita:", f"{rezultat.distanta_totala:.2f}")
    print("Generatia in care s-a fixat cel mai bun individ:", rezultat.generatie_gasita)
    print("Repartizarea insulelor la depozitul cel mai apropiat:")
    for depozit, insule in rezultat.repartizare.items():
        etichete = [f"I{insula + 1}" for insula in insule]
        print(f"  Depozit I{depozit + 1}: {etichete}")
    print("Primele valori din evolutie:", [round(valoare, 2) for valoare in rezultat.evolutie_best[:20]])


if __name__ == "__main__":
    exemplu()
from __future__ import annotations

from dataclasses import dataclass
from random import Random


BUGET = 10_000
COSTURI = (1000, 800, 1500)
CREDITE = (5, 3, 8)
ORE = (80, 40, 100)
PRAG_ORE = 70
EPS = 1e-9


@dataclass
class Evaluare:
    a: int
    b: int
    c: int
    cost_total: int
    total_cursuri: int
    total_credite: int
    credite_medii: float
    ore_medii: float
    fezabil: bool
    scor: float


@dataclass
class RezultatGA:
    a: int
    b: int
    c: int
    cost_total: int
    total_credite: int
    credite_medii: float
    ore_medii: float
    generatie_gasita: int
    evolutie_best: list[float]


def cost_total(individ: list[int]) -> int:
    return sum(gene * cost for gene, cost in zip(individ, COSTURI))


def total_credite(individ: list[int]) -> int:
    return sum(gene * credit for gene, credit in zip(individ, CREDITE))


def credite_medii(individ: list[int]) -> float:
    total_cursuri = sum(individ)
    if total_cursuri == 0:
        return 0.0
    return total_credite(individ) / total_cursuri


def ore_medii(individ: list[int]) -> float:
    total_cursuri = sum(individ)
    if total_cursuri == 0:
        return 0.0
    return sum(gene * ore for gene, ore in zip(individ, ORE)) / total_cursuri


def evalueaza(individ: list[int]) -> Evaluare:
    a, b, c = individ
    total_c = a + b + c
    cost = cost_total(individ)
    total_cr = total_credite(individ)
    medie_credite = credite_medii(individ)
    medie_ore = ore_medii(individ)

    fezabil = total_c > 0 and cost <= BUGET and medie_ore <= PRAG_ORE + EPS

    if fezabil:
        scor = medie_credite * 10_000 + total_cr
    else:
        penalizare = 1_000_000 + max(0, cost - BUGET) * 100 + max(0.0, medie_ore - PRAG_ORE) * 10_000
        if total_c == 0:
            penalizare += 100_000
        scor = -penalizare

    return Evaluare(
        a=a,
        b=b,
        c=c,
        cost_total=cost,
        total_cursuri=total_c,
        total_credite=total_cr,
        credite_medii=medie_credite,
        ore_medii=medie_ore,
        fezabil=fezabil,
        scor=scor,
    )


def repara_individ(individ: list[int], aleator: Random) -> None:
    for i in range(3):
        individ[i] = max(0, int(individ[i]))

    while cost_total(individ) > BUGET:
        pozitii = [i for i, valoare in enumerate(individ) if valoare > 0]
        if not pozitii:
            break
        i = aleator.choice(pozitii)
        individ[i] -= 1

    if sum(individ) == 0:
        individ[aleator.randrange(3)] = 1
        while cost_total(individ) > BUGET:
            i = aleator.randrange(3)
            individ[i] = 0
        if sum(individ) == 0:
            individ[1] = 1


def genereaza_individ_aleator(aleator: Random) -> list[int]:
    if aleator.random() < 0.5:
        perechi = aleator.randint(0, min(BUGET // 2300, 4))
        individ = [0, perechi, perechi]
        individ[aleator.randrange(3)] += aleator.randint(0, 2)
    else:
        limite = [BUGET // cost for cost in COSTURI]
        individ = [aleator.randint(0, limita) for limita in limite]

    repara_individ(individ, aleator)
    return individ


def selectie_turneu(populatie: list[list[int]], aleator: Random, dimensiune_turneu: int) -> list[int]:
    candidati = aleator.sample(populatie, dimensiune_turneu)
    return max(candidati, key=lambda individ: evalueaza(individ).scor)[:]


def crossover_unipunct(parinte1: list[int], parinte2: list[int], aleator: Random) -> list[int]:
    punct = aleator.randint(1, 2)
    copil = parinte1[:punct] + parinte2[punct:]
    repara_individ(copil, aleator)
    return copil


def mutatie(individ: list[int], aleator: Random) -> None:
    operatie = aleator.choice(["adauga", "sterge", "transfera", "pachet"])

    if operatie == "adauga":
        i = aleator.randrange(3)
        individ[i] += aleator.randint(1, 2)
    elif operatie == "sterge":
        i = aleator.randrange(3)
        individ[i] = max(0, individ[i] - aleator.randint(1, 2))
    elif operatie == "transfera":
        sursa = aleator.randrange(3)
        destinatie = aleator.randrange(3)
        while destinatie == sursa:
            destinatie = aleator.randrange(3)
        cantitate = min(individ[sursa], aleator.randint(1, 2))
        individ[sursa] -= cantitate
        individ[destinatie] += cantitate
    else:
        individ[1] += 1
        individ[2] += 1

    repara_individ(individ, aleator)


def algoritm_genetic_cursuri(
    dimensiune_populatie: int = 100,
    generatii_maxime: int = 300,
    probabilitate_crossover: float = 0.9,
    probabilitate_mutatie: float = 0.25,
    dimensiune_turneu: int = 3,
    elitism: int = 4,
    seed: int = 17,
) -> RezultatGA:
    aleator = Random(seed)
    populatie = [genereaza_individ_aleator(aleator) for _ in range(dimensiune_populatie)]

    cel_mai_bun = max(populatie, key=lambda individ: evalueaza(individ).scor)[:]
    evaluare_best = evalueaza(cel_mai_bun)
    generatie_gasita = 0
    evolutie_best: list[float] = []

    for generatie in range(generatii_maxime + 1):
        populatie.sort(key=lambda individ: evalueaza(individ).scor, reverse=True)
        evaluare_curenta = evalueaza(populatie[0])
        evolutie_best.append(evaluare_curenta.credite_medii if evaluare_curenta.fezabil else evaluare_curenta.scor)

        if evaluare_curenta.scor > evaluare_best.scor + EPS:
            cel_mai_bun = populatie[0][:]
            evaluare_best = evaluare_curenta
            generatie_gasita = generatie

        noua_populatie = [individ[:] for individ in populatie[:elitism]]

        while len(noua_populatie) < dimensiune_populatie:
            parinte1 = selectie_turneu(populatie, aleator, dimensiune_turneu)
            parinte2 = selectie_turneu(populatie, aleator, dimensiune_turneu)

            if aleator.random() < probabilitate_crossover:
                copil = crossover_unipunct(parinte1, parinte2, aleator)
            else:
                copil = parinte1[:]

            if aleator.random() < probabilitate_mutatie:
                mutatie(copil, aleator)

            noua_populatie.append(copil)

        populatie = noua_populatie

    rezultat = evalueaza(cel_mai_bun)
    return RezultatGA(
        a=rezultat.a,
        b=rezultat.b,
        c=rezultat.c,
        cost_total=rezultat.cost_total,
        total_credite=rezultat.total_credite,
        credite_medii=rezultat.credite_medii,
        ore_medii=rezultat.ore_medii,
        generatie_gasita=generatie_gasita,
        evolutie_best=evolutie_best,
    )


def exemplu() -> None:
    rezultat = algoritm_genetic_cursuri()

    print("Buget maxim:", BUGET)
    print("Solutie gasita:")
    print(f"Tip a = {rezultat.a}")
    print(f"Tip b = {rezultat.b}")
    print(f"Tip c = {rezultat.c}")
    print("Cost total:", rezultat.cost_total)
    print("Total credite:", rezultat.total_credite)
    print("Credite medii:", f"{rezultat.credite_medii:.2f}")
    print("Ore medii de studiu:", f"{rezultat.ore_medii:.2f}")
    print("Generatia in care s-a fixat cel mai bun individ:", rezultat.generatie_gasita)
    print("Primele valori din evolutie:", [round(valoare, 2) for valoare in rezultat.evolutie_best[:20]])


if __name__ == "__main__":
    exemplu()
from __future__ import annotations

from dataclasses import dataclass
from random import Random


CAPACITATE_BUCURESTI = 120
CAPACITATE_CRAIOVA = 140
DEPOZITE = ("Ploiesti", "Pitesti", "Cluj")
CERERI = (100, 60, 80)
COSTURI_BUCURESTI = (50, 70, 90)
COSTURI_CRAIOVA = (60, 70, 100)
EPS = 1e-9


@dataclass
class Evaluare:
    bucuresti: list[int]
    craiova: list[int]
    cost_total: int
    fezabil: bool
    scor: float


@dataclass
class RezultatGA:
    bucuresti: list[int]
    craiova: list[int]
    cost_total: int
    generatie_gasita: int
    evolutie_best: list[int]


def cost_transport(bucuresti: list[int]) -> int:
    craiova = [cerere - livrare for cerere, livrare in zip(CERERI, bucuresti)]
    cost_b = sum(cantitate * cost for cantitate, cost in zip(bucuresti, COSTURI_BUCURESTI))
    cost_c = sum(cantitate * cost for cantitate, cost in zip(craiova, COSTURI_CRAIOVA))
    return cost_b + cost_c


def repara_individ(individ: list[int], aleator: Random) -> None:
    for i in range(3):
        individ[i] = min(max(0, int(individ[i])), CERERI[i])

    while sum(individ) > CAPACITATE_BUCURESTI:
        pozitii = [i for i, valoare in enumerate(individ) if valoare > 0]
        i = aleator.choice(pozitii)
        individ[i] -= 1

    while sum(individ) < sum(CERERI) - CAPACITATE_CRAIOVA:
        pozitii = [i for i in range(3) if individ[i] < CERERI[i]]
        i = aleator.choice(pozitii)
        individ[i] += 1


def evalueaza(individ: list[int]) -> Evaluare:
    bucuresti = individ[:]
    craiova = [cerere - livrare for cerere, livrare in zip(CERERI, bucuresti)]
    fezabil = (
        all(0 <= livrare <= cerere for livrare, cerere in zip(bucuresti, CERERI))
        and sum(bucuresti) <= CAPACITATE_BUCURESTI
        and sum(craiova) <= CAPACITATE_CRAIOVA
    )
    cost = cost_transport(bucuresti)

    if fezabil:
        scor = -float(cost)
    else:
        penalizare = 1_000_000 + max(0, sum(bucuresti) - CAPACITATE_BUCURESTI) * 1000
        penalizare += max(0, sum(craiova) - CAPACITATE_CRAIOVA) * 1000
        scor = -penalizare

    return Evaluare(
        bucuresti=bucuresti,
        craiova=craiova,
        cost_total=cost,
        fezabil=fezabil,
        scor=scor,
    )


def genereaza_individ_aleator(aleator: Random) -> list[int]:
    total_b = aleator.randint(sum(CERERI) - CAPACITATE_CRAIOVA, CAPACITATE_BUCURESTI)
    individ = [0, 0, 0]

    for _ in range(total_b):
        pozitii = [i for i in range(3) if individ[i] < CERERI[i]]
        i = aleator.choice(pozitii)
        individ[i] += 1

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
    operatie = aleator.choice(["muta", "adauga", "sterge"])

    if operatie == "muta":
        sursa = aleator.randrange(3)
        destinatie = aleator.randrange(3)
        while destinatie == sursa:
            destinatie = aleator.randrange(3)
        cantitate = min(individ[sursa], aleator.randint(1, 10))
        individ[sursa] -= cantitate
        individ[destinatie] += cantitate
    elif operatie == "adauga":
        i = aleator.randrange(3)
        individ[i] += aleator.randint(1, 10)
    else:
        i = aleator.randrange(3)
        individ[i] = max(0, individ[i] - aleator.randint(1, 10))

    repara_individ(individ, aleator)


def algoritm_genetic_transport(
    dimensiune_populatie: int = 80,
    generatii_maxime: int = 250,
    probabilitate_crossover: float = 0.9,
    probabilitate_mutatie: float = 0.25,
    dimensiune_turneu: int = 3,
    elitism: int = 4,
    seed: int = 23,
) -> RezultatGA:
    aleator = Random(seed)
    populatie = [genereaza_individ_aleator(aleator) for _ in range(dimensiune_populatie)]

    cel_mai_bun = max(populatie, key=lambda individ: evalueaza(individ).scor)[:]
    evaluare_best = evalueaza(cel_mai_bun)
    generatie_gasita = 0
    evolutie_best: list[int] = []

    for generatie in range(generatii_maxime + 1):
        populatie.sort(key=lambda individ: evalueaza(individ).scor, reverse=True)
        evaluare_curenta = evalueaza(populatie[0])
        evolutie_best.append(evaluare_curenta.cost_total)

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
        bucuresti=rezultat.bucuresti,
        craiova=rezultat.craiova,
        cost_total=rezultat.cost_total,
        generatie_gasita=generatie_gasita,
        evolutie_best=evolutie_best,
    )


def afiseaza_transport(rezultat: RezultatGA) -> None:
    print("Bucuresti ->")
    for depozit, cantitate in zip(DEPOZITE, rezultat.bucuresti):
        print(f"  {depozit}: {cantitate} tone")

    print("Craiova ->")
    for depozit, cantitate in zip(DEPOZITE, rezultat.craiova):
        print(f"  {depozit}: {cantitate} tone")


def exemplu() -> None:
    rezultat = algoritm_genetic_transport()

    print("=== TRANSPORT FABRICI - DEPOZITE CU ALGORITM GENETIC ===")
    print("Capacitate Bucuresti:", CAPACITATE_BUCURESTI)
    print("Capacitate Craiova:", CAPACITATE_CRAIOVA)
    print("Cereri depozite:", dict(zip(DEPOZITE, CERERI)))
    print("Cost total minim gasit:", rezultat.cost_total)
    print("Generatia in care s-a fixat cel mai bun individ:", rezultat.generatie_gasita)
    afiseaza_transport(rezultat)
    print("Primele valori din evolutie:", rezultat.evolutie_best[:20])


if __name__ == "__main__":
    exemplu()
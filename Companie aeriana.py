from __future__ import annotations

from dataclasses import dataclass
from random import Random


BUGET = 5000
COSTURI = (100, 60, 50)
AUTONOMII = (6000, 4200, 2800)
VIZIBILITATI = (1500, 2400, 1600)
PRAG_VIZIBILITATE = 2000
EPS = 1e-9


@dataclass
class Evaluare:
    a: int
    b: int
    c: int
    cost_total: int
    total_avioane: int
    autonomie_medie: float
    vizibilitate_medie: float
    fezabil: bool
    scor: float


@dataclass
class RezultatGA:
    a: int
    b: int
    c: int
    cost_total: int
    autonomie_medie: float
    vizibilitate_medie: float
    generatie_gasita: int
    evolutie_best: list[float]


def cost_total(individ: list[int]) -> int:
    return sum(gene * cost for gene, cost in zip(individ, COSTURI))


def autonomie_medie(individ: list[int]) -> float:
    total = sum(individ)
    if total == 0:
        return 0.0
    return sum(gene * autonomie for gene, autonomie in zip(individ, AUTONOMII)) / total


def vizibilitate_medie(individ: list[int]) -> float:
    total = sum(individ)
    if total == 0:
        return 0.0
    return sum(gene * vizibilitate for gene, vizibilitate in zip(individ, VIZIBILITATI)) / total


def evalueaza(individ: list[int]) -> Evaluare:
    a, b, c = individ
    total = a + b + c
    cost = cost_total(individ)
    autonomie = autonomie_medie(individ)
    vizibilitate = vizibilitate_medie(individ)

    depasire_buget = max(0, cost - BUGET)
    lipsa_vizibilitate = max(0.0, PRAG_VIZIBILITATE - vizibilitate + EPS)
    fezabil = total > 0 and depasire_buget == 0 and vizibilitate > PRAG_VIZIBILITATE

    if fezabil:
        scor = autonomie
    else:
        penalizare = 1_000_000 + depasire_buget * 1_000 + lipsa_vizibilitate * 10_000
        if total == 0:
            penalizare += 100_000
        scor = -penalizare

    return Evaluare(
        a=a,
        b=b,
        c=c,
        cost_total=cost,
        total_avioane=total,
        autonomie_medie=autonomie,
        vizibilitate_medie=vizibilitate,
        fezabil=fezabil,
        scor=scor,
    )


def genereaza_individ_aleator(aleator: Random) -> list[int]:
    if aleator.random() < 0.5:
        pachete = aleator.randint(0, BUGET // 700)
        individ = [4 * pachete, 5 * pachete, 0]
        individ[aleator.randrange(3)] += aleator.randint(0, 4)
        individ[aleator.randrange(3)] += aleator.randint(0, 3)
    else:
        limite = [BUGET // cost for cost in COSTURI]
        individ = [aleator.randint(0, limita) for limita in limite]

    repara_individ(individ, aleator)
    return individ


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
        tip = aleator.randrange(3)
        individ[tip] = 1
        while cost_total(individ) > BUGET:
            individ[tip] -= 1
            tip = aleator.randrange(3)
            individ[tip] = 1


def imbunatateste_individ(individ: list[int], aleator: Random) -> None:
    pasi = [
        [4, 5, 0],
        [1, 1, 0],
        [0, 1, -1],
        [0, 1, 0],
        [1, 0, 0],
        [0, 0, 1],
    ]

    while True:
        evaluare_curenta = evalueaza(individ)
        cel_mai_bun = individ[:]
        scor_maxim = evaluare_curenta.scor

        for pas in pasi:
            candidat = [individ[i] + pas[i] for i in range(3)]
            if any(valoare < 0 for valoare in candidat):
                continue
            repara_individ(candidat, aleator)
            scor_candidat = evalueaza(candidat).scor
            if scor_candidat > scor_maxim + EPS:
                cel_mai_bun = candidat
                scor_maxim = scor_candidat

        if cel_mai_bun == individ:
            break

        individ[:] = cel_mai_bun


def selectie_turneu(
    populatie: list[list[int]],
    aleator: Random,
    dimensiune_turneu: int,
) -> list[int]:
    candidati = aleator.sample(populatie, dimensiune_turneu)
    return max(candidati, key=lambda individ: evalueaza(individ).scor)[:]


def crossover_unipunct(parinte1: list[int], parinte2: list[int], aleator: Random) -> list[int]:
    punct = aleator.randint(1, 2)
    copil = parinte1[:punct] + parinte2[punct:]
    repara_individ(copil, aleator)
    imbunatateste_individ(copil, aleator)
    return copil


def mutatie(individ: list[int], aleator: Random) -> None:
    operatie = aleator.choice(["adauga", "sterge", "transfera", "reseteaza", "pachet"])

    if operatie == "adauga":
        tip = aleator.randrange(3)
        cantitate = aleator.randint(1, 4)
        individ[tip] += cantitate
    elif operatie == "sterge":
        tip = aleator.randrange(3)
        cantitate = aleator.randint(1, 4)
        individ[tip] = max(0, individ[tip] - cantitate)
    elif operatie == "transfera":
        sursa = aleator.randrange(3)
        destinatie = aleator.randrange(3)
        while destinatie == sursa:
            destinatie = aleator.randrange(3)
        cantitate = min(individ[sursa], aleator.randint(1, 4))
        individ[sursa] -= cantitate
        individ[destinatie] += cantitate
    elif operatie == "pachet":
        cantitate = aleator.randint(1, 3)
        individ[0] += 4 * cantitate
        individ[1] += 5 * cantitate
    else:
        tip = aleator.randrange(3)
        limite = [BUGET // cost for cost in COSTURI]
        individ[tip] = aleator.randint(0, limite[tip])

    repara_individ(individ, aleator)
    imbunatateste_individ(individ, aleator)


def algoritm_genetic_aeronave(
    dimensiune_populatie: int = 120,
    generatii_maxime: int = 500,
    probabilitate_crossover: float = 0.9,
    probabilitate_mutatie: float = 0.25,
    dimensiune_turneu: int = 3,
    elitism: int = 4,
    seed: int = 11,
) -> RezultatGA:
    aleator = Random(seed)
    populatie = [genereaza_individ_aleator(aleator) for _ in range(dimensiune_populatie)]
    for individ in populatie:
        imbunatateste_individ(individ, aleator)

    cel_mai_bun = max(populatie, key=lambda individ: evalueaza(individ).scor)[:]
    evaluare_best = evalueaza(cel_mai_bun)
    generatie_gasita = 0
    evolutie_best: list[float] = []

    for generatie in range(generatii_maxime + 1):
        populatie.sort(key=lambda individ: evalueaza(individ).scor, reverse=True)
        evaluare_curenta = evalueaza(populatie[0])
        evolutie_best.append(evaluare_curenta.autonomie_medie if evaluare_curenta.fezabil else evaluare_curenta.scor)

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
        autonomie_medie=rezultat.autonomie_medie,
        vizibilitate_medie=rezultat.vizibilitate_medie,
        generatie_gasita=generatie_gasita,
        evolutie_best=evolutie_best,
    )


def exemplu() -> None:
    rezultat = algoritm_genetic_aeronave()

    print("Buget maxim:", BUGET)
    print("Solutie gasita:")
    print(f"Tip I   (a) = {rezultat.a}")
    print(f"Tip II  (b) = {rezultat.b}")
    print(f"Tip III (c) = {rezultat.c}")
    print("Cost total:", rezultat.cost_total)
    print("Autonomie medie:", f"{rezultat.autonomie_medie:.2f}", "km")
    print("Vizibilitate medie:", f"{rezultat.vizibilitate_medie:.2f}", "m")
    print("Generatia in care s-a fixat cel mai bun individ:", rezultat.generatie_gasita)
    print("Primele valori din evolutie:", [round(valoare, 2) for valoare in rezultat.evolutie_best[:20]])


if __name__ == "__main__":
    exemplu()
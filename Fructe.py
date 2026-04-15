from __future__ import annotations

from dataclasses import dataclass
from random import Random


RESURSE = {
    "smochine": 100_000,
    "ananas": 80_000,
    "curmale": 120_000,
    "merisor": 50_000,
}

RETETE = [
    {"smochine": 60, "ananas": 50, "curmale": 50, "merisor": 40},
    {"smochine": 50, "ananas": 0, "curmale": 150, "merisor": 0},
    {"smochine": 50, "ananas": 50, "curmale": 50, "merisor": 50},
    {"smochine": 0, "ananas": 0, "curmale": 200, "merisor": 0},
    {"smochine": 200, "ananas": 0, "curmale": 0, "merisor": 0},
]
PROFITURI = [20, 10, 15, 12, 5]
ETICHETE = ["C1", "C2", "C3", "C4", "C5"]
EPS = 1e-9


@dataclass
class Evaluare:
    cantitati: list[int]
    consum: dict[str, int]
    profit_total: int
    fezabil: bool
    scor: float


@dataclass
class RezultatGA:
    cantitati: list[int]
    profit_total: int
    consum: dict[str, int]
    generatie_gasita: int
    evolutie_best: list[float]


def calculeaza_consum(cantitati: list[int]) -> dict[str, int]:
    consum = {resursa: 0 for resursa in RESURSE}
    for index, numar_pachete in enumerate(cantitati):
        for resursa, grame in RETETE[index].items():
            consum[resursa] += numar_pachete * grame
    return consum


def calculeaza_profit(cantitati: list[int]) -> int:
    return sum(cantitate * profit for cantitate, profit in zip(cantitati, PROFITURI))


def este_fezabil(cantitati: list[int]) -> bool:
    consum = calculeaza_consum(cantitati)
    return sum(cantitati) > 0 and all(consum[resursa] <= RESURSE[resursa] for resursa in RESURSE)


def evalueaza(cantitati: list[int]) -> Evaluare:
    consum = calculeaza_consum(cantitati)
    profit_total = calculeaza_profit(cantitati)
    fezabil = sum(cantitati) > 0 and all(consum[resursa] <= RESURSE[resursa] for resursa in RESURSE)

    if fezabil:
        scor = float(profit_total)
    else:
        penalizare = 1_000_000
        for resursa in RESURSE:
            penalizare += max(0, consum[resursa] - RESURSE[resursa]) * 10
        if sum(cantitati) == 0:
            penalizare += 100_000
        scor = -penalizare

    return Evaluare(
        cantitati=cantitati[:],
        consum=consum,
        profit_total=profit_total,
        fezabil=fezabil,
        scor=scor,
    )


def poate_adauga(cantitati: list[int], tip: int) -> bool:
    candidat = cantitati[:]
    candidat[tip] += 1
    return este_fezabil(candidat)


def repara_individ(cantitati: list[int], aleator: Random) -> None:
    for i in range(len(cantitati)):
        cantitati[i] = max(0, int(cantitati[i]))

    while not este_fezabil(cantitati):
        pozitii = [i for i, valoare in enumerate(cantitati) if valoare > 0]
        if not pozitii:
            break
        pozitii.sort(key=lambda i: PROFITURI[i])
        ales = pozitii[0] if aleator.random() < 0.7 else aleator.choice(pozitii)
        cantitati[ales] -= 1

    if sum(cantitati) == 0:
        cantitati[0] = 1
        if not este_fezabil(cantitati):
            cantitati[0] = 0
            cantitati[3] = 1


def genereaza_individ_aleator(aleator: Random) -> list[int]:
    cantitati = [0] * len(RETETE)

    while True:
        tipuri_fezabile = [tip for tip in range(len(RETETE)) if poate_adauga(cantitati, tip)]
        if not tipuri_fezabile or aleator.random() < 0.08:
            break

        if aleator.random() < 0.4:
            tip = max(tipuri_fezabile, key=lambda index: PROFITURI[index])
        else:
            tip = aleator.choice(tipuri_fezabile)

        pas = 1
        if aleator.random() < 0.3:
            pas = aleator.randint(1, 5)

        for _ in range(pas):
            if poate_adauga(cantitati, tip):
                cantitati[tip] += 1
            else:
                break

    repara_individ(cantitati, aleator)
    return cantitati


def imbunatateste_individ(cantitati: list[int]) -> None:
    while True:
        evaluare_curenta = evalueaza(cantitati)
        if not evaluare_curenta.fezabil:
            break

        cel_mai_bun = None
        scor_maxim = evaluare_curenta.scor

        for tip in range(len(RETETE)):
            candidat = cantitati[:]
            candidat[tip] += 1
            evaluare_candidat = evalueaza(candidat)
            if evaluare_candidat.fezabil and evaluare_candidat.scor > scor_maxim + EPS:
                cel_mai_bun = candidat
                scor_maxim = evaluare_candidat.scor

        for tip_scos in range(len(RETETE)):
            if cantitati[tip_scos] == 0:
                continue
            for tip_adaugat in range(len(RETETE)):
                if tip_adaugat == tip_scos:
                    continue
                candidat = cantitati[:]
                candidat[tip_scos] -= 1
                candidat[tip_adaugat] += 1
                evaluare_candidat = evalueaza(candidat)
                if evaluare_candidat.fezabil and evaluare_candidat.scor > scor_maxim + EPS:
                    cel_mai_bun = candidat
                    scor_maxim = evaluare_candidat.scor

        if cel_mai_bun is None:
            break

        cantitati[:] = cel_mai_bun


def selectie_turneu(populatie: list[list[int]], aleator: Random, dimensiune_turneu: int) -> list[int]:
    candidati = aleator.sample(populatie, dimensiune_turneu)
    return max(candidati, key=lambda individ: evalueaza(individ).scor)[:]


def crossover_unipunct(parinte1: list[int], parinte2: list[int], aleator: Random) -> list[int]:
    punct = aleator.randint(1, len(RETETE) - 1)
    copil = parinte1[:punct] + parinte2[punct:]
    repara_individ(copil, aleator)
    imbunatateste_individ(copil)
    return copil


def mutatie(cantitati: list[int], aleator: Random) -> None:
    operatie = aleator.choice(["adauga", "sterge", "transfera", "salt"])

    if operatie == "adauga":
        tip = aleator.randrange(len(RETETE))
        cantitati[tip] += aleator.randint(1, 10)
    elif operatie == "sterge":
        tip = aleator.randrange(len(RETETE))
        cantitati[tip] = max(0, cantitati[tip] - aleator.randint(1, 10))
    elif operatie == "transfera":
        sursa = aleator.randrange(len(RETETE))
        destinatie = aleator.randrange(len(RETETE))
        while destinatie == sursa:
            destinatie = aleator.randrange(len(RETETE))
        cantitate = min(cantitati[sursa], aleator.randint(1, 10))
        cantitati[sursa] -= cantitate
        cantitati[destinatie] += cantitate
    else:
        tip = aleator.randrange(len(RETETE))
        cantitati[tip] += aleator.randint(20, 80)

    repara_individ(cantitati, aleator)
    imbunatateste_individ(cantitati)


def algoritm_genetic_pachete(
    dimensiune_populatie: int = 20,
    generatii_maxime: int = 40,
    probabilitate_crossover: float = 0.9,
    probabilitate_mutatie: float = 0.25,
    dimensiune_turneu: int = 3,
    elitism: int = 4,
    prag_stagnare: int = 5,
    seed: int = 43,
) -> RezultatGA:
    aleator = Random(seed)
    populatie = [genereaza_individ_aleator(aleator) for _ in range(dimensiune_populatie)]
    for individ in populatie:
        imbunatateste_individ(individ)

    cel_mai_bun = max(populatie, key=lambda individ: evalueaza(individ).scor)[:]
    evaluare_best = evalueaza(cel_mai_bun)
    generatie_gasita = 0
    evolutie_best: list[float] = []
    stagnare = 0

    for generatie in range(generatii_maxime + 1):
        populatie.sort(key=lambda individ: evalueaza(individ).scor, reverse=True)
        evaluare_curenta = evalueaza(populatie[0])
        evolutie_best.append(evaluare_curenta.profit_total if evaluare_curenta.fezabil else evaluare_curenta.scor)

        if evaluare_curenta.scor > evaluare_best.scor + EPS:
            cel_mai_bun = populatie[0][:]
            evaluare_best = evaluare_curenta
            generatie_gasita = generatie
            stagnare = 0
        else:
            stagnare += 1

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

        if stagnare >= prag_stagnare:
            break

    rezultat = evalueaza(cel_mai_bun)
    return RezultatGA(
        cantitati=rezultat.cantitati,
        profit_total=rezultat.profit_total,
        consum=rezultat.consum,
        generatie_gasita=generatie_gasita,
        evolutie_best=evolutie_best,
    )


def exemplu() -> None:
    rezultat = algoritm_genetic_pachete()

    print("=== PRODUCTIA PACHETELOR DE FRUCTE CU ALGORITM GENETIC ===")
    for eticheta, cantitate in zip(ETICHETE, rezultat.cantitati):
        print(f"{eticheta}: {cantitate} pachete")
    print("Profit total:", rezultat.profit_total)
    print("Consum resurse:")
    for resursa in RESURSE:
        consum = rezultat.consum[resursa]
        disponibil = RESURSE[resursa]
        print(f"  {resursa}: {consum / 1000:.2f} kg din {disponibil / 1000:.2f} kg")
    print("Generatia in care s-a fixat cel mai bun individ:", rezultat.generatie_gasita)
    print("Primele valori din evolutie:", [round(valoare, 2) for valoare in rezultat.evolutie_best[:20]])


if __name__ == "__main__":
    exemplu()
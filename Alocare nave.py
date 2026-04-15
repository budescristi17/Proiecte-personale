from __future__ import annotations

from dataclasses import dataclass
from pathlib import Path
from random import Random


NUMAR_EXPLOATARI = 5
FISIER_CAPACITATI = "capacitati_nave.txt"
EPS = 1e-9


@dataclass
class RezultatGA:
    asignare: list[int]
    incarcari: list[float]
    dezechilibru: float
    interval_capacitati: float
    generatie_gasita: int
    evolutie_best: list[float]


def citeste_capacitati(cale_fisier: str) -> list[float]:
    continut = Path(cale_fisier).read_text(encoding="utf-8")
    capacitati = [float(token) for token in continut.replace(",", " ").split()]
    if not capacitati:
        raise ValueError("Fisierul cu capacitati este gol.")
    return capacitati


def calculeaza_incarcari(asignare: list[int], capacitati: list[float], numar_exploatari: int) -> list[float]:
    incarcari = [0.0] * numar_exploatari
    for nava, exploatare in enumerate(asignare):
        incarcari[exploatare] += capacitati[nava]
    return incarcari


def evalueaza(asignare: list[int], capacitati: list[float], numar_exploatari: int) -> tuple[float, list[float], float]:
    incarcari = calculeaza_incarcari(asignare, capacitati, numar_exploatari)
    tinta = sum(capacitati) / numar_exploatari
    dezechilibru = sum((incarcare - tinta) ** 2 for incarcare in incarcari)
    interval_capacitati = max(incarcari) - min(incarcari)
    scor = -(dezechilibru * 1000 + interval_capacitati)
    return scor, incarcari, dezechilibru


def genereaza_individ_aleator(capacitati: list[float], numar_exploatari: int, aleator: Random) -> list[int]:
    if aleator.random() < 0.5:
        ordine = list(range(len(capacitati)))
        aleator.shuffle(ordine)
        ordine.sort(key=lambda index: capacitati[index], reverse=True)
        asignare = [0] * len(capacitati)
        incarcari = [0.0] * numar_exploatari
        for nava in ordine:
            exploatare = min(range(numar_exploatari), key=lambda i: incarcari[i])
            asignare[nava] = exploatare
            incarcari[exploatare] += capacitati[nava]
        return asignare

    return [aleator.randrange(numar_exploatari) for _ in capacitati]


def selectie_turneu(
    populatie: list[list[int]],
    capacitati: list[float],
    numar_exploatari: int,
    aleator: Random,
    dimensiune_turneu: int,
) -> list[int]:
    candidati = aleator.sample(populatie, dimensiune_turneu)
    return max(candidati, key=lambda individ: evalueaza(individ, capacitati, numar_exploatari)[0])[:]


def crossover_uniform(parinte1: list[int], parinte2: list[int], aleator: Random) -> list[int]:
    copil = []
    for gena1, gena2 in zip(parinte1, parinte2):
        copil.append(gena1 if aleator.random() < 0.5 else gena2)
    return copil


def imbunatateste_asignare(asignare: list[int], capacitati: list[float], numar_exploatari: int) -> None:
    while True:
        scor_curent, _, _ = evalueaza(asignare, capacitati, numar_exploatari)
        cel_mai_bun = None
        scor_maxim = scor_curent

        for nava in range(len(asignare)):
            exploatare_curenta = asignare[nava]
            for exploatare_noua in range(numar_exploatari):
                if exploatare_noua == exploatare_curenta:
                    continue
                candidat = asignare[:]
                candidat[nava] = exploatare_noua
                scor_candidat, _, _ = evalueaza(candidat, capacitati, numar_exploatari)
                if scor_candidat > scor_maxim + EPS:
                    cel_mai_bun = candidat
                    scor_maxim = scor_candidat

        if cel_mai_bun is None:
            break

        asignare[:] = cel_mai_bun


def mutatie(asignare: list[int], numar_exploatari: int, aleator: Random) -> None:
    numar_mutatii = aleator.randint(1, min(3, len(asignare)))
    pozitii = aleator.sample(range(len(asignare)), numar_mutatii)
    for pozitie in pozitii:
        asignare[pozitie] = aleator.randrange(numar_exploatari)


def algoritm_genetic_nave(
    capacitati: list[float],
    numar_exploatari: int = NUMAR_EXPLOATARI,
    dimensiune_populatie: int = 120,
    generatii_maxime: int = 400,
    probabilitate_crossover: float = 0.9,
    probabilitate_mutatie: float = 0.3,
    dimensiune_turneu: int = 3,
    elitism: int = 6,
    prag_stagnare: int = 40,
    seed: int = 41,
) -> RezultatGA:
    aleator = Random(seed)
    populatie = [genereaza_individ_aleator(capacitati, numar_exploatari, aleator) for _ in range(dimensiune_populatie)]
    for individ in populatie:
        imbunatateste_asignare(individ, capacitati, numar_exploatari)

    cel_mai_bun = max(populatie, key=lambda individ: evalueaza(individ, capacitati, numar_exploatari)[0])[:]
    scor_best, incarcari_best, dezechilibru_best = evalueaza(cel_mai_bun, capacitati, numar_exploatari)
    generatie_gasita = 0
    evolutie_best: list[float] = []
    stagnare = 0

    for generatie in range(generatii_maxime + 1):
        populatie.sort(key=lambda individ: evalueaza(individ, capacitati, numar_exploatari)[0], reverse=True)
        scor_curent, _, dezechilibru_curent = evalueaza(populatie[0], capacitati, numar_exploatari)
        evolutie_best.append(dezechilibru_curent)

        if scor_curent > scor_best + EPS:
            cel_mai_bun = populatie[0][:]
            scor_best, incarcari_best, dezechilibru_best = evalueaza(cel_mai_bun, capacitati, numar_exploatari)
            generatie_gasita = generatie
            stagnare = 0
        else:
            stagnare += 1

        noua_populatie = [individ[:] for individ in populatie[:elitism]]

        while len(noua_populatie) < dimensiune_populatie:
            parinte1 = selectie_turneu(populatie, capacitati, numar_exploatari, aleator, dimensiune_turneu)
            parinte2 = selectie_turneu(populatie, capacitati, numar_exploatari, aleator, dimensiune_turneu)

            if aleator.random() < probabilitate_crossover:
                copil = crossover_uniform(parinte1, parinte2, aleator)
            else:
                copil = parinte1[:]

            if aleator.random() < probabilitate_mutatie:
                mutatie(copil, numar_exploatari, aleator)

            imbunatateste_asignare(copil, capacitati, numar_exploatari)
            noua_populatie.append(copil)

        populatie = noua_populatie

        if stagnare >= prag_stagnare:
            break

    return RezultatGA(
        asignare=cel_mai_bun,
        incarcari=incarcari_best,
        dezechilibru=dezechilibru_best,
        interval_capacitati=max(incarcari_best) - min(incarcari_best),
        generatie_gasita=generatie_gasita,
        evolutie_best=evolutie_best,
    )


def afiseaza_rezultat(rezultat: RezultatGA, capacitati: list[float], numar_exploatari: int) -> None:
    for exploatare in range(numar_exploatari):
        nave = [index + 1 for index, ales in enumerate(rezultat.asignare) if ales == exploatare]
        cap = [capacitati[index - 1] for index in nave]
        print(
            f"Exploatarea {exploatare + 1}: nave {nave}, "
            f"capacitati {cap}, total {rezultat.incarcari[exploatare]:.2f}"
        )


def exemplu() -> None:
    capacitati = citeste_capacitati(FISIER_CAPACITATI)
    rezultat = algoritm_genetic_nave(capacitati)

    print("=== ALOCAREA NAVELOR LA EXPLOATARI CU ALGORITM GENETIC ===")
    print("Numar nave:", len(capacitati))
    print("Numar exploatari:", NUMAR_EXPLOATARI)
    print("Capacitate totala:", f"{sum(capacitati):.2f}")
    print("Capacitate medie tinta:", f"{sum(capacitati) / NUMAR_EXPLOATARI:.2f}")
    print("Dezechilibru final:", f"{rezultat.dezechilibru:.4f}")
    print("Interval intre exploatarea cea mai incarcata si cea mai usoara:", f"{rezultat.interval_capacitati:.2f}")
    print("Generatia in care s-a fixat cel mai bun individ:", rezultat.generatie_gasita)
    afiseaza_rezultat(rezultat, capacitati, NUMAR_EXPLOATARI)
    print("Primele valori din evolutie:", [round(valoare, 4) for valoare in rezultat.evolutie_best[:20]])


if __name__ == "__main__":
    exemplu()
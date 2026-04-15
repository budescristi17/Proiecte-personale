from __future__ import annotations

from dataclasses import dataclass
from pathlib import Path
from random import Random


NUMAR_VAGOANE = 4
FISIER_MASE = "mase_containere.txt"
EPS = 1e-9


@dataclass
class RezultatGA:
    asignare: list[int]
    mase_vagoane: list[float]
    dezechilibru: float
    interval_mase: float
    generatie_gasita: int
    evolutie_best: list[float]


def citeste_mase(cale_fisier: str) -> list[float]:
    continut = Path(cale_fisier).read_text(encoding="utf-8")
    valori = []
    for token in continut.replace(",", " ").split():
        valori.append(float(token))

    if not valori:
        raise ValueError("Fisierul cu mase este gol.")
    return valori


def calculeaza_mase_vagoane(asignare: list[int], mase_containere: list[float], numar_vagoane: int) -> list[float]:
    mase = [0.0] * numar_vagoane
    for container, vagon in enumerate(asignare):
        mase[vagon] += mase_containere[container]
    return mase


def evalueaza(asignare: list[int], mase_containere: list[float], numar_vagoane: int) -> tuple[float, list[float], float]:
    mase_vagoane = calculeaza_mase_vagoane(asignare, mase_containere, numar_vagoane)
    tinta = sum(mase_containere) / numar_vagoane
    dezechilibru = sum((masa - tinta) ** 2 for masa in mase_vagoane)
    interval_mase = max(mase_vagoane) - min(mase_vagoane)
    scor = -(dezechilibru * 1000 + interval_mase)
    return scor, mase_vagoane, dezechilibru


def genereaza_individ_aleator(mase_containere: list[float], numar_vagoane: int, aleator: Random) -> list[int]:
    if aleator.random() < 0.4:
        ordine = sorted(range(len(mase_containere)), key=lambda index: mase_containere[index], reverse=True)
        asignare = [0] * len(mase_containere)
        mase_curente = [0.0] * numar_vagoane
        for index in ordine:
            vagon = min(range(numar_vagoane), key=lambda i: mase_curente[i])
            asignare[index] = vagon
            mase_curente[vagon] += mase_containere[index]
        return asignare

    return [aleator.randrange(numar_vagoane) for _ in mase_containere]


def selectie_turneu(
    populatie: list[list[int]],
    mase_containere: list[float],
    numar_vagoane: int,
    aleator: Random,
    dimensiune_turneu: int,
) -> list[int]:
    candidati = aleator.sample(populatie, dimensiune_turneu)
    return max(candidati, key=lambda individ: evalueaza(individ, mase_containere, numar_vagoane)[0])[:]


def crossover_uniform(parinte1: list[int], parinte2: list[int], aleator: Random) -> list[int]:
    copil = []
    for gena1, gena2 in zip(parinte1, parinte2):
        copil.append(gena1 if aleator.random() < 0.5 else gena2)
    return copil


def imbunatateste_asignare(asignare: list[int], mase_containere: list[float], numar_vagoane: int) -> None:
    while True:
        _, mase_vagoane, dezechilibru_curent = evalueaza(asignare, mase_containere, numar_vagoane)
        vagon_greu = max(range(numar_vagoane), key=lambda i: mase_vagoane[i])
        vagon_usor = min(range(numar_vagoane), key=lambda i: mase_vagoane[i])
        cel_mai_bun = None
        cel_mai_bun_dezechilibru = dezechilibru_curent

        for index, vagon in enumerate(asignare):
            if vagon != vagon_greu:
                continue
            candidat = asignare[:]
            candidat[index] = vagon_usor
            _, _, dezechilibru_nou = evalueaza(candidat, mase_containere, numar_vagoane)
            if dezechilibru_nou + EPS < cel_mai_bun_dezechilibru:
                cel_mai_bun = candidat
                cel_mai_bun_dezechilibru = dezechilibru_nou

        if cel_mai_bun is None:
            break

        asignare[:] = cel_mai_bun


def mutatie(asignare: list[int], numar_vagoane: int, aleator: Random) -> None:
    numar_mutatii = aleator.randint(1, 3)
    pozitii = aleator.sample(range(len(asignare)), numar_mutatii)
    for pozitie in pozitii:
        asignare[pozitie] = aleator.randrange(numar_vagoane)


def algoritm_genetic_vagoane(
    mase_containere: list[float],
    numar_vagoane: int,
    dimensiune_populatie: int = 120,
    generatii_maxime: int = 400,
    probabilitate_crossover: float = 0.9,
    probabilitate_mutatie: float = 0.3,
    dimensiune_turneu: int = 3,
    elitism: int = 6,
    seed: int = 31,
) -> RezultatGA:
    aleator = Random(seed)
    populatie = [genereaza_individ_aleator(mase_containere, numar_vagoane, aleator) for _ in range(dimensiune_populatie)]
    for individ in populatie:
        imbunatateste_asignare(individ, mase_containere, numar_vagoane)

    cel_mai_bun = max(populatie, key=lambda individ: evalueaza(individ, mase_containere, numar_vagoane)[0])[:]
    scor_best, mase_best, dezechilibru_best = evalueaza(cel_mai_bun, mase_containere, numar_vagoane)
    generatie_gasita = 0
    evolutie_best: list[float] = []

    for generatie in range(generatii_maxime + 1):
        populatie.sort(key=lambda individ: evalueaza(individ, mase_containere, numar_vagoane)[0], reverse=True)
        scor_curent, _, dezechilibru_curent = evalueaza(populatie[0], mase_containere, numar_vagoane)
        evolutie_best.append(dezechilibru_curent)

        if scor_curent > scor_best + EPS:
            cel_mai_bun = populatie[0][:]
            scor_best, mase_best, dezechilibru_best = evalueaza(cel_mai_bun, mase_containere, numar_vagoane)
            generatie_gasita = generatie

        noua_populatie = [individ[:] for individ in populatie[:elitism]]

        while len(noua_populatie) < dimensiune_populatie:
            parinte1 = selectie_turneu(populatie, mase_containere, numar_vagoane, aleator, dimensiune_turneu)
            parinte2 = selectie_turneu(populatie, mase_containere, numar_vagoane, aleator, dimensiune_turneu)

            if aleator.random() < probabilitate_crossover:
                copil = crossover_uniform(parinte1, parinte2, aleator)
            else:
                copil = parinte1[:]

            if aleator.random() < probabilitate_mutatie:
                mutatie(copil, numar_vagoane, aleator)

            imbunatateste_asignare(copil, mase_containere, numar_vagoane)
            noua_populatie.append(copil)

        populatie = noua_populatie

    _, mase_finale, dezechilibru_final = evalueaza(cel_mai_bun, mase_containere, numar_vagoane)
    return RezultatGA(
        asignare=cel_mai_bun,
        mase_vagoane=mase_finale,
        dezechilibru=dezechilibru_final,
        interval_mase=max(mase_finale) - min(mase_finale),
        generatie_gasita=generatie_gasita,
        evolutie_best=evolutie_best,
    )


def afiseaza_rezultat(rezultat: RezultatGA, mase_containere: list[float], numar_vagoane: int) -> None:
    for vagon in range(numar_vagoane):
        containere = [index + 1 for index, ales in enumerate(rezultat.asignare) if ales == vagon]
        mase = [mase_containere[index - 1] for index in containere]
        print(f"Vagon {vagon + 1}: containere {containere}, mase {mase}, total {rezultat.mase_vagoane[vagon]:.2f}")


def exemplu() -> None:
    mase_containere = citeste_mase(FISIER_MASE)
    rezultat = algoritm_genetic_vagoane(mase_containere, NUMAR_VAGOANE)

    print("=== ALOCAREA CONTAINERELOR IN VAGOANE CU ALGORITM GENETIC ===")
    print("Numar vagoane:", NUMAR_VAGOANE)
    print("Numar containere:", len(mase_containere))
    print("Masa totala:", f"{sum(mase_containere):.2f}")
    print("Masa medie tinta pe vagon:", f"{sum(mase_containere) / NUMAR_VAGOANE:.2f}")
    print("Dezechilibru final:", f"{rezultat.dezechilibru:.4f}")
    print("Interval intre vagonul cel mai greu si cel mai usor:", f"{rezultat.interval_mase:.2f}")
    print("Generatia in care s-a fixat cel mai bun individ:", rezultat.generatie_gasita)
    afiseaza_rezultat(rezultat, mase_containere, NUMAR_VAGOANE)
    print("Primele valori din evolutie:", [round(valoare, 4) for valoare in rezultat.evolutie_best[:20]])


if __name__ == "__main__":
    exemplu()
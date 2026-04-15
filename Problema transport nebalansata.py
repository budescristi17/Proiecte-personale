from __future__ import annotations

from collections import deque
from dataclasses import dataclass

EPS = 1e-9


@dataclass
class RezultatTransport:
    cost_total: float
    alocare_echilibrata: list[list[float]]
    alocare_initiala: list[list[float]]
    costuri_echilibrate: list[list[float]]
    oferta_echilibrata: list[float]
    cerere_echilibrata: list[float]
    sursa_fictiva: bool
    destinatie_fictiva: bool


def _echilibreaza(
    costuri: list[list[float]],
    oferta: list[float],
    cerere: list[float],
) -> tuple[list[list[float]], list[float], list[float], bool, bool]:
    costuri_noi = [rand[:] for rand in costuri]
    oferta_noua = oferta[:]
    cerere_noua = cerere[:]

    surplus = sum(oferta_noua) - sum(cerere_noua)
    sursa_fictiva = False
    destinatie_fictiva = False

    if surplus > EPS:
        for rand in costuri_noi:
            rand.append(0.0)
        cerere_noua.append(surplus)
        destinatie_fictiva = True
    elif surplus < -EPS:
        costuri_noi.append([0.0] * len(cerere_noua))
        oferta_noua.append(-surplus)
        sursa_fictiva = True

    return costuri_noi, oferta_noua, cerere_noua, sursa_fictiva, destinatie_fictiva


def _solutie_initiala_colt_nord_vest(
    oferta: list[float],
    cerere: list[float],
) -> tuple[list[list[float]], set[tuple[int, int]]]:
    m = len(oferta)
    n = len(cerere)
    alocare = [[0.0 for _ in range(n)] for _ in range(m)]
    baza: set[tuple[int, int]] = set()

    oferta_ramasa = oferta[:]
    cerere_ramasa = cerere[:]
    i = 0
    j = 0

    while i < m and j < n:
        if oferta_ramasa[i] <= EPS:
            i += 1
            continue
        if cerere_ramasa[j] <= EPS:
            j += 1
            continue

        cantitate = min(oferta_ramasa[i], cerere_ramasa[j])
        alocare[i][j] = cantitate
        baza.add((i, j))
        oferta_ramasa[i] -= cantitate
        cerere_ramasa[j] -= cantitate

        if oferta_ramasa[i] <= EPS and cerere_ramasa[j] <= EPS:
            i += 1
            j += 1
        elif oferta_ramasa[i] <= EPS:
            i += 1
        else:
            j += 1

    return alocare, baza


def _completeaza_baza(
    alocare: list[list[float]],
    baza: set[tuple[int, int]],
) -> None:
    m = len(alocare)
    n = len(alocare[0])
    parent = list(range(m + n))
    rang = [0] * (m + n)

    def find(x: int) -> int:
        while parent[x] != x:
            parent[x] = parent[parent[x]]
            x = parent[x]
        return x

    def union(a: int, b: int) -> None:
        ra = find(a)
        rb = find(b)
        if ra == rb:
            return
        if rang[ra] < rang[rb]:
            parent[ra] = rb
        elif rang[ra] > rang[rb]:
            parent[rb] = ra
        else:
            parent[rb] = ra
            rang[ra] += 1

    for i, j in baza:
        union(i, m + j)

    while len(baza) < m + n - 1:
        adaugat = False
        for i in range(m):
            for j in range(n):
                if (i, j) in baza:
                    continue
                if find(i) != find(m + j):
                    baza.add((i, j))
                    alocare[i][j] = 0.0
                    union(i, m + j)
                    adaugat = True
                    break
            if adaugat:
                break
        if not adaugat:
            raise RuntimeError("Baza degenerata nu a putut fi completata.")


def _calculeaza_potentiale(
    costuri: list[list[float]],
    baza: set[tuple[int, int]],
) -> tuple[list[float], list[float]]:
    m = len(costuri)
    n = len(costuri[0])
    u: list[float | None] = [None] * m
    v: list[float | None] = [None] * n
    u[0] = 0.0

    progres = True
    while progres:
        progres = False
        for i, j in baza:
            if u[i] is not None and v[j] is None:
                v[j] = costuri[i][j] - u[i]
                progres = True
            elif v[j] is not None and u[i] is None:
                u[i] = costuri[i][j] - v[j]
                progres = True

    if any(valoare is None for valoare in u + v):
        raise RuntimeError("Potentialele nu au putut fi calculate pentru baza curenta.")

    return [float(x) for x in u], [float(x) for x in v]


def _gaseste_celula_de_intrare(
    costuri: list[list[float]],
    baza: set[tuple[int, int]],
    u: list[float],
    v: list[float],
) -> tuple[int, int] | None:
    m = len(costuri)
    n = len(costuri[0])
    celula = None
    delta_minim = 0.0

    for i in range(m):
        for j in range(n):
            if (i, j) in baza:
                continue
            delta = costuri[i][j] - u[i] - v[j]
            if delta < delta_minim - EPS:
                delta_minim = delta
                celula = (i, j)

    return celula


def _gaseste_ciclu(
    baza: set[tuple[int, int]],
    intrare: tuple[int, int],
    m: int,
    n: int,
) -> list[tuple[int, int]]:
    start = intrare[0]
    tinta = m + intrare[1]
    adiacenta: dict[int, list[int]] = {nod: [] for nod in range(m + n)}

    for i, j in baza:
        adiacenta[i].append(m + j)
        adiacenta[m + j].append(i)

    coada = deque([start])
    parinte: dict[int, int | None] = {start: None}

    while coada:
        nod = coada.popleft()
        if nod == tinta:
            break
        for vecin in adiacenta[nod]:
            if vecin not in parinte:
                parinte[vecin] = nod
                coada.append(vecin)

    if tinta not in parinte:
        raise RuntimeError("Nu s-a putut construi ciclul de imbunatatire.")

    drum_noduri: list[int] = []
    nod_curent: int | None = tinta
    while nod_curent is not None:
        drum_noduri.append(nod_curent)
        nod_curent = parinte[nod_curent]
    drum_noduri.reverse()

    ciclu = [intrare]
    for a, b in zip(drum_noduri, drum_noduri[1:]):
        if a < m and b >= m:
            ciclu.append((a, b - m))
        else:
            ciclu.append((b, a - m))

    return ciclu


def _pivoteaza(
    alocare: list[list[float]],
    baza: set[tuple[int, int]],
    intrare: tuple[int, int],
) -> None:
    m = len(alocare)
    n = len(alocare[0])
    ciclu = _gaseste_ciclu(baza, intrare, m, n)
    theta = min(alocare[i][j] for index, (i, j) in enumerate(ciclu) if index % 2 == 1)

    for index, (i, j) in enumerate(ciclu):
        if index % 2 == 0:
            alocare[i][j] += theta
        else:
            alocare[i][j] -= theta
            if abs(alocare[i][j]) <= EPS:
                alocare[i][j] = 0.0

    baza.add(intrare)
    candidati_iesire = [
        (i, j)
        for index, (i, j) in enumerate(ciclu)
        if index % 2 == 1 and alocare[i][j] <= EPS
    ]

    if not candidati_iesire:
        raise RuntimeError("Nu exista variabila care sa iasa din baza.")

    baza.remove(candidati_iesire[0])


def rezolva_transport_nebalansat(
    costuri: list[list[float]],
    oferta: list[float],
    cerere: list[float],
) -> RezultatTransport:
    if not costuri or not costuri[0]:
        raise ValueError("Matricea costurilor nu poate fi vida.")
    if len(costuri) != len(oferta):
        raise ValueError("Numarul liniilor din costuri trebuie sa fie egal cu numarul surselor.")
    if any(len(rand) != len(cerere) for rand in costuri):
        raise ValueError("Fiecare linie din costuri trebuie sa aiba aceeasi lungime ca cererea.")

    (
        costuri_echilibrate,
        oferta_echilibrata,
        cerere_echilibrata,
        sursa_fictiva,
        destinatie_fictiva,
    ) = _echilibreaza(costuri, oferta, cerere)

    alocare, baza = _solutie_initiala_colt_nord_vest(oferta_echilibrata, cerere_echilibrata)
    _completeaza_baza(alocare, baza)

    while True:
        u, v = _calculeaza_potentiale(costuri_echilibrate, baza)
        intrare = _gaseste_celula_de_intrare(costuri_echilibrate, baza, u, v)
        if intrare is None:
            break
        _pivoteaza(alocare, baza, intrare)

    cost_total = 0.0
    for i in range(len(costuri_echilibrate)):
        for j in range(len(costuri_echilibrate[0])):
            cost_total += alocare[i][j] * costuri_echilibrate[i][j]

    alocare_initiala = [rand[: len(cerere)] for rand in alocare[: len(oferta)]]

    return RezultatTransport(
        cost_total=cost_total,
        alocare_echilibrata=alocare,
        alocare_initiala=alocare_initiala,
        costuri_echilibrate=costuri_echilibrate,
        oferta_echilibrata=oferta_echilibrata,
        cerere_echilibrata=cerere_echilibrata,
        sursa_fictiva=sursa_fictiva,
        destinatie_fictiva=destinatie_fictiva,
    )


def afiseaza_matrice(matrice: list[list[float]]) -> None:
    for rand in matrice:
        valori = " ".join(f"{valoare:8.2f}" for valoare in rand)
        print(valori)


def exemplu_transport() -> None:
    costuri = [
        [4, 8, 8],
        [16, 24, 16],
        [8, 16, 24],
    ]
    oferta = [76, 82, 77]
    cerere = [72, 102, 41]

    rezultat = rezolva_transport_nebalansat(costuri, oferta, cerere)

    print("Cost total optim:", f"{rezultat.cost_total:.2f}")
    print("Alocare pe problema initiala:")
    afiseaza_matrice(rezultat.alocare_initiala)

    if rezultat.sursa_fictiva or rezultat.destinatie_fictiva:
        print("\nAlocare pe problema echilibrata:")
        afiseaza_matrice(rezultat.alocare_echilibrata)
        if rezultat.sursa_fictiva:
            print("A fost adaugata o sursa fictiva.")
        if rezultat.destinatie_fictiva:
            print("A fost adaugata o destinatie fictiva.")


if __name__ == "__main__":
    exemplu_transport()

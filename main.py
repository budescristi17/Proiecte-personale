# Problema 1
def numar_linii_crescatoare(matrice):
    count = 0
    for linie in matrice:
        ok = True
        for i in range(0, len(linie) - 1):
            if linie[i] >= linie[i + 1]:
                ok = False
                break
        if ok == True:
            count = count + 1
    return count
A = [[1, 2, 3, 4],[1, 2, 6, 7],[0, 1, 2, 3],[5, 5, 6, 7]]
print(numar_linii_crescatoare(A))

# Problema 2
def coloane_minim_5(matrice):
    coloane = []
    for j in range(len(matrice[0])):
        minim = matrice[0][j]
        for i in range(len(matrice)):
            if matrice[i][j] < minim:
                minim = matrice[i][j]
        if minim == 5:
            coloane.append(j)
    return coloane
A = [[5, 7, 9, 5],[6, 5, 10, 8],[8, 6, 12, 9]]
print(coloane_minim_5(A))

#Problema 3
def bubble_sort(linie):
    n = len(linie)
    for i in range(n - 1):
        for j in range(n - 1 - i):
            if linie[j] > linie[j + 1]:
                linie[j], linie[j + 1] = linie[j + 1], linie[j]
def sortare(matrice):
    for i in range(len(matrice)):
        bubble_sort(matrice[i])
    return matrice
A = [[4, 1, 3, 2],[9, 5, 7, 6],[8, 0, 2, 2]]
print(sortare(A))

#Problema 4
def insertion_sort(vector):
    for i in range(1, len(vector)):
        x = vector[i]
        j = i - 1
        while j >= 0 and vector[j] > x:
            vector[j + 1] = vector[j]
            j -= 1
        vector[j + 1] = x

def sortare(matrice):
    n = len(matrice)
    m = len(matrice[0])

    for j in range(m):
        col = []
        for i in range(n):
            col.append(matrice[i][j])
        insertion_sort(col)
        for i in range(n):
            matrice[i][j] = col[i]

    return matrice
B = [[4, 9, 3],[1, 5, 2],[8, 6, 7]]
print(sortare(B))

#Problema 5
def cmmdc(a, b):
    if b == 0:
        return a
    else:
        return cmmdc(b, a % b)
print(cmmdc(48, 18))

#Problema 6
def transpusa(A):
    n = len(A)
    T = []
    for i in range(n):
        linie = []
        for j in range(n):
            linie.append(A[j][i])
        T.append(linie)
    return T

def adunare(A, B):
    n = len(A)
    C = []

    for i in range(n):
        linie = []
        for j in range(n):
            linie.append(A[i][j] + B[i][j])
        C.append(linie)
    return C

def inmultire(A, B):
    n = len(A)
    D = []
    for i in range(n):
        linie = []
        for j in range(n):
            s = 0
            for k in range(n):
                s += A[i][k] * B[k][j]
            linie.append(s)
        D.append(linie)
    return D

def putere(A, p):
    rezultat = A
    for i in range(p - 1):
        rezultat = inmultire(rezultat, A)
    return rezultat

A = [[1, 2],[3, 4]]
B = [[5, 6],[7, 8]]
print("Transpusa A:", transpusa(A))
print("A + B:", adunare(A, B))
print("A * B:", inmultire(A, B))
print("A^2:", putere(A, 2))

#Problema 7
def insertion_sort(v):
    for i in range(1, len(v)):
        x = v[i]
        j = i - 1
        while j >= 0 and v[j] > x:
            v[j + 1] = v[j]
            j -= 1
        v[j + 1] = x
    return v

lista = [7, 3, 9, 1, 5]
print(insertion_sort(lista))

#Problema 8
def este_permutare_identica(p):
    for i in range(len(p)):
        if p[i] != i + 1:
            return False
    return True

p1 = [1, 2, 3, 4]
p2 = [2, 1, 3, 4]
print(este_permutare_identica(p1))
print(este_permutare_identica(p2))
#include <stdio.h>
#include <stdbool.h>
float valoarea_productiei(int m, int n, float p[], float a[][40],float v[]){
    float q[40] = {0};
    int i,j;
for(i=0; i<m; i++)
{
   
    for(j=0; j<n; j++)
{
    q[i] += a[i][j];
    }
}

for (i=0; i < m; i++){
    v[i] = p[i]*q[i];
}

for (i=0; i < m; i++){
    return v[i];
  }
}



float prod_maxima(int m, int n, float a[][40], float max[]){
    int i,j;
    for(i = 0; i < m; i++){
        max[i] = a[i][0];
        for(j = 1; j < n; j++){
            if(a[i][j] > max[i]){
                max[i] = a[i][j];
            }
        }
    }
for (i=0; i< m; i++){
   return max[i];
   }
}



void surse(int m, int n, float a[][40], int suc[], int *marime) {
    int i, j;
    *marime = 0; // Reset size
    for (i = 0; i < m; i++) {
        bool are_zero = false;
        for (j = 0; j < n; j++) {
            if (a[i][j] == 0) {
                are_zero = true; // Mark row as having zero
                break; // Exit inner loop
            }
        }
        if (!are_zero) {
            suc[(*marime)++] = i + 1; // Store row index (1-based)
        }
    }
}
void crescator(int m, int n,int crest[], float a[][40], int *marime){
int i,j;
*marime = 0;
for(i=0; i < m; i++){
    bool creste = true;
    for (j=1; j < n; j++){
        if(a[i][j-1] > a[i][j]){
            creste = false;
            break;
        }
    }
    if(creste == true){
        crest[(*marime)++] = i + 1;
    }
    
  }
}
void amplitudine(int m, int n,float a[][40],float ampl[40][2]){
int i,j;
float min, max;
for(i=0; i< m; i++){
    min = a[i][0];
    max = a[i][0];
    for(j=0; j < n; j++){
        if(min > a[i][j]){
            min = a[i][j];
        }
        if(max < a[i][j]){
            max = a[i][j];
        }
    }
    ampl[i][0] = min;
    ampl[i][1] = max;
}
}
int main()
{
int m, n, i, j, suc[40]= {0},crest[40] = {0}, marime;
float a[40][40], p[40], q[40], v[40], max[40], ampl[40][2];
printf("introduceti nr de produse: ");
scanf ("%d", &m); // m este numartul de produse / linia
printf("introduceti nr de sucursale: ");
scanf("%d", &n); // n este numarul de sucursale / coloana
for (i=0; i<m; i++)
{
    printf ("introduceti pretul produsului: ");
    scanf("%f", &p[i]); //preturile vor fi stocate in vectorul p
}
for(i=0; i<m; i++)
{
   
    for(j=0; j<n; j++)
{
    printf("Cantitatea produsa in sucursala (%d,%d): ", i ,j);
    scanf("%f", &a[i][j]); // cantitatile produse
}
}
    valoarea_productiei(m, n, p, a, v); // Valoarea totala a productiei
    printf("Valoarea productiei pentru fiecare produs este:\n ");
    for(i = 0; i < m; i++){
        printf("Produsul %d: %4.2f\n", i+1, v[i]);
    }
    prod_maxima(m, n, a, max); //Capacitatea maxima de productie
    printf("Valoarea productiei maxime pentru fiecare produs este:\n ");
    for(i = 0; i < m; i++){
        printf("Produsul %d: %4.2f\n", i+1, max[i]);
    }
    surse(m, n, a, suc, &marime);
    printf("Produsele la care s-a inregistrat productie la fiecare sucursala sunt urmatoarele: \n");
    for (i = 0; i < marime; i++) {
        printf("Produsul: %d\n", suc[i]);
    }
    crescator(m, n, crest, a, &marime);
    printf("Produsele care au productie crescatoare: \n");
    for(i=0; i < marime; i++){
        printf("Produsul: %d\n", crest[i]);
    }
    amplitudine(m, n, a, ampl);
    printf("Amplitudinea produselor: \n");
    for(i=0; i < m; i++){
            printf("Amplitudinea produsului %d este: Min %4.2f si Max %4.2f\n", i+1, ampl[i][0], ampl[i][1]);
    }
    return 0;
}
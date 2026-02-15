/******************************************************************************

                            Online C Compiler.
                Code, Compile, Run and Debug C program online.
Write your code in this editor and press "Run" button to compile and execute it.

*******************************************************************************/
#include <stdio.h>
#define MAX 100 
int main()
 {
    int m, n, prag, k = 0;
    int a[MAX][MAX];
    int luna[MAX]; 
    printf("Introduceți numărul de abonați (m) și numărul de luni (n): ");
    scanf("%d %d", &m, &n);
    printf("Introduceți valorile convorbirilor telefonice:\n");
    for (int i = 0; i < m; i++) 
{
        for (int j = 0; j < n; j++) 
{
            scanf("%d", &a[i][j]);
        }
    }
    printf("Introduceți valoarea prag: ");
    scanf("%d", &prag);
    for (int j = 0; j < n; j++) 
{
        int suma_luna = 0;
        for (int i = 0; i < m; i++) 
{
            suma_luna += a[i][j];
        }
        if (suma_luna < prag) {
            luna[k] = j; 
            k++;   
        }
    }
    if (k > 0) 
{
        printf("Lunile cu valoarea convorbirilor sub pragul %d sunt:\n", prag);
        for (int i = 0; i < k; i++) 
{
            printf("Luna %d\n", luna[i] + 1); 
        }
    } else 
{
        printf("Nu există luni cu valoarea convorbirilor sub pragul %d.\n", prag);
    }
    return 0;
}

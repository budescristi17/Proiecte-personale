/******************************************************************************

                            Online C Compiler.
                Code, Compile, Run and Debug C program online.
Write your code in this editor and press "Run" button to compile and execute it.

*******************************************************************************/

#include <stdio.h>
void find_min_sales_per_day(int sales[][100], int m, int n) 
{
    for (int j = 0; j < n; j++) 
{
        int min_sales = sales[0][j];  
        for (int i = 1; i < m; i++) 
{
            if (sales[i][j] < min_sales) 
{
                min_sales = sales[i][j];
            }
        }
        printf("Ziua %d: Vânzarea minimă = %d\n", j + 1, min_sales);
    }
}
int main() 
{
    int m, n;
    printf("Introduceți numărul de produse (m): ");
    scanf("%d", &m);
    printf("Introduceți numărul de zile (n): ");
    scanf("%d", &n);
    int sales[100][100];
    printf("Introduceți vânzările:\n");
    for (int i = 0; i < m; i++) 
{
        for (int j = 0; j < n; j++) 
{
            printf("Produsul %d, ziua %d: ", i + 1, j + 1);
            scanf("%d", &sales[i][j]);
        }
    }
    printf("Vânzările minime pentru fiecare zi:\n");
    find_min_sales_per_day(sales, m, n);
    return 0;
}

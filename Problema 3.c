/******************************************************************************

                            Online C Compiler.
                Code, Compile, Run and Debug C program online.
Write your code in this editor and press "Run" button to compile and execute it.

*******************************************************************************/
#include <stdio.h>
#include <stdbool.h>
void find_products_with_constant_sales(int sales[][100], int m, int n) 
{
    for (int i = 0; i < m; i++) 
{
        bool constant_sales = true;
        int first_day_sales = sales[i][0];  
        for (int j = 1; j < n; j++) {
            if (sales[i][j] != first_day_sales) 
{
                constant_sales = false;
                break;
            }
        }
        if (constant_sales) 
{
            printf("Produsul %d are vânzări constante: %d\n", i + 1, first_day_sales);
        }
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
    printf("Produsele cu vânzări constante pe întreaga perioadă:\n");
    find_products_with_constant_sales(sales, m, n);
    return 0;
}


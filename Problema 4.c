/******************************************************************************

                            Online C Compiler.
                Code, Compile, Run and Debug C program online.
Write your code in this editor and press "Run" button to compile and execute it.

*******************************************************************************/
#include <stdio.h>
void find_sales_amplitude_for_each_product(int sales[][100], int m, int n) 
{
    for (int i = 0; i < m; i++) 
{
        int min_sales = sales[i][0];
        int max_sales = sales[i][0];

        for (int j = 1; j < n; j++) 
{
            if (sales[i][j] < min_sales) 
{
                min_sales = sales[i][j];
            }
            if (sales[i][j] > max_sales) 
{
                max_sales = sales[i][j];
            }
        }
        int amplitude = max_sales - min_sales;
        printf("Produsul %d are amplitudinea vânzărilor: %d\n", i + 1, amplitude);
    }
}
int main() {
    int m, n;
    printf("Introduceți numărul de produse (m): ");
    scanf("%d", &m);
    printf("Introduceți numărul de zile (n): ");
    scanf("%d", &n);
    int sales[100][100];
    printf("Introduceți vânzările:\n");
    for (int i = 0; i < m; i++) {
        for (int j = 0; j < n; j++) {
            printf("Produsul %d, ziua %d: ", i + 1, j + 1);
            scanf("%d", &sales[i][j]);
        }
    )
    printf("Amplitudinea vânzărilor pentru fiecare produs:\n");
    find_sales_amplitude_for_each_product(sales, m, n);
    return 0;
}


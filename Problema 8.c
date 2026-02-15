/******************************************************************************

                            Online C Compiler.
                Code, Compile, Run and Debug C program online.
Write your code in this editor and press "Run" button to compile and execute it.

*******************************************************************************/
#include <stdio.h>
int main() 
{
    int m, n, threshold;
    int a[100][100]; 
    printf("Enter the number of subscribers (m) and the number of months (n): ");
    scanf("%d %d", &m, &n);
    printf("Enter the phone conversation values for each subscriber and month:\n");
    for (int i = 0; i < m; i++) 
{
        for (int j = 0; j < n; j++) 
{
            scanf("%d", &a[i][j]);
        }
    }
    printf("Enter the threshold value: ");
    scanf("%d", &threshold);
    for (int i = 0; i < m; i++) {
        int month_count = 0; 
        for (int j = 0; j < n; j++) 
{
            if (a[i][j] < threshold) 
{
                month_count++;  
            }
        }
        printf("Subscriber %d has %d months with values below the threshold %d.\n", i + 1, month_count, threshold);
    }
    return 0;
}

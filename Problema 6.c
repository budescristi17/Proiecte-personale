/******************************************************************************

                            Online C Compiler.
                Code, Compile, Run and Debug C program online.
Write your code in this editor and press "Run" button to compile and execute it.

*******************************************************************************/
#include <stdio.h>
int main() {
    int m, n;
    int a[100][100]; 
    float average;
    printf("Enter the number of subscribers (m) and the number of months (n): ");
    scanf("%d %d", &m, &n);
    printf("Enter the phone conversation values for each subscriber and month:\n");
    for (int i = 0; i < m; i++) {
        for (int j = 0; j < n; j++) {
            scanf("%d", &a[i][j]);
        }
    }
    for (int i = 0; i < m; i++) {
        int subscriber_sum = 0;
        for (int j = 0; j < n; j++) {
            subscriber_sum += a[i][j]; 
        }
        average = (float)subscriber_sum / n; 
        printf("The average for subscriber %d is: %.2f\n", i + 1, average);
    }
    return 0;
}

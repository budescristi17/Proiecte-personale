/******************************************************************************

                            Online C Compiler.
                Code, Compile, Run and Debug C program online.
Write your code in this editor and press "Run" button to compile and execute it.

*******************************************************************************/
#include <stdio.h>
int main() {
    int m, n, value;
    int a[100][100]; 
    printf("Enter the number of subscribers (m) and the number of months (n): ");
    scanf("%d %d", &m, &n);
    printf("Enter the phone conversation values for each subscriber and month:\n");
    for (int i = 0; i < m; i++) {
        for (int j = 0; j < n; j++) {
            scanf("%d", &a[i][j]);
        }
    }
    printf("Enter the conversation value to search for: ");
    scanf("%d", &value);
    for (int j = 0; j < n; j++) {
        int last_subscriber = -1;  
        for (int i = 0; i < m; i++) {
            if (a[i][j] == value) {
                last_subscriber = i + 1;  
            }
        }
        if (last_subscriber != -1) {
            printf("For month %d, the last subscriber with value %d is: %d\n", j + 1, value, last_subscriber);
        } 
else 
{
            printf("For month %d, no subscriber has value %d.\n", j + 1, value);
        }
    }
    return 0;
}

// MyAtoi.cpp : This file contains the 'main' function. Program execution begins and ends there.
#include <stdlib.h>
#include <stdio.h>
#include <string.h>


long MyAtoi(const char* input);
constexpr int pow10(int n);

int main()
{
    char num[50] = "1997";
    int  i, len;
    int result = 0;

    if (fgets(num, 50, stdin) == NULL)
        return -1;
    len = strlen(num);

    for (i = 0; i < len; i++) {
        result = result * 10 + (num[i] - '0');
    }

    printf("%d", result);
    // printf("Integer value = %d\n", MyAtoi("1997"));
}


long MyAtoi(const char* input)
{
    long retVal = 0;
    int i, j = 0;
    i = (int)strlen(input);
 //   retVal = atoi(input);
 //   return retVal;

 
    //for (i = (int)strlen(input); i >= 0; i--, j++)
    //{
    //    int powerOf = pow10(j);
    //    int strValue = (input[i] - '0');
    //    retVal += strValue * powerOf;

    //}

    return retVal;
}


constexpr int pow10(int n) {
    int result = 1;
    if (n == 0) return 1;
    for (int i = 1; i <= n; ++i)
        result *= 10;
    return result;
}
//Nx10^n
/*
int Npow(int x, int y) {

    int solution = 1;
    while (y) {
        if (y & 1)
            solution *= x;
        x *= x;
        y >>= 1;
    }
    return solution;


    N <<= n;
    while (n--) N += N << 2;
    return N;

}

*/

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file

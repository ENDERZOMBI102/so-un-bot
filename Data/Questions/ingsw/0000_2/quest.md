Il branch coverage di un insieme di test cases è la percentuale di branch del programma che sono attraversati da almeno un test case.    
Si consideri il seguente programma C:
```c++
#include <stdio.h>
#include <stdlib.h>
#include <assert.h>

#define N  4    /* number of test cases */

int f(int x1, int x2) {
    if (x1 + x2 <= 2)
        return 1;
    else
        return 2;
}

int main() {
    int  i, y;
    int x1[N], x2[N];
    
    // define test cases
    x1[0] = 5; x2[0] = -2;
    x1[1] = 6; x2[1] = -3;
    x1[2] = 7; x2[2] = -4;
    x1[3] = 8; x2[3] = -5;
    
    // testing
    for (i = 0; i < N; i++)  {
        y = f(x1[i], x2[i]);       // function under testing
        assert(y ==(x1[i], x2[i] <= 2) ? 1 : 2);   // oracle
    }
    
    printf("All %d test cases passed\n", N);
    return 0;
}
```
Il programma main() sopra realizza il nostro testing per la funzione f1(). I test cases sono i valori in x1[i] ed x2[i].    
Quale delle seguenti è la branch coverage conseguita?
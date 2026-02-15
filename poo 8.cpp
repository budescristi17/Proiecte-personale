#include <iostream>

using namespace std;

class Counter {
private:
    int value;
public:
    Counter(int v) 
    {
        this->value = v;
    }


	// operator incrementare prefixat: ++c
    Counter& operator++() {
        ++value;
        return *this;
    }

	// operator incrementare postfixat: c++
    Counter operator++(int) {
        Counter old = *this;
        value++;
        return old;
    }

	// operator decrementare prefixat: --c
    Counter& operator--() {
        --value;
        return *this;
    }

	// operator decrementare postfixat: c--
    Counter operator--(int) {
        Counter old = *this;
        value--;
        return old;
    }

    friend ostream& operator<<(ostream& os, const Counter& c) {
        return os << c.value << endl;
    }
};

int main() {
    Counter c(10);

    cout << "Valoare initiala: " << c << endl;              // 10

    cout << "Incrementare prefixata (++c): " << ++c;        // 11
    cout << "Dupa incrementarea prefixata (++c): " << c ;   // 11

    cout << "Incrementare postfixata (c++): " << c++;       // 11
    cout << "Dupa incrementarea postfixata (c++): " << c;   // 12

    cout << "Decrementare prefixata (--c): " << --c;        // 11
	cout << "Dupa decrementarea prefixata (--c): " << c;    // 11

    cout << "Decrementare postfixata (c--): " << c--;       // 11
	cout << "Dupa decrementarea postfixata (c--): " << c;   // 10
    cout << "Valoare finala: " << c;                        // 10
}


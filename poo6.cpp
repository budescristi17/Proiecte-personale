#include <iostream>

using namespace std;

class Cent {
private:
    int valoare;

public:
    Cent(int v) : valoare(v) {}

	// Cast la int, doar explicit
    explicit operator int() {
        return valoare;
    }

    friend ostream& operator<<(ostream& os, const Cent& c) {
        os << c.valoare << " centi";
        return os;
    }
};

class Dolar {
private:
    int valoare;
public:
    Dolar(int d) : valoare(d) {}

	// Cast la cent, permite atat implicit cat si explicit
    operator Cent() {
        return Cent(valoare * 100);
    }

    friend ostream& operator<<(ostream& os, const Dolar& d) {
        os << d.valoare << " dolari";
        return os;
    }
};

int main() {
    Cent c(40);
    cout << c << endl;

    Dolar d(11);
    cout << d << endl;

    //cast implicit
    Cent centiImplicit = d;
    cout << centiImplicit << endl;

	//cast explicit
	Cent centiExplicit = (Cent)(d);
	cout << centiExplicit << endl;

	//cast explicit
    int centiInt = (int)(centiImplicit);
    cout << centiInt << " centi" << endl;
}
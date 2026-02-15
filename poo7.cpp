#include <iostream>

using namespace std;

class RON {
private:
    double valoare;

public:
    RON(double v) : valoare(v) {}

    double getValue() {
        return valoare;
    }

    friend ostream& operator<<(ostream& os, const RON& r) {
        os << r.valoare << " RON";
        return os;
    }
};

class USD {
private:
    double valoare;

public:
    USD(double v) : valoare(v) {}

    double getValue() {
        return valoare;
    }

    friend ostream& operator<<(ostream& os, const USD& u) {
        os << u.valoare << " USD";
        return os;
    }
};

class ConvertorValutar {
private:
    double ronToUsdRate;

public:
    ConvertorValutar(double rate)
        : ronToUsdRate(rate) {
    }

    // RON -> USD
    USD operator()(RON& r) {
        return USD(r.getValue() / ronToUsdRate);
    }

    // USD -> RON
    RON operator()(USD& u) {
        return RON(u.getValue() * ronToUsdRate);
    }
};

int main() {
    ConvertorValutar converter(4.34);

    RON r(250);
    USD u = converter(r);

    cout << r << " = " << u << endl;

    RON back = converter(u);
    cout << u << " = " << back << endl;
}
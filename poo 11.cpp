#include <iostream>

using namespace std;

class Connection {
private:
    bool connected;

public:
    Connection(bool state)
    {
        this->connected = state;
    }

    void connect() {
        connected = true;
    }

    void disconnect() {
        connected = false;
    }

    bool operator!() const {
        return !connected;
    }
};

int main()
{
    Connection conn(false);

    if (!conn) {
        cout << "Not connected" << endl;
    }
    else
    {
		cout << "Connected" << endl;
    }

    conn.connect();

    if (!conn) {
        cout << "Not connected" << endl;
    }
    else
    {
        cout << "Connected" << endl;
    }

    conn.disconnect();

    if (!conn) {
        cout << "Not connected" << endl;
    }
    else
    {
        cout << "Connected" << endl;
    }

    return 0;
}
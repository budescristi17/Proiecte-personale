#include <iostream>
#include <cstring>

using namespace std;

class IBehavior {
public:
    virtual void Eat() = 0;
    virtual void Drink() = 0;
    virtual ~IBehavior() {}
};

class Animal : public IBehavior {
protected:
    char* name;
    int age;

public:
    Animal(const char* name, int age) : age(age) {
        this->name = new char[strlen(name) + 1];
        strcpy_s(this->name, strlen(name) + 1, name);
    }

	// Destructor virtual, pentru a asigura distrugerea corecta a obiectelor derivate
    virtual ~Animal() {
        cout << "Destroying Animal\n";
        delete[] this->name;
    }

    //Metoda non-virtuala
    void WhatAmI() {
        cout << "I am an animal\n";
    }

	//Metoda virtuala
    virtual void Display() {
        cout << "Name: " << name << "\nAge: " << age << endl;
    }

	//Metoda virtuala pura
    virtual void Speak() = 0;
};

class Dog : public Animal {
private:
    char* owner;

public:
    Dog(const char* name, int age, const char* owner)
        : Animal(name, age) {
        this->owner = new char[strlen(owner) + 1];
        strcpy_s(this->owner, strlen(owner) + 1, owner);
    }

    ~Dog() override {
        cout << "Destroying Dog\n";
        delete[] owner;
    }

	//Shadowing, nu polimorfica
    void WhatAmI() {
        cout << "I am a dog\n";
    }

	//Suprascrierea metodei virtuale
    void Display() override {
        Animal::Display();
        cout << "Owner: " << owner << endl;
    }

	// Suprascrierea metodei virtuale pure
    void Speak() override {
        cout << "Woof\n";
    }

	// Implementarea metodelor din interfata
    void Eat() override {
        cout << "Dog eats dog food\n";
    }

	// Implementarea metodelor din interfata
    void Drink() override {
        cout << "Dog drinks water\n";
    }
};

class Cat : public Animal {
private:
    char* breed;

public:
    Cat(const char* name, int age, const char* breed)
        : Animal(name, age) {
        this->breed = new char[strlen(breed) + 1];
        strcpy_s(this->breed, strlen(breed) + 1, breed);
    }

	// Destructor 
    ~Cat() override {
        cout << "Destroying Cat\n";
        delete[] breed;
    }

	// Shadowing, nu polimorfica
    void WhatAmI() {
        cout << "I am a cat\n";
    }

	// Suprascrierea metodei virtuale
    void Display() override {
        Animal::Display();
        cout << "Breed: " << breed << endl;
    }

	// Suprascrierea metodei virtuale pure
    void Speak() override {
        cout << "Meow\n";
    }

	// Implementarea metodelor din interfata
    void Eat() override {
        cout << "Cat eats cat food\n";
    }

	// Implementarea metodelor din interfata
    void Drink() override {
        cout << "Cat drinks milk\n";
    }
};

// ================= MAIN =================
int main() {

    Animal* animals[2];
    animals[0] = new Cat("Coco", 5, "Maine Coon");
    animals[1] = new Dog("Labus", 10, "Lucian");

    for (int i = 0; i < 2; i++) {
		// Deoarece metoda WhatAmI nu este virtuala,
		// se apeleaza versiunea din clasa de baza,
		// iar programul va afisa mereu "I am an animal"
        animals[i]->WhatAmI();   
    }


    for (int i = 0; i < 2; i++) {
		// Deoarece metodele Display si Speak sunt virtuale,
		// se apeleaza versiunile din clasele derivate,
		// desi pointerul este de tip Animal*
        animals[i]->Display();
        animals[i]->Speak(); 
        cout << endl;
    }

    for (int i = 0; i < 2; i++) {
        IBehavior* b = animals[i];
		// Apelam metodele din interfata IBehavior
		// Acestea sunt implementate in clasele derivate,
		// iar apelurile vor fi corecte datorita polimorfismului
        b->Eat();
        b->Drink();
        cout << endl;
    }

    for (int i = 0; i < 2; i++) {
		// Distrugem obiectele corect datorita destructorului virtual
		// Se va apela mai intai destructorul clasei derivate, apoi al clasei de baza,
		// chiar daca pointerul este de tip Animal*
        delete animals[i];       
    }

    return 0;
}

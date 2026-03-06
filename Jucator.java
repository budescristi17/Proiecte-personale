public class Jucator {

    private String nume;
    private int varsta;
    private double bani;

    public Jucator() {
    }

    public Jucator(String nume, int varsta, double bani) {
        this.nume = nume;
        setVarsta(varsta);
        this.bani = bani;
    }

    public String getNume() {
        return nume;
    }

    public void setNume(String nume) {
        this.nume = nume;
    }

    public int getVarsta() {
        return varsta;
    }

    public void setVarsta(int varsta) {
        if (varsta < 18) {
            throw new IllegalArgumentException("Jucatorul trebuie sa aiba minim 18 ani");
        }
        this.varsta = varsta;
    }

    public double getBani() {
        return bani;
    }

    public void setBani(double bani) {
        this.bani = bani;
    }

    @Override
    public String toString() {
        return "Nume jucator: " + nume +
                "\nVarsta: " + varsta +
                "\nBani: " + bani;
    }
}
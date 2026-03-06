public class Tranzactie {

    private Jucator jucator;
    private Pacanea pacanea;
    private double sumaJucata;

    public Tranzactie() {
    }

    public Tranzactie(Jucator jucator, Pacanea pacanea, double sumaJucata) {
        this.jucator = jucator;
        this.pacanea = pacanea;
        this.sumaJucata = sumaJucata;
    }

    public Jucator getJucator() {
        return jucator;
    }

    public void setJucator(Jucator jucator) {
        this.jucator = jucator;
    }

    public Pacanea getPacanea() {
        return pacanea;
    }

    public void setPacanea(Pacanea pacanea) {
        this.pacanea = pacanea;
    }

    public double getSumaJucata() {
        return sumaJucata;
    }

    public void setSumaJucata(double sumaJucata) {
        this.sumaJucata = sumaJucata;
    }

    @Override
    public String toString() {
        return "Jucator: " + jucator.getNume() +
                "\nPacanea: " + pacanea.getNumePacanea() +
                "\nSuma jucata: " + sumaJucata;
    }
}

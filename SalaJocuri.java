import java.util.ArrayList;

public class SalaJocuri {

    private String numeSala;
    private String adresa;
    private ArrayList<Pacanea> pacanele;

    public SalaJocuri() {
        pacanele = new ArrayList<>();
    }

    public SalaJocuri(String numeSala, String adresa) {
        this.numeSala = numeSala;
        this.adresa = adresa;
        this.pacanele = new ArrayList<>();
    }

    public String getNumeSala() {
        return numeSala;
    }

    public void setNumeSala(String numeSala) {
        this.numeSala = numeSala;
    }

    public String getAdresa() {
        return adresa;
    }

    public void setAdresa(String adresa) {
        this.adresa = adresa;
    }

    public ArrayList<Pacanea> getPacanele() {
        return pacanele;
    }

    public void adaugaPacanea(Pacanea p) {
        pacanele.add(p);
    }

    public double profitTotal() {
        double suma = 0;
        for (Pacanea p : pacanele) {
            suma += p.getProfit();
        }
        return suma;
    }

    @Override
    public String toString() {
        String rezultat = "Sala: " + numeSala +
                "\nAdresa: " + adresa +
                "\nPacanele:\n";

        for (Pacanea p : pacanele) {
            rezultat += p + "\n\n";
        }

        rezultat += "Profit total: " + profitTotal();
        return rezultat;
    }
}
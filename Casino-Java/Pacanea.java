public class Pacanea {

    private int profit;
    private int jocuri;
    private String numePacanea;

    public Pacanea() {
    }

    public Pacanea(String numePacanea, int jocuri, int profit) {
        this.numePacanea = numePacanea;
        setJocuri(jocuri);
        this.profit = profit;
    }

    public int getProfit() {
        return profit;
    }

    public void setProfit(int profit) {
        this.profit = profit;
    }

    public int getJocuri() {
        return jocuri;
    }

    public void setJocuri(int jocuri) {
        if (jocuri < 3) {
            throw new IllegalArgumentException("Invalid, o pacanea n are cum sa aiba mai putin de 3 jocuri");
        }
        this.jocuri = jocuri;
    }

    public String getNumePacanea() {
        return numePacanea;
    }

    public void setNumePacanea(String numePacanea) {
        this.numePacanea = numePacanea;
    }

    @Override
    public String toString() {
        return "Nume pacanea: " + numePacanea +
                "\nNumar jocuri: " + jocuri +
                "\nProfit: " + profit;
    }
}
import java.util.Scanner;

public class main {

    public static void main(String[] args) {

        Scanner scanner = new Scanner(System.in);

        SalaJocuri sala = new SalaJocuri("Casino Vegas", "Constanta");

        while(scanner.hasNextLine()){

            String linie = scanner.nextLine();

            String[] parti = linie.split(";");

            String numePacanea = parti[0];
            int jocuri = Integer.parseInt(parti[1]);
            int profit = Integer.parseInt(parti[2]);

            String numeJucator = parti[3];
            int varsta = Integer.parseInt(parti[4]);
            double bani = Double.parseDouble(parti[5]);
            double suma = Double.parseDouble(parti[6]);

            Pacanea p = new Pacanea(numePacanea, jocuri, profit);

            Jucator j = new Jucator(numeJucator, varsta, bani);

            Tranzactie t = new Tranzactie(j, p, suma);

            sala.adaugaPacanea(p);

            System.out.println("PACANEA:");
            System.out.println(p);

            System.out.println("JUCATOR:");
            System.out.println(j);

            System.out.println("TRANZACTIE:");
            System.out.println(t);

            System.out.println("-------------------");
        }

        System.out.println();
        System.out.println("===== SALA DE JOCURI =====");
        System.out.println(sala);
    }
}
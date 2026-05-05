using CabinetMedical.Forms;
using CabinetMedical.Services;

namespace CabinetMedical;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Incarcam datele din fisiere JSON inainte sa pornim interfata.
        DataStorage.Initialize();

        Application.Run(new MainForm());

        // Salvam automat si la inchiderea aplicatiei, ca datele sa ramana pastrate.
        DataStorage.SaveAll();
    }
}

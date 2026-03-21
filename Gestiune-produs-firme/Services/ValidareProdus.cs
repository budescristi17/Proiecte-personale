namespace Gestiune_produs_firme
{
    public static class ValidareProdus
    {
        public static string Valideaza(Produs p)
        {
            if (p == null)
                return "Produsul este null.";

            if (p.Cod <= 0)
                return "Codul trebuie sa fie mai mare decat 0.";

            if (string.IsNullOrWhiteSpace(p.Denumire))
                return "Denumirea nu poate fi goala.";

            if (p.Pret <= 0)
                return "Pretul trebuie sa fie mai mare decat 0.";

            if (string.IsNullOrWhiteSpace(p.Categorie))
                return "Categoria nu poate fi goala.";

            if (p.Stoc < 0)
                return "Stocul nu poate fi negativ.";

            return "";
        }
    }
}


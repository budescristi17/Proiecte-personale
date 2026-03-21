using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace Gestiune_produs_firme
{
    public class DatabaseHelper
    {
        private readonly string connectionString = "Data Source=produse.db";

        public void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                    CREATE TABLE IF NOT EXISTS Produse (
                        Cod INTEGER PRIMARY KEY,
                        Denumire TEXT NOT NULL,
                        Pret REAL NOT NULL,
                        Categorie TEXT NOT NULL,
                        Stoc INTEGER NOT NULL
                    );
                ";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Produs> GetAllProduse()
        {
            List<Produs> produse = new List<Produs>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT Cod, Denumire, Pret, Categorie, Stoc FROM Produse";

                using (var command = new SqliteCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        produse.Add(new Produs(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetDouble(2),
                            reader.GetString(3),
                            reader.GetInt32(4)
                        ));
                    }
                }
            }

            return produse;
        }

        public void InsertProdus(Produs p)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                    INSERT INTO Produse (Cod, Denumire, Pret, Categorie, Stoc)
                    VALUES (@cod, @denumire, @pret, @categorie, @stoc);
                ";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@cod", p.Cod);
                    command.Parameters.AddWithValue("@denumire", p.Denumire);
                    command.Parameters.AddWithValue("@pret", p.Pret);
                    command.Parameters.AddWithValue("@categorie", p.Categorie);
                    command.Parameters.AddWithValue("@stoc", p.Stoc);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProdus(Produs p, int codInitial)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                    UPDATE Produse
                    SET Cod = @cod,
                        Denumire = @denumire,
                        Pret = @pret,
                        Categorie = @categorie,
                        Stoc = @stoc
                    WHERE Cod = @codInitial;
                ";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@cod", p.Cod);
                    command.Parameters.AddWithValue("@denumire", p.Denumire);
                    command.Parameters.AddWithValue("@pret", p.Pret);
                    command.Parameters.AddWithValue("@categorie", p.Categorie);
                    command.Parameters.AddWithValue("@stoc", p.Stoc);
                    command.Parameters.AddWithValue("@codInitial", codInitial);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProdus(int cod)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "DELETE FROM Produse WHERE Cod = @cod";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@cod", cod);
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool AreProduse()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT COUNT(*) FROM Produse";

                using (var command = new SqliteCommand(sql, connection))
                {
                    long count = (long)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}

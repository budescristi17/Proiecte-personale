using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Model_Test_PAW
{
    public partial class FormMedicamente : Form
    {
        private string connectionString = "Data Source=medicamente.db;Version=3;";

        public FormMedicamente()
        {
            InitializeComponent();
            InitializareBD();
            IncarcaMedicamente();
        }

        private void FormMedicamente_Load(object sender, EventArgs e)
        {
        }

        private void InitializareBD()
        {
            if (!File.Exists("medicamente.db"))
            {
                SQLiteConnection.CreateFile("medicamente.db");
            }

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string sql = @"CREATE TABLE IF NOT EXISTS Medicamente
                               (
                                   Cod INTEGER PRIMARY KEY,
                                   Denumire TEXT NOT NULL,
                                   Pret REAL NOT NULL
                               )";

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }

        private void IncarcaMedicamente()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT Cod, Denumire, Pret FROM Medicamente";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvMedicamente.DataSource = dt;
            }
        }

        private void btnAdauga_Click(object sender, EventArgs e)
        {
            int cod;
            float pret;

            if (!int.TryParse(txtCod.Text, out cod))
            {
                MessageBox.Show("Cod invalid.");
                return;
            }

            if (txtDenumire.Text == "")
            {
                MessageBox.Show("Introdu denumirea.");
                return;
            }

            if (!float.TryParse(txtPret.Text, out pret))
            {
                MessageBox.Show("Pret invalid.");
                return;
            }

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string sql = "INSERT INTO Medicamente(Cod, Denumire, Pret) VALUES(@cod, @denumire, @pret)";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@cod", cod);
                cmd.Parameters.AddWithValue("@denumire", txtDenumire.Text);
                cmd.Parameters.AddWithValue("@pret", pret);

                cmd.ExecuteNonQuery();
            }

            IncarcaMedicamente();
            CurataCampuri();
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            int cod;
            float pret;

            if (!int.TryParse(txtCod.Text, out cod))
            {
                MessageBox.Show("Cod invalid.");
                return;
            }

            if (txtDenumire.Text == "")
            {
                MessageBox.Show("Introdu denumirea.");
                return;
            }

            if (!float.TryParse(txtPret.Text, out pret))
            {
                MessageBox.Show("Pret invalid.");
                return;
            }

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string sql = "UPDATE Medicamente SET Denumire=@denumire, Pret=@pret WHERE Cod=@cod";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@cod", cod);
                cmd.Parameters.AddWithValue("@denumire", txtDenumire.Text);
                cmd.Parameters.AddWithValue("@pret", pret);

                cmd.ExecuteNonQuery();
            }

            IncarcaMedicamente();
            CurataCampuri();
        }

        private void btnSterge_Click(object sender, EventArgs e)
        {
            int cod;

            if (!int.TryParse(txtCod.Text, out cod))
            {
                MessageBox.Show("Cod invalid.");
                return;
            }

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string sql = "DELETE FROM Medicamente WHERE Cod=@cod";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@cod", cod);

                cmd.ExecuteNonQuery();
            }

            IncarcaMedicamente();
            CurataCampuri();
        }

        private void dgvMedicamente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtCod.Text = dgvMedicamente.Rows[e.RowIndex].Cells["Cod"].Value.ToString();
                txtDenumire.Text = dgvMedicamente.Rows[e.RowIndex].Cells["Denumire"].Value.ToString();
                txtPret.Text = dgvMedicamente.Rows[e.RowIndex].Cells["Pret"].Value.ToString();
            }
        }

        private void CurataCampuri()
        {
            txtCod.Clear();
            txtDenumire.Clear();
            txtPret.Clear();
        }

        private void btnAdauga_Click_1(object sender, EventArgs e)
        {
            btnAdauga_Click(sender, e);
        }
    }
}
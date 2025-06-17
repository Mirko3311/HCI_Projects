using ASystem;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Configuration;

namespace PrviProjektniZadatakHCI.DataAccess
{

    class KorisnikDataAccess
    {


        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["MySql_hci"].ConnectionString;

        public void InsertKorisnik(Korisnik korisnik)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO Korisnik (Ime, Prezime, Email, Username, Password, Tip_korisnika) " +
                           "VALUES (@Ime, @Prezime, @Email, @Username, @Password, @Tip_korisnika)";

            cmd.Parameters.AddWithValue("@Ime", korisnik.ime);
            cmd.Parameters.AddWithValue("@Prezime", korisnik.prezime);
            cmd.Parameters.AddWithValue("@Email", korisnik.email);
            cmd.Parameters.AddWithValue("@Username", korisnik.username);
            cmd.Parameters.AddWithValue("@Password", korisnik.password);
            cmd.Parameters.AddWithValue("@Tip_korisnika", korisnik.tipKorisnika);
            cmd.ExecuteNonQuery();
            korisnik.idKorisnika = (int)cmd.LastInsertedId;
        }


        public static bool provjeriLozinku(Korisnik korisnik, string currentPassword)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT password FROM Korisnik WHERE username = @username";
                    cmd.Parameters.AddWithValue("@username", korisnik.username);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        string storedPassword = result.ToString();
                        return BCrypt.Net.BCrypt.Verify(currentPassword, storedPassword);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Greška pri provjeri lozinke: " + ex.Message);
            }

            return false;
        }

        public static bool azurirajLozinku(Korisnik korisnik, string novaLozinka)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE Korisnik SET Password = @nova_lozinka WHERE idKorisnik = @idKorisnika";

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(novaLozinka);

                cmd.Parameters.AddWithValue("@idKorisnika", korisnik.idKorisnika);
                cmd.Parameters.AddWithValue("@nova_lozinka", hashedPassword);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }


        public static bool deleteKorisnik(Korisnik korisnik)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"DELETE FROM Korisnik WHERE idKorisnik = @parametar";
                cmd.Parameters.AddWithValue("@parametar", korisnik.idKorisnika);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public int getIdKorisnika(Korisnik korisnik)
        {
            return korisnik.idKorisnika;
        }
        public static ObservableCollection<Korisnik> GetKorisnici()
        {
            ObservableCollection<Korisnik> korisnici = new ObservableCollection<Korisnik>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * from Korisnik k order by k.ime";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                Korisnik korisnik = new Korisnik(
                    reader.GetInt32("idKorisnik"),
                    reader.GetString("ime"),
                    reader.GetString("prezime"),
                    reader.GetString("email"),
                    reader.GetString("username"),
                    reader.GetString("password"),
                    reader.GetString("tip_korisnika"));
                korisnici.Add(korisnik);
            }
            return korisnici;
        }

        public static string GetTheme(string username)
        {
            string theme = "Light";


            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT Tema FROM Korisnik WHERE Username = @username";
                        command.Parameters.AddWithValue("@username", username);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                theme = reader["Tema"] != DBNull.Value
                                    ? reader["Tema"].ToString()
                                    : "Light";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Greška prilikom dohvatanja teme: " + ex.Message);
            }

            return theme;
        }

        public static Korisnik getKorisnika(String username)
        {

            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = @"SELECT * FROM Korisnik where username = @str";
            command.Parameters.AddWithValue("@str", username);
            MySqlDataReader reader = command.ExecuteReader();

            Korisnik korisnik = new Korisnik(
               reader.GetInt32("idKorisnik"),
               reader.GetString("ime"),
               reader.GetString("prezime"),
               reader.GetString("email"),
               reader.GetString("username"),
               reader.GetString("password"),
               reader.GetString("tip_korisnika"));
            return korisnik;

            reader.Close();
            conn.Close();


        }

        public static Korisnik getKorisnikById(int id)
        {

            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = @"SELECT * FROM Korisnik where idKorisnik = @str";
            command.Parameters.AddWithValue("@str", id);
            MySqlDataReader reader = command.ExecuteReader();

            Korisnik korisnik = new Korisnik(
               reader.GetInt32("idKorisnik"),
               reader.GetString("ime"),
               reader.GetString("prezime"),
               reader.GetString("email"),
               reader.GetString("username"),
               reader.GetString("password"),
               reader.GetString("tip_korisnika"));
            return korisnik;

            reader.Close();
            conn.Close();


        }

        public static string VerifyUser(string username, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM korisnik WHERE username = @username", conn);
                cmd.Parameters.AddWithValue("@username", username);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string hesiranaLozinka = reader.GetString("password");
                        string tip = reader.GetString("Tip_korisnika");


                        if (BCrypt.Net.BCrypt.Verify(password, hesiranaLozinka))
                        {
                            return tip;
                        }
                    }
                }
            }

            return null; // ako nije validan korisnik
        }




    }
}

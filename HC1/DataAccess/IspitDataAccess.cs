﻿
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using ASystem;
using MySql.Data.MySqlClient;

namespace PrviProjektniZadatakHCI.DataAccess
{
    class IspitDataAccess
    {

        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["MySql_hci"].ConnectionString;

        public static bool sacuvajIspit(double bodovi, int ocjena, DateTime datum, Predmet predmet, Student student)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT into Ispit_Student(Bodovi,Ocjena,Ispit_DatumIspita,Student_Korisnik_idKorisnik,Predmet_idPredmeta) values (@Bodovi,@Ocjena,@Datum,@Id,@Predmet)";
                command.Parameters.AddWithValue("@Bodovi", bodovi);
                command.Parameters.AddWithValue("@Ocjena", ocjena);
                command.Parameters.AddWithValue("@Datum", datum);
                command.Parameters.AddWithValue("@Id", student.idKorisnika);
                command.Parameters.AddWithValue("@Predmet", predmet.IdPredmeta);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške: " + ex.Message);
                return false;
            }
            finally
            {

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }


        public static bool obrisiOcjenu(double bodovi, int ocjena, DateTime datum, Predmet predmet, Student student)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "DELETE FROM Ispit_Student WHERE Bodovi = @Bodovi AND Ocjena = @Ocjena AND Ispit_DatumIspita = @Datum AND Student_Korisnik_idKorisnik = @Id AND Predmet_idPredmeta = @Predmet";
                command.Parameters.AddWithValue("@Bodovi", bodovi);
                command.Parameters.AddWithValue("@Ocjena", ocjena);
                command.Parameters.AddWithValue("@Datum", datum);
                command.Parameters.AddWithValue("@Id", student.idKorisnika);
                command.Parameters.AddWithValue("@Predmet", predmet.IdPredmeta);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }


        public static bool daLiPostojiOcjena(DateTime datum, Predmet predmet, Student student)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand command = conn.CreateCommand())
                    {
                        command.CommandText = @"SELECT COUNT(*) 
                                        FROM Ispit_Student 
                                        WHERE Student_Korisnik_idKorisnik = @studentId 
                                          AND Predmet_idPredmeta = @predmetId 
                                          AND Ispit_DatumIspita = @datum";

                        command.Parameters.AddWithValue("@studentId", student.idKorisnika);
                        command.Parameters.AddWithValue("@predmetId", predmet.IdPredmeta);
                        command.Parameters.AddWithValue("@datum", datum);

                        var result = command.ExecuteScalar();
                        return Convert.ToInt32(result) > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Došlo je do greške: " + ex.Message);
                    return false;
                }
            }
        }


        public static ObservableCollection<Ispit> pregledIspita(Predmet predmet, Student student)
        {

            ObservableCollection<Ispit> ispiti = new ObservableCollection<Ispit>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = " SELECT Ocjena, Bodovi, Ispit_DatumIspita, p.idPredmeta, s.Korisnik_idKorisnik " +
                    " from Ispit_Student ip  inner join Student s on s.Korisnik_idKorisnik = ip.Student_Korisnik_idKorisnik" +
                    "  inner join Predmet p on p.idPredmeta = ip.Predmet_idPredmeta  where Predmet_idPredmeta = @parametarPredmet  and  Korisnik_idKorisnik = @parametarStudent";

                command.Parameters.AddWithValue("@parametarPredmet", predmet.IdPredmeta);
                command.Parameters.AddWithValue("@parametarStudent", student.idKorisnika);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    Ispit ispit = new Ispit
                    {
                        bodovi = reader.GetDouble("Bodovi"),
                        ocjena = reader.GetInt32("Ocjena"),
                        datumIspita = reader.GetDateTime("Ispit_DatumIspita"),
                        Student = student,
                        predmetId = predmet.IdPredmeta
                    };
                    ispiti.Add(ispit);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doslo je do greske:" + ex.Message);

            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return ispiti;
        }
    }
}


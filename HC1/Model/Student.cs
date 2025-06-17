using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

namespace ASystem
{
    public class Student : Korisnik
    {
       
        public string BrojIndeksa { get; set; }
        public int GodinaStudija { get; set; }
        public string DisplayText
        {
            get { return $"{ime} {prezime}"; }
        }

    
        public Student(int idKorisnika, string ime, string prezime) : base(idKorisnika, ime, prezime) 
        {}
        public bool IsPrisutanChecked { get; set; }
        public bool IsOdsutanChecked { get; set; }
        public Student(int idKorisnika, string ime, string prezime, string email, string username, string hashedPassword, string tipKorisnika, string brojIndeksa, int godinaStudija)
            : base(idKorisnika,ime, prezime, email, username, hashedPassword, tipKorisnika)  
        {
            this.BrojIndeksa = brojIndeksa;
            this.GodinaStudija = godinaStudija;
        }

        public Student()
        {
            idKorisnika = 0;
            ime = string.Empty;
            prezime = string.Empty;
            email = string.Empty;
            username = string.Empty;
            password = string.Empty;
            BrojIndeksa = string.Empty;
            GodinaStudija = 0;
        }
        public Student DeepCopy()
        {
            return (Student)this.MemberwiseClone();
        }

    }

}

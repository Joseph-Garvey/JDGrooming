using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows.Media.Imaging;
using System.IO;
using System.Data;

namespace JDGrooming.Classes.Database_Management
{
    public class ProjectDBAccess
    {
        public Database db = new Database();

        #region Methods
        public void QueryDatabase(String Query)
        {
            using (SqlCommand command = db.Conn.CreateCommand())
            {
                command.CommandText = Query;
                db.Cmd = command;
                using (db.Cmd.ExecuteReader()) { }
            }
        }
        public SqlDataReader ReadDatabase(String Query)
        {
            using (SqlCommand command = db.Conn.CreateCommand())
            {
                command.CommandText = Query;
                db.Cmd = command;
                return db.Cmd.ExecuteReader();
            }
        }
        public List<String> GetBreeds()
        {
            List<String> breeds = new List<string> { };
            try
            {
                SqlDataReader reader = ReadDatabase("SELECT [Name] FROM [Breed];");
                while (reader.Read()) breeds.Add(reader.GetString(0));
                reader.Close();
            }
            catch { db.Rdr.Close(); }
            return breeds;
        }
        public int GetBreedID(String breedname)
        {
            int id = new int();
            try
            {
                SqlDataReader reader = ReadDatabase("SELECT [ID] FROM [Breed] WHERE [Name]='" + breedname + "';");
                while (reader.Read()) id = reader.GetInt32(0);
                reader.Close();
            }
            catch { db.Rdr.Close(); }
            return id;
        }
        public BitmapImage GetBreedImage(String breedname)
        {
            BitmapImage image = new BitmapImage();
            try
            {
                SqlDataReader reader = ReadDatabase("SELECT [DefaultImage] FROM [Breed] WHERE [Name]='" + breedname + "';");
                while (reader.Read())
                {
                    String s = reader.GetString(0);
                    if (!File.Exists(s)) { throw new FileNotFoundException(); } // fix in future
                    image = new BitmapImage(new Uri(s));
                }
                reader.Close();
            }
            catch { }
            return image;
        }
        public List<String> GetClientsString()
        {
            List<String> clients = new List<string> { };
            try
            {
                SqlDataReader reader = ReadDatabase("SELECT ID, Surname+' '+Forename AS Name FROM [Client];");
                while (reader.Read()) clients.Add(reader.GetInt32(0) + " - " + reader.GetString(1));
                reader.Close();
            }
            catch { db.Rdr.Close(); }
            return clients;
        }
        public int GetClientIDFromString(String input)
        {
            int id = int.Parse(input.Substring(0, input.IndexOf(' ')));
            return id;
        }
        public DataView FillDogTable()
        {
            using (SqlCommand command = db.Conn.CreateCommand())
            {
                command.CommandText = "SELECT [Dog].ID, [Dog].[Name], [Breed].[Name], Forename + ' ' + Surname AS Client_Name, " +
                    "DOB, [Image] FROM [Dog] " +
                    "INNER JOIN [Client] ON [Dog].ClientID = [Client].Id " +
                    "INNER JOIN [Breed] ON [Dog].BreedID = [Breed].Id;";
                db.Cmd = command;
                SqlDataAdapter sda = new SqlDataAdapter(db.Cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt.DefaultView;
            }
        }
        #endregion
    }
}

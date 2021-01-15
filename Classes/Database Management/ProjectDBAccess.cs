using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

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
                SqlDataReader reader = ReadDatabase("SELECT Name FROM [Breed];");
                while (reader.Read()) breeds.Add(reader.GetString(0));
                reader.Close();
            }
            catch { db.Rdr.Close(); }
            return breeds;
        }
        public List<String> GetClients()
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
        #endregion
    }
}

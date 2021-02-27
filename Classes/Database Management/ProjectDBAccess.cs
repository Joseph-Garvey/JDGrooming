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
using System.Windows;

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
        public String GetBreedImageSource(String breedname)
        {
            String image = "";
            try
            {
                SqlDataReader reader = ReadDatabase("SELECT [DefaultImage] FROM [Breed] WHERE [Name]='" + breedname + "';");
                while (reader.Read())
                {
                    String s = reader.GetString(0);
                    if (!File.Exists(s)) { throw new FileNotFoundException(); } // fix in future
                    image = s;
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
                command.CommandText = "SELECT [Dog].ID, [Dog].[Name], [Breed].[Name] AS Breed, Forename + ' ' + Surname AS Client_Name, " +
                    "DOB, [Status] FROM [Dog] " +
                    "INNER JOIN [Client] ON [Dog].ClientID = [Client].Id " +
                    "INNER JOIN [Breed] ON [Dog].BreedName = [Breed].Name;";
                db.Cmd = command;
                SqlDataAdapter sda = new SqlDataAdapter(db.Cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt.DefaultView;
            }
        }
        /// <summary>
        /// only update the changed field in future
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="breed"></param>
        public void UpdateDog(string id, string name, string breedname, string doginfo, bool status)
        {
            String Start = String.Format("UPDATE [Dog] SET [Name] = '{0}', [BreedName] = '{1}', [Status] = '{2}'",
                name, breedname, status ? 1:0);
            String SQL = Start;
            if(doginfo != "") { SQL += ", [AdditionalInfo] = '" + doginfo + "'"; }
            SQL += " WHERE [ID] = " + id + " ;";
            QueryDatabase(SQL);
        }
        public String[] GetUpdateDog(string id)
        {
            String[] results = new String[4];
            try
            {
                SqlDataReader reader = ReadDatabase("SELECT AdditionalInfo, Info, Image, DefaultImage FROM [Dog] " +
                    "INNER JOIN [Breed] ON [Dog].BreedName = [Breed].Name" +
                    " WHERE [Dog].ID = " + id + ";");
                while (reader.Read()) {
                    if(!reader.IsDBNull(0)) results[0] = (reader.GetString(0));
                    if (!reader.IsDBNull(1)) results[1] = (reader.GetString(1));
                    if (!reader.IsDBNull(2)) results[2] = (reader.GetString(2));
                    if (!reader.IsDBNull(3)) results[3] = (reader.GetString(3));
                        }
                reader.Close();
            }
            catch { db.Rdr.Close(); }
            return results;
        }

        public void RegisterDog(String DogName, int ClientIndex, String ClientName, int BreedIndex, String BreedName, String AdditionalInfo, String Img_Source, DateTime DOB)
        {
            // add null checks + whitespace for name etc check user reqs
            const String failedNameFormat = "\u2022 Names must consist of letters.";
            const String failedNameLength = "\u2022 Names must be less than 32 characters."; // less than or equal to?
            const String failedAdditionaInfoLength = "\u2022 Information must be less than 255 characters.";
            const String failedFileNameLength = "\u2022 Image file-path must be less than 260 characters. (Windows limit)";
            String Errors = "";
            if (DogName.Length > 32) { AddToErrors(ref Errors, failedNameLength); }
            if (AdditionalInfo.Length > 255) { AddToErrors(ref Errors, failedAdditionaInfoLength); }
            if (!CheckCharsAreLetters(DogName)) { AddToErrors(ref Errors, failedNameFormat); }
            if ((Img_Source ?? "").Length > 260) { AddToErrors(ref Errors, failedFileNameLength); }
            if (Errors != "") MessageBox.Show(Errors, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                // image is optional // add check that image still exists
                String Start = "INSERT INTO [Dog] ([Name], [DOB], [ClientID], [BreedName], [Status]";
                String End = String.Format("VALUES('{0}', '{1}', '{2}', '{3}', '{4}'", DogName, (DOB).ToString("yyyy-MM-dd"), GetClientIDFromString(ClientName), BreedName, 1); // find way of getting client id
                if (ImgSourceHasChanged(Img_Source)) { Start += ", [Image]"; End += (", '" + Img_Source + "'"); }
                if (AdditionalInfo != "") { Start += ", [AdditionalInfo]"; End += (", '" + AdditionalInfo + "'"); }
                QueryDatabase(Start + ") " + End + ");");
                MessageBox.Show("Client registered successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        /// <summary>
        /// Appends to end of a newline in string.
        /// </summary>
        /// <param name="ErrorList"></param>
        /// <param name="Error"></param>
        private void AddToErrors(ref String ErrorList, String Error)
        {
            Error += Environment.NewLine;
            ErrorList += Error;
        }
        /// <summary>
        /// Checks if supplied string consists of letter characters
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        private bool CheckCharsAreLetters(String test)
        {
            foreach (Char c in test)
            {
                if (!Char.IsLetter(c)) { return false; }
            }
            return true;
        }
        // fix img source
        public bool ImgSourceHasChanged(String Img_Source) // fix this later with a bool type value??
        {
            return Img_Source != "";
        }
        #endregion
    }
}

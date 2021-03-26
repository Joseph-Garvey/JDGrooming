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
        /// <summary>
        /// Returns Client Details as list
        /// </summary>
        /// <param name="ClientString"></param>
        /// <returns></returns>
        public string[] GetClientString(String ClientString)
        {
            String[] client = new String[9];
            try
            {
                int clientid = GetClientIDFromString(ClientString);
                SqlDataReader reader = ReadDatabase("SELECT Forename, Surname, FirstLine, SecondLine, Town, Postcode, Email, HomePhone, Mobile FROM [Client] WHERE ID = " + clientid + " ;");
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0)) client[0] = (reader.GetString(0));
                    if (!reader.IsDBNull(1)) client[1] = (reader.GetString(1));
                    if (!reader.IsDBNull(2)) client[2] = (reader.GetString(2));
                    if (!reader.IsDBNull(3)) client[3] = (reader.GetString(3));
                    if (!reader.IsDBNull(4)) client[4] = (reader.GetString(4));
                    if (!reader.IsDBNull(5)) client[5] = (reader.GetString(5));
                    if (!reader.IsDBNull(6)) client[6] = (reader.GetString(6));
                    if (!reader.IsDBNull(7)) client[7] = (reader.GetString(7));
                    if (!reader.IsDBNull(8)) client[8] = (reader.GetString(8));
                }
                reader.Close();
            }
            catch { db.Rdr.Close(); }
            return client;
        }
        public List<String> GetClientStringShort()
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
        public DataView FillClientDogTable(int id)
        {
            return CreateDataView("SELECT [Dog].ID, [Dog].[Name], [Breed].[Name] AS Breed, " +
        "DOB FROM [Dog] " +
        "INNER JOIN [Client] ON [Dog].ClientID = [Client].Id " +
        "INNER JOIN [Breed] ON [Dog].BreedName = [Breed].Name " +
        "WHERE [Client].ID = " + id.ToString() + " " +
        "AND Status = 1 ;");
        }
        public DataView FillClientTable()
        {
            return CreateDataView("SELECT [ID], [Surname] + ' ' + [Forename] AS Name, FirstLine + ', ' + SecondLine AS Address, Town, Postcode, Email, HomePhone, Mobile FROM [Client]");
        }
        public DataView FillDogTable()
        {
            return CreateDataView("SELECT [Dog].ID, [Dog].[Name], [Breed].[Name] AS Breed, Forename + ' ' + Surname AS Client_Name, " +
                    "DOB, [Status] FROM [Dog] " +
                    "INNER JOIN [Client] ON [Dog].ClientID = [Client].Id " +
                    "INNER JOIN [Breed] ON [Dog].BreedName = [Breed].Name;");
        }
        public DataView CreateDataView(String CommandText)
        {
            using (SqlCommand command = db.Conn.CreateCommand())
            {
                command.CommandText = CommandText;
                db.Cmd = command;
                SqlDataAdapter sda = new SqlDataAdapter(db.Cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt.DefaultView;
            }
        }
        public void UpdateClient(string id, string forename, string surname, string firstline, string secondline, string town, string postcode, string email, string mobile, string homephone)
        {
            String Start = String.Format("UPDATE [Client] SET [Forename] = '{0}', [Surname] = '{1}', [FirstLine] = '{2}', [Town] = '{3}', [Postcode] = '{4}'",
                forename, surname, firstline, town, postcode);
            String SQL = Start;
            if (secondline != "") { SQL += ", [SecondLine] = '" + secondline + "'"; }
            if (email != "") { SQL += ", [Email] = '" + email + "'"; }
            if (mobile != "") { SQL += ", [Mobile] = '" + mobile + "'"; }
            if (homephone!= "") { SQL += ", [HomePhone] = '" + homephone + "'"; }
            SQL += " WHERE [ID] = " + id + " ;";
            QueryDatabase(SQL);
            // secondline and contact are nullable
        }
        /// <summary>
        /// only update the changed field in future + nullable values
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
        public List<Staff> GetStaff()
        {
            List<Staff> results = new List<Staff> { };
            try
            {
                SqlDataReader reader = ReadDatabase("SELECT * FROM [Staff]");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(3);
                    results.Add(new Staff(id, name));
                }
                reader.Close();
            }
            catch { db.Rdr.Close(); }
            return results;
        }
        /// <summary>
        /// Checks if this is the dog's first time with JDGrooming
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckFirstAppointment(int id)
        {
            bool first = true;
            // put in try catch??
            try
            {
                SqlDataReader reader = ReadDatabase("SELECT COUNT([DogID]) FROM [Appointment] WHERE [DogID] = " + id + " ;");
                while((reader.Read())) if(reader.GetInt32(0) > 0) { first = false; }
            }
            catch { db.Rdr.Close(); }
            return first;
        }
        public ObservableCollection<Service> GetServices()
        {
            ObservableCollection<Service> services = new ObservableCollection<Service> { };
            try
            {
                SqlDataReader reader = ReadDatabase("SELECT * FROM [Service];");
                while (reader.Read())
                {
                    Service s = new Service(reader.GetString(0), reader.GetTimeSpan(1), (reader.GetSqlMoney(2)).ToDouble());
                    services.Add(s);
                }
                reader.Close();
            }
            catch { db.Rdr.Close(); }
            return services;
        }

        public void RegisterClient(String Forename, String Surname, String FirstLine, String SecondLine, String Postcode, String Town, String Email, String Mobile, String HomePhone)
        {
            // add letter validation for names etc
            // try something similar to last year's system but more efficient.
            // this time get something that works, then improve on it.
            // also auto format the strings in future.
            const String failedNameFormat = "\u2022 Names must consist of letters.";
            const String failedForenameLength = "\u2022 Forename must be less than 35 characters.";
            const String failedSurnameLength = "\u2022 Surname must be less than 50 characters.";
            const String failedAddressLength = "\u2022 Address line must be less than 35 characters.";
            const String failedTownLength = "\u2022 Town/City name must be less than 64 characters.";
            const String failedPostCode = "\u2022 Postcode must be of valid format and length eg. XX0 XX0.";
            const String failedEmail = "\u2022 Email must be of valid format and length (<255 chars) eg. JoeBloggs@mail.com";
            const String failedPhone = "\u2022 PhoneNo must be of valid format and length (<32 chars)";
            const String failedContactDetail = "\u2022 Customer must provide at least one method of contact.";
            String Errors = "";
            if ((Email == "")
                && (HomePhone == "")
                && (Mobile == "")
                ) { AddToErrors(ref Errors, failedContactDetail); }
            if (Forename.Length > 35) AddToErrors(ref Errors, failedForenameLength);
            if (Surname.Length > 50) AddToErrors(ref Errors, failedSurnameLength);
            if (!CheckCharsAreLetters(Forename) || !CheckCharsAreLetters(Surname)) AddToErrors(ref Errors, failedNameFormat);
            if ((FirstLine.Length > 35) || (SecondLine.Length > 35)) AddToErrors(ref Errors, failedAddressLength);
            if (Town.Length > 64) AddToErrors(ref Errors, failedTownLength);
            if ((Postcode.Length > 8) || (!Verification.VerifyPostcode(Postcode))) AddToErrors(ref Errors, failedPostCode);
            bool ContactDetailsPresent = false;
            if (Email != "")
            {
                ContactDetailsPresent = true;
                if ((Email.Length > 255) || (!Verification.VerifyEmail(Email))) AddToErrors(ref Errors, failedEmail);
            }
            if (Mobile != "")
            {
                ContactDetailsPresent = true;
                if ((Mobile.Length > 32) || (!Verification.VerifyPhoneNumber(Mobile))) AddToErrors(ref Errors, failedPhone);
            }
            if (HomePhone != "")
            {
                ContactDetailsPresent = true;
                if ((HomePhone.Length > 32) || (!Verification.VerifyPhoneNumber(HomePhone))) AddToErrors(ref Errors, failedPhone);
            }
            if (!ContactDetailsPresent) { AddToErrors(ref Errors, failedContactDetail); }
            if (Errors != "") { MessageBox.Show(Errors, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else
            {
                String Start = "INSERT INTO [Client] ([Surname], [Forename], [FirstLine], [Town], [Postcode]";
                String End = String.Format("VALUES('{0}', '{1}', '{2}', '{3}', '{4}'", Surname, Forename, FirstLine, Town, Postcode);
                if (Email != "") { Start += ", [Email]"; End += ", '" + Email + "'"; }
                if (HomePhone != "") { Start += ", [HomePhone]"; End += ", '" + HomePhone + "'"; } // fix
                if (Mobile != "") { Start += ", [Mobile]"; End += ", '" + Mobile + "'"; } // fix
                if (SecondLine != "") { Start += ", [SecondLine]"; End += ", '" + SecondLine + "'"; }
                ((App)Application.Current).query.QueryDatabase(Start + ") " + End + ");");
                MessageBox.Show("Client registered successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        /// <summary>
        /// Registers a supplied dog to the database and verifies in info fits criteria
        /// </summary>
        /// <param name="DogName">Name of dog</param>
        /// <param name="ClientName">Name of owner</param>
        /// <param name="BreedName">The dog's breed</param>
        /// <param name="AdditionalInfo">Staff notes on dog</param>
        /// <param name="Img_Source">Name of dog's image file.</param>
        /// <param name="DOB">Dog Date of Birth</param>
        public void RegisterDog(String DogName, String ClientName, String BreedName, String AdditionalInfo, String Img_Source, DateTime DOB)
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

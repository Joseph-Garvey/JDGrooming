using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace JDGrooming.Classes.Database_Management
{
    public class Database
    {
        // Variables //
        SqlCommand cmd;
        SqlConnection conn;
        SqlDataReader rdr;

        // Properties //
        public SqlCommand Cmd { get => cmd; set => cmd = value; }
        public SqlConnection Conn { get => conn; set => conn = value; }
        public SqlDataReader Rdr { get => rdr; set => rdr = value; }

        // Methods //
        public bool Connect()
        {
            SqlConnectionStringBuilder scStrBuild = new SqlConnectionStringBuilder
            {
                DataSource = "(LocalDB)\\MSSQLLocalDB",
                AttachDBFilename = "|DataDirectory|JDDatabase.mdf",
                IntegratedSecurity = true
            };
            conn = new SqlConnection(scStrBuild.ToString());
            try { conn.Open(); }
            catch (SqlException ex) { System.Windows.MessageBox.Show(ex.Message); }
            if (conn.State == System.Data.ConnectionState.Open) return true;
            else return false;
        }
    }
}

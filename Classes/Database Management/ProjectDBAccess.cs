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
        private SqlDataReader QueryDatabase(String Query)
        {
            using (SqlCommand command = db.Conn.CreateCommand())
            {
                command.CommandText = Query;
                db.Cmd = command;
                return db.Cmd.ExecuteReader();
            }
        }
        public void AddClient(Client client)
        {
            QueryDatabase("");
        }
        #endregion
    }
}

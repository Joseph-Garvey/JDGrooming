using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDGrooming.Classes
{
    public class Client
    {
        #region Constructors
        // check these constructors work
        public Client(string surname, string forename, Address address)
        {
            Surname = surname ?? throw new ArgumentNullException(nameof(surname));
            Forename = forename ?? throw new ArgumentNullException(nameof(forename));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public Client(string surname, string forename, Address address, string email) : this(surname, forename, address)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public Client(string surname, string forename, Address address, int homePhone) : this(surname, forename, address)
        {
            HomePhone = homePhone;
        }

        public Client(string surname, string forename, int mobile, Address address) : this(surname, forename, address)
        {
            Mobile = mobile;
        }

        public Client(string surname, string forename, Address address, string email, int homePhone) : this(surname, forename, address, email)
        {
            HomePhone = homePhone;
        }

        public Client(string surname, string forename, int mobile, Address address, string email) : this(surname, forename, mobile, address)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public Client(string surname, string forename, string email, Address address, int homePhone, int mobile) : this(surname, forename, address, email, homePhone)
        {
            Mobile = mobile;
        }
        #endregion

        public int ID { get; set; }
        public String Surname { get; set; }
        public String Forename { get; set; }
        public String Email { get; set; }
        public Address Address { get; set; }
        public int HomePhone { get; set; }
        public int Mobile { get; set; }

        public String GetInsertSQL()
        {
            String Start = "INSERT INTO [Client] ([Surname], [Forename), [FirstLine], [Town], [Postcode]";
            String End = String.Format("VALUES('{0}', '{1}', '{2}', '{3}', '{4}'", Surname, Forename, Address.FirstLine, Address.Town, Address.PostCode);
            if (Email != null) { Start += ", [Email]"; End += ", '" + Email + "'"; }
            if (HomePhone != null) { Start += ", [HomePhone]"; End += ", '" + HomePhone + "'"; } // fix
            if (Mobile != null) { Start += ", [Mobile]"; End += ", '" + Mobile + "'"; } // fix
            if(Address.SecondLine != null) { Start += ", [SecondLine]"; End += ", '" + Address.SecondLine + "'"; }
            return Start + ") " + End + ");";
        }
    }
}

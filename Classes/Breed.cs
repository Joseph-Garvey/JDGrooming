using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDGrooming.Classes
{
    public class Breed
    {
        //tmp
        public Breed(string name)
        {
            Name = name;
        }

        public int ID { get; set; }
        public String Name { get; set; }
        public String Info { get; set; }
        public Uri DefaultImage { get; set; }

        public String GetInsertSQL()
        {
            return String.Format("INSERT INTO [Breed] ([Name]) VALUES('{0}');", this.Name);
            //return String.Format("INSERT INTO [Breed] ([Name], [Info], [DefaultImage]) VALUES('{0}', '{1}', '{2}');", this.Name, this.Info, this.DefaultImage);
        }
    }
}

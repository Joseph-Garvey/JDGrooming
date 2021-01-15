using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDGrooming.Classes
{
    public class Address
    {
        public Address(string firstLine, string postCode, string town)
        {
            FirstLine = firstLine ?? throw new ArgumentNullException(nameof(firstLine));
            PostCode = postCode ?? throw new ArgumentNullException(nameof(postCode));
            Town = town ?? throw new ArgumentNullException(nameof(town));
        }

        public Address(String firstLine, String postCode, String town, String secondLine):this(firstLine, postCode, town)
        {
            SecondLine = secondLine ?? throw new ArgumentNullException(nameof(secondLine));
        }

        public String FirstLine { get; set; }
        public String SecondLine { get; set; }
        public String PostCode { get; set; }
        public String Town { get; set; }
    }
}

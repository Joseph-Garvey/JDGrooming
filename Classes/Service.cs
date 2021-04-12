using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace JDGrooming.Classes
{
    public class Service
    {
        public Service(string name, TimeSpan duration, double _price)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Duration = duration;
            price = _price;
        }

        public Service()
        {

        }
        public String Name { get; set; }
        public TimeSpan Duration { get; set; }
        public double price;
        public String Price
        {
            get => price.ToString("C", CultureInfo.CurrentCulture);
        }
    }
}

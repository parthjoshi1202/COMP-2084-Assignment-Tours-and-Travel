using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToursandTravel.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int PackageId { get; set; }

        public string ClientId { get; set; }

        public string Duration { get; set; }

        public DateTime Date { get; set; }

        //parent object references 
        public Package Package { get; set; }
    }
}

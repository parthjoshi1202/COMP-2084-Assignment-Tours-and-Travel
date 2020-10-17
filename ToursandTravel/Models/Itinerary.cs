using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToursandTravel.Models
{
    public class Itinerary
    {
        public int Id { get; set; }

        public string ClientId { get; set; }

        public DateTime Date { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string FinalAmt { get; set; }
      
        /// <summary>
        /// child object referecne 
        /// </summary>
        public List<FinalBill> FinalBills { get; set; }
    }
}

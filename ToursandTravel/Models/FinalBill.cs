using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToursandTravel.Models
{
    public class FinalBill
    {
        public int Id { get; set; }

        public int PackageId { get; set; }

        public int ItineraryId { get; set; }

        public string Duration { get; set; }

        public double Amount { get; set; }

        //parent object references
        public Itinerary Itinerary { get; set; }

        public Package Package { get; set; }
    }
}

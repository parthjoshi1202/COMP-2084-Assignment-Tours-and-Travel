using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToursandTravel.Models
{
    public class Package
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public int TypeId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:c}")] 
        [Range(0.01, 99999)]
        public double Price { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Contact Details")]
        public string ContactDetails { get; set; }

        public string Picture { get; set; }

        /// <summary>
        /// this is a parent object reference
        /// </summary>
        public PackageType PackageType { get; set; }

        /// <summary>
        /// These are child object references 
        /// </summary>
        public List<FinalBill> FinalBills { get; set; }
        public List<ShoppingCart> ShoppingCarts { get; set; }


    }
}

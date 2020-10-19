using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ToursandTravel.Models
{
    public class PackageType
    {
            public int Id { get; set; }

            [Required]
            public string Name { get; set; }

            /// <summary>
            /// This is a child object reference , Package class 
            /// </summary>
            public List<Package> Packages { get; set; }

        
    }
}

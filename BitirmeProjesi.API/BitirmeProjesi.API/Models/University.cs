using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitirmeProjesi.API.Models
{
    public class University
    {
        public int UniversityID { get; set; }
        public string UniversityName { get; set; }
        public string UniversityAdress { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}

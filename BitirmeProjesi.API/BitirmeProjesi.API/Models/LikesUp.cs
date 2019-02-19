using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitirmeProjesi.API.Models
{
    public class LikesUp
    {
        public int LikesUpID { get; set; }
        public int StudentID { get; set; }
        public int ProjectID { get; set; }
        public Project Project { get; set; }

    }
}

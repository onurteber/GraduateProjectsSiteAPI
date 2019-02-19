using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitirmeProjesi.API.Dtos
{
    public class ProjectForScoreDTO
    {
        public int ProjectID { get; set; }
        public int ProjectScore { get; set; }

        public int StudentId { get; set; }
    }
}

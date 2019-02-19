using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitirmeProjesi.API.Dtos
{
    public class MyFavroiteProjectDTO
    {
        public string ProjectName { get; set; }
        public string StudentNameandSurname { get; set; }
        public string ProjectDescription { get; set; }
        public int ProjectScore { get; set; }
        public string AcademicalPersonalName { get; set; }
        public int StudentID { get; set; }
        public int ProjectID { get; set; }
    }
}

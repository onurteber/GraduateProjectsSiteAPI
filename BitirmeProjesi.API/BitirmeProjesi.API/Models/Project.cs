using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitirmeProjesi.API.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int StudentID { get; set; }
        public string AcademicalPersonalName { get; set; }
        public int DepartmentID { get; set; }
        public int UniversityID { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public int ProjectScore { get; set; }
        public string ProjectUrl { get; set; }
        public int Status { get; set; }
    }
}

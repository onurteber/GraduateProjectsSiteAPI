using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitirmeProjesi.API.Dtos
{
    public class UserForRegisterDTO
    {
        public string StudentName { get; set; }
        public string StudentLastName { get; set; }
        public string Email { get; set; }
        public int UniversityID { get; set; }
        public int DepartmentID { get; set; }
        public string Password { get; set; }
        public bool sozlesme { get; set; }
    }
}

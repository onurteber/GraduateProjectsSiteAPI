using BitirmeProjesi.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitirmeProjesi.API.Data
{
    public interface IStudentRepository
    {
        List<Student> GetStudent();
        Student GetStudentByProjectId(int projectid);
        Student GetStudentByID(int id);

        List<Project> GetStudentProjects(int StudentID);
    }
}

using BitirmeProjesi.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitirmeProjesi.API.Data
{
    
    public class StudentRepository : IStudentRepository
    {
        private IStudentRepository _studentRepository;
        private DataContext _context;

        public StudentRepository(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public Student GetStudentByProjectId(int projectid)
        {
            var student = _context.Students.Include(c => c.Project)
                .FirstOrDefault(c => c.Project.ProjectID == projectid);
            return student;

        }
        public List<Project> GetStudentProjects(int StudentID)
        {
            var model = _context.Projects.Where(s => s.StudentID == StudentID).Where(p => p.Status > 0).ToList();
            return model;
        }




        public List<Student> GetStudent()
        {
            var student = _context.Students.ToList();
            return student;
        }



        public Student GetStudentByID(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentID == id);
            return student;
        }
    }
}

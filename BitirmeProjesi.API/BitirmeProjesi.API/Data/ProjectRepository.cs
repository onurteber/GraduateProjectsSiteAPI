using BitirmeProjesi.API.Dtos;
using BitirmeProjesi.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitirmeProjesi.API.Data
{
    public class ProjectRepository:IProjectRepository
    {
        private DataContext _context;
        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }


        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }



        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public List<Project> GetProjects()
        {
            var model = _context.Projects.Where(p => p.Status > 0).ToList();
            return model;
        }

        public List<ProjectForListDto> GetProjectsInDepartment(int id)
        {
            var data = new List<ProjectForListDto>();
            foreach (var project in _context.Projects.Where(d => d.DepartmentID == id).Where(p => p.Status > 0).ToList())
            {
                var proje = new ProjectForListDto();
                proje.ProjectScore = project.ProjectScore;
                var department = _context.Departments.FirstOrDefault(d => d.DepartmentID == project.DepartmentID);
                proje.DepartmentName = department.DepartmentName;
                var student = _context.Students.FirstOrDefault(f => f.StudentID == project.StudentID);
                proje.StudentNameandSurname = student.StudentName + " " + student.StudentLastName;
                proje.ProjectDescription = project.ProjectDescription;
                proje.ProjectName = project.ProjectName;
                proje.StudentID = student.StudentID;
                proje.ProjectID = project.ProjectID;
                proje.AcademicalPersonalName = project.AcademicalPersonalName;
                data.Add(proje);
            }
            return data;
        }

        public List<Project> GetStudentProjects(int StudentID)
        {
            var model = _context.Projects.Where(s => s.StudentID == StudentID).Where(p => p.Status > 0).ToList();
            return model;
        }

        public List<ProjectForListDto> GetProjectsScore()
        {
            var data = new List<ProjectForListDto>();
            foreach (var project in _context.Projects.Where(p => p.Status > 0).OrderByDescending(m => m.ProjectScore).Take(10))
            {
                var proje = new ProjectForListDto();
                proje.ProjectScore = project.ProjectScore;
                var student = _context.Students.FirstOrDefault(f => f.StudentID == project.StudentID);
                proje.StudentNameandSurname = student.StudentName + " " + student.StudentLastName;
                proje.ProjectDescription = project.ProjectDescription;
                proje.ProjectName = project.ProjectName;
                proje.StudentID = student.StudentID;
                proje.ProjectID = project.ProjectID;
                proje.AcademicalPersonalName = project.AcademicalPersonalName;
                data.Add(proje);
            }
            return data;
        }

        public List<ProjectForListDto> GetProjectsTop20Score()
        {
            var data = new List<ProjectForListDto>();
            foreach (var project in _context.Projects.Where(p => p.Status > 0).OrderByDescending(m => m.ProjectScore))
            {
                var proje = new ProjectForListDto();
                proje.ProjectScore = project.ProjectScore;
                var student = _context.Students.FirstOrDefault(f => f.StudentID == project.StudentID);
                proje.StudentNameandSurname = student.StudentName + " " + student.StudentLastName;
                proje.ProjectDescription = project.ProjectDescription;
                proje.ProjectName = project.ProjectName;
                proje.StudentID = student.StudentID;
                proje.ProjectID = project.ProjectID;
                proje.AcademicalPersonalName = project.AcademicalPersonalName;
                data.Add(proje);
            }
            return data;
        }

        public List<Project> GetProjectById(int id)
        {
            var model = _context.Projects.Where(p => p.ProjectID == id).Where(p => p.Status > 0).ToList();
            return model;
        }

        public List<Project> GetProjectStudent(int ProjectID)
        {
            var student = _context.Projects.Where(s => s.ProjectID == ProjectID).ToList();
            return student;
        }

        public List<LikesUp> GetLikesUp(int ProjectID, int StudentID)
        {
            var model = _context.LikesUp.Where(p => p.ProjectID == ProjectID && p.StudentID == StudentID).ToList();
            return model;
        }

        public LikesDown GetLikeDown(int ProjectID, int StudenID)
        {
            var model = _context.LikesDown.FirstOrDefault(p => p.StudentID == StudenID && p.ProjectID == ProjectID);
            return model;
        }

        public LikesUp GetLikeUp(int ProjectID, int StudentID)
        {
            var model = _context.LikesUp.FirstOrDefault(p => p.ProjectID == ProjectID && p.StudentID == StudentID);
            return model;
        }




        public List<LikesDown> GetLikesDown(int ProjectID, int StudentID)
        {
            var model = _context.LikesDown.Where(p => p.ProjectID == ProjectID && p.StudentID == StudentID).ToList();
            return model;
        }
        public Project GetProjectByID(int projectId)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectID == projectId);
            return project;
        }
        public List<Project> GetProjectsByStudentID(int studentid)
        {
            var projects = _context.Projects.Where(p => p.StudentID == studentid).Where(p => p.Status > 0).ToList();
            return projects;
        }

        public List<MyFavroiteProjectDTO> GetFavoriteProjectsByStudentID(int studentid)
        {
            var data = new List<MyFavroiteProjectDTO>();
            foreach (var project in _context.LikesUp.Include(p=>p.Project).Where(p => p.StudentID == studentid && p.Project.Status>0))
            {
                var proje = new MyFavroiteProjectDTO();
                var student = _context.Students.FirstOrDefault(f => f.StudentID == project.StudentID);
                proje.StudentNameandSurname = student.StudentName + " " + student.StudentLastName;
                proje.ProjectName = project.Project.ProjectName;
                proje.StudentID = project.StudentID;
                proje.ProjectID = project.ProjectID;
                proje.ProjectScore = project.Project.ProjectScore;
                proje.ProjectDescription = project.Project.ProjectDescription;
                proje.AcademicalPersonalName = project.Project.AcademicalPersonalName;
                data.Add(proje);
            }
            return data;
        }


        public Student GetStudentByProjectId(int projectid)
        {
            var student = _context.Students.Include(c => c.Project)
                .FirstOrDefault(c => c.Project.ProjectID == projectid);
            return student;

        }

        public Student GetStudentByID(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentID == id);
            return student;
        }

        public University GetUniversityByID(int id)
        {
            var university = _context.Universities.Where(u => u.UniversityID == id).FirstOrDefault();
            return university;
        }
    }
}

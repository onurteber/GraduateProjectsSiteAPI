using BitirmeProjesi.API.Dtos;
using BitirmeProjesi.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitirmeProjesi.API.Data
{
    public interface IProjectRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        bool SaveAll();
        List<Project> GetProjects();
        List<ProjectForListDto> GetProjectsScore();
        List<ProjectForListDto> GetProjectsTop20Score();
        List<ProjectForListDto> GetProjectsInDepartment(int id);
        List<Project> GetProjectById(int id);
        List<Project> GetProjectStudent(int ProjectID);
        List<Project> GetStudentProjects(int StudentID);
        Project GetProjectByID(int projectId);
        List<LikesDown> GetLikesDown(int ProjectID, int StudentID);
        List<LikesUp> GetLikesUp(int ProjectID, int StudentID);
        LikesUp GetLikeUp(int ProjectID, int StudentID);
        LikesDown GetLikeDown(int ProjectID, int StudenID);
        List<Project> GetProjectsByStudentID(int studentid);
        List<MyFavroiteProjectDTO> GetFavoriteProjectsByStudentID(int studentid);
        University GetUniversityByID(int id);
        Student GetStudentByID(int id);
    }
}

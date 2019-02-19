using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BitirmeProjesi.API.Data;
using BitirmeProjesi.API.Dtos;
using BitirmeProjesi.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BitirmeProjesi.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Projects")]
    public class ProjectsController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IProjectRepository _appRepository;
        private IMapper _mapper;

        public ProjectsController(IProjectRepository projectRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _appRepository = projectRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public ActionResult GetProject()
        {
            var projects = _appRepository.GetProjects();
            var projectreturn = _mapper.Map<List<ProjectForListDto>>(projects);
            return Ok(projectreturn);
        }

        [HttpGet]
        [Route("topfive")]
        public ActionResult TopFiveProject()
        {
            var projectTopFive = _appRepository.GetProjectsScore();
            return Ok(projectTopFive);
        }
        

        [HttpGet]
        [Route("toptwenty")]
        public ActionResult TopTwentyProjects()
        {
            var projectTopTwenty = _appRepository.GetProjectsTop20Score();
            return Ok(projectTopTwenty);
        }

        [HttpPost]
        [Route("add")]
        public ActionResult ProjectAdd([FromBody]Project project)
        {
            var student = _appRepository.GetStudentByID(project.StudentID);
            project.UniversityID = student.UniversityID;
            project.Status = 1;
            project.ProjectStartDate=DateTime.Now;
            _appRepository.Add(project);
            _appRepository.SaveAll();
            return Ok(project);
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult ProjectDelete(int projectid)
        {
            var project = _appRepository.GetProjectByID(projectid);
            project.Status = -1;
            _appRepository.SaveAll();
            return Ok();
        }

        [HttpGet]
        [Route("getmyprojects")]
        public ActionResult GetProjectsByStudentID(int studentid)
        {
            var projects = _appRepository.GetProjectsByStudentID(studentid);
            return Ok(projects);
        }

        [HttpGet]
        [Route("getmyfavoriteprojects")]
        public ActionResult GetFavoriteProjectsByStudentID(int studentid)
        {
            var projects = _appRepository.GetFavoriteProjectsByStudentID(studentid);
            return Ok(projects);
        }


        [HttpGet]
        [Route("projectdetail")]
        public ActionResult GetProjectById(int id)
        {
            var model = _appRepository.GetProjectById(id);
            return Ok(model);
        }

        [HttpPost]
        [Route("arttir")]
        public ActionResult ScoreArttir(int projectid,int studentid)
        {
            var project = _appRepository.GetProjectByID(projectid);
            
            if (project == null)
            {
                return BadRequest("Böyle bir proje bulunmamaktadır.");
            }
            if (project.StudentID == studentid)
            {
                return BadRequest("Kullanıcı kendi projesini oylayamaz!");
            }
            int begenmeDurumu = _appRepository.GetLikesUp(projectid, studentid).Count();
            if (begenmeDurumu == 1)
            {
                project.ProjectScore -= 1;
                _appRepository.Delete(_appRepository.GetLikeUp(projectid, studentid));
                if (_appRepository.SaveAll())
                {
                    return Ok();
                }
                
                return Unauthorized();
            }
            if (begenmeDurumu != 0)
            {
                return Unauthorized();
            }

            

            LikesUp likes = new LikesUp();
            likes.StudentID = studentid;
            likes.ProjectID = projectid;
            _appRepository.Add(likes);

            var likeDownId = _appRepository.GetLikesDown(projectid, studentid).ToList();
            if (likeDownId.Count() != 0)
            {
                project.ProjectScore += 1;

                _appRepository.Delete(_appRepository.GetLikeDown(projectid, studentid));
                //_appRepository.Delete(_appRepository.GetLikesUp(projectid, studentid));
            }

            if (studentid == 0)
            {
                return Unauthorized();
            }

            var currentUserId = studentid;

            if (currentUserId == 0)
            {
                return Unauthorized();
            }
            
            project.ProjectScore+=1;

            if (_appRepository.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Hata");
        }

        [HttpPost]
        [Route("azalt")]
        public ActionResult ScoreAzalt(int projectid,int studentid)
        {
            var project = _appRepository.GetProjectByID(projectid);

            if (project == null)
            {
                return BadRequest("Böyle bir proje bulunmamaktadır.");
            }
            if (project.StudentID == studentid)
            {
                return BadRequest("Kullanıcı kendi projesini oylayamaz!"); 
            }

            int begenmemeDurumu = _appRepository.GetLikesDown(projectid, studentid).Count();
            if (begenmemeDurumu == 1)
            {
                project.ProjectScore += 1;
                _appRepository.Delete(_appRepository.GetLikeDown(projectid, studentid));
                if (_appRepository.SaveAll())
                {
                    return Ok();
                }

                return BadRequest("Kayıt islemi gerceklestirilemedi!");
            }
            if (begenmemeDurumu != 0)
            {
                return BadRequest("Zaten islenmis bir islem. Yeniden yapilamaz!!!");
            }

            

            LikesDown likes = new LikesDown();
            likes.StudentID = studentid;
            likes.ProjectID = projectid;
            _appRepository.Add(likes);

            var likeDownId = _appRepository.GetLikesUp(projectid, studentid).ToList();
            if (likeDownId.Count() != 0)
            {
                project.ProjectScore -= 1;

                _appRepository.Delete(_appRepository.GetLikeUp(projectid, studentid));
            }

            if (studentid == 0)
            {
                return Unauthorized();
            }
            var currentUserId = studentid;
            
            if (currentUserId == 0)
            {
                return Unauthorized();
            }
            
            project.ProjectScore -= 1;

            if (project.ProjectScore < 0)
            {
                project.ProjectScore = 0;

            }

            if (_appRepository.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Hata");
        }
        
        [HttpGet]
        [Route("studentprojects")]
        public ActionResult GetProjects(int studentid)
        {
            var student = _appRepository.GetStudentProjects(studentid);
            return Ok(student);
        }

        [HttpGet]
        [Route("GetProjectsOfDepartment")]
        public ActionResult GetProjectsOfDepartment(int id)
        {
            var projects = _appRepository.GetProjectsInDepartment(id).ToList();
            return Ok(projects);
        }

    }
}
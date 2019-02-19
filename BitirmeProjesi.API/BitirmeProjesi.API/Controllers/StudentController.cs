using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitirmeProjesi.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BitirmeProjesi.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Student")]
    public class StudentController : Controller
    {
        private IStudentRepository _appRepository;

        public StudentController(IStudentRepository appRepository)
        {
            _appRepository = appRepository;
        }

        [HttpGet]
        public ActionResult GetStudent()
        {
            var student = _appRepository.GetStudent();
            return Ok(student);
        }

        [HttpGet]
        [Route("studentprojects")]
        public ActionResult GetProjects(int studentid)
        {
            var student = _appRepository.GetStudentProjects(studentid);
            return Ok(student);
        }

    }
}
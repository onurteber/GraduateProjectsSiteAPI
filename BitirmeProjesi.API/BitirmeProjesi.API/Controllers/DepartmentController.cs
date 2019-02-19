using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BitirmeProjesi.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BitirmeProjesi.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Department")]
    public class DepartmentController : Controller
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;

        public DepartmentController(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("departments")]
        public ActionResult GetDepartments()
        {
            var departments = _appRepository.GetDepartments().ToList();
            return Ok(departments);
        }

        
    }
}
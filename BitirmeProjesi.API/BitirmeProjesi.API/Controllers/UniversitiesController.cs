using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BitirmeProjesi.API.Data;
using BitirmeProjesi.API.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BitirmeProjesi.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Universities")]
    public class UniversitiesController : Controller
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;

        public UniversitiesController(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        public ActionResult GetUniversities()
        {
            var universities = _appRepository.GetUniversity();
            var universitiesReturn = _mapper.Map<List<UniversityForListDto>>(universities);
            return Ok(universitiesReturn);
        }

        [HttpGet]
        [Route("university")]
        public ActionResult GetUniversity()
        {
            var university = _appRepository.GetUniversity();
            return Ok(university);
        }

        //[Route("universitybyid")]
        //public ActionResult GetUniversityByID(int id)
        //{
        //    var university = _appRepository.GetUniversityByID(id);
        //    return Ok(university);
        //}
    }
}
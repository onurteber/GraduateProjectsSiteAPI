using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BitirmeProjesi.API.Data;
using BitirmeProjesi.API.Dtos;
using BitirmeProjesi.API.Models;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BitirmeProjesi.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private IAuthRepository _authRepository;
        private IConfiguration _configuration;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDTO userForRegisterDto)
        {
            if (userForRegisterDto.sozlesme == false)
            {
                return BadRequest("Sözleşme Kabul Edilmedi.");
            }
            if (await _authRepository.UserExist(userForRegisterDto.Email))
            {
                ModelState.AddModelError("Email", "Email already exist");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userToCreate = new Models.Student
            {
                Email = userForRegisterDto.Email,
                DepartmentID = userForRegisterDto.DepartmentID,
                StudentName = userForRegisterDto.StudentName,
                StudentLastName = userForRegisterDto.StudentLastName,
                UniversityID = userForRegisterDto.UniversityID                
            };
            var createdUser =  _authRepository.Register(userToCreate, userForRegisterDto.Password);
            return StatusCode(201);
        }
       

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]UserForLoginDTO userForLoginDto)
        {
            var student = await _authRepository.Login(userForLoginDto.Email, userForLoginDto.Password);
            if (student == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, student.StudentID.ToString()),
                    new Claim(ClaimTypes.Name, student.StudentName)
                }),
                Expires = DateTime.Now.AddDays(1), // Token ne kadar Geçerli
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            HttpContext.Session.SetInt32("studentid",student.StudentID);
            var token = tokenHandler.CreateToken((tokenDescriptor));
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(tokenString);
        }
    }
}
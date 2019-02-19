using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitirmeProjesi.API.Models;

namespace BitirmeProjesi.API.Data
{
    public interface IAuthRepository
    {
        Task<Student> Register(Student user, string password);
        Task<Student> Login(string userName, string password);
        Task<bool> UserExist(string userName);
    }
}

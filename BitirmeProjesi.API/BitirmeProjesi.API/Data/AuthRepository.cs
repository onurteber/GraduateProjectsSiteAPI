using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitirmeProjesi.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BitirmeProjesi.API.Data
{
    public class AuthRepository:IAuthRepository
    {
        private DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Student> Register(Student user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Students.AddAsync(user);


            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash((System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }

        public async Task<Student> Login(string mail, string password)
        {
            var user = await _context.Students.FirstOrDefaultAsync(x => x.Email == mail);
            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash((System.Text.Encoding.UTF8.GetBytes(password)));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public async Task<bool> UserExist(string email)
        {
            if (await _context.Students.AnyAsync(x => x.Email == email))
            {
                return true;
            }

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitirmeProjesi.API.Dtos;
using BitirmeProjesi.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BitirmeProjesi.API.Data
{
    public class AppRepository : IAppRepository
    {
        private DataContext _context;

        public AppRepository(DataContext context)
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

        

       
        public List<Department> GetDepartments()
        {
            var model = _context.Departments.ToList();
            return model;
        }


        public University GetUniversityByID(int id)
        {
            var university = _context.Universities.Where(u => u.UniversityID == id).FirstOrDefault();
            return university;
        }


       

        public List<University> GetUniversity()
        {
            var model = _context.Universities.ToList();
            return model;
        }
    }
}

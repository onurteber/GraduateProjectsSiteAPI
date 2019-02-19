using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitirmeProjesi.API.Dtos;
using BitirmeProjesi.API.Models;

namespace BitirmeProjesi.API.Data
{
    public interface IAppRepository
    {
        void Add<T>(T entity) where T:class ;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        bool SaveAll();
        List<University> GetUniversity();
        List<Department> GetDepartments();
    }
}

using BLL.VotingSystem.Interfaces;
using DAL.VotingSystem.Context;
using DAL.VotingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Repository
{
    public class GenericRepository<T>:IGenericRepository<T>where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);

        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);

        }


        public IEnumerable<T> GetAll()
   => _context.Set<T>().ToList();

        public T GetById(string? Id)  // test string
        {
            return _context.Set<T>().Find(Id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}

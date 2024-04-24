using DAL.VotingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        public IEnumerable<T> GetAll();
        public T GetById(string? Id); //test string 

        public   void Add(T entity);

        public void Delete(T entity);

        public void Update(T entity);
    }
}
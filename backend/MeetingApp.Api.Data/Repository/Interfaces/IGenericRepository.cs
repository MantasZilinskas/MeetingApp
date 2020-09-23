using MeetingApp.Api.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    { 
        public Task<T> Insert(T entity);
        public Task<T> Update(int id, T entity);
        public Task Delete(T entity);
        public Task<T> Get(int id);
        public Task<ICollection<T>> GetAll();
    }
}

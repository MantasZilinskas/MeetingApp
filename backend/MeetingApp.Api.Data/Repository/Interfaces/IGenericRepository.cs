using MeetingApp.Api.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    { 
        Task<T> Insert(T entity);
        Task<T> Update(int id, T entity);
        Task Delete(T entity);
        Task<T> Get(int id);
        Task<ICollection<T>> GetAll();
    }
}

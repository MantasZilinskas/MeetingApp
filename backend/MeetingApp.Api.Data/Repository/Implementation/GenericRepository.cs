using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingApp.Api.Data.Context;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeetingApp.Api.Data.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly MeetingAppContext _context;

        public GenericRepository(MeetingAppContext context)
        {
            _context = context;
        }

        public async Task Delete(T registry)
        {
            _context.Set<T>().Remove(registry);
            await _context.SaveChangesAsync();
        }

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(value => value.Id == id);
        }

        public async Task<ICollection<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> Insert(T registry)
        {
            _context.Set<T>().Add(registry);
            await _context.SaveChangesAsync();
            return registry;
        }

        public async Task<T> Update(int id, T registry)
        {
            T existing = await _context.Set<T>().FindAsync(id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(registry);
                await _context.SaveChangesAsync();
            }
            return existing;
        }
    }
}

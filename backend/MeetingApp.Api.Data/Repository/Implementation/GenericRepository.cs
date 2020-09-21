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
        private readonly MeetingAppContext _context;

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
            _context.Set<T>().Update(registry);
            await _context.SaveChangesAsync();
            return registry;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingApp.Api.Data.Context;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeetingApp.Api.Data.Repository.Implementation
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly MeetingAppContext _context;

        public TemplateRepository(MeetingAppContext context)
        {
            _context = context;
        }

        public async Task<bool> IsDuplicateName(Template resource)
        {
            return await _context.Templates.AnyAsync(template => template.Name == resource.Name);
        }
        public async Task<int> GetCount()
        {
            return await _context.Templates
                .CountAsync();
        }
        public async Task<Template> Get(int id)
        {
            return await _context.Templates.FirstOrDefaultAsync(template => template.Id == id);
        }
        public async Task<List<Template>> GetSlice(SliceRequestDao request)
        {
            if (request.Order == "asc")
            {
                return request.OrderBy switch
                {
                    "name" => await _context.Templates
                            .OrderBy(i => i.Name)
                            .Skip(request.RowsPerPage * request.Page)
                            .Take(request.RowsPerPage)
                            .ToListAsync(),
                    _ => null
                };
            }
            else
            {
                return request.OrderBy switch
                {
                    "name" => await _context.Templates
                            .OrderByDescending(i => i.Name)
                            .Skip(request.RowsPerPage * request.Page)
                            .Take(request.RowsPerPage)
                            .ToListAsync(),
                    _ => null
                };
            }
        }
        public async Task<List<Template>> GetAll()
        {
            var templates = await _context.Templates
                 .ToListAsync();
            return templates;
        }
        public async Task<Template> Delete(int templateId)
        {
            var template = await _context.Templates.FirstOrDefaultAsync(template => template.Id == templateId);
            if (template != null)
            {
                _context.Templates.Remove(template);
                await _context.SaveChangesAsync();
            }
            return template;
        }

        public async Task<Template> Insert(Template template)
        {
            await _context.Templates.AddAsync(template);
            await _context.SaveChangesAsync();
            return template;
        }

        public async Task<Template> Update(int id, Template template)
        {
            var existing = await _context.Templates.AsNoTracking().FirstOrDefaultAsync(value => value.Id == id);
            if (existing != null)
            {
                template.Id = id;
                _context.Templates.Update(template);
                await _context.SaveChangesAsync();
            }
            return existing;
        }
        
        public async Task<bool> TemplateExists(int templateId)
        {
            return await _context.Templates.AnyAsync(template => template.Id == templateId);
        }

    }
}

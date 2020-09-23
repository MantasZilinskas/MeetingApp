using MeetingApp.Api.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Interfaces
{
    public interface IMeetingRepository : IGenericRepository<Meeting>
    {
        public Task<bool> IsDuplicate(Meeting entity);
    }
}

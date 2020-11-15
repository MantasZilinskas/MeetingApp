using MeetingApp.Api.Data.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        public Task<IdentityResult> InsertUser(User user, string password);
    }
}

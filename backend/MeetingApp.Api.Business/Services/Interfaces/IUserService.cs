using MeetingApp.Api.Business.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Business.Services.Interfaces
{
    public interface IUserService
    {
        public Task<IdentityResult> InsertUser(UserDTO user);
    }
}

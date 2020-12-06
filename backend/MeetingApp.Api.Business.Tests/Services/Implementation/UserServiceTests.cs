using AutoMapper;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Business.Mapping;
using MeetingApp.Api.Business.Services.Implementation;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MeetingApp.Api.Business.Tests.Services.Implementation
{
    public class UserServiceTests
    {

        private Mock<IUserRepository> mockUserRepository;
        private IMapper mockMapper;

        public UserServiceTests()
        {

            this.mockUserRepository = new Mock<IUserRepository>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MeetingDTOProfile());
                cfg.AddProfile(new TodoItemDTOProfile());
                cfg.AddProfile(new UserProfile());
            });
            this.mockMapper = config.CreateMapper();
        }

        private UserService CreateService()
        {
            return new UserService(
                this.mockUserRepository.Object,
                this.mockMapper);
        }

        [Fact]
        public async Task InsertUser_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            UserRequest user = new UserRequest {UserName = "user1"};
            mockUserRepository.Setup(repo => repo.InsertUser(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<IList<string>>())).ReturnsAsync(IdentityResult.Success);
            // Act
            var result = await service.InsertUser(user);

            // Assert
            Assert.True(result.Succeeded);
            
        }

        [Fact]
        public async Task Login_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string userName = "user";
            string password = "user123";
            var expected =  new LoginResponseDAO
            {
                Roles = new List<string> { "testRole" },
                Token = "testToken",
                UserId = "testId"
            };
            mockUserRepository.Setup(repo => repo.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expected);
            // Act
            var result = await service.Login(
                userName,
                password);

            // Assert
            Assert.True(result.UserId == expected.UserId);
            
        }

        [Fact]
        public async Task GetAllUsers_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            List<User> expected = new List<User>
            {
                new User{Id ="0" , UserName="user0"},
                 new User{Id ="1" , UserName="user1"},
            };
            mockUserRepository.Setup(repo => repo.GetAllUsers()).ReturnsAsync(expected);
            // Act
            var result = await service.GetAllUsers();

            // Assert
            Assert.True(expected.Count == result.Count);
            
        }

        [Fact]
        public async Task DeleteUser_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            string userName = "user";
            mockUserRepository.Setup(repo => repo.DeleteUser(It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            // Act
            var result = await service.DeleteUser(userName);

            // Assert
            Assert.True(result.Succeeded);
            
        }

        [Fact]
        public async Task UpdateUser_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            UserRequest user = new UserRequest { UserName = "user"};
            string userId = "testId";
            mockUserRepository.Setup(repo => repo.UpdateUser(It.IsAny<User>(),It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IList<string>>())).ReturnsAsync(IdentityResult.Success);
            // Act
            var result = await service.UpdateUser(user,userId);

            // Assert
            Assert.True(result.Succeeded);
            
        }
    }
}

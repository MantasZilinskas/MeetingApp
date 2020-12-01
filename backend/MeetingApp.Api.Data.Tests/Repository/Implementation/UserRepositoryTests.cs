using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Implementation;
using MeetingApp.Api.Data.Tests.AsyncMock;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MeetingApp.Api.Data.Tests.Repository.Implementation
{
    public class UserRepositoryTests
    {
        private Mock<UserManager<User>> mockUserManager;
        private Mock<IOptions<ApplicationSettings>> mockOptions;
        private UserRepository userRepository;

        public UserRepositoryTests()
        {

            var _users = new List<User>
            {
                new User{Id = "0",UserName = "user0"},
                new User{Id = "1",UserName = "user1"},
                new User{Id = "2",UserName = "user2"}
            };
            this.mockUserManager = MockUserManager<User>(_users);
            this.mockOptions = new Mock<IOptions<ApplicationSettings>>();
            userRepository = new UserRepository(mockUserManager.Object, mockOptions.Object);
        }
        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(ls[0]);
            var list = ls.AsQueryable()
            .BuildMockDbSet();
            mgr.Setup(x => x.Users).Returns(list.Object);
            return mgr;
        }
        [Fact]
        public async Task GetUserProfile_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            string userId = "0";

            // Act
            var result = await userRepository.GetUserProfile(userId);

            // Assert
            Assert.True(result.Id.CompareTo(userId) == 0);

        }

        [Fact]
        public async Task InsertUser_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            User user = new User { Id = "5", UserName = "user5"};
            string password = "testPassword";
            IList<string> roles = new List<string> { "testRole" };

            // Act
            var result = await userRepository.InsertUser(user,password, roles);

            // Assert
            mockUserManager.Verify(m => m.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            Assert.True(result.Succeeded);

        }

        //[Fact]
        //public async Task Login_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange

        //    string userName = null;
        //    string password = null;

        //    // Act
        //    var result = await userRepository.Login(
        //        userName,
        //        password);

        //    // Assert
        //    Assert.True(false);

        //}

        [Fact]
        public async Task GetAllUsers_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            // Act
            var result = await userRepository.GetAllUsers();

            // Assert
            Assert.True(result.Count == 3);

        }

        [Fact]
        public async Task DeleteUser_IfUserExists_ReturnSuccesfulIdentityResult()
        {
            // Arrange

            string userName = "Mantas";
            User expected = new User { Id = "6", UserName = userName };
            mockUserManager.Setup(m => m.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(expected));
            mockUserManager.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            // Act
            var result = await userRepository.DeleteUser(userName);

            // Assert
            Assert.True(result.Succeeded);

        }
        [Fact]
        public async Task DeleteUser_IfUserDoesNotExist_ReturnNull()
        {
            // Arrange

            string userName = "Mantas";
            User expected = null;
            mockUserManager.Setup(m => m.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(expected));
            // Act
            var result = await userRepository.DeleteUser(userName);

            // Assert
            Assert.True(result == null);

        }

        [Fact]
        public async Task UpdateUser_IfUserExists_ReturnSuccesfullIdentityResult()
        {
            // Arrange
            string userId = "6";
            User expected = new User { Id = userId, UserName = "Mantas" };
            mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(expected));
            mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await userRepository.UpdateUser(expected,userId);

            // Assert
            Assert.True(result.Succeeded);

        }
        [Fact]
        public async Task UpdateUser_IfUserDoesNotExist_ThrowKeyNotFoundException()
        {
            // Arrange
            string userId = "6";
            User expected = null;
            mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(expected));
            mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);

            // Act
            await Assert.ThrowsAsync<KeyNotFoundException>(() => userRepository.UpdateUser(expected, userId));

            // Assert

        }
    }
}

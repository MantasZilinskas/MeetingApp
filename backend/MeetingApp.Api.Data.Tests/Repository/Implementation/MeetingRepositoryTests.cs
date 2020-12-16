using System;
using MeetingApp.Api.Data.Context;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Implementation;
using MeetingApp.Api.Data.Tests.AsyncMock;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MeetingApp.Api.Data.Tests.Repository.Implementation
{
    public class MeetingRepositoryTests
    {
        private readonly Mock<MeetingAppContext> mockContext;
        private Mock<UserManager<User>> mockUserManager;
        private Mock<DbSet<Meeting>> mockSet;
        private readonly MeetingRepository meetingRepository;

        public MeetingRepositoryTests()
        {
            List<User> users = new List<User>
            {
                new User{Id = "0",UserName = "user0"},
                new User{Id = "1",UserName = "user1"},
                new User{Id = "2",UserName = "user2"}
            };
            this.mockUserManager = MockUserManager<User>(users);
            this.mockContext = new Mock<MeetingAppContext>();
            // Mock meeting data that is used in testing methods
            mockSet = new List<Meeting>
            {
                new Meeting{Id= 0, Name  = "Test meeting"},
                new Meeting{Id = 1, Name = "Test meeting2"}
            }.AsQueryable()
            .BuildMockDbSet();
            mockContext.Setup(c => c.Meetings).Returns(mockSet.Object);
            this.meetingRepository = new MeetingRepository(mockContext.Object, mockUserManager.Object);
        }

        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            //mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            //mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            //mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            //mgr.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(ls[0]);

            return mgr;
        }

        [Fact]
        public async Task IsDuplicateName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            Meeting resource = new Meeting { Id= 0 , Name= "Test meeting" };
            // Act
            var result = await meetingRepository.IsDuplicateName(resource);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            int id = 1;

            // Act
            var result = await meetingRepository.Get(id);

            // Assert
            Assert.True(String.Compare(result.Name, "Test meeting2", StringComparison.Ordinal) == 0);
            
        }

        [Fact]
        public async Task GetAll_StateUnderTest_ExpectedBehavior()
        {
            // Arrange


            // Act
            var result = await meetingRepository.GetAll();

            // Assert
            Assert.True(result.Count == 2);

        }

        [Fact]
        public async Task Delete_IfMeetingExist_ReturnDeletedMeeting()
        {
            // Arrange
            var meetingId = 1;
            // Act
            var meeting = await meetingRepository.Delete(meetingId);
            // Assert
            mockContext.Verify(s => s.SaveChangesAsync(new CancellationToken()), Times.Once);
            Assert.True(meeting.Id == 1);

        }
        [Fact]
        public async Task Delete_IfMeetingDoesNotExist_ReturnNull()
        {
            // Arrange
            var meetingId = 464949;
            // Act
            var meeting = await meetingRepository.Delete(meetingId);
            // Assert
            Assert.True(meeting == null); 

        }

        [Fact]
        public async Task Insert_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            Meeting meetingToInsert = new Meeting { Id = 3, Name = "Inserted meeting" };

            // Act
            var returnedMeeting = await meetingRepository.Insert(meetingToInsert);
            // Assert
            mockContext.Verify(s => s.SaveChangesAsync(new CancellationToken()), Times.Once);
            Assert.True(returnedMeeting.Name == meetingToInsert.Name);

        }

        [Fact]
        public async Task Update_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            int id = 1;
            Meeting meeting = new Meeting { Name = "Updated meeting" }; ;

            // Act
             await meetingRepository.Update(id,meeting);

            // Assert
            mockContext.Verify(c => c.Meetings.Update(It.IsAny<Meeting>()), Times.Once);
            mockContext.Verify(c => c.SaveChangesAsync(new CancellationToken()), Times.Once);
        }
        [Fact]
        public async Task Update_MeetingDoesNotExist_ExpectedBehavior()
        {
            // Arrange

            int id = 56654;
            Meeting meeting = new Meeting { Name = "Updated meeting" }; ;

            // Act
            var result =  await meetingRepository.Update(id, meeting);

            // Assert
            mockContext.Verify(c => c.Meetings.Update(It.IsAny<Meeting>()), Times.Never);
            mockContext.Verify(c => c.SaveChangesAsync(new CancellationToken()), Times.Never);
        }

        [Fact]
        public async Task MeetingExists_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            int meetingId = 0;

            // Act
            var result = await meetingRepository.MeetingExists(meetingId);

            // Assert
            Assert.True(result);

        }
    }
}

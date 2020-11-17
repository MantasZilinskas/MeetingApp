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
    public class MeetingServiceTests
    {
        private Mock<IMeetingRepository> mockMeetingRepository;
        private Mock<ITodoItemRepository> mockTodoItemRepository;
        private IMapper mockMapper;
        private MeetingService service;

        public MeetingServiceTests()
        {
            this.mockMeetingRepository = new Mock<IMeetingRepository>();
            this.mockTodoItemRepository = new Mock<ITodoItemRepository>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MeetingDTOProfile());
                cfg.AddProfile(new TodoItemDTOProfile());
                cfg.AddProfile(new UserProfile());
            });
            this.mockMapper = config.CreateMapper();
            this.service = new MeetingService(
                this.mockMeetingRepository.Object,
                this.mockTodoItemRepository.Object,
                this.mockMapper);
        }

        [Fact]
        public async Task Delete_IfMeetingExists_ReturnDeletedMeeting()
        {
            // Arrange
            int meetingId = 0;
            var expectedMeeting = new Meeting { Id = meetingId, Name = "test" };
            mockMeetingRepository.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(Task.FromResult(expectedMeeting));

            // Act
            var result = await service.Delete(meetingId);

            // Assert
            mockTodoItemRepository.Verify(repo => repo.DeleteMeetingItems(It.Is<int>(id => id == meetingId)), Times.Once());
            mockMeetingRepository.Verify(repo => repo.Delete(It.Is<int>(id => id == meetingId)), Times.Once());
            Assert.True(result.Id == 0);

        }
        [Fact]
        public async Task Delete_IfMeetingDoesntExist_ReturnNull()
        {
            // Arrange
            Meeting meeting = null;
            mockMeetingRepository.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(Task.FromResult(meeting));

            // Act
            var result = await service.Delete(0);

            // Assert
            Assert.True(result == null); ;

        }

        [Fact]
        public async Task Get_Returns_Meeting()
        {
            // Arrange
            int meetingId = 0;
            var expected = new Meeting { Id = 0, Name = "test" };
            mockMeetingRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns(Task.FromResult(expected));

            // Act
            var result = await service.Get(meetingId);

            // Assert
            Assert.True(expected.Id == result.Id);

        }

        [Fact]
        public async Task GetAll_Returns_List()
        {
            // Arrange
            var expectedResult = new List<Meeting> {
                new Meeting { Id = 0, Name = "test0", Description = "test0" },
                new Meeting { Id = 1, Name = "test1", Description = "test1" }};
            mockMeetingRepository.Setup(repo => repo.GetAll()).Returns(Task.FromResult(expectedResult));
            // Act
            var result = await service.GetAll();
            // Assert
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task Insert_IfMeetingIsNotDuplicate_ReturnInsertedMeeting()
        {
            // Arrange
            Meeting expected = new Meeting { Id = 0, Name = "test" };
            MeetingDTO toInsert = new MeetingDTO { Id = 0, Name = "test" };
            mockMeetingRepository.Setup(repo => repo.IsDuplicateName(It.IsAny<Meeting>())).Returns(Task.FromResult(false));
            mockMeetingRepository.Setup(repo => repo.Insert(It.IsAny<Meeting>())).Returns(Task.FromResult(expected));

            // Act
            var result = await service.Insert(toInsert);

            // Assert
            Assert.True(expected.Id == result.Id);
        }
        [Fact]
        public async Task Insert_IfMeetingIsDuplicate_ReturnNull()
        {
            // Arrange
            MeetingDTO toInsert = new MeetingDTO { Id = 0, Name = "test" };
            mockMeetingRepository.Setup(repo => repo.IsDuplicateName(It.IsAny<Meeting>())).Returns(Task.FromResult(true));

            // Act
            var result = await service.Insert(toInsert);

            // Assert
            Assert.True(result == null);
        }

        [Fact]
        public async Task Update_IfMeetingIsNotDuplicate_ReturnUpdatedMeeting()
        {
            // Arrange
            Meeting expected = new Meeting { Id = 0, Name = "test" };
            MeetingDTO toUpdate = new MeetingDTO { Id = 0, Name = "test" };
            mockMeetingRepository.Setup(repo => repo.IsDuplicateName(It.IsAny<Meeting>())).Returns(Task.FromResult(false));
            mockMeetingRepository.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<Meeting>())).Returns(Task.FromResult(expected));

            // Act
            var result = await service.Update(0, toUpdate);

            // Assert
            Assert.True(expected.Name == result.Name);
        }
        [Fact]
        public async Task Update_IfMeetingIsDuplicate_ReturnNull()
        {
            // Arrange
            MeetingDTO toUpdate = new MeetingDTO { Id = 0, Name = "test" };
            mockMeetingRepository.Setup(repo => repo.IsDuplicateName(It.IsAny<Meeting>())).Returns(Task.FromResult(true));

            // Act
            var result = await service.Update(0, toUpdate);

            // Assert
            Assert.True(result == null);
        }

        [Fact]
        public async Task GetMeetingTodoItems_IfMeetingExists_ReturnMeetingTodoItemsList()
        {
            // Arrange
            int meetingId = 0;
            Meeting returnedMeeting = new Meeting { Id = meetingId, Name = "Test" };
            ICollection<TodoItem> expected = new List<TodoItem>
            {
                new TodoItem { Id = 0, Name = "item1" },
                new  TodoItem {Id = 1, Name = "item2"}
            };
            mockMeetingRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns(Task.FromResult(returnedMeeting));
            mockTodoItemRepository.Setup(repo => repo.GetMeetingTodoItems(It.IsAny<int>())).Returns(Task.FromResult(expected));

            // Act
            var result = await service.GetMeetingTodoItems(meetingId);

            // Assert
            Assert.True(result.Count == 2);

        }
        [Fact]
        public async Task GetMeetingTodoItems_IfMeetingDoesNotExist_ReturnNull()
        {
            // Arrange
            int meetingId = 0;
            Meeting expected = null;
            mockMeetingRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns(Task.FromResult(expected));
            // Act
            var result = await service.GetMeetingTodoItems(meetingId);
            // Assert
            Assert.True(result == null);

        }

        [Theory]
        [InlineData(0,"testId","testId")]
        public async Task InsertMeetingUser_IfMeetingExists_ReturnInsertedUserId(int meetingId, string userId, string expected)
        {
            // Arrange
            mockMeetingRepository.Setup(repo => repo.MeetingExists(It.IsAny<int>())).Returns(Task.FromResult(true));
            mockMeetingRepository.Setup(repo => repo.InsertMeetingUser(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(expected));

            // Act
            var result = await service.InsertMeetingUser(userId,meetingId);

            // Assert
            Assert.True(result.CompareTo(expected) == 0);
        }
        [Theory]
        [InlineData(0, "testId")]
        public async Task InsertMeetingUser_IfMeetingDoesNotExist_ThrowKeyNotFoundException(int meetingId, string userId)
        {
            // Arrange
            mockMeetingRepository.Setup(repo => repo.MeetingExists(It.IsAny<int>())).Returns(Task.FromResult(false));
            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.InsertMeetingUser(userId, meetingId));
        }

        [Fact]
        public async Task DeleteMeetingUser_IfMeetingExists_ReturnIdentityResult()
        {
            // Arrange
            int meetingId = 0;
            string userId = "testId";
            mockMeetingRepository.Setup(repo => repo.MeetingExists(It.IsAny<int>())).Returns(Task.FromResult(true));

            // Act
            await service.DeleteMeetingUser(meetingId, userId);

            // Assert
            mockMeetingRepository.Verify(repo => repo.DeleteMeetingUser(It.Is<int>(id => id == meetingId), It.Is<string>(id => id == userId)),Times.Once());

        }
        [Fact]
        public async Task DeleteMeetingUser_IfMeetingDoesNotExist_ThrowKeyNotFoundException()
        {
            // Arrange
            int meetingId = 0;
            string userId = "testId";
            mockMeetingRepository.Setup(repo => repo.MeetingExists(It.IsAny<int>())).Returns(Task.FromResult(false));
            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.DeleteMeetingUser(meetingId,userId));
        }

        [Fact]
        public async Task GetAllMeetingUsers_IfMeetingExists_ReturnUserList()
        {
            // Arrange
            int meetingId = 0;
            var expected = new List<User>
            {
                new User{Id = "1", UserName = "user1"},
                new User{Id = "2", UserName = "user2"},
            };
            mockMeetingRepository.Setup(repo => repo.MeetingExists(It.IsAny<int>())).Returns(Task.FromResult(true));
            mockMeetingRepository.Setup(repo => repo.GetAllMeetingUsers(It.IsAny<int>())).Returns(Task.FromResult(expected));
            // Act
            var result = await service.GetAllMeetingUsers(meetingId);
            // Assert
            Assert.True(result.Count == 2);

        }
        [Fact]
        public async Task GetAllMeetingUsers_IfMeetingDoesNotExist_ThrowKeyNotFoundException()
        {
            // Arrange
            int meetingId = 0;
            mockMeetingRepository.Setup(repo => repo.MeetingExists(It.IsAny<int>())).Returns(Task.FromResult(false));
            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetAllMeetingUsers(meetingId));
        }

        [Fact]
        public async Task GetMeetingUser_IfMeetingExists_ReturnUser()
        {
            // Arrange
            int meetingId = 0;
            string userId = "testId";
            User expected = new User { Id = "1", UserName = "user1" };
            mockMeetingRepository.Setup(repo => repo.MeetingExists(It.IsAny<int>())).Returns(Task.FromResult(true));
            mockMeetingRepository.Setup(repo => repo.GetMeetingUser(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(expected));
            // Act
            var result = await service.GetMeetingUser(meetingId,userId);
            // Assert
            Assert.True(result.UserName.CompareTo(expected.UserName) == 0);
        }
        [Fact]
        public async Task GetMeetingUser_IfMeetingDoesNotExist_ThrowKeyNotFoundException()
        {
            // Arrange
            int meetingId = 0;
            string userId = "testId";
            mockMeetingRepository.Setup(repo => repo.MeetingExists(It.IsAny<int>())).Returns(Task.FromResult(false));
            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetMeetingUser(meetingId,userId));
        }
    }
}

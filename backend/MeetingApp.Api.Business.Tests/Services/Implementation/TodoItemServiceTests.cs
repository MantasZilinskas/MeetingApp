using AutoMapper;
using MeetingApp.Api.Business.DTO;
using MeetingApp.Api.Business.Mapping;
using MeetingApp.Api.Business.Services.Implementation;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MeetingApp.Api.Business.Tests.Services.Implementation
{
    public class TodoItemServiceTests
    {

        private Mock<ITodoItemRepository> mockTodoItemRepository;
        private IMapper mockMapper;
        private Mock<IMeetingRepository> mockMeetingRepository;

        public TodoItemServiceTests()
        {

            this.mockTodoItemRepository = new Mock<ITodoItemRepository>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MeetingDTOProfile());
                cfg.AddProfile(new TodoItemDTOProfile());
                cfg.AddProfile(new UserProfile());
            });
            this.mockMapper = config.CreateMapper();
            this.mockMeetingRepository = new Mock<IMeetingRepository>();
        }

        private TodoItemService CreateService()
        {
            return new TodoItemService(
                this.mockTodoItemRepository.Object,
                this.mockMapper,
                this.mockMeetingRepository.Object);
        }

        [Fact]
        public async Task Delete_IfTodoItemExists_ReturnDeletedTodoItem()
        {
            // Arrange
            var service = this.CreateService();
            int id = 0;
            int meetingId = 0;
            TodoItem todoItem = new TodoItem { Id = 0, Name = "todoItem0" };
            mockTodoItemRepository.Setup(repo => repo.GetMeetingTodoItem(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(todoItem));

            // Act
            var result = await service.Delete(id, meetingId);

            // Assert
            mockTodoItemRepository.Verify(repo => repo.Delete(It.IsAny<TodoItem>()), Times.Once);
            Assert.True(result.Id == todoItem.Id);
        }
        [Fact]
        public async Task Delete_IfTodoItemDoesNotExist_ReturnNull()
        {
            // Arrange
            var service = this.CreateService();
            int id = 0;
            int meetingId = 0;
            TodoItem todoItem = null;
            mockTodoItemRepository.Setup(repo => repo.GetMeetingTodoItem(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(todoItem));
            // Act
            var result = await service.Delete(id, meetingId);

            // Assert
            Assert.True(result == null);
        }

        [Fact]
        public async Task Get_StateUnderTest_ReturnTodoItem()
        {
            // Arrange
            var service = this.CreateService();
            int id = 0;
            int meetingId = 0;
            TodoItem todoItem = new TodoItem { Id = 0, Name = "todoItem0" };
            mockTodoItemRepository.Setup(repo => repo.GetMeetingTodoItem(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(todoItem));
            // Act
            var result = await service.Get(id, meetingId);

            // Assert
            Assert.True(result.Name == todoItem.Name);

        }

        [Fact]
        public async Task GetAll_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            List<TodoItem> list = new List<TodoItem>
            {
                new TodoItem{Id = 0, Name="item0"},
                new TodoItem{Id= 1 , Name ="item1"}
            };
            mockTodoItemRepository.Setup(repo => repo.GetAll()).ReturnsAsync(list);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.True(result.Count == list.Count);

        }

        [Fact]
        public async Task Insert_IfMeetingExists_ReturnInsertedTodoItem()
        {
            // Arrange
            var service = this.CreateService();
            Meeting meeting = new Meeting { Id = 0, Name = "meeting0" };
            TodoItemDTO todoItem = new TodoItemDTO { Id = 0, Name = "todoItem0" };
            TodoItem expected = new TodoItem { Id = 0, Name = "todoItem0" };

            mockMeetingRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns(Task.FromResult(meeting));
            mockTodoItemRepository.Setup(repo => repo.Insert(It.IsAny<TodoItem>())).ReturnsAsync(expected);
            // Act
            var result = await service.Insert(todoItem);

            // Assert
            mockTodoItemRepository.Verify(repo => repo.Insert(It.IsAny<TodoItem>()), Times.Once);
            Assert.True(result.Id == todoItem.Id);
        }
        [Fact]
        public async Task Insert_IfTMeetingDoesNotExist_ReturnNull()
        {
            // Arrange
            var service = this.CreateService();
            Meeting meeting = null;
            TodoItemDTO todoItem = new TodoItemDTO { Id = 0, Name = "todoItem0" };
            mockMeetingRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns(Task.FromResult(meeting));

            // Act
            var result = await service.Insert(todoItem);

            // Assert
            Assert.True(result == null);
        }

        [Fact]
        public async Task Update_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            int todoItemId = 0;
            TodoItemDTO todoItem = new TodoItemDTO { Id = 0, Name = "todoItem0" };
            TodoItem expected = new TodoItem { Id = 0, Name = "todoItem0" };
            mockTodoItemRepository.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<TodoItem>())).ReturnsAsync(expected);

            // Act
            var result = await service.Update(todoItemId,todoItem);

            // Assert
            Assert.True(result.Name == todoItem.Name);

        }
    }
}

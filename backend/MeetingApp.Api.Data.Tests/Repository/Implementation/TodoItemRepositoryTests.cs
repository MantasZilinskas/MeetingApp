using MeetingApp.Api.Data.Context;
using MeetingApp.Api.Data.Repository.Implementation;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MeetingApp.Api.Data.Tests.Repository.Implementation
{
    public class TodoItemRepositoryTests
    {
        private MockRepository mockRepository;

        private Mock<MeetingAppContext> mockMeetingAppContext;

        public TodoItemRepositoryTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockMeetingAppContext = this.mockRepository.Create<MeetingAppContext>();
        }

        private TodoItemRepository CreateTodoItemRepository()
        {
            return new TodoItemRepository(
                this.mockMeetingAppContext.Object);
        }

        [Fact]
        public async Task DeleteMeetingItems_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var todoItemRepository = this.CreateTodoItemRepository();
            int meetingId = 0;

            // Act
            await todoItemRepository.DeleteMeetingItems(
                meetingId);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task InsertMeetingItems_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var todoItemRepository = this.CreateTodoItemRepository();
            List todoItems = null;

            // Act
            await todoItemRepository.InsertMeetingItems(
                todoItems);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetMeetingTodoItems_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var todoItemRepository = this.CreateTodoItemRepository();
            int meetingId = 0;

            // Act
            var result = await todoItemRepository.GetMeetingTodoItems(
                meetingId);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetMeetingTodoItem_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var todoItemRepository = this.CreateTodoItemRepository();
            int meetingId = 0;
            int todoItemId = 0;

            // Act
            var result = await todoItemRepository.GetMeetingTodoItem(
                meetingId,
                todoItemId);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Insert_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var todoItemRepository = this.CreateTodoItemRepository();
            TodoItem todoItem = null;

            // Act
            var result = await todoItemRepository.Insert(
                todoItem);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Update_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var todoItemRepository = this.CreateTodoItemRepository();
            int todoItemId = 0;
            TodoItem todoItem = null;

            // Act
            var result = await todoItemRepository.Update(
                todoItemId,
                todoItem);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var todoItemRepository = this.CreateTodoItemRepository();
            TodoItem todoItem = null;

            // Act
            await todoItemRepository.Delete(
                todoItem);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var todoItemRepository = this.CreateTodoItemRepository();
            int id = 0;

            // Act
            var result = await todoItemRepository.Get(
                id);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetAll_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var todoItemRepository = this.CreateTodoItemRepository();

            // Act
            var result = await todoItemRepository.GetAll();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}

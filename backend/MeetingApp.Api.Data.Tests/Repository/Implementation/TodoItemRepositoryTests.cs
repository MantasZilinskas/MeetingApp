using MeetingApp.Api.Data.Context;
using MeetingApp.Api.Data.Model;
using MeetingApp.Api.Data.Repository.Implementation;
using MeetingApp.Api.Data.Tests.AsyncMock;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MeetingApp.Api.Data.Tests.Repository.Implementation
{
    public class TodoItemRepositoryTests
    {

        private readonly Mock<MeetingAppContext> mockContext;
        private readonly TodoItemRepository todoItemRepository;

        public TodoItemRepositoryTests()
        {
            this.mockContext = new Mock<MeetingAppContext>();
            var mockSet = new List<TodoItem>
            {
                new TodoItem{Id= 0, Name  = "Test meeting0"},
                new TodoItem{Id = 1, Name = "Test meeting1"},
                new TodoItem{Id = 2, Name = "Test meeting2"}
            }.AsQueryable()
            .BuildMockDbSet();
            mockContext.Setup(c => c.TodoItems).Returns(mockSet.Object);
            this.todoItemRepository = new TodoItemRepository(mockContext.Object);
        }

        //[Fact]
        //public async Task DeleteMeetingItems_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    int meetingId = 0;
        //    var list = new List<Meeting>
        //    {
        //        new Meeting{Id = 0, Name = "test0" },
        //        new Meeting{Id = 1, Name = "test1" },
        //    };

        //    // Act
        //    await todoItemRepository.DeleteMeetingItems(meetingId);

        //    // Assert
        //    mockContext.Verify(c => c.RemoveRange(), Times.Once);
        //}

        [Fact]
        public async Task InsertMeetingItems_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            List<TodoItem> todoItems = new List<TodoItem>
            {
                new TodoItem { Id = 0 , Name = "test0"},
                new TodoItem { Id = 1 , Name = "test1"}
            };

            // Act
            await todoItemRepository.InsertMeetingItems(todoItems);

            // Assert
            mockContext.Verify(c => c.TodoItems.Add(It.IsAny<TodoItem>()), Times.Exactly(todoItems.Count));
            mockContext.Verify(s => s.SaveChangesAsync(new CancellationToken()), Times.Once);

        }

        //[Fact]
        //public async Task GetMeetingTodoItems_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange

        //    int meetingId = 0;

        //    // Act
        //    var result = await todoItemRepository.GetMeetingTodoItems(
        //        meetingId);

        //    // Assert
        //    Assert.True(false);

        //}

        //[Fact]
        //public async Task GetMeetingTodoItem_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange

        //    int meetingId = 0;
        //    int todoItemId = 0;

        //    var mockSetMeeting = new List<Meeting>
        //    {
        //        new Meeting{Id= 0, Name  = "Test meeting"},
        //        new Meeting{Id = 1, Name = "Test meeting2"}
        //    }.AsQueryable()
        //    .BuildMockDbSet();
        //    mockContext.Setup(c => c.Meetings).Returns(mockSetMeeting.Object);

        //    // Act
        //    var result = await todoItemRepository.GetMeetingTodoItem(
        //        meetingId,
        //        todoItemId);

        //    // Assert
        //    Assert.True(false);

        //}

        [Fact]
        public async Task Insert_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            TodoItem todoItem = new TodoItem { Id = 2 , Name = "test3"};

            // Act
            var result = await todoItemRepository.Insert(todoItem);

            // Assert
            mockContext.Verify(c => c.TodoItems.Add(It.IsAny<TodoItem>()), Times.Once);
            mockContext.Verify(s => s.SaveChangesAsync(new CancellationToken()), Times.Once);

        }

        //[Fact]
        //public async Task Update_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange

        //    int todoItemId = 0;
        //    TodoItem todoItem = null;

        //    // Act
        //    var result = await todoItemRepository.Update(
        //        todoItemId,
        //        todoItem);

        //    // Assert
        //    Assert.True(false);

        //}

        [Fact]
        public async Task Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            TodoItem todoItem = new TodoItem { Id = 2, Name = "test3" };

            // Act
            await todoItemRepository.Delete(
                todoItem);

            // Assert
            mockContext.Verify(c => c.TodoItems.Remove(It.IsAny<TodoItem>()), Times.Once);
            mockContext.Verify(s => s.SaveChangesAsync(new CancellationToken()), Times.Once);

        }

        [Fact]
        public async Task Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            int id = 0;

            // Act
            var result = await todoItemRepository.Get(id);

            // Assert
            Assert.True(result.Id == id);

        }

        [Fact]
        public async Task GetAll_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            // Act
            var result = await todoItemRepository.GetAll();

            // Assert
            Assert.True(result.Count == 3);

        }
    }
}

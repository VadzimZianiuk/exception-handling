using NUnit.Framework;
using System;
using Task3.DoNotChange;
using Task3.Tests.Stubs;

namespace Task3.Tests
{
    [TestFixture]
    public class UserTaskServiceTests
    {
        private const int CorrectUserId = 1;
        private readonly UserTaskService taskService;
        private readonly IUserDao userDao;
        private readonly UserTask taskToAdd;

        public UserTaskServiceTests()
        {
            this.userDao = new UserDaoStub();
            this.taskService = new UserTaskService(this.userDao);
            this.taskToAdd = new UserTask("task4");
        }

        [Test]
        public void Ctor_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new UserTaskService(null));

        [Test]
        public void CreateUserTask_ValidData()
        {
            var tasks = userDao.GetUser(CorrectUserId).Tasks;
            int expectedTasksCount = tasks.Count + 1;

            Assert.IsFalse(tasks.Contains(taskToAdd));
            taskService.AddTaskForUser(CorrectUserId, taskToAdd);
            Assert.AreEqual(expectedTasksCount, tasks.Count);
            Assert.IsTrue(tasks.Contains(taskToAdd));

            tasks.Remove(taskToAdd);
        }

        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public void CreateUserTask_ArgumentOutOfRangeException(int userId) =>
            Assert.Throws<ArgumentOutOfRangeException>(() => taskService.AddTaskForUser(userId, taskToAdd));

        [TestCase(999)]
        [TestCase(int.MaxValue)]
        public void CreateUserTask_UserNotFoundException(int userId) =>
            Assert.Throws<UserNotFoundException>(() => taskService.AddTaskForUser(userId, taskToAdd));

        [Test]
        public void CreateUserTask_TaskAlreadyExistsException()
        {
            var tasks = userDao.GetUser(CorrectUserId).Tasks;
            
            AddTaskForUser();
            Assert.Throws<TaskAlreadyExistsException>(AddTaskForUser);
            
            tasks.Remove(taskToAdd);

            void AddTaskForUser() => taskService.AddTaskForUser(CorrectUserId, taskToAdd);
        }
    }
}
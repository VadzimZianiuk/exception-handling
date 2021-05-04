using NUnit.Framework;
using System;
using System.Linq;
using Task3.DoNotChange;
using Task3.Tests.Stubs;

namespace Task3.Tests
{
    [TestFixture]
    public class UserTaskControllerTests
    {
        private const int CorrectUserId = 1;
        private const string TaskDescription = "task4";
        private readonly UserTaskController controller;
        private readonly IUserDao userDao;

        public UserTaskControllerTests()
        {
            this.userDao = new UserDaoStub();
            UserTaskService service = new UserTaskServiceStub(this.userDao);
            this.controller = new UserTaskController(service);
        }

        [Test]
        public void Ctor_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new UserTaskController(null));

        [Test]
        public void CreateUserTask_ValidData()
        {
            var tasks = userDao.GetUser(CorrectUserId).Tasks;
            int expectedTasksCount = tasks.Count + 1;

            Assert.IsFalse(tasks.Any(TaskPredicate));
            controller.AddTaskForUser(CorrectUserId, TaskDescription);
            Assert.AreEqual(expectedTasksCount, tasks.Count);
            Assert.IsTrue(tasks.Any(TaskPredicate));

            tasks.Remove(tasks.First(TaskPredicate));
        }

        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public void CreateUserTask_ArgumentOutOfRangeException(int userId) =>
            Assert.Throws<ArgumentOutOfRangeException>(() => controller.AddTaskForUser(userId, TaskDescription));

        [TestCase(999)]
        [TestCase(int.MaxValue)]
        public void CreateUserTask_UserNotFoundException(int userId) =>
            Assert.Throws<UserNotFoundException>(() => controller.AddTaskForUser(userId, TaskDescription));

        [Test]
        public void CreateUserTask_TaskAlreadyExistsException()
        {
            var tasks = userDao.GetUser(CorrectUserId).Tasks;
            
            AddTaskForUser();
            Assert.Throws<TaskAlreadyExistsException>(AddTaskForUser);
            
            tasks.Remove(tasks.First(TaskPredicate));

            void AddTaskForUser() => controller.AddTaskForUser(CorrectUserId, TaskDescription);
        }

        private bool TaskPredicate(UserTask task) => task.Description == TaskDescription;
    }
}
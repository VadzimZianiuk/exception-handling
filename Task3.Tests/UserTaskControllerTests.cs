using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Task3.DoNotChange;
using Task3.Tests.Stubs;

namespace Task3.Tests
{
    [TestFixture]
    public class UserTaskControllerTests
    {
        private const string NewTaskDescription = "task4";
        private readonly UserTaskController controller;
        private readonly IUserDao userDao;

        public UserTaskControllerTests()
        {
            this.userDao = new UserDaoStub();
            var taskService = new UserTaskServiceStub(userDao);
            this.controller = new UserTaskController(taskService);
        }

        [Test]
        public void Ctor_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new UserTaskController(null));

        [TestCase(1, NewTaskDescription, true)]
        [TestCase(-1, NewTaskDescription, false)]
        [TestCase(1, "task3", false)]
        public void CreateUserTask_DoesNotThrow(int userId, string taskDescription, bool expectedResult)
        {
            var tasks = userDao.GetUser(userId)?.Tasks;

            var result = controller.AddTaskForUser(userId, taskDescription);
            Assert.AreEqual(expectedResult, result);

            RemoveTask(tasks, taskDescription);
        }

        private static void RemoveTask(ICollection<UserTask> tasks, string description)
        {
            var task = tasks?.FirstOrDefault(x => x?.Description == description);
            if(task != null)
            {
                tasks.Remove(task);
            }
        }
    }
}
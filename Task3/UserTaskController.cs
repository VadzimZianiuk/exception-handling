using System;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskController
    {
        private readonly UserTaskService taskService;

        public UserTaskController(UserTaskService taskService)
        {
            this.taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
        }

        /// <summary>
        /// Add task for user.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <param name="description">Task to add.</param>
        /// <returns>True if task added for user, otherwise false.</returns>
        /// <remarks>Method uses <see cref="UserTaskService.AddTaskForUser"/>. It can throws exceptions.</remarks>
        public bool AddTaskForUser(int userId, string description)
        {
            var task = new UserTask(description);
            return taskService.AddTaskForUser(userId, task);
        }
    }
}
using System;
using System.Linq;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskService
    {
        private readonly IUserDao userDao;

        public UserTaskService(IUserDao userDao)
        {
            this.userDao = userDao ?? throw new ArgumentNullException(nameof(userDao));
        }

        /// <summary>
        /// Add task for user.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <param name="task">Task to add.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws when userId less than 0.</exception>
        /// <exception cref="UserNotFoundException">Throws when user </exception>
        /// <exception cref="TaskAlreadyExistsException">Throws when User already has a <param name="task">.</param></exception>
        /// <returns>True if task added for user, otherwise false.</returns>
        public bool AddTaskForUser(int userId, UserTask task)
        {
            if (userId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "Invalid userId");
            }

            var user = userDao.GetUser(userId) ?? throw new UserNotFoundException("User not found", nameof(userId));
            if (IsUserContainsTask(user, task))
            {
                throw new TaskAlreadyExistsException("The task already exists", nameof(task));
            }

            user.Tasks.Add(task);
            return true;
        }

        protected virtual bool IsUserContainsTask(IUser user, UserTask task) =>
            user.Tasks.Any(x =>
                string.Equals(task?.Description, x.Description, StringComparison.OrdinalIgnoreCase));
    }
}
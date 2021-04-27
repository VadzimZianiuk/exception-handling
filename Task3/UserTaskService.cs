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

        public void AddTaskForUser(int userId, UserTask task)
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
        }

        protected bool IsUserContainsTask(IUser user, UserTask task) =>
            user.Tasks.Any(x =>
                string.Equals(task?.Description, x.Description, StringComparison.OrdinalIgnoreCase));
    }
}
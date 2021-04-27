using System;
using System.Linq;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IUserDao userDao;

        public UserTaskService(IUserDao userDao)
        {
            this.userDao = userDao ?? throw new ArgumentNullException(nameof(userDao));
        }

        public bool AddTaskForUser(int userId, UserTask task)
        {
            if (userId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "Invalid userId");
            }

            var user = userDao.GetUser(userId) ?? throw new UserNotFoundException("User not found", nameof(userId));
            if (IsUserContainsTask(user, task))
            {
                return false;
            }

            user.Tasks.Add(task);
            return true;

        }

        protected bool IsUserContainsTask(IUser user, UserTask task)
        {
            if (user.Tasks.Any(x =>
                string.Equals(task?.Description, x.Description, StringComparison.OrdinalIgnoreCase)))
            {
                throw new TaskAlreadyExistsException("The task already exists", nameof(task));  
            }

            return false;
        }
    }
}
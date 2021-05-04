using System;
using System.Linq;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskService
    {
        protected readonly IUserDao UserDao;

        public UserTaskService(IUserDao userDao)
        {
            this.UserDao = userDao ?? throw new ArgumentNullException(nameof(userDao));
        }

        public virtual bool AddTaskForUser(int userId, UserTask task)
        {
            if (userId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "Invalid userId");
            }

            var user = UserDao.GetUser(userId) ?? throw new UserNotFoundException("User not found", nameof(userId));
            if (user.Tasks.Any(x => string.Equals(x?.Description, task?.Description, StringComparison.OrdinalIgnoreCase)))
            {
                throw new TaskAlreadyExistsException("The task already exists", nameof(task));
            }

            user.Tasks.Add(task);
            return true;
        }
    }
}
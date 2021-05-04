using System.Linq;
using Task3.DoNotChange;

namespace Task3.Tests.Stubs
{
    internal class UserTaskServiceStub : UserTaskService
    {
        public UserTaskServiceStub(IUserDao userDao) : base(userDao)
        {
        }

        public override bool AddTaskForUser(int userId, UserTask task)
        {
            if (userId < 0)
            {
                return false;
            }

            var user = this.UserDao.GetUser(userId);
            if (user?.Tasks?.FirstOrDefault(x => x?.Description == task?.Description) != null)
            {
                return false;
            }

            user.Tasks.Add(task);
            return true;
        }
    }
}
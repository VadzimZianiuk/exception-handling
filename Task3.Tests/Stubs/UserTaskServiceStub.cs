using System.Linq;
using Task3.DoNotChange;

namespace Task3.Tests.Stubs
{
    class UserTaskServiceStub : UserTaskService
    {
        public UserTaskServiceStub(IUserDao userDao) : base(userDao)
        {
        }

        protected override bool IsUserContainsTask(IUser user, UserTask task)
        {
            return user.Tasks.Any(x => x.Description == task.Description);
        }
    }
}

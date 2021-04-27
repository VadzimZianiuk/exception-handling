using Task3.DoNotChange;

namespace Task3
{
    public interface IUserTaskService
    {
        public bool AddTaskForUser(int userId, UserTask task);
    }
}

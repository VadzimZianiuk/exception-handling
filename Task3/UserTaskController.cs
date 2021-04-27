using System;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskController
    {
        private const string ModelErrorKey = "action_result";
        private readonly UserTaskService taskService;

        public UserTaskController(UserTaskService taskService)
        {
            this.taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
        }

        public bool AddTaskForUser(int userId, string description, IResponseModel model)
        {
            var task = new UserTask(description);
            try
            {
                taskService.AddTaskForUser(userId, task);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                model?.AddAttribute(ModelErrorKey, "Invalid userId");
            }
            catch (UserNotFoundException)
            {
                model?.AddAttribute(ModelErrorKey, "User not found");
            }
            catch (TaskAlreadyExistsException)
            {
                model?.AddAttribute(ModelErrorKey, "The task already exists");
            }

            return false;
        }
    }
}
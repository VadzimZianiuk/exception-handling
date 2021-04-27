using System;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskController
    {
        private readonly IUserTaskService taskService;

        public UserTaskController(IUserTaskService taskService)
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
            catch (Exception ex)
            {
                HandleException(ex, model);
            }

            return false;
        }

        protected void HandleException(Exception ex, IResponseModel model)
        {
            switch (ex)
            {
                case ArgumentOutOfRangeException _:
                    model?.AddAttribute("action_result", "Invalid userId");
                    break;
                case UserNotFoundException _:
                    model?.AddAttribute("action_result", "User not found");
                    break;
                case ArgumentException _:
                    model?.AddAttribute("action_result", "The task already exists");
                    break;
                default:
                    model?.AddAttribute("action_result", "Unknown exception");
                    break;
            }
        }
    }
}
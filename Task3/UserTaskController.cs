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
                return taskService.AddTaskForUser(userId, task);
            }
            catch (Exception ex)
            {
                HandleException(ex, model);
            }

            return false;
        }

        protected void HandleException(Exception ex, IResponseModel model)
        {
            model?.AddAttribute("action_result", ex switch
            {
                ArgumentOutOfRangeException _ => "Invalid userId",
                UserNotFoundException _ => "User not found",
                TaskAlreadyExistsException _ => "The task already exists",
                _ => "Unknown exception"
            });
        }
    }
}
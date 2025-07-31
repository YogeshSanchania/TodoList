using System.ComponentModel.DataAnnotations;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<IEnumerable<TaskItem>> GetAllTasksAsync(string? filter = null)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return _taskRepository.GetAllTasksAsync();
            }
            return _taskRepository.GetAllTasksAsync(filter);
        }

        public async Task<TaskItem?> AddTaskAsync(TaskItem task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task), "Task cannot be null.");
            }
            var validationContext = new ValidationContext(task);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(task, validationContext, validationResults, true))
            {
                throw new ValidationException(validationResults.First().ErrorMessage);
            }
            return await _taskRepository.AddTaskAsync(task);
        }

        public async Task<TaskItem?> UpdateTaskAsync(TaskItem task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task), "Task cannot be null.");
            }
            var validationContext = new ValidationContext(task);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(task, validationContext, validationResults, true))
            {
                throw new ValidationException(validationResults.First().ErrorMessage);
            }
            return await _taskRepository.UpdateTaskAsync(task);
        }


        public Task<TaskItem?> GetTaskByIdAsync(int id) => _taskRepository.GetTaskByIdAsync(id);
    }
}

using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync(string? filter);
        Task<TaskItem?> GetTaskByIdAsync(int id);
        Task<TaskItem?> AddTaskAsync(TaskItem taskItem);
        Task<TaskItem?> UpdateTaskAsync(TaskItem taskItem);
    }
}

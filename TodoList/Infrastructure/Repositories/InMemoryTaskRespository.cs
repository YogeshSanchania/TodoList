using Microsoft.AspNetCore.Routing.Tree;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;

namespace TodoList.Infrastructure.Respositories
{
    public class InMemoryTaskRespository : ITaskRepository
    {

        private readonly List<TaskItem> _tasks = new();
        private int _nextId = 1;
        public async Task<TaskItem> AddTaskAsync(TaskItem taskItem)
        {
            await Task.Delay(10); // Simulate async operation
            taskItem.Id = _nextId++;
            taskItem.CreatedAt = DateTime.Now;
            _tasks.Add(taskItem);
            return taskItem;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            await Task.Delay(10); // Simulate async operation
            return _tasks;
        }

        public Task<IEnumerable<TaskItem>> GetAllTasksAsync(string filter)
        {
            switch (filter)
            {
                case "Completed":
                    return Task.FromResult(_tasks.Where(t => t.IsCompleted).AsEnumerable());
                case "Incomplete":
                    return Task.FromResult(_tasks.Where(t => !t.IsCompleted).AsEnumerable());
                default:
                    return Task.FromResult(_tasks.AsEnumerable());
            }
        }

        public Task<TaskItem> GetTaskByIdAsync(int id) => _tasks.FirstOrDefault(t => t.Id == id) is TaskItem task 
                ? Task.FromResult(task) 
                : Task.FromResult<TaskItem>(null);

        public async Task<TaskItem> UpdateTaskAsync(TaskItem taskItem)
        {
            await Task.Delay(10); // Simulate async operation
            var existingTask = _tasks.FirstOrDefault(t => t.Id == taskItem.Id);
            if (existingTask == null)
            {
                throw new KeyNotFoundException($"Task with ID {taskItem.Id} not found.");
            }
            existingTask = taskItem;
            return taskItem;
        }
    }
}

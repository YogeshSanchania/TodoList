using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;

namespace TodoList.Web.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        
        public async Task<IActionResult> Index(string? filter = "All")
        {
            ViewBag.Filter = filter;
            var tasks = await _taskService.GetAllTasksAsync(filter);
            return View(tasks);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                var createdTask = await _taskService.AddTaskAsync(taskItem);
                return RedirectToAction(nameof(Index));
            }
            return View(taskItem);
        }

        public async Task<IActionResult> Complete(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            task.IsCompleted = true;
            await _taskService.UpdateTaskAsync(task);
            return RedirectToAction(nameof(Index));
        }
    }
}

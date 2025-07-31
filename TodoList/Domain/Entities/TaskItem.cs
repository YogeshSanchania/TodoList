using System.ComponentModel.DataAnnotations;

namespace TodoList.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The title must be between 1 and 100 characters long.", MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Due date must be in the future.")]
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is DateTime date && date > DateTime.Now;
        }
    }
}

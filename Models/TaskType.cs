using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PTr.Models
{
    public enum TaskTypeName
    {
        [Display(Name = "IT Specialist")]
        ITSpecialist = 1,

        [Display(Name = "Accountant")]
        Accountant = 2,

        [Display(Name = "Cleaner")]
        Cleaner = 3
    }
    public class TaskType
    {
        public int Id { get; set; }
        
        public TaskTypeName Name { get; set; }
        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    }
}

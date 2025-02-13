using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PTr.Models
{
    public class WorkSession
    {
        public int Id { get; set; }
        [Display(Name = "Data rozpoczęcia")]
        public DateTime LogIn { get; set; }
        [Display(Name = "Data zakończenia")]
        public DateTime LogOut { get; set; }
        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTr.Models
{
    public class WorkStat
    {
        public int Id { get; set; }

        [Display(Name = "Praca jaką wykonujesz")]
        public int TaskTypeId { get; set; }
        [Display(Name = "Praca jaką wykonujesz")]
        public virtual TaskType? TaskType { get; set; }
        [Display(Name = "Numer przepracowanych godzin")]
        //[Range(0, int.MaxValue, ErrorMessage = "Liczba godzin musi być dodatnia.")]
        public int NumberOfHoursWorked { get; set; }
        public int WorkMoney { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual IdentityUser? User { get; set; }
    }
}

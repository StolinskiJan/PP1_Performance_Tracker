using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTr.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Range(1, 24, ErrorMessage = "Proszę podać wartość Godzin od 1 do 24.")]
        [Display(Name = "Liczba godzin w dniu")]
        public int Hours { get; set; }
        [Range(1, 365, ErrorMessage = "Proszę podać wartość Dni od 1 do 365.")]

        [Display(Name = "Liczba dni roboczych")]
        public int Days { get; set; }
        [Display(Name = "Ile zarabiasz")]
        public int Money { get; set; }

        [Display(Name = "Praca jaką wykonujesz")]

        public int TaskTypeId { get; set; }
        [Display(Name = "Praca jaką wykonujesz")]
        public virtual TaskType? TaskType { get; set; }
        [Display(Name = "Sesja")]
        public int WorkSessionId { get; set; }
        [Display(Name = "Sesja")]
        public virtual WorkSession? WorkSession { get; set; }
        [ForeignKey("User")]
        [Display(Name = "Użytkownik")]
        public string? UserId { get; set; }
        [Display(Name = "Użytkownik")]
        public virtual IdentityUser? User { get; set; }
    }
}

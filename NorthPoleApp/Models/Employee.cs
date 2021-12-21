using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NorthPoleApp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name required")]
        [Display(Name = "Full Name")]
        [MaxLength(40)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Title required")]
        [Display(Name = "Job Title")]
        [MaxLength(40)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Username required")]
        [Display(Name ="User Name")]
        [MaxLength(40)]
        public string UserName { get; set; }

        [MaxLength(40)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public List<Task> Tasks { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NorthPoleApp.Models
{
    public class Letter
    {
        [Key]
        public int LetterId { get; set; }

        [Required(ErrorMessage ="Please enter name")]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please enter city/state")]
        [MaxLength(40)]
        [Display(Name="City/State/Province")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter country")]
        [MaxLength(40)]
        public string Country { get; set; }

        [MaxLength(200)]
        [Display(Name= "Note To Santa")]
        public string Note { get; set; }

        [Display(Name="Were they good?")]
        public bool IsGood { get; set; }

        public bool IsReviewed { get; set; }

        [Display(Name = "Gift Choice")]
        public int GiftId { get; set; }

    }
}

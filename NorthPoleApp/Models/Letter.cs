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
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter country")]
        [MaxLength(40)]
        public string Country { get; set; }

        [MaxLength(200)]
        public string Note { get; set; }

        public bool IsGood { get; set; }

        public int GiftId { get; set; }

    }
}

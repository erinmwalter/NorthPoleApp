using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NorthPoleApp.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Task Name Required")]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public Status CurrentStatus { get; set; } = Status.New;

        public int GiftId { get; set; }

        public int LetterId { get; set; }

        public List<Employee> Employees { get; set; }

    }

    public enum Status 
    { 
        New,
        Production,
        Quality,
        Wrapping,
        OnSleigh,
        Delivered
    }
}

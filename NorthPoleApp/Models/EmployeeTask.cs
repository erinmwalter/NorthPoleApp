using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NorthPoleApp.Models
{
    public class EmployeeTask
    {
        [Key]
        public int Id { get; set; }

        public int EmployeeID { get; set; }

        public int TaskId { get; set; }

        public Employee Employee { get; set; }
        public Task Task { get; set; }
    }
}

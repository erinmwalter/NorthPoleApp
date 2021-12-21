using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NorthPoleApp.Models
{
    public class Gift
    {
        [Key]
        public int GiftId { get; set; }

        public string GiftName { get; set; }
    }
}

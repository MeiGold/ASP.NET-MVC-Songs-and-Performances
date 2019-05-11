using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Songs_and_Performances.Models
{
    public class Performer
    {
        public int ID { get; set; }
        [Required]
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }

        public ICollection<Performance> Performances { get; set; }
    }
}

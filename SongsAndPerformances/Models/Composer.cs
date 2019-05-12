using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAndPerformances.Models
{
    public class Composer
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Full Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        public string FullName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Nationality")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s-]*$")]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        
    }
}

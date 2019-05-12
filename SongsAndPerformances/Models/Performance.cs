using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Songs_and_Performances.Models
{
    public class Performance
    {
        public int ID { get; set; }
        public int SongID { get; set; }
        public int PerformerID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Performance Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s-]*$")]
        public string Place { get; set; }

        public Song Song { get; set; }
        public Performer Performer { get; set; }
    }
}

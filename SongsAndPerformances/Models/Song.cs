using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Songs_and_Performances.Models
{
    public class Song
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Song Name")]
        [RegularExpression(@"^[A-Z0-9]+[0-9a-zA-Z'\s-]*$")]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Genre")]
        [RegularExpression(@"^[A-Z]+[a-z'\s-]*$")]
        public string Genre { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        public ICollection<Performance> Performances { get; set; }
        public ICollection<SongsAndPerformances.Models.Composer> Composers { get; set; }
    }
}

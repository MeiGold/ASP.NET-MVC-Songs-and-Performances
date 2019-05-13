using Songs_and_Performances.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAndPerformances.Models
{
    public class ComposerSong
    {
        public int ID { get; set; }

        [Display(Name ="Song name")]
        public int SongID { get; set; }
        [Display(Name ="Composer Full Name")]
        public int ComposerID { get; set; }

        public Song Song { get; set; }
        public Composer Composer { get; set; }
    }
}

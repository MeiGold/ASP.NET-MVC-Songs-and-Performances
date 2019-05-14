using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAndPerformances.Models
{
    public enum TypeOfUser
    {
        Creator=1,
        Admin=2,
        User=3
    }
    public class User
    {
        public int ID { get; set; }

        
        [Required]
        [StringLength(20)]
        [Display(Name = "Your nickname")]
        [RegularExpression(@"^[A-Z0-9]+[0-9a-zA-Z'\s-]*$")]
        public string Nickname { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Your password")]
        [RegularExpression(@"^[0-9a-zA-Z]*$")]
        public string Password { get; set; }

        [Required]
        public TypeOfUser Type { get; set; }
    }
}

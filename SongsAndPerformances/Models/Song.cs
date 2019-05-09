using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Songs_and_Performances.Models
{
    public class Song
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public TimeSpan Duration { get; set; }

        public ICollection<Performance> Performances { get; set; }
    }
}

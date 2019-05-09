using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Songs_and_Performances.Models
{
    public class Performance
    {
        public int ID { get; set; }
        public int SongID { get; set; }
        public int PerformerID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }

        public Song Song { get; set; }
        public Performer Performer { get; set; }
    }
}

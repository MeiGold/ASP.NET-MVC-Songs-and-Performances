using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Songs_and_Performances.Models
{
    public class Performer
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }

        public ICollection<Performance> Performances { get; set; }
    }
}

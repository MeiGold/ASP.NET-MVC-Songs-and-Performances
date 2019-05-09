using Songs_and_Performances.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Songs_and_Performances.Data
{
    public class DbInitializer
    {
        public static void Initialize(Database database)
        {
            database.Database.EnsureCreated();

            // Look for any students.
            if (database.Songs.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Song[]
            {
            new Song{Name="E rock",Genre="Rock",Duration=(TimeSpan.MinValue)},
            
            };
            foreach (Song s in students)
            {
                database.Songs.Add(s);
            }
            database.SaveChanges();

            var courses = new Performer[]
            {
           
            };
            foreach (Performer c in courses)
            {
                database.Performers.Add(c);
            }
            database.SaveChanges();

            var enrollments = new Performance[]
            {
            
            };
            foreach (Performance e in enrollments)
            {
                database.Performances.Add(e);
            }
            database.SaveChanges();
        }
    
}
}

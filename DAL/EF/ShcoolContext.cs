using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Models;

namespace DAL
{
    public class SchoolContext : DbContext
    {
        public SchoolContext() : base("DefaultConnection")
        { }
        static SchoolContext()
        {
            //Database.SetInitializer(new SchoolDBInitializer());
        }
        public DbSet<School> Schools { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Pupil> Pupils { get; set; }
        public DbSet<Subject> Subjects { get; set; }

    }
}

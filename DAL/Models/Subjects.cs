using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Pupil> Pupils { get; set; }

        public bool Valid { get; set; }

        public Subject()
        {
            Date = DateTime.Now;
            Teachers = new List<Teacher>();
            Pupils = new List<Pupil>();
            Valid = true;
        }
    }
}

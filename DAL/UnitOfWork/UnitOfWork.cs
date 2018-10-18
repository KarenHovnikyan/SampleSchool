using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IDisposable
    {
        private SchoolContext db = new SchoolContext();
        private Repository<School> schoolRepository;
        private Repository<Teacher> teacherRepository;
        private Repository<Pupil> pupilRepository;
        private Repository<Subject> subjectRepository;

        public IRepository<School> SchoolRepository
        {
            get
            {
                if (schoolRepository == null)
                {
                    schoolRepository = new Repository<School>(db);
                }
                return schoolRepository;
            }
        }

        public IRepository<Teacher> TeacherRepository
        {
            get
            {
                if (teacherRepository == null)
                {
                    teacherRepository = new Repository<Teacher>(db);
                }
                return teacherRepository;
            }
        }
        public IRepository<Pupil> PupilRepository
        {
            get
            {
                if (pupilRepository == null)
                {
                    pupilRepository = new Repository<Pupil>(db);
                }
                return pupilRepository;
            }
        }
        public IRepository<Subject> SubjectRepository
        {
            get
            {
                if (subjectRepository == null)
                {
                    subjectRepository = new Repository<Subject>(db);
                }
                return subjectRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

using BasicDotNetServices.DAL.Class;
using BasicDotNetServices.DAL.Data;
using BasicDotNetServices.DAL.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDotNetServices.DAL.Repository
{
    /// <summary>
    /// This will do all the database Operations rather than individual repository
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IContactRepository Contact { get;private set; }

        public IInstitutionRepository Institution { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Contact = new ContactsRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

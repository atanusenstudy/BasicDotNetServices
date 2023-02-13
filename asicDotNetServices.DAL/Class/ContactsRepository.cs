using BasicDotNetServices.Core.Model;
using BasicDotNetServices.DAL.Data;
using BasicDotNetServices.DAL.Repository;
using BasicDotNetServices.DAL.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasicDotNetServices.DAL.Class
{
    // We are implementing IContactRepository but it will show error because
    // we have implemented this inside Repository.
    // Thats why whe are inheriting Repository<Contact>
    public class ContactsRepository : Repository<Contact>, IContactRepository
    {
        private ApplicationDbContext _db;
        // We nee to pass ApplicationDbContext because
        // Repository<T> want's this
        public ContactsRepository(ApplicationDbContext db ):base(db) 
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Contact contact)
        {
            _db.Contacts.Update( contact );
        }
    }
}

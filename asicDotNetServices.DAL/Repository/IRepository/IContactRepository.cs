using BasicDotNetServices.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDotNetServices.DAL.Repository.IRepository
{
    /// <summary>
    /// Anything Which is implemented according to requirement of 
    /// specific class will be implemented here
    /// </summary>
    public interface IContactRepository : IRepository<Contact>
    {
        void Update(Contact contact);
        void Save();
    }
}

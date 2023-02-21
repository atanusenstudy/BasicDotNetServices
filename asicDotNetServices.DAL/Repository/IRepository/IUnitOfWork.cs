using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDotNetServices.DAL.Repository.IRepository
{
    /// <summary>
    /// This will do all the database Operations rather than individual repository
    /// </summary>
    public interface IUnitOfWork
    {
        IContactRepository Contact { get; }
        IInstitutionRepository Institution { get; }
        IUserRepository User { get; }
        void Save();
    }
}

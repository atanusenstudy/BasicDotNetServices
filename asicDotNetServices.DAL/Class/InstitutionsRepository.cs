using BasicDotNetServices.Core.Model;
using BasicDotNetServices.DAL.Data;
using BasicDotNetServices.DAL.Repository;
using BasicDotNetServices.DAL.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDotNetServices.DAL.Class
{
    public class InstitutionsRepository : Repository<Institution>, IInstitutionRepository
    {
        private ApplicationDbContext _db;
        public InstitutionsRepository(ApplicationDbContext db) : base(db)
        {
            _db= db;
        }
    
    }
}

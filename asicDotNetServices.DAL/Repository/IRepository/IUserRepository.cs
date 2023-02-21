using BasicDotNetServices.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDotNetServices.DAL.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        public void Update(User user);
        public User Authenticate(AuthorizeModel data);
    }
}

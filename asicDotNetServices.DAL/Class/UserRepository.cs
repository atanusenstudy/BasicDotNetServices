using BasicDotNetServices.Core.Model;
using BasicDotNetServices.DAL.Data;
using BasicDotNetServices.DAL.Repository.IRepository;
using BasicDotNetServices.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BasicDotNetServices.DAL.Class
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private ApplicationDbContext _db;
        // We nee to pass ApplicationDbContext because
        // Repository<T> want's this
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public void Update(User user)
        {
            _db.Users.Update(user);
        }
        
        public User Authenticate( AuthorizeModel data)
        {
            /*
             {
  "email": "atanusen@gmail.com",
  "password": "admin123"
}
            */
            User user = null;
            try
            {
                string connectionString = "Data Source=DESKTOP-C0LDQUI\\SQLEXPRESS;Initial Catalog=BasicDotNetServicesDatabase2;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "getUserByEmailAndPassword";
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter para1 = new SqlParameter
                    {
                        ParameterName = "email",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 200,
                        Value = data.Email,
                        Direction = System.Data.ParameterDirection.Input,
                    };
                    SqlParameter para2 = new SqlParameter
                    {
                        ParameterName = "password",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 200,
                        Value = data.Password,
                        Direction = System.Data.ParameterDirection.Input,
                    };
                    cmd.Parameters.Add(para1);
                    cmd.Parameters.Add(para2);

                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        user = new User();
                        user.Id = rd.GetInt32(0);
                        user.FirstName = rd.GetString(1);
                        user.LastName = rd.GetString(2);
                        user.Email = rd.GetString(3);
                        user.Password = rd.GetString(4);
                        user.Phone = rd.GetInt64(5);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

    }
}

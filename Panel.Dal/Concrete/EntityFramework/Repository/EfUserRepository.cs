using Panel.Dal.Abstract;
using Panel.Dal.Concrete.EntityFramework.Context;
using Panel.Entities.Models;
using Panel.Entities.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panel.Dal.Concrete.EntityFramework.Repository
{
    public class EfUserRepository : EfGenericRepository<User>, IUserRepository
    {
        public EfUserRepository() : base()
        {

        }




        public List<User> ListUsers(int UserReferenceID)
        {
            return context.User.Where(x => x.UserReferenceID == UserReferenceID).ToList();
        }







    }
}

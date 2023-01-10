using Panel.Entities.Models;
using Panel.Entities.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panel.Dal.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {
        List<User> ListUsers(int UserReferenceID);




    }
}



using Panel.Entities.Models;
using Panel.Entities.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panel.Interfaces
{
    public interface IUserService : IGenericService<User>
    {

        List<User> ListUsers(int notGrubuId);

    }
}

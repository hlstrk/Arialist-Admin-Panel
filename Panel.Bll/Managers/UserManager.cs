
using Panel.Dal.Abstract;
using Panel.Entities.Models;
using Panel.Entities.PocoModels;
using Panel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panel.Bll
{
    public class UserManager : GenericManager<User>, IUserService
    {
        IUserRepository UserRepository;

        public UserManager(IUserRepository UserRepository) : base(UserRepository)
        {
            this.UserRepository = UserRepository;
        }



        public List<User> ListUsers(int UserGrubuId)
        {
            return UserRepository.ListUsers(UserGrubuId);
        }




    }
}

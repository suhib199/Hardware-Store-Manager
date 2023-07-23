using HardwareStoreRepository.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.Repo.User
{
    public class UserRepository:IUserRepository
    {
        public readonly HardwareStoreDBContext _HardwareStoreDBContext;
        public UserRepository (HardwareStoreDBContext HardwareStoreDBContext)
        {
            _HardwareStoreDBContext = HardwareStoreDBContext;
        }

        public Models.User Create (Models.User user)
        {
            _HardwareStoreDBContext.Users.Add(user);

            user.Id = _HardwareStoreDBContext.SaveChanges();
            return user;
        }
        
        public Models.User GetByEmail(string email)
        {
            return _HardwareStoreDBContext.Users.FirstOrDefault (user =>user.Email ==email);
        }  
        public Models.User GetById(int Id)
        {
            return _HardwareStoreDBContext.Users.FirstOrDefault (user =>user.Id ==Id);
        }


    }
}

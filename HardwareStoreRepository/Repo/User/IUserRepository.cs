using HardwareStoreRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.Repo.User
{
    public interface IUserRepository
    {
      Models.User Create (Models.User user);
      Models.User GetByEmail (string email);
      Models.User GetById (int id);
    }
}

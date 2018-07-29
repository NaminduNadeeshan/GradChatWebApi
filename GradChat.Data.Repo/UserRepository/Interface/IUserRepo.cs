using GradChat.Data.Entity;
using GradChat.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradChat.Data.Repo.UserRepository.Interface
{
    public interface IUserRepo
    {
      User Authenticate(string userName, string password);

     IQueryable<User> GetById(int Id);

      User Create(User user, string password);

      User Update(User user, string password = null);

    }
}

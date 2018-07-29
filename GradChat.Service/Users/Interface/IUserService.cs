using GradChat.Data.Entity;
using GradChat.DtoModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradChat.Service.Interface
{
    public interface IUserService
    {
       UserDtoResponse Authenticate(string userName, string password);

       void Register(UserDto user);

      UserDtoResponse UpdateUser(UserDto user);

      UserDto GetById(int Id);

       
    }
}

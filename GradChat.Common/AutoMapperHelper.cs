using AutoMapper;
using GradChat.Data.Entity;
using GradChat.DtoModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradChat.Common
{
    public class AutoMapperHelper: Profile
    {
      public AutoMapperHelper()
      {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
      }
    }
}

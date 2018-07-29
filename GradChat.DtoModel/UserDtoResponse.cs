using System;
using System.Collections.Generic;
using System.Text;

namespace GradChat.DtoModel
{
    public class UserDtoResponse
    {
      public int Id { get; set; }

      public string UserName { get; set; }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string token { get; set; }
  }
}

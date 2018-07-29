using GradChat.Common;
using GradChat.Data.Entity;
using GradChat.Data.Repo.UserRepository.Interface;
using GradChat.DtoModel;
using GradChat.Service.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GradChat.Service.Users
{
  public class UserService : IUserService
  {
    private IUserRepo _userRepo;
    private readonly AppSettingsHelper _appSetiingHelper;

    public UserService(IUserRepo userRepo, AppSettingsHelper appSettingsHelper)
    {
      _userRepo = userRepo;
      _appSetiingHelper = appSettingsHelper;
    }

    public UserDtoResponse Authenticate(string userName, string password)
    {
      var user = _userRepo.Authenticate(userName, password);

      if (user == null)
        throw new Exception($"usename: {userName} or password not existing");

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes("!@#$U$U#I$$((($($*&#&##&#&#&DHHDDDJHBMFDJKHDHDJKKJDHKJHDKLJLWLJLJW)");
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
          }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      // return basic user info (without password) and token to store client side
      var userResponse = new UserDtoResponse
      {
        Id = user.Id,
        UserName = user.UserName,
        FirstName = user.FirstName,
        LastName = user.LastName,
        token = tokenString
      };

      return userResponse;
    }

    public void Register(UserDto user)
    {
      // map dto to entity
      var users = new User
      {
        UserName = user.UserName,
        FirstName = user.FristName,
        LastName = user.LastName,

      };

      try
      {
        // save 
        _userRepo.Create(users, user.password);
      }
      catch (Exception ex)
      {
        // return error message if there was an exception
        throw new Exception("user not created!");
      }
    }

    public UserDtoResponse UpdateUser(UserDto userDto)
    {
      // map userDto with User model

      var user = new User
      {
        Id = userDto.Id,
        FirstName = userDto.FristName,
        LastName = userDto.LastName,
        UserName = userDto.UserName

      };
      var userRepoResponse = _userRepo.Update(user, userDto.password);

      var retunedUser = new UserDtoResponse
      {
        Id = userRepoResponse.Id,
        FirstName = userRepoResponse.FirstName,
        LastName = userRepoResponse.LastName,
        UserName = userRepoResponse.UserName
      };

      return retunedUser;

    }

    public UserDto GetById(int Id)
    {
      var user = (_userRepo.GetById(Id)).ToList();

      var userDto = new UserDto
      {
        FristName = user[0].FirstName,
        LastName = user[0].LastName,
        Id = user[0].Id,
        UserName = user[0].UserName
      };

      return userDto;
    }
  }
}

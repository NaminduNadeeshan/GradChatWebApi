using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradChat.DtoModel;
using GradChat.Service.Interface;
using GradChat.Service.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradChat.Controllers.UserController
{
  [Produces("application/json")]
  [Route("api/User")]
  public class UserController : Controller
  {
    private IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpPost("authenticate")]
    public IActionResult Authencate([FromBody]UserDto user)
    {
      return Ok(_userService.Authenticate(user.UserName, user.password));
    }

    [HttpPost("create")]
    public IActionResult createUser([FromBody]UserDto user)
    {
      try
      {
        _userService.Register(user);
        return Ok("user created");
      }
      catch (Exception e)
      {
        return BadRequest($"user created fail, Exception throw {e}");
      }
    }

    [Authorize]
    [HttpPost("update")]
    public IActionResult updateUser([FromBody]UserDto user)
    {
      try
      {
        _userService.UpdateUser(user);

        return Ok("User has been update sucusessfull");
      }
      catch (Exception e)
      {
        return BadRequest($"user update fail {e}");
      }
    }

    [Authorize]
    [HttpGet("{Id}")]
    public IActionResult GetById(int Id)
    {
      return Ok(_userService.GetById(Id));
    }
  }
}

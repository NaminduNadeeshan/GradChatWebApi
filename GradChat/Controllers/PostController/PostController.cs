using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradChat.DtoModel;
using GradChat.Service.PostService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradChat.Controllers.PostController
{
    [Produces("application/json")]
    [Route("api/Post")]
    [Authorize]
    public class PostController : Controller
    {
    private IPostService _postService;

      public PostController(IPostService postService)
      {
        _postService = postService;
      }

    [HttpGet("GetAllPost")]
    public IActionResult GetAllPost()
    {
      return Ok(_postService.GetAllPost());
    }

    [HttpPost("update")]
    public IActionResult UpdatePost([FromBody]RequestSinglePost post)
    {
      return Ok(_postService.UpdatePost(post));
    }

    [HttpPost("create")]
    public IActionResult CreatePost([FromBody]RequestSinglePost post)
    {
      return Ok(_postService.AddPost(post));
    }

    [HttpGet("Getpost/{Id}")]
    public IActionResult GetPostByID(int Id)
    {
      return Ok(_postService.GetPostById(Id));
    }

    [HttpGet("{id}/user")]
    public IActionResult GetPostByUserId(int Id)
    {
      return Ok(_postService.GetPostByUserId(Id));
    }

    }
}

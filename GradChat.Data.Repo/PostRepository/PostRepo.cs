using GradChat.Data.Entity;
using GradChat.Data.Entity.Post;
using GradChat.Data.Repo.PostRepository.Interface;
using GradChat.DtoModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradChat.Data.Repo.PostRepository
{
  public class PostRepo : IPostRepo
  {
    private GradChatDbContext _context;

    public PostRepo(GradChatDbContext context)
    {
      _context = context;

    }

    public Posts AddPost(Posts post)
    {
       try
      {
        _context.Posts.Add(post);
        _context.SaveChanges();

        return post;

      } catch(Exception e)
      {
        throw new Exception($"Post Not Added. {e}");
      }
    
    }

    public List<Posts> GetAll()
    {
       var post = _context.Posts.ToList();

      return post;
    }

    public Posts UpdatePost(Posts post)
    {
      try
      {
        _context.Update(post);
        _context.SaveChanges();

        return post;

      }catch(Exception e)
      {
        throw new Exception($"Update faild, exception trow: {e}");
      }
    }

    public ResponseSinglePost GetPostById(int Id)
    {
      var postList = _context.Posts.Where(post => post.Id == Id)
       .Include(user=> user.User).ToList();

      var responsePost = new ResponseSinglePost
      {

        PostId = postList[0].Id,
        AbstractContent = postList[0].AbstractContent,
        Content = postList[0].Content,
        CoverPhoto = postList[0].CoverPhoto,
        FirstName = postList[0].User.FirstName,
        LastName = postList[0].User.LastName,
        Title = postList[0].Title
      };

      return responsePost;
    }

    public List<Posts> GetPostByUserId(int Id)
    {
      var postList = _context.Posts.Where(post => post.UserId == Id).ToList();

      return postList;
    }
  }
}

using GradChat.Data.Entity.Post;
using GradChat.DtoModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GradChat.Service.PostService.Interface
{
    public interface IPostService
    {
    ResponseSinglePost AddPost(RequestSinglePost post);

    List <Posts> GetAllPost();

    ResponseSinglePost UpdatePost(RequestSinglePost post);

    ResponseSinglePost GetPostById(int Id);

    List<Posts> GetPostByUserId(int Id);

  }
}

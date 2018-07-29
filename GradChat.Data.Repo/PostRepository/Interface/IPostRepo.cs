using GradChat.Data.Entity.Post;
using GradChat.DtoModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GradChat.Data.Repo.PostRepository.Interface
{
    public interface IPostRepo
  {
    Posts AddPost(Posts post);

    List<Posts> GetAll();

    ResponseSinglePost GetPostById(int Id);

    Posts UpdatePost(Posts post);

    List<Posts> GetPostByUserId(int Id);
  }
}

using GradChat.Data.Entity.Attachments;
using GradChat.Data.Entity.Post;
using GradChat.Data.Repo.PostRepository;
using GradChat.Data.Repo.PostRepository.Interface;
using GradChat.DtoModel;
using GradChat.Service.PostService.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GradChat.Service.PostService
{
  public class PostService : IPostService
  {
    private IPostRepo _postRepo;

    public PostService(IPostRepo postRepo)
    {
      _postRepo = postRepo;
    }

    public ResponseSinglePost AddPost(RequestSinglePost post)
    {
      // map to Pots Model
      var postPass = new Posts
      {
        Title = post.Title,
        AbstractContent = post.AbstractContent,
        Content = post.Content,
        CoverPhoto = post.CoverPhoto,
        UserId = post.UserId,
      };

      var postResponse = _postRepo.AddPost(postPass);


      // map to ResponseSinglePost
      var singlePostResponse = new ResponseSinglePost
      {
        PostId = postResponse.Id,
        Title = postResponse.Title,
        AbstractContent = postResponse.AbstractContent,
        Content = postResponse.Content,
        CoverPhoto = postResponse.CoverPhoto
      };

      return singlePostResponse;
    }

    public List<Posts> GetAllPost()
    {
      return _postRepo.GetAll();
    }

    public ResponseSinglePost UpdatePost(RequestSinglePost post)
    {

      // map RequestSinglPost to post
      var updatePost = new Posts
      {
        Id = post.PostId,
        Title = post.Title,
        AbstractContent = post.AbstractContent,
        Content = post.Content,
        UserId = post.UserId
      };

      var responsePost = _postRepo.UpdatePost(updatePost);

      // map to responseSinglePost
      var mapResponse = new ResponseSinglePost
      {
        PostId = responsePost.Id,
        Title = responsePost.Title,
        AbstractContent = responsePost.AbstractContent,
        Content = responsePost.Content,
        CoverPhoto = responsePost.CoverPhoto

      };

      return mapResponse;

    }

    public ResponseSinglePost GetPostById(int Id)
    {
      return _postRepo.GetPostById(Id);
    }
    public List<Posts> GetPostByUserId(int Id)
    {
      return _postRepo.GetPostByUserId(Id);
    }

  }
}

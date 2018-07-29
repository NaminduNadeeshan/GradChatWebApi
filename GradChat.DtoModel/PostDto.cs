using GradChat.Data.Entity;
using GradChat.Data.Entity.Attachments;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradChat.DtoModel
{
    public class ResponseSinglePost
  {
    public int PostId { get; set; }

    public string Title { get; set; }

    public string AbstractContent { get; set; }

    public string Content { get; set; }

    public string CoverPhoto { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
  }

  public class RequestSinglePost
  {
    public int PostId { get; set; }

    public string Title { get; set; }

    public string AbstractContent { get; set; }

    public string Content { get; set; }

    public string CoverPhoto { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int UserId { get; set; }

  }

}

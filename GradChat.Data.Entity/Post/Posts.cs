using GradChat.Data.Entity.Attachments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GradChat.Data.Entity.Post
{
    public class Posts
  {
 
    public int Id { get; set; }

    public string Title { get; set; }

    public string AbstractContent { get; set; }

    public string Content { get; set; }

    public string CoverPhoto { get; set; }

    public virtual User User { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Attachment> Attachment { get; set; }

  }
}

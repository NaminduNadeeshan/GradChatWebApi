using GradChat.Data.Entity.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradChat.Data.Entity.Attachments
{
  public class Attachment
  {
    public int Id { get; set; }

    public string FileName { get; set; }

    public string FileTye { get; set; }

    public virtual Posts Posts { get; set; }

  }
}

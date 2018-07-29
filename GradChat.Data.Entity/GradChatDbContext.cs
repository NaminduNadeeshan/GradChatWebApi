using GradChat.Data.Entity.Attachments;
using GradChat.Data.Entity.Post;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradChat.Data.Entity
{
  public class GradChatDbContext : DbContext
  {
    public GradChatDbContext(DbContextOptions<GradChatDbContext> options): base(options)
    {

    }

    public DbSet<User> User { get; set; }
    public DbSet<Posts> Posts { get; set; }
    public DbSet<Attachment> Attachment { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) //TODO refactor - break into few functions , set pk ,set fk ....
    {


      modelBuilder.Entity<Posts>()
          .HasOne(p => p.User)
          .WithMany(c => c.Posts)
          .HasForeignKey(p => p.UserId);


      modelBuilder.Entity<Attachment>()
        .HasOne(p => p.Posts)
        .WithMany(c => c.Attachment);

    }

  }
}

﻿// <auto-generated />
using GradChat.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace GradChat.Data.Entity.Migrations
{
    [DbContext(typeof(GradChatDbContext))]
    [Migration("20180728174033_add coverphoto")]
    partial class addcoverphoto
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GradChat.Data.Entity.Attachments.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName");

                    b.Property<string>("FileTye");

                    b.Property<int?>("PostsId");

                    b.HasKey("Id");

                    b.HasIndex("PostsId");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("GradChat.Data.Entity.Post.Posts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<int>("CoverPhoto");

                    b.Property<string>("Title");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("GradChat.Data.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("GradChat.Data.Entity.Attachments.Attachment", b =>
                {
                    b.HasOne("GradChat.Data.Entity.Post.Posts", "Posts")
                        .WithMany("Attachment")
                        .HasForeignKey("PostsId");
                });

            modelBuilder.Entity("GradChat.Data.Entity.Post.Posts", b =>
                {
                    b.HasOne("GradChat.Data.Entity.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

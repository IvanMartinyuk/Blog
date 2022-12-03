using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Context
{
    public class BlogContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Subscribers> Subscribers { get; set; }
        public DbSet<News> News { get; set; }
        public BlogContext(DbContextOptions<BlogContext> option) : base(option)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscribers>()
                .HasKey(o => new { o.PublisherId, o.SubscriberId });

            modelBuilder.Entity<Subscribers>()
                              .HasOne(sub => sub.Publisher)
                              .WithMany(user => user.Subscribers) // <--
                              .HasForeignKey(sub => sub.PublisherId)
                              .OnDelete(DeleteBehavior.Restrict); // see the note at the end

            modelBuilder.Entity<Subscribers>()
                .HasOne(sub => sub.Subscriber)
                .WithMany(user => user.Publishers)
                .HasForeignKey(sub => sub.SubscriberId);

            modelBuilder.Entity<News>()
                .HasKey(o => new { o.UserId, o.ArticleId });

            modelBuilder.Entity<News>()
                              .HasOne(sub => sub.Article)
                              .WithMany(user => user.ArticlesUsers) // <--
                              .HasForeignKey(sub => sub.ArticleId)
                              .OnDelete(DeleteBehavior.Restrict); // see the note at the end

            modelBuilder.Entity<News>()
                .HasOne(sub => sub.User)
                .WithMany(user => user.ArticlesUsers)
                .HasForeignKey(sub => sub.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

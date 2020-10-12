
using GitHubApi.Helpers;
using GitHubApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GitHubApi
{
    public class GitHubApiDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Commit> Commits { get; set; }
        public DbSet<Repository> Repositories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@$"Data Source= {DirectoryHelper.GetProjectDirectory()}\commits.db");
        }

        public GitHubApiDbContext()
        {
           
        }
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Repository>().HasIndex(x => x.Name).IsUnique();
        }

       
    }
}

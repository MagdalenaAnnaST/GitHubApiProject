using GitHubApi.Dto;
using GitHubApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubApi.DataLogic
{
    public class DataStore
    {
        private GitHubApiDbContext dbContext;
        public DataStore(GitHubApiDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbContext.Database.EnsureCreated();
        }

        public async Task<Repository> AddRepositoryAsync(string repositoryName)
        {
            var repo = dbContext.Repositories?.Where(x => x.Name == repositoryName).FirstOrDefault();
            if( repo == null)
            {
                repo = new Models.Repository() { Name = repositoryName };
                await dbContext.Repositories.AddAsync(repo);
                await dbContext.SaveChangesAsync(); 
            }
            return repo;
        }

        public async Task<User> AddUserAsync(string userName)
        {
            var user = dbContext.Users.Where(x => x.Name == userName).FirstOrDefault();
            if (user == null)
            {
                user = new Models.User() { Name = userName };
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            };
            return user;
        }

        public async Task<int> AddCommitAsync(CommitDto commit, User user, Repository repo )
        {
            var commiter = dbContext.Users.Where(x => x.Name == commit.CommiterName).FirstOrDefault();
            if (commiter == null)
            {
                commiter = new Models.User() { Name = commit.CommiterName };
                await dbContext.Users.AddAsync(commiter);
            };

            var comm = dbContext.Commits?.Where(x => x.Sha == commit.Sha).FirstOrDefault();
            if (comm == null)
            {
                comm = new Models.Commit() { Sha = commit.Sha, Commiter = commiter, Repository = repo, User = user, Message = commit.Message };
                await dbContext.Commits.AddAsync(comm);
            }
            else 
            {
                comm.Commiter = commiter;
                comm.Repository = repo;
                comm.User = user;
                comm.Message = commit.Message;
                dbContext.Commits.Update(comm);
            }

            return await dbContext.SaveChangesAsync();
        }
    }
}

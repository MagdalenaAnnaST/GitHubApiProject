using GitHubApi.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GitHubApi.DataLogic
{
    public class GitHubApiHelper
    {
        private static readonly HttpClient HttpClient;

        static GitHubApiHelper()
        {
            HttpClient = new HttpClient();
        }
        public async Task<IEnumerable<CommitDto>> GetRepositoryCommitsAsync(string repositoryName, string userName)
        {

            HttpClient.BaseAddress = new Uri("https://api.github.com");
            HttpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("GitHubApi.Console", "1.0"));
            HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = await HttpClient.GetAsync($"repos/{userName}/{repositoryName}/commits?page=1&per_page=100");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var commits = JArray.Parse(data);
                return GetCommits(commits, repositoryName, userName);
            }
            else
            {
                throw new HttpRequestException($"An error \"{response.StatusCode.ToString()}\" occurred during performing operation.");
            }
        }
        
        private IEnumerable<CommitDto> GetCommits(JArray commits, string repositoryName, string userName)
        {
            foreach (var commit in commits)
            {
                var commiterName = commit["commit"]?["committer"]?.Value<string>("name");
                var message = commit["commit"]?.Value<string>("message");
                var sha = commit?.Value<string>("sha");

                if (!String.IsNullOrEmpty(commiterName) && !string.IsNullOrEmpty(sha) && !string.IsNullOrEmpty(message))
                {
                    yield return new CommitDto() { CommiterName = commiterName, Message = message, Sha = sha, User = userName, Repository = repositoryName};
                }
            }
        }
    }
}

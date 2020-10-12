using System;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubApi.DataLogic;
namespace GitHubApi.Main
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            Console.WriteLine("Please enter user name:");
            var userName = Console.ReadLine();
            Console.WriteLine("Please enter repository name:");
            var repositoryName = Console.ReadLine();

            try 
            {

                GitHubApiHelper apiHelper = new GitHubApiHelper();
                var commits = await apiHelper.GetRepositoryCommitsAsync(repositoryName, userName);

                using (var context = new GitHubApiDbContext())
                {
                    DataStore dataStore = new DataStore(context);
                    DataDisplay dataDisplay = new DataDisplay();

                    var repository = await dataStore.AddRepositoryAsync(repositoryName);
                    var user = await dataStore.AddUserAsync(userName);

                    Console.WriteLine($"{Environment.NewLine}COMMITS:{Environment.NewLine}");

                    foreach (var commit in commits)
                    {
                        await dataStore.AddCommitAsync(commit, user, repository);

                        Console.WriteLine( dataDisplay.GetCommitDescription(commit));
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error occured during performing opeartion: {ex.Message}");
            }
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }
    }
}

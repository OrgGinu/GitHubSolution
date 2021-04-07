using Octokit;
using System;

namespace GitHubSolution.Services
{
    public class GitHubClientHelper
    {
        private readonly string _repoName;
        private readonly string _token;
        public GitHubClientHelper(string repoName, string token)
        {
            _repoName = repoName;
            _token = token;
        }
        public GitHubClient GetGitHubClient()
        {
            try
            {
                var githubClient = new GitHubClient(new ProductHeaderValue(_repoName))
                {
                    Credentials = new Credentials(_token)
                };
                return githubClient;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using GitHubSolution.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Octokit;
using System;
using System.Threading.Tasks;

namespace GitHubSolution.Services
{
    public class IssueServices : IIssueServices
    {
        private readonly IConfiguration _config;
        private readonly GitHubClient _githubClient;
        public IssueServices(IConfiguration config)
        {
            _config = config;
            _githubClient = new GitHubClientHelper(_config["GitHubSetting:RepoName"], _config["GitHubSetting:Token"]).GetGitHubClient();
        }

        public async Task<bool> CreateIssue(string owner, string repo, string title, string body)
        {
            try
            {
                var issue = new NewIssue(title) { Body = body };
                var issueCreated = await _githubClient.Issue.Create(owner, repo, issue);
                return issueCreated.Number > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

    }
}

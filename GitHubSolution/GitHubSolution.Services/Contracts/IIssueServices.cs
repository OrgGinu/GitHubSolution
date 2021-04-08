using System;
using System.Threading.Tasks;

namespace GitHubSolution.Services.Contracts
{
    public interface IIssueServices
    {
        public Task<bool> CreateIssue(string owner, string repo, string title, string body);
    }
}

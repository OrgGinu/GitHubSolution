using GitHubSolution.Services.ViewModels.Responses;
using System.Threading.Tasks;

namespace GitHubSolution.Services.Contracts
{
    public interface IBranchServices
    {
        /// <summary>
        /// Protect a branch
        /// </summary>
        /// <param name="branchName"></param>
        /// <returns>success status and message</returns>
        public Task<ProtectBranchResponse> ProtectBranch(string branchName, string owner, string repoName);

        /// <summary>
        /// Get default branch for the repo
        /// </summary>
        /// <returns>default branch name for the repo</returns>
        public Task<string> GetDefaultBranch(string owner, string repoName);
    }
}

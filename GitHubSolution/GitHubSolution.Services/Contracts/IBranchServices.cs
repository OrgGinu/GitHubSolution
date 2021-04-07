using GitHubSolution.Services.ViewModels.Responses;
using System.Threading.Tasks;

namespace GitHubSolution.Services.Contracts
{
    public interface IBranchServices
    {
        public Task<ProtectBranchResponse> ProtectBranch(string branchName);
    }
}

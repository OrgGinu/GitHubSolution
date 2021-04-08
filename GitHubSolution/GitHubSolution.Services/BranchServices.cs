using GitHubSolution.Services.Contracts;
using GitHubSolution.Services.ViewModels.Responses;
using Microsoft.Extensions.Configuration;
using Octokit;
using System;
using System.Threading.Tasks;

namespace GitHubSolution.Services
{
    public class BranchServices : IBranchServices
    {
        private readonly IConfiguration _config;
        private readonly GitHubClient _githubClient;
        public BranchServices(IConfiguration config)
        {
            _config = config;
            _githubClient = new GitHubClientHelper(_config["GitHubSetting:RepoName"], _config["GitHubSetting:Token"]).GetGitHubClient();
        }
        public async Task<ProtectBranchResponse> ProtectBranch(string branchName, string owner, string repoName)
        {
            var responseModel = new ProtectBranchResponse();
            try
            {
                //Apply the branch protections below
                var update = new BranchProtectionSettingsUpdate(
                    new BranchProtectionRequiredStatusChecksUpdate(true, new[] { "build", "test" }),
                        new BranchProtectionRequiredReviewsUpdate(false, true, 2), true);

                var updateBranchResponse = await _githubClient.Repository.Branch.UpdateBranchProtection(owner, repoName, branchName, update);

                responseModel.IsSuccess = true;
                responseModel.Message = "Branch protection applied succesfully";
                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = ex.Message;
                return responseModel;
            }

        }

        public async Task<string> GetDefaultBranch(string owner, string repoName)
        {
            try
            {
                var repositoryDetails = await _githubClient.Repository.Get(owner,
                    repoName);
                return repositoryDetails.DefaultBranch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

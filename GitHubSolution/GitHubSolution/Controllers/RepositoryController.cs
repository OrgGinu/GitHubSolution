using GitHubSolution.Services.Contracts;
using GitHubSolution.Services.ViewModels.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace GitHubSolution.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RepositoryController : ControllerBase
    {
        private const string issueText = "@ginuraju Protections applied - Require pull request reviews before merging,Require built, test status checks to pass before merging";
        private readonly IBranchServices _branchServices;
        private readonly IIssueServices _issueServices;
        private readonly IWebHookServices _webHookServices;
        private readonly ILogger<RepositoryController> _logger;
        public RepositoryController(IBranchServices branchServices, IIssueServices issueServices, IWebHookServices webHookServices,
            ILogger<RepositoryController> logger)
        {
            _logger = logger;
            _branchServices = branchServices;
            _issueServices = issueServices;
            _webHookServices = webHookServices;
        }

        /// <summary>
        /// API end point that listens to Organization created event
        /// </summary>
        /// <returns>status of branch protection update + any other actions that needs to be performed when a repo is created</returns>
        [HttpPost]
        public async Task<IActionResult> Created([FromBody] JsonElement body)
        {
            try
            {

                Request.Headers.TryGetValue("X-Hub-Signature", out StringValues signature);

                if (string.IsNullOrEmpty(signature))
                {
                    return Unauthorized();
                }
                var payload = JsonSerializer.Deserialize<PayLoad>(body.GetRawText());
                if (payload.Equals(null))
                {
                    return BadRequest();
                }
                var isvalidGitHubPushEvent = _webHookServices.IsValidGitHubWebHookRequest(body.GetRawText(), signature);
                if (!isvalidGitHubPushEvent)
                {
                    return Unauthorized();
                }

                //Get the default branch for the repository
                var defaultBranch = await _branchServices.GetDefaultBranch(payload.repository.owner.login, payload.repository.name);

                //Protect the default repository
                var protectBranchResponse = await _branchServices.ProtectBranch(defaultBranch, payload.repository.owner.login, payload.repository.name);

                bool res = await _issueServices.CreateIssue(payload.repository.owner.login, payload.repository.name, "Protection applied to the default branch " + defaultBranch + " of the new repository "
                    + payload.repository.name, issueText);
                return Ok(protectBranchResponse);
            }
            catch (Exception ex)
            {
                //Log the exception
                _logger.Log(LogLevel.Error, ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}

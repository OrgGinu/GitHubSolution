using GitHubSolution.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GitHubSolution.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationController : ControllerBase
    {
        private IBranchServices _branchRepository;
        public OrganizationController(IBranchServices branchRepository)
        {
            _branchRepository = branchRepository;
        }
        public async Task<IActionResult> Created()
        {
            var res = _branchRepository.ProtectBranch("main");

            return Ok(true) ;
        }
    }
}

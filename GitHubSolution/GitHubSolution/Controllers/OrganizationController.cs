using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GitHubSolution.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationController : ControllerBase
    {
        public async Task<IActionResult> Created()
        {
           

            return Ok(true) ;
        }
    }
}

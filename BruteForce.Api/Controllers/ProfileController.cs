using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BruteForce.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        public ProfileController()
        {
            
        }

        [HttpGet("passport")]
        public IActionResult GetPassportData()
            => Ok(new { Series = "2244", Number = "333666" });
    }
}

using Microsoft.AspNetCore.Mvc;

namespace HomeForPets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestContoller : ControllerBase
    {
        public TestContoller()
        {
            
        }
        public async Task<IActionResult> Test()
        {
            return Ok();
        }
    }
}

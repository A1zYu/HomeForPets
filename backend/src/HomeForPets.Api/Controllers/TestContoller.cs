using HomeForPets.Enums;
using HomeForPets.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeForPets.WebApi.Controllers
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

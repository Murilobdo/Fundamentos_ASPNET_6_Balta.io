using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }        


        [HttpGet("")]
        public IActionResult Get() => Ok($"Serviço online {DateTime.Now} !");    
    }
}
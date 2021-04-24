using Microsoft.AspNetCore.Mvc;

namespace CurrencySpeakerServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencySpeaker : ControllerBase
    {
        [HttpGet]
        public string Get(string amount = null)
        {
            return amount;
        }
    }
}

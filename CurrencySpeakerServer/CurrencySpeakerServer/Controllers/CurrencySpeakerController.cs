using CurrencySpeakerServer.Parser;
using Microsoft.AspNetCore.Mvc;

namespace CurrencySpeakerServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencySpeakerController : ControllerBase
    {
        private readonly ISpeakerParser _speakerParser;

        public CurrencySpeakerController(ISpeakerParser speakerParser)
        {
            _speakerParser = speakerParser;
        }

        [HttpGet]
        public string Get(string amount = null)
        {
            return _speakerParser.Parse(amount);
        }
    }
}

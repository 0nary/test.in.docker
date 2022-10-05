using Example.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Example.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private static ValuesService _valuesService = new ValuesService();

        // GET: api/Values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get() => Ok(_valuesService.GetValues());

        // GET: api/Values/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<string>> Get(int id) => Ok(_valuesService.GetValue());
    }
}

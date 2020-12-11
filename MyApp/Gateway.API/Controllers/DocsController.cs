using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Writers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var urlOrder = "http://gateway-api/swagger/docs/v1/orders";

            var client = new HttpClient();
            var response = await client.GetAsync(urlOrder);

            var content = await response.Content.ReadAsStringAsync();

            //return Ok(content);

            var doc = new OpenApiStringReader().Read(content, out var diagnostic);

            string json;

            using (var outputString = new StringWriter())
            {
                var writer = new OpenApiJsonWriter(outputString);
                doc.SerializeAsV3(writer);
                json = outputString.ToString();
            }

            return Ok(json);
        }
    }
}
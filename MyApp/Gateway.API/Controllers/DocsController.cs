using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

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

            //OpenApiDocument openApi = JsonConvert.DeserializeObject<OpenApiDocument>(content);

            return Ok(content);
        }
    }
}
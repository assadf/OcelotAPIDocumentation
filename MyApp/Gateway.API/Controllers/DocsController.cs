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
            // TODO: Cache the generated document
            var urlOrders = "http://gateway-api/swagger/docs/v1/orders";
            var urlCustomers = "http://gateway-api/swagger/docs/v1/customers";
            var urlOrderProviderA = "http://orderprovidera-api/swagger/v1/swagger.json";
            var urlOrderProviderB = "http://orderproviderb-api/swagger/v1/swagger.json";

            var client = new HttpClient();

            var orderResponse = await client.GetAsync(urlOrders);
            var orderContent = await orderResponse.Content.ReadAsStringAsync();
            var orderDoc = new OpenApiStringReader().Read(orderContent, out var diagnosticOrder);

            var customerResponse = await client.GetAsync(urlCustomers);
            var customerContent = await customerResponse.Content.ReadAsStringAsync();
            var customerDoc = new OpenApiStringReader().Read(customerContent, out var diagnosticCustomer);

            var orderProviderAResponse = await client.GetAsync(urlOrderProviderA);
            var orderProviderAContent = await orderProviderAResponse.Content.ReadAsStringAsync();
            var orderProviderADoc = new OpenApiStringReader().Read(orderProviderAContent, out var diagnosticOrderProvidera );

            var orderProviderBResponse = await client.GetAsync(urlOrderProviderB);
            var orderProviderBContent = await orderProviderBResponse.Content.ReadAsStringAsync();
            var orderProviderBDoc = new OpenApiStringReader().Read(orderProviderBContent, out var diagnosticOrderProviderb);

            orderDoc.Info.Title = "Order System API";

            foreach (var path in orderProviderADoc.Paths)
            {
                var newpath = path.Key.Replace("/api", "").ToLower();
                newpath += $" ({orderProviderADoc.Info.Title})";
                orderDoc.Paths.Add(newpath, path.Value);
            }

            foreach (var schema in orderProviderADoc.Components.Schemas)
            {
                orderDoc.Components.Schemas.Add(schema.Key, schema.Value);
            }

            foreach (var path in orderProviderBDoc.Paths)
            {
                var newpath = path.Key.Replace("/api", "").ToLower();
                newpath += $" ({orderProviderBDoc.Info.Title})";
                orderDoc.Paths.Add(newpath, path.Value);
            }

            foreach (var schema in orderProviderBDoc.Components.Schemas)
            {
                orderDoc.Components.Schemas.Add(schema.Key, schema.Value);
            }

            foreach (var path in customerDoc.Paths)
            {
                orderDoc.Paths.Add(path.Key, path.Value);
            }

            foreach (var schema in customerDoc.Components.Schemas)
            {
                orderDoc.Components.Schemas.Add(schema.Key, schema.Value);
            }

            orderDoc.Tags.Add(new OpenApiTag
            {
                Name = "Order Provider",
                Description = "Use this section to select the correct model for the data part in POST /orders"
            });

            string json;

            using (var outputString = new StringWriter())
            {
                var writer = new OpenApiJsonWriter(outputString);
                orderDoc.SerializeAsV3(writer);
                json = outputString.ToString();
            }

            return Ok(json);
        }
    }
}
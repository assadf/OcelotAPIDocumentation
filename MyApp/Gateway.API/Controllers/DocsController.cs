using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Writers;

namespace Gateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocsController : ControllerBase
    {
        private readonly OcelotSwaggerOptions _ocelotSwaggerOptions;
        private readonly IMemoryCache _memoryCache;

        public DocsController(IOptions<OcelotSwaggerOptions> options, IMemoryCache memoryCache)
        {
            _ocelotSwaggerOptions = options.Value;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var baseUri = "http://gateway-api/swagger/docs";
            var client = new HttpClient();
            var doc = new OpenApiDocument();
            doc.Paths = new OpenApiPaths();
            doc.Components = new OpenApiComponents();
            doc.Tags = new List<OpenApiTag>();

            foreach (var api in _ocelotSwaggerOptions.SwaggerEndpoints)
            {
                var firstConfig = api.Config[0];
                var apiUri = $"{baseUri}/{firstConfig.Version}/{api.Key}";

                if (firstConfig.Url.StartsWith("http://orderprovider"))
                {
                    apiUri = firstConfig.Url;
                }

                var response = await client.GetAsync(apiUri);
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseDoc = new OpenApiStringReader().Read(responseContent, out var diagnostic);

                foreach (var path in responseDoc.Paths)
                {
                    var pathKey = path.Key;

                    if (firstConfig.Url.StartsWith("http://orderprovider"))
                    {
                        pathKey = $"{path.Key.Replace("/api", "").ToLower()} ({firstConfig.Name})";
                    }

                    doc.Paths.Add(pathKey, path.Value);
                }

                foreach (var schema in responseDoc.Components.Schemas)
                {
                    doc.Components.Schemas.Add(schema.Key, schema.Value);
                }

                foreach (var tag in responseDoc.Tags)
                {
                    doc.Tags.Add(tag);
                }
            }

            string json;

            using (var outputString = new StringWriter())
            {
                var writer = new OpenApiJsonWriter(outputString);
                doc.SerializeAsV3(writer);
                json = outputString.ToString();
            }

            return Ok(json);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAsync()
        //{
        //    // TODO: Cache the generated document
        //    var urlOrders = "http://gateway-api/swagger/docs/v1/orders";
        //    var urlCustomers = "http://gateway-api/swagger/docs/v1/customers";
        //    var urlOrderProviderA = "http://orderprovidera-api/swagger/v1/swagger.json";
        //    var urlOrderProviderB = "http://orderproviderb-api/swagger/v1/swagger.json";

        //    var client = new HttpClient();

        //    var orderResponse = await client.GetAsync(urlOrders);
        //    var orderContent = await orderResponse.Content.ReadAsStringAsync();
        //    var orderDoc = new OpenApiStringReader().Read(orderContent, out var diagnosticOrder);

        //    var customerResponse = await client.GetAsync(urlCustomers);
        //    var customerContent = await customerResponse.Content.ReadAsStringAsync();
        //    var customerDoc = new OpenApiStringReader().Read(customerContent, out var diagnosticCustomer);

        //    var orderProviderAResponse = await client.GetAsync(urlOrderProviderA);
        //    var orderProviderAContent = await orderProviderAResponse.Content.ReadAsStringAsync();
        //    var orderProviderADoc = new OpenApiStringReader().Read(orderProviderAContent, out var diagnosticOrderProvidera );

        //    var orderProviderBResponse = await client.GetAsync(urlOrderProviderB);
        //    var orderProviderBContent = await orderProviderBResponse.Content.ReadAsStringAsync();
        //    var orderProviderBDoc = new OpenApiStringReader().Read(orderProviderBContent, out var diagnosticOrderProviderb);

        //    orderDoc.Info.Title = "Order System API";

        //    foreach (var path in orderProviderADoc.Paths)
        //    {
        //        var newpath = path.Key.Replace("/api", "").ToLower();
        //        newpath += $" ({orderProviderADoc.Info.Title})";
        //        orderDoc.Paths.Add(newpath, path.Value);
        //    }

        //    foreach (var schema in orderProviderADoc.Components.Schemas)
        //    {
        //        orderDoc.Components.Schemas.Add(schema.Key, schema.Value);
        //    }

        //    foreach (var path in orderProviderBDoc.Paths)
        //    {
        //        var newpath = path.Key.Replace("/api", "").ToLower();
        //        newpath += $" ({orderProviderBDoc.Info.Title})";
        //        orderDoc.Paths.Add(newpath, path.Value);
        //    }

        //    foreach (var schema in orderProviderBDoc.Components.Schemas)
        //    {
        //        orderDoc.Components.Schemas.Add(schema.Key, schema.Value);
        //    }

        //    foreach (var path in customerDoc.Paths)
        //    {
        //        orderDoc.Paths.Add(path.Key, path.Value);
        //    }

        //    foreach (var schema in customerDoc.Components.Schemas)
        //    {
        //        orderDoc.Components.Schemas.Add(schema.Key, schema.Value);
        //    }

        //    orderDoc.Tags.Add(new OpenApiTag
        //    {
        //        Name = "Order Provider",
        //        Description = "Use this section to select the correct model for the data part in POST /orders"
        //    });

        //    string json;

        //    using (var outputString = new StringWriter())
        //    {
        //        var writer = new OpenApiJsonWriter(outputString);
        //        orderDoc.SerializeAsV3(writer);
        //        json = outputString.ToString();
        //    }

        //    return Ok(json);
        //}
    }
}
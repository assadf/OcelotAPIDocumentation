﻿using System.IO;
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

            foreach (var path in customerDoc.Paths)
            {
                orderDoc.Paths.Add(path.Key, path.Value);
            }

            foreach (var schema in customerDoc.Components.Schemas)
            {
                orderDoc.Components.Schemas.Add(schema.Key, schema.Value);
            }

            foreach (var path in orderProviderADoc.Paths)
            {
                orderDoc.Paths.Add(path.Key, path.Value);
            }

            foreach (var schema in orderProviderADoc.Components.Schemas)
            {
                orderDoc.Components.Schemas.Add(schema.Key, schema.Value);
            }

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
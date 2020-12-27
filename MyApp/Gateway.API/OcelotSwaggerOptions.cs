namespace Gateway.API
{
    public class OcelotSwaggerOptions
    {
        public SwaggerEndpoint[] SwaggerEndpoints { get; set; }
    }

    public class SwaggerEndpoint
    {
        public string Key { get; set; }

        public SwaggerEndpointConfig[] Config { get; set; }
    }

    public class SwaggerEndpointConfig
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public string Url { get; set; }
    }
}

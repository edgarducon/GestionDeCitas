using System.Web.Http;
using WebActivatorEx;
using Swashbuckle.Application;
using CitasMicorservice;

namespace CitasMicroservice
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "CitasMicroservice");
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("API Documentation");
                    c.InjectStylesheet(thisAssembly, "YourNamespace.Stylesheet.css"); 
                });
        }
    }

}

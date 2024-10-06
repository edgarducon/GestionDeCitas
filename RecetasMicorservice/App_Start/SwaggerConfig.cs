using System.Web.Http;
using WebActivatorEx;
using Swashbuckle.Application;
using RecetasMicroservice;

namespace RecetasMicroservice
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "RecetasMicroservice");
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("API Documentation");
                    c.InjectStylesheet(thisAssembly, "YourNamespace.Stylesheet.css");
                });
        }

    }
}

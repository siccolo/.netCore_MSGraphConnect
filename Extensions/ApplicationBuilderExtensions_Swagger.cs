using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Extensions
{
    public static partial class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddSwagger(this IApplicationBuilder app)
        {
           var ab = app.UseSwagger()
                    .UseSwaggerUI(c =>
                    {
                        c.RoutePrefix = "swagger/ui";
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI(v1)");
                    });

            return ab;
        }
    }
}

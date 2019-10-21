using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Helpers
{
    public static partial class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddErrorHandler(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var ex = error.Error;

                        await context.Response.WriteAsync(new Models.ErrorInfo()
                        {
                            StatusCode = 500,
                            Message = ex.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}

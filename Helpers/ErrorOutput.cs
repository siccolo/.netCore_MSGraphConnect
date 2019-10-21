using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Helpers
{
    public static class ErrorOutput
    {
        public static Task ErrorOutputResponse(HttpContext httpContext, Models.ErrorInfo result)
        {
            httpContext.Response.ContentType = "application/json";

            var json = new JObject(
                new JProperty("status", result.StatusCode),
                new JProperty("results", result.ToString())
                );
            return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
        }
    }
}

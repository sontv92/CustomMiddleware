using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIMiddleware
{
    public class MyCustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public MyCustomMiddleware(RequestDelegate next, ILoggerFactory logFactory)
        {
            _next = next;

            _logger = logFactory.CreateLogger("MyCustomMiddleware");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation($"RequestMiddleware is executing..: query={httpContext.Request.QueryString.ToString()}");

            var queries = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(httpContext.Request.QueryString.ToString());

            foreach (var query in queries)
            {
                httpContext.Request.Headers.Add(query.Key, query.Value);
            }

            await _next(httpContext); // calling next middleware

        }
    }

}

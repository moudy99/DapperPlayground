using System.Text;

namespace Dapper_VS_EFcore
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly ILogger<RequestResponseLoggingMiddleware> logger;
        private readonly RequestDelegate next;

        public RequestResponseLoggingMiddleware(ILogger<RequestResponseLoggingMiddleware> logger , RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            LogRequestInfo(context);

            await next(context);
            LogResponseInfo(context);

        }

        private void LogRequestInfo(HttpContext context) 
        {
            var request = context.Request;
            var requestLog = new StringBuilder();
            requestLog.Append("Incomming Request");
            requestLog.AppendLine($"HTTP {request.Method} {request.Path}");
            requestLog.AppendLine($"Host: {request.Host}");

            logger.LogInformation(requestLog.ToString());
        }
        private void LogResponseInfo(HttpContext context)
        {
            var response = context.Response;

            var responseLog = new StringBuilder();
            responseLog.AppendLine("Outgoing Response:");
            responseLog.AppendLine($"HTTP {response.StatusCode}");

            logger.LogInformation(responseLog.ToString());
        }

    }
}

namespace LaboAppWebV1._0._0.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Capturar el request
            var request = await FormatRequest(context.Request);
            _logger.LogInformation($"Request: {request}");

            // Guardar el body original del response
            var originalBodyStream = context.Response.Body;

            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                // Continuar con el pipeline
                await _next(context);

                // Capturar el response
                var response = await FormatResponse(context.Response);
                _logger.LogInformation($"Response: {response}");

                // Copiar el response al body original
                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error procesando la solicitud.");
                throw;
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering(); // Permite leer el body del request
            var body = request.Body;

            using var reader = new StreamReader(body, leaveOpen: true);
            var bodyText = await reader.ReadToEndAsync();
            body.Seek(0, SeekOrigin.Begin); // Volver a la posición inicial para que otros puedan usarlo

            return $"{request.Method} {request.Path} {request.QueryString} Body: {bodyText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin); // Volver a la posición inicial para leer
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin); // Volver a la posición inicial para la siguiente lectura
            return $"Status Code: {response.StatusCode} Body: {text}";
        }
    }

}

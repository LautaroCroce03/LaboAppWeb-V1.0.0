using System.Diagnostics;

namespace LaboAppWebV1._0._0.Middleware
{
    public class TraceIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TraceIdMiddleware> _logger;

        public TraceIdMiddleware(RequestDelegate next, ILogger<TraceIdMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Crear un TraceId único para la solicitud (si no hay uno ya presente)
            var traceId = Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString();

            // Añadir el TraceId al contexto de la solicitud para que esté disponible en todo el ciclo de vida
            context.Items["TraceId"] = traceId;

            // Loguear el inicio de la solicitud con el TraceId
            _logger.LogInformation("Inicio de la solicitud: TraceId = {TraceId}", traceId);

            // Continuar con la ejecución de la solicitud
            await _next(context);

            // Loguear el fin de la solicitud con el TraceId
            _logger.LogInformation("Fin de la solicitud: TraceId = {TraceId}", traceId);
        }
    }

}

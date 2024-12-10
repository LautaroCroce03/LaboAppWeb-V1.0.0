﻿using System.Diagnostics;

namespace LaboAppWebV1._0._0.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var traceId = Activity.Current?.TraceId.ToString() ?? "NoTraceId";

            try
            {
                // Ejecutamos la solicitud
                await _next(context);
            }
            catch (Exception ex)
            {
                // Logueamos la excepción para el monitoreo interno
                _logger.LogError(ex, "Se ha producido un error al procesar la solicitud.");

                // Retornamos una respuesta amigable al cliente
                context.Response.StatusCode = 500;  // Código de error 500 (Internal Server Error)
                context.Response.ContentType = "application/json";
                var errorResponse = new
                {
                    traceId = traceId,
                    message = "Algo salió mal. Estamos trabajando para solucionarlo.",
                    details = "Por favor, intenta nuevamente más tarde."
                };

                // Enviar la respuesta en formato JSON
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
            finally
            {
                // Lógica que se ejecuta después de que la acción se haya completado
                _logger.LogInformation("Finalización del proceso de solicitud.");
            }
        }
    }

}
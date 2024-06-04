using EmptyProject.Areas.System.FailureBack.Entities;
using EmptyProject.Areas.System.FailureBack.Interfaces;

namespace EmptyProject.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                using IServiceScope serviceScope = _serviceProvider.CreateScope();

                IFailureRepository failureRepository = serviceScope.ServiceProvider.GetRequiredService<IFailureRepository>();
                Failure failure = new()
                {
                    Active = true,
                    DateTimeCreation = DateTime.Now,
                    DateTimeLastModification = DateTime.Now,
                    UserCreationId = 1,
                    UserLastModificationId = 1,
                    EmergencyLevel = 1,
                    Comment = "",
                    Message = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace
                };

                failureRepository.Add(failure);
            }
        }
    }
}

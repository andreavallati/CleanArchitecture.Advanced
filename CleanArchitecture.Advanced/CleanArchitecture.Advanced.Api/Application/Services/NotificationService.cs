using CleanArchitecture.Advanced.Api.Application.Interfaces.Services;

namespace CleanArchitecture.Advanced.Api.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task SendNotificationAsync(string message)
        {
            // Simulate sending a notification (e.g., email, log entry, etc.)
            _logger.LogInformation("Notification sent: {message}", message);

            return Task.CompletedTask;
        }
    }
}

namespace LibFlow.Services.Email;

public interface ISendEmail
{
    Task SendEmailAsync(string email, string subject, string body);
}

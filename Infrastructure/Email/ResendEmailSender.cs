using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Email;

public class ResendEmailSender(IConfiguration config, ILogger<ResendEmailSender> logger)
    : IEmailSender<User>
{
    public Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
    {
        var subject = "Confirm your email - Reactivities";
        var body = $"""
            <h2>Welcome to Reactivities!</h2>
            <p>Please confirm your email address by clicking the link below:</p>
            <p><a href="{confirmationLink}">Confirm Email</a></p>
            """;

        return SendEmailAsync(email, subject, body);
    }

    public Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
    {
        var clientUrl = config["ClientAppUrl"] ?? "https://localhost:5173";
        var encodedCode = Uri.EscapeDataString(resetCode);
        var resetLink = $"{clientUrl}/reset-password?email={Uri.EscapeDataString(email)}&code={encodedCode}";

        var subject = "Password Reset - Reactivities";
        var body = $"""
            <h2>Password Reset</h2>
            <p>You requested a password reset. Click the link below to reset your password:</p>
            <p><a href="{resetLink}">Reset Password</a></p>
            <p>If you did not request this, please ignore this email.</p>
            """;

        return SendEmailAsync(email, subject, body);
    }

    public Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
    {
        var subject = "Reset your password - Reactivities";
        var body = $"""
            <h2>Password Reset</h2>
            <p>Click the link below to reset your password:</p>
            <p><a href="{resetLink}">Reset Password</a></p>
            <p>If you did not request this, please ignore this email.</p>
            """;

        return SendEmailAsync(email, subject, body);
    }

    private async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
    {
        var apiKey = config["Resend:ApiKey"];

        if (string.IsNullOrEmpty(apiKey))
        {
            logger.LogWarning("Resend API key is not configured. Email not sent to {Email}", toEmail);
            return;
        }

        var fromEmail = config["Resend:FromEmail"] ?? "onboarding@resend.dev";

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var payload = new
        {
            from = fromEmail,
            to = new[] { toEmail },
            subject,
            html = htmlBody
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("https://api.resend.com/emails", content);

        if (response.IsSuccessStatusCode)
        {
            logger.LogInformation("Email sent successfully to {Email}", toEmail);
        }
        else
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            logger.LogError("Failed to send email to {Email}. Status: {Status}. Response: {Response}",
                toEmail, response.StatusCode, responseBody);
        }
    }
}

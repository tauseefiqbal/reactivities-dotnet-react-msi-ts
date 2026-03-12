using System;
using System.Net.Http.Headers;
using System.Text;
using API.DTOs;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using static API.DTOs.GitHubInfo;

namespace API.Controllers;

public class AccountController(SignInManager<User> signInManager,
    IConfiguration config) : BaseApiController
{
    [AllowAnonymous]
    [HttpPost("github-login")]
    public async Task<ActionResult> LoginWithGithub(string code)
    {
        if (string.IsNullOrEmpty(code))
            return BadRequest("Missing authorization code");

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // step 1 - exchange code for access token
        var tokenResponse = await httpClient.PostAsJsonAsync(
            "https://github.com/login/oauth/access_token",
            new GitHubAuthRequest
            {
                Code = code,
                ClientId = config["Authentication:GitHub:ClientId"]!,
                ClientSecret = config["Authentication:GitHub:ClientSecret"]!,
                RedirectUri = $"{config["ClientAppUrl"]}/auth-callback"
            }
        );

        if (!tokenResponse.IsSuccessStatusCode)
            return BadRequest("Failed to get access token");

        var tokenContent = await tokenResponse.Content.ReadFromJsonAsync<GitHubTokenResponse>();

        if (string.IsNullOrEmpty(tokenContent?.AccessToken))
            return BadRequest("Failed to retrieve access token");

        // step 2 - fetch user info from GitHub
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenContent.AccessToken);
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Reactivities");

        var userResponse = await httpClient.GetAsync("https://api.github.com/user");
        if (!userResponse.IsSuccessStatusCode)
            return BadRequest("Failed to fetch user from GitHub");

        var user = await userResponse.Content.ReadFromJsonAsync<GitHubUser>();
        if (user == null) return BadRequest("Failed to read user from GitHub");

        // step 3 - getting the email if needed
        if (string.IsNullOrEmpty(user?.Email))
        {
            var emailResponse = await httpClient.GetAsync("https://api.github.com/user/emails");
            if (emailResponse.IsSuccessStatusCode)
            {
                var emails = await emailResponse.Content.ReadFromJsonAsync<List<GitHubEmail>>();

                var primary = emails?.FirstOrDefault(e => e is { Primary: true, Verified: true })?.Email;

                if (string.IsNullOrEmpty(primary))
                    return BadRequest("Failed to get email from GitHub");

                user!.Email = primary;
            }
            else
            {
                return BadRequest("Failed to fetch email from GitHub API");
            }
        }

        // Ensure we have an email before proceeding
        if (string.IsNullOrEmpty(user.Email))
            return BadRequest("No email address available from GitHub account");

        // step 4 - find or create user and sign in
        var existingUser = await signInManager.UserManager.FindByEmailAsync(user.Email);

        if (existingUser == null)
        {
            existingUser = new User
            {
                Email = user.Email,
                UserName = user.Email,
                DisplayName = user.Name,
                ImageUrl = user.ImageUrl,
                EmailConfirmed = true
            };

            var createdResult = await signInManager.UserManager.CreateAsync(existingUser);
            if (!createdResult.Succeeded)
                return BadRequest("Failed to create user");
        }
        else if (!existingUser.EmailConfirmed)
        {
            // If user exists but email not confirmed, confirm it for OAuth users
            existingUser.EmailConfirmed = true;
            await signInManager.UserManager.UpdateAsync(existingUser);
        }

        await signInManager.SignInAsync(existingUser, false);

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(RegisterDto registerDto)
    {
        var user = new User
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            DisplayName = registerDto.DisplayName
        };

        var result = await signInManager.UserManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, false);
            return Ok();
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        return ValidationProblem();
    }

    [AllowAnonymous]
    [HttpGet("user-info")]
    public async Task<ActionResult> GetUserInfo()
    {
        if (User.Identity?.IsAuthenticated == false) return NoContent();

        var user = await signInManager.UserManager.GetUserAsync(User);

        if (user == null) return Unauthorized();

        return Ok(new
        {
            user.DisplayName,
            user.Email,
            user.Id,
            user.ImageUrl
        });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await signInManager.SignOutAsync();

        return NoContent();
    }

    [HttpPost("change-password")]
    public async Task<ActionResult> ChangePassword(ChangePasswordDto passwordDto)
    {
        var user = await signInManager.UserManager.GetUserAsync(User);

        if (user == null) return Unauthorized();

        var result = await signInManager.UserManager.ChangePasswordAsync(user, passwordDto.CurrentPassword, passwordDto.NewPassword);

        if (result.Succeeded) return Ok();

        return BadRequest(result.Errors.First().Description);
    }
}
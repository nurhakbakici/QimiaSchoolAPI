using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace QimiaSchool.Business.Middleware;
public class TokenRefreshMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _domain;
    private readonly string _audience;

    public TokenRefreshMiddleware(
        RequestDelegate next,
        IConfiguration config)
    {
        _next = next;
        _config = config;
        _clientId = _config["Auth0:ClientId"] ?? throw new ArgumentNullException(_config["Auth0:ClientId"]);
        _domain = _config["Auth0:Domain"] ?? throw new ArgumentNullException(_config["Auth0:Domain"]);
        _clientSecret = _config["Auth0:ClientSecret"] ?? throw new ArgumentNullException(_config["Auth0:ClientSecret"]);
        _audience = _config["Auth0:Audience"] ?? throw new ArgumentNullException(_config["Auth0:Audience"]);
    }

    public async Task Invoke(HttpContext context)
    {
        var result = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
        var authorizationHeader = context.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        var token = authorizationHeader["Bearer ".Length..].Trim();

        if (result?.Succeeded == true && !string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var expirationTime = jwt.ValidTo;

            if (expirationTime <= DateTime.UtcNow.AddMinutes(10))
            {
                var refreshToken = GenerateToken();

                try
                {
                    var tokenValidationParameters = GetTokenValidationParameters();
                    SecurityToken validatedToken;
                    var principal = handler.ValidateToken(refreshToken, tokenValidationParameters, out validatedToken);

                    context.Request.Headers["Authorization"] = $"Bearer {refreshToken}";

                    await _next(context);
                }
                catch (SecurityTokenExpiredException)
                {
                    var newToken = await GetNewToken();

                    context.Request.Headers["Authorization"] = $"Bearer {newToken}";

                    await _next(context);
                }
                catch (SecurityTokenInvalidSignatureException)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }
            else
            {
                await _next(context);
            }
        }
        else
        {
            await _next(context);
        }
    }

    private string GenerateToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_clientSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private TokenValidationParameters GetTokenValidationParameters()
    {
        var key = Encoding.ASCII.GetBytes(_clientSecret);

        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidIssuer = $"{_domain}",
            ValidAudience = _audience,

        };
    }

    private async Task<string> GetNewToken()
    {
        using (var client = new HttpClient())
        {
            var tokenEndpoint = $"{_domain}/oauth/token";

            var requestBody = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", _clientId },
                { "client_secret", _clientSecret },
                { "audience", _audience }
            };

            var response = await client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(requestBody));

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to obtain new token. Status code: {response.StatusCode}. Error message: {errorMessage}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenData = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);
            var accessToken = tokenData!["access_token"];

            return accessToken;
        }
    }
}



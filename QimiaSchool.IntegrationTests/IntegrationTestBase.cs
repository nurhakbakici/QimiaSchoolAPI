using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using QimiaSchool.Common;
using QimiaSchool.DataAccess;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;

namespace QimiaSchool.IntegrationTests;

public abstract class IntegrationTestBase : IDisposable
{
    protected QimiaSchoolDbContext databaseContext;
    protected HttpClient client;

    private WebApplicationFactory<Program> _testWebAppFactory;

    protected IntegrationTestBase()
    {

    }

    [SetUp]
    public void Setup()
    {
        databaseContext.Database.EnsureCreated();
        // to make sure that the database created before each test.
    }

    [TearDown]
    public void TearDown()
    {
        databaseContext.Database.EnsureDeleted();
        // to make sure that the database deleted after each test.
    }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var auth0Configuration = GetAuth0Configuration();

        var connectionString = $"Server=LAPTOP-3PS3KIUT\\SQLEXPRESS;Database=QimiaSchool_{Guid.NewGuid()};Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;";

        _testWebAppFactory = new CustomWebApplicationFactory(connectionString);

        // this method will create an instance of webApplicationFactory with the custom connection string before test starts. 

        client = _testWebAppFactory.CreateClient();

        var token = GetAuth0AccessTokenAsync(auth0Configuration).GetAwaiter().GetResult();

        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"bearer {token}");

        Environment.SetEnvironmentVariable("ConnectionStrings:DefaultConnection", connectionString);

        databaseContext = new QimiaSchoolDbContextFactory().CreateDbContext(new[] { connectionString });


    }

    private static Auth0Configuration GetAuth0Configuration()
    {
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .AddEnvironmentVariables();

        var configuration = configBuilder.Build();

        var auth0Configuration = new Auth0Configuration();
        configuration.GetSection("Auth0").Bind(auth0Configuration);

        return auth0Configuration;
    }



    private static async Task<string?> GetAuth0AccessTokenAsync(Auth0Configuration auth0Configuration)
    {
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(auth0Configuration.Domain);
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var requestData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("client_id", auth0Configuration.ClientId),
            new KeyValuePair<string, string>("client_secret", auth0Configuration.ClientSecret),
            new KeyValuePair<string, string>("audience", auth0Configuration.Audience)
        });

        var response = await httpClient.PostAsync("/oauth/token", requestData);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error while requesting the Auth0 access token.");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

        return tokenResponse?.AccessToken;
    }


    public void Dispose()
    {
        _testWebAppFactory.Dispose();
        databaseContext.Dispose();

    }
}

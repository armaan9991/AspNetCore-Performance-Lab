using BenchmarkDotNet.Attributes;
using System.Net.Http.Json;
using System.Net.Http.Json;

namespace MyBenchmarks;

public class ApiBenchmark
{
    private HttpClient _httpClient = null!; // null as intialization but would be assigned before use.

    private class LoginModel
    {
        public string Email { get; set; } = "a@g.com"; 
        public string Password { get; set; } = "gill1234";   
    }

    // Helper class to match your login API response JSON payload
    private class LoginResponse
    {
        public string Token { get; set; } = string.Empty; 
    }

    [GlobalSetup]  // this tells donet to run exactly once before  any performance begins. handles logic outside of timer.
    public async void setup()
    {

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };

        _httpClient = new HttpClient(handler);
        _httpClient.BaseAddress = new Uri("http://localhost:5263");

        
        var loginData = new LoginModel();

        var response = await _httpClient.PostAsJsonAsync("/api/Auth/login", loginData);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Authentication failed! Status: {response.StatusCode}. Ensure your test user exists in the DB.");
        }

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (result == null || string.IsNullOrEmpty(result.Token))
        {
            throw new Exception("Authentication succeeded but no token property was returned in the JSON payload.");
        }
        _httpClient.DefaultRequestHeaders.Authorization =
           new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);
    }
    


    [Benchmark]   // means this method will  test performance.
    public async Task GetProducts()
    {
        var response = await _httpClient.GetAsync("/api/Product/paged?page=1&pageSize=10"); // hit actually api.
        response.EnsureSuccessStatusCode();         // if there is any exception then this throw exception to stop the false beanchmark / measurements.
    }

    [GlobalCleanup]   // execute this method once all benchmarks are finished. closes all network socket.
    public void Cleanup()
    {
        _httpClient.Dispose();
    }

    //[Benchmark]        //  means it tells that this method is to test and measure.
    
}
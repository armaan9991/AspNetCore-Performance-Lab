using BenchmarkDotNet.Attributes;

namespace MyBenchmarks;

public class ApiBenchmark
{
    private HttpClient _httpClient = null!; // null as intialization but would be assigned before use.

    [GlobalSetup]  // this tells donet to run exactly once before  any performance begins. handles logic outside of timer.
    public void setup()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://localhost:5263");
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

    [Benchmark]        //  means it tells that this method is to test and measure.
    public void Test()
    {
        
    }
}
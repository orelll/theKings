namespace TheKings.Core;

public class HttpDataLoader
{
    // both of those variables should be injected as config (through ctor)
    private readonly Uri _kingsBaseAddress = new Uri("https://gist.githubusercontent.com");
    private readonly string _kingsGetEndpoint = "christianpanton/10d65ccef9f29de3acd49d97ed423736/raw/b09563bc0c4b318132c7a738e679d4f984ef0048/kings";
    
    
    public async Task<string> FetchData(CancellationToken token)
    {
        // in real life it is a good idea to use IHttpClientFactory and configure dedicated client in Startup.cs
        var client = new HttpClient();
        client.BaseAddress = _kingsBaseAddress;

        try
        {
            var httpResponse = (await client.GetAsync(_kingsGetEndpoint, token)).EnsureSuccessStatusCode();
            return await httpResponse.Content.ReadAsStringAsync(token);
        }
        catch (Exception e)
        {
            // maybe logger?
            Console.WriteLine(e);
            return string.Empty;
        }
    }
}
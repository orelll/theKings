using System.Text.Json.Serialization;
using Newtonsoft.Json;
using TheKings.Core.Entities;

namespace TheKings.Core;

public class KingsService
{
    private readonly HttpDataLoader _dataLoader;
    
    public KingsService()
    {
        // in real life this dependency should be injected as ctor param
        _dataLoader = new HttpDataLoader();
    }

    public async Task<IEnumerable<King>> LoadKings(CancellationToken token)
    {
        var dataAsString = await _dataLoader.FetchData(token);
        return JsonConvert.DeserializeObject<IEnumerable<King>>(dataAsString);
    }
}
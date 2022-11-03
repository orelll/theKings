using Newtonsoft.Json;
using TheMonarchs.Core.Entities;

namespace TheMonarchs.Core;

public class MonarchsService
{
    private readonly HttpDataLoader _dataLoader;
    
    public MonarchsService()
    {
        // in real life this dependency should be injected as ctor param
        _dataLoader = new HttpDataLoader();
    }

    public async Task<IEnumerable<Monarch>> LoadKings(CancellationToken token)
    {
        var dataAsString = await _dataLoader.FetchData(token);
        return JsonConvert.DeserializeObject<IEnumerable<Monarch>>(dataAsString);
    }
}
using Newtonsoft.Json;

namespace TheKings.Core.Entities;

public class King
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("nm")]
    public string Name { get; set; }
    
    [JsonProperty("cty")]
    public string City { get; set; }
    
    [JsonProperty("hse")]
    public string House { get; set; }

    private string _years;

    [JsonProperty("yrs")]
    public string Years
    {
        get => _years; 
        set
        {
            _years = value;
            var (start, end, diff) = CalculateYears(_years);
            StartYear = start;
            EndYear = end;
            YearsOfRule = diff;
        }
    }

    public int StartYear { get; set; }
    public int EndYear { get; set; }
    public int YearsOfRule { get; set; }

    private (int start, int end, int diff) CalculateYears(string years)
    {
        if (string.IsNullOrEmpty(years)) return (0, 0, 0);
        
        var splitted = years.Split("-", StringSplitOptions.RemoveEmptyEntries);
        
        // this should be changed to TryParse in future
        var casted = splitted.Select(s => int.Parse(s));
        var start = casted.Min();
        var end = casted.Max();
        var diff = start == end ? 1 : Math.Abs(start - end);

        return (start, end, diff);
    }
}

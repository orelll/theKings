using TheKings.Core;
using TheKings.Core.Entities;

namespace TheKings;

public class QuestionsSolver
{
    private readonly KingsService kingsService;
    private IEnumerable<King> kings = new List<King>();

    // this class is bit messy, should be refactored in the future
    
    public QuestionsSolver()
    {
        kingsService = new KingsService();
    }

    public async Task AnswerQuestion1(CancellationToken token)
    {
        await LoadKingsData(token);
        Console.WriteLine($"Question 1: there is {kings.Count()} kings");
    }
    
    public async Task AnswerQuestion2(CancellationToken token)
    {
        await LoadKingsData(token);
        var longestRule = kings.Aggregate(
            (a, b) => a.YearsOfRule > b.YearsOfRule ? a : b  // whatever you need to compare
        );
        Console.WriteLine($"Question 2: king {longestRule.Name} ruled longest. It was from {longestRule.StartYear} to {longestRule.EndYear} what makes {longestRule.YearsOfRule} years");
    }
    
    public async Task AnswerQuestion3(CancellationToken token)
    {
        await LoadKingsData(token);
        var groupedByHouseAndSummed = kings.GroupBy(k => k.House).Select(g => new
        {
            House = g.First().House,
            SumOfYears = g.Sum(a => a.YearsOfRule)
        });
        var longestRulingHouse = groupedByHouseAndSummed.Aggregate(
            (a, b) => a.SumOfYears > b.SumOfYears ? a : b  // whatever you need to compare
        );
        Console.WriteLine($"Question 3: Longest ruling house is {longestRulingHouse.House}, it spent {longestRulingHouse.SumOfYears} on throne");
    }
    
    public async Task AnswerQuestion4(CancellationToken token)
    {
        await LoadKingsData(token);
        var groupedByName = kings.GroupBy(king => king.Name).Select(gr => new
        {
            Name = gr.First().Name,
            Count = gr.Count()
        });

        var mostCommonName = groupedByName.Aggregate(
            (a, b) => a.Count > b.Count ? a : b  // whatever you need to compare
        );
        var mostCommonNames = groupedByName.Where(x => x.Count == mostCommonName.Count);

        Console.WriteLine($"Question 4: Most common king name is {mostCommonName.Name}. It occured {mostCommonName.Count} times");
        Console.WriteLine($"Question 4: In fact eq equo on first place we have {mostCommonNames.Count()} names: {string.Join(Environment.NewLine,  mostCommonNames.Select(x => x.Name))}. Each of them occured {mostCommonName.Count} times ;)");

    }

    private async Task LoadKingsData(CancellationToken token)
    {
        if(!kings.Any())
            kings = await kingsService.LoadKings(token);
    }
}
using TheMonarchs.Core;
using TheMonarchs.Core.Entities;

namespace TheMonarchs;

public class QuestionsSolver
{
    private readonly MonarchsService monarchsService;
    private IEnumerable<Monarch> monarchs = new List<Monarch>();

    // this class is bit messy, should be refactored in the future
    
    public QuestionsSolver()
    {
        monarchsService = new MonarchsService();
    }

    public async Task AnswerQuestion1(CancellationToken token)
    {
        await LoadMonarchsData(token);
        Console.WriteLine($"Question 1: there is {monarchs.Count()} monarchs");
    }
    
    public async Task AnswerQuestion2(CancellationToken token)
    {
        await LoadMonarchsData(token);
        var longestRule = monarchs.Aggregate(
            (a, b) => a.YearsOfRule > b.YearsOfRule ? a : b  // whatever you need to compare
        );
        Console.WriteLine($"Question 2: monarch {longestRule.Name} ruled longest. It was from {longestRule.StartYear} to {longestRule.EndYear} what makes {longestRule.YearsOfRule} years");
    }
    
    public async Task AnswerQuestion3(CancellationToken token)
    {
        await LoadMonarchsData(token);
        var groupedByHouseAndSummed = monarchs.GroupBy(k => k.House).Select(g => new
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
        await LoadMonarchsData(token);
        var groupedByName = monarchs.GroupBy(monarch => monarch.Name).Select(gr => new
        {
            Name = gr.First().Name,
            Count = gr.Count()
        });

        var mostCommonName = groupedByName.Aggregate(
            (a, b) => a.Count > b.Count ? a : b  // whatever you need to compare
        );
        var mostCommonNames = groupedByName.Where(x => x.Count == mostCommonName.Count);

        Console.WriteLine($"Question 4: Most common monarch name is {mostCommonName.Name}. It occured {mostCommonName.Count} times");
        Console.WriteLine($"Question 4: In fact eq equo on first place we have {mostCommonNames.Count()} names: {string.Join(Environment.NewLine,  mostCommonNames.Select(x => x.Name))}. Each of them occured {mostCommonName.Count} times ;)");
    }

    private async Task LoadMonarchsData(CancellationToken token)
    {
        if(!monarchs.Any())
            monarchs = await monarchsService.LoadKings(token);
    }
}
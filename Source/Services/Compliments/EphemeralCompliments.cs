namespace CutieBot.Source.Services.Compliments;

public class EphemeralCompliments : ICompliments
{
    public EphemeralCompliments(params string[] compliments)
        => ComplimentsList.AddRange(compliments);

    private List<string> ComplimentsList = new List<string>();
    public IEnumerable<string> Compliments => ComplimentsList;

    public void AddCompliment(string compliment)
        => ComplimentsList.Add(compliment);

    public string GetCompliment(Random rng)
        => ComplimentsList[rng.Next(0, ComplimentsList.Count - 1)];
}

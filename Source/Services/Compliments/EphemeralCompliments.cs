namespace CutieBot.Source.Services.Compliments;

public class EphemeralCompliments : ICompliments
{
    public EphemeralCompliments(List<string> compliments)
        => ComplimentsList.AddRange(compliments);

    // We make a temp list to return to the IEnumerable; why? idk
    private List<string> ComplimentsList = new List<string>();
    public IEnumerable<string> Compliments => ComplimentsList;

    public void AddCompliment(string compliment)
        => ComplimentsList.Add(compliment);

    public string GetCompliment(Random rng)
        => ComplimentsList[rng.Next(0, ComplimentsList.Count - 1)];
}

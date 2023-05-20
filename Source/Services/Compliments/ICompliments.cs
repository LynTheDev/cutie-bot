using System;

namespace CutieBot.Source.Services.Compliments;

public interface ICompliments
{
    IEnumerable<string> Compliments { get; }

    void AddCompliment(string compliment);
    string GetCompliment(Random rng);
}

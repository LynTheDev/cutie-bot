using CutieBot.Source.Services.Compliments;
using CutieBot.Source.Services.Logs;
using DSharpPlus;
using DSharpPlus.Entities;
using System.Timers;

namespace CutieBot.Source.Services;

public class ComplimentTimer
{
    private DiscordClient Client { get; }
    private ICompliments Compliments { get; }
    private ILogWriter Logger { get; }

    public ComplimentTimer(DiscordClient client, ICompliments compliments, ILogWriter logger)
    {
        Client = client;
        Compliments = compliments;
        Logger = logger;
    }

    public void SetTimer()
    {
        System.Timers.Timer timer = new System.Timers.Timer(5400000);

        timer.Elapsed += TimePassed;

        timer.AutoReset = true;
        timer.Enabled = true;

        Logger.WriteLog("Timer has been set up.", LogLevel.Info);
    }

    private async void TimePassed(Object source, ElapsedEventArgs e)
    {
        DiscordChannel channel = await Client.GetChannelAsync((ulong)CutieBot.Config["CID"]);
        string compliment = Compliments.GetCompliment(new Random());

        await Client.SendMessageAsync(channel, compliment);

        Logger.WriteLog("Message sent in channel!", LogLevel.Message);
    }
}

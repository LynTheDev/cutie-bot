using System;
using System.Reflection;
using CutieBot.Source.Services;
using CutieBot.Source.Services.Compliments;
using CutieBot.Source.Services.Logs;
using CutieBot.Source.XML;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;

namespace CutieBot.Source;

public static class CutieBot
{
    public static IServiceProvider ServiceProvider = null!;
    public static readonly Dictionary<string, object> Config = ReadXML.GetConfigDict();

    public static async Task Main()
    {
        DiscordClient client = new DiscordClient(new DiscordConfiguration()
        {
            Token = (string)Config["Token"]
        });

        IServiceCollection services = new ServiceCollection();
        services.AddSingleton(client);
        services.AddSingleton<ICompliments>(new EphemeralCompliments(
            "You are the most amazing partner in the whole universe <3",
            "No wonder your eyes are blue, I could get lost in them like an ocean",
            "You're a gift to those around you",
            "You fill my world with colour <3",
            "Your sweetness can rival sugar",
            "Your hair is so soft I wanna play in it sdnjfcsdjfsduihgfsjilbfsjodfdsohji -Nat",
            "You are so adorable and kind <3",
            "You're plain cute <33",
            "you've got a smile that outshines the stars in the sky <3"
        ));
        services.AddSingleton<ILogWriter>(new LogWriter());
        services.AddSingleton<ComplimentTimer>();

        ServiceProvider = services.BuildServiceProvider();

        ILogWriter Logger = ServiceProvider.GetRequiredService<ILogWriter>();

        SlashCommandsExtension slashCommands = client.UseSlashCommands(new SlashCommandsConfiguration()
        {
            Services = ServiceProvider
        });

        slashCommands.RegisterCommands(Assembly.GetExecutingAssembly());
        Logger.WriteLog("Loaded commands!", LogLevel.Info);

        slashCommands.SlashCommandErrored += async (_, e) => {
            Logger.WriteLog(e.Exception.Message, LogLevel.Error);

            try 
            {                
                await e.Context.CreateResponseAsync(
                    "An error occurred while executing the command!"
                );
            } 
            catch (DSharpPlus.Exceptions.BadRequestException)
            {
                await e.Context.EditResponseAsync(new DiscordWebhookBuilder()
                    .WithContent("An error occurred while executing the command!")
                );  
            }
        };

        await client.ConnectAsync();
        
        ComplimentTimer timer = ServiceProvider.GetRequiredService<ComplimentTimer>();
        timer.SetTimer();

        using (SemaphoreSlim sem = new SemaphoreSlim(0, 1))
        {
            Console.CancelKeyPress += (_, e) => {
                sem.Release();
                e.Cancel = true;
            };
            
            await sem.WaitAsync();
        }
        await client.DisconnectAsync();
    }
}

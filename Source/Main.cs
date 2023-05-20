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
            (List<string>)Config["Compliments"]
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

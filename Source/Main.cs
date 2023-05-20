using System;
using System.Reflection;
using CutieBot.Source.XML;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;

namespace CutieBot.Source;

public static class CutieBot
{
    public static IServiceProvider ServiceProvider = null!;

    public static async Task Main()
    {
        DiscordClient client = new DiscordClient(new DiscordConfiguration()
        {   
            Token = ReadXML.GetToken()
        });

        IServiceCollection services = new ServiceCollection();
        services.AddSingleton(client);

        ServiceProvider = services.BuildServiceProvider();

        SlashCommandsExtension slashCommands = client.UseSlashCommands(new SlashCommandsConfiguration()
        {
            Services = ServiceProvider
        });

        slashCommands.RegisterCommands(Assembly.GetExecutingAssembly());
        slashCommands.SlashCommandErrored += async (_, e) => {
            Console.WriteLine(e.Exception.Message);
            try 
            {                
                await e.Context.CreateResponseAsync(
                    "An error ocurred while executing the command!"
                );
            } 
            catch (DSharpPlus.Exceptions.BadRequestException)
            {
                await e.Context.EditResponseAsync(new DiscordWebhookBuilder()
                    .WithContent("An error ocurred while executing the command!")
                );  
            }
        };

        await client.ConnectAsync();
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

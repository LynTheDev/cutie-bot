using CutieBot.Source.Services;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace CutieBot.Source.Commands;

public class TestCommands : ApplicationCommandModule
{
    [SlashCommand("check", "Check availibility")]
    public async Task CheckCommand(InteractionContext ctx)
    {
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, 
        new DiscordInteractionResponseBuilder()
            .WithContent($"Acknowledged.")
            .AsEphemeral()
        );
    }
}

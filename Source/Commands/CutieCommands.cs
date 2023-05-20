using DSharpPlus.Entities;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using CutieBot.Source.Services.Compliments;
using CutieBot.Source.XML;

namespace CutieBot.Source.Commands;

public class CutieCommands : ApplicationCommandModule
{
    private ICompliments Compliments { get; }
    
    public CutieCommands(ICompliments compliments)
    {
        Compliments = compliments;
    }

    [SlashCommand("submit", "Submit Compliment to list")]
    public async Task SubmitCommand(
        InteractionContext ctx,
        [Option("compliment", "The compliment to attach to the compliments list.")] string compliment
    )
    {
        // Here the bot DMS me so I can manually edit in case something goes wrong
        DiscordMember user = await ctx.Guild.GetMemberAsync((ulong)CutieBot.Config["NID"]);
        await user.SendMessageAsync($"{CutieBot.Config["Name"]} has implemented: {compliment}");

        // This add compliment function is used to temporarily update, till the bot resets.
        Compliments.AddCompliment(compliment);

        ReadXML.NewCompliment(compliment);

        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
            .WithContent("Done!")
            .AsEphemeral()
        );
    }

    [SlashCommand("marry", "Marry someone")]
    public async Task MarryCommand(
        InteractionContext ctx,
        [Option("user", "The user to marry")] DiscordUser member
    )
    {
        DiscordEmbed marryEmbed = new DiscordEmbedBuilder()
        {
            Color = DiscordColor.Green,
            Title = $"{ctx.Member.Username} is marrying {member.Username}!",
            Description = $"What a cute gesture for {member.Mention}!",

            ImageUrl = "https://64.media.tumblr.com/882c14c4342f5a135b69accc1a2051b7/tumblr_pqmcu2mzxH1x09hz0o1_1280.jpg"
        };

        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
            .AddEmbed(marryEmbed)
        );
    }
}

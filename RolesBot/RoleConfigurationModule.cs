using DSharpPlus;
using DSharpPlus.SlashCommands;

public class RoleConfigurationModule : ApplicationCommandModule {
	[SlashCommand("test", "A slash command made to test the DSharpPlusSlashCommands library!")]
	public async Task TestCommand(InteractionContext ctx) {
		await ctx.CreateResponseAsync("Hi");
	}
}

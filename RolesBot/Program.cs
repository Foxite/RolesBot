using System.Reflection;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args)
	.ConfigureAppConfiguration((hostingContext, configuration) => {
		configuration.Sources.Clear();

		configuration
			.AddJsonFile("appsettings.json", true, true)
			.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
			.AddEnvironmentVariables()
			.AddCommandLine(args);
	});

hostBuilder.ConfigureServices((hbc, isc) => {
	isc.Configure<DiscordConfig>(hbc.Configuration.GetSection(nameof(DiscordConfig)));
	
	isc.AddSingleton<DiscordClient>(isp => {
		DiscordConfig discordConfig = isp.GetRequiredService<IOptions<DiscordConfig>>().Value;
		
		var discord = new DiscordClient(new DiscordConfiguration() {
			LoggerFactory = isp.GetRequiredService<ILoggerFactory>(),
			Token = discordConfig.Token,
			Intents = DiscordIntents.GuildMessageReactions | DiscordIntents.Guilds
		});

		SlashCommandsExtension commands = discord.UseSlashCommands(new SlashCommandsConfiguration() {
			Services = isp
		});
		
		commands.RegisterCommands(Assembly.GetExecutingAssembly(), discordConfig.CommandsGuild);
		
		return discord;
	});
});

IHost host = hostBuilder.Build();

var discord = host.Services.GetRequiredService<DiscordClient>();
await discord.ConnectAsync();

await host.RunAsync();

await discord.DisconnectAsync();

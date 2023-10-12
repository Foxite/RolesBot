using Microsoft.EntityFrameworkCore;

namespace RolesBot.Data; 

public class AppDbContext : DbContext {
	public DbSet<Guild> Guilds { get; set; }
}

public class Guild {
	public ulong Id { get; set; }
	
	public ICollection<GuildRoleMenu> Menus { get; set; }
}

public class GuildRoleMenu {
	public ulong Id { get; set; }
	
	public ICollection<GuildRoleMenuOption> Options { get; set; }
}

public class GuildRoleMenuOption {
	public ulong GrmId { get; set; }
	public string Emote { get; set; }
	
	public string Label { get; set; }
	public ulong RoleId { get; set; }
}

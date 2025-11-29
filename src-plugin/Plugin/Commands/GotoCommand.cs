using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Natives;

namespace K4SimpleTeleports;

public static class GotoCommand
{
	public static void OnCommand(Plugin plugin, ICommandContext ctx)
	{
		var sender = ctx.Sender;
		if (sender == null || !sender.IsValid)
			return;

		var localizer = plugin.Core.Translation.GetPlayerLocalizer(sender);

		if (ctx.Args.Length < 1)
		{
			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.usage.goto"]}");
			return;
		}

		var targets = plugin.FindTargets(sender, ctx.Args[0], false).ToList();
		if (targets.Count == 0)
		{
			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.error.no_target_alive"]}");
			return;
		}

		var target = targets.First();
		if (target.SteamID == sender.SteamID)
		{
			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.error.cannot_target_self"]}");
			return;
		}

		var pos = target.Pawn?.AbsOrigin;
		if (!pos.HasValue)
			return;

		plugin.TeleportPlayer(sender, pos.Value);
		ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.goto.success", target.GetName()]}");
	}
}

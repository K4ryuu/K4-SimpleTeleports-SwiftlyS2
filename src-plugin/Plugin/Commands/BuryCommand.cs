using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Natives;

namespace K4SimpleTeleports;

public static class BuryCommand
{
	public static void OnCommand(Plugin plugin, ICommandContext ctx)
	{
		var sender = ctx.Sender;
		if (sender == null || !sender.IsValid)
			return;

		var localizer = plugin.Core.Translation.GetPlayerLocalizer(sender);

		if (ctx.Args.Length < 1)
		{
			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.usage.bury"]}");
			return;
		}

		var targets = plugin.FindTargets(sender, ctx.Args[0]).ToList();
		if (targets.Count == 0)
		{
			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.error.no_target_alive"]}");
			return;
		}

		if (targets.Count > 1 && !Plugin.Config.CurrentValue.AllowMultipleTargets)
		{
			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.error.multiple_targets"]}");
			return;
		}

		foreach (var target in targets)
		{
			var pos = target.Pawn?.AbsOrigin;
			if (!pos.HasValue)
				continue;

			var buriedPos = new Vector(pos.Value.X, pos.Value.Y, pos.Value.Z - 25);
			plugin.TeleportPlayer(target, buriedPos);

			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.bury.success", target.GetName()]}");

			if (target.SteamID != sender.SteamID)
			{
				var targetLocalizer = plugin.Core.Translation.GetPlayerLocalizer(target);
				target.SendChat($"{targetLocalizer["k4.stp.prefix"]} {targetLocalizer["k4.stp.bury.you_were_buried", sender.GetName()]}");
			}
		}
	}
}

using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Natives;

namespace K4SimpleTeleports;

public static class TpCommand
{
	public static void OnCommand(Plugin plugin, ICommandContext ctx)
	{
		var sender = ctx.Sender;
		if (sender == null || !sender.IsValid)
			return;

		var localizer = plugin.Core.Translation.GetPlayerLocalizer(sender);

		if (ctx.Args.Length < 1)
		{
			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.usage.tp"]}");
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

		if (ctx.Args.Length >= 4)
		{
			if (!float.TryParse(ctx.Args[1], out var x) || !float.TryParse(ctx.Args[2], out var y) || !float.TryParse(ctx.Args[3], out var z))
			{
				ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.error.invalid_coordinates"]}");
				return;
			}

			var position = new Vector(x, y, z);

			foreach (var target in targets)
			{
				plugin.TeleportPlayer(target, position);

				ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.tp.player_to_coords", target.GetName(), x, y, z]}");

				if (target.SteamID != sender.SteamID)
				{
					var targetLocalizer = plugin.Core.Translation.GetPlayerLocalizer(target);
					target.SendChat($"{targetLocalizer["k4.stp.prefix"]} {targetLocalizer["k4.stp.tp.you_were_teleported_to_coords", x, y, z]}");
				}
			}
		}
		else if (ctx.Args.Length >= 2)
		{
			var destTargets = plugin.FindTargets(sender, ctx.Args[1], false).ToList();
			if (destTargets.Count == 0)
			{
				ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.error.no_target_alive"]}");
				return;
			}

			var dest = destTargets.First();
			var destPos = dest.Pawn?.AbsOrigin;
			if (!destPos.HasValue)
				return;

			var position = new Vector(destPos.Value.X, destPos.Value.Y, destPos.Value.Z);

			foreach (var target in targets)
			{
				plugin.TeleportPlayer(target, position);

				ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.tp.player_to_player", target.GetName(), dest.GetName()]}");

				if (target.SteamID != sender.SteamID)
				{
					var targetLocalizer = plugin.Core.Translation.GetPlayerLocalizer(target);
					target.SendChat($"{targetLocalizer["k4.stp.prefix"]} {targetLocalizer["k4.stp.tp.you_were_teleported_to_player", dest.GetName()]}");
				}
			}
		}
		else
		{
			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.usage.tp"]}");
		}
	}
}

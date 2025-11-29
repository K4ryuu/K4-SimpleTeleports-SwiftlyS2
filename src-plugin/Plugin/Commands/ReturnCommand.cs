using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Natives;

namespace K4SimpleTeleports;

public static class ReturnCommand
{
	public static void OnCommand(Plugin plugin, ICommandContext ctx)
	{
		var sender = ctx.Sender;
		if (sender == null || !sender.IsValid)
			return;

		var localizer = plugin.Core.Translation.GetPlayerLocalizer(sender);

		if (ctx.Args.Length < 1)
		{
			var savedPos = plugin.GetSavedPosition(sender);
			if (!savedPos.HasValue)
			{
				ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.error.no_saved_position"]}");
				return;
			}

			sender.Teleport(savedPos.Value, new QAngle(0, 0, 0), new Vector(0, 0, 0));
			plugin.SavedPositions.Remove(sender.SteamID);

			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.return.self_success"]}");
			return;
		}

		var targets = plugin.FindTargets(sender, ctx.Args[0]).ToList();
		if (targets.Count == 0)
		{
			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.error.no_target_alive"]}");
			return;
		}

		if (targets.Count > 1 && !plugin.Config.AllowMultipleTargets)
		{
			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.error.multiple_targets"]}");
			return;
		}

		foreach (var target in targets)
		{
			var savedPos = plugin.GetSavedPosition(target);
			if (!savedPos.HasValue)
			{
				ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.error.no_saved_position"]}");
				continue;
			}

			target.Teleport(savedPos.Value, new QAngle(0, 0, 0), new Vector(0, 0, 0));
			plugin.SavedPositions.Remove(target.SteamID);

			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.return.success", target.GetName()]}");

			if (target.SteamID != sender.SteamID)
			{
				var targetLocalizer = plugin.Core.Translation.GetPlayerLocalizer(target);
				target.SendChat($"{targetLocalizer["k4.stp.prefix"]} {targetLocalizer["k4.stp.return.you_were_returned"]}");
			}
		}
	}
}

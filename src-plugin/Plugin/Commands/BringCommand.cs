using SwiftlyS2.Shared.Commands;

namespace K4SimpleTeleports;

public static class BringCommand
{
	public static void OnCommand(Plugin plugin, ICommandContext ctx)
	{
		var sender = ctx.Sender;
		if (sender == null || !sender.IsValid)
			return;

		var localizer = plugin.Core.Translation.GetPlayerLocalizer(sender);

		if (ctx.Args.Length < 1)
		{
			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.usage.bring"]}");
			return;
		}

		var aimPos = plugin.GetAimPosition(sender);
		if (!aimPos.HasValue)
		{
			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.error.invalid_coordinates"]}");
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
			plugin.TeleportPlayer(target, aimPos.Value);

			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.bring.success", target.GetName()]}");

			if (target.SteamID != sender.SteamID)
			{
				var targetLocalizer = plugin.Core.Translation.GetPlayerLocalizer(target);
				target.SendChat($"{targetLocalizer["k4.stp.prefix"]} {targetLocalizer["k4.stp.bring.you_were_brought", sender.GetName()]}");
			}
		}
	}
}

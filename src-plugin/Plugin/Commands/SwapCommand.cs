using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Natives;

namespace K4SimpleTeleports;

public static class SwapCommand
{
	public static void OnCommand(Plugin plugin, ICommandContext ctx)
	{
		var sender = ctx.Sender;
		if (sender == null || !sender.IsValid)
			return;

		var localizer = plugin.Core.Translation.GetPlayerLocalizer(sender);

		if (ctx.Args.Length < 2)
		{
			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.usage.swap"]}");
			return;
		}

		var targets1 = plugin.FindTargets(sender, ctx.Args[0], false).ToList();
		var targets2 = plugin.FindTargets(sender, ctx.Args[1], false).ToList();

		if (targets1.Count == 0 || targets2.Count == 0)
		{
			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.error.no_target_alive"]}");
			return;
		}

		var player1 = targets1.First();
		var player2 = targets2.First();

		if (player1.SteamID == player2.SteamID)
		{
			ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.error.need_two_targets"]}");
			return;
		}

		var pos1 = player1.Pawn?.AbsOrigin;
		var pos2 = player2.Pawn?.AbsOrigin;
		var angles1 = player1.PlayerPawn?.EyeAngles ?? new QAngle(0, 0, 0);
		var angles2 = player2.PlayerPawn?.EyeAngles ?? new QAngle(0, 0, 0);

		if (!pos1.HasValue || !pos2.HasValue)
			return;

		var position1 = new Vector(pos1.Value.X, pos1.Value.Y, pos1.Value.Z);
		var position2 = new Vector(pos2.Value.X, pos2.Value.Y, pos2.Value.Z);

		plugin.SavePosition(player1);
		plugin.SavePosition(player2);

		player1.Teleport(position2, angles2, new Vector(0, 0, 0));
		player2.Teleport(position1, angles1, new Vector(0, 0, 0));

		ctx.Reply($"{localizer["k4.stp.prefix"]} {localizer["k4.stp.swap.success", player1.GetName(), player2.GetName()]}");

		var p1Localizer = plugin.Core.Translation.GetPlayerLocalizer(player1);
		player1.SendChat($"{p1Localizer["k4.stp.prefix"]} {p1Localizer["k4.stp.swap.you_were_swapped", player2.GetName()]}");

		var p2Localizer = plugin.Core.Translation.GetPlayerLocalizer(player2);
		player2.SendChat($"{p2Localizer["k4.stp.prefix"]} {p2Localizer["k4.stp.swap.you_were_swapped", player1.GetName()]}");
	}
}

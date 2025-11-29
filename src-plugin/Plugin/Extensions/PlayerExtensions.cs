using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace K4SimpleTeleports;

public static class PlayerExtensions
{
	public static bool IsAlive(this IPlayer player)
	{
		return player.PlayerPawn?.LifeState == (byte)LifeState_t.LIFE_ALIVE;
	}

	public static string GetName(this IPlayer player)
	{
		return player.Controller?.PlayerName ?? "Unknown";
	}
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Plugins;

namespace K4SimpleTeleports;

[PluginMetadata(Id = "k4.simpleteleports", Version = "1.0.2", Name = "K4 - Simple Teleports", Author = "K4ryuu", Description = "Simple teleport commands for Counter-Strike: 2 using SwiftlyS2 framework.")]
public sealed partial class Plugin(ISwiftlyCore core) : BasePlugin(core)
{
	private const string ConfigFileName = "config.json";
	private const string ConfigSection = "K4SimpleTeleports";

	internal new ISwiftlyCore Core => base.Core;
	internal static IOptionsMonitor<PluginConfig> Config { get; private set; } = null!;
	internal readonly Dictionary<ulong, Vector> SavedPositions = [];

	public override void Load(bool hotReload)
	{
		Core.Configuration
			.InitializeJsonWithModel<PluginConfig>(ConfigFileName, ConfigSection)
			.Configure(builder =>
			{
				builder.AddJsonFile(ConfigFileName, optional: false, reloadOnChange: true);
			});

		ServiceCollection services = new();
		services.AddSwiftly(Core)
			.AddOptionsWithValidateOnStart<PluginConfig>()
			.BindConfiguration(ConfigSection);

		var provider = services.BuildServiceProvider();
		Config = provider.GetRequiredService<IOptionsMonitor<PluginConfig>>();

		// Events
		Core.GameEvent.HookPost<EventRoundStart>(OnRoundStart);
		Core.Event.OnMapUnload += OnMapUnload;

		// Commands
		RegisterCommand(Config.CurrentValue.Commands.Tp, TpCommand.OnCommand);
		RegisterCommand(Config.CurrentValue.Commands.Goto, GotoCommand.OnCommand);
		RegisterCommand(Config.CurrentValue.Commands.Bring, BringCommand.OnCommand);
		RegisterCommand(Config.CurrentValue.Commands.TpHere, TpHereCommand.OnCommand);
		RegisterCommand(Config.CurrentValue.Commands.Swap, SwapCommand.OnCommand);
		RegisterCommand(Config.CurrentValue.Commands.Return, ReturnCommand.OnCommand);
		RegisterCommand(Config.CurrentValue.Commands.Bury, BuryCommand.OnCommand);
		RegisterCommand(Config.CurrentValue.Commands.Unbury, UnburyCommand.OnCommand);
	}

	public override void Unload()
	{
		SavedPositions.Clear();
	}

	private HookResult OnRoundStart(EventRoundStart ev)
	{
		SavedPositions.Clear();
		return HookResult.Continue;
	}

	private void OnMapUnload(IOnMapUnloadEvent ev)
	{
		SavedPositions.Clear();
	}

	private void RegisterCommand(CommandConfig config, Action<Plugin, ICommandContext> handler)
	{
		if (string.IsNullOrEmpty(config.Command))
			return;

		Core.Command.RegisterCommand(config.Command, ctx => handler(this, ctx), permission: config.Permission);

		foreach (var alias in config.Aliases)
		{
			Core.Command.RegisterCommandAlias(config.Command, alias);
		}
	}

	#region Helpers

	internal void SavePosition(IPlayer player)
	{
		var pawn = player.Pawn;
		if (pawn == null)
			return;

		var pos = pawn.AbsOrigin;
		if (pos.HasValue)
		{
			SavedPositions[player.SteamID] = new Vector(pos.Value.X, pos.Value.Y, pos.Value.Z);
		}
	}

	internal Vector? GetSavedPosition(IPlayer player)
	{
		return SavedPositions.TryGetValue(player.SteamID, out var pos) ? pos : null;
	}

	internal void TeleportPlayer(IPlayer player, Vector position)
	{
		SavePosition(player);
		player.Teleport(position, player.PlayerPawn?.EyeAngles ?? new QAngle(0, 0, 0), new Vector(0, 0, 0));
	}

	internal IEnumerable<IPlayer> FindTargets(IPlayer sender, string target, bool allowMultiple = true, bool requireAlive = true)
	{
		var searchMode = TargetSearchMode.IncludeSelf;

		if (!allowMultiple || !Config.CurrentValue.AllowMultipleTargets)
		{
			searchMode |= TargetSearchMode.NoMultipleTargets;
		}

		if (requireAlive)
		{
			searchMode |= TargetSearchMode.Alive;
		}

		return Core.PlayerManager.FindTargettedPlayers(sender, target, searchMode);
	}

	internal Vector? GetAimPosition(IPlayer player)
	{
		var pawn = player.PlayerPawn;
		if (pawn == null)
			return null;

		var eyePos = pawn.EyePosition;
		if (!eyePos.HasValue)
			return null;

		pawn.EyeAngles.ToDirectionVectors(out var forward, out _, out _);

		var startPos = new Vector(eyePos.Value.X, eyePos.Value.Y, eyePos.Value.Z);
		var endPos = startPos + forward * 8192;

		var trace = new CGameTrace();
		Core.Trace.SimpleTrace(
			startPos,
			endPos,
			RayType_t.RAY_TYPE_LINE,
			RnQueryObjectSet.Static | RnQueryObjectSet.Dynamic,
			MaskTrace.Solid | MaskTrace.Player,
			MaskTrace.Empty,
			MaskTrace.Empty,
			CollisionGroup.Player,
			ref trace,
			pawn
		);

		if (trace.Fraction < 1.0f)
		{
			return new Vector(trace.EndPos.X, trace.EndPos.Y, trace.EndPos.Z + 10);
		}

		return null;
	}

	#endregion
}

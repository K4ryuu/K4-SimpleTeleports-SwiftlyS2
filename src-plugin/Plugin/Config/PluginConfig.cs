namespace K4SimpleTeleports;

public sealed partial class Plugin
{
	public sealed class PluginConfig
	{
		/// <summary>Allow targeting multiple players with a single command</summary>
		public bool AllowMultipleTargets { get; set; } = true;

		/// <summary>Command settings</summary>
		public CommandSettings Commands { get; set; } = new();
	}

	public sealed class CommandSettings
	{
		/// <summary>Teleport command config</summary>
		public CommandConfig Tp { get; set; } = new()
		{
			Command = "tp",
			Aliases = ["teleport"],
			Permission = "k4-stp.tp"
		};

		/// <summary>Goto command config</summary>
		public CommandConfig Goto { get; set; } = new()
		{
			Command = "goto",
			Aliases = [],
			Permission = "k4-stp.goto"
		};

		/// <summary>Bring command config</summary>
		public CommandConfig Bring { get; set; } = new()
		{
			Command = "bring",
			Aliases = [],
			Permission = "k4-stp.bring"
		};

		/// <summary>TpHere command config</summary>
		public CommandConfig TpHere { get; set; } = new()
		{
			Command = "tphere",
			Aliases = [],
			Permission = "k4-stp.tphere"
		};

		/// <summary>Swap command config</summary>
		public CommandConfig Swap { get; set; } = new()
		{
			Command = "swap",
			Aliases = [],
			Permission = "k4-stp.swap"
		};

		/// <summary>Return command config</summary>
		public CommandConfig Return { get; set; } = new()
		{
			Command = "return",
			Aliases = ["back"],
			Permission = "k4-stp.return"
		};

		/// <summary>Bury command config</summary>
		public CommandConfig Bury { get; set; } = new()
		{
			Command = "bury",
			Aliases = [],
			Permission = "k4-stp.bury"
		};

		/// <summary>Unbury command config</summary>
		public CommandConfig Unbury { get; set; } = new()
		{
			Command = "unbury",
			Aliases = [],
			Permission = "k4-stp.unbury"
		};
	}

	public sealed class CommandConfig
	{
		/// <summary>Primary command name</summary>
		public string Command { get; set; } = "";

		/// <summary>Command aliases</summary>
		public List<string> Aliases { get; set; } = [];

		/// <summary>Required permission</summary>
		public string Permission { get; set; } = "";
	}
}

# Changelog

## [1.0.2] - 2026-02-11

### Fixed

- **CRITICAL**: Fixed configuration binding bug in `Load()` method
  - Changed `.BindConfiguration(ConfigFileName)` to `.BindConfiguration(ConfigSection)`
  - This bug caused all config values to use hardcoded defaults instead of reading from `config.json`
  - **Impact**: Plugin configuration was completely non-functional - all command settings, permissions, and multi-target settings were ignored

## [1.0.1] - 2025-12-12

### Changed

- Migrated configuration system from `IOptions<PluginConfig>` to `IOptionsMonitor<PluginConfig>` for live config reloading support
- Configuration values are now accessed via `Config.CurrentValue` pattern for consistency with SwiftlyS2 best practices
- Made `Config` property static for easier access across command classes
- Moved config constants (`ConfigFileName`, `ConfigSection`) to class-level fields

## [1.0.0] - 2025-12-??

### Added

- Initial release
- Teleport commands: tp, goto, bring, tphere, swap, return, bury, unbury
- Multi-target support with configurable toggle
- Position saving and return functionality
- Aim-based teleportation for bring command
- Round start position clearing
- Configurable command aliases and permissions

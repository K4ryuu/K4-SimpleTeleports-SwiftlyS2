<a name="readme-top"></a>

![GitHub tag (with filter)](https://img.shields.io/github/v/tag/K4ryuu/K4-SimpleTeleports-SwiftlyS2?style=for-the-badge&label=Version)
![GitHub Repo stars](https://img.shields.io/github/stars/K4ryuu/K4-SimpleTeleports-SwiftlyS2?style=for-the-badge)
![GitHub issues](https://img.shields.io/github/issues/K4ryuu/K4-SimpleTeleports-SwiftlyS2?style=for-the-badge)
![GitHub](https://img.shields.io/github/license/K4ryuu/K4-SimpleTeleports-SwiftlyS2?style=for-the-badge)
![GitHub all releases](https://img.shields.io/github/downloads/K4ryuu/K4-SimpleTeleports-SwiftlyS2/total?style=for-the-badge)
[![Discord](https://img.shields.io/badge/Discord-Join%20Server-5865F2?style=for-the-badge&logo=discord&logoColor=white)](https://dsc.gg/k4-fanbase)

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <h1 align="center">KitsuneLab©</h1>
  <h3 align="center">K4 - SimpleTeleports</h3>
  <a align="center">A simple teleport commands plugin for Counter-Strike 2. Allows admins to teleport players, swap positions, go to players, and more. I've used them on my own servers and find them very useful.</a>

  <p align="center">
    <br />
    <a href="https://github.com/K4ryuu/K4-SimpleTeleports-SwiftlyS2/releases/latest">Download</a>
    ·
    <a href="https://github.com/K4ryuu/K4-SimpleTeleports-SwiftlyS2/issues/new?assignees=K4ryuu&labels=bug&projects=&template=bug_report.md&title=%5BBUG%5D">Report Bug</a>
    ·
    <a href="https://github.com/K4ryuu/K4-SimpleTeleports-SwiftlyS2/issues/new?assignees=K4ryuu&labels=enhancement&projects=&template=feature_request.md&title=%5BREQ%5D">Request Feature</a>
  </p>
</div>

### Support My Work

I create free, open-source projects for the community. While not required, donations help me dedicate more time to development and support. Thank you!

<p align="center">
  <a href="https://paypal.me/k4ryuu"><img src="https://img.shields.io/badge/PayPal-00457C?style=for-the-badge&logo=paypal&logoColor=white" /></a>
  <a href="https://revolut.me/k4ryuu"><img src="https://img.shields.io/badge/Revolut-0075EB?style=for-the-badge&logo=revolut&logoColor=white" /></a>
</p>

### Dependencies

To use this server addon, you'll need the following dependencies installed:

- [**SwiftlyS2**](https://github.com/swiftly-solution/swiftlys2): SwiftlyS2 is a server plugin framework for Counter-Strike 2

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- INSTALLATION -->

## Installation

1. Install [SwiftlyS2](https://github.com/swiftly-solution/swiftlys2) on your server
2. [Download the latest release](https://github.com/K4ryuu/K4-SimpleTeleports-SwiftlyS2/releases/latest)
3. Extract to your server's `swiftlys2/plugins/` directory

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONFIGURATION -->

## Configuration

| Option                 | Description                                       | Default |
| ---------------------- | ------------------------------------------------- | ------- |
| `AllowMultipleTargets` | Allow targeting multiple players with one command | `true`  |

### Command Settings

Each command can be configured with:

- `Command` - Primary command name (leave empty to disable the command)
- `Aliases` - Alternative command names
- `Permission` - Required permission to use

> **Note:** If you leave the `Command` field empty, the command will not be registered at all.

| Command  | Default Aliases | Default Permission |
| -------- | --------------- | ------------------ |
| `tp`     | `teleport`      | `k4-stp.tp`        |
| `goto`   | -               | `k4-stp.goto`      |
| `bring`  | -               | `k4-stp.bring`     |
| `tphere` | -               | `k4-stp.tphere`    |
| `swap`   | -               | `k4-stp.swap`      |
| `return` | `back`          | `k4-stp.return`    |
| `bury`   | -               | `k4-stp.bury`      |
| `unbury` | -               | `k4-stp.unbury`    |

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- COMMANDS -->

## Commands

| Command                       | Description                                    |
| ----------------------------- | ---------------------------------------------- |
| `!tp <target> [target2/x y z]`| Teleport target to player or coordinates       |
| `!goto <target>`              | Teleport yourself to target player             |
| `!bring <target>`             | Bring target player to your aim position       |
| `!tphere <target>`            | Teleport target player to your position        |
| `!swap <target1> <target2>`   | Swap positions (and angles) of two players     |
| `!return [target]`            | Return yourself or target to previous position |
| `!bury <target>`              | Bury target player (move down 25 units)        |
| `!unbury <target>`            | Unbury target player (move up 25 units)        |

### Target Selectors

| Selector | Description                    |
| -------- | ------------------------------ |
| `@all`   | All players                    |
| `@me`    | Yourself                       |
| `@!me`   | Everyone except yourself       |
| `@ct`    | All Counter-Terrorists         |
| `@t`     | All Terrorists                 |
| `@spec`  | All Spectators                 |
| `@alive` | All alive players              |
| `@dead`  | All dead players               |
| `@bots`  | All bots                       |
| `@!bots` | All human players              |
| `@aim`   | Player you're looking at       |
| `#<id>`  | Player by ID                   |
| `<name>` | Player by name (partial match) |

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- LICENSE -->

## License

Distributed under the GPL-3.0 License. See [`LICENSE.md`](LICENSE.md) for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

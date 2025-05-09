# Longplay Mod

Longplay is a mod for the game **Schedule I** that enhances gameplay by allowing users to customize growth times for plants and cooking durations for recipes. This mod is built using the [MelonLoader](https://melonwiki.xyz/#/) framework and leverages Harmony for runtime patching.

---

## Features

- **Customizable Plant Growth Time**: Adjust the growth time of plants to suit your gameplay style.
- **Customizable Recipe Cook Time**: Modify the cooking duration for recipes in the Chemistry Station.
- **Configurable Settings**: Easily configure growth and cook times via a JSON file.
- **Seamless Integration**: Automatically applies changes when the game starts.

---

## Installation

1. **Install MelonLoader**:
   - Download and install [MelonLoader](https://melonwiki.xyz/#/).
   
2. **Download the Mod**:
   - Clone or download this repository and extract the files.

3. **Place the Mod**:
   - Copy the `Longplay.dll` file into the `Mods` folder of your game directory.

4. **Run the Game**:
   - Launch the game, and the mod will automatically initialize.

---

## Configuration

The mod uses a configuration file named `LongplayConfig.json` located in the `UserData` directory of your game. If the file does not exist, it will be created with default values.

### Default Configuration:
{ "GrowthTime": 60, "CookDuration": 960 }

### How to Edit:
- Open `LongplayConfig.json` in a text editor.
- Modify the `GrowthTime` (in minutes) and `CookDuration` (in minutes) values as desired.
- Save the file and restart the game for changes to take effect.

---

## Usage

- **Plant Growth Time**: The growth time of plants is patched during their initialization.
- **Recipe Cook Time**: The cooking duration for recipes is patched when a cook operation is set in the Chemistry Station.

---

## Development

### Code Overview

- **Core.cs**:
  - Contains the main mod logic.
  - Patches the `Plant.Initialize` and `ChemistryStation.SetCookOperation` methods using Harmony.
  - Loads and applies configuration settings.

### Key Classes:
- `Core`: The main entry point for the mod.
- `PlantPatches`: Adjusts plant growth times.
- `ChemistryStationPatches`: Adjusts recipe cook times.

---

## Requirements

- **MelonLoader**: v0.5.7 or higher
- **.NET 6 Runtime**
- **Game**: Schedule I

---

## Troubleshooting

- **Error: `System.AccessViolationException`**:
  - Ensure the game and mod are compatible with the latest version of MelonLoader.
  - Verify that the `LongplayConfig.json` file is correctly formatted.

- **Mod Not Working**:
  - Check the MelonLoader logs for errors.
  - Ensure the mod is placed in the correct `Mods` folder.

---

## Contributing

Huge thanks to my good people over at the modding Discord for Schedule 1 for their love and support.
Massive thanks to Max on Discord for suggesting I use Mono instead of IL2Cpp. That helped a ton.

Thanks a bunch to Estonia, whose mod SpeedGrow I looked at to start my project, go check their mod out on Thunderstore:
https://thunderstore.io/c/schedule-i/p/Estonia/SpeedGrow/source/

I also borrowed code from DeliveryDash mod for the Harmony setup, so thanks to the author Hyde.
https://thunderstore.io/c/schedule-i/p/Hyde/DeliveryDash/

Contributions are welcome! Feel free to submit issues or pull requests to improve the mod.

---

## Declaration of AI usage

Large portions of this code was generated with the help of Copilot autocomplete, but the logic was my own.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.

---

## Credits

- **Author**: Freshairkaboom
- **Frameworks Used**:
  - [MelonLoader](https://melonwiki.xyz/#/)
  - [Harmony](https://harmony.pardeike.net/)
- **Game**: Schedule I by TVGS

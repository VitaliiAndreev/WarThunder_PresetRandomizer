**War Thunder Preset Randomizer** (or **WTPR**) is a work-in-progress Windows application with the primary function of providing a player with means to randomly select vehicles to play, with extra features down the road. In its current iteration (Beta) randomisation is implemented based on game mode, vehicle branch and branch tags, vehicle class and subclass, nation, country of origin, rank and battle rating, and individual vehicle preferences. While presets are generated by **WTPR**, they still have to be assembled in-game.

The secondary function of the app is display, categorisation and aggregation of some of data available in game files. **WTPR** recreates full vehicle research trees (including playable hidden vehicles), displays vehicle availability, counts and lists vehicles by categories. Generated lists can be copied and pasted into spreadsheets for further user processing.

There's a YouTube playlist for related information: https://www.youtube.com/playlist?list=PLTkOsj0Uogp4z4Px8IrmZIl_z6M60mmqX

### Requirements

In order to work **WTPR** requires an up-to-date version of the **War Thunder** client available at [Gaijin's website](https://warthunder.com/en) or [Steam](https://store.steampowered.com/app/236390/War_Thunder/), and a release of [Klensy](https://github.com/klensy/wt-tools/commits?author=klensy)'s **[WT Tools](https://github.com/klensy/wt-tools)**. Paths to both are inquired at the start of **WTPR**. Additionally, one might need to install a runtime version of [.NET Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework/net472).

### Application modes

Releases come packaged with BAT files that run the app in different modes using a set of statrup arguments:
- Mode 1: default (no arguments) - every start up-to-date game files are unpacked and deserialised. It's the second fastest mode.
- Mode 2: (-!w): game files already unpacked (into the temp folder) are deserialised. No new data is being pulled from the game. It's the fastest mode.
- Mode 3: (-dbw): same as mode 1, but also an SQLite database is generated. It's the third fastest mode, same as mode 1 if an up-to-date database is already generated.
- Mode 4: (-!w -!r -dbr -dbw): every start up-to-date game files are unpacked and deserialised from which an SQLite database is generated. If an up-to-date database is already generated, it's loaded instead. It's the second slowest mode, same as mode 5 if an up-to-date database is already generated while same as mode 3 if it isn't.
- Mode 5: (-!w -!r -dbr): the most recently generated SQLite database is read. It's the slowest mode.

### User settings

*Client.Wpf.Settings.xml* stores user preferences. While it can be edited manually, it shouldn't be necessary to do so.

### GUI tips

- Numbers on buttons to the right of icons indicate that there are filters attached to the context menu (RMB) - the left number is the amount of items toggled on and the right one shows how many there are in total.
- Hovering over game mode, vehicle class, nation, and country icons shows clarifications.
- Battle ratings can be adjusted either by clicking arrows or scrolling with the mouse wheel.
- Hovering over a vehicle in the research tree highlights the required vehicle.
- Hovering over a vehicle in a preset highlights the former in the research tree.
- Clicking a vehicle in the preset opens the research tree tab it's on (if necessary) and scrolls it to bring the vehicle into view.
- Clicking a vehicle in the research tree toggles it on/off which affects whether it's used in randomisation.
- Presets need to be scrapped (the button with a recycle icon) before research trees are unlocked for free browsing.
- The right button on the preset panel switches between the primary and secondary generated presets if there are enough vehicles.

### Updating to new releases

New releases are shipped ready-to-go. The previous release should be deleted prior to extracting release files.

If you want to keep user preferences from the previous release, copy *Client.Wpf.Settings.xml* from the previous release to the folder with the new release. Applicable settings would be carried over on the first start. Use the backup (*Client.Wpf.Settings.xml.bak*) to restore individual settings in case of possible conflicts. The user is notified when issues are resolved, after which a restart is required to continue.

You can also copy over any SQLite databases generated with the previous version of the client, but compatibility with newer versions of the app is not guaranteed.

### Randomisation criteria

The following criteria order is used (combinations producing empty presets are meant to be minimised, except for the case of setting too narrow battle rating preferences):
- Enabled vehicles
- Enabled branches.
- Enabled branch tags (e.g. naval aircraft). At the moment branch tags are directly derived from those found in unittags.blkx. There are plans to expand those based on available vehicle and weapon data.
- Enabled vehicle classes.
- Enabled vehicle subclasses.
- Enabled nations.
- Enabled countries.
- Enabled ranks.
- Enabled battle ratings.
- Enabled vehicles (individually).

Preset compositions are based on whether the main selected branch involves combined battles and which branches are enabled (except helicopters in Realistic or Simulator Battles where they require spawn points primarily earned by ground vehicles).

### Randomisation algorithms

The following algorithms can be chosen from:
- Top-down (by category). Applies minor filters and randomly selects from toggled on major filter categories (branch, nation, battle rating) before selecting available vehicles.
- Bottom-up (by vehicle). Randomly selects the main vehicle from those enabled by filters, building presets around it.

### Road map

#### Pre-Alpha releases (reached)
- ~~Setting up the framework.~~
- ~~Prototyping.~~
- ~~Visualisation of reseach trees with available vehicles.~~

#### Alpha releases (reached)
- ~~Baseline randomisation.~~

#### Beta releases (reached)
- ~~Extended randomisation: toggles for nations, countries (Australia, post-war Germany, Israel, etc.), branches, battle ratings, ranks, vehicle sub-types, individual vehicles, etc.~~
- Display of more vehicle information: what is seen on vehicle cards in-game, and beyond.
- Filter expansion based on previously unused data (weapon types, calibers, payloads, reload times, etc.).
- Vehicle lists, aggregation of vehicle data.
- Improved presentation.

#### Production releases
- Visualisation of data changes between patches.

### Bonuses

Releases come packaged with a tool that automates calls to Klensy's WT Tools for an one-button solution of unpacking available game data and cleaning up afterward. That's how [JSON File Changes](https://github.com/VitaliiAndreev/WarThunder_JsonFileChanges) and [JSON File Changes (Dev)](https://github.com/VitaliiAndreev/WarThunder_JsonFileChanges_DevClient) repositories are maintained. The tool requires three startup arguments: the path to War Thunder, the path to Klensy's WT Tools, and the output path. There are example BAT files in the package that show how to call it. See "Argument Reference.txt" for optional arguments.

Another bonuses are utility LINQPad scripts for querying JSON, code generation, etc, as well as SQLite queries (see *Application modes*) that should work even if some tweaks might be required.
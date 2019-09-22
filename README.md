**War Thunder Preset Randomizer** (or **WTPR**) is a work-in-progress application intended to provide a player with means to randomly select vehicles to play, with extra features down the road. In its current iteration (Pre-Alpha) it only displays all vehicles avalable in the game, except those related to April Fool's events.

The source code contains a prototype console application that picks random vehicles within a selected game mode, nation, branch, and battle rating. There's also a utility that automates **WT Tools** (see *Requirements*) to perform a full cycle of unpacking and converting game files into JSON and CSV files - that's how [JSON File Changes](https://github.com/VitaliiAndreev/WarThunder_JsonFileChanges) and [JSON File Changes (Dev)](https://github.com/VitaliiAndreev/WarThunder_JsonFileChanges_DevClient) repositories are maintained.

### Requirements
In order to work **WTPR** requires an up-to-date version of the **War Thunder** client available at [Gaijin's website](https://warthunder.com/en) or [Steam](https://store.steampowered.com/app/236390/War_Thunder/), and a release of [Klensy](https://github.com/klensy/wt-tools/commits?author=klensy)'s **[WT Tools](https://github.com/klensy/wt-tools)**. Paths to both are inquired at the start of **WTPR**. Additionally, one might need to install a runtime version of [.NET Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework/net472).

### Workflow

At the start **WTPR** scans the **War Thunder** client for its current version. With every new patch **WT Tools** are used to unpack data stored with the client and convert it into JSON and CSV, **WTPR** deserializes that data into entities and stores them in an SQLite database (a new database for every patch). From that point **WTPR** uses only the database unless it detects a new **War Thunder** version.

### Road Map

#### Pre-Alpha releases (reached)
- Setting up the framework.
- Prototyping.
- Visualisation of reseach trees with available vehicles.

#### Alpha releases
- Baseline randomisation.

#### Beta releases
- Extended randomisation: toggles for nations, countries (Australia, post-war Germany, Israel, etc.), branches, battle ratings, vehicle sub-types, individual vehicles, etc.
- Extended vehicle information: what is seen on vehicle cards in-game, and beyond.
- Aggregation of vehicle data.
- Improved presentation.

#### Production releases
- Visualisation of data changes between patches.

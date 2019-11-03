<Query Kind="Program">
  <Reference Relative="..\Core.DataBase.Tests\bin\Debug\Core.DataBase.Tests.dll">D:\Code\Source\_Repositories\WarThunderPresetRandomizer\Core.DataBase.Tests\bin\Debug\Core.DataBase.Tests.dll</Reference>
  <Reference Relative="..\Core\bin\Debug\Core.dll">D:\Code\Source\_Repositories\WarThunderPresetRandomizer\Core\bin\Debug\Core.dll</Reference>
  <Reference Relative="..\Core.Json\bin\Debug\Core.Json.dll">D:\Code\Source\_Repositories\WarThunderPresetRandomizer\Core.Json\bin\Debug\Core.Json.dll</Reference>
  <Reference Relative="..\Core.Json.WarThunder\bin\Debug\Core.Json.WarThunder.dll">D:\Code\Source\_Repositories\WarThunderPresetRandomizer\Core.Json.WarThunder\bin\Debug\Core.Json.WarThunder.dll</Reference>
  <Reference Relative="..\Core.Tests\bin\Debug\Core.Tests.dll">D:\Code\Source\_Repositories\WarThunderPresetRandomizer\Core.Tests\bin\Debug\Core.Tests.dll</Reference>
  <Reference Relative="..\Core.Json\bin\Debug\Newtonsoft.Json.dll">D:\Code\Source\_Repositories\WarThunderPresetRandomizer\Core.Json\bin\Debug\Newtonsoft.Json.dll</Reference>
  <Namespace>Core.Extensions</Namespace>
  <Namespace>Core.Json.Extensions</Namespace>
  <Namespace>Core.Json.Helpers</Namespace>
  <Namespace>Core.Tests</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

void Main()
{
	var tags = new HashSet<string>();
	var lines = File.ReadAllText(@"D:\Code\Source\_Repositories\WarThunderJsonFileChanges\Files\char.vromfs.bin_u\config\unittags.blkx").Split('\n');
	
	for (var i = 0; i < lines.Count(); i++)
	{
		string getLine(int index) => lines[index];
		string getNextLine(ref int index) { return lines[++index]; }
	
		var line = getLine(i);
		
		if (line.StartsWith("    \"tags\": {"))
		{
			bool endWritingTags(string l) => l.StartsWith("    }");
		
			while (!endWritingTags(line))
			{
				line = getNextLine(ref i);
				
				if (endWritingTags(line)) break;
				
				tags.Add(line.Split('"')[1]);
			}
		}
	}
	
	var jsonMappingCode = new List<List<string>>();
	var classCode = new List<string>();
	var interfaceCode = new List<string>();
	foreach (var tag in tags)
	{
		var propertyName = GetPropertyName(tag);
		jsonMappingCode.Add
		(
			new List<string>
			{
				$"[JsonProperty(\"{tag}\")]",
				$"public bool {propertyName}" + " { get; set; }"
			}
		);
		interfaceCode.Add($"bool {propertyName}" + " { get; }");
		classCode.Add($"[Property()] public virtual bool {propertyName}" + " { get; protected set; }");
	}
	
	//string.Join("\n\n", jsonMappingCode.Select(item => string.Join("\n", item))).Dump();
	//string.Join("\n\n", interfaceCode.Select(item => string.Join("\n", item))).Dump();
	string.Join("\n\n", classCode.Select(item => string.Join("\n", item))).Dump();
}

HashSet<string> GetVehicleProperties(IEnumerable<string> lines)
{
	var vehicleProperties = new HashSet<string>();
	
	foreach (var line in lines)
	{
		if (line.StartsWith("    \""))
			vehicleProperties.Add(line.Split(new char[] { ':' }).First().Replace("\"", "").Trim());
	}
	
	return vehicleProperties;
}

public static class Extensions
{
	public static TextReader CreateTextReader(this string sourceString)
	{
	    var memoryStream = new MemoryStream();
	    var streamWriter = new StreamWriter(memoryStream);
	
	    streamWriter.Write(sourceString);
	    streamWriter.Flush();
	    memoryStream.Position = 0;
	
	    return new StreamReader(memoryStream);
	}
	
	public static string[] Split(this string sourceString, string separator) =>
        sourceString.Split(new string[] { separator }, StringSplitOptions.None);

    public static IEnumerable<string> GetNestedPropertyNames(this JObject jsonObject, string propertyName)
    {
        var propertyNames = new HashSet<string>();

        var weapons = jsonObject[propertyName] as JObject;
		
        foreach (var weaponObject in weapons)
        {
            foreach (var property in weaponObject.Value)
                if (property is JProperty jp)
                    propertyNames.Add(jp.Name);
        }

        return propertyNames;
    }
}

public string GetPropertyName(string tag)
{
	return tag
		.Split("_")
		.Select(s => $"{s.First().ToString().ToUpper()}{string.Join("", s.Skip(1))}")
		.Aggregate((s1, s2) => $"{s1}{s2}")
		.Replace("NotInDynamicCampaign", "NotAvailableInDynamicCampaign")
		.Replace("Type", "Is")
		.Replace("Air", "IsAirVehicle")
		.Replace("Hydroplane", "IsHydroplane_")
		.Replace("IsIsHydroplane_", "IsHydroplane")
		.Replace("Fw190Series", "IsFw190")
		.Replace("IsAaFighter", "IsNightFighter")
		.Replace("IsAssault", "IsAttacker")
		.Replace("IsTorpedo", "CanCarryTorpedoes")
		.Replace("IsLongrangeBomber", "IsLongRangeBomber")
		.Replace("Bomberview", "HasBomberView")
		.Replace("Tank", "IsTank")
		.Replace("IsLightIsTank", "IsLightTank")
		.Replace("IsMediumIsTank", "IsMediumTank")
		.Replace("IsHeavyIsTank", "IsHeavyTank")
		.Replace("IsMissileIsTank", "IsMissileTank")
		.Replace("Scout", "CanScout")
		.Replace("Ship", "IsShip")
		.Replace("Boat", "IsBoat_")
		.Replace("IsFlyingIsBoat_", "IsFlyingBoat")
		.Replace("IsIsBoat_", "IsBoat")
		.Replace("IsGunIsBoat_", "IsGunBoat")
		.Replace("CanCarryTorpedoesIsBoat_", "IsTorpedoBoat")
		.Replace("IsHydrofoilTorpedoIsBoat_", "IsHydrofoilTorpedoBoat")
		.Replace("CanCarryTorpedoesGunIsBoat_", "IsTorpedoGunBoat")
		.Replace("IsMissileIsBoat_", "IsMissileBoat")
		.Replace("IsArmoredIsBoat_", "IsArmoredBoat")
		.Replace("IsHeavyIsBoat_", "IsHeavyBoat")
		.Replace("IsHeavyGunIsBoat_", "IsHeavyGunBoat")
		.Replace("IsNavalFerryBarge", "IsFerry")
		.Replace("IsNavalAaFerry", "IsAaFerry")
		.Replace("Destroyer", "IsDestroyer_")
		.Replace("IsIsDestroyer_", "IsDestroyer")
		.Replace("Cruiser", "IsCruiser_")
		.Replace("IsIsCruiser_", "IsCruiser")
		.Replace("IsLightIsCruiser_", "IsLightCruiser")
		.Replace("LightIsCruiser_", "IsLightCruiser_")
		.Replace("IsHeavyIsCruiser_", "IsHeavyCruiser")
		.Replace("Carrier", "IsCarrier")
		.Replace("IsCarrierTakeOff", "CanTakeOffFromCarrier")
		.Replace("IsIsTankIsDestroyer_", "IsTankDestroyer")
		.Replace("MaxRatio", "HasMaximumRatio")
		.Replace("Ally", "IsAllied")
		.Replace("Axis", "IsAxis")
		.Replace("CountryUsa", "IsAmerican")
		.Replace("CountryGermany", "IsGerman")
		.Replace("CountryUssr", "IsSoviet")
		.Replace("CountryBritain", "IsBritish")
		.Replace("CountryAustralia", "IsAustralian")
		.Replace("CountryJapan", "IsJapanese")
		.Replace("CountryChina", "IsChinese")
		.Replace("CountryItaly", "IsItalian")
		.Replace("CountryFrance", "IsFrench")
		.Replace("CountrySweden", "IsSwedish")
		.Replace("WesternFront", "UsedOnWesternFront")
		.Replace("Westernfront", "UsedOnWesternFront_")
		.Replace("Britain", "UsedInBritain")
		.Replace("Bulge", "UsedInBattleOfBulge")
		.Replace("Ruhr", "UsedInRuhr")
		.Replace("EasternFront", "UsedOnEasternFront")
		.Replace("Easternfront", "UsedOnEasternFront_")
		.Replace("Stalingrad", "UsedAtStalingrad")
		.Replace("UsedAtStalingradW", "UsedAtStalingrad_")
		.Replace("Krymsk", "UsedAtKrymsk")
		.Replace("Korsun", "UsedAtKorsun")
		.Replace("Berlin", "UsedAtBerlin")
		.Replace("FarUsedOnEasternFront", "UsedOnFarEasternFront")
		.Replace("KhalkinGol", "UsedAtKhalkinGol")
		.Replace("China", "UsedInChina")
		.Replace("Mediterran", "UsedInMediterranean_")
		.Replace("UsedInMediterranean_ean", "UsedInMediterranean")
		.Replace("Malta", "UsedAtMalta")
		.Replace("Sicily", "UsedAtSicily")
		.Replace("Pacific", "UsedInPacific")
		.Replace("Honolulu", "UsedAtHonolulu")
		.Replace("Midway", "UsedInBattleOfMidway")
		.Replace("WakeIsland", "UsedAtWakeIsland")
		.Replace("Guadalcanal", "UsedAtGuadalcanal")
		.Replace("PortMoresby", "UsedAtPortMoresby")
		.Replace("Guam", "UsedInGuam")
		.Replace("IwoJima", "UsedAtIwoJima")
		.Replace("Korea", "UsedInKorea")
		.Replace("UsedInKoreanFront", "UsedOnKoreanFront")
		.Replace("CanRepairAnyIsAllied", "CanRepairTeammates")
	;
}
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
	WpCost();
}

void WpCost()
{
	var filePath = @"D:\Code\Source\_Repositories\WarThunderJsonFileChanges\Files\char.vromfs.bin_u\config\wpcost.blkx";
	var textLines = File.ReadAllLines(filePath);
	var vehicleProperties = GetVehicleProperties(textLines);
	
	var jsonMappingCode = new List<List<string>>();
	var classCode = new List<string>();
	foreach (var property in vehicleProperties.Where(line => line.StartsWith("ship")))
	{
		jsonMappingCode.Add
		(
			new List<string>
			{
				$"[JsonProperty(\"{property}\")]",
				$"public decimal? {property.First().ToString().ToUpper()}{string.Join("", property.Skip(1))}" + " { get; set; }"
			}
		);
		classCode.Add($"[Property()] public virtual decimal? {property.First().ToString().ToUpper()}{string.Join("", property.Skip(1))}" + " { get; protected set; }");
	}
	
	//string.Join("\n", vehicleProperties).Dump();
	//string.Join("\n\n", jsonMappingCode.Select(item => string.Join("\n", item))).Dump();
	//string.Join("\n\n", classCode).Dump();
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
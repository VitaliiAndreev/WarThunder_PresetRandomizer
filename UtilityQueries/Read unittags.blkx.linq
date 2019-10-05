<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	var filePath = @"D:\Code\Source\_Repositories\WarThunderJsonFileChanges\Files\char.vromfs.bin_u\config\unittags.blkx";
	var textLines = File.ReadAllLines(filePath);
	var vehicleProperties = GetVehicleProperties(textLines);
	var vehicleTypes = new List<string>();
	
	foreach (var line in textLines.Where(line => line.Contains("\"type\": \"")).Distinct())
	{
		vehicleTypes.Add(line);
	}
	
	string.Join("\n", vehicleProperties).Dump();
	vehicleTypes.Dump();
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
<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	var countries = new HashSet<string>();
	var lines = File.ReadAllText(@"F:\Code\Source\_Repositories\WarThunder_JsonFileChanges\Files\char.vromfs.bin_u\config\unittags.blkx").Split('\n');
	
	for (var i = 0; i < lines.Count(); i++)
	{
		var line = lines[i];
		
		if (line.StartsWith("    \"operatorCountry\":"))
			countries.Add(line.Split(':').Last().Trim().Replace("\"", string.Empty));
	}
	
	countries.OrderBy(country => country).Dump();
}
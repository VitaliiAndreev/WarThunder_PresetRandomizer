<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	var countries = new HashSet<string>();
	var lines = File.ReadAllText(@"D:\Code\Source\_Repositories\WarThunderJsonFileChanges\Files\char.vromfs.bin_u\config\unittags.blkx").Split('\n');
	
	for (var i = 0; i < lines.Count(); i++)
	{
		var line = lines[i];
		
		if (line.StartsWith("    \"originCountry\":"))
			countries.Add(line.Split(':').Last().Trim().Replace("\"", string.Empty));
	}
	
	countries.Dump();
}
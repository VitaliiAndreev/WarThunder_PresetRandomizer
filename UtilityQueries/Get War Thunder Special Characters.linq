<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var text = File.ReadAllText(@"F:\Code\Source\_Repositories\WarThunder_JsonFileChanges\Files\fonts.vromfs.bin_u\fonts.dynfont.blkx");
	var pairs = text
		.Split('{')
		.Where(group => group.Contains("skyquake"))
		.SelectMany(group => group.Split('\n'))
		.Where(line => line.Contains("range"))
		.SelectMany(line => line.Substring(line.IndexOf("=") + 1).Split(','))
		.Select(pair => pair.Trim())
		.Distinct()
		.Select(code => (char)int.Parse(code))
		.Dump();
}

// Define other methods and classes here

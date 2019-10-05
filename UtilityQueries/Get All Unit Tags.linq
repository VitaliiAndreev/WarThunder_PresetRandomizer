<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
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
	
	tags.Dump();
}
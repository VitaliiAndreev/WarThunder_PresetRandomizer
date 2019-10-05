<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	var d = new Dictionary<int, List<string>>
	{
		{ 0, new List<string> { "a1" }},
		{ 1, new List<string> { "b1", "b2" }},
		{ 2, new List<string> { "c1", "c2", "c3" }},
		{ 3, new List<string> { "d1", "d2", "d3", "d4" }},
		{ 4, new List<string> { "e1", "e2", "e3", "e4", "e5" }},
		{ 5, new List<string> { "f1", "f2", "f3", "f4", "f5", "f6" }},
		{ 6, new List<string> { "g1", "g2", "g3", "g4", "g5", "g6", "g7" }},
		{ 7, new List<string> { "h1", "h2", "h3", "h4" }},
	};
	
	var r = new Random();
	
	var counters = d.SelectMany(i => i.Value).ToDictionary(i => i, i => 0);
	for (var j = 0; j < 1_000_000; j++)
	{
		var randomKey = r.Next(0, d.Count());
		var hashSet = new HashSet<string>();
		
		var values = d[randomKey];
		var indeces = new List<int>();
		for (var i = 0; i < 3; i++)
		{
			var randomIndex = r.Next(0, values.Count());
			while(indeces.Contains(randomIndex))
			{
				randomIndex = r.Next(0, values.Count());
			}
			var randomItem = values[randomIndex];
			counters[randomItem]++;
			hashSet.Add(randomItem);
			indeces.Add(randomIndex);
			
			//Console.WriteLine($"index {randomIndex}");
			
			if (indeces.Count() == values.Count)
			{
				//Console.WriteLine("breaking");
				break;
			}
		}
	}
	counters.Dump();
}

public static List<T> Copy<T>(ICollection<T> sourceCollection)
{
    var newCollection = new List<T>();

    foreach (var item in sourceCollection)
        newCollection.Add(item);

    return newCollection;
}
<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	var d = new Dictionary<char, List<string>>
	{
		{ 'a', new List<string> { "a1" }},
		{ 'b', new List<string> { "b1", "b2" }},
		{ 'c', new List<string> { "c1", "c2", "c3" }},
		{ 'd', new List<string> { "d1", "d2", "d3", "d4" }},
		{ 'e', new List<string> { "e1", "e2", "e3", "e4", "e5" }},
		{ 'f', new List<string> { "f1", "f2", "f3", "f4", "f5", "f6" }},
		{ 'g', new List<string> { "g1", "g2", "g3", "g4", "g5", "g6", "g7" }},
		{ 'h', new List<string> { "h1", "h2", "h3", "h4" }},
		{ 'i', new List<string> { "i1" }},
		{ 'j', new List<string> { "j1", "j2" }},
		{ 'k', new List<string> { "k1", "k2", "k3" }},
	};
	var w = d.ToDictionary(di => di.Key, di => di.Value.Count);
	
	var wc = new List<char>();
	
	foreach (var wi in w)
		for (var i = 0; i < (wi.Value <= 3 ? 1 : wi.Value / 5.0); i++)
			wc.Add(wi.Key);
	
	/*d.Dump();
	w.Dump();
	wc.Dump();*/
	
	var r = new Random();
	
	var counters = d.SelectMany(i => i.Value).ToDictionary(i => i, i => 0);
	for (var j = 0; j < 1_000_000; j++)
	{
		var rk = wc[r.Next(0, wc.Count())];
		var hs = new HashSet<string>();
		
		var copy = d[rk]; //copy.Count.Dump();
		var indeces = new List<int>();
		for (var i = 0; i < 3; i++)
		{
			var randomIndex = r.Next(0, copy.Count());
			while(indeces.Contains(randomIndex))
			{
				randomIndex = r.Next(0, copy.Count());
			}
			var randomItem = copy[randomIndex];
			counters[randomItem]++;
			hs.Add(randomItem);
			indeces.Add(randomIndex);
			
			//Console.WriteLine($"index {randomIndex}");
			
			if (indeces.Count() == copy.Count)
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
<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	for (var economicRank = 0; economicRank < 100; economicRank++)
	{
		var battleRating = Math.Round(economicRank / 3.0m + 1, 1);
		
		$"{economicRank.ToString().PadLeft(2)} - {battleRating.ToString("0.0").PadLeft(4)}".Dump();
		
		if (battleRating == 10.0m)
			string.Empty.Dump();
	}
}
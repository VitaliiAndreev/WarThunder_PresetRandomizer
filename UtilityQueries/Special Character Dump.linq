<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	for (var i = 0; i < int.MaxValue; i++)
	{
		var character = (char)i;
			
		if (i % 1000 == 0)
		{
			Console.Write($"\n{i.ToString().PadLeft(int.MaxValue.ToString().Count())}: ");
			Console.ReadLine();
		}
		
		if (!char.IsWhiteSpace(character) && !char.IsLetterOrDigit(character))
			Console.Write(character);
	}
}
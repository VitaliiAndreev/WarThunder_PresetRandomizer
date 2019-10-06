<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	foreach (var letterCode in Enumerable.Range((int)'A', 26))
	{
		Console.WriteLine($"        #region {(char)letterCode}");
		Console.WriteLine($"        #endregion {(char)letterCode}");
	}
}
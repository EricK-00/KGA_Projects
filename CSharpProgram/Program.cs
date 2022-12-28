using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace CSharpProgram
{
	class Program
	{
		static void Main(string[] args)
		{
			CardGame cardGame = new CardGame();
			cardGame.PlayCardGame();
		}
	}
}
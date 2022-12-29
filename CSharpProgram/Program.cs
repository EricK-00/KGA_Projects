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
			BattleGame battleGame = new BattleGame();
			battleGame.PlayBattleGame();
		}
	}
}
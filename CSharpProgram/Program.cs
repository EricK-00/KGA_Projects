using System;

namespace CSharpProgram
{
	internal class Program
	{
		static void Main(string[] args)
		{
/*			int bitNumber = 10;//1010
			int result = 0;
			result = bitNumber & 0b_0010;
			Console.WriteLine(Convert.ToString(result, 2));*/

			Program3.CountConsonantAndVowel();
			Program3.FindNumberVer1();
			Program3.FindNumberVer2();
			Program3.FindNumberVer3();
			Program3.MakeMathQuiz();
		}
	}
}
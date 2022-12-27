using System;

namespace CSharpProgram
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int AddNumber(int a, int b)
			{
				return a + b;
			}

			int GetBigger(int a, int b)
			{
				if (a >= b)
					return a;
				else
					return b;
			}

			int GetSmaller(int a, int b)
			{
				if (a < b)
					return a;
				else
					return b;
			}

			int GetAbs(int a)
			{
				if (a < 0)
					return -a;
				else
					return a;
			}

			Console.WriteLine(AddNumber(5, 2));
			Console.WriteLine(GetBigger(1, 3));
			Console.WriteLine(GetSmaller(3, 1));
			Console.WriteLine(GetAbs(-5));
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgram
{
	internal class Params
	{
		public int[] FlexibleTypeParam(params int[] numbers)
		{
			foreach (int num in numbers)
			{
				Console.Write($"{numbers} ");
			}
			Console.WriteLine();

			return numbers;
		}
	}
}

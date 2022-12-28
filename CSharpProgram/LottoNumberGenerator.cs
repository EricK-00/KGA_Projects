using System;

namespace CSharpProgram
{
	class LottoNumberGenerator
	{
		int[] numbers = new int[45];
		Random random = new Random();

		public void GetNumber()
		{
			for (int i = 0; i < numbers.Length; i++)
				numbers[i] = i + 1;

			for (int i = 0; i < numbers.Length; i++)
			{
				int randomIndex = random.Next(0, 44 + 1);
				int temp = numbers[i];
				numbers[i] = numbers[randomIndex];
				numbers[randomIndex] = temp;
			}

			for (int i = 0; i < 6; i++)
				Console.Write($"{numbers[i]} ");
			Console.WriteLine();
		}
	}
}

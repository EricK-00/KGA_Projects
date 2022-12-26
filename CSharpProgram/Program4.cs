using System;
using System.Runtime.CompilerServices;

namespace MySpace
{
	class Program4
	{
		public static void PlaceStar()
		{
			int userInput;

			Console.WriteLine("줄의 수를 입력하세요");
			while (true)
			{
				int.TryParse(Console.ReadLine(), out userInput);
				if (userInput < 1 || userInput > 20)
				{
					Console.WriteLine("다시 입력하세요");
					continue;
				}
				break;
			}

			for (int i = 0; i < userInput; i++)
			{
				for (int j = 0; j < i + 1; j++)
				{
					Console.Write("*");
				}
				Console.WriteLine();
			}
		}

		public static void DrawDiamond()
		{
			int userInput, mid, drawCount;

			Console.WriteLine("줄의 수를 입력하세요");
			while (true)
			{
				int.TryParse(Console.ReadLine(), out userInput);
				if (userInput < 1 || userInput > 20)
				{
					Console.WriteLine("다시 입력하세요");
					continue;
				}
				break;
			}

			mid = userInput % 2 == 0 ? userInput / 2 : userInput / 2 + 1;
			for (int i = 0; i < userInput; i++)
			{
				drawCount = i < mid ? i + 1 : userInput - i;
				Console.Write($"{i + 1}\t");

				for (int j = 0; j < mid - drawCount; j++)
				{
					Console.Write(" ");
				}
				for (int j = 0; j < drawCount; j++)
				{
					Console.Write("* ");
				}
				Console.WriteLine();
			}
		}

		public static void NumberBaseball()
		{
			Random random = new Random();
			bool isCorrect = false;
			int[] inputNumberPlace = new int[3] { 0, 0, 0 };
			int[] answerNumberPlace = new int[3] { 0, 0, 0 };

			answerNumberPlace[0] = random.Next(0, 9 + 1);

			answerNumberPlace[1] = random.Next(0, 8 + 1);
			if (answerNumberPlace[0] <= answerNumberPlace[1]) ++answerNumberPlace[1];

			answerNumberPlace[2] = random.Next(0, 7 + 1);
			if (answerNumberPlace[0] <= answerNumberPlace[2]) ++answerNumberPlace[2];
			if (answerNumberPlace[1] <= answerNumberPlace[2]) ++answerNumberPlace[2];

/*			while (answerNumberPlace[1] == answerNumberPlace[0])
			{
				answerNumberPlace[1] = random.Next(0, 9 + 1);
			}
			while (answerNumberPlace[2] == answerNumberPlace[0] || answerNumberPlace[2] == answerNumberPlace[1])
				answerNumberPlace[2] = random.Next(0, 9 + 1);*/

			Console.WriteLine($"숫자야구 정답:{answerNumberPlace[0]} {answerNumberPlace[1]} {answerNumberPlace[2]}");
			for (int i = 0; i < 9; i++)
			{
				int input;
				int ballCount = 0;
				int strikeCount = 0;
				int outCount;

				while (true)
				{
					Console.WriteLine($"숫자를 입력하세요. 남은기회: {9 - i}");
					int.TryParse(Console.ReadLine(), out input);

					inputNumberPlace[0] = input / 100;
					inputNumberPlace[1] = (input - inputNumberPlace[0] * 100) / 10;
					inputNumberPlace[2] = (input - inputNumberPlace[0] * 100 - inputNumberPlace[1] * 10);

					bool isSameNumber = (inputNumberPlace[0] == inputNumberPlace[1]) || 
										(inputNumberPlace[1] == inputNumberPlace[2]) || 
										(inputNumberPlace[0] == inputNumberPlace[2]);
					if (isSameNumber)
					{
						Console.WriteLine("중복된 숫자가 있습니다.");
						continue;
					}
					else
					{
						break;
					}
				}

				for (int j = 0; j < 3; j++)
				{
					if (inputNumberPlace[j] == answerNumberPlace[j])
					{
						++strikeCount;
					}
					else
					{
						for (int k = 0; k < 3; k++)
						{
							if (j == k) continue;
							if (inputNumberPlace[j] == answerNumberPlace[k])
							{
								++ballCount;
							}
						}
					}
				}

				outCount = 3 - (strikeCount + ballCount);
				Console.WriteLine($"strike: {strikeCount}, ball: {ballCount}, out: {outCount}");

				if (strikeCount == 3)
				{
					Console.WriteLine("정답입니다.");
					isCorrect = true;
					break;
				}
			}

			if (!isCorrect)
			{
				Console.WriteLine("실패");
			}
		}
	}
}
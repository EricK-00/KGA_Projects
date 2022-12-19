using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgram
{
	internal class Program2
	{
		//최대한도의 사탕 구매하기
		public static string GetMaximumCandy()
		{
			int money;
			const int CANDY_PRICE = 300;

			Console.WriteLine("돈을 입력하세요.");
			int.TryParse(Console.ReadLine(), out money);

			return $"최대 구입 가능한 사탕의 수: {money / CANDY_PRICE}\n남은 돈: {money % CANDY_PRICE}";
		}

		//화씨온도를 입력받아 섭씨온도 출력하기
		public static string ConvertToCelsius()
		{
			float fahrenheit, celsius;

			Console.WriteLine("화씨온도를 입력하세요.");
			float.TryParse(Console.ReadLine(), out fahrenheit);

			celsius = (fahrenheit - 32) / 1.8f;

			return $"화씨온도 {fahrenheit}도는 섭씨온도 {celsius}도 입니다.";
		}

		//2개의 주사위를 던져서 주사위의 합 출력
		public static string DiceGame()
		{
			int[] dice = new int[2];

			Random random = new Random();

			dice[0] = random.Next(1, 6 + 1);
			dice[1] = random.Next(1, 6 + 1);

			return $"첫 번째 주사위: {dice[0]}\n두 번째 주사위: {dice[1]}\n두 주사위의 합: {dice[0] + dice[1]}";
		}

		//컵의 사이즈를 입력받아 크기에 따라 다르게 출력
		public static string GetCupSize(int cupSize)
		{
			if (cupSize < 100 && cupSize > 0)
				return "small";
			else if (cupSize < 200 && cupSize >= 100)
				return "medium";
			else if (cupSize >= 200)
				return "large";
			else
				return "잘못된 값 입력";
		}

		//비밀 코드 맞추기
		public static void FindSecretCode()
		{
			Random random = new Random();
			//const char SECRET_CODE = (char)(97 + random.Next(0, 25 + 1));
			const char SECRET_CODE = 'h';
			bool corrected = false;
			bool isAlphabet = false;
			bool isBigAlphabet = false;

			while (!corrected)
			{
				Console.WriteLine("비밀코드를 맞춰보세요(알파벳).");
				char userInput;
				char.TryParse(Console.ReadLine(), out userInput);
				isAlphabet = (userInput >= 'a' && userInput <= 'z') || (userInput >= 'A' && userInput <= 'Z') ? true : false;
				isBigAlphabet = (userInput >= 'A' && userInput <= 'Z') ? true : false;

				if (isAlphabet && isBigAlphabet)
				{
					if (userInput < SECRET_CODE - 32)
					{
						Console.WriteLine($"{userInput} 뒤에 있습니다.");
					}
					else if (userInput > SECRET_CODE - 32)
					{
						Console.WriteLine($"{userInput} 앞에 있습니다.");
					}
					else
					{
						Console.WriteLine($"정답입니다.");
						corrected = true;
					}
				}
				else if (isAlphabet && !isBigAlphabet)
				{
					if (userInput < SECRET_CODE)
					{
						Console.WriteLine($"{userInput} 뒤에 있습니다.");
					}
					else if (userInput > SECRET_CODE)
					{
						Console.WriteLine($"{userInput} 앞에 있습니다.");
					}
					else
					{
						Console.WriteLine($"정답입니다.");
						corrected = true;
					}
				}
				else
				{
					Console.WriteLine("잘못된 값이 입력되었습니다. 알파벳을 입력해주세요.");
				}
			}
		}

		public static int GetLargeNumber()
		{
			int largeNumber = int.MinValue;

			Console.Write("3개의 정수를 입력하세요: ");
			string? input = Console.ReadLine();

			string[] words = input.Split(new char[] { ' ' });
			int[] numbers = new int[words.Length];

/*			for (int i = 0; i < numbers.Length; i++)
			{
				int.TryParse(words[i], out numbers[i]);
				if (largeNumber < numbers[i])
				{
					largeNumber = numbers[i];
				}
			}*/

			largeNumber = numbers.Max();

			return largeNumber;
		}
	}
}

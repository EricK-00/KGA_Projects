using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
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

		//3개의 정수 중에 큰 수 출력
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

		//입력한 값만큼 문자열 출력
		public static void PrintHelloWorld()
		{
			int time;
			while (true)
			{
				Console.Write("양의 정수를 입력하세요. ");
				int.TryParse(Console.ReadLine(), out time);
				if (time <= 0)
				{
					Console.WriteLine("잘못된 입력");
				}
				else
				{
					while (time > 0)
					{
						Console.WriteLine("Hello world!");
						--time;
					}
					break;
				}
			}
		}

		//입력한 값만큼 3의 배수 출력
		public static void PrintMultipleNumber()
		{
			int time;
			while (true)
			{
				Console.Write("양의 정수를 입력하세요. ");
				int.TryParse(Console.ReadLine(), out time);
				if (time <= 0)
				{
					Console.WriteLine("잘못된 입력");
				}
				else
				{
					int initialValue = time;
					while (time > 1)
					{
						Console.Write($"{3 * (initialValue - time + 1)}, ");
						--time;
					}
					Console.WriteLine(3 * (initialValue - time + 1));
					break;
				}
			}
		}

		//0을 입력할 때까지 받은 정수의 합을 출력
		public static void PrintSum()
		{
			int userInput;
			int sum = 0;
			while (true)
			{
				Console.Write("정수를 입력하세요(0을 입력하면 종료됩니다). ");
				int.TryParse(Console.ReadLine(), out userInput);

				if (userInput == 0)
				{
					Console.WriteLine($"입력한 정수의 합: {sum}");
					break;
				}
				else
				{
					sum += userInput;
				}
			}
		}

		//입력한 값과 같은 구구단의 단 출력
		public static void ReverseGuGuDan()
		{
			int danNum;
			int loopCounter = 9;

			Console.Write("출력하고 싶은 구구단의 단을 입력하세요. ");
			int.TryParse(Console.ReadLine(), out danNum);

			while (loopCounter > 1)
			{
				Console.Write($"{loopCounter * danNum}, ");
				--loopCounter;
			}
			Console.WriteLine($"{loopCounter * danNum}");
		}

		//입력한 값들의 평균 출력
		public static void PrintAverage()
		{
			int time;

			Console.Write("User input(Loop count) -> ");
			int.TryParse(Console.ReadLine(), out time);

			int initialValue = time;
			int input = 0;
			int sum = 0;
			while (time > 0)
			{
				Console.Write("User Input -> ");
				int.TryParse(Console.ReadLine(), out input);
				sum += input;
				--time;
			}

			float averageNum = (float)sum / (float)initialValue;
			Console.WriteLine($"평균 값: {averageNum:f1}");
		}

		public static void A()
		{
			int sum = 0;
			for (int i = 1; i <= 100; i++)
			{
				if ((i % 3 == 0) && (i % 4 == 0))
				{
					sum += i;
				}
			}
			Console.WriteLine(sum);
		}

		public static void B()
		{
			int input1, input2;
			Console.WriteLine("두 개의 정수를 입력하세요.");
			int.TryParse(Console.ReadLine(), out input1);
			int.TryParse(Console.ReadLine(), out input2);

			if (input1 < input2)
			{
				int temp = input2;
				input2 = input1;
				input1 = temp;
			}
			Console.WriteLine(input1 - input2);
		}

		public static void C()
		{
			for (int i = 1; i <= 9; i++)
			{
				if (i % 2 != 0) continue;
				for (int j = 1; j <= 9; j++)
				{
					if (j == i) break;
					Console.WriteLine($"{i} * {j} = {i * j}");
				}
			}
		}

		public static void D()
		{
			bool[] check = new bool[100];
			for (int i = 0; i < check.Length; i++)
			{
				check[i] = false;
			}

			for (int i = 1; i <= 9; i++)
			{
				for (int j = 1; j <= 9; j++)
				{
					if (check[i * 10 + j]) continue;

					if (i * 10 + j == j * 10 + i)
					{
						Console.WriteLine($"{i * 10 + j}, {j * 10 + i}");
					}
					check[i * 10 + j] = true;
					check[j * 10 + i] = true;
				}
			}
		}
	}
}

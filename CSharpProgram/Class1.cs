using System;
using System.Linq.Expressions;

namespace CSharpProgram
{
	internal class Class1
	{
		public static void CompareString()
		{
			bool isSame = true;

			Console.WriteLine("문자열을 입력하세요.(2)");
			string inputStr1 = Console.ReadLine();
			Console.WriteLine("문자열을 입력하세요.(1)");
			string inputStr2 = Console.ReadLine();


			if (inputStr1.Length != inputStr2.Length)
			{
				Console.WriteLine("다른 문자열입니다.");
				return;
			}

			for (int i = 0; i < inputStr1.Length; i++)
			{
				if (inputStr1[i] != inputStr2[i])
					isSame = false;
			}

			if (isSame)
			{
				Console.WriteLine("같은 문자열입니다.");
			}
			else
			{
				Console.WriteLine("다른 문자열입니다.");
			}
		}

		public static void VendingMachine()
		{
			int userInput;
			const int MIN_NUM = 1;
			const int MAX_NUM = 5;
			string[] beverage = new string[MAX_NUM] {"콜라", "물", "스프라이트", "주스", "커피"};


			Console.WriteLine($"음료수를 선택하세요.");
			for (int i = 0; i < MAX_NUM; i++)
			{
				Console.Write($"{i + 1}.{beverage[i]} ");
			}

			while (true)
			{
				if (!int.TryParse(Console.ReadLine(), out userInput) || userInput < MIN_NUM || userInput > MAX_NUM)
				{
					Console.WriteLine("[error]잘못된 입력");
					continue;
				}
				break;
			}

			Console.WriteLine($"{beverage[userInput - 1]}를(을) 선택하였습니다.");

/*			switch (userInput)
			{
				case 1:
					Console.WriteLine("콜라를 선택하였습니다");
					break;
				case 2:
					Console.WriteLine("물을 선택하였습니다");
					break;
				case 3:
					Console.WriteLine("스프라이트를 선택하였습니다");
					break;
				case 4:
					Console.WriteLine("주스를 선택하였습니다");
					break;
				case 5:
					Console.WriteLine("커피를 선택하였습니다");
					break;
			}*/
		}

		public static void PrintLastDayInMonth()
		{
			int[] lastDays = new int[12] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

			for (int i = 0; i < lastDays.Length; i++)
			{
				Console.WriteLine($"{i + 1}월은 {lastDays[i]}일까지 입니다.");
			}
		}
	}
}
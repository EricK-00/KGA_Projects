using System;

namespace CSharpProgram
{
	class LearnFunction
	{
		public int AddNumber(int a, int b)
		{
			return a + b;
		}

		private int GetBigger(int a, int b)
		{
			if (a >= b)
				return a;
			else
				return b;
		}

		private int GetSmaller(int a, int b)
		{
			if (a < b)
				return a;
			else
				return b;
		}

		private int GetAbs(int a)
		{
			if (a < 0)
				return -a;
			else
				return a;
		}

		private string[] CompareNumberAndGetAbs(int a, int b)
		{
			string[] result = new string[4];

			int bigger = GetBigger(a, b);
			int smaller = GetSmaller(a, b);
			int biggerAbs = GetAbs(bigger);
			int smallerAbs = GetAbs(smaller);

			result[0] = $"더 큰 수: {bigger}";
			result[1] = $"더 작은 수: {smaller}";
			result[2] = $"더 큰 수의 절댓값: {biggerAbs}";
			result[3] = $"더 작은 수의 절댓값: {smallerAbs}";

			return result;
		}

		public static int GetUserInputNumber()
		{
			int userInput;

			Console.Write("정수를 입력해주세요: ");
			while (true)
			{
				if (int.TryParse(Console.ReadLine(), out userInput))
				{
					break;
				}
				else
				{
					Console.WriteLine("잘못된 입력");
					continue;
				}
			}

			return userInput;
		}

		public static double GetUserInputDouble()
		{
			double userInput;

			Console.Write("실수를 입력해주세요: ");
			while (true)
			{
				if (double.TryParse(Console.ReadLine(), out userInput))
				{
					break;
				}
				else
				{
					Console.WriteLine("잘못된 입력");
					continue;
				}
			}

			return userInput;
		}

		private string GetPhoneNumber()
		{
			string userInput;

			Console.Write("소괄호를 포함한 전화번호를 입력해주세요(q 입력 시 종료): ");
			userInput = Console.ReadLine();

			return userInput;
		}

		public void Module()
		{
			int userInput1 = GetUserInputNumber();
			int userInput2 = GetUserInputNumber();

			foreach (string item in CompareNumberAndGetAbs(userInput1, userInput2))
			{
				Console.WriteLine(item);
			}
		}

		public static void Print()
		{
			Console.WriteLine("안녕하세요.");
		}

		public static void Print(string msg)
		{
			Console.WriteLine(msg);
		}

		public static void Print(string msg, int count)
		{
			for (int i = 0; i < count; i++)
				Console.WriteLine(msg);
		}

		public static long Factorial(int n, long[] memo)
		{
			if (memo[n] != -1)
				return memo[n];

			if (n == 0 || n == 1)
			{
				memo[n] = 1;
				return memo[n];
			}

			memo[n] = n * Factorial(n - 1, memo);
			return memo[n];
		}

		public static long FactorialWithNoMemo(int n)
		{
			if (n == 0 || n == 1)
			{
				return 1;
			}

			return n * FactorialWithNoMemo(n - 1);
		}

		public static void Swap(ref int a, ref int b)
		{
			int temp;
			temp = a;
			a = b;
			b = temp;
		}


		private void Print(int a, int b) => Console.WriteLine($"{a}, {b}");

		private void SayHello(int time)
		{
			while (time-- > 0)
				Console.WriteLine("Hello");
		}

		private void Maximum(int x, int y, int z)
		{
			int max = x;
			if (max < y) max = y;
			if (max < z) max = z;

			Console.WriteLine($"최댓값: {max}");
		}

		private double Hypot(double num1, double num2)
		{
			return Math.Sqrt(num1 * num1 + num2 * num2);
		}

		private void Prime(int maxNum)
		{
			Console.Write($"2부터 {maxNum}까지의 소수: 2 ");
			for (int i = 3; i <= maxNum; i++)
			{
				bool isPrime = true;
				if (i % 2 == 0)
					continue;
				for (int j = 3; j <= i; j++)
				{
					if (i == j)
						continue;
					if (i % j == 0)
					{
						isPrime = false;
						break;
					}
				}

				if (isPrime)
					Console.Write($"{i} ");
			}
			Console.WriteLine();
		}

		private void ConvertPhoneNumber(string phoneNumber)
		{
			Console.WriteLine("수정된 전화번호: ");
			for (int i = 0; i < phoneNumber.Length; i++)
			{
				if (phoneNumber[i] == '(' || phoneNumber[i] == ')')
				{
					continue;
				}
				else
				{
					Console.Write(phoneNumber[i]);
				}
			}
			Console.WriteLine();
		}

		private void ConvertAndReversePhoneNumber(string phoneNumber)
		{
			Console.WriteLine("수정된 전화번호: ");
			for (int i = phoneNumber.Length - 1; i >= 0; i--)
			{
				if (phoneNumber[i] == '(' || phoneNumber[i] == ')')
				{
					continue;
				}
				else
				{
					Console.Write(phoneNumber[i]);
				}
			}
			Console.WriteLine();
		}

		public void SayHelloModule()
		{
			Console.WriteLine("횟수만큼 Hello를 출력하는 프로그램");
			int time = GetUserInputNumber();
			SayHello(time);
			Console.WriteLine();
		}

		public void MaximumModule()
		{
			Console.WriteLine("최댓값을 출력하는 프로그램");
			int x = GetUserInputNumber();
			int y = GetUserInputNumber();
			int z = GetUserInputNumber();
			Maximum(x, y, z);
			Console.WriteLine();
		}

		public void HypotModule()
		{
			Console.WriteLine("빗변의 길이를 출력하는 프로그램");
			double num1 = GetUserInputDouble();
			double num2 = GetUserInputDouble();
			Console.WriteLine($"빗변의 길이: {Hypot(num1, num2)}");
			Console.WriteLine();
		}

		public void PrimeModule()
		{
			Console.WriteLine("소수를 출력하는 프로그램");
			Prime(100);
			Console.WriteLine();
		}

		public void ConvertPhoneNumberModule()
		{
			Console.WriteLine("전화번호에서 소괄호를 제거해서 출력하는 프로그램");
			string userString = string.Empty;
			while (true)
			{
				userString = GetPhoneNumber();
				if (userString == "q")
				{
					Console.WriteLine("프로그램 종료");
					break;
				}
				else
					ConvertPhoneNumber(userString);
			}
			Console.WriteLine();
		}

		public void ConvertAndReversePhoneNumberModule()
		{
			Console.WriteLine("전화번호에서 소괄호를 제거해서 출력하는 프로그램");
			string userString = string.Empty;
			while (true)
			{
				userString = GetPhoneNumber();
				if (userString == "q")
				{
					Console.WriteLine("프로그램 종료");
					break;
				}
				else
					ConvertAndReversePhoneNumber(userString);
			}
			Console.WriteLine();
		}
	}
}

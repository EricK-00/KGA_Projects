using System;

namespace CSharpProgram
{
	class Function
	{
		//주어, 동사, 목적어를 입력받아 문장 출력
		public static string WriteSentence()
		{
			string?[] words = new string[3];
			words[0] = "주어"; words[1] = "동사"; words[2] = "목적어";
			for (int i = 0; i < words.Length; i++)
			{
				Console.WriteLine($"{words[i]}를 입력하세요.");
				words[i] = Console.ReadLine();
			}

			Console.WriteLine(words.Length);

			return $"{words[0]} {words[1]} a {words[2]}";
		}

		//나이 입력받아 더하기
		public static int AddAge(int addition)
		{
			Console.WriteLine("나이를 입력하세요.");
			int age = 0;
			int.TryParse(Console.ReadLine(), out age);

			return age + addition;
		}

		//빗변의 길이 구하기
		public static double CalculateHypotenuse()
		{
			Console.WriteLine("삼각형의 빗변을 제외한 두 변의 길이를 입력하세요.");
			double[] length = new double[2];
			for (int i = 0; i < length.Length; i++)
				double.TryParse(Console.ReadLine(), out length[i]);

			return Math.Sqrt(length[0] * length[0] + length[1] * length[1]);
		}

		//상자의 부피와 겉넓이 구하기
		public static string CalculateBoxVolumeAndSurfaceArea()
		{
			Console.WriteLine("상자의 길이, 너비, 높이를 입력하세요.");
			double boxLength, boxWidth, boxHeight;

			double.TryParse(Console.ReadLine(), out boxLength);
			double.TryParse(Console.ReadLine(), out boxWidth);
			double.TryParse(Console.ReadLine(), out boxHeight);

			double boxVolume = boxLength * boxWidth * boxHeight;
			double boxSurfaceArea = 2 * (boxLength * boxWidth + boxLength * boxHeight + boxHeight * boxWidth);

			return $"상자의 부피: {boxVolume}, 상자의 겉넓이: {boxSurfaceArea}";
		}

		//평을 입력받아 제곱미터 구하기
		public static string GetSquareMeter()
		{
			Console.WriteLine("평을 입력하세요.");
			double pyong = 0;
			double.TryParse(Console.ReadLine(), out pyong);

			return $"{pyong}평은 {3.3058 * pyong}m^2";
		}

		//시, 분, 초를 입력받아 초로 표현하기
		public static string GetSeconds()
		{
			Console.WriteLine("시, 분, 초를 입력하세요.");
			int hour, minute, second;
			int.TryParse(Console.ReadLine(), out hour);
			int.TryParse(Console.ReadLine(), out minute);
			int.TryParse(Console.ReadLine(), out second);

			int resultSecond = 3600 * hour + 60 * minute + second;

			return $"{resultSecond}";
		}

		//최종성적 구하기
		public static void CalculateTotalScore()
		{
			int[] quizScore = new int[3];
			int midTermScore, finalsScore;

			for (int i = 0; i < quizScore.Length; i++)
			{
				Console.Write($"퀴즈\t#{i + 1}\t성적:\t");
				int.TryParse(Console.ReadLine(), out quizScore[i]);
			}
			Console.Write("중간고사\t성적:\t");
			int.TryParse(Console.ReadLine(), out midTermScore);
			Console.Write("기말고사\t성적:\t");
			int.TryParse(Console.ReadLine(), out finalsScore);

			int totalScore = quizScore[0] + quizScore[1] + quizScore[2] + midTermScore + finalsScore;

			Console.WriteLine("=========================");
			Console.WriteLine($"성적 총합: {totalScore}");
			Console.WriteLine("=========================");
		}
	}
}

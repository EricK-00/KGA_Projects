using System;

namespace CSharpProgram
{
	class RockPaperScissors
	{
		string userSkill;
		int userIndex;
		string computerSkill;
		int computerIndex;

		string[] skills = new string[3] { "가위", "바위", "보" };
		string[,] gameResult = new string[3, 3] { { "무승부", "패배", "승리" }, { "승리", "무승부", "패배" }, { "패배", "승리", "무승부" } };

		public void PlayRockPaperScissors()
		{
			while (true)
			{
				userIndex = -1;
				computerIndex = -1;

				Console.WriteLine("가위, 바위, 보를 입력해주세요.");
				userSkill = Console.ReadLine();

				for (int i = 0; i < skills.Length; i++)
				{
					if (userSkill == skills[i])
					{
						userIndex = i;
					}
				}

				if (userIndex == -1)
				{
					Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
					continue;
				}

				Console.WriteLine($"[플레이어]: {userSkill}");
				computerSkill = GenerateComputerSkill();
				Console.WriteLine($"[컴퓨터]: {computerSkill}");

				Console.WriteLine($"{gameResult[userIndex, computerIndex]}");
			}
		}
		private string GenerateComputerSkill()
		{
			Random random = new Random();
			computerIndex = random.Next(0, skills.Length);
			return skills[computerIndex];
		}
	}
}

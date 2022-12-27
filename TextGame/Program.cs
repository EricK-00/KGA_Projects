using System;
using System.Collections.Generic;

namespace TextGame
{
	internal class Program
	{
		static string gameState = "Title";
		const int GET_RANDOM = -1;

		static int gold = 100;
		static int health = 4;
		static int power = 9;

		const int DEFAULT_BATTLE_HP = 100;
		const int DEFAULT_BATTLE_POWER = 10;

		static int myBattleHP;
		static int myBattlePower;

		static string? foeName;
		static int foeBattleHP;
		static int foeBattlePower;

		const int STORY_TEXT_COUNT = 3;
		static int[] selectionCount = new int[STORY_TEXT_COUNT];
		static string[] storyText = new string[STORY_TEXT_COUNT];

		static List<string> inventory = new List<string>();

		static float successPercent;

		static void SetStoryText()
		{
			selectionCount[0] = 3;
			storyText[0] = $"당신은 수상한 집을 발견했다.";

			selectionCount[1] = 2;
			storyText[1] = $"당신은 길을 막고 있는 바위를 발견했다.";

			selectionCount[2] = 1;
			storyText[2] = $"당신 앞에 고블린이 길을 막고 있다.";
		}

		static void PrintSelectionText(int textIndex)
		{
			switch (textIndex)
			{
				case 0:
					Console.WriteLine($"선택: 1.들어간다 2.지나간다 3.입구를 살핀다");
					break;
				case 1:
					successPercent = (float)Math.Round((float)power * 10 / 1000 + 50, 1);
					if (successPercent > 100) successPercent = 100;
					Console.WriteLine($"선택: 1.부순다{{성공확률(힘): {successPercent}}} 2. 돌아간다");
					successPercent += 50;
					break;
				case 2:
					Console.WriteLine($"선택: 1.전투한다");
					break;
			}
		}

		static int Event(int textIndex, int userInput)
		{
			switch (textIndex)
			{
				case 0:
					switch (userInput)
					{
						case 1:
							Console.WriteLine($"당신은 유령에게 습격당했다");

							--health;
							gold -= 10;
							if (gold < 0) gold = 0;
							Console.WriteLine($"체력 -1, 현재체력 {health}");
							Console.WriteLine($"골드 -10, 현재골드{gold}");
							break;
						case 2:
							Console.WriteLine($"당신은 수상한 집을 지나쳤다");
							break;
						case 3:
							Console.WriteLine($"당신은 동전을 발견했다");
							gold += 100;
							Console.WriteLine($"골드 +100, 현재골드{gold}");
							break;
					}
					break;
				case 1:
					switch (userInput)
					{
						case 1:
							int randomNumber = new Random().Next(1, 1000 + 1);
							bool succeed = (randomNumber <= successPercent * 10) ? true : false;
							if (succeed)
							{
								Console.WriteLine($"당신은 바위를 부쉈다");
								inventory.Add("갑옷");
								Console.WriteLine($"아이템 획득: 갑옷");
							}
							else
							{
								Console.WriteLine($"당신은 바위를 부수지 못했다");
								--health;
								Console.WriteLine($"체력 -1, 현재체력{health}");
							}
							break;
						case 2:
							Console.WriteLine($"당신은 바위를 지나쳤다");
							break;
					}
					break;
				case 2:
					switch (userInput)
					{
						case 1:
							Console.WriteLine($"이벤트 {textIndex} {userInput}");
							foeName = "고블린";
							gameState = "Battle";
							SetFoeStatus(foeName);
							bool isWin = Battle(foeName);
							if (isWin)
							{
								gold += 100;
								inventory.Add("검");
								Console.WriteLine($"골드 +100");
								Console.WriteLine($"아이템 획득: 검");
							}
							else
							{
								--health;
								Console.WriteLine($"체력 -1, 현재체력{health}");
							}
							break;
					}
					break;
			}
			return GET_RANDOM;
		}

		static bool Battle(string foeName)
		{
			bool isMyAttack = true;
			int dice = new Random().Next(1, 20 + 1);

			myBattleHP = DEFAULT_BATTLE_HP;
			myBattlePower = DEFAULT_BATTLE_POWER;



			Console.WriteLine($"[당신] 체력: {myBattleHP}, 전투력: {myBattlePower} VS [{foeName}] 상대체력: {foeBattleHP}, 상대전투력: {foeBattlePower}");
			while (myBattleHP > 0 && foeBattleHP > 0)
			{
				if (isMyAttack)
				{
					foeBattleHP -= myBattlePower;
					Console.Write("당신의 공격: ");
				}
				else
				{
					myBattleHP -= foeBattlePower;
					Console.Write("상대의 공격: ");
				}
				Console.WriteLine($"체력: {myBattleHP}, 전투력: {myBattlePower}, 상대체력: {foeBattleHP}, 상대전투력: {foeBattlePower}");
				isMyAttack = !isMyAttack;
			}

			Console.WriteLine("전투 종료");
			gameState = "InGame";

			bool win = myBattleHP == 0 ? true : false;
			if (win)
			{
				Console.WriteLine("승리");
			}
			else
			{
				Console.WriteLine("패배");
			}

			return win;
		}

		static void SetFoeStatus(string foeName)
		{
			switch (foeName)
			{
				case "고블린":
					foeBattleHP = 5;
					foeBattlePower = 1;
					break;
			}
		}

		static void Main(string[] args)
		{
			string userInput;
			int userInputNum;
			Random random = new Random();
			int textIndex = GET_RANDOM;

			SetStoryText();

			Console.WriteLine("모험가 이야기\n시작하려면 아무 키나 입력하세요");
			userInput = Console.ReadLine();
			if (userInput != null)
			{
				gameState = "InGame";
			}

			Console.WriteLine("모험 시작(내정보(!), 인벤토리(@))");

			while (health > 0)
			{
				if (textIndex == GET_RANDOM)
				{
					textIndex = random.Next(0, STORY_TEXT_COUNT);
				}

				Console.WriteLine(storyText[textIndex]);
				Console.WriteLine();
				PrintSelectionText(textIndex);
				while (true)
				{
					Console.Write("입력: ");
					userInput = Console.ReadLine();
					int.TryParse(userInput, out userInputNum);

					if (userInputNum > 0 && userInputNum <= selectionCount[textIndex])
					{
						textIndex = Event(textIndex, userInputNum);
						break;
					}
					else if (userInput == "!")
					{
						Console.WriteLine($"내정보: 체력 {health}, 힘: {power}");
					}
					else if (userInput == "@")
					{
						Console.WriteLine($"인벤토리: ");
						foreach (string item in inventory)
						{
							Console.WriteLine($"{item} ");
						}
					}
					else
					{
						Console.WriteLine("잘못된 입력");
						continue;
					}
				}

				Console.WriteLine("-------------------------------");
			}
		}
	}
}
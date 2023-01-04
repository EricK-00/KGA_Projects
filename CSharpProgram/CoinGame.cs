using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpProgram
{
	struct Position3
	{
		public int X;
		public int Y;
		public int Z;

		public Position3(int x, int y, int z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public static bool operator ==(Position3 p1, Position3 p2)
		{
			if (p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z)
				return true;
			else
				return false;
		}

		public static bool operator !=(Position3 p1, Position3 p2)
		{
			if (p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z)
				return false;
			else
				return true;
		}
	}

	struct Position
	{
		public int Row { get; private set; }
		public int Col { get; private set; }

		public Position(int row, int column)
		{
			this.Row = row;
			this.Col = column;
		}

		public void MoveLeft()
		{
			Col -= 1;
		}
		public void MoveRight()
		{
			Col += 1;
		}
		public void MoveUp()
		{
			Row -= 1;
		}
		public void MoveDown()
		{
			Row += 1;
		}

		public static Position operator -(Position p1, Position p2)
		{
			return new Position(p1.Row - p2.Row, p1.Col - p2.Col);
		}

		public static Position operator +(Position p1, Position p2)
		{
			return new Position(p1.Row + p2.Row, p1.Col + p2.Col);
		}

		public static bool operator ==(Position p1, Position p2)
		{
			if (p1.Row == p2.Row && p1.Col == p2.Col)
				return true;
			else
				return false;
		}

		public static bool operator !=(Position p1, Position p2)
		{
			if (p1.Row == p2.Row && p1.Col == p2.Col)
				return false;
			else
				return true;
		}
	}

	class CoinGame
	{
		int[,] map;
		int mapSize;
		Position playerPos;

		bool isEnd;
		int coin;
		int turn;
		int floar;
		MapCode mapCode;
		Position3 mapPos3;

		Position leftPortalPos;
		Position rightPortalPos;
		Position upPortalPos;
		Position downPortalPos;
		Position topPortalPos;
		Position bottomPortalPos;

		List<Position> blankPosList;

		Dictionary<MapCode, int[,]> mapDictionary;

		enum MapCode
		{
			LEFT = 0b0001,
			RIGHT = 0b0010,
			UP = 0b0100,
			DOWN = 0b1000,
			MID = 0b0011_1111,
			TOP = 0b0001_0000,
			BOTTOM = 0b0010_0000
		}

		enum MapObject
		{
			NONE = 0,
			WALL,
			PLAYER,
			COIN,
			PORTAL,
			UP_LADDER,
			DOWN_LADDER
		}

		public CoinGame()
		{
			isEnd = false;
			coin = 0;
			turn = 0;
			floar = 1;
			mapCode = MapCode.MID;
			mapPos3 = new Position3(0, 0, 0);
			blankPosList = new List<Position>();
			mapDictionary = new Dictionary<MapCode, int[,]>();

			InitializeGame();
		}

		public void PlayGame()
		{
			while (!IsGameOver())
				MovePlayer();
			Console.WriteLine("게임 종료");
		}

		private void InitializeGame()
		{
			//맵 크기 받기
			while (true)
			{
				Console.Write("맵의 크기를 입력하세요(최소 5이상): ");
				if (int.TryParse(Console.ReadLine(), out mapSize) && mapSize >= 5)
				{
					map = new int[mapSize, mapSize];
					break;
				}
				else
				{
					Console.WriteLine("잘못된 입력입니다.");
				}
			}
			//맵 크기에 따라 포탈 위치 초기화
			leftPortalPos = new Position(mapSize / 2, 0);
			rightPortalPos = new Position(mapSize / 2, mapSize - 1);
			upPortalPos = new Position(0, mapSize / 2);
			downPortalPos = new Position(mapSize - 1, mapSize / 2);
			topPortalPos = new Position(mapSize / 2 - 1, mapSize / 2 - 1);
			bottomPortalPos = new Position(mapSize / 2 + 1, mapSize / 2 + 1);

			//맵 생성하기
			GenerateMap();

			//플레이어를 랜덤한 위치에 생성
			playerPos = new Position( new Random().Next(1, mapSize - 1), new Random().Next(1, mapSize - 1));
			map[playerPos.Row, playerPos.Col] = (int)MapObject.PLAYER;

			//맵 출력
			PrintMap();
			Console.WriteLine($"[현재 코인: {coin}]");
		}

		private void GenerateMap()
		{
			//벽과 빈 칸 생성
			for (int row = 0; row < mapSize; row++)
			{
				for (int column = 0; column < mapSize; column++)
				{
					if (row > 0 && row < mapSize - 1 && column > 0 && column < mapSize - 1)
						map[row, column] = (int)MapObject.NONE;
					else
						map[row, column] = (int)MapObject.WALL;
				}
			}

			//포탈 생성
			if (((int)mapCode & (int)MapCode.LEFT) == (int)MapCode.LEFT)
			{
				map[rightPortalPos.Row, rightPortalPos.Col] = (int)MapObject.PORTAL;
			}
			if (((int)mapCode & (int)MapCode.RIGHT) == (int)MapCode.RIGHT)
			{
				map[leftPortalPos.Row, leftPortalPos.Col] = (int)MapObject.PORTAL;
			}
			if (((int)mapCode & (int)MapCode.UP) == (int)MapCode.UP)
			{
				map[downPortalPos.Row, downPortalPos.Col] = (int)MapObject.PORTAL;
			}
			if (((int)mapCode & (int)MapCode.DOWN) == (int)MapCode.DOWN)
			{
				map[upPortalPos.Row, upPortalPos.Col] = (int)MapObject.PORTAL;
			}
			if (((int)mapCode & (int)MapCode.TOP) == (int)MapCode.TOP)
			{
				map[bottomPortalPos.Row, bottomPortalPos.Col] = (int)MapObject.DOWN_LADDER;
			}
			if (((int)mapCode & (int)MapCode.BOTTOM) == (int)MapCode.BOTTOM)
			{
				map[topPortalPos.Row, topPortalPos.Col] = (int)MapObject.UP_LADDER;
			}
		}

		//현재 맵 저장하기
		private void SaveMap(MapCode mapCode_)
		{
			int[,] currentMap = new int[mapSize, mapSize];
			for (int i = 0; i < mapSize; i++)
			{
				for (int j = 0; j < mapSize; j++)
				{
					currentMap[i, j] = map[i, j];
				}
			}

			if (!mapDictionary.ContainsKey(mapCode_))
				mapDictionary.Add(mapCode_, currentMap);
			else
				mapDictionary[mapCode_] = currentMap;
		}

		//맵 불러오기
		private void LoadMap(MapCode mapCode_)
		{
			if (!mapDictionary.ContainsKey(mapCode_))
				GenerateMap();
			else
				map = mapDictionary[mapCode_];
		}

		//랜덤한 빈 칸의 위치 가져오기
		private Position GetRandomBlankPosition()
		{
			blankPosList.Clear();
			for (int i = 0; i < mapSize; i++)
			{
				for (int j = 0; j < mapSize; j++)
				{
					if (map[i, j] == (int)MapObject.NONE)
					{
						blankPosList.Add(new Position(i, j));
					}
				}
			}

			int randomIndex = new Random().Next(0, blankPosList.Count);
			return blankPosList[randomIndex];
		}

		//코인 생성하기
		private void GenerateCoin()
		{
			//코인 생성
			Position coinPos = GetRandomBlankPosition();
			map[coinPos.Row, coinPos.Col] = (int)MapObject.COIN;

			PrintMap();
		}

		private void MovePlayer()
		{
			//userInput;
			bool isValidKey = false;

			//코인 생성
			if (turn % 4 == 0)
			{
				GenerateCoin();
			}


			Console.Write("(w)위, (a)왼쪽, (s)아래, (d)오른쪽, (x)종료: ");
			//이동 입력 받기
			while (!isValidKey)
			{

				ConsoleKeyInfo inputKey = Console.ReadKey();

				//입력한 문자 확인
				Position nextPos = playerPos;
				switch (inputKey.Key)
				{
					case ConsoleKey.W:
						nextPos += new Position(-1, 0);
						break;
					case ConsoleKey.S:
						nextPos += new Position(+1, 0);
						break;
					case ConsoleKey.A:
						nextPos += new Position(0, -1);
						break;
					case ConsoleKey.D:
						nextPos += new Position(0, +1);
						break;
					case ConsoleKey.X:
						isEnd = true;
						return;
					default:
						continue;
				}
				Console.WriteLine();

				//다음 칸이 벽일 때
				if (map[nextPos.Row, nextPos.Col] == (int)MapObject.WALL)
				{
					Console.WriteLine($"벽입니다. 다른 키를 입력해주세요. [입력한 키: {inputKey.Key}]");
					continue;
				}
				//다음 칸이 벽이 아닐 때
				else
				{
					isValidKey = true;
					//이전 위치 초기화
					map[playerPos.Row, playerPos.Col] = (int)MapObject.NONE;

					if (map[nextPos.Row, nextPos.Col] == (int)MapObject.PORTAL)
					{
						//상
						if (nextPos == upPortalPos)
						{
							mapPos3 = new Position3(0, 0, mapPos3.Z - 1);
							nextPos = new Position(mapSize - 2, nextPos.Col);
						}
						//하
						else if (nextPos == downPortalPos)
						{
							mapPos3 = new Position3(0, 0, mapPos3.Z + 1);
							nextPos = new Position(1, nextPos.Col);
						}
						//좌
						else if (nextPos == leftPortalPos)
						{
							mapPos3 = new Position3(mapPos3.X - 1, 0, 0);
							nextPos = new Position(nextPos.Row, mapSize - 2);
						}
						//우
						else if (nextPos == rightPortalPos)
						{
							mapPos3 = new Position3(mapPos3.X + 1, 0, 0);
							nextPos = new Position(nextPos.Row, 1);
						}
						SaveMap(mapCode);
						mapCode = GetCurrentMapCode();
						LoadMap(mapCode);
					}
					else if (map[nextPos.Row, nextPos.Col] == (int)MapObject.UP_LADDER)
					{
						mapPos3 = new Position3(0, mapPos3.Y + 1, 0);
						nextPos = playerPos;
						floar += 1;
						SaveMap(mapCode);
						mapCode = GetCurrentMapCode();
						LoadMap(mapCode);
					}
					else if (map[nextPos.Row, nextPos.Col] == (int)MapObject.DOWN_LADDER)
					{
						mapPos3 = new Position3(0, mapPos3.Y - 1, 0);
						nextPos = playerPos;
						floar -= 1;
						SaveMap(mapCode);
						mapCode = GetCurrentMapCode();
						LoadMap(mapCode);
					}
					else if (map[nextPos.Row, nextPos.Col] == (int)MapObject.COIN)
						++coin;

					playerPos = nextPos;
					map[playerPos.Row, playerPos.Col] = (int)MapObject.PLAYER;
				}
			}

			//결과 맵 출력
			PrintMap();
			++turn;
		}

		private MapCode GetCurrentMapCode()
		{
			if (mapPos3 == new Position3(-1, 0, 0))
				return MapCode.LEFT;
			else if (mapPos3 == new Position3(1, 0, 0))
				return MapCode.RIGHT;
			else if (mapPos3 == new Position3(0, 0, -1))
				return MapCode.UP;
			else if (mapPos3 == new Position3(0, 0, 1))
				return MapCode.DOWN;
			else if (mapPos3 == new Position3(0, 1, 0))
				return MapCode.TOP;
			else if (mapPos3 == new Position3(0, -1, 0))
				return MapCode.BOTTOM;
			else
				return MapCode.MID;
		}

		private void PrintMap()
		{
			Console.Clear();
			Console.WriteLine($"[코인: {coin} / 10]");
			//맵 출력
			string word;
			for (int row = 0; row < mapSize; row++)
			{
				for (int column = 0; column < mapSize; column++)
				{
					switch (map[row, column])
					{
						case (int)MapObject.PLAYER:
							word = "★";
							break;
						case (int)MapObject.WALL:
							word = "□";
							break;
						case (int)MapObject.COIN:
							word = "＠";
							break;
						case (int)MapObject.PORTAL:
							word = "▣";
							break;
						case (int)MapObject.UP_LADDER:
							word = "↑";
							break;
						case (int)MapObject.DOWN_LADDER:
							word = "↓";
							break;
						default:
							word = "..";
							break;
					}
					Console.Write($"{word} ");
				}
				Console.WriteLine();
			}

			if (floar > 0)
			Console.WriteLine($"{floar}층");
			else
				Console.WriteLine($"지하 1층");
			PrintMiniMap();
		}

		private void PrintMiniMap()
		{
			Console.WriteLine("\n ------");
			switch (mapCode)
			{
				case MapCode.LEFT:
					Console.WriteLine("|  ○  |".PadRight(8));
					Console.WriteLine("|◆○○|".PadRight(8));
					Console.WriteLine("|  ○  |".PadRight(8));
					break;
					case MapCode.RIGHT:
					Console.WriteLine("|  ○  |".PadRight(8));
					Console.WriteLine("|○○◆|".PadRight(8));
					Console.WriteLine("|  ○  |".PadRight(8));
					break;
					case MapCode.UP:
					Console.WriteLine("|  ◆  |".PadRight(8));
					Console.WriteLine("|○○○|".PadRight(8));
					Console.WriteLine("|  ○  |".PadRight(8));
					break;
					case MapCode.DOWN:
					Console.WriteLine("|  ○  |".PadRight(8));
					Console.WriteLine("|○○○|".PadRight(8));
					Console.WriteLine("|  ◆  |".PadRight(8));
					break;
				case MapCode.TOP:
				case MapCode.BOTTOM:
					Console.WriteLine("|      |".PadRight(8));
					Console.WriteLine("|  ◆  |".PadRight(8));
					Console.WriteLine("|      |".PadRight(8));
					break;
				default:
					Console.WriteLine("|  ○  |".PadRight(8));
					Console.WriteLine("|○◆○|".PadRight(8));
					Console.WriteLine("|  ○  |".PadRight(8));
					break;
			}

			Console.WriteLine(" ------");
		}

		private bool IsGameOver()
		{
			if (!isEnd && coin < 10)
				return false;
			else
				return true;
		}
	}
}

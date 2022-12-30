using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpProgram
{
	class Board
	{
		int rowSize;
		int columnSize;
		int[,] board;
		int playerRow;
		int playerColumn;

		bool isEnd = false;
		int coin = 0;
		int turn = 0;

		int[] blankRows;
		int[] blankColumns;
		int maximumBlankCount;
		int currentBlankCount = 0;

		enum BoardObject
		{
			NONE = 0,
			WALL,
			PLAYER,
			COIN
		}

		public void PlayGame()
		{
			InitializeGame();
			while (!isEnd && coin < 3)
				MovePlayer();
			Console.WriteLine("게임 종료");
		}

		private void InitializeGame()
		{
			//보드 크기 받기
			int boardSize;
			while (true)
			{
				Console.Write("보드의 크기를 입력하세요(최소 4이상): ");
				if (int.TryParse(Console.ReadLine(), out boardSize) && boardSize >= 4)
				{
					rowSize = columnSize = boardSize;
					board = new int[rowSize, columnSize];
					break;
				}
				else
				{
					Console.WriteLine("잘못된 입력입니다.");
				}
			}

			//벽과 빈 칸 생성
			for (int row = 0; row < rowSize; row++)
			{
				for (int column = 0; column < columnSize; column++)
				{
					if (row > 0 && row < rowSize - 1 && column > 0 && column < columnSize - 1)
						board[row, column] = (int)BoardObject.NONE;
					else
						board[row, column] = (int)BoardObject.WALL;
				}
			}

			//플레이어 생성
			playerRow = new Random().Next(1, rowSize - 1);
			playerColumn = new Random().Next(1, columnSize - 1);
			board[playerRow, playerColumn] = (int)BoardObject.PLAYER;

			//보드 출력
			PrintBoard();
			Console.WriteLine($"[현재 코인: {coin}]");

			maximumBlankCount = (rowSize - 2) * (columnSize - 2) - 1;
			blankRows = new int[maximumBlankCount];
			blankColumns = new int[maximumBlankCount];
		}

		//랜덤한 빈 칸의 위치 가져오기
		private (int, int) GetRandomBlankPosition()
		{
			int boardStartIndex = 0;
			currentBlankCount = 0;
			for (int i = 0; i < maximumBlankCount; i++)
			{
				for (int j = boardStartIndex; j < board.Length; j++)
				{
					if (board[j / columnSize, j % columnSize] == (int)BoardObject.NONE)
					{
						blankRows[i] = j / columnSize;
						blankColumns[i] = j % columnSize;
						++currentBlankCount;
						boardStartIndex = j + 1;
						break;
					}
				}
			}

			int randomIndex = new Random().Next(0, currentBlankCount);
			return (blankRows[randomIndex], blankColumns[randomIndex]);
		}

		//코인 생성하기
		private void GenerateCoin()
		{
			string noticeStr = "코인이 랜덤한 위치에 생성됩니다.";
			foreach (char item in noticeStr)
			{
				Task.Delay(100).Wait();
				//Thread.Sleep(100);
				Console.Write(item);
			}
			Task.Delay(1000).Wait();
			//Thread.Sleep(1000);
			Console.Clear();

			//코인 생성
			(int coinRow, int coinColumn) = GetRandomBlankPosition();
			board[coinRow, coinColumn] = (int)BoardObject.COIN;

			PrintBoard();
		}

		private void MovePlayer()
		{
			char userInput;
			bool isValidKey = false;

			//코인 생성
			if (turn % 3 == 0)
			{
				GenerateCoin();
				Console.WriteLine($"[현재 코인: {coin}]");
			}

			//이동 입력 받기
			while (!isValidKey)
			{
				Console.Write("w(W, ㅈ)-위로 이동, a(A, ㅁ)-왼쪽으로 이동, s(S, ㄴ)-아래로 이동, d(D, ㅇ)-오른쪽으로 이동, q(Q, ㅂ)-종료 : ");

				//문자 하나가 아닐 때
				if (!char.TryParse(Console.ReadLine(), out userInput))
				{
					Console.WriteLine("잘못된 입력입니다. 문자를 하나 입력해주세요.");
					continue;
				}
				//입력한 문자 확인
				int nextRow = 0;
				int nextColumn = 0;
				switch (userInput)
				{
					case 'w':
					case 'W':
					case 'ㅈ':
						nextRow = playerRow - 1;
						nextColumn = playerColumn;
						break;
					case 'a':
					case 'A':
					case 'ㅁ':
						nextRow = playerRow;
						nextColumn = playerColumn - 1;
						break;
					case 's':
					case 'S':
					case 'ㄴ':
						nextRow = playerRow + 1;
						nextColumn = playerColumn;
						break;
					case 'd':
					case 'D':
					case 'ㅇ':
						nextRow = playerRow;
						nextColumn = playerColumn + 1;
						break;
					case 'q':
					case 'Q':
					case 'ㅂ':
						isEnd = true;
						return;
					default:
						Console.WriteLine("잘못된 문자입니다.");
						continue;
				}

				//다음 칸이 벽일 때
				if (board[nextRow, nextColumn] == (int)BoardObject.WALL)
				{
					Console.WriteLine($"벽입니다. 다른 키를 입력해주세요(현재 입력한 키: {userInput}).");
					continue;
				}
				//다음 칸이 벽이 아닐 때
				else
				{
					isValidKey = true;
					board[playerRow, playerColumn] = (int)BoardObject.NONE;
					if (board[nextRow, nextColumn] == (int)BoardObject.COIN)
						++coin;

					playerRow = nextRow;
					playerColumn = nextColumn;
					board[playerRow, playerColumn] = (int)BoardObject.PLAYER;
				}
			}

			//결과 보드 출력
			Console.Clear();
			PrintBoard();
			Console.WriteLine($"[현재 코인: {coin}]");

			++turn;
		}

		private void PrintBoard()
		{
			//보드 출력
			for (int row = 0; row < rowSize; row++)
			{
				for (int column = 0; column < columnSize; column++)
				{
					string word;
					switch (board[row, column])
					{
						case (int)BoardObject.PLAYER:
							word = "옷";
							break;
						case (int)BoardObject.WALL:
							word = "□";
							break;
						case (int)BoardObject.COIN:
							word = "C ";
							break;
						default:
							word = ". ";
							break;
					}
					Console.Write($"{word} ");
				}
				Console.WriteLine();
			}
		}
	}
}

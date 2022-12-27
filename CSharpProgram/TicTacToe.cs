using System;

namespace CSharpProgram
{
	class TicTacToe
	{
		/**
		 * 보드 크기는 3x3
		 * 컴퓨터는 중앙이 비었으면 중앙 선점, 이후에 빈자리 아무곳에 둔다
		 */

		enum PlayerType
		{
			NONE = 0,
			PLAYER,
			COMPUTER
		}

		public static void PlayTicTacToe()
		{
			int[,] gameBoard = new int[3, 3];
			int playerX, playerY = 0;
			bool isValidLocation = false;
			bool isPlayerTurn = false;
			bool isGameOver = false;

			string playerIcon = string.Empty;
			string playerType = string.Empty;

			while (isGameOver == false)
			{
				isValidLocation = false;
				isPlayerTurn = true;
				playerType = $"[플레이어]";


				// { 플레이어에게서 좌표를 입력받는다.
				while (true)
				{
					if (isValidLocation)
						break;

					Console.Write("[플레이어] (x) 좌표: ");
					int.TryParse(Console.ReadLine(), out playerX);
					Console.Write("[플레이어] (y) 좌표: ");
					int.TryParse(Console.ReadLine(), out playerY);

					if (gameBoard[playerY, playerX].Equals((int)PlayerType.NONE))
					{
						gameBoard[playerY, playerX] = (int)PlayerType.PLAYER;
						isValidLocation = true;
					}
					else
					{
						Console.WriteLine("[System] 해당좌표는 비어있지 않습니다.\n다른 좌표를 입력하세요.");
						isValidLocation = false;
					}       //else: 보드가 빈 곳이 아닌 경우
				}       //loop: 플레이어의 좌표를 입력받는 루프
						// } 플레이어에게서 좌표를 입력받는다.


				// { 플레이어의 턴 진행을 보드에 출력한다
				for (int y = 0; y <= gameBoard.GetUpperBound(0); y++)
				{
					Console.WriteLine("---|---|---");
					for (int x = 0; x <= gameBoard.GetUpperBound(1); x++)
					{
						switch (gameBoard[y, x])
						{
							case (int)PlayerType.PLAYER:
								playerIcon = "O";
								break;
							case (int)PlayerType.COMPUTER:
								playerIcon = "X";
								break;
							default:
								playerIcon = " ";
								break;
						}
						Console.Write($" {playerIcon} ");

						if (x == gameBoard.GetUpperBound(1)) { /* Do nothing */ }
						else { Console.Write("|"); }
					}
					Console.WriteLine();
				}
				Console.WriteLine("---|---|---");
				// } 플레이어의 턴 진행을 보드에 출력한다

				// { 게임이 끝났는지 보드 검사
				Console.WriteLine();
				isGameOver = false;
				for (int y = 0; y <= gameBoard.GetUpperBound(0); y++)
				{
					if (gameBoard[y, 0].Equals((int)PlayerType.PLAYER) &&
						gameBoard[y, 1].Equals((int)PlayerType.PLAYER) &&
						gameBoard[y, 2].Equals((int)PlayerType.PLAYER))
					{
						isGameOver = true;
					}
					else { continue; }
				}       //loop: 가로 방향으로 검사하는 루프
				for (int x = 0; x <= gameBoard.GetUpperBound(1); x++)
				{
					if (gameBoard[0, x].Equals((int)PlayerType.PLAYER) &&
						gameBoard[1, x].Equals((int)PlayerType.PLAYER) &&
						gameBoard[2, x].Equals((int)PlayerType.PLAYER))
					{
						isGameOver = true;
					}
					else { continue; }
				}       //loop: 세로 방향으로 검사하는 루프

				//대각선 방향으로 검사
				if (gameBoard[0, 0].Equals((int)PlayerType.PLAYER) &&
					gameBoard[1, 1].Equals((int)PlayerType.PLAYER) &&
					gameBoard[2, 2].Equals((int)PlayerType.PLAYER))
				{
					isGameOver = true;
				}
				if (gameBoard[0, 2].Equals((int)PlayerType.PLAYER) &&
					gameBoard[1, 1].Equals((int)PlayerType.PLAYER) &&
					gameBoard[2, 0].Equals((int)PlayerType.PLAYER))
				{
					isGameOver = true;
				}
				// } 게임이 끝났는지 보드 검사

				// 게임이 끝난 경우 루프 탈출
				if (isGameOver) { break; }

				// 게임이 끝나지 않은 경우 턴 교체
				isPlayerTurn = false;
				playerType = $"[컴퓨터]";
				bool isComputerDoing = false;

				Console.WriteLine($"{playerType}의 턴");

				// { 1. 컴퓨터는 중앙이 비어 있으면 선점
				if (isComputerDoing == false)
				{
					if (gameBoard[1, 1].Equals((int)PlayerType.NONE))
					{
						gameBoard[1, 1] = (int)PlayerType.COMPUTER;
						isComputerDoing = true;
					}		// if: 중앙이 비어 있는 경우
					else { /* Do nothing */ }
				}		// if: 아직 컴퓨터가 아무것도 하지 않은 경우
				else { /* Do noting */ }
				// } 1. 컴퓨터는 중앙이 비어 있으면 선점

				// { 2. 컴퓨터가 적당히 빈 자리 찾아서 착수
				if (isComputerDoing == false)
				{
					for (int y = 0; y <= gameBoard.GetUpperBound(0); y++)
					{
						for (int x = 0; x <= gameBoard.GetUpperBound(1); x++)
						{
							if (isComputerDoing == false && gameBoard[y,x].Equals((int)PlayerType.NONE))
							{
								gameBoard[y, x] = (int)PlayerType.COMPUTER;
								isComputerDoing = true;
								break;
							}		// if: 서치한 자리가 비어있는 경우
							else { continue; }
						}		// loop: search horizontal
					}       //loop: search vertical
				}       // if: 아직 컴퓨터가 아무것도 하지 않은 경우
				else { /* Do noting*/ }
				// } 2. 컴퓨터가 적당히 빈 자리 찾아서 착수

				// { 컴퓨터의 턴 진행을 보드에 출력한다
				for (int y = 0; y <= gameBoard.GetUpperBound(0); y++)
				{
					Console.WriteLine("---|---|---");
					for (int x = 0; x <= gameBoard.GetUpperBound(1); x++)
					{
						switch (gameBoard[y, x])
						{
							case (int)PlayerType.PLAYER:
								playerIcon = "O";
								break;
							case (int)PlayerType.COMPUTER:
								playerIcon = "X";
								break;
							default:
								playerIcon = " ";
								break;
						}		//switch
						Console.Write($" {playerIcon} ");

						// Print Separator
						if (x == gameBoard.GetUpperBound(1)) { /* Do nothing */ }
						else { Console.Write("|"); }
					}		//loop: 한 줄에서 출력할 내용을 연산한다
					Console.WriteLine();
				}		//loop
				Console.WriteLine("---|---|---");
				// } 컴퓨터의 턴 진행을 보드에 출력한다

				// { 게임이 끝났는지 보드 검사
				Console.WriteLine();
				isGameOver = false;
				for (int y = 0; y <= gameBoard.GetUpperBound(0); y++)
				{
					if (gameBoard[y, 0].Equals((int)PlayerType.COMPUTER) &&
						gameBoard[y, 1].Equals((int)PlayerType.COMPUTER) &&
						gameBoard[y, 2].Equals((int)PlayerType.COMPUTER))
					{
						isGameOver = true;
					}
					else { continue; }
				}       //loop: 가로 방향으로 검사하는 루프
				for (int x = 0; x <= gameBoard.GetUpperBound(1); x++)
				{
					if (gameBoard[0, x].Equals((int)PlayerType.COMPUTER) &&
						gameBoard[1, x].Equals((int)PlayerType.COMPUTER) &&
						gameBoard[2, x].Equals((int)PlayerType.COMPUTER))
					{
						isGameOver = true;
					}
					else { continue; }
				}       //loop: 세로 방향으로 검사하는 루프

				//대각선 방향으로 검사
				if (gameBoard[0, 0].Equals((int)PlayerType.COMPUTER) &&
					gameBoard[1, 1].Equals((int)PlayerType.COMPUTER) &&
					gameBoard[2, 2].Equals((int)PlayerType.COMPUTER))
				{
					isGameOver = true;
				}
				if (gameBoard[0, 2].Equals((int)PlayerType.COMPUTER) &&
					gameBoard[1, 1].Equals((int)PlayerType.COMPUTER) &&
					gameBoard[2, 0].Equals((int)PlayerType.COMPUTER))
				{
					isGameOver = true;
				}
				// } 게임이 끝났는지 보드 검사
			}       // loop: 틱택토 게임 루프

			Console.WriteLine($"{playerType}의 승리!");
		}
	}
}
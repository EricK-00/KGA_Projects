using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgram
{
	/**
	 * 10 x 10 보드
	 * debug mode에서 지뢰가 아닌 곳은 .(닷)으로, 지뢰인 곳은 #(샵)으로 표현
	 * play mode 에서는 확인되지 않은 곳은 전부 □(스퀘어)로 표현
	 * 첫 턴에 지뢰를 밟으면 해당 칸에 지뢰를 삭제
	 */

	class MineSweeper
	{
		public static void PlaytMineSweeper()
		{
			Random randomMine = new Random();
			const int MINE_PERCENTAGE = 30;
			const int BOARD_SIZE_X = 10;
			const int BOARD_SIZE_Y = 10;

			bool isDebugMode = true;
			bool isGameOver = false;
			bool isPlayerWin = false;
			int playerTurnCnt = 0;

			/**
			 * 10 x 10 보드에 지뢰 초기화
			 * 
			 * gameBoard 상태
			 *	지뢰: MINE_PERCENTAGE 미만 값
			 * 빈 칸: MINE_PERCENTAGE 이상 값
			 * 
			 * playBoard 상태
			 * -2: 지뢰 있음
			 * -1: 초기값
			 * n: 주변 9타일 이내에 지뢰 수(0일 경우 ■ 표기, 양수일 경우 정수 표기)
			 * 
			 * mineCntMap 상태
			 * -1: 지뢰 있음
			 *	n: wnqus 9타일 이내에 지뢰 수
			 */

			int[,] gameBoard = new int[BOARD_SIZE_X, BOARD_SIZE_Y];
			int[,] playBoard = new int[BOARD_SIZE_X, BOARD_SIZE_Y];
			int[,] mineCntMap = new int[BOARD_SIZE_X, BOARD_SIZE_Y];

			for (int y = 0; y < BOARD_SIZE_Y; y++)
			{
				for (int x = 0; x < BOARD_SIZE_X; x++)
				{
					gameBoard[y, x] = randomMine.Next(1, 100 + 1);
					playBoard[y, x] = -1;

					if (gameBoard[y, x] < MINE_PERCENTAGE)
					{
						mineCntMap[y, x] = -1;
					}
					else
					{
						mineCntMap[y, x] = 0;
					}
				}
			}

			while (isGameOver == false)
			{
				// { 현재 보드의 상태를 플레이 시점으로 보여준다
				for (int y = 0; y < BOARD_SIZE_Y; y++)
				{
					for (int x = 0; x < BOARD_SIZE_X; x++)
					{
						switch (playBoard[y, x])
						{
							case -2:
								Console.Write("X".PadRight(3, ' '));
								break;
							case -1:
								Console.Write("□".PadRight(2, ' '));
								break;
							case 0:
								Console.Write("■".PadRight(2, ' '));
								break;
							default:
								Console.Write($"{playBoard[y, x]}".PadRight(3, ' '));
								break;
						}       //switch
					}       //loop
					Console.WriteLine();
				}       // loop: 현재 보드의 상태를 출력하는 루프
				Console.WriteLine();
				// } 현재 보드의 상태를 플레이 시점으로 보여준다

				int playerX = 0;
				int playerY = 0;
				bool isLocationValid = false;

				// { 플레이어 좌표 입력
				while (isLocationValid == false)
				{
					Console.Write("[플레이어] x 좌표 입력: ");
					int.TryParse(Console.ReadLine(), out playerX);
					Console.Write("[플레이어] y 좌표 입력: ");
					int.TryParse(Console.ReadLine(), out playerY);

					//입력한 좌표의 유효성을 검사한다
					isLocationValid =
						(0 <= playerX && playerX < BOARD_SIZE_X) &&
						(0 <= playerY && playerY < BOARD_SIZE_Y);

					if (isLocationValid == false)
					{
						Console.WriteLine($"[System] 해당 좌표는 유효하지 않습니다. 다른 좌표를 입력하세요.\n");
						continue;
					}       // if: 좌표를 잘못 입력한 경우

					// 좌표를 제대로 입력한 경우만 이 아래로 코드가 진행됨
					// 왜냐하면 유효하지 않은 경우 위에서 continue

					// 플레이 보드에서 선택 가능한지 검사한다
					isLocationValid = isLocationValid && playBoard[playerY, playerX].Equals(-1);
					if (isLocationValid == false)
					{
						Console.WriteLine($"[System] 해당 좌표는 이미 오픈되었습니다. 다른 좌표를 입력하세요.\n");
						continue;
					}       //if : 오픈된 좌표를 선택한 경우

					// 좌표를 제대로 입력한 경우만 이 아래로 코드가 진행됨
					// 왜냐하면 유효하지 않은 경우 위에서 continue
				}       // loop
				playerTurnCnt++;
				// } 플레이어 좌표 입력

				// 현재 첫 턴이라면 해당 좌표에 지뢰가 있어도 지워준다
				if (playerTurnCnt.Equals(1))
				{
					gameBoard[playerY, playerX] = MINE_PERCENTAGE + 1;
					mineCntMap[playerY, playerX] = 0;
					playBoard[playerY, playerX] = -1;

					// { 지뢰의 수를 세어서 지도를 생성한다
					for (int y = 0; y < BOARD_SIZE_Y; y++)
					{
						for (int x = 0; x < BOARD_SIZE_X; x++)
						{
							// 지뢰가 없는 곳은 넘어간다
							if (mineCntMap[y, x].Equals(-1) == false) { continue; }

							// 지뢰 주변 9타일에 수를 1씩 추가한다
							bool isSearchTileValid = false;
							for (int searchY = y - 1; searchY <= y + 1; searchY++)
							{
								for (int searchX = x - 1; searchX <= x + 1; searchX++)
								{
									// 유효하지 않은 좌표는 패스한다
									isSearchTileValid =
										(0 <= searchX && searchX < BOARD_SIZE_X) &&
										(0 <= searchY && searchY < BOARD_SIZE_Y);
									if (isSearchTileValid == false) { continue; }

									// 9타일 서치 중에 지뢰가 있다면 패스한다
									if (mineCntMap[searchY, searchX].Equals(-1)) { continue; }
									mineCntMap[searchY, searchX]++;
								}
							}       // loop: 지뢰 주변 9타일을 순회하는 루프
						}
					}
					// } 지뢰의 수를 세어서 지도를 생성한다
				}       // if: 첫 턴인 경우

				// { 선택한 좌표에서 지뢰를 검사한다
				if (gameBoard[playerY, playerX] < MINE_PERCENTAGE)
				{
					isGameOver = true;
					isPlayerWin = false;
					playBoard[playerY, playerX] = -2;
				}       // if: 지뢰를 선택한 경우
				else
				{
					// { 선택한 타일 인근 9칸의 숫자를 오픈한다
					bool isSearchTileValid = false;
					for (int searchY = playerY - 1; searchY <= playerY + 1; searchY++)
					{
						for (int searchX = playerX - 1; searchX <= playerX + 1; searchX++)
						{
							//유효하지 않은 좌표는 패스한다
							isSearchTileValid =
								(0 <= searchX && searchX < BOARD_SIZE_X) &&
								(0 <= searchY && searchY <= BOARD_SIZE_Y);
							if (isSearchTileValid == false) { continue; }

							if (mineCntMap[searchY, searchX].Equals(-1))
							{
								playBoard[searchY, searchX] = -2;
							}       // if: 지뢰인 경우
							else
							{
								playBoard[searchY, searchX] = mineCntMap[searchY, searchX];
							}       // else: 지뢰가 아닌 경우
						}
					}       // loop: 선택한 타일 인근 9칸을 순회하는 루프
							// } 선택한 타일 인근 9칸의 숫자를 오픈한다
				}       // else: 지뢰 아닌, 빈 타일을 선택한 경우
						// } 선택한 좌표에서 지뢰를 검사한다


				// { 게임 승리 조건을 검사한다
				int unknownTileCnt = 0;
				for (int y = 0; y < BOARD_SIZE_Y; y++)
				{
					//오픈되지 않은 타일이 있는 경우 게임을 이어서 진행한다
					if (0 < unknownTileCnt) { break; }
					for (int x = 0; x < BOARD_SIZE_X; x++)
					{
						//오픈되지 않은 타일이 있는 경우 게임을 이어서 진행한다
						if (0 < unknownTileCnt) { break; }

						if (playBoard[y, x].Equals(-1) &&
							mineCntMap[y, x].Equals(-1) == false)
						{
							unknownTileCnt++;
						}       // if: 아직 오픈할 타일이 존재하고, 해당 타일이 지뢰가 아닌 경우
					}
				}       // loop: 플레이 보드를 순회하는 루프

				if (unknownTileCnt.Equals(0))
				{
					isGameOver = true;
					isPlayerWin = true;
				}
				// } 게임 승리 조건을 검사한다

				// { 게임 종료 조건을 검사한다
				if (isGameOver) { break; }
				// } 게임 종료 조건을 검사한다

				if (isDebugMode)
				{
					// { 현재 보드의 상태를 숫자 지도로 보여준다
					for (int y = 0; y < BOARD_SIZE_Y; y++)
					{
						for (int x = 0; x < BOARD_SIZE_X; x++)
						{
							Console.Write($"{mineCntMap[y, x]} ".PadLeft(3, ' '));
						}
						Console.WriteLine();
					}       // loop: 현재 보드의 상태를 출력
							// } 현재 보드의 상태를 숫자 지도로 보여준다

					// { 현재 게임 보드의 상태를 보여준다
					Console.WriteLine();
					for (int y = 0; y < BOARD_SIZE_Y; y++)
					{
						for (int x = 0; x < BOARD_SIZE_X; x++)
						{
							if (gameBoard[y,x] < MINE_PERCENTAGE)
							{
								Console.Write("# ".PadRight(3, ' '));
							}
							else
							{
								Console.Write(". ".PadRight(3, ' '));
							}
						}
						Console.WriteLine();
					}		// loop: 현재 게임 보드의 상태를 출력하는 루프
					Console.WriteLine();
					// } 현재 게임 보드의 상태를 보여준다
				}       // if: 디버그 모드에서 출력
			}       // loop: 게임 루프

			// { 현재 보드의 상태를 플레이 시점으로 보여준다
			Console.WriteLine();
			for (int y = 0; y < BOARD_SIZE_Y; y++)
			{
				for (int x = 0; x < BOARD_SIZE_X; x++)
				{
					switch(playBoard[y,x])
					{
						case -2:

							Console.Write("X".PadRight(3,' '));
							break;
						case -1:
							Console.Write("□".PadRight(2, ' '));
							break;
						case 0:
							Console.Write("■".PadRight(2, ' '));
							break;
						default:
							Console.Write($"{playBoard[y,x]}".PadRight(3,' '));
							break;
					}		//switch
				}
				Console.WriteLine();
			}       // loop: 현재 보드의 상태를 플레이 시점으로 출력하는 루프
			Console.WriteLine();
			// } 현재 보드의 상태를 플레이 시점으로 보여준다

			if (isPlayerWin)
			{
				Console.WriteLine("지뢰를 모두 찾아 승리했습니다");
			}
			else
			{
				Console.WriteLine("지뢰를 밟고 패배했습니다");
			}
		}
	}
}
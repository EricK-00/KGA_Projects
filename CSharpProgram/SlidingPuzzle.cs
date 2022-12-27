using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgram
{
	class SlidingPuzzle
	{
		int boardLength;
		int[,] board;
		int blankRow = -1;
		int blankCol = -1;
		int moveCount = 0;

		//슬라이딩 퍼즐
		public void PlaySlidingPuzzle()
		{
			InitializeGame();
			while (!IsGameOver())
				MoveBlank();
			GameOver();
		}

		//게임 초기화
		private void InitializeGame()
		{
			//보드 길이 입력 받기
			do
			{
				Console.Write("보드의 길이를 입력하세요(범위: 2 ~ 10): ");
			}
			while (!int.TryParse(Console.ReadLine(), out boardLength) || boardLength < 2 || boardLength > 10);

			board = new int[boardLength, boardLength];

			for (int i = 0; i < boardLength; i++)
			{
				for (int j = 0; j < boardLength; j++)
				{
					board[i, j] = i * boardLength + j;
				}
			}

			//보드 섞기(랜덤한 인덱스의 수와 스왑)
			Random random = new Random();
			for (int i = 0; i < boardLength; i++)
			{
				for (int j = 0; j < boardLength; j++)
				{
					int randomNum = random.Next(0, boardLength * boardLength);
					int temp = board[i, j];
					board[i, j] = board[randomNum / boardLength, randomNum % boardLength];
					board[randomNum / boardLength, randomNum % boardLength] = temp;
				}
			}
			//빈 칸 위치 설정하기
			SetBlankPos();

			//유효한 보드가 아닐 때 빈 칸을 제외한 숫자 섞기
			while (!IsValidBoard())
			{
				Console.WriteLine("유효한 보드가 아닙니다.");

				//보드 상태 출력하기
				PrintBoard();

				//아무 키나 입력 받기
				Console.WriteLine("아무 키나 입력해서 보드 섞기");
				string s = Console.ReadLine();

				//보드 섞기
				int randomNum1 = random.Next(0, boardLength * boardLength - 1);
				int randomNum2 = random.Next(0, boardLength * boardLength - 2);

				Console.WriteLine($"{randomNum1}, {randomNum2}");

				int blankIndex = blankRow * boardLength + blankCol;

				if (randomNum1 >= blankIndex + 1)
					randomNum1 += 1;
				if (randomNum2 >= blankIndex + 1)
					randomNum2 += 1;
				if (randomNum2 >= randomNum1)
					randomNum2 += 1;

				Console.WriteLine($"{randomNum1}, {randomNum2}");

				int temp = board[randomNum1 / boardLength, randomNum1 % boardLength];
				board[randomNum1 / boardLength, randomNum1 % boardLength] = board[randomNum2 / boardLength, randomNum2 % boardLength];
				board[randomNum2 / boardLength, randomNum2 % boardLength] = temp;
			}

			//처음 보드 상태 출력하기
			Console.Clear();
			PrintBoard();
		}

		//입력에 따라 빈 칸 이동하기
		private void MoveBlank()
		{
			int nextRow;
			int nextCol;

			//유효한 입력 받기
			while (true)
			{
				char userInput;

				Console.Write("움직일 방향을 선택하세요(w: 위, a: 왼쪽, s: 아래, d: 오른쪽): ");

				if (!char.TryParse(Console.ReadLine(), out userInput))
				{
					Console.WriteLine("잘못된 입력입니다.");
					continue;
				}

				nextRow = blankRow;
				nextCol = blankCol;
				switch (userInput)
				{
					case 'w':
						nextRow -= 1;
						break;
					case 'a':
						nextCol -= 1;
						break;
					case 's':
						nextRow += 1;
						break;
					case 'd':
						nextCol += 1;
						break;
					default:
						Console.WriteLine("잘못된 입력입니다.");
						continue;
				}

				if (nextRow < 0 || nextRow >= boardLength || nextCol < 0 || nextCol >= boardLength)
				{
					Console.WriteLine($"벽입니다. 다른 문자를 입력해주세요.[현재 입력한 문자: {userInput}]");
					continue;
				}

				break;
			}

			//빈 칸을 보드에서 이동(목적지와 현재 빈 칸의 위치를 스왑)하고, 이동카운트 올리기
			int temp = board[nextRow, nextCol];
			board[nextRow, nextCol] = board[blankRow, blankCol];
			board[blankRow, blankCol] = temp;
			blankRow = nextRow;
			blankCol = nextCol;
			++moveCount;

			//보드 상태 출력하기
			Console.Clear();
			PrintBoard();
		}

		//풀 수 있는 보드인지 확인하기
		//해결 가능한 보드일 조건:
		//보드의 길이가 홀수이고, inversion이 짝수일 경우
		//보드의 길이가 짝수이고, inversion이 홀수이고, 빈 칸의 행이 밑에서부터 셀 때 짝수일 경우
		//보드의 길이가 짝수이고, inversion이 짝수이고, 빈 칸의 행이 밑에서부터 셀 때 홀수일 경우
		//inversion은 배열을 일차원으로 볼 때 더 큰 수가 앞에 있는 경우의 개수다.
		//https://natejin.tistory.com/22
		private bool IsValidBoard()
		{
			int inversionCount = 0;

			//더 앞에 있는 수가 큰 경우에 inversion 증가
			for (int i = 0; i < boardLength * boardLength; i++)
			{
				int targetNum = board[i / boardLength, i % boardLength];
				if (targetNum == board[blankRow, blankCol])
					continue;
				for (int j = 0; j < i; j++)
				{
					int comparingNum = board[j / boardLength, j % boardLength];
					if (comparingNum == board[blankRow, blankCol])
						continue;

					if (targetNum < comparingNum)
					{
						++inversionCount;
					}
				}
			}

			Console.Write($"inversionCount: {inversionCount}, ");

			//크기가 홀수일 때
			if ((boardLength & 1) == 1)
			{
				Console.WriteLine("보드의 길이는 홀수");
				//inversion이 짝수일 때
				if ((inversionCount & 1) != 1)
				{
					return true;
				}
			}
			//크기가 짝수일 때
			else
			{
				Console.WriteLine("보드의 길이는 짝수");
				//inversion이 홀수이고, 빈 칸의 행이 밑에서부터 셀 때 짝수일 때
				if ((inversionCount & 1) == 1 && ((boardLength - blankRow) & 1) != 1)
				{
					return true;
				}
				//inversion이 짝수이고, 빈 칸의 행이 밑에서부터 셀 때 홀수일 때
				else if ((inversionCount & 1) != 1 && ((boardLength - blankRow) & 1) == 1)
				{
					return true;
				}
			}

			return false;
		}

		private void SetBlankPos()
		{
			for (int i = 0; i < boardLength; i++)
			{
				if (blankRow != -1 && blankCol != -1)
					break;
				for (int j = 0; j < boardLength; j++)
				{
					if (blankRow != -1 && blankCol != -1)
						break;
					if (board[i, j] == 0)
					{
						blankRow = i;
						blankCol = j;
					}
				}
			}
		}

		//보드 출력하기
		private void PrintBoard()
		{
			for (int i = 0; i < boardLength; i++)
			{
				for (int j = 0; j < boardLength; j++)
				{
					if (board[i, j] == 0)
						Console.Write("X".PadLeft(4, ' '));
					else
						Console.Write($"{board[i, j]}".PadLeft(4, ' '));
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}

		//게임이 종료되는지 확인하기
		private bool IsGameOver()
		{
			for (int i = 0; i < boardLength; i++)
			{
				for (int j = 0; j < boardLength; j++)
				{
					if (board[i, j] != (i * boardLength + j) + 1 && board[i, j] != board[blankRow,blankCol])
						return false;
				}
			}

			return true;
		}

		//게임 종료하기
		private void GameOver()
		{
			Console.WriteLine($"이동한 횟수: {moveCount}\n[게임종료]");
		}
	}
}
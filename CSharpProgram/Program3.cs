using System;

namespace CSharpProgram
{
	class Program3
	{
		//[LAB 1]
		//자음, 모음 개수 출력하기
		public static void CountConsonantAndVowel()
		{
			char inputChar;//입력 받는 문자
			int consonantCount = 0;//자음 총합
			int vowelCount = 0;//모음 총합

			while (true)
			{
				Console.WriteLine("알파벳을 입력하세요(# 입력 시 종료).");
				char.TryParse(Console.ReadLine(), out inputChar);//문자를 입력받는다

				bool isAlphabet = ((inputChar >= 'A' && inputChar <= 'Z') || (inputChar >= 'a' && inputChar <= 'z')) ? true : false;//입력 문자가 알파벳인지 판정한다
				if (isAlphabet)//알파벳이라면
				{
					//입력 문자의 종류에 따라 실행
					switch (inputChar)
					{
						case 'a':
						case 'e':
						case 'i':
						case 'o':
						case 'u':
						case 'A':
						case 'E':
						case 'I':
						case 'O':
						case 'U':
							++vowelCount;//모음 개수 증가
							break;
						default:
							++consonantCount;//자음 개수 증가
							break;
					}
					continue;
				}

				if (inputChar == '#')//종료문자라면
				{
					Console.WriteLine($"모음: {vowelCount}\n자음: {consonantCount}");//모음 자음의 개수를 출력한다
					break;//루프에서 빠져나온다
				}
			}
		}

		//[LAB 2]
		//랜덤한 숫자 맞추기
		public static void FindNumberVer1()
		{
			Random random = new Random();
			int answer = random.Next(1, 100 + 1);//1~100에서 무작위 자연수를 정답으로 한다
			int input;//입력 받는 숫자

			Console.WriteLine("숫자를 맞춰보세요(1 ~ 100의 자연수)");

			//정답을 맞출 때까지 반복
			while (true)
			{
				int.TryParse(Console.ReadLine(), out input);//숫자를 입력받는다

				if (input <= 0 || input > 100)//1~100의 자연수가 아닐 경우
				{
					Console.WriteLine("범위 밖의 숫자입니다.");
				}
				else if (input < answer)//입력 숫자가 정답보다 작을 경우
				{
					Console.WriteLine("입력한 숫자보다 큽니다.");
				}
				else if (input > answer)//입력 숫자가 정답보다 클 경우
				{
					Console.WriteLine("입력한 숫자보다 작습니다.");
				}
				else//나머지 경우(정답일 경우)
				{
					Console.WriteLine("정답입니다.");
					break;//루프에서 빠져나온다
				}
			}
		}

		//[LAB 2]
		//컴퓨터가 랜덤한 숫자 맞추기
		public static void FindNumberVer2()
		{
			Random random = new Random();
			bool isEnd = false;//루프 종료 판정 변수
			int answer;//정답으로 설정할 숫자
			string? inputString;//입력 받는 문자열
			int min = 1;//컴퓨터가 탐색하는 최솟값
			int max = 100;//컴퓨터가 탐색하는 최댓값

			Console.WriteLine("숫자를 입력하세요(1 ~ 100의 자연수)");
			//1~100사이의 자연수를 입력할 때까지 반복
			while (true)
			{
				int.TryParse(Console.ReadLine(), out answer);//숫자를 입력받는다

				if (answer <= 0 || answer > 100)
				{
					Console.WriteLine("잘못된 숫자입니다. 다시 입력하세요.");
					continue;//잘못된 숫자면 입력 반복
				}
				break;//입력 반복 종료
			}

			//정답을 맞추거나 오류가 날 때까지 반복
			while (!isEnd)
			{
				int randomNumber = random.Next(min, max + 1);//컴퓨터의 추측값
				Console.WriteLine($"정답이 {randomNumber}입니까?");
				Console.WriteLine($"더 높은 값이면 UP, 낮은 값이면 DOWN, 정답이면 O를 입력.");

				//일치하는 문자열을 받을 때까지 반복
				while (true)
				{
					inputString = Console.ReadLine();//문자열을 입력받는다
													 //입력 문자열에 따라 실행
					switch (inputString)
					{
						case "UP":
							min = randomNumber + 1;//
							break;
						case "DOWN":
							max = randomNumber - 1;
							break;
						case "O":
							Console.WriteLine("프로그램 종료");
							isEnd = true;//루프 종료
							break;
						default:
							Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
							continue;//잘못된 문자열이면 입력 반복
					}
					break;//입력 반복 종료
				}

				//최솟값이 최댓값보다 클 때 프로그램 종료
				if (min > max)
				{
					Console.WriteLine("모순 발생. 프로그램 종료");
					isEnd = true;//루프 종료
				}
			}
		}

		//[LAB 2]
		//컴퓨터가 랜덤한 숫자 맞추기(이진탐색)
		public static void FindNumberVer3()
		{
			Random random = new Random();
			bool isEnd = false;//루프 종료 판정 변수
			int answer;////정답으로 설정할 숫자
			string? inputString;//입력 받는 문자열
			int min = 1;//컴퓨터가 판정하는 최솟값
			int max = 100;//컴퓨터가 판정하는 최댓값

			Console.WriteLine("숫자를 입력하세요(1 ~ 100의 자연수)");
			//1~100사이의 자연수를 입력할 때까지 반복
			while (true)
			{
				int.TryParse(Console.ReadLine(), out answer);//숫자를 입력받는다

				if (answer <= 0 || answer > 100)
				{
					Console.WriteLine("잘못된 숫자입니다. 다시 입력하세요.");
					continue;//잘못된 숫자면 입력 반복
				}
				break;//입력 반복 종료
			}

			//정답을 맞추거나 오류가 날 때까지 반복
			while (!isEnd)
			{
				int guessNumber = (max + min) / 2;//컴퓨터의 추측값
				Console.WriteLine($"정답이 {guessNumber}입니까?");
				Console.WriteLine($"더 높은 값이면 UP, 낮은 값이면 DOWN, 정답이면 O를 입력.");

				//일치하는 문자열을 받을 때까지 반복
				while (true)
				{
					inputString = Console.ReadLine();
					switch (inputString)
					{
						case "UP":
							min = guessNumber + 1;
							break;
						case "DOWN":
							max = guessNumber - 1;
							break;
						case "O":
							Console.WriteLine("프로그램 종료");
							isEnd = true;
							break;
						default:
							Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
							continue;//잘못된 문자열이면 입력 반복
					}
					break;//입력 반복 종료
				}

				//최솟값이 최댓값보다 클 때 프로그램 종료
				if (min > max)
				{
					Console.WriteLine("모순 발생. 프로그램 종료");
					isEnd = true;//루프 종료
				}
			}
		}

		//[LAB 3]
		//산수 문제 출제하기
		public static void MakeMathQuiz()
		{
			Random random = new Random();
			int operatorNum, operand1, operand2;//연산자 번호, 피연산자(앞), 피연산자(뒤)
			float answer;//정답값
			char[] operators = new char[] { '+', '-', '*', '/' };//연산자 문자 배열

			operatorNum = random.Next(0, 3 + 1);//4가지 연산자 중 하나를 뽑는다
			operand1 = random.Next(0, 99 + 1);//자연수 0~100 중에서 뽑는다
			operand2 = random.Next(0, 99 + 1);//자연수 0~100 중에서 뽑는다

			string Question = $"문제: {operand1} {operators[operatorNum]} {operand2} = ?";//출력할 문제 질문

			//뽑힌 연산자 번호에 따라 실행한다
			switch (operatorNum)
			{
				case 0:
					answer = operand1 + operand2;
					break;
				case 1:
					answer = operand1 - operand2;
					break;
				case 2:
					answer = operand1 * operand2;
					break;
				default:
					if (operand2 == 0)//피연산자(뒤)가 0일 경우
					{
						operand2 = random.Next(1, 99 + 1);//자연수 1~100 중에서 뽑는다
					}
					answer = (float)Math.Round((float)operand1 / operand2, 2);//소수점 셋째 자리에서 반올림
					Question += "(소수점 셋째 자리에서 반올림)";//정답 입력에 관한 문자열 추가
					break;
			}

			Console.WriteLine(Question);

			//정답을 맞출 때까지 반복
			while (true)
			{
				float userInput;//입력 받는 숫자
				float.TryParse(Console.ReadLine(), out userInput);//입력 숫자를 받는다

				if (userInput == answer)//정답일 때
				{
					Console.WriteLine("정답입니다.");
					break;
				}
				else//정답이 아닐 때
				{
					Console.WriteLine("틀렸습니다. 다시 입력해주세요.");
				}
			}
		}
	}
}

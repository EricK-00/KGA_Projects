using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LearnArray
{
	internal class LearnArray
	{
/*		public static void MyArray()
		{
			int[] oneD = new int[5] { 0, -1, -2, -3, -4 };
			int[,] twoD = new int[2, 5] { { 1, 2, 3, 4, 5 }, { 0, 1, 2, 3, 4 } };
			int[,,] threeD = new int[1, 2, 3] { { { 5, 4, 3 }, { 6, 7, 8 } } };

			twoD = new int[3, 3];
			for (int y = 0; y < 3; y++)
			{
				for (int x = 0; x < 3; x++)
				{
					if (x == y)
					{ twoD[y, x] = 1; }
					else
					{ twoD[y, x] = 0; }
				}
			}

			for (int y = 0; y < twoD.GetUpperBound(0) + 1; y++)
			{
				for (int x = 0; x < twoD.GetUpperBound(1) + 1; x++)
				{
					Console.Write($"{twoD[y, x]} ");
				}
				Console.WriteLine();
			}
			Console.WriteLine();

			int[][] jagArray = new int[2][];
			jagArray[0] = new int[1] { 0 };
			jagArray[1] = new int[2] { -1, 1 };

			for (int y = 0; y < jagArray.Length; y++)
			{
				for (int x = 0; x < jagArray[y].Length; x++)
				{
					Console.Write($"{jagArray[y][x]} ");
				}
				Console.WriteLine();
			}
		}

		public static void PrintTotalScoreAndAverage()
		{
			const int MAX_SCORE = 100;
			const int MIN_SCORE = 1;
			int totalScore = 0;
			float average;
			int counter;

			int[] studentScores = new int[3];
			counter = studentScores.Length;

			for (int i = 0; i < studentScores.Length; i++)
			{
				while (true)
				{
					Console.WriteLine($"국어 점수를 입력하세요.(남은 학생: {counter})");
					bool isParsed = int.TryParse(Console.ReadLine(), out studentScores[i]);
					if (!isParsed || studentScores[i] > MAX_SCORE || studentScores[i] < MIN_SCORE)
					{
						Console.WriteLine("잘못된 입력");
						continue;
					}
					break;
				}
				totalScore += studentScores[i];
				--counter;
			}
			average = (float)totalScore / studentScores.Length;
			Console.WriteLine($"총점: {totalScore}\n평균: {average}");
		}*/

		//[LAB 1] 배열에서 최댓값 출력
		public static void FindMaxInArray()
		{
			Random random = new Random();
			int[] intArray = new int[100];//랜덤숫자를 저장할 배열
			int maxNum = int.MinValue;//비교에 쓰일 최댓값

			//1~100 사이의 랜덤숫자 생성
			for (int i = 0; i < intArray.Length; i++)
			{
				intArray[i] = random.Next(1, 100 + 1);
			}
			//최댓값 갱신
			foreach (int num in intArray)
			{
				if (maxNum < num)
				{
					maxNum = num;
				}
			}

			//배열 출력하기
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					if (intArray[i * 10 + j] == maxNum)//최댓값인지 비교
					{
						Console.Write($"<{intArray[i * 10 + j]}> ");//최댓값이면 <>안에 작성
					}
					else
					{
						Console.Write($"{intArray[i * 10 + j]} ");//최댓값이 아니면 
					}
				}
				Console.WriteLine();//10번째마다 한 줄 내리기
			}
		}

		//[LAB 2] 사과를 제일 좋아하는 사람 찾기
		public static void FindAppleLover()
		{
			int[] appleCount = new int[5];//아침에 먹은 사과의 수
			int maxNum = int.MinValue;//비교에 쓰일 최댓값
			int minNum = int.MaxValue;//비교에 쓰일 최솟값

			for (int i = 0; i < appleCount.Length; i++)
			{
				Console.WriteLine($"사람{i + 1}의 아침에 먹은 사과의 개수를 입력하세요");
				//제대로된 입력을 받을 때까지 입력 받기 반복
				while (true)
				{
					if (!int.TryParse(Console.ReadLine(), out appleCount[i]) || appleCount[i] < 0)//잘못된 입력인지 판단
					{
						Console.WriteLine("잘못된 입력");
						continue;//입력 받기 반복
					}
					else break;//입력 받기 중단
				}
				//최댓값 갱신
				if (maxNum < appleCount[i])
				{
					maxNum = appleCount[i];
				}
				//최솟값 갱신
				else if (minNum > appleCount[i])
				{
					minNum = appleCount[i];
				}
			}

			//최댓값 비교 후 가장 많이 먹은 사람 출력
			Console.Write("가장 많이 먹은 사람: ");
			for (int i = 0; i < appleCount.Length; i++)
			{
				if (appleCount[i] == maxNum)//최댓값일 때
				{
					Console.Write($"사람{i + 1} ");
				}
			}
			Console.WriteLine();

			//최솟값 비교 후 가장 적게 먹은 사람 출력
			Console.Write("가장 적게 먹은 사람: ");
			for (int i = 0; i < appleCount.Length; i++)
			{
				if (appleCount[i] == minNum)//최솟값일 때
				{
					Console.Write($"사람{i + 1} ");
				}
			}
			Console.WriteLine();
		}

		//[LAB 2] 정렬
		public static void SortMethod()
		{
			int[] randomArray = new int[100];
			Random random = new Random();

			//배열에 인덱스와 같은 값 저장
			for (int i = 0; i < randomArray.Length; i++)
			{
				randomArray[i] = i + 1;
			}
			//랜덤한 인덱스의 값과 스왑(중복값 없이 랜덤)
			for (int i = 0; i < randomArray.Length; i++)
			{
				//값 스왑
				int temp = randomArray[i];
				int randomNum = random.Next(0, 99 + 1);//랜덤숫자 뽑기
				randomArray[i] = randomArray[randomNum];
				randomArray[randomNum] = temp;
			}

			Console.Write("\n정렬 전: ");
			PrintArray(randomArray);//정렬 전 배열 출력
			Console.Write("\n버블정렬: ");
			PrintArray(BubbleSort(randomArray));//버블정렬된 배열 출력

			Console.Write("\n정렬 전: ");
			PrintArray(randomArray);//정렬 전 배열 출력
			Console.Write("\n머지정렬: ");
			PrintArray(StartMergeSort(randomArray));//머지정렬된 배열 출력
		}

		//인자로 받은 배열 출력하기
		private static void PrintArray(int[] array)
		{
			foreach (int item in array)
				Console.Write($"{item} ");
			Console.WriteLine();
		}

		//버블정렬
		private static int[] BubbleSort(int[] array)
		{
			//출력할 배열 생성(원래 배열 값 보존)
			int[] sortedArray = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
				sortedArray[i] = array[i];

			for (int i = 0; i < sortedArray.Length; i++)
			{
				for (int j = 1; j < sortedArray.Length - i; j++)
				{
					if (sortedArray[j - 1] > sortedArray[j])
					{
						//두 값 스왑
						int temp = sortedArray[j];
						sortedArray[j] = sortedArray[j - 1];
						sortedArray[j - 1] = temp;
					}
				}
			}

			return sortedArray;//정렬된 배열 반환
		}

		//머지정렬된 배열 반환
		private static int[] StartMergeSort(int[] array)
		{
			//출력할 배열 생성(원래 배열 값 보존)
			int[] sortedArray = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
				sortedArray[i] = array[i];

			MergeSort(sortedArray, 0, sortedArray.Length - 1);
			return sortedArray;//정렬된 배열 반환
		}

		//머지정렬
		private static void MergeSort(int[] array, int startIndex, int endIndex)
		{
			if (startIndex == endIndex) return;//길이가 1이면 반환
			int midIndex = (startIndex + endIndex) / 2;//중간 인덱스

			MergeSort(array, startIndex, midIndex);//왼쪽 배열
			MergeSort(array, midIndex + 1, endIndex);//오른쪽 배열

			int[] tempArray = new int[endIndex - startIndex + 1];//임시로 값을 저장할 배열
			int tempIndex = 0;
			int leftIndex = startIndex;//왼쪽 배열의 인덱스
			int rightIndex = midIndex + 1;//오른쪽 배열의 인덱스

			while (leftIndex <= midIndex && rightIndex <= endIndex)//왼쪽 배열이나 오른쪽 배열의 끝에 도달할 때까지 실행
			{
				if (array[leftIndex] <= array[rightIndex])//왼쪽 배열에 있는 원소의 값이 오른쪽 배열에 있는 원소의 값보다 작거나 같을 때
				{
					tempArray[tempIndex] = array[leftIndex];
					++leftIndex;//왼쪽 배열 이동
				}
				else
				{
					tempArray[tempIndex] = array[rightIndex];
					++rightIndex;//오른쪽 배열 이동
				}
				++tempIndex;//임시 배열 이동
			}
			while(leftIndex <= midIndex)//왼쪽 배열에 남은 값들이 있을 때 실행
			{
				tempArray[tempIndex] = array[leftIndex];
				++tempIndex;//임시 배열 이동
				++leftIndex;//왼쪽 배열 이동
			}
			while (rightIndex <= endIndex)//오른쪽 배열에 남은 값들이 있을 때 실행
			{
				tempArray[tempIndex] = array[rightIndex];
				++tempIndex;//임시 배열 이동
				++rightIndex;//오른쪽 배열 이동
			}

			//임시 배열의 값을 원래 배열에 덮어쓰기
			for (int i = 0; i < tempArray.Length; i++)
			{
				array[i + startIndex] = tempArray[i];
			}
		}
	}
}

using System;

namespace CSharpProgram
{
	class TrumpCard
	{
		private int[] cardSet;
		private string[] cardMarks;

		public void SetUpCards()
		{
			cardSet = new int[52];
			for (int i = 0; i < cardSet.Length; i++)
				cardSet[i] = i + 1;

			cardMarks = new string[4] { "♥", "♠", "◆", "♣" };
		}

		private void SuffleOnce(int[] intArray)
		{
			Random random = new Random();
			int sourceIndex = random.Next(0, intArray.Length);
			int destinationIndex = random.Next(0, intArray.Length);

			int temp = intArray[sourceIndex];
			intArray[sourceIndex] = intArray[destinationIndex];
			intArray[destinationIndex] = temp;
		}

		public void SuffleCards()
		{
			SuffleCards(200);
		}

		private void SuffleCards(int num)
		{
			for (int i = 0; i < num; i++)
				SuffleOnce(cardSet);
		}

		public void RerollCard()
		{
			SuffleCards();
			RollCard();
		}

		public void RollCard()
		{
			int card = cardSet[0];
			string cardMark = cardMarks[(card - 1) / 13];
			string cardNumber = ((int)Math.Ceiling(card % 13.1)).ToString();

			switch (cardNumber)
			{
				case "11":
					cardNumber = "J";
					break;
				case "12":
					cardNumber = "Q";
					break;
				case "13":
					cardNumber = "K";
					break;
			}

			Console.WriteLine($"내가 뽑은 카드는 {cardMark}{cardNumber}입니다");
			Console.WriteLine(" ---- ");
			Console.WriteLine($"|{cardMark} {cardNumber}|");
			Console.WriteLine("|    |");
			Console.WriteLine($"|{cardNumber} {cardMark}|");
			Console.WriteLine(" ----");
		}

		private void PrintCardSet()
		{
			foreach (int item in cardSet)
				Console.Write($"{item} ");
			Console.WriteLine();
		}
	}
}
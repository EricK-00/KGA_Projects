using System;
using System.Runtime.InteropServices;

namespace CSharpProgram
{
	class CardGame
	{
		//컴퓨터가 2장 뽑기
		//플레이어 배팅(0포인트 이상)
		//플레이어가 뽑은 카드가 컴퓨터가 뽑은 2장의 카드 사이에 있는 카드면 2배 획득, 같거나 사이에 없으면 금액 잃음
		//초기 포인트 1만
		//카드 대,소 비교는 숫자로만
		//10만 = 승리, 0 = 패배

		const int CARD_COUNT = 52;
		const int SUFFLE_NUMBER = 200;
		int[] deck = new int[CARD_COUNT];
		char[] cardMarks = new char[4] { '♥', '♠', '♣', '◆' };

		int gamePoint;
		int nextCardIndex;
		int gameRound;
		int bettingPoint;

		public void PlayCardGame()
		{
			InitializeGame();

			while(!IsGameOver())
			{
				Console.WriteLine($"현재 라운드: {gameRound}");

				int computerCard1 = GetNextCard();
				Console.WriteLine($"컴퓨터가 뽑은 카드1: {PrintCard(computerCard1)}");

				int computerCard2 = GetNextCard();
				Console.WriteLine($"컴퓨터가 뽑은 카드2: {PrintCard(computerCard2)}");

				BetPoint();
				int playerCard = GetNextCard();
				Console.WriteLine($"당신이 뽑은 카드: {PrintCard(playerCard)}");

				if (WinThisRound(playerCard, computerCard1, computerCard2))
				{
					gamePoint += bettingPoint;
					Console.WriteLine($"이번 라운드를 승리하였습니다! 베팅한 포인트의 2배 얻습니다. [현재 포인트) {gamePoint}]");
				}
				else
				{
					gamePoint -= bettingPoint;
					Console.WriteLine($"이번 라운드를 패배하였습니다. 베팅한 포인트를 모두 잃습니다. [현재 포인트) {gamePoint}]");
				}

				Console.WriteLine("-------------------------");
				++gameRound;
			}
		}

		private void InitializeGame()
		{
			gamePoint = 10000;
			gameRound = 1;
			nextCardIndex = 0;

			for (int i = 0; i < deck.Length; i++)
			{
				deck[i] = i;
			}
			SuffleDeck();

			Console.WriteLine("[카드게임]");
			Console.WriteLine("------------룰-----------");
			Console.WriteLine("컴퓨터가 뽑은 두 카드 사이의 수를 뽑으면 베팅한 포인트의 2배를 획득합니다\n이외의 수를 뽑으면 베팅 포인트를 모두 잃습니다.");
			Console.WriteLine("10만 포인트 이상 획득하거나 포인트를 모두 잃으면 게임이 종료됩니다.");
			Console.WriteLine("-------------------------");
		}

		private void SuffleDeck()
		{
			for (int i = 0; i < SUFFLE_NUMBER; i++)
			{
				int randomIndex1 = new Random().Next(0, deck.Length);
				int randomIndex2 = new Random().Next(0, deck.Length);

				int temp = deck[randomIndex1];
				deck[randomIndex1] = deck[randomIndex2];
				deck[randomIndex2] = temp;
			}
		}

		private void BetPoint()
		{
			while (true)
			{
				Console.Write($"베팅할 포인트를 입력해주세요(0 이상, 보유 포인트 이하). [현재 포인트) {gamePoint}]: ");
				if (!int.TryParse(Console.ReadLine(), out bettingPoint) || bettingPoint < 0 || bettingPoint > gamePoint)
				{
					Console.WriteLine("잘못된 입력입니다.");
					continue;
				}
				break;
			}

			if (bettingPoint == gamePoint)
				Console.WriteLine("올인!");
		}

		private int GetNextCard()
		{
			int nextCard = deck[nextCardIndex];
			++nextCardIndex;

			if (nextCardIndex >= deck.Length)
				ResetDeck();

			return nextCard;
		}

		private int TranslateCardNumber(int card)
		{
			int cardNumber = card % 13 + 1;
			return cardNumber;
		}

		private char TranslateCardMark(int card)
		{
			char cardMark = cardMarks[card / 13];
			return cardMark;
		}

		private bool WinThisRound(int playerCard, int computerCard1, int computerCard2)
		{
			playerCard = TranslateCardNumber(playerCard);
			computerCard1 = TranslateCardNumber(computerCard1);
			computerCard2 = TranslateCardNumber(computerCard2);

			int maxCard = Math.Max(computerCard1, computerCard2);
			int minCard = Math.Min(computerCard1, computerCard2);

			if (playerCard < maxCard && playerCard > minCard)
				return true;
			else
				return false;
		}

		private string PrintCard(int card)
		{
			int cardNumber = TranslateCardNumber(card);
			string cardNumberString = cardNumber.ToString();
			switch (cardNumber)
			{
				case 11:
					cardNumberString = "K";
					break;
					case 12:
					cardNumberString = "Q";
					break;
					case 13:
					cardNumberString = "J";
					break;
			}

			return $"{TranslateCardMark(card)}, {cardNumberString}";
		}

		private void ResetDeck()
		{
			Console.WriteLine("[덱을 교체합니다]");
			nextCardIndex = 0;
			SuffleDeck();
		}

		private bool IsGameOver()
		{
			if (gamePoint >= 100000)
			{
				Console.WriteLine("10만 포인트 이상 획득해서 게임을 숭리하였습니다!");
				return true;
			}
			else if (gamePoint <= 0)
			{
				Console.WriteLine("포인트를 모두 잃어서 게임을 패배하였습니다.");
				return true;
			}

			return false;
		}
	}
}

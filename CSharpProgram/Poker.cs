using System;
using System.Collections.Generic;

namespace CSharpProgram
{
	struct Card
	{
		public int markIndex;
		public int number;

		public Card (int markIndex_, int number_)
		{
			markIndex = markIndex_;
			number = number_;
		}

		public int GetCardPower()
		{
			int cardNumberPower = number - 1;
			if (number == 1) cardNumberPower = 13;
			int cardPower = 4 * cardNumberPower - markIndex;

			return cardPower;
		}
	}

	enum CardCombination
	{
		노페어 = 0,
		원페어,
		투페어,
		트리플,
		스트레이트,
		백스트레이트,
		마운틴,
		플러시,
		풀하우스,
		포카드,
		스트레이트플러시,
		백스트레이트플러시,
		로얄스트레이트플러시
	}

	internal class Poker
	{
		bool isWinGame;
		int gamePoint;
		int currentCardIndex;
		int bettingPoint;
		Card[] deck;
		Card[] computerCards;
		Card[] playerCards;
		List<Card> combinationCards;

		string playerName = "플레이어";
		const string COMPUTER_NAME = "컴퓨터";

		const int FIRST_COMPUTER_CARDS_END_INDEX = 4;
		int computerCardsEndIndex;
		int playerCardsEndIndex;

		int turnCount;

		Random random = new Random();

		public void PlayPoker()
		{
			Console.WriteLine("<포인트를 10만 이상 획득하면 승리, 포인트를 모두 잃으면 패배>");
			Console.Write("플레이어의 이름을 입력하세요: ");
			playerName = Console.ReadLine();
			while (!GetIsGameOver())
			{
				++turnCount;
				Console.WriteLine("--------------------------------------------------------------------------------------");
				Console.Write("라운드를 시작하려면 아무 키나 입력하세요...");
				Console.ReadLine();
				Console.WriteLine($"<라운드 {turnCount}>\n[보유 포인트: {gamePoint}]");
				Console.WriteLine();

				currentCardIndex = 0;
				ShuffleDeck(deck);

				SetUpCards(playerName, playerCards, 0, playerCardsEndIndex);
				Console.WriteLine($"{playerName}(이)가 뽑은 카드: {ShowCard(playerCards, playerCardsEndIndex)} ");
				Console.Write($"[{playerName}의 조합: {GetCombination(playerCards, playerCardsEndIndex).Item1}] ");
				Console.WriteLine($"|{ShowCard(combinationCards)}|");
				Console.WriteLine();

				SetUpCards(COMPUTER_NAME, computerCards, 0, FIRST_COMPUTER_CARDS_END_INDEX);
				Console.WriteLine($"{COMPUTER_NAME}(이)가 뽑은 카드: {ShowCard(computerCards, FIRST_COMPUTER_CARDS_END_INDEX)} ");
				Console.Write($"[{COMPUTER_NAME}의 조합: {GetCombination(computerCards, FIRST_COMPUTER_CARDS_END_INDEX).Item1}] ");
				Console.WriteLine($"|{ShowCard(combinationCards)}|");
				Console.WriteLine();

				Bet();
				Console.WriteLine();

				ReplaceCards();
				Console.WriteLine();

				Console.WriteLine($"교체 이후 {playerName}의 카드: {ShowCard(playerCards, playerCardsEndIndex)} ");
				//Console.Write($"[조합: {GetCombination(playerCards, playerCardsEndIndex).Item1} {GetCombination(playerCards, playerCardsEndIndex).Item2}]");
				Console.Write($"[{playerName}의 조합: {GetCombination(playerCards, playerCardsEndIndex).Item1}] ");
				Console.WriteLine($"|{ShowCard(combinationCards)}|");
				Console.WriteLine();

				SetUpCards(COMPUTER_NAME, computerCards, FIRST_COMPUTER_CARDS_END_INDEX + 1, computerCardsEndIndex);
				Console.WriteLine($"2장을 추가로 뽑은 후 {COMPUTER_NAME}의 카드: {ShowCard(computerCards, computerCardsEndIndex)} ");
				//Console.Write($"[조합: {GetCombination(computerCards, computerCardsEndIndex).Item1} {GetCombination(computerCards, computerCardsEndIndex).Item2}]");
				Console.Write($"[{COMPUTER_NAME}의 조합: {GetCombination(computerCards, computerCardsEndIndex).Item1}] ");
				Console.WriteLine($"|{ShowCard(combinationCards)}|");
				Console.WriteLine();

				CompareCombination(GetCombination(computerCards, computerCardsEndIndex), GetCombination(playerCards, playerCardsEndIndex));
				Console.WriteLine("--------------------------------------------------------------------------------------");
			}

			if (isWinGame)
				Console.WriteLine("10만 포인트 이상을 얻어 게임에 승리하였습니다.");
			else
				Console.WriteLine("모든 포인트를 잃어 게임에 패배하였습니다.");
		}

		public Poker()
		{
			isWinGame = false;
			gamePoint = 10000;
			currentCardIndex = -1;

			deck = new Card[52];
			for (int i = 0; i < deck.Length; i++)
			{
				deck[i].markIndex = i / 13;
				deck[i].number = i % 13 + 1;
			}

			computerCards = new Card[7];
			playerCards = new Card[5];
			combinationCards = new List<Card>();

			computerCardsEndIndex = computerCards.Length - 1;
			playerCardsEndIndex = playerCards.Length - 1;

			turnCount = 0;
		}

		private void ShuffleDeck(Card[] deck)
		{
			for (int i = 0; i < deck.Length - 1; i++)
			{
				int randomIndex = random.Next(i, deck.Length);

				Card temp = deck[i];
				deck[i] = deck[randomIndex];
				deck[randomIndex] = temp;
			}
		}

		private Card DrawCard()
		{
			++currentCardIndex;
			return deck[currentCardIndex];
		}
		private string ShowCard(Card card)
		{
			char[] marks = new char[4] { '♠', '◆', '♥', '♣' };
			string resultString = string.Empty;
			string cardNumberString;

			switch (card.number)
			{
				case 1:
					cardNumberString = "A";
					break;
				case 11:
					cardNumberString = "K";
					break;
				case 12:
					cardNumberString = "Q";
					break;
				case 13:
					cardNumberString = "J";
					break;
				default:
					cardNumberString = card.number.ToString();
					break;
			}

			resultString = $"{marks[card.markIndex]} {cardNumberString}";

			return resultString;
		}

		private string ShowCard(Card[] cards, int endIndex)
		{
			char[] marks = new char[4] { '♠', '◆', '♥', '♣' };
			string resultString = string.Empty;
			string cardNumberString;

			for (int i = 0; i <= endIndex; i++)
			{
				switch (cards[i].number)
				{
					case 1:
						cardNumberString = "A";
						break;
					case 11:
						cardNumberString = "K";
						break;
					case 12:
						cardNumberString = "Q";
						break;
					case 13:
						cardNumberString = "J";
						break;
					default:
						cardNumberString = cards[i].number.ToString();
						break;
				}

				if (i < endIndex)
					resultString += $"{marks[cards[i].markIndex]} {cardNumberString}, ";
				else
					resultString += $"{marks[cards[i].markIndex]} {cardNumberString}";
			}

			return resultString;
		}

		private string ShowCard(List<Card> cards)
		{
			char[] marks = new char[4] { '♠', '◆', '♥', '♣' };
			string resultString = string.Empty;
			string cardNumberString;

			for (int i = 0; i < cards.Count; i++)
			{
				switch (cards[i].number)
				{
					case 1:
						cardNumberString = "A";
						break;
					case 11:
						cardNumberString = "K";
						break;
					case 12:
						cardNumberString = "Q";
						break;
					case 13:
						cardNumberString = "J";
						break;
					default:
						cardNumberString = cards[i].number.ToString();
						break;
				}

				if (i < cards.Count - 1)
					resultString += $"{marks[cards[i].markIndex]} {cardNumberString}, ";
				else
					resultString += $"{marks[cards[i].markIndex]} {cardNumberString}";
			}

			return resultString;
		}

		private void SetUpCards(string playerName, Card[] cards, int startIndex, int endIndex)
		{
			for (int i = startIndex; i <= endIndex; i++)
			{
				cards[i] = DrawCard();
			}
		}

		private void ReplaceCards()
		{
			int replacingCount;
			int[] cardNumber = {0, 0};

		Console.Write("몇 장의 카드를 교체하겠습니까?(0 ~ 2장까지): ");
			while (true)
			{
				if (int.TryParse(Console.ReadLine(), out replacingCount) &&
					replacingCount >= 0 &&
					replacingCount <= 2)
				{
					break;
				}
				else
				{
					Console.WriteLine("잘못된 입력입니다.");
					continue;
				}
			}

			for (int i = 0; i < replacingCount; i++)
			{
				Console.Write("교체할 카드 위치를 입력하세요(1 ~ 5번): ");
				while (true)
				{
					if (int.TryParse(Console.ReadLine(), out cardNumber[i]) &&
						cardNumber[i] >= 1 &&
						cardNumber[i] <= 5 &&
						cardNumber[0] != cardNumber[1])
					{
						Console.WriteLine($"{ShowCard(playerCards[cardNumber[i] - 1])} 선택");
						break;
					}
					else
					{
						Console.WriteLine("잘못된 입력입니다.");
						continue;
					}
				}
			}

			for (int i = 0; i < replacingCount; i++)
			{
				Card temp = DrawCard();
				Console.WriteLine($"{ShowCard(playerCards[cardNumber[i] - 1])} -> {ShowCard(temp)}(으)로 교체");
				playerCards[cardNumber[i] - 1] = temp;
			}
		}

		private void Bet()
		{
			Console.Write("베팅할 포인트를 입력하세요: ");

			while (true)
			{
				if (int.TryParse(Console.ReadLine(), out bettingPoint) && bettingPoint >= 0 && bettingPoint <= gamePoint)
					break;
				else
				{
					Console.WriteLine("잘못된 입력입니다.");
					continue;
				}
			}
			Console.WriteLine($"{bettingPoint} 포인트 베팅");

			gamePoint -= bettingPoint;
		}

		private (CardCombination, int) GetCombination(Card[] cards, int endIndex)
		{
			CardPowerComparer cardPowerComparer = new CardPowerComparer();

			List<Card> noPairList = new List<Card>();
			List<Card> pairList = new List<Card>();
			List<Card> tripleList = new List<Card>();
			List<Card> fourCardList = new List<Card>();
			List<Card> flushList = new List<Card>();
			List<Card> straightList = new List<Card>();
			int maxCardPower = 0;//조합이 동일할 경우 비교
			CardCombination combination = CardCombination.노페어;
			Card[,] cardTable = new Card[4, 13];
			for (int i = 0; i <= cardTable.GetUpperBound(0); i++)
				for (int j = 0; j <= cardTable.GetUpperBound(1); j++)
				{
					cardTable[i, j] = new Card(-1, -1);
				}
			int[] cardNumberCount = new int[13];
			int[] cardMarkCount = new int[4];
			for (int i = 0; i <= endIndex; i++)
			{
				cardTable[cards[i].markIndex, cards[i].number - 1] = cards[i];
				++cardNumberCount[cards[i].number - 1];
				++cardMarkCount[cards[i].markIndex];
			}

			//같은 수의 개수에 따라 해당하는 리스트에 저장
			for (int i = 0; i <= endIndex; i++)
			{
				if (cardNumberCount[cards[i].number - 1] == 2)
				{
					pairList.Add(cards[i]);
				}
				else if (cardNumberCount[cards[i].number - 1] == 3)
				{
					tripleList.Add(cards[i]);
				}
				else if (cardNumberCount[cards[i].number - 1] >= 4)
				{
					fourCardList.Add(cards[i]);
				}
				else
				{
					noPairList.Add(cards[i]);
				}
			}

			//원페어, 투페어, 트리플, 풀하우스, 포카드
			if (fourCardList.Count >= 4 && (int)combination < (int)CardCombination.포카드)
			{
				combination = CardCombination.포카드;
			}
			if (tripleList.Count == 3 && pairList.Count >= 2 && (int)combination < (int)CardCombination.풀하우스)
			{
				combination = CardCombination.풀하우스;
			}
			if (tripleList.Count == 6 && (int)combination < (int)CardCombination.풀하우스)
			{
				combination = CardCombination.풀하우스;
			}
			if (tripleList.Count >= 3 && (int)combination < (int)CardCombination.트리플)
			{
				combination = CardCombination.트리플;
			}
			if (pairList.Count >= 4 && (int)combination < (int)CardCombination.투페어)
			{
				combination = CardCombination.투페어;
			}
			if (pairList.Count == 2 && (int)combination < (int)CardCombination.원페어)
			{
				combination = CardCombination.원페어;
			}

			//플러시
			for (int i = 0; i < cardMarkCount.Length; i++)
			{
				if (cardMarkCount[i] >= 5 && (int)combination < (int)CardCombination.플러시)
				{
					combination = CardCombination.플러시;

					flushList.Clear();
					for (int n = 13; n > 0; n--)
					{
						if (flushList.Count < 5)
						{
							flushList.Add(cardTable[i, n % 13]);
						}
					}

					break;
				}
			}

			//스트레이트 종류
			for (int i = 13; i > 3; i--)
			{
				if (cardNumberCount[i % 13] >= 1 &&
					cardNumberCount[(i - 1) % 13] >= 1 &&
					cardNumberCount[(i - 2) % 13] >= 1 &&
					cardNumberCount[(i - 3) % 13] >= 1 &&
					cardNumberCount[(i - 4) % 13] >= 1)
				{
					if (i % 13 == 1 - 1 && (i - 4) % 13 == 10 - 1)
					{
						//로얄 플러시 스트레이트
						for (int j = 0; j < cardMarkCount.Length; j++)
						{
							if (cardTable[j, i % 13].number != -1 &&
								cardTable[j, (i - 1) % 13].number != -1 &&
								cardTable[j, (i - 2) % 13].number != -1 &&
								cardTable[j, (i - 3) % 13].number != -1 &&
								cardTable[j, (i - 4) % 13].number != -1)
							{
								combination = CardCombination.로얄스트레이트플러시;

								straightList.Clear();
								straightList.Add(cardTable[j, i % 13]);
								straightList.Add(cardTable[j, (i - 1) % 13]);
								straightList.Add(cardTable[j, (i - 2) % 13]);
								straightList.Add(cardTable[j, (i - 3) % 13]);
								straightList.Add(cardTable[j, (i - 4) % 13]);

								break;
							}
						}

						//마운틴
						if ((int)combination < (int)CardCombination.마운틴)
						{
							combination = CardCombination.마운틴;

							straightList.Clear();
							for (int j = 0; j < 5; j++)
							{
								if (cardTable[0, (i - j) % 13].number != -1)
								{
									straightList.Add(cardTable[0, (i - j) % 13]);
								}
								else if (cardTable[1, (i - j) % 13].number != -1)
								{
									straightList.Add(cardTable[1, (i - j) % 13]);
								}
								else if (cardTable[2, (i - j) % 13].number != -1)
								{
									straightList.Add(cardTable[2, (i - j) % 13]);
								}
								else
								{
									straightList.Add(cardTable[3, (i - j) % 13]);
								}
							}
						}
					}

					if (i % 13 == 5 - 1 && (i - 4) % 13 == 1 - 1)
					{
						//백 스트레이트 플러시
						for (int j = 0; j < cardMarkCount.Length; j++)
						{
							if (cardTable[j, i % 13].number != -1 &&
								cardTable[j, (i - 1) % 13].number != -1 &&
								cardTable[j, (i - 2) % 13].number != -1 &&
								cardTable[j, (i - 3) % 13].number != -1 &&
								cardTable[j, (i - 4) % 13].number != -1 &&
								(int)combination < (int)CardCombination.백스트레이트플러시)
							{
								combination = CardCombination.백스트레이트플러시;

								straightList.Clear();
								straightList.Add(cardTable[j, (i - 4) % 13]);
								straightList.Add(cardTable[j, (i - 3) % 13]);
								straightList.Add(cardTable[j, (i - 2) % 13]);
								straightList.Add(cardTable[j, (i - 1) % 13]);
								straightList.Add(cardTable[j, i % 13]);

								break;
							}
						}

						//백 스트레이트
						if ((int)combination < (int)CardCombination.백스트레이트)
						{
							combination = CardCombination.백스트레이트;

							straightList.Clear();
							for (int j = 4; j >= 0; j--)
							{
								if (cardTable[0, (i - j) % 13].number != -1)
								{
									straightList.Add(cardTable[0, (i - j) % 13]);
								}
								else if (cardTable[1, (i - j) % 13].number != -1)
								{
									straightList.Add(cardTable[1, (i - j) % 13]);
								}
								else if (cardTable[2, (i - j) % 13].number != -1)
								{
									straightList.Add(cardTable[2, (i - j) % 13]);
								}
								else
								{
									straightList.Add(cardTable[3, (i - j) % 13]);
								}
							}
						}
					}

					//스트레이트 플러시
					for (int j = 0; j < cardMarkCount.Length; j++)
					{
						if (cardTable[j, i % 13].number != -1 &&
							cardTable[j, (i - 1) % 13].number != -1 &&
							cardTable[j, (i - 2) % 13].number != -1 &&
							cardTable[j, (i - 3) % 13].number != -1 &&
							cardTable[j, (i - 4) % 13].number != -1 &&
						(int)combination < (int)CardCombination.스트레이트플러시)
						{
							combination = CardCombination.스트레이트플러시;

							straightList.Clear();
							straightList.Add(cardTable[j, i]);
							straightList.Add(cardTable[j, (i - 1) % 13]);
							straightList.Add(cardTable[j, (i - 2) % 13]);
							straightList.Add(cardTable[j, (i - 3) % 13]);
							straightList.Add(cardTable[j, (i - 4) % 13]);

							break;
						}
					}

					//스트레이트
					if ((int)combination < (int)CardCombination.스트레이트)
					{
						combination = CardCombination.스트레이트;

						straightList.Clear();
						for (int j = 0; j < 5; j++)
						{
							if (cardTable[0, (i - j) % 13].number != -1)
							{
								straightList.Add(cardTable[0, (i - j) % 13]);
							}
							else if (cardTable[1, (i - j) % 13].number != -1)
							{
								straightList.Add(cardTable[1, (i - j) % 13]);
							}
							else if (cardTable[2, (i - j) % 13].number != -1)
							{
								straightList.Add(cardTable[2, (i - j) % 13]);
							}
							else
							{
								straightList.Add(cardTable[3, (i - j) % 13]);
							}
						}
					}
				}
			}

			if (noPairList.Count > 0)
				noPairList.Sort(cardPowerComparer);
			if (pairList.Count > 0)
				pairList.Sort(cardPowerComparer);
			if (tripleList.Count > 0)
				tripleList.Sort(cardPowerComparer);
			if (fourCardList.Count > 0)
				fourCardList.Sort(cardPowerComparer);
			if (flushList.Count > 0)
				flushList.Sort(cardPowerComparer);
			if (straightList.Count > 0 && (combination != CardCombination.백스트레이트 || combination != CardCombination.백스트레이트플러시))
				straightList.Sort(cardPowerComparer);

			combinationCards.Clear();
			switch (combination)
			{
				case CardCombination.노페어:
					maxCardPower = noPairList[0].GetCardPower();
					combinationCards.Add(noPairList[0]);
					break;
				case CardCombination.원페어:
					maxCardPower = pairList[0].GetCardPower();
					combinationCards = pairList;
					break;
				case CardCombination.투페어:
					maxCardPower = pairList[0].GetCardPower();
					for (int i = 0; i < 4; i++)
					combinationCards.Add(pairList[i]);
					break;
				case CardCombination.트리플:
					maxCardPower = tripleList[0].GetCardPower();
					for (int i = 0; i < 3; i++)
						combinationCards.Add(tripleList[i]);
					break;
				case CardCombination.풀하우스:
					maxCardPower = tripleList[0].GetCardPower();
					for (int i = 0; i < 3; i++)
						combinationCards.Add(tripleList[i]);
					if (tripleList[3].GetCardPower() > pairList[0].GetCardPower())
					{
						combinationCards.Add(tripleList[3]);
						combinationCards.Add(tripleList[4]);
					}
					else
					{
						combinationCards.Add(pairList[0]);
						combinationCards.Add(pairList[1]);
					}
					break;
				case CardCombination.포카드:
					maxCardPower = fourCardList[0].GetCardPower();
					combinationCards = fourCardList;
					break;
				case CardCombination.플러시:
					maxCardPower = flushList[0].GetCardPower();
					combinationCards = flushList;
					break;
				case CardCombination.스트레이트:
				case CardCombination.스트레이트플러시:
				case CardCombination.백스트레이트:
				case CardCombination.백스트레이트플러시:
				case CardCombination.마운틴:
				case CardCombination.로얄스트레이트플러시:
					maxCardPower = straightList[0].GetCardPower();
					combinationCards = straightList;
					break;
			}

			return (combination, maxCardPower);
		}

		//카드 숫자, 문양에 따라 내림차순 정렬
		private class CardPowerComparer : IComparer<Card>
		{
			public int Compare(Card cardA, Card cardB)
			{
				int cardAPower = cardA.GetCardPower();
				int cardBPower = cardB.GetCardPower();

				if (cardAPower == cardBPower)
					return 0;
				if (cardAPower < cardBPower)
					return 1;
				else
					return -1;
			}
		}

		private void CompareCombination((CardCombination, int) computerCombination, (CardCombination, int) playerCombination)
		{
			if (playerCombination.Item1 > computerCombination.Item1 ||
				(playerCombination.Item1 == computerCombination.Item1 && playerCombination.Item2 > computerCombination.Item2))
			{
				Console.Write("이번 라운드에서 승리하였습니다. 베팅한 포인트의 2배를 얻습니다. ");
				gamePoint += 2 * bettingPoint;
			}
			else if (playerCombination.Item1 < computerCombination.Item1 ||
				(playerCombination.Item1 == computerCombination.Item1 && playerCombination.Item2 < computerCombination.Item2))
			{
				Console.Write("이번 라운드에서 패배하였습니다. 베팅한 포인트를 모두 잃습니다. ");
			}
			else
			{
				Console.Write("이번 라운드는 무승부입니다. 베팅한 포인트의 절반만 얻습니다. ");
				gamePoint += bettingPoint / 2;
			}

			Console.WriteLine($"[보유 포인트: {gamePoint}]");
		}

		private bool GetIsGameOver()
		{
			if (gamePoint <= 0)
			{
				isWinGame = false;
				return true;
			}
			else if (gamePoint >= 100000)
			{
				isWinGame = true;
				return true;
			}

			return false;
		}
	}
}
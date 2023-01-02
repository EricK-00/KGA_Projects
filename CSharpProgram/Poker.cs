using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgram
{
	struct Card
	{
		public char mark;
		public int number;
	}

	enum CardCombinationPower
	{
		노페어 = 0,
		원페어,
		투페어,
		트리플,
		포카드,
		플러시
	}

	internal class Poker
	{
		bool isWinGame;
		bool isWinThisRound;
		int gamePoint;
		char[] marks;
		int currentCardIndex;
		int bettingPoint;
		Card[] deck;
		Card[] computerCards;
		Card[] playerCards;
		//Dictionary<string, int> cardCombinationTable;

		string playerName = "플레이어";
		const string COMPUTER_NAME = "컴퓨터";
		const string DEFAULT_COMBINATION = "노페어";

		Random random = new Random();

		public void PlayPoker()
		{
			Console.WriteLine("룰 - 포인트를 10만 이상 획득하면 승리, 포인트를 모두 잃으면 패배");
			while (!GetIsGameOver())
			{
				SetUpCards(COMPUTER_NAME, computerCards, 0, 4);
				SetUpCards(playerName, playerCards, 0, playerCards.Length - 1);
				Bet();
				SetUpCards(COMPUTER_NAME, computerCards, 5, computerCards.Length - 1);
				ReplaceCards();

				//GetCardCombination();
			}

			if (isWinGame)
			{
				Console.WriteLine("승리");
			}
			else
			{
				Console.WriteLine("패배");
			}
		}

		public Poker()
		{
			isWinGame = false;
			isWinThisRound = false;
			gamePoint = 10000;
			marks = new char[4] { '♠', '◆', '♥', '♣' };
			currentCardIndex = -1;

			deck = new Card[52];
			for (int i = 0; i < deck.Length; i++)
			{
				deck[i].mark = marks[i / 13];
				deck[i].number = i % 13 + 1;
			}
			ShuffleDeck(deck);

			computerCards = new Card[7];
			playerCards = new Card[5];

/*			cardCombinationTable = new Dictionary<string, int>
			{
				{ DEFAULT_COMBINATION, 0 },
				{ "원페어", 1 },
				{ "투페어", 2 },
				{ "트리플", 3 },
				{ "", 4 }
			};*/
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
			if (currentCardIndex == 51)
			{
				Console.WriteLine("새로운 덱 개봉");
				ShuffleDeck(deck);
				currentCardIndex = 0;
			}

			return deck[currentCardIndex];
		}

		private string ShowCard(Card card)
		{
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
			return $"{card.mark} {cardNumberString}";
		}

		private void SetUpCards(string playerName, Card[] cards, int startIndex, int endIndex)
		{
			for (int i = startIndex; i <= endIndex; i++)
			{
				cards[i] = DrawCard();
				Console.WriteLine($"{playerName}(이)가 뽑은 카드: {ShowCard(cards[i])}");
			}
		}

		private void ReplaceCards()
		{
			int replacingCount;
			int[] cardNumber = {0, 0};

		Console.WriteLine("교체할 카드의 수 입력(0 ~ 2장까지)");
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
					Console.WriteLine("잘못된 입력");
					continue;
				}
			}

			Console.WriteLine("교체할 카드 위치 입력(1 ~ 5번)");
			for (int i = 0; i < replacingCount; i++)
			{
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
						Console.WriteLine("잘못된 입력");
						continue;
					}
				}
			}

			Console.WriteLine("교체할 카드 위치 입력(1 ~ 5번)");
			for (int i = 0; i < replacingCount; i++)
			{
				Card temp = DrawCard();
				Console.WriteLine($"{ShowCard(playerCards[cardNumber[i] - 1])} -> {ShowCard(temp)}(으)로 교체");
				playerCards[cardNumber[i] - 1] = temp;
			}

			Console.Write("플레이어의 카드: ");
			foreach (var card in playerCards)
			{
				Console.Write($"{ShowCard(card)}, ");
			}
			Console.WriteLine();
		}

		private void Bet()
		{
			Console.WriteLine("베팅 금액 입력");

			while (true)
			{
				if (int.TryParse(Console.ReadLine(), out bettingPoint) && bettingPoint >= 0 && bettingPoint <= gamePoint)
					break;
				else
				{
					Console.WriteLine("잘못된 입력");
					continue;
				}
			}

			Console.WriteLine($"{bettingPoint} 포인트 베팅");
		}

		private CardCombinationPower GetCombinationPower(Card[] cards)
		{
			CardCombinationPower combination = CardCombinationPower.노페어;
			int combinationPower = (int)combination;

			int[] cardNumberCount = new int[13];
			int[] cardMarkCount = new int[4];
			Card[] sortedCards = new Card[cards.Length];
			for (int i = 0; i < cards.Length; i++)
			{
				sortedCards[i] = cards[i];
			}
			Array.Sort<Card>(sortedCards, (x, y) => y.number.CompareTo(x.number));//구조체 배열을 숫자 기준으로 내림차순 정렬
			foreach (Card card in sortedCards)
			{
				++cardNumberCount[card.number - 1];
				++cardMarkCount[card.mark];
			}

			int numberPairCount = 0;
			int maxNumberCount = 0;
			int maxMarkCount = 0;
			for (int i = 0; i < 13; i++)
			{
				if (cardNumberCount[i] == 2)
				{
					++numberPairCount;
				}
				if (maxNumberCount < cardNumberCount[i])
				{
					maxNumberCount = cardNumberCount[i];
				}
			}
			for (int i = 0; i < 4; i++)
			{
				if (maxMarkCount < cardMarkCount[i])
				{
					maxMarkCount = cardMarkCount[i];
				}
			}

			bool isContinuous = true;
			Card continuousEnd = sortedCards[0];
			Card continuousStart = sortedCards[sortedCards.Length - 1];
			int continuousCount = 0;
			for (int i = 0; i < sortedCards.Length - 1; i++)
			{
				if (continuousCount == 5)
				{
					continuousStart = sortedCards[i];
					break;
				}

				if (sortedCards[i].number - 1 != sortedCards[i + 1].number)
				{
					if (sortedCards.Length - i >= 5)
					{
						if (sortedCards[i].number - 9 == sortedCards[i + 1].number)
						{
							++continuousCount;
							continue;
						}
						continuousEnd = sortedCards[i + 1];
						continue;
					}
					else
					{
						isContinuous = false;
						break;
					}
				}
				++continuousCount;
			}

			if (maxNumberCount >= 4 && combinationPower < (int)CardCombinationPower.포카드)
			{
				combination = CardCombinationPower.포카드;
				combinationPower = (int)combination;
			}
			else if (maxNumberCount == 3)
			{
				if (numberPairCount >= 1 && combinationPower < (int)CardCombinationPower.풀하우스)
				{
					combination = CardCombinationPower.풀하우스;
					combinationPower = (int)combination;
				}
				else if (combinationPower < (int)CardCombinationPower.트리플)
				{
					combination = CardCombinationPower.트리플;
					combinationPower = (int)combination;
				}
			}
			else if (maxNumberCount == 2)
			{
				if (numberPairCount >= 2 && combinationPower < (int)CardCombinationPower.투페어)
				{
					combination = CardCombinationPower.투페어;
					combinationPower = (int)combination;
				}
				else if (combinationPower < (int)CardCombinationPower.원페어)
				{
					combination = CardCombinationPower.원페어;
					combinationPower = (int)combination;
				}
			}

			if (maxMarkCount >= 5 && combinationPower < (int)CardCombinationPower.플러시)
			{
				combination = CardCombinationPower.플러시;
				combinationPower = (int)combination;
			}

			if (isContinuous)
			{
				if (continuousStart.number == 1 && continuousEnd.number == 13)
				{

				}
			}

			return combination;
		}

		private void CompareCardCombination(int computerCombinationPower, int playerCombinationPower)
		{
			//cardCombinationTable.TryGetValue(computerCombination, out int computerPower);
			//cardCombinationTable.TryGetValue(playerCombination, out int playerPower);
			if (playerCombinationPower > computerCombinationPower)
			{
				Console.WriteLine("이번 라운드에서 승리");
			}
			else if (playerCombinationPower < computerCombinationPower)
			{
				Console.WriteLine("이번 라운드에서 패배");
			}
			else
			{
				Console.WriteLine("이번 라운드는 무승부");
			}
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
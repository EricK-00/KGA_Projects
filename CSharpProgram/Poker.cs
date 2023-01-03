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

		public int GetCardScore()
		{
			int cardNumberScore = number - 1;
			if (number == 1) cardNumberScore = 13;
			int cardScore = 4 * cardNumberScore - markIndex;

			return cardScore;
		}

		public char GetCardMark()
		{
			char markChar;

                switch (markIndex)
                {
                    case 0:
                    markChar = '♠';
                        break;
                    case 1:
                    markChar = '◆';
                        break;
                    case 2:
                    markChar = '♥';
                        break;
                    case 3:
                    markChar = '♣';
                        break;
                    default:
					markChar = '?';
                        break;
                }

			return markChar;
            }

		public string GetCardNumberString()
		{
			string numberString;

			switch (number)
			{
				case 1:
                    numberString = "A";
					break;
				case 11:
                    numberString = "J";
					break;
				case 12:
                    numberString = "Q";
					break;
				case 13:
                    numberString = "K";
					break;
				default:
                    numberString = number.ToString();
					break;
			}

			return numberString;
		}
	}

	enum Combination
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
        Card[] playerHands;
        Card[] computerHands;
		List<Card> handsCombination;

		string playerName = "플레이어";
		const string COMPUTER_NAME = "컴퓨터";

		const int FIRST_COMPUTER_CARDS_END_INDEX = 4;
		int computerHandsEndIndex;
		int playerHandsEndIndex;

		int roundCount;

		public void PlayPoker()
		{
			Console.WriteLine("<포인트를 10만 이상 획득하면 승리, 포인트를 모두 잃으면 패배>");
			Console.Write("플레이어의 이름을 입력하세요: ");
			playerName = Console.ReadLine();

			while (!GetIsGameOver())
			{
				Combination playerCombination;
				int playerTopCardScore;
				Combination computerCombination;
				int computerTopCardScore;

				Console.WriteLine("--------------------------------------------------------------------------------------");
				StartNewRound();

				ShuffleDeck(deck);

				SetUpHands(playerHands, 0, playerHandsEndIndex);
				playerCombination = GetCombination(playerHands, playerHandsEndIndex, out handsCombination).Item1;
				PrintHandsAndCombination(playerName, playerHands, playerHandsEndIndex, playerCombination, handsCombination);

				SetUpHands(computerHands, 0, FIRST_COMPUTER_CARDS_END_INDEX);
                computerCombination = GetCombination(computerHands, FIRST_COMPUTER_CARDS_END_INDEX, out handsCombination).Item1;
                PrintHandsAndCombination(COMPUTER_NAME, computerHands, FIRST_COMPUTER_CARDS_END_INDEX, computerCombination, handsCombination);

				Bet();

				ReplaceHands();
                (playerCombination, playerTopCardScore) = GetCombination(playerHands, playerHandsEndIndex, out handsCombination);
                PrintHandsAndCombination($"교체 후 {playerName}", playerHands, playerHandsEndIndex, playerCombination, handsCombination);

				SetUpHands(computerHands, FIRST_COMPUTER_CARDS_END_INDEX + 1, computerHandsEndIndex);
                (computerCombination, computerTopCardScore) = GetCombination(computerHands, computerHandsEndIndex, out handsCombination);
                PrintHandsAndCombination($"2장을 추가한 {COMPUTER_NAME}", computerHands, computerHandsEndIndex, computerCombination, handsCombination);

                CompareCombination((computerCombination, computerTopCardScore), (playerCombination, playerTopCardScore));
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
            playerHands = new Card[5];
            computerHands = new Card[7];
			handsCombination = new List<Card>();

			computerHandsEndIndex = computerHands.Length - 1;
			playerHandsEndIndex = playerHands.Length - 1;

			roundCount = 0;
		}

		//라운드 초기화 메서드
		private void StartNewRound()
		{
            ++roundCount;
            Console.Write("라운드를 시작하려면 아무 키나 입력하세요...");
            Console.ReadLine();
            Console.WriteLine($"<라운드 {roundCount}>\n[보유 포인트: {gamePoint}]");
            Console.WriteLine();
            currentCardIndex = 0;
        }

		//덱 셔플 메서드
		private void ShuffleDeck(Card[] deck)
		{
            Random random = new Random();

            for (int i = 0; i < deck.Length - 1; i++)
			{
				int randomIndex = random.Next(i, deck.Length);

				Card temp = deck[i];
				deck[i] = deck[randomIndex];
				deck[randomIndex] = temp;
			}
		}

		//카드 뽑기 메서드
		private Card DrawCard()
		{
			++currentCardIndex;
			return deck[currentCardIndex];
		}

		//카드를 문자열로 반환하는 메서드(오버로딩)
		//카드 한 장
		private string ShowCard(Card card)
		{
			return $"[{card.GetCardMark()}{card.GetCardNumberString().PadLeft(2)}]";
		}
		//카드 배열
		private string ShowCard(Card[] cards, int endIndex)
		{
            string resultString = string.Empty;

            for (int i = 0; i <= endIndex; i++)
            {
                if (i <= endIndex - 1)
					resultString += $"{ShowCard(cards[i])}, ";
                else
                    resultString += ShowCard(cards[i]);
            }

            return resultString;
        }
		//카드 리스트
		private string ShowCard(List<Card> cards)
		{
			string resultString = string.Empty;

			for (int i = 0; i < cards.Count; i++)
			{
				if (i < cards.Count - 1)
					resultString += $"{ShowCard(cards[i])}, ";
                else
                    resultString += ShowCard(cards[i]);
            }

			return resultString;
		}

		//현재 패와 카드 조합 출력 메서드
		private void PrintHandsAndCombination(string targertName, Card[] cards, int endIndex, Combination cardCombination, List<Card> combinationList)
		{
            Console.WriteLine($"{targertName}의 카드: {ShowCard(cards, endIndex)}");
            Console.Write($"{targertName}의 조합:「{cardCombination}」 ");
            Console.WriteLine($"{{{ShowCard(combinationList)}}}\n");
        }

		//카드 패 설정 메서드
		private void SetUpHands(Card[] cards, int startIndex, int endIndex)
		{
			for (int i = startIndex; i <= endIndex; i++)
			{
				cards[i] = DrawCard();
			}
        }

		//플레이어의 카드 교체 메서드
		private void ReplaceHands()
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
				Console.Write("교체할 카드의 위치를 입력하세요(1 ~ 5번): ");
				while (true)
				{
					if (int.TryParse(Console.ReadLine(), out cardNumber[i]) &&
						cardNumber[i] >= 1 &&
						cardNumber[i] <= 5 &&
						cardNumber[0] != cardNumber[1])
					{
						Console.WriteLine($"{ShowCard(playerHands[cardNumber[i] - 1])} 선택");
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
				Card nextCard = DrawCard();
				Console.WriteLine($"{ShowCard(playerHands[cardNumber[i] - 1])} -> {ShowCard(nextCard)}(으)로 교체");
				playerHands[cardNumber[i] - 1] = nextCard;
			}
			Console.WriteLine();
		}

		//포인트 베팅 메서드
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
			if (bettingPoint == gamePoint)
				Console.WriteLine($"{bettingPoint} 포인트 베팅(올인)\n");
			else
				Console.WriteLine($"{bettingPoint} 포인트 베팅\n");

			gamePoint -= bettingPoint;
		}

        //카드 조합과 탑 카드(조합이 동일할 경우 비교)를 반환하는 메서드
        #region 카드 조합 판별 메서드
        private (Combination, int) GetCombination(Card[] cards, int endIndex, out List<Card> combinationList)
		{
			CardScoreComparer cardScoreComparer = new CardScoreComparer();

			List<Card> resultList = new List<Card>();
			List<Card> noPairList = new List<Card>();
			List<Card> pairList = new List<Card>();
			List<Card> tripleList = new List<Card>();
			List<Card> fourCardList = new List<Card>();
			List<Card> flushList = new List<Card>();
			List<Card> straightList = new List<Card>();

            Combination combination = Combination.노페어;
            int topCardScore = 0;//탑 카드 점수(조합이 동일할 경우 비교하는 점수)
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

			//같은 수의 개수에 따라 해당하는 조합의 리스트에 저장
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
			if (fourCardList.Count >= 4 && (int)combination < (int)Combination.포카드)
			{
				combination = Combination.포카드;
			}
			if (tripleList.Count == 3 && pairList.Count >= 2 && (int)combination < (int)Combination.풀하우스)
			{
				combination = Combination.풀하우스;
			}
			if (tripleList.Count == 6 && (int)combination < (int)Combination.풀하우스)
			{
				combination = Combination.풀하우스;
			}
			if (tripleList.Count >= 3 && (int)combination < (int)Combination.트리플)
			{
				combination = Combination.트리플;
			}
			if (pairList.Count >= 4 && (int)combination < (int)Combination.투페어)
			{
				combination = Combination.투페어;
			}
			if (pairList.Count == 2 && (int)combination < (int)Combination.원페어)
			{
				combination = Combination.원페어;
			}

			//플러시
			for (int i = 0; i < cardMarkCount.Length; i++)
			{
				if (cardMarkCount[i] >= 5 && (int)combination < (int)Combination.플러시)
				{
					combination = Combination.플러시;

					flushList.Clear();
					for (int n = 13; n > 0; n--)
					{
						if (flushList.Count < 5 && cardTable[i, n % 13].number != -1)
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
								combination = Combination.로얄스트레이트플러시;

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
						if ((int)combination < (int)Combination.마운틴)
						{
							combination = Combination.마운틴;

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
								(int)combination < (int)Combination.백스트레이트플러시)
							{
								combination = Combination.백스트레이트플러시;

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
						if ((int)combination < (int)Combination.백스트레이트)
						{
							combination = Combination.백스트레이트;

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
						(int)combination < (int)Combination.스트레이트플러시)
						{
							combination = Combination.스트레이트플러시;

							straightList.Clear();
							straightList.Add(cardTable[j, i % 13]);
							straightList.Add(cardTable[j, (i - 1) % 13]);
							straightList.Add(cardTable[j, (i - 2) % 13]);
							straightList.Add(cardTable[j, (i - 3) % 13]);
							straightList.Add(cardTable[j, (i - 4) % 13]);

							break;
						}
					}

					//스트레이트
					if ((int)combination < (int)Combination.스트레이트)
					{
						combination = Combination.스트레이트;

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

			//백 스트레이트, 백 스트레이트 플러시가 아니면 정렬
			if (noPairList.Count > 0)
				noPairList.Sort(cardScoreComparer);
			if (pairList.Count > 0)
				pairList.Sort(cardScoreComparer);
			if (tripleList.Count > 0)
				tripleList.Sort(cardScoreComparer);
			if (fourCardList.Count > 0)
				fourCardList.Sort(cardScoreComparer);
			if (flushList.Count > 0)
				flushList.Sort(cardScoreComparer);
			if (straightList.Count > 0 && (int)combination != (int)Combination.백스트레이트 && (int)combination != (int)Combination.백스트레이트플러시)
				straightList.Sort(cardScoreComparer);

			//정렬된 리스트로 조합 리스트와 탑 카드 완성
			switch (combination)
			{
				case Combination.노페어:
					topCardScore = noPairList[0].GetCardScore();
					resultList.Add(noPairList[0]);
					break;
				case Combination.원페어:
					topCardScore = pairList[0].GetCardScore();
					resultList = pairList;
					break;
				case Combination.투페어:
					topCardScore = pairList[0].GetCardScore();
					for (int i = 0; i < 4; i++)
					resultList.Add(pairList[i]);
					break;
				case Combination.트리플:
					topCardScore = tripleList[0].GetCardScore();
					for (int i = 0; i < 3; i++)
						resultList.Add(tripleList[i]);
					break;
				case Combination.풀하우스:
					topCardScore = tripleList[0].GetCardScore();
					for (int i = 0; i < 3; i++)
						resultList.Add(tripleList[i]);
					
					if (tripleList.Count > 3)
					{
                        resultList.Add(tripleList[3]);
                        resultList.Add(tripleList[4]);
                    }
					else if (pairList.Count > 0)
					{
                        resultList.Add(pairList[0]);
                        resultList.Add(pairList[1]);
                    }

					break;
				case Combination.포카드:
					topCardScore = fourCardList[0].GetCardScore();
					resultList = fourCardList;
					break;
				case Combination.플러시:
					topCardScore = flushList[0].GetCardScore();
					resultList = flushList;
					break;
				case Combination.스트레이트:
				case Combination.스트레이트플러시:
				case Combination.백스트레이트:
				case Combination.백스트레이트플러시:
				case Combination.마운틴:
				case Combination.로얄스트레이트플러시:
					topCardScore = straightList[0].GetCardScore();
					resultList = straightList;
					break;
			}

			combinationList = resultList;

			return (combination, topCardScore);
		}
        #endregion

        //카드 숫자, 문양에 따라 내림차순으로 정렬하는 IComparer 클래스
        //숫자: A > K > ... > 2
        //문양: ♠ > ◆ > ♥ > ♣
        private class CardScoreComparer : IComparer<Card>
		{
			public int Compare(Card cardA, Card cardB)
			{
				int cardAScore = cardA.GetCardScore();
				int cardBScore = cardB.GetCardScore();

				if (cardAScore == cardBScore)
					return 0;
				if (cardAScore < cardBScore)
					return 1;
				else
					return -1;
			}
		}

		//조합 비교 후 결과 출력 메서드
		private void CompareCombination((Combination, int) computer, (Combination, int) player)
		{
			Combination computerCombination = computer.Item1;
			int computerTopCardScore = computer.Item2;
            Combination playerCombination = player.Item1;
            int playerTopCardScore = player.Item2;

            if ((int)playerCombination > (int)computerCombination ||
				((int)playerCombination == (int)computerCombination && playerTopCardScore > computerTopCardScore))
			{
				Console.Write("이번 라운드에서 승리하였습니다. 베팅한 포인트를 2배로 받습니다. ");
				gamePoint += 2 * bettingPoint;
			}
			else if ((int)playerCombination < (int)computerCombination ||
				(playerCombination == computerCombination && playerTopCardScore < computerTopCardScore))
			{
				Console.Write("이번 라운드에서 패배하였습니다. 베팅한 포인트를 모두 잃습니다. ");
			}
			else
			{
				Console.Write("이번 라운드는 무승부입니다. 베팅한 포인트의 절반만 받습니다. ");
				gamePoint += bettingPoint / 2;
			}

			Console.WriteLine($"[보유 포인트: {gamePoint}]");
		}

		//게임 종료 판단 메서드
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
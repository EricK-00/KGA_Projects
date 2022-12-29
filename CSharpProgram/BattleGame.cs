using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgram
{
	internal class BattleGame
	{
		Player player;
		Monster monster;
		int kills;
		bool isBattleEnd;
		bool isGameOver;

		public void PlayBattleGame()
		{
			while (!isGameOver)
			{
				Console.WriteLine("|---------------------------------------------------------------|");
				isBattleEnd = false;
				CreateMonster();
				while (!isBattleEnd)
					PlayAction();
				++kills;
				Console.WriteLine("|---------------------------------------------------------------|");
			}
			kills -= 1;
			GameOver();
		}

		public BattleGame()
		{
			player = new Player();
			kills = 0;
			isBattleEnd = false;
			isGameOver = false;
		}

		private void CreateMonster()
		{
			int randomMonster = new Random().Next(0, 2 + 1);
			switch (randomMonster)
			{
				case 0:
					Slime slime = new Slime();
					monster = slime;
					break;
				case 1:
					Wolf wolf = new Wolf();
					monster = wolf;
					break;
				case 2:
					Goblin goblin = new Goblin();
					monster = goblin;
					break;
			}

			Console.WriteLine($"{monster.Name} 등장! [체력: {monster.HP}, 공격력: {monster.Power}]");
		}

		private void PlayAction()
		{
			while (true)
			{
				Console.WriteLine("[a]: 공격, [!]: 인벤토리 [@]: 장착무기");

				string userInput = Console.ReadLine();
				switch (userInput)
				{
					case "a":
					case "A":
					case "ㅁ":
						Battle(player, monster);
						break;
					case "!":
						ShowInventory();
						continue;
					case "@":
						ShowWeapon();
						continue;
					default:
						Console.WriteLine("잘못된 입력");
						continue;
				}
				break;
			}
		}

		private void Battle(Player player, Monster monster)
		{
			int isPlayerFirst = new Random().Next(0, 1 + 1);

			if (isPlayerFirst == 1)
			{
				Attack(player, monster);
				if (!isBattleEnd)
					Attack(monster, player);
			}
			else
			{
				Attack(monster, player);
				if (!isBattleEnd)
					Attack(player, monster);
			}
		}

		private void Attack(GameCharacter attacker, GameCharacter defenser)
		{
			defenser.HP -= attacker.Power;
			Console.WriteLine($"{attacker.Name} 공격: {defenser.Name} {attacker.Power} 피해. 남은 {defenser.Name} 체력: {defenser.HP}");
			Console.WriteLine();

			if (defenser.HP <= 0)
			{
				if (defenser.Name == player.Name)
				{
					isGameOver = true;
				}
				else
				{
					(string item, int itemCount) = monster.DieAndDropItem();
					player.AddItem(item, itemCount);
				}
				isBattleEnd = true;
			}
		}

		private void ShowInventory()
		{
			string[] inventory = player.GetInventory().Item1;
			int[] inventoryItemCount = player.GetInventory().Item2;

			Console.WriteLine("---------------------------------------------------------------");
			Console.Write("인벤토리: ");
			for (int i = 0; i < inventory.Length; i++)
			{
				if (inventory[i] == player.GetInventoryDefaultValue())
					Console.Write($"|{inventory[i]}| ");
				else
					Console.Write($"|{inventory[i]} x{inventoryItemCount[i]}/{player.GetItemCountMaximum()}| ");
			}
			Console.WriteLine();
			Console.WriteLine("---------------------------------------------------------------");
			EquipItem();
			Console.WriteLine();
		}

		private void ShowWeapon()
		{
			Console.WriteLine("---------------------------------------------------------------");
			Console.Write($"장착무기: {player.weapon.WeaponName}");
			Console.WriteLine();
			Console.WriteLine("---------------------------------------------------------------");
		}

		private void EquipItem()
		{
			Console.WriteLine("[1~3]: 아이템 장착, [q]: 돌아가기");
			while (true)
			{
				string userInput = Console.ReadLine();
				switch (userInput)
				{
					case "q":
					case "Q":
					case "ㅂ":
						break;
					case "1":
					case "2":
					case "3":
						ChangeWeapon(userInput);
						continue;
					default:
						Console.WriteLine("잘못된 입력");
						continue;
				}
				break;
			}
		}

		private void ChangeWeapon(string userInput)
		{
			int index;
			int.TryParse(userInput, out index);
			--index;

			string[] inventory = player.GetInventory().Item1;
			int[] inventoryItemCount = player.GetInventory().Item2;


			for (int i = 0; i < Weapon.weaponNameArray.Length; i++)
			{

				if (inventory[index] == Weapon.weaponNameArray[i])
				{
					--inventoryItemCount[index];
					player.EquipWeapon(i);
					Console.WriteLine($"{Weapon.weaponNameArray[i]} 장착");
					return;
				}
			}
			Console.WriteLine("장착할 수 없는 아이템입니다. [장착 가능한 아이템: 낡은 검, 검, 대검]");
		}

		private void GameOver()
		{
			Console.WriteLine($"플레이어가 쓰러졌습니다.\n처치한 몬스터의 수: {kills}\n[게임종료]");
		}
	}

	class Weapon
	{
		const int WEAPON_COUNT = 3;
		public static string[] weaponNameArray = new string[WEAPON_COUNT] { "낡은 검", "검", "대검"};
		public int WeaponIndex { get; }
		public string WeaponName { get; }
		public int WeaponPower { get; }

		public Weapon(int index)
		{
			WeaponIndex = index;
			switch (WeaponIndex)
			{
				case 1:
					WeaponName = "검";
					WeaponPower = 3;
					break;
				case 2:
					WeaponName = "대검";
					WeaponPower = 10;
					break;
				case 0:
				default:
					WeaponName = "낡은 검";
					WeaponPower = 1;
					break;
			}
		}
	}

	class GameCharacter
	{
		protected string name;
		public string Name { get { return name; } }

		protected int hp;
		public int HP
		{
			get
			{
				if (hp <= 0)
					hp = 0;
				return hp;
			}

			set { hp = value; }
		}

		protected int power;
		public int Power { get { return power; } }
	}

	class Player : GameCharacter
	{
		const int INVENTORY_SIZE = 3;
		const int ITEM_COUNT_MAXIMUM = 9;
		const string INVENTORY_DEFAULT_VALUE = "없음";
		const int DEFAULT_WEAPON_INDEX = 0;

		private string[] inventory;
		private int[] inventoryItemCount;

		public Weapon weapon;

		public Player()
		{
			name = "플레이어";
			hp = 300;
			power = 15;

			inventory = new string[INVENTORY_SIZE];
			inventoryItemCount = new int[INVENTORY_SIZE];
			for (int i = 0; i < inventory.Length; i++)
			{
				inventory[i] = INVENTORY_DEFAULT_VALUE;
				inventoryItemCount[i] = 0;
			}

			EquipWeapon(DEFAULT_WEAPON_INDEX);
		}

		public int GetItemCountMaximum()
		{
			return ITEM_COUNT_MAXIMUM;
		}

		public string GetInventoryDefaultValue()
		{
			return INVENTORY_DEFAULT_VALUE;
		}

		public (string[], int[]) GetInventory()
		{
			return (inventory, inventoryItemCount);
		}

		public void AddItem(string item, int itemCount)
		{
			for (int i = 0; i < inventory.Length; i++)
			{

				if (inventory[i] == INVENTORY_DEFAULT_VALUE)
				{
					inventory[i] = item;
					inventoryItemCount[i] = itemCount;
					return;
				}

				else if (inventory[i] == item && inventoryItemCount[i] < ITEM_COUNT_MAXIMUM)
				{
					inventoryItemCount[i] += itemCount;
					if (inventoryItemCount[i] > ITEM_COUNT_MAXIMUM)
						inventoryItemCount[i] = ITEM_COUNT_MAXIMUM;
					return;
				}
			}

			Console.WriteLine("가방에 빈 공간이 없어 보상을 획득하지 못했습니다.");
		}

		public void EquipWeapon(int weaponIndex)
		{
			if (weapon != null)
				power -= weapon.WeaponPower;
			weapon = new Weapon(weaponIndex);
			power += weapon.WeaponPower;
		}
	}

	class Monster : GameCharacter
	{
		protected string[] dropItems;

		public (string, int) DieAndDropItem()
		{
			string dropItem = dropItems[new Random().Next(0, dropItems.Length)];
			int itemCount = new Random().Next(1, 3 + 1);

			Console.WriteLine($"{name} 처치!");
			Console.WriteLine($"보상: {dropItem} x{itemCount}");

			return (dropItem,itemCount);
		}
	}

	class Slime : Monster
	{
		public Slime()
		{
			name = "슬라임";
			hp = 20;
			power = 5;
			dropItems = new string[1] { "골드" };
		}
	}

	class Wolf : Monster
	{
		public Wolf()
		{
			name = "늑대";
			hp = 50;
			power = 10;
			dropItems = new string[4] { "가죽", "늑대 이빨", "검", "대검" };
		}
	}

	class Goblin : Monster
	{
		public Goblin()
		{
			name = "고블린";
			hp = 10;
			power = 2;
			dropItems = new string[3] { "가죽", "고블린 손 뼈", "낡은 검" };
		}
	}
}

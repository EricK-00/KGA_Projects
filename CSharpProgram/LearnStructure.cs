using System;

namespace CSharpProgram
{
	class LearnStructure
	{
		struct Slime
		{
			int hp;
			int power;
			int defense;
			string monsterType;
			string[] dropItem;
		}
	}
	public struct BusinessCard
	{
		public string name;
		public int age;
		public string address;

		public BusinessCard(string name_, int age_, string address_)
		{
			name = name_;
			age = age_;
			address = address_;
		}

		public void PrintBusinessCard()
		{
			Console.WriteLine($"이름: {name}, 나이: {age}, 주소 {address}");
		}
	}
}
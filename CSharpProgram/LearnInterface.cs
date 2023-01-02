using System;

namespace CSharpProgram
{
	internal class LearnInterface
	{

	}

	interface IPlayable
	{
		void Move();
	}

	interface IDamagable
	{
		int HP { get; }

		public void Damaged(int damage);
	}

	class Player : IDamagable, IPlayable
	{
		public int HP { get; }
		public void Damaged(int damage)
		{

		}

		public void Move()
		{

		}
	}
}
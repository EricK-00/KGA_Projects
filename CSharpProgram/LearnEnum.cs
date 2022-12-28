using System;

namespace CSharpProgram
{
	class LearnEnum
	{
		Animal animal;

		enum Animal
		{
			CHICKEN, DOG, PIG
		}

		public void GetAnimalsSound()
		{
			switch (animal)
			{
				case Animal.CHICKEN:
					Console.WriteLine("꼬끼오");
					break;
					case Animal.DOG:
					Console.WriteLine("멍멍");
					break;
				case Animal.PIG:
					Console.WriteLine("꿀꿀");
					break;
				default:
					Console.WriteLine("예외");
					break;
			}
		}
	}
}

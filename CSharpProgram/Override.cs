using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgram
{

	class MyParents
	{
		public virtual void Say()
		{
			Console.WriteLine("[부모] Say");
		}
		public virtual void Walk()
		{
			Console.WriteLine("[부모] Walk");
		}
	}

	class MyChild : MyParents
	{
		public override void Say()
		{
			Console.WriteLine("[자식] Say");
		}
		public override void Walk()
		{
			Console.WriteLine("[자식] Walk");
		}
	}

	class Button
	{
		protected int _index;

		public virtual void OnClickButton()
		{
			Console.WriteLine($"{_index}번 버튼");
		}
	}

	class StoreButton : Button
	{
		public override void OnClickButton()
		{
			_index = 1;
			base.OnClickButton();

			Console.WriteLine("상점");
		}
	}

	class QuestButton : Button
	{
		public override void OnClickButton()
		{
			_index = 2;
			base.OnClickButton();

			Console.WriteLine("퀘스트");
		}
	}
}
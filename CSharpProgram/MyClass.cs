using System;

namespace CSharpProgram
{
	class MyClassParents
	{
		public string GetString()
		{
			return "Parents";
		}
	}

	class MyClass : MyClassParents
	{
		int a;
		new public string GetString()//하이딩
		{
			int a;//섀도잉
			return "Child";
		}
	}

	class ClassPrinter
	{
		public static void PrintClass(MyClass myClass, MyClassParents myClassParents)
		{
			Console.WriteLine(myClassParents.GetString());
			Console.WriteLine(myClass.GetString());
			Console.WriteLine(((MyClassParents)myClass).GetString());
			Console.WriteLine((myClass as MyClassParents).GetString());
		}
	}
}
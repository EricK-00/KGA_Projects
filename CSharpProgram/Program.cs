using System;
using System.Threading.Tasks;

namespace CSharpProgram
{
	class Program
	{
		static void Main(string[] args)
		{
			Board board = new Board();
			board.PlayGame();

			//Task.Delay(1000).Wait();
/*			Task loopTask;
			loopTask = Task.Run(async () => { await Task.Delay(1000); });
			loopTask.Wait();*/
		}
	}
}
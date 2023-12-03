using task_2;

namespace task_2
{
    public class Program
    {
        public static void Main()
        {
            Run("../../../input/", "../../../output/");
        }

        public static async Task Run(string input, string output)
        {
            // GenerateTestTransactions generateTestTransactions = new GenerateTestTransactions(10, input);
            // generateTestTransactions.GenerateTransactions();
            
            //EmulatorTasks emulatorTasks = new EmulatorTasks(input, output);
            //await emulatorTasks.Run();

            //EmulatorThreadPool emulatorThreadPool = new EmulatorThreadPool(input, output);
            //emulatorThreadPool.Run();

            EmulatorThreads emulatorThreads = new EmulatorThreads(input, output);
            emulatorThreads.Run();
        }
    }
}
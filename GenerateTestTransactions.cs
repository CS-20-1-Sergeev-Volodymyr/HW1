namespace task_2
{
    public class GenerateTestTransactions
    {
        public int amount;
        private readonly string inputPath;
        Random random = new Random();

        public GenerateTestTransactions(int amount, string inputPath)
        {
            this.amount = amount;
            this.inputPath = inputPath;
        }

        public void GenerateTransactions()
        {
            int minRange = -10000;
            int maxRange = 10000;
            int i = 0;
    
            while (i < amount)
            {
                int randomInRange = random.Next(minRange, maxRange + 1);

                if (randomInRange != 0)
                {
                    CreateTestTransactionFile(randomInRange);
                    i++; 
                }
            }
        }

        public void CreateTestTransactionFile(decimal amount)
        {
            var filePath = Path.Combine(inputPath, $"{GenerateNameForFile()}_log.csv");

            using (var writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{amount}");
            }
        }

        public string GenerateNameForFile()
        {
            string uniqueFileName = Guid.NewGuid().ToString();
            return uniqueFileName;
        }
    }
}
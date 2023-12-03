using task_2;
public class EmulatorThreads
{
    public string inputPath;
    public string outputPath;
    public const string resultFileName = "/data.json";
    public ProcessFiles ProcessFiles = new ProcessFiles();
    public DebitCard DebitCard = new DebitCard();
    public LogTransaction LogTransaction = new LogTransaction();

    public EmulatorThreads(string inputPath, string outputPath)
    {
        this.inputPath = inputPath;
        this.outputPath = outputPath;
    }

    public void Run()
    {
        ProcessFiles.DeleteAllFilesInDirectory(outputPath);
        string[] files = ProcessFiles.FilesInFolder(inputPath);
        List<Thread> threads = new List<Thread>();

        foreach (string file in files)
        {
            Thread thread = new Thread(ProcessTransaction);
            threads.Add(thread);
            thread.Start(file);
        }
        
        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        LogTransaction.WriteObjectToJsonFile(new Result(DebitCard.balance, DebitCard.successfulTransactions, DebitCard.nonSuccessfulTransactions, ProcessFiles.filesProcessed), outputPath + resultFileName);
    }

    public void ProcessTransaction(object state)
    {
        if (state is string filePath)
        {
            int transactionAmount = ProcessFiles.GetValue(filePath);
            bool isDeposit = transactionAmount > 0;
            bool success;
            if (isDeposit)
            {
                success = DebitCard.Deposit(Math.Abs(transactionAmount));
            }
            else
            {
                success = DebitCard.Withdraw(Math.Abs(transactionAmount));
            }

            LogTransaction.LogTransactionToFile(outputPath, filePath, transactionAmount, success);
            Console.WriteLine($"File {filePath} processed. Success: {success}");
        }
    }
}
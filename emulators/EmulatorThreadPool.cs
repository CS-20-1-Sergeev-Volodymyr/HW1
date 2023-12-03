using task_2;

public class EmulatorThreadPool
{
    public string inputPath;
    public string outputPath;
    public ProcessFiles ProcessFiles = new ProcessFiles();
    public DebitCard DebitCard = new DebitCard();
    public LogTransaction LogTransaction = new LogTransaction();

    public EmulatorThreadPool(string inputPath, string outputPath)
    {
        this.inputPath = inputPath;
        this.outputPath = outputPath;
    }

    public void Run()
    {
        ProcessFiles.DeleteAllFilesInDirectory(outputPath);
        string[] files = ProcessFiles.FilesInFolder(inputPath);

        foreach (string file in files)
        {
            ThreadPool.QueueUserWorkItem(ProcessTransaction, file);
        }
        
        ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);
        ThreadPool.SetMinThreads(Environment.ProcessorCount, Environment.ProcessorCount);
        while (ThreadPool.PendingWorkItemCount > 0) { }

        LogTransaction.WriteObjectToJsonFile(new Result(DebitCard.balance, DebitCard.successfulTransactions, DebitCard.nonSuccessfulTransactions, ProcessFiles.filesProcessed), outputPath + "/data.json");
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
        }
    }
}
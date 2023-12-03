using task_2;

public class EmulatorTasks
{
    public string inputPath;
    public string outputPath;
    List<Task> tasks = new List<Task>();
    public ProcessFiles ProcessFiles = new ProcessFiles();
    public DebitCard DebitCard = new DebitCard();
    public LogTransaction LogTransaction = new LogTransaction();

    public EmulatorTasks(string inputPath, string outputPath)
    {
        this.inputPath = inputPath;
        this.outputPath = outputPath;
    }

    public async Task Run()
    {
        ProcessFiles.DeleteAllFilesInDirectory(outputPath);
        string[] files = ProcessFiles.FilesInFolder(inputPath);
        foreach (string file in files)
        {
            tasks.Add(ProcessTransaction(file));
        }
        await Task.WhenAll(tasks);
        
        LogTransaction.WriteObjectToJsonFile(new Result(DebitCard.balance, DebitCard.successfulTransactions, DebitCard.nonSuccessfulTransactions, ProcessFiles.filesProcessed), outputPath + "/data.json");
    }
    public async Task ProcessTransaction(string filePath)
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
            success =  DebitCard.Withdraw(Math.Abs(transactionAmount));
        }
        LogTransaction.LogTransactionToFile(outputPath, filePath, transactionAmount, success);
    }
   
}
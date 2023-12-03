namespace task_2;

public class Result
{
    public int FinalBalance { get; set; }
    public int TotalSuccessfulTransactions { get; set; }
    public int TotalUnsuccessfulTransactions { get; set; }
    public int TotalProcessedFiles { get; set; }

    public Result(int finalBalance, int totalSuccessfulTransactions, int totalUnsuccessfulTransactions, int totalProcessedFiles)
    {
        FinalBalance = finalBalance;
        TotalSuccessfulTransactions = totalSuccessfulTransactions;
        TotalUnsuccessfulTransactions = totalUnsuccessfulTransactions;
        TotalProcessedFiles = totalProcessedFiles;
    }
}

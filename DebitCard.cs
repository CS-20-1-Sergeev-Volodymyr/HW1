namespace task_2;

public class DebitCard
{
    public int balance = 0;
    public int successfulTransactions = 0;

    public int SuccessfulTransactions
    {
        get => successfulTransactions;
        set => successfulTransactions = value;
    }

    public int NonSuccessfulTransactions
    {
        get => nonSuccessfulTransactions;
        set => nonSuccessfulTransactions = value;
    }

    public int nonSuccessfulTransactions = 0;
    
    private readonly object balanceLock = new object();

    public bool Deposit(int amount)
    {

        lock (balanceLock)
        {
            balance += amount;
            successfulTransactions++;
        }

        return true;
    }

    public bool Withdraw(int amount)
    {

        lock (balanceLock)
        {
            if (balance >= amount)
            {
                balance -= amount;
                successfulTransactions++;
                return true;
            }

            nonSuccessfulTransactions++;
            return false;
        }
    }
}
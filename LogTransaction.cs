using Newtonsoft.Json;

namespace task_2;

public class LogTransaction
{
    public void LogTransactionToFile(string outputPath, string filePath,  decimal amount, bool success)
    {
        var file = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(filePath));
        
        using (var writer = new StreamWriter(file, true))
        {
            writer.WriteLine($"{amount},{success}");
        }
    }

    public static void WriteObjectToJsonFile(object obj, string filePath)
    {
        try
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing JSON to file: {ex.Message}");
        }
    }
}
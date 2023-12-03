namespace task_2
{
    public class ProcessFiles
    {
        public int filesProcessed = 0;

        public int FilesProcessed
        {
            get => filesProcessed;
            set => filesProcessed = value;
        }

        public string[] FilesInFolder(string inputPath)
        {
                string[] files = Directory.GetFiles(inputPath);
                var sortedFiles = files
                    .OrderBy(f => File.GetCreationTime(f))
                    .ToArray();
                filesProcessed = files.Length;
                return sortedFiles;
        }

        public int GetValue(string filePath)
        {
            int line = 0;
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        line = Convert.ToInt32(reader.ReadLine());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading file {filePath}: {ex.Message}");
            }
            return line;
            
            
        }
        public void DeleteAllFilesInDirectory(string directoryPath)
        {
            try
            {
                string[] files = Directory.GetFiles(directoryPath);
                
                foreach (string file in files)
                {
                    File.Delete(file);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting files: {ex.Message}");
            }
        }
    }
}
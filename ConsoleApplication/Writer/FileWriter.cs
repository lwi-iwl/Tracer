using System.IO;

namespace ConsoleApplication.Writer
{
    public class FileWriter
    {
        public void Write(string result, string filepath)
        {
            using StreamWriter writer = new StreamWriter(filepath);
            writer.Write(result);
        }
    }
}

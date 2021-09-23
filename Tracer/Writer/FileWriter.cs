using System.IO;

namespace Tracer.writer
{
    class FileWriter
    {
        public void Write(string result, string filepath)
        {
            using StreamWriter writer = new StreamWriter(filepath);
            writer.Write(result);
        }
    }
}

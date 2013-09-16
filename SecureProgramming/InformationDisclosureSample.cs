using System;
using System.IO;

namespace InformationDisclosureSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                using (FileStream fileStream = new FileStream(@"C:\test.txt", FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(fileStream))
                    {
                        writer.WriteLine("Test");
                        Console.WriteLine("Successfully written file.");
                    }
                }
                Console.ReadLine();
            }
            catch (IOException)
            {
                Console.WriteLine("An error has occured.");
                Console.ReadLine();
            }
        }

    }
}



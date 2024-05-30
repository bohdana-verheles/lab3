using System.ComponentModel.Design;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Text;

namespace lab4
{
    internal class Program
    {
        static void Help()
        {
            Console.WriteLine("Counts the number of subdirectories\ndirectory|[hidden]\n" +
                "directory\tSpecifies the directory to count the number of subdirectories\n" +
                "hidden\t\tFlag to specify the need to count hidden subfoders. " +
                "If blank - all subfoders are counted, true - only hidden subfoders, false - only visible subfoders");
        }

        static int CountSubdirectories(string directory)
        {
            return Directory.GetDirectories(directory, "*", SearchOption.AllDirectories).Length;
        }

        static int CountSubdirectories(string directory, bool hidden)
        {
            string[] subdirectories = Directory.GetDirectories(directory, "*", SearchOption.AllDirectories);
            int countHidden = 0;
            foreach(var d in subdirectories)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(d);
                if (directoryInfo.Attributes.HasFlag(FileAttributes.Hidden))
                    countHidden++;
            }
            if (hidden == true)
                return countHidden;
            else 
                return subdirectories.Length - countHidden;
        }

        static int Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            string parametersStr = Console.ReadLine();
            if(parametersStr.ToLower() == "help")
                Help();
            else
            {
                string[] parameters = parametersStr.Split('|');
                if (parameters.Length == 0 || parameters.Length > 2)
                {
                    Console.WriteLine("Incorrect number of parameters");
                    return 1;
                }
                else
                {
                    string directory = parameters[0];
                    if (parameters.Length == 1)
                    {
                        Console.WriteLine(CountSubdirectories(directory));
                    }
                    else
                    {
                        bool hidden;
                        if (bool.TryParse(parameters[1], out hidden))
                        {
                            Console.WriteLine(CountSubdirectories(directory, hidden));
                        }
                        else
                        {
                            Console.WriteLine("The second parameter should be true/false");
                            return 1;
                        }
                    }
                }
            }
            return 0;
        }
    }
}

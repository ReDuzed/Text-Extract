using System;
using System.IO;

namespace program
{
    class Program
    {
        static void Main(string[] args)
        {
            string 
                query = string.Empty, 
                end = string.Empty, 
                fileNoExt = string.Empty, 
                ext = string.Empty, 
                outputName = string.Empty;

            if (args[0] == "/help" || args[0] == "/?" || args[0] == "--help")
            {
                Console.WriteLine("Parameters:\n"+
                    "   <-query  <symbol or phrase>>\n" +
                    "   <-end    <symbol or phrase>>\n" +
                    "   <-file   <file name [and extension]>>\n" +
                    "   [-ext    <file extension>]\n" + 
                    "   <-output <file name and extension>>");
                    return;
            }

            for (int i = 0; i < args.Length; i++)
            {
                if (args.Length >= i)
                {
                    switch (args[i])
                    {
                        case "-query":
                            query = args[i + 1];
                            break;
                        case "-end":
                            end = args[i + 1];
                            break;
                        case "-file":
                            fileNoExt = args[i + 1];
                            break;
                        case "-ext":
                            ext = args[i + 1];
                            break;
                        case "-output":
                            outputName = args[i + 1];
                            break;
                    }
                }
            }
            Console.WriteLine(string.Format("Input\n   query: {0}\n   end: {1}\n   file: {2}\n   ext: {3}\n   output: {4}", query, end, fileNoExt, ext, outputName));
            Console.WriteLine("Continue? Y/n");
            ConsoleKey key;
            while ((key = Console.ReadKey().Key) != ConsoleKey.Enter && key != ConsoleKey.Y)
            {
                if (key == ConsoleKey.N)
                    return;
            }
            new Function(query, end, fileNoExt, ext, outputName).Reformat();
        }
    }
    class Function
    {
        string query, end, fileNoExt, ext, outputName;
        public Function(string query, string end, string fileNoExt, string ext = ".txt", string outputName = "output.txt")
        {
            this.query = query;
            this.end = end;
            this.fileNoExt = fileNoExt;
            this.ext = ext;
            this.outputName = outputName;
        }
        public void Reformat()
        {
            using (StreamReader sr = new StreamReader(fileNoExt + ext))
            {
                using (StreamWriter sw = new StreamWriter(outputName))
                {
                    while (!sr.EndOfStream)
                    {
                        string param = sr.ReadLine();
                        string result = string.Empty;
                        if (param.ToLower().Contains(query.ToLower()))
                        {
                            result = param.Substring(param.IndexOf(query) + query.Length);
                            result = result.Substring(0, result.IndexOf(end));
                            Console.WriteLine(result);
                            sw.WriteLine(result);
                        }
                    }
                }
            }
        }
    }
}

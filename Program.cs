namespace Sectra
{
    public class Program
    {
        /// <summary>
        /// Starting point of the program
        /// Gets the input from arguments and 
        /// pases the input to the RegisterData class
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {   
            string[] linesFromFile;
            if(args.Count() != 1){
                Console.WriteLine("Need two command line arguments: sectra [filename.txt]");
                return;
            }
            string path = args[0];
            if(File.Exists(path)) {
                RegisterData inputData = new();
                linesFromFile = File.ReadAllLines(path);
                foreach(string line in linesFromFile){
                    if (line.Count() <= 40)
                    {
                        inputData.ReadInput(line.Trim());
                    }
                    else{
                        string message = (line.Length > 0) ?
                        "is greater than 40 characters" :
                        "can not be empty";
                        Console.WriteLine("Error: Line:\n\n\t" +
                        $"{line}\n\n {message}.\n");
                    }
                    Console.ReadLine();
                }
            }
            else {
                Console.WriteLine($"Error: File path:\n\n\t {path} \n\ndoes not exist.\n");
            }
        }
    }
}
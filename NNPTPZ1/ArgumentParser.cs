namespace NNPTPZ1
{
    public static class ArgumentParser
    {
        public static void ParseArguments(string[] args)
        {
            try
            {
                Program.XSize = int.Parse(args[0]);
                Program.YSize = int.Parse(args[1]);
                Program.XMin = double.Parse(args[2]);
                Program.XMax = double.Parse(args[3]);
                Program.YMin = double.Parse(args[4]);
                Program.YMax = double.Parse(args[5]);


                // only return outputFile if the argument was given
                Program.OutputFile = args.Length > 6 ? args[6] : null;
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error parsing arguments: " + ex.Message);
                Environment.Exit(1);
            }
        }
    }
}
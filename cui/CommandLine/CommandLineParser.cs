namespace cui.CommandLine
{
    public class CommandLineParser
    {
        public CommandLineParser(string[] args)
        {
            _cmd = args;
        }

        readonly string[] _cmd;

        public CommandLineResult Parse()
        {
            return null;
        }
    }
}
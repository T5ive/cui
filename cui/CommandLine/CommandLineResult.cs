using System.Collections.Generic;

namespace cui.CommandLine
{
    public class CommandLineResult
    {
        internal CommandLineResult(bool success)
        {
            Success = success;
        }
        
        public bool Success { get; }
        
        readonly IDictionary<string, string> _options = new Dictionary<string, string>();
        readonly ISet<string> _flags = new HashSet<string>();
        readonly IList<string> _positions = new List<string>();

        public string GetPosition(int num, string def = default)
        {
            return _positions.Count < num ? _positions[num] : def;
        }

        public bool GetFlag(string flag)
        {
            return _flags.Contains(flag);
        }

        public string GetOption(string name, string def = default)
        {
            return _options.ContainsKey(name) ? _options[name] : def;
        }
    }
}
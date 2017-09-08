using System.Collections.Generic;

namespace MonoDragons.Core.Logs
{
    public sealed class InMemoryLog : ILog
    {
        private readonly int _maxLines;

        public List<string> Lines { get; } = new List<string> { "" };

        public InMemoryLog(int maxLines = 10000)
        {
            _maxLines = maxLines;
        }

        public void Write(string msg)
        {
            while (Lines.Count >= _maxLines)
                Lines.RemoveAt(0);
            Lines.Add(msg);
        }
    }
}

using System.Collections.Generic;
using System.Diagnostics;

namespace MonoDragons.Core.Logs
{
    public sealed class DebugLog : ILog
    {
        public List<string> Messages { get; } = new List<string>();

        public void Write(string msg)
        {
            Messages.Add(msg);
            Debug.WriteLine(msg);
        }
    }
}

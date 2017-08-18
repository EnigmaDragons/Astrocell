using System.Collections.Generic;
using System.Diagnostics;
using Astrocell.Battles.Battles;

namespace Astrocell.Battles
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

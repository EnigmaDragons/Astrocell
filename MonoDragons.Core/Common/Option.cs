using System;

namespace MonoDragons.Core.Common
{
    public struct Option
    {
        public string Name { get; set; }
        public Action Action { get; set; }

        public Option(string name, Action action)
        {
            Name = name;
            Action = action;
        }
    }
}

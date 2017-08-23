using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoDragons.Core.Networking
{
    public class Message
    {
        public long Number { get; set; }
        public string Type { get; private set; }
        public object Value { get; private set; }

        public Message(long number, object value)
        {
            Number = number;
            Type = value.GetType().ToString();
            Value = value;
        }
    }
}

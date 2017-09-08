using System;
using System.Collections.Generic;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Logs;

namespace Astrocell.Plugins
{
    public sealed class BufferedLog : EntityComponent, ILog
    {
        private readonly List<string> _newLines = new List<string>();
        private TimeSpan _remainingBufferDuration = TimeSpan.Zero;

        public List<string> Lines { get; set; } = new List<string> { "" };
        public TimeSpan BufferDuration { get; set; } = TimeSpan.FromMilliseconds(800);

        public void Update(TimeSpan delta)
        {
            _remainingBufferDuration -= delta;
            if (_newLines.None() || _remainingBufferDuration > TimeSpan.Zero)
                return;

            _remainingBufferDuration = BufferDuration;
            Lines.Add(_newLines[0]);
            _newLines.RemoveAt(0);
        }

        public void Write(string msg)
        {
            _newLines.Add(msg);
        }
    }
}

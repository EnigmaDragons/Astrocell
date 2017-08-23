using System;

namespace MonoDragons.Core.Networking
{
    [Flags]
    public enum LatencyMonitorMethod
    {
        Ping = 1,
        Echo = 2
    }
}

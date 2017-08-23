using System;

namespace MonoDragons.Core.Networking
{
    public interface ILatencyMonitor
    {
        LatencyMonitorMethod LatencyMonitor { get; set; }
        int PingEveryMillis { get; set; }
        long Latency { get; }
        Action NoResponseCallback { set; }
    }
}

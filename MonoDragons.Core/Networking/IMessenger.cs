using MonoDragons.Core.Common;
using System;

namespace MonoDragons.Core.Networking
{
    public interface IMessenger : IDisposable
    {
        void Send(byte[] bytes);
        Action<byte[]> ReceivedCallback { set; }
        Optional<bool> ConnectionSuccessful { get; }
        long UniqueIdentifier { get; }
        Action OnConnectionFail { set; }
        Action OnConnectionSuccess { set; }
    }
}

using System;
using Lidgren.Network;
using System.Linq;
using System.Threading;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.Networking
{
    public class PeerToPeerClient : IMessenger, ILatencyMonitor, IAutomaton
    {
        private const byte NormalDenoter = 0;
        private const byte PingDenoter = 1;
        private const byte PongDenoter = 2;
        private const byte ToEchoDenoter = 3;
        private const byte EchoedDenoter = 4;

        public const long NOT_TESTED = -2;
        public const long NO_RESPONSE = -1;

        private long _now => DateTimeOffset.UtcNow.Ticks;
        private NetClient _client;
        private NetConnection _connectionStatus;
        private long _toEchoSent = 0;
        private bool _awaitingEchoResponse = false;
        private long _pingSent = 0;
        private bool _awaitingPingResponse = false;
        private bool _hasFinishedTryingToConnect = false;

        public LatencyMonitorMethod LatencyMonitor { get; set; }
        public int PingEveryMillis { get; set; } = 1000;
        public long Latency { get; private set; } = NOT_TESTED;
        public Action NoResponseCallback { private get; set; } = () => { };
        public Action<byte[]> ReceivedCallback { private get; set; } = (s) => { };
        public long UniqueIdentifier => _client.UniqueIdentifier;
        public Optional<bool> ConnectionSuccessful => _connectionStatus.Status == NetConnectionStatus.Connected ? new Optional<bool>(true)
            : _connectionStatus.Status == NetConnectionStatus.Disconnected ? new Optional<bool>(false) : new Optional<bool>();
        public Action OnConnectionFail { private get; set; }
        public Action OnConnectionSuccess { private get; set; }

        private PeerToPeerClient()
        {
            _client = new NetClient(new NetPeerConfiguration("chat") { AutoFlushSendQueue = false });
        }

        private void Init(string url, int port)
        {
            _client.RegisterReceivedCallback(new SendOrPostCallback((a) => GetNewMessages()));
            _client.Start();
            _connectionStatus = _client.Connect(url, port);
        }

        public static PeerToPeerClient CreateConnected(string url, int port)
        {
            return CreateConnected(url, port, (a) => { }, () => { });
        }

        public static PeerToPeerClient CreateConnected(string url, int port, Action<PeerToPeerClient> onConnectionSuccess, Action onConnectionFailed)
        {
            var client = new PeerToPeerClient();
            client.OnConnectionSuccess = () => onConnectionSuccess(client);
            client.OnConnectionFail = onConnectionFailed;
            client.Init(url, port);
            return client;
        }

        private void GetNewMessages()
        {
            NetIncomingMessage inMessage;
            while ((inMessage = _client.ReadMessage()) != null)
            {
                if (inMessage.MessageType == NetIncomingMessageType.Data)
                {
                    var s = inMessage.ReadBytes(inMessage.LengthBytes);
                    if (s[0] == PingDenoter)
                    {
                        SendMessage(new[] { PongDenoter });
                    }
                    else if (s[0] == PongDenoter)
                    {
                        Latency = _now - _pingSent;
                        _awaitingPingResponse = false;
                    }
                    else if (s[0] == ToEchoDenoter)
                    {
                        SendMessage(new[] { EchoedDenoter });
                        ReceivedCallback(s.Skip(1).ToArray());
                    }
                    else if (s[0] == EchoedDenoter)
                    {
                        Latency = _now - _toEchoSent;
                        _awaitingEchoResponse = false;
                    }
                    else if (s[0] == NormalDenoter)
                    {
                        ReceivedCallback(s.Skip(1).ToArray());
                    }
                }
                _client.Recycle(inMessage);
            }
        }

        private void SendMessage(byte[] bytes)
        {
            NetOutgoingMessage outMessage = _client.CreateMessage();
            outMessage.Write(bytes);
            _client.SendMessage(outMessage, NetDeliveryMethod.ReliableOrdered);
            _client.FlushSendQueue();
        }

        public void Update(TimeSpan delta)
        {
            if (LatencyMonitor.HasFlag(LatencyMonitorMethod.Ping) && !_awaitingPingResponse && !_awaitingEchoResponse
                    && _now >= _pingSent + PingEveryMillis * TimeSpan.TicksPerMillisecond)
            {
                _awaitingPingResponse = true;
                _pingSent = _now;
                SendMessage(new[] { PingDenoter });
            }

            if (_awaitingEchoResponse && _now >= _toEchoSent + 5 * TimeSpan.TicksPerSecond
                || _awaitingPingResponse && _now >= _pingSent + 5 * TimeSpan.TicksPerSecond)
            {
                Latency = NO_RESPONSE;
                NoResponseCallback();
            }

            if (!_hasFinishedTryingToConnect && ConnectionSuccessful.HasValue)
                (ConnectionSuccessful.Value ? OnConnectionSuccess : OnConnectionFail)();
        }

        public void Send(byte[] bytes)
        {
            if (LatencyMonitor.HasFlag(LatencyMonitorMethod.Echo))
            {
                _awaitingEchoResponse = true;
                _toEchoSent = DateTimeOffset.UtcNow.Ticks;
            }
            SendMessage((LatencyMonitor.HasFlag(LatencyMonitorMethod.Echo) ? new[] { ToEchoDenoter } : new[] { NormalDenoter })
                .Concat(bytes).ToArray());
        }

        public void Dispose()
        {
            _client.Shutdown("Disconnect requested.");
        }
    }
}

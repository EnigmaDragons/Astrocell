using System.Collections.Generic;
using Lidgren.Network;
using System.Threading;
using System.Linq;
using System;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.Networking
{
    public class PeerToPeerHost : IMessenger, ILatencyMonitor, IAutomaton
    {
        private const byte NormalDenoter = 0;
        private const byte PingDenoter = 1;
        private const byte PongDenoter = 2;
        private const byte ToEchoDenoter = 3;
        private const byte EchoedDenoter = 4;

        public const long NOT_TESTED = -2;
        public const long NO_RESPONSE = -1;

        private NetPeerConfiguration _config { get { return _server.Configuration; } }
        private long _now => DateTimeOffset.UtcNow.Ticks;
        private NetServer _server;
        private long _toEchoSent = 0;
        private bool _awaitingEchoResponse = false;
        private long _pingSent = 0;
        private bool _awaitingPingResponse = false;
        private List<long> _lastConnections = new List<long> { };

        public LatencyMonitorMethod LatencyMonitor { get; set; }
        public int PingEveryMillis { get; set; } = 1000;
        public long Latency { get; private set; } = NOT_TESTED;
        public Action NoResponseCallback { private get; set; } = () => { };
        public Action<byte[]> ReceivedCallback { private get; set; } = (s) => { };
        public long UniqueIdentifier => _server.UniqueIdentifier;
        public List<long> Connections => _server.Connections.Select((c) => c.RemoteUniqueIdentifier).ToList();
        public Action<long> OnConnect { private get; set; } = (a) => { };
        public Action<long> OnDisconnect { private get; set; } = (a) => { };
        public Optional<bool> ConnectionSuccessful { get; private set; } = new Optional<bool>();
        public Action OnConnectionFail { private get; set; }
        public Action OnConnectionSuccess { private get; set; }

        private PeerToPeerHost() { }

        private void Init(int port, int maxConnections)
        {
            _server = new NetServer(new NetPeerConfiguration("chat") { Port = port, MaximumConnections = maxConnections });
            _server.RegisterReceivedCallback(new SendOrPostCallback((a) => GetNewMessages()));
            try
            {
                _server.Start();
            }
            catch
            {
                ConnectionSuccessful = new Optional<bool>(false);
                OnConnectionFail();
            }
            ConnectionSuccessful = new Optional<bool>(true);
            OnConnectionSuccess();
        }

        public static PeerToPeerHost CreateConnected(int port, int maxConnections)
        {
            return CreateConnected(port, maxConnections, (a) => { }, () => { });
        }

        public static PeerToPeerHost CreateConnected(int port, int maxConnections, Action<PeerToPeerHost> onConnectionSuccess,
            Action onConnectionFailed)
        {
            var host = new PeerToPeerHost();
            host.OnConnectionFail = onConnectionFailed;
            host.OnConnectionSuccess = () => onConnectionSuccess(host);
            host.Init(port, maxConnections);
            return host;
        }

        private void GetNewMessages()
        {
            NetIncomingMessage inMessage;
            while ((inMessage = _server.ReadMessage()) != null)
            {
                if (inMessage.MessageType == NetIncomingMessageType.Data)
                {
                    var s = inMessage.ReadBytes(inMessage.LengthBytes);
                    if (s[0] == PingDenoter)
                    {
                        SendMessage(new[] { PongDenoter }, new List<NetConnection>() { inMessage.SenderConnection });
                    }
                    else if (s[0] == PongDenoter)
                    {
                        Latency = _now - _pingSent;
                        _awaitingPingResponse = false;
                    }
                    else if (s[0] == ToEchoDenoter)
                    {
                        SendMessage(new[] { EchoedDenoter }, new List<NetConnection>() { inMessage.SenderConnection });
                        RelayAndRespondToMessage(inMessage, s);
                    }
                    else if (s[0] == EchoedDenoter)
                    {
                        Latency = _now - _toEchoSent;
                        _awaitingEchoResponse = false;
                    }
                    else if (s[0] == NormalDenoter)
                    {
                        RelayAndRespondToMessage(inMessage, s);
                    }
                }
                _server.Recycle(inMessage);
            }
        }
        
        private void RelayAndRespondToMessage(NetIncomingMessage im, byte[] s)
        {
            List<NetConnection> all = _server.Connections;
            all.Remove(im.SenderConnection);
            SendMessage(new byte[] { NormalDenoter }.Concat(s.Skip(1)), all);
            ReceivedCallback(s.Skip(1).ToArray());
        }
        
        private void SendMessage(IEnumerable<byte> bytes, List<NetConnection> connections)
        {
            if (connections.Count > 0)
            {
                NetOutgoingMessage message = _server.CreateMessage();
                message.Write(bytes.ToArray());
                _server.SendMessage(message, connections, NetDeliveryMethod.ReliableOrdered, 0);
            }
        }

        public void Update(TimeSpan delta)
        {
            if (LatencyMonitor.HasFlag(LatencyMonitorMethod.Ping) && !_awaitingPingResponse && !_awaitingEchoResponse
                    && _now >= _pingSent + PingEveryMillis * TimeSpan.TicksPerMillisecond)
            {
                _awaitingPingResponse = true;
                _pingSent = _now;
                SendMessage(new[] { PingDenoter }, _server.Connections);
            }

            if (_awaitingEchoResponse && _now >= _toEchoSent + 5 * TimeSpan.TicksPerSecond
                || _awaitingPingResponse && _now >= _pingSent + 5 * TimeSpan.TicksPerSecond)
            {
                Latency = NO_RESPONSE;
                NoResponseCallback();
            }
            
            for (var i = 0; i < _lastConnections.Count() ; i++)
                if (!Connections.Any((c) => c == _lastConnections[i]))
                    OnDisconnect(_lastConnections[i]);
            for (var i = 0; i < Connections.Count(); i++)
                if (!_lastConnections.Any((c) => c == Connections[i]))
                    OnConnect(Connections[i]);
        }
        
        public void Send(byte[] bytes)
        {
            List<NetConnection> all = _server.Connections;
            if (LatencyMonitor.HasFlag(LatencyMonitorMethod.Echo))
            {
                var randomConnection = all.Random();
                all.RemoveAll((c) => c.RemoteUniqueIdentifier == randomConnection.RemoteUniqueIdentifier);
                _awaitingEchoResponse = true;
                _toEchoSent = _now;
                SendMessage(new[] { ToEchoDenoter }.Concat(bytes), new List<NetConnection>() { randomConnection });
            }
            SendMessage(new[] { NormalDenoter }.Concat(bytes), all);
        }

        public void Dispose()
        {
            _server.Shutdown("Server Shutdown");
        }
    }
}

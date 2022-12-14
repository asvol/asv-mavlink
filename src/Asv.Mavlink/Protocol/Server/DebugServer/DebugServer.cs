using System;
using System.Threading;
using System.Threading.Tasks;
using Asv.Mavlink.V2.Common;


namespace Asv.Mavlink.Server
{
    public class DebugServer: IDebugServer,IDisposable
    {
        private readonly IMavlinkV2Connection _connection;
        private readonly IPacketSequenceCalculator _seq;
        private readonly MavlinkServerIdentity _identity;
        private readonly int _maxDebugFloatArrayNameLength = new DebugFloatArrayPayload().Name.Length;
        private readonly int _maxDebugFloatArrayDataLength = new DebugFloatArrayPayload().Data.Length;
        private readonly int _maxMemoryVectLength = new MemoryVectPayload().Value.Length;
        private readonly int _maxNamedValueKeyLength = new NamedValueFloatPayload().Name.Length;
        private readonly DateTime _bootTime;
        private readonly CancellationTokenSource _disposableCancel = new CancellationTokenSource();

        public DebugServer(IMavlinkV2Connection connection, IPacketSequenceCalculator seq, MavlinkServerIdentity identity)
        {
            _connection = connection;
            _seq = seq;
            _identity = identity;
            _bootTime = DateTime.Now;
        }

        public int MaxDebugFloatArrayNameLength => _maxDebugFloatArrayNameLength;
        public int MaxDebugFloatArrayDataLength => _maxDebugFloatArrayDataLength;
        public int MaxMemoryVectLength => _maxMemoryVectLength;
        public int MaxNamedValueKeyLength => _maxNamedValueKeyLength;


        public Task SendDebugFloatArray(string name, ushort arrayId, float[] data)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            if (name.Length > _maxDebugFloatArrayNameLength)
            {
                throw new ArgumentException($"Name '{name}' is too long for parameter name (max size {_maxDebugFloatArrayNameLength})", nameof(name));
            }

            if (data.Length > _maxDebugFloatArrayDataLength)
            {
                throw new ArgumentException($"Data is too long (max size {_maxDebugFloatArrayDataLength})", nameof(name));
            }

            var packet = new DebugFloatArrayPacket()
            {
                ComponenId = _identity.ComponentId,
                SystemId = _identity.SystemId,
                CompatFlags = 0,
                IncompatFlags = 0,
                Sequence = _seq.GetNextSequenceNumber(),
                Payload =
                {
                    Name = name.ToCharArray(),
                    TimeUsec = (uint)(DateTime.Now - _bootTime).TotalMilliseconds,
                    ArrayId = arrayId,
                }
            };
            data.CopyTo(packet.Payload.Data,0);
            return _connection.Send(packet, _disposableCancel.Token);
        }

        public Task SendMemoryVect(ushort address, byte version, byte type, sbyte[] value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.Length > _maxMemoryVectLength)
            {
                throw new ArgumentException($"Value '{value}' is too long (max size {_maxMemoryVectLength})", nameof(value));
            }
            var packet = new MemoryVectPacket
            {
                ComponenId = _identity.ComponentId,
                SystemId = _identity.SystemId,
                CompatFlags = 0,
                IncompatFlags = 0,
                Sequence = _seq.GetNextSequenceNumber(),
                Payload =
                {
                    Address = address,
                    Type = type,
                    Ver = version,
                }
            };
            value.CopyTo(packet.Payload.Value, 0);
            return _connection.Send(packet, _disposableCancel.Token);
        }

        public Task SendNamedValueFloat(string name, float value)
        {
            if (name.Length > _maxNamedValueKeyLength)
            {
                throw new ArgumentException($"Name '{name}' is too long for parameter name (max size {_maxNamedValueKeyLength})", nameof(name));
            }

            var packet = new NamedValueFloatPacket
            {
                ComponenId = _identity.ComponentId,
                SystemId = _identity.SystemId,
                CompatFlags = 0,
                IncompatFlags = 0,
                Sequence = _seq.GetNextSequenceNumber(),
                Payload =
                {
                    Name = name.ToCharArray(),
                    TimeBootMs = (uint)(DateTime.Now - _bootTime).TotalMilliseconds,
                    Value = value,
                }
            };
            return _connection.Send(packet, _disposableCancel.Token);
        }

        public Task SendNamedValueInteger(string name, int value)
        {
            if (name.Length > _maxNamedValueKeyLength)
            {
                throw new ArgumentException($"Name '{name}' is too long for parameter name (max size {_maxNamedValueKeyLength})", nameof(name));
            }

            var packet = new NamedValueIntPacket()
            {
                ComponenId = _identity.ComponentId,
                SystemId = _identity.SystemId,
                CompatFlags = 0,
                IncompatFlags = 0,
                Sequence = _seq.GetNextSequenceNumber(),
                Payload =
                {
                    Name = name.ToCharArray(),
                    TimeBootMs = (uint)(DateTime.Now - _bootTime).TotalMilliseconds,
                    Value = value,
                }
            };
            return _connection.Send(packet, _disposableCancel.Token);
        }

        public void Dispose()
        {
            _disposableCancel.Cancel(false);
            _disposableCancel.Dispose();
        }
    }
}

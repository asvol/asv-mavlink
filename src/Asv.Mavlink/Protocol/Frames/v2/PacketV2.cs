using System;
using System.Buffers;
using Asv.IO;

namespace Asv.Mavlink
{
    public abstract class PacketV2<TPayload> : IPacketV2<TPayload> where TPayload : IPayload
    {
        private readonly Signature _signature = new Signature();
        public byte Magic => PacketV2Helper.MagicMarkerV2;
        public abstract byte GetCrcEtra();
        public abstract int MessageId { get; }
        public byte IncompatFlags { get; set; }
        public byte CompatFlags { get; set; }
        public byte Sequence { get; set; }
        ///
        public byte SystemId { get; set; }
        public byte ComponenId { get; set; }
        public ushort FullId => (ushort)(ComponenId | SystemId << 8);
        public abstract TPayload Payload { get; }
        public int Size { get; set; }
        public abstract string Name { get; }

        public ISignature Signature => _signature;

        public int GetMaxByteSize() => Payload.GetMaxByteSize() + Signature.GetMaxByteSize() + 12;

        

        public void Serialize(ref Span<byte> buffer)
        {
            var fillBuffer = buffer.Slice(0);
            var payloadBuffer = buffer.Slice(10);
            var payloadSize = Payload.Serialize(ref payloadBuffer);
            // Debug.Assert(payloadSize <= byte.MaxValue, $"Wrong payload serialize size (must be {byte.MaxValue} size)");

            BinSerialize.WriteByte(ref fillBuffer, PacketV2Helper.MagicMarkerV2);
            BinSerialize.WriteByte(ref fillBuffer, (byte)payloadSize);
            BinSerialize.WriteByte(ref fillBuffer, IncompatFlags);
            BinSerialize.WriteByte(ref fillBuffer, CompatFlags);
            BinSerialize.WriteByte(ref fillBuffer, Sequence);
            BinSerialize.WriteByte(ref fillBuffer, SystemId);
            BinSerialize.WriteByte(ref fillBuffer, ComponenId);
            BinSerialize.WriteByte(ref fillBuffer, (byte)((MessageId) & 0xFF));
            BinSerialize.WriteByte(ref fillBuffer, (byte)((MessageId >> 8) & 0xFF));
            BinSerialize.WriteByte(ref fillBuffer, (byte)((MessageId >> 16) & 0xFF));
            var crcBuff = buffer.Slice(1,9);
            var crc = X25Crc.Accumulate(ref crcBuff, X25Crc.CrcSeed);
            crcBuff = buffer.Slice(10, payloadSize);
            crc = X25Crc.Accumulate(ref crcBuff, crc);
            crc = X25Crc.Accumulate(GetCrcEtra(), crc);
            fillBuffer = fillBuffer.Slice(payloadSize);
            BinSerialize.WriteUShort(ref fillBuffer, crc);
            if (Signature.IsPresent)
            {
                Signature.Serialize(ref fillBuffer);
            }
            // Debug.Assert((payloadSize + Signature.ByteSize + PacketV2Helper.PacketV2FrameSize) == (buffer.Length - fillBuffer.Length));
            buffer = fillBuffer;
            
        }

        public int Serialize(byte[] buffer, int offset)
        {
            PacketV2Helper.SetStx(buffer, offset);
            PacketV2Helper.SetIncompatFlags(buffer, offset, IncompatFlags);
            PacketV2Helper.SetCompatFlags(buffer, offset, CompatFlags);
            PacketV2Helper.SetSequence(buffer, offset, Sequence);
            PacketV2Helper.SetSystemId(buffer, offset, SystemId);
            PacketV2Helper.SetComponenId(buffer, offset, ComponenId);
            PacketV2Helper.SetMessageId(buffer, offset, MessageId);
            var index = offset + PacketV2Helper.PaylodStartIndexInFrame;
            var payloadSize = Payload.Serialize(buffer, index);
            // Debug.Assert(payloadSize <= byte.MaxValue,$"Wrong payload serialize size (must be {byte.MaxValue} size)");
            PacketV2Helper.SetPayloadSize(buffer, offset, (byte) payloadSize);
            PacketV2Helper.SetCrc(buffer, offset, GetCrcEtra());
            if (Signature.IsPresent)
            {
                _signature.Serialize(buffer, offset + payloadSize + PacketV2Helper.PacketV2FrameSize);
            }
            return payloadSize + Signature.ByteSize + PacketV2Helper.PacketV2FrameSize;
        }

        public void Deserialize(ref ReadOnlySpan<byte> buffer)
        {
            var crcBuffer = buffer.Slice(1);
            
            var stx = BinSerialize.ReadByte(ref buffer);
            if (stx != PacketV2Helper.MagicMarkerV2)
                throw new MavlinkException(string.Format(RS.WheelKnownConstant_VerifyStx_Unknown_STX_value, PacketV2Helper.MagicMarkerV2, stx));

            var payloadSize = BinSerialize.ReadByte(ref buffer);
            IncompatFlags = BinSerialize.ReadByte(ref buffer);
            CompatFlags = BinSerialize.ReadByte(ref buffer);
            Sequence = BinSerialize.ReadByte(ref buffer);
            SystemId = BinSerialize.ReadByte(ref buffer);
            ComponenId = BinSerialize.ReadByte(ref buffer);
            int messageId = BinSerialize.ReadByte(ref buffer);
            messageId |= BinSerialize.ReadByte(ref buffer) << 8;
            messageId |= BinSerialize.ReadByte(ref buffer) << 16;
            
            if (messageId != MessageId)
                throw new MavlinkException(string.Format(RS.PacketV2_Deserialize_Error_message_id_type, MessageId, messageId));
            
            var crcStartBuffer = buffer.Slice(payloadSize, 2);
            var crc = BinSerialize.ReadUShort(ref crcStartBuffer);

            var originSize = Payload.GetMinByteSize();
            if (payloadSize < originSize)
            {
                
                // this is Empty-Byte Payload Truncation https://mavlink.io/en/guide/serialization.html#payload_truncation
                var data = ArrayPool<byte>.Shared.Rent(originSize);
                var span = new Span<byte>(data,0,originSize);
                buffer.Slice(0, payloadSize).CopyTo(span);
                var zeroByteNotEnough = originSize - payloadSize;
                for (var i = 0; i < zeroByteNotEnough; i++)
                {
                    data[payloadSize + i] = 0;
                }

                try
                {
                    var readOnly = new ReadOnlySpan<byte>(data, 0, originSize);
                    Payload.Deserialize(ref readOnly, originSize);
                    // Debug.Assert(readOnly.Length == 0);
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(data);
                }
                
            }
            else
            {
                var payloadBuffer = buffer.Slice(0, payloadSize);
                Payload.Deserialize(ref payloadBuffer, payloadSize);
                //Debug.Assert(payloadBuffer.Length == 0);
            }
            
            var crcBuffer1 = crcBuffer.Slice(0, 9);
            var crcBuffer2 = crcBuffer.Slice(9, payloadSize);
            var calcCrc = X25Crc.Accumulate(ref crcBuffer1, X25Crc.CrcSeed);
            calcCrc = X25Crc.Accumulate(ref crcBuffer2, calcCrc);
            calcCrc = X25Crc.Accumulate(GetCrcEtra(), calcCrc);
            if (crc != calcCrc)
                throw new MavlinkException(string.Format(RS.PacketV2Helper_VerifyCrc_Bad_X25Crc, calcCrc, crc));

            if ((IncompatFlags & 0x01) != 0)
            {
                Signature.Deserialize(ref buffer);
            }
        }

        public int Deserialize(byte[] buffer, int offset)
        {
            PacketV2Helper.VerifyStx(buffer,offset);
            var payloadSize = PacketV2Helper.GetPayloadSize(buffer, offset);
            IncompatFlags = PacketV2Helper.GetIncompatFlags(buffer, offset);
            CompatFlags = PacketV2Helper.GetCompatFlags(buffer, offset);
            Sequence = PacketV2Helper.GetSequence(buffer, offset);
            SystemId = PacketV2Helper.GetSystemId(buffer, offset);
            ComponenId = PacketV2Helper.GetComponenId(buffer, offset);
            var messageId = PacketV2Helper.GetMessageId(buffer, offset);
            if (messageId != MessageId)
                throw new MavlinkException(string.Format(RS.PacketV2_Deserialize_Error_message_id_type, MessageId, messageId));
            Payload.Deserialize(buffer, offset + PacketV2Helper.PaylodStartIndexInFrame, payloadSize);
            PacketV2Helper.VerifyCrc(buffer,offset, GetCrcEtra());
            if (PacketV2Helper.CheckSignaturePresent(buffer, offset))
            {
                _signature.Deserialize(buffer, PacketV2Helper.GetSignatureStartIndex(buffer, offset));
            }
            return payloadSize + Signature.ByteSize + PacketV2Helper.PacketV2FrameSize;
        }

        public override string ToString()
        {
            return $"{Name.PadRight(30)}(" +
                   $"INC:{Convert.ToString(IncompatFlags, 2).PadLeft(8, '0')}|" +
                   $"COM:{Convert.ToString(CompatFlags, 2).PadLeft(8, '0')}|" +
                   $"SEQ:{Sequence:000}|" +
                   $"SYS:{SystemId:000}|" +
                   $"COM:{ComponenId:000}|" +
                   $"MSG:{MessageId:000000}|" +
                   $"{Payload.ToString()})";
        }

    }
}

using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Asv.IO;

namespace Asv.Mavlink
{
    

    public interface IMavlinkV2Connection:IObservable<IPacketV2<IPayload>>, IDisposable
    {
        long RxPackets { get; }
        long TxPackets { get; }
        long SkipPackets { get; }
        IObservable<DeserializePackageException> DeserializePackageErrors { get; }
        IObservable<IPacketV2<IPayload>> OnSendPacket { get; }
        Task Send(IPacketV2<IPayload> packet, CancellationToken cancel);
        IDataStream DataStream { get; }
    }

    public static class MavlinkV2Helper
    {

        /// <summary>
        /// Subscribe to connection packet pipe fore waiting answer packet and then send request
        /// </summary>
        /// <typeparam name="TAnswerPacket"></typeparam>
        /// <typeparam name="TAnswerPayload"></typeparam>
        /// <param name="src"></param>
        /// <param name="packet"></param>
        /// <param name="targetSystem"></param>
        /// <param name="targetComponent"></param>
        /// <param name="cancel"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static async Task<TAnswerPacket> SendAndWaitAnswer<TAnswerPacket, TAnswerPayload>(this IMavlinkV2Connection src, IPacketV2<IPayload> packet, int targetSystem, int targetComponent, CancellationToken cancel, Func<TAnswerPacket,bool> filter = null)
        where TAnswerPacket : IPacketV2<TAnswerPayload>, new() where TAnswerPayload : IPayload
        {
            var p = new TAnswerPacket();
            var tcs = new TaskCompletionSource<TAnswerPacket>();
            using var c1 = cancel.Register(()=>tcs.TrySetCanceled());
            filter = filter ?? (_ => true);
            using var subscribe = src.Where(_ => _.ComponenId == targetComponent && _.SystemId == targetSystem && _.MessageId == p.MessageId)
                .Cast<TAnswerPacket>()
                .FirstAsync(filter)
                .Subscribe(_=>tcs.TrySetResult(_));
            await src.Send(packet, cancel).ConfigureAwait(false);
            return await tcs.Task.ConfigureAwait(false);
        }
    }
}

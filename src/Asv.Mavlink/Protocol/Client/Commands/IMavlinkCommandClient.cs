using System.Threading;
using System.Threading.Tasks;
using Asv.Mavlink.V2.Common;

namespace Asv.Mavlink.Client
{
    public interface IMavlinkCommandClient
    {

        #region Commands

        /// <summary>
        /// 
        /// Send a command with up to seven parameters to the MAV
        /// </summary>
        /// <param name="command"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <param name="param4"></param>
        /// <param name="param5"></param>
        /// <param name="param6"></param>
        /// <param name="param7"></param>
        /// <param name="attemptCount"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<CommandAckPayload> CommandLong(MavCmd command, float param1, float param2, float param3, float param4, float param5, float param6, float param7, int attemptCount, CancellationToken cancel);

        /// <summary>
        /// Send a command with up to seven parameters to the MAV
        /// Don't wait answer
        /// </summary>
        /// <param name="command"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <param name="param4"></param>
        /// <param name="param5"></param>
        /// <param name="param6"></param>
        /// <param name="param7"></param>
        /// <param name="attemptCount"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task SendCommandLong(MavCmd command, float param1, float param2, float param3, float param4, float param5, float param6, float param7,  CancellationToken cancel);

        /// <summary>
        /// Message encoding a command with parameters as scaled integers. Scaling depends on the actual command value.
        ///  Don't wait answer
        /// </summary>
        /// <param name="command">The scheduled action for the mission item.</param>
        /// <param name="frame">The coordinate system of the COMMAND.</param>
        /// <param name="current"></param>
        /// <param name="autoContinue">autocontinue to next wp</param>
        /// <param name="param1">PARAM1, see MAV_CMD enum</param>
        /// <param name="param2">PARAM2, see MAV_CMD enum</param>
        /// <param name="param3">PARAM3, see MAV_CMD enum</param>
        /// <param name="param4">PARAM4, see MAV_CMD enum</param>
        /// <param name="x">PARAM5 / local: x position in meters * 1e4, global: latitude in degrees * 10^7</param>
        /// <param name="y">PARAM6 / local: y position in meters * 1e4, global: longitude in degrees * 10^7</param>
        /// <param name="z">PARAM7 / z position: global: altitude in meters (relative or absolute, depending on frame).</param>
        /// <param name="attemptCount"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task SendCommandInt(MavCmd command, MavFrame frame, bool current, bool autocontinue,
            float param1, float param2,
            float param3, float param4, int x, int y, float z, int attemptCount, CancellationToken cancel);

        /// <summary>
        /// Message encoding a command with parameters as scaled integers. Scaling depends on the actual command value.
        /// </summary>
        /// <param name="command">The scheduled action for the mission item.</param>
        /// <param name="frame">The coordinate system of the COMMAND.</param>
        /// <param name="current"></param>
        /// <param name="autoContinue">autocontinue to next wp</param>
        /// <param name="param1">PARAM1, see MAV_CMD enum</param>
        /// <param name="param2">PARAM2, see MAV_CMD enum</param>
        /// <param name="param3">PARAM3, see MAV_CMD enum</param>
        /// <param name="param4">PARAM4, see MAV_CMD enum</param>
        /// <param name="x">PARAM5 / local: x position in meters * 1e4, global: latitude in degrees * 10^7</param>
        /// <param name="y">PARAM6 / local: y position in meters * 1e4, global: longitude in degrees * 10^7</param>
        /// <param name="z">PARAM7 / z position: global: altitude in meters (relative or absolute, depending on frame).</param>
        /// <param name="attemptCount"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<CommandAckPayload> CommandInt(MavCmd command, MavFrame frame, bool current, bool autoContinue, float param1, float param2, float param3, float param4, int x, int y, float z, int attemptCount, CancellationToken cancel);

        #endregion

        Task<AutopilotVersionPayload> GetAutopilotVersion(CancellationToken cancel);



    }
}

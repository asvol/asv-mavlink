﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Asv.Mavlink {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class RS {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal RS() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Asv.Mavlink.RS", typeof(RS).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error for deserialize mavlink V2 package with messageID {0}: {1}.
        /// </summary>
        public static string DecoderV2_TryDecodePacket_Error_for_deserialize_mavlink_V2 {
            get {
                return ResourceManager.GetString("DecoderV2_TryDecodePacket_Error_for_deserialize_mavlink_V2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to At this confidence level, positional measurements are considered accurate enough to meet all but the most sensitive applications..
        /// </summary>
        public static string GpsInfoHelper_GetDescription_Excellent {
            get {
                return ResourceManager.GetString("GpsInfoHelper_GetDescription_Excellent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Represents a low confidence level. Positional measurements should be discarded or used only to indicate a very rough estimate of the current location..
        /// </summary>
        public static string GpsInfoHelper_GetDescription_Fair {
            get {
                return ResourceManager.GetString("GpsInfoHelper_GetDescription_Fair", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Represents a level that marks the minimum appropriate for making accurate decisions. Positional measurements could be used to make reliable in-route navigation suggestions to the user..
        /// </summary>
        public static string GpsInfoHelper_GetDescription_Good {
            get {
                return ResourceManager.GetString("GpsInfoHelper_GetDescription_Good", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Highest possible confidence level to be used for applications demanding the highest possible precision at all times..
        /// </summary>
        public static string GpsInfoHelper_GetDescription_Ideal {
            get {
                return ResourceManager.GetString("GpsInfoHelper_GetDescription_Ideal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Positional measurements could be used for calculations, but the fix quality could still be improved. A more open view of the sky is recommended..
        /// </summary>
        public static string GpsInfoHelper_GetDescription_Moderate {
            get {
                return ResourceManager.GetString("GpsInfoHelper_GetDescription_Moderate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to At this level, measurements are inaccurate by as much as 300 meters with a 6-meter accurate device (50 DOP × 6 meters) and should be discarded..
        /// </summary>
        public static string GpsInfoHelper_GetDescription_Poor {
            get {
                return ResourceManager.GetString("GpsInfoHelper_GetDescription_Poor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown.
        /// </summary>
        public static string GpsInfoHelper_GetDescription_Unknown {
            get {
                return ResourceManager.GetString("GpsInfoHelper_GetDescription_Unknown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Excellent.
        /// </summary>
        public static string GpsInfoHelper_GetDisplayName_Excellent {
            get {
                return ResourceManager.GetString("GpsInfoHelper_GetDisplayName_Excellent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fair.
        /// </summary>
        public static string GpsInfoHelper_GetDisplayName_Fair {
            get {
                return ResourceManager.GetString("GpsInfoHelper_GetDisplayName_Fair", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Good.
        /// </summary>
        public static string GpsInfoHelper_GetDisplayName_Good {
            get {
                return ResourceManager.GetString("GpsInfoHelper_GetDisplayName_Good", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ideal.
        /// </summary>
        public static string GpsInfoHelper_GetDisplayName_Ideal {
            get {
                return ResourceManager.GetString("GpsInfoHelper_GetDisplayName_Ideal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Moderate.
        /// </summary>
        public static string GpsInfoHelper_GetDisplayName_Moderate {
            get {
                return ResourceManager.GetString("GpsInfoHelper_GetDisplayName_Moderate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Poor.
        /// </summary>
        public static string GpsInfoHelper_GetDisplayName_Poor {
            get {
                return ResourceManager.GetString("GpsInfoHelper_GetDisplayName_Poor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mavlink packet version v2 with MessageId=&apos;{0}&apos; not found in decoder.
        /// </summary>
        public static string MessageIdNotFoundException_MessageIdNotFoundException {
            get {
                return ResourceManager.GetString("MessageIdNotFoundException_MessageIdNotFoundException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error message id type. Want {0}, Got {1}.
        /// </summary>
        public static string PacketV2_Deserialize_Error_message_id_type {
            get {
                return ResourceManager.GetString("PacketV2_Deserialize_Error_message_id_type", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bad X25Crc: want {0}, got {1}.
        /// </summary>
        public static string PacketV2Helper_VerifyCrc_Bad_X25Crc {
            get {
                return ResourceManager.GetString("PacketV2Helper_VerifyCrc_Bad_X25Crc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Payload.
        /// </summary>
        public static string PayloadDiagnosticRtt_PayloadDiagnosticRtt_Payload {
            get {
                return ResourceManager.GetString("PayloadDiagnosticRtt_PayloadDiagnosticRtt_Payload", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Connection string &apos;{0}&apos; is invalid.
        /// </summary>
        public static string RemoteStreamFactory_CreateStream_Connection_string_is_invalid {
            get {
                return ResourceManager.GetString("RemoteStreamFactory_CreateStream_Connection_string_is_invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Integer value not assigned for param &apos;{0}&apos; with type {1}.
        /// </summary>
        public static string Vehicle_ConvertToMavlinkUnionToParamValue_Integer_value_not_assigned_for_param {
            get {
                return ResourceManager.GetString("Vehicle_ConvertToMavlinkUnionToParamValue_Integer_value_not_assigned_for_param", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Author of this library doesn&apos;t know, how how to read 8 byte from float (4 byte) field.
        /// </summary>
        public static string Vehicle_ConvertToMavlinkUnionToParamValue_NeedMoreByte {
            get {
                return ResourceManager.GetString("Vehicle_ConvertToMavlinkUnionToParamValue_NeedMoreByte", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Real value not assigned for param &apos;{0}&apos; with type {1}.
        /// </summary>
        public static string Vehicle_ConvertToMavlinkUnionToParamValue_Real_value_not_assigned_for_param {
            get {
                return ResourceManager.GetString("Vehicle_ConvertToMavlinkUnionToParamValue_Real_value_not_assigned_for_param", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Timeout ({0} ms) to read all params from Vehicle..
        /// </summary>
        public static string Vehicle_ReadAllParams_Timeout_to_read_all_params_from_Vehicle {
            get {
                return ResourceManager.GetString("Vehicle_ReadAllParams_Timeout_to_read_all_params_from_Vehicle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Altitude of end point is null.
        /// </summary>
        public static string VehicleArdupilot_GoTo_Altitude_of_end_point_is_null {
            get {
                return ResourceManager.GetString("VehicleArdupilot_GoTo_Altitude_of_end_point_is_null", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Altitude of home position is null.
        /// </summary>
        public static string VehicleArdupilotPlane_InternalGoToGlob_Altitude_of_home_position_is_null {
            get {
                return ResourceManager.GetString("VehicleArdupilotPlane_InternalGoToGlob_Altitude_of_home_position_is_null", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Timeout to execute &apos;{0}&apos; (wait {1:g} time).
        /// </summary>
        public static string VehicleBase_WaitCompleteWithDefaultTimeout_Timeout_to_execute {
            get {
                return ResourceManager.GetString("VehicleBase_WaitCompleteWithDefaultTimeout_Timeout_to_execute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown STX value. Need {0}. Get {1}.
        /// </summary>
        public static string WheelKnownConstant_VerifyStx_Unknown_STX_value {
            get {
                return ResourceManager.GetString("WheelKnownConstant_VerifyStx_Unknown_STX_value", resourceCulture);
            }
        }
    }
}
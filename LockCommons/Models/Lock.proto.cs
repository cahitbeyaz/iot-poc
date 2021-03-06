﻿// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: lock.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace LockCommons.Models.Proto
{

    /// <summary>Holder for reflection information generated from lock.proto</summary>
    public static partial class LockReflection
    {

        #region Descriptor
        /// <summary>File descriptor for lock.proto</summary>
        public static pbr::FileDescriptor Descriptor
        {
            get { return descriptor; }
        }
        private static pbr::FileDescriptor descriptor;

        static LockReflection()
        {
            byte[] descriptorData = global::System.Convert.FromBase64String(
                string.Concat(
                  "Cgpsb2NrLnByb3RvGh9nb29nbGUvcHJvdG9idWYvdGltZXN0YW1wLnByb3Rv",
                  "It8BCglMb2NrRXZlbnQSFAoMTG9ja0RldmljZUlkGAEgASgJEi0KCUV2ZW50",
                  "VGltZRgCIAEoCzIaLmdvb2dsZS5wcm90b2J1Zi5UaW1lc3RhbXASLwoLRGV2",
                  "aWNlRXZlbnQYAyABKA4yGi5Mb2NrRXZlbnQuRGV2aWNlRXZlbnRFbnVtEh4K",
                  "FlJlcXVlc3RSZWZlcmVuY2VOdW1iZXIYBCABKAkiPAoPRGV2aWNlRXZlbnRF",
                  "bnVtEggKBE5vbmUQABIICgRPcGVuEAESCQoFQ2xvc2UQAhIKCgZUYW1wZXIQ",
                  "A0IbqgIYTG9ja0NvbW1vbnMuTW9kZWxzLlByb3RvYgZwcm90bzM="));
            descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
                new pbr::FileDescriptor[] { global::Google.Protobuf.WellKnownTypes.TimestampReflection.Descriptor, },
                new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::LockCommons.Models.Proto.LockEvent), global::LockCommons.Models.Proto.LockEvent.Parser, new[]{ "LockDeviceId", "EventTime", "DeviceEvent", "RequestReferenceNumber" }, null, new[]{ typeof(global::LockCommons.Models.Proto.LockEvent.Types.DeviceEventEnum) }, null)
                }));
        }
        #endregion

    }
    #region Messages
    public sealed partial class LockEvent : pb::IMessage<LockEvent>
    {
        private static readonly pb::MessageParser<LockEvent> _parser = new pb::MessageParser<LockEvent>(() => new LockEvent());
        private pb::UnknownFieldSet _unknownFields;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<LockEvent> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor
        {
            get { return global::LockCommons.Models.Proto.LockReflection.Descriptor.MessageTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor
        {
            get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public LockEvent()
        {
            OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public LockEvent(LockEvent other) : this()
        {
            lockDeviceId_ = other.lockDeviceId_;
            eventTime_ = other.eventTime_ != null ? other.eventTime_.Clone() : null;
            deviceEvent_ = other.deviceEvent_;
            requestReferenceNumber_ = other.requestReferenceNumber_;
            _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public LockEvent Clone()
        {
            return new LockEvent(this);
        }

        /// <summary>Field number for the "LockDeviceId" field.</summary>
        public const int LockDeviceIdFieldNumber = 1;
        private string lockDeviceId_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string LockDeviceId
        {
            get { return lockDeviceId_; }
            set
            {
                lockDeviceId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
            }
        }

        /// <summary>Field number for the "EventTime" field.</summary>
        public const int EventTimeFieldNumber = 2;
        private global::Google.Protobuf.WellKnownTypes.Timestamp eventTime_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public global::Google.Protobuf.WellKnownTypes.Timestamp EventTime
        {
            get { return eventTime_; }
            set
            {
                eventTime_ = value;
            }
        }

        /// <summary>Field number for the "DeviceEvent" field.</summary>
        public const int DeviceEventFieldNumber = 3;
        private global::LockCommons.Models.Proto.LockEvent.Types.DeviceEventEnum deviceEvent_ = 0;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public global::LockCommons.Models.Proto.LockEvent.Types.DeviceEventEnum DeviceEvent
        {
            get { return deviceEvent_; }
            set
            {
                deviceEvent_ = value;
            }
        }

        /// <summary>Field number for the "RequestReferenceNumber" field.</summary>
        public const int RequestReferenceNumberFieldNumber = 4;
        private string requestReferenceNumber_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string RequestReferenceNumber
        {
            get { return requestReferenceNumber_; }
            set
            {
                requestReferenceNumber_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override bool Equals(object other)
        {
            return Equals(other as LockEvent);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(LockEvent other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            if (ReferenceEquals(other, this))
            {
                return true;
            }
            if (LockDeviceId != other.LockDeviceId) return false;
            if (!object.Equals(EventTime, other.EventTime)) return false;
            if (DeviceEvent != other.DeviceEvent) return false;
            if (RequestReferenceNumber != other.RequestReferenceNumber) return false;
            return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode()
        {
            int hash = 1;
            if (LockDeviceId.Length != 0) hash ^= LockDeviceId.GetHashCode();
            if (eventTime_ != null) hash ^= EventTime.GetHashCode();
            if (DeviceEvent != 0) hash ^= DeviceEvent.GetHashCode();
            if (RequestReferenceNumber.Length != 0) hash ^= RequestReferenceNumber.GetHashCode();
            if (_unknownFields != null)
            {
                hash ^= _unknownFields.GetHashCode();
            }
            return hash;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override string ToString()
        {
            return pb::JsonFormatter.ToDiagnosticString(this);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void WriteTo(pb::CodedOutputStream output)
        {
            if (LockDeviceId.Length != 0)
            {
                output.WriteRawTag(10);
                output.WriteString(LockDeviceId);
            }
            if (eventTime_ != null)
            {
                output.WriteRawTag(18);
                output.WriteMessage(EventTime);
            }
            if (DeviceEvent != 0)
            {
                output.WriteRawTag(24);
                output.WriteEnum((int)DeviceEvent);
            }
            if (RequestReferenceNumber.Length != 0)
            {
                output.WriteRawTag(34);
                output.WriteString(RequestReferenceNumber);
            }
            if (_unknownFields != null)
            {
                _unknownFields.WriteTo(output);
            }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize()
        {
            int size = 0;
            if (LockDeviceId.Length != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(LockDeviceId);
            }
            if (eventTime_ != null)
            {
                size += 1 + pb::CodedOutputStream.ComputeMessageSize(EventTime);
            }
            if (DeviceEvent != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeEnumSize((int)DeviceEvent);
            }
            if (RequestReferenceNumber.Length != 0)
            {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(RequestReferenceNumber);
            }
            if (_unknownFields != null)
            {
                size += _unknownFields.CalculateSize();
            }
            return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(LockEvent other)
        {
            if (other == null)
            {
                return;
            }
            if (other.LockDeviceId.Length != 0)
            {
                LockDeviceId = other.LockDeviceId;
            }
            if (other.eventTime_ != null)
            {
                if (eventTime_ == null)
                {
                    eventTime_ = new global::Google.Protobuf.WellKnownTypes.Timestamp();
                }
                EventTime.MergeFrom(other.EventTime);
            }
            if (other.DeviceEvent != 0)
            {
                DeviceEvent = other.DeviceEvent;
            }
            if (other.RequestReferenceNumber.Length != 0)
            {
                RequestReferenceNumber = other.RequestReferenceNumber;
            }
            _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(pb::CodedInputStream input)
        {
            uint tag;
            while ((tag = input.ReadTag()) != 0)
            {
                switch (tag)
                {
                    default:
                        _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
                        break;
                    case 10:
                        {
                            LockDeviceId = input.ReadString();
                            break;
                        }
                    case 18:
                        {
                            if (eventTime_ == null)
                            {
                                eventTime_ = new global::Google.Protobuf.WellKnownTypes.Timestamp();
                            }
                            input.ReadMessage(eventTime_);
                            break;
                        }
                    case 24:
                        {
                            deviceEvent_ = (global::LockCommons.Models.Proto.LockEvent.Types.DeviceEventEnum)input.ReadEnum();
                            break;
                        }
                    case 34:
                        {
                            RequestReferenceNumber = input.ReadString();
                            break;
                        }
                }
            }
        }

        #region Nested types
        /// <summary>Container for nested types declared in the LockEvent message type.</summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static partial class Types
        {
            public enum DeviceEventEnum
            {
                [pbr::OriginalName("None")] None = 0,
                [pbr::OriginalName("Open")] Open = 1,
                [pbr::OriginalName("Close")] Close = 2,
                [pbr::OriginalName("Tamper")] Tamper = 3,
                [pbr::OriginalName("Hearbeat")] Heartbeat = 4,
            }

        }
        #endregion

    }

    #endregion

}

#endregion Designer generated code

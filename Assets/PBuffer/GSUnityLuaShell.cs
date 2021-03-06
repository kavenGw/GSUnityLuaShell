// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: GSUnityLuaShell.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from GSUnityLuaShell.proto</summary>
public static partial class GSUnityLuaShellReflection {

  #region Descriptor
  /// <summary>File descriptor for GSUnityLuaShell.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static GSUnityLuaShellReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "ChVHU1VuaXR5THVhU2hlbGwucHJvdG8iKwoXR1NVbml0eUx1YVNoZWxsQ29t",
          "bWFuZHMSEAoIQ29tbWFuZHMYASADKAliBnByb3RvMw=="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::GSUnityLuaShellCommands), global::GSUnityLuaShellCommands.Parser, new[]{ "Commands" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class GSUnityLuaShellCommands : pb::IMessage<GSUnityLuaShellCommands> {
  private static readonly pb::MessageParser<GSUnityLuaShellCommands> _parser = new pb::MessageParser<GSUnityLuaShellCommands>(() => new GSUnityLuaShellCommands());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<GSUnityLuaShellCommands> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::GSUnityLuaShellReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public GSUnityLuaShellCommands() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public GSUnityLuaShellCommands(GSUnityLuaShellCommands other) : this() {
    commands_ = other.commands_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public GSUnityLuaShellCommands Clone() {
    return new GSUnityLuaShellCommands(this);
  }

  /// <summary>Field number for the "Commands" field.</summary>
  public const int CommandsFieldNumber = 1;
  private static readonly pb::FieldCodec<string> _repeated_commands_codec
      = pb::FieldCodec.ForString(10);
  private readonly pbc::RepeatedField<string> commands_ = new pbc::RepeatedField<string>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::RepeatedField<string> Commands {
    get { return commands_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as GSUnityLuaShellCommands);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(GSUnityLuaShellCommands other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if(!commands_.Equals(other.commands_)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    hash ^= commands_.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    commands_.WriteTo(output, _repeated_commands_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    size += commands_.CalculateSize(_repeated_commands_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(GSUnityLuaShellCommands other) {
    if (other == null) {
      return;
    }
    commands_.Add(other.commands_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          commands_.AddEntriesFrom(input, _repeated_commands_codec);
          break;
        }
      }
    }
  }

}

#endregion


#endregion Designer generated code

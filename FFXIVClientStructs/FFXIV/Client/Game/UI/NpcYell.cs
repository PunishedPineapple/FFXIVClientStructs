using FFXIVClientStructs.FFXIV.Client.Game.Object;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;

namespace FFXIVClientStructs.FFXIV.Client.Game.UI;

// Client::Game::UI::NpcYell
[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 0x2850)]
public partial struct NpcYell {

    [FieldOffset(0x48), FixedSizeArray] internal FixedSizeArray32<NpcYellSpeakerInfo> _speakerInfo;
    [FieldOffset(0x548), FixedSizeArray] internal FixedSizeArray32<NpcYellInfo> _yells;
    [FieldOffset(0x2848)] private short Unk_2848; // Probably number of unprocessed yells.

    [StructLayout(LayoutKind.Explicit, Size = 0x28)]
    public struct NpcYellSpeakerInfo {
        [FieldOffset(0x0)] public uint EntityId; // Might be a full GameObjectId, uncertain.
        [FieldOffset(0xC)] public uint NameId;
        [FieldOffset(0x10)] private uint Unk_10; // Probably some kind of ID.  Lines up with the YellInfo field at offset 0xE8.
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x118)]
    public struct NpcYellInfo {
        [FieldOffset(0x0)] public uint NpcYellRowId;
        [FieldOffset(0x8)] public uint EntityId; // Might be a full GameObjectId, uncertain.
        [FieldOffset(0x10)] public uint NameId;
        [FieldOffset(0x18)] public Utf8String Name;
        [FieldOffset(0x80)] public Utf8String Message;
        [FieldOffset(0xE8)] private uint Unk_E8; // Probably some kind of ID.  Lines up with the SpeakerInfo field at offset 0x10.
        [FieldOffset(0xFC)] public float BalloonTime;
        [FieldOffset(0x100)] public float BattleTalkTime;
        [FieldOffset(0x104)] public NpcYellOutputFlags OutputType;
        [FieldOffset(0x10C)] public byte ParentBone;
        [FieldOffset(0x110)] public NpcYellFlags Flags;
    }

    [Flags]
    public enum NpcYellOutputFlags : byte {
        None = 0,
        PrintToLog = 1,
        ShowBalloon = 2,
        ShowBattleTalk = 4,
    }
    
    [Flags]
    public enum NpcYellFlags : byte {
        None = 0,
        SkipCloseChecks = 1,
        SkipRangeCheck = 2, // Also checked when formatting NPC name.
        Unk_4 = 4,
        Unk_8 = 8,
        Unk_10 = 0x10, // This is the default value after initialization.  Unclear what it does.
        Unk_20 = 0x20,
        Unk_40 = 0x40,
    }
}

using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using FFXIVClientStructs.FFXIV.Client.System.String;

namespace FFXIVClientStructs.FFXIV.Client.UI.Agent;

// Client::UI::Agent::AgentScreenLog
//   Client::UI::Agent::AgentInterface
//     Component::GUI::AtkModuleInterface::AtkEventInterface
[Agent(AgentId.ScreenLog)]
[GenerateInterop]
[Inherits<AgentInterface>]
[StructLayout(LayoutKind.Explicit, Size = 0x400)]
public unsafe partial struct AgentScreenLog {

    [FieldOffset(0x30)] public long TimeSinceSomething_mSec;
    [FieldOffset(0x38)] public void* Unk_38;
    [FieldOffset(0x40)] public uint NumTargetableCharactersInRangeInFoV;  // Caps at eight.
    [FieldOffset(0x48)] public int Unk_48;  // Rarely goes above 255.
    [FieldOffset(0x4C)] public float Unk_4C;    // Have only seen it be exactly 1.0;

    [FieldOffset(0x50), FixedSizeArray] internal FixedSizeArray20<Unk50Struct> _unk_60;

    [FieldOffset(0x338)] public long AlsoTimeSinceSomething_mSec;   // Seems to match 0x30
    [FieldOffset(0x350)] public StdDeque<BalloonInfo> BalloonQueue;
    //[FieldOffset(0x350)] public void* ContainerBase; // iterator base nonsense
    //[FieldOffset(0x358)] public T** Map; // pointer to array of pointers (size MapSize) to arrays of T (size BlockSize)
    //[FieldOffset(0x360)] public ulong MapSize; // size of map
    //[FieldOffset(0x368)] public ulong MyOff; // offset of current first element
    //[FieldOffset(0x370)] public ulong MySize; // current length
    [FieldOffset(0x378)] public bool BalloonsHaveUpdate; // bool used to know if any balloons have been added/changed since last frame update
    [FieldOffset(0x37C)] public int BalloonCounter; // count of all balloons since changing areas, used as unique balloon ID

    [FieldOffset(0x390), FixedSizeArray] internal FixedSizeArray10<BalloonSlot> _balloonSlots;
    [FieldOffset(0x3E0)] public byte NumSlotsAvailable;
    [FieldOffset(0x3F0)] public UIModule* UIModule;

    [MemberFunction("E8 ?? ?? ?? ?? F6 86 ?? ?? ?? ?? ?? C7 46")]
    public partial void OpenBalloon(Character* chara, CStringPointer str, bool slowly, int parentBone);
}

[StructLayout(LayoutKind.Explicit, Size = 0x20)]
public unsafe struct Unk50Struct {
    [FieldOffset(0x0)] public void* Unk_0; // Something on the heap.
    [FieldOffset(0x8)] public void* Unk_8; // Something on the heap.
    [FieldOffset(0x10)] public float X;  // Maybe
    [FieldOffset(0x14)] public float Y;  // Maybe
    [FieldOffset(0x18)] public float Z;  // Maybe
}

[StructLayout(LayoutKind.Explicit, Size = 0xF0)]
public unsafe struct BalloonInfo {
    [FieldOffset(0x0)] public Utf8String FormattedText; // Contains breaks for newlines
    [FieldOffset(0x68)] public Utf8String OriginalText;
    [FieldOffset(0xD0)] public GameObjectId ObjectId;
    [FieldOffset(0xD8)] public Character* Character; // this keeps getting changed between null and the character pointer so isn't entirely reliable
    [FieldOffset(0xE0)] public float CameraDistance;
    [FieldOffset(0xE4)] public int BalloonId; // matches BalloonCounter when the balloon is made
    [FieldOffset(0xE8)] public ushort ParentBone;
    [FieldOffset(0xEA)] public bool Unk_EA; // Is assigned the value of the third parameter of OpenBalloon.
    [FieldOffset(0xEB)] public byte BalloonSlotIndex;
    [FieldOffset(0xEC)] public bool Unk_EC; // Based upon something in chara+0x1F0.
}

// not sure how this maps to the addon yet, might just be in order though
[StructLayout(LayoutKind.Explicit, Size = 0x8)]
public struct BalloonSlot {
    [FieldOffset(0x0)] public int Id;
    [FieldOffset(0x4)] public bool Available;
}

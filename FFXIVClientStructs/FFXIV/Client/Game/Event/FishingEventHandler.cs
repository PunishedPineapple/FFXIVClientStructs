using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using InteropGenerator.Runtime.Attributes;

using static FFXIVClientStructs.FFXIV.Component.GUI.AtkModuleInterface;

namespace FFXIVClientStructs.FFXIV.Client.Game.Event;

// Client::Game::Event::FishingEventHandler
//   Client::Game::Event::EventHandler
//   Component::GUI::AtkModuleInterface::AtkEventInterface
[GenerateInterop]
[Inherits<EventHandler>, Inherits<AtkEventInterface>]
[StructLayout(LayoutKind.Explicit, Size = 0x290)]
public unsafe partial struct FishingEventHandler {
    [FieldOffset(0x220)] public ulong Unk_220; // Have not seen it be anything besides zero.
    [FieldOffset(0x228)] public FishingState FishingState;
    [FieldOffset(0x22C)] public float Heartbeat; // 4 Hz inverse sawtooth between one and zero.  Latches upon cast.  No clue what it is for.
    [FieldOffset(0x230)] public bool AtFishingHole; // Only updates when on FSH.
    [FieldOffset(0x231)] public bool CanMoochPreviousCatch; // Returns to false when opportunity is gone (i.e., Spareful Hand is used).
    [FieldOffset(0x232)] public bool CanMooch2PreviousCatch; // Returns to false when opportunity is gone (i.e., 15s elapsed).  Ignores skill cooldown.
    [FieldOffset(0x233)] public bool CanReleasePreviousCatch;
    [FieldOffset(0x234)] public bool ChangingPosition; // True while in the process of sitting down or standing up.
    [FieldOffset(0x235)] public bool CanIdenticalCastPreviousCatch;
    [FieldOffset(0x236)] public bool CanSurfaceSlapPreviousCatch;
    [FieldOffset(0x237)] public bool Unk_237;    //	Never seen false.
    [FieldOffset(0x238)] public FishingBaitFlags CurrentCastBaitFlags;
    [FieldOffset(0x23C)] public sbyte SelectedSwimBaitIndex; // -1 when none selected.
    [FieldOffset(0x240), FixedSizeArray] internal FixedSizeArray3<uint> _swimBaitItemIDs;
    [FieldOffset(0x24C)] public uint Unk_24C;
    [FieldOffset(0x250)] public long MoochOpportunityExpirationTimestamp_mSec; // Unix timestamp in milliseconds for when the current Mooch/Spareful Hand opportunity will cease being available.
    [FieldOffset(0x258)] public long CatchActionExpirationTimestamp_mSec; // Unix timestamp in milliseconds for when actions like Surface Slap will cease being available.

    // An instance of something with a vtable right next to this event handler's.  Probably 0x30 bytes long, destructor vfunc 2.
    //[FieldOffset( 0x260 )] public void* pUnk_260;
    //[FieldOffset( 0x268 )] public ulong Unk_268;
    //[FieldOffset( 0x270 )] public FishingEventHandler* FishingEventHandlerInstance;
    //[FieldOffset( 0x278 )] public ulong Unk_278;
    //[FieldOffset( 0x280 )] public int Unk_280;
    //[FieldOffset( 0x284 )] public int Unk_284;
    //[FieldOffset( 0x288 )] public ulong Unk_288;
}

[Flags]
public enum FishingBaitFlags : int	// Numbered like flags, but none of the ones that I have seen can be combined.
{
    Normal = 0,
    AmbitiousLure = 0x1,
    ModestLure = 0x2,
    Mooch = 0x10,
    Swimbait = 0x20,
}

public enum FishingState : int {
    None = 0,
    CastingOut = 1,
    PullingPoleIn = 2,      //	When fish slips, there is no bite, briefly after reeling in a fish, and when using Rest.
    Quitting = 3,
    PoleReady = 4,          //	The standby "gathering" condition.
    Bite = 5,
    Hooking = 6,            //	Includes the subsequent reeling in.
    ReleasingCatch = 7,
    ConfirmingCollectable = 8,
    AmbitiousLure = 9,      //	When using the skill.
    ModestLure = 10,        //	When using the skill.

    LineInWater = 12,		//	Or air, sand, etc.; just when you are actually fishing.
}

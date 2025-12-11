using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using FFXIVClientStructs.FFXIV.Client.System.String;

namespace FFXIVClientStructs.FFXIV.Client.Game;

// Client::Game::NPCYellBalloon
// Probably part of Client::Game::Character::Character
[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 0x88)]    //***** TEMP: Likely correct size since it would fit perfectly between Balloon and Alpha fields in Character.
public unsafe partial struct NPCYellBalloon {
    [FieldOffset(0x0)] public Utf8String FormattedText;
    [FieldOffset(0x68)] public Character.Character* Character;
    [FieldOffset(0x70)] public float PlayTimer; // Set to starting value during Initializing state, starts counting down in Active state.
    [FieldOffset(0x74)] public float DelayTime; // Will sit in the Waiting state for this long before displaying; however, if this is zero when entering the waiting state, a time of one frame will be used.
    [FieldOffset(0x78)] public NPCYellBalloonState State;
    [FieldOffset(0x7C)] public float Unk_7C; // Gets checked for NaN during update while in Active state and equality to zero while in Activating state.
    [FieldOffset(0x80)] public byte ParentBone;
    [FieldOffset(0x81)] public NPCYellBalloonFlags Flags;

    [MemberFunction("E8 ?? ?? ?? ?? 0F BE 4B 44")]
    public partial NPCYellBalloon* Ctor();

    [MemberFunction("E8 ?? ?? ?? ?? 48 8D 8B ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8D 8B ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8D 35")]
    public partial void Dtor();

    /// <summary>
    /// Sets suitable default inactive values.
    /// </summary>
    /// <param name="character">This is expected by the game to be the address of the <see cref="Character.Character"/> to which this balloon info is attached.</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 89 51 ?? 33 FF")]
    public partial void Initialize(Character.Character* character);

    /// <summary>
    /// Prepares a balloon to be opened during the next applicable <see cref="Update"/>.
    /// </summary>
    /// <param name="str">A null-terminated string containing the text to display.</param>
    /// <param name="playTime">Time in seconds that the balloon should remain visible.</param>
    /// <param name="param3">Unknown purpose.  Corresponds to <see cref="NPCYellBalloonFlags.Unk_2"/></param>
    /// <param name="openDelay">Time in seconds to wait before opening the balloon.</param>
    /// <param name="printToLog">Whether the balloon text should also be printed to the chat log.</param>
    /// <param name="param7">Unknown purpose.</param>
    /// <param name="ignoreRangeCheck">Ignore whether the character is "in range" when checking whether to display the balloon.</param>
    /// <param name="parentBone">The bone index to which the balloon is visually attached.  A value of 25 is used if the specified bone does not exist.</param>
    [MemberFunction("E8 ?? ?? ?? ?? 0F 28 B4 24 ?? ?? ?? ?? 48 8D 4D")]
    public partial void SetupBalloon(CStringPointer str, float playTime, bool param3, float openDelay, bool printToLog, bool param7, bool ignoreRangeCheck, byte parentBone );

    /// <summary>
    /// Closes and resets the balloon.
    /// </summary>
    [MemberFunction("E8 ?? ?? ?? ?? 66 3B BB")]
    public partial void CloseBalloon();

    /// <summary>
    /// Updates this balloon's timers and state machine, closing the balloon once it times out.
    /// </summary>
    [MemberFunction("E8 ?? ?? ?? ?? 48 8B CB E8 ?? ?? ?? ?? 0F B6 8B ?? ?? ?? ?? 83 E9")]
    public partial void Update();

    /// <summary>
    /// Resets most members to default values.
    /// </summary>
    /// <remarks>
    /// Does <i>not</i> close the balloon.  Effects are identical to <see cref="Reset2"/>.
    /// </remarks>
    [MemberFunction("E8 ?? ?? ?? ?? F6 86 ?? ?? ?? ?? ?? 48 8B 56")]
    public partial void Reset();

    /// <summary>
    /// Resets most members to default values.
    /// </summary>
    /// <remarks>
    /// Does <i>not</i> close the balloon.  Effects are identical to <see cref="Reset"/>.
    /// </remarks>
    [MemberFunction("E8 ?? ?? ?? ?? 45 33 C0 41 8B D7 48 8B CE E8 ?? ?? ?? ?? 80 A6")]
    public partial void Reset2();
}

public enum NPCYellBalloonState : int {
    Inactive = 0,
    Waiting = 1, // Delayed before being shown (by DelayTime seconds)
    Active = 2,
    Activating = 3, // Goes into this state for one frame after waiting.
}

[Flags]
public enum NPCYellBalloonFlags : byte {
    None = 0,
    Unk_1 = 1, // If this is not set, actually opening the bubble will be skipped.  Unsure of purpose.
    Unk_2 = 2, // Is passed as the bool third parameter to AgentScreenLog::OpenBalloon, and probably has the same effect as the Balloon EXD's boolean column.
    IgnoreRangeCheck = 4, // Call AgentScreenLog::OpenBalloon regardless of character range test result.
    PrintToLog = 8, // Also call RaptureLogModule::PrintMessage with balloon text when Balloon is opened.
}

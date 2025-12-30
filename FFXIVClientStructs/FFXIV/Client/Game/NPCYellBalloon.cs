using FFXIVClientStructs.FFXIV.Client.System.String;

namespace FFXIVClientStructs.FFXIV.Client.Game;

// Client::Game::NpcYellBalloon
// Probably part of Client::Game::Character::Character
[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 0x88)]
public unsafe partial struct NpcYellBalloon {

    /// <summary>
    /// The text that this balloon will display.
    /// </summary>
    /// <remarks>
    /// Empty when balloon is <see cref="NpcYellBalloonState.Inactive"/>.
    /// </remarks>
    [FieldOffset(0x0)] public Utf8String Text;

    /// <summary>
    /// Pointer to the <see cref="Character.Character"/> that contains this balloon.
    /// </summary>
    [FieldOffset(0x68)] public Character.Character* Character;

    /// <summary>
    /// How much longer (in seconds) the balloon should remain open.
    /// </summary>
    /// <remarks>
    /// Starting value set by OpenBalloon.  Starts counting down once in <see cref="NpcYellBalloonState.Active"/> state.  If this is set
    /// to zero before the balloon is activated, the balloon will remain open indefinitely, subject to the value of <see cref="CloseType"/>.
    /// </remarks>
    [FieldOffset(0x70)] public float PlayTimer;

    /// <summary>
    /// How much longer (in seconds) before the balloon should actually be opened with AgentScreenLog.
    /// </summary>
    /// <remarks>
    /// Starting value is set by OpenBalloon.  Resets to one second when balloon exits <see cref="NpcYellBalloonState.Waiting"/> state.
    /// </remarks>
    [FieldOffset(0x74)] public float DelayTime;

    /// <summary>
    /// The balloon's current state.
    /// </summary>
    [FieldOffset(0x78)] public NpcYellBalloonState State;

    /// <summary>
    /// Controls how the balloon determines when to close.
    /// </summary>
    /// <remarks>
    /// If OpenBalloon's reducedCloseChecks parameter is true, this is <see cref="NpcYellBalloonCloseType.ReducedCloseChecks"/>, else if
    /// playTime is zero this is <see cref="NpcYellBalloonCloseType.HoldOpen"/>, else this is <see cref="NpcYellBalloonCloseType.Normal"/>.
    /// </remarks>
    [FieldOffset(0x7C)] public NpcYellBalloonCloseType CloseType;

    /// <summary>
    /// The bone index to which the balloon is attached on the character.
    /// </summary>
    [FieldOffset(0x80)] public byte ParentBone;

    /// <summary>
    /// Miscellaneous balloon behaviors.
    /// </summary>
    [FieldOffset(0x81)] public NpcYellBalloonFlags Flags;

    [MemberFunction("E8 ?? ?? ?? ?? 0F BE 4B 44")]
    public partial NpcYellBalloon* Ctor();

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
    /// <param name="softOpen">If this is true, the bubble will fade in more gently than the normal "popping" in.</param>
    /// <param name="openDelay">Time in seconds to wait before actually opening the balloon.</param>
    /// <param name="printToLog">Whether the balloon text should also be printed to the chat log.</param>
    /// <param name="reducedCloseChecks">Skips certain non-timer checks for closing the balloon.  Unknown purpose.  Usually false.</param>
    /// <param name="ignoreRangeCheck">Ignore whether the character is "in range" when checking whether to display the balloon.</param>
    /// <param name="parentBone">The bone index to which the balloon is visually attached.  A value of 25 is used if the specified bone does not exist.</param>
    [MemberFunction("E8 ?? ?? ?? ?? 0F 28 B4 24 ?? ?? ?? ?? 48 8D 4D"), GenerateStringOverloads]
    public partial void OpenBalloon(CStringPointer str, float playTime, bool softOpen, float openDelay, bool printToLog, bool reducedCloseChecks, bool ignoreRangeCheck, byte parentBone);

    /// <summary>
    /// Closes and resets the balloon.
    /// </summary>
    /// <remarks>
    /// Only required if balloon is set to hold open (or needs to be closed early).
    /// </remarks>
    [MemberFunction("E8 ?? ?? ?? ?? 66 3B BB ?? ?? ?? ?? 75")]
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

public enum NpcYellBalloonState : int {

    /// <summary>
    /// Balloon is inactive and in the default state.
    /// </summary>
    Inactive = 0,

    /// <summary>
    /// Balloon is waiting to open (until <see cref="NpcYellBalloon.DelayTime"/> expires).
    /// </summary>
    Waiting = 1,

    /// <summary>
    /// Balloon is open.
    /// </summary>
    Active = 2,

    /// <summary>
    /// Balloon is attempting to open.
    /// </summary>
    Activating = 3,
}

public enum NpcYellBalloonCloseType : int {

    /// <summary>
    /// Performs normal timer checks every active update cycle.
    /// </summary>
    Normal = 0,

    /// <summary>
    /// Ignore <see cref="NpcYellBalloon.PlayTimer"/> and hold open indefinitely until <see cref="NpcYellBalloon.CloseBalloon"/> is called.
    /// </summary>
    HoldOpen = 1,

    /// <remarks>
    /// Similar to <see cref="Normal"/>, but skips some non-<see cref="NpcYellBalloon.PlayTimer"/> checks.
    /// </remarks>
    ReducedCloseChecks = 2,
}

[Flags]
public enum NpcYellBalloonFlags : byte {
    None = 0,

    /// <summary>
    /// All balloons will have this flag while in use.
    /// </summary>
    /// <remarks>
    /// If this is not set, <see cref="NpcYellBalloon.Update"/> will not open the balloon (or will immediately close it).
    /// </remarks>
    Valid = 1,

    /// <summary>
    /// The balloon fades in instead of opening with a "pop".
    /// </summary>
    /// <remarks>
    /// Is passed as the bool third parameter to AgentScreenLog::OpenBalloon, and probably has the same effect as the Balloon EXD's boolean "Slowly" column.
    /// </remarks>
    SoftOpen = 2,

    /// <remarks>
    /// Call AgentScreenLog::OpenBalloon regardless of character range test result.
    /// </remarks>
    IgnoreRangeCheck = 4,

    /// <remarks>
    /// Also call RaptureLogModule::PrintMessage with balloon text when the balloon is opened.
    /// </remarks>
    PrintToLog = 8,
}

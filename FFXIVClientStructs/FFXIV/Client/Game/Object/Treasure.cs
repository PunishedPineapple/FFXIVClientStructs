namespace FFXIVClientStructs.FFXIV.Client.Game.Object;

// Client::Game::Object::Treasure
//   Client::Game::Object::GameObject
[GenerateInterop]
[Inherits<GameObject>]
[StructLayout(LayoutKind.Explicit, Size = 0x210)]
public unsafe partial struct Treasure {
    [FieldOffset(0x1A0)] public TreasureState State;
    [FieldOffset(0x1A4)] public float CountdownTimer_Sec; // Starts counting down from the starting value below once spawned.  Unsure what hitting zero actually does.  Opening the treasure resets this to the claim period length and it starts counting down again.  Hitting zero then results in automatic disposition of the contents.
    [FieldOffset(0x1A8)] public float CountdownStartingValue_Sec; // The starting value of the countdown timer at object spawn.
    [FieldOffset(0x1AC)] public float ClaimPeriodLength_Sec; // The time after opening to be able to roll on or assign drops before auto-disposition of loot.
    [FieldOffset(0x1B0), FixedSizeArray] internal FixedSizeArray15<uint> _lootableItemIds; //***** TODO: Assuming that none of this is padding.  It is at least 12 long.  Not sure how to check.  Tower's final chest maybe?
    [FieldOffset(0x1F0)] public byte LootableItemCount; //***** TODO: Not positive about the size of this field.
    [FieldOffset(0x1F4)] public int Unk_1F4; //***** TODO: Might just be a bool.  Went 0 to 1 after opening a savage weekly-locked coffer (that was lootable by the player).
    [FieldOffset(0x1F8)] public float TreasureOpenTime_Sec; // Starts from zero when opening.  Stops when fadeout begins.
    [FieldOffset(0x1FC)] public TreasureFlags Flags;
    [FieldOffset(0x1FE)] public short Unk_1FE; //***** TODO: Seems to be -1 for treasures that spawn in non-fixed locations?
    [FieldOffset(0x200)] public TreasureKind Unk_200; // Some kind of type or state.  Have seen 1 for levequest coffers, 2 for dungeon/raid chests, and 5 for personal spoils (variant, OC).
    [FieldOffset(0x204)] public int Unk_204;
    [FieldOffset(0x208)] public long Unk_208;

    public enum TreasureState : byte {
        Unopened = 0,
        Opening = 1,
        Opened = 2,  // Saw this in alliance raid when coffer was sitting open.
        Unk_3 = 3,  // Went directly to this (skipped 1 and 2) when opening personal spoils in Sil'dih.
        FadingOut = 4,
        FadedOut = 5,
    }

    [Flags]
    public enum TreasureFlags : byte {
        None = 0,
        Opened = 1,
        FadedOut = 2,   // This is set when the fading starts, not finishes.
        Unk_4 = 4,  // This was set (along with opened) after opening the party's chest in Dun Scaith.  Possibly flags that it belongs to the player's party.
    }

    public enum TreasureKind : byte {
        Unknown = 0,
        Levequest = 1,
        DungeonRaid = 2,
        Unk_3 = 3,  //*****TODO: One of these might be treasure maps?
        Unk_4 = 4,
        PersonalLoot = 5,   // Variant, Occult Crescent
    }
}

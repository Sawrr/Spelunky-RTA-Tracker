namespace AchievementsTracker
{
    public static class ImgOrder
    {
        public static (EntryType, string)[] DEFAULT = {
            (EntryType.Character, "c2"),  // Yellow
            (EntryType.Character, "c6"),  // Cyan
            (EntryType.Character, "c7"),  // Lime
            (EntryType.Character, "c4"),  // Purple
            (EntryType.Character, "c9"),  // Round Girl
            (EntryType.Character, "c12"), // Round Boy
            (EntryType.Character, "c11"), // Viking
            (EntryType.Character, "c5"),  // Black
            (EntryType.Character, "c1"),  // Meat Boy
            (EntryType.Character, "c8"),  // Magenta
            (EntryType.Character, "c14"), // Robot
            (EntryType.Character, "c10"), // Ninja
            (EntryType.Character, "c13"), // Carl
            (EntryType.Character, "c15"), // Golden Monk
            (EntryType.Character, "c3"),  // Brown
            (EntryType.Character, "c0"),  // Yang

            (EntryType.Monster, "m0"),  // Snake
            (EntryType.Monster, "m1"),  // Cobra
            (EntryType.Monster, "m2"),  // Bat
            (EntryType.Monster, "m3"),  // Spike
            (EntryType.Monster, "m4"),  // Hangspider
            (EntryType.Monster, "m5"),  // Giant Spider
            (EntryType.Monster, "m6"),  // Skeleton
            (EntryType.Monster, "m7"),  // Scorpian
            (EntryType.Monster, "m8"),  // Caveman
            (EntryType.Monster, "m9"),  // Damsel
            (EntryType.Monster, "m10"), // Shopkeeper
            (EntryType.Monster, "m11"), // Tunnelman
            (EntryType.Monster, "m12"), // Scarab
            (EntryType.Monster, "m13"), // Tiki Man
            (EntryType.Monster, "m14"), // Blue Frog
            (EntryType.Monster, "m15"), // Orange Frog
            (EntryType.Monster, "m16"), // Giant Frog
            (EntryType.Monster, "m17"), // Mantrap
            (EntryType.Monster, "m18"), // Piranha
            (EntryType.Monster, "m19"), // Old Bitey
            (EntryType.Monster, "m20"), // Bee
            (EntryType.Monster, "m21"), // Queen Bee
            (EntryType.Monster, "m22"), // Snail
            (EntryType.Monster, "m23"), // Monkey
            (EntryType.Monster, "m24"), // Golden Monkey
            (EntryType.Monster, "m25"), // Jiangshi
            (EntryType.Monster, "m26"), // Green Knight
            (EntryType.Monster, "m27"), // Black Knight
            (EntryType.Monster, "m28"), // Vampire
            (EntryType.Monster, "m29"), // Ghost
            (EntryType.Monster, "m30"), // Bacterium
            (EntryType.Monster, "m31"), // Worm Egg
            (EntryType.Monster, "m32"), // Worm Baby
            (EntryType.Monster, "m33"), // Yeti
            (EntryType.Monster, "m34"), // Yeti King
            (EntryType.Monster, "m35"), // Mammoth
            (EntryType.Monster, "m36"), // Alien
            (EntryType.Monster, "m37"), // UFO
            (EntryType.Monster, "m38"), // Alien Tank
            (EntryType.Monster, "m39"), // Alien Lord
            (EntryType.Monster, "m40"), // Alien Queen
            (EntryType.Monster, "m41"), // Hawkman
            (EntryType.Monster, "m42"), // Crocman
            (EntryType.Monster, "m43"), // Magmaman
            (EntryType.Monster, "m44"), // Scorpian Fly
            (EntryType.Monster, "m45"), // Mummy
            (EntryType.Monster, "m46"), // Anubis
            (EntryType.Monster, "m47"), // Abubis II
            (EntryType.Monster, "m48"), // Olmec
            (EntryType.Monster, "m49"), // Vlad
            (EntryType.Monster, "m50"), // Imp
            (EntryType.Monster, "m51"), // Blue Devil
            (EntryType.Monster, "m52"), // Succubus
            (EntryType.Monster, "m53"), // Horse Head
            (EntryType.Monster, "m54"), // Ox Face
            (EntryType.Monster, "m55"), // Yama

            (EntryType.Item, "i0"),  // Rope Pile
            (EntryType.Item, "i1"),  // Bomb Bag
            (EntryType.Item, "i2"),  // Bomb Box
            (EntryType.Item, "i3"),  // Specs
            (EntryType.Item, "i4"),  // Climbing Glove
            (EntryType.Item, "i5"),  // Pitcher's Mitt
            (EntryType.Item, "i6"),  // Spring Shoes
            (EntryType.Item, "i7"),  // Spike Shoes
            (EntryType.Item, "i8"),  // Paste
            (EntryType.Item, "i9"),  // Compass
            (EntryType.Item, "i10"), // Mattock
            (EntryType.Item, "i11"), // Boomerang
            (EntryType.Item, "i12"), // Machete
            (EntryType.Item, "i13"), // Crysknife
            (EntryType.Item, "i14"), // Webgun
            (EntryType.Item, "i15"), // Shotgun
            (EntryType.Item, "i16"), // Freezeray
            (EntryType.Item, "i17"), // Plasma Cannon
            (EntryType.Item, "i18"), // Camera
            (EntryType.Item, "i19"), // Teleporter
            (EntryType.Item, "i20"), // Parachute
            (EntryType.Item, "i21"), // Cape
            (EntryType.Item, "i22"), // Jetpack
            (EntryType.Item, "i23"), // Shield
            (EntryType.Item, "i24"), // Royal Jelly
            (EntryType.Item, "i25"), // Idol
            (EntryType.Item, "i26"), // Kapala
            (EntryType.Item, "i27"), // Udjat
            (EntryType.Item, "i28"), // Ankh
            (EntryType.Item, "i29"), // Hedjet
            (EntryType.Item, "i30"), // Sceptre
            (EntryType.Item, "i31"), // Book of the Dead
            (EntryType.Item, "i32"), // Vlad's Cape
            (EntryType.Item, "i33"), // Vlad's Amulet

            (EntryType.Trap, "t0"),  // Spikes
            (EntryType.Trap, "t1"),  // Arrow Trap
            (EntryType.Trap, "t2"),  // TNT
            (EntryType.Trap, "t3"),  // Boulder
            (EntryType.Trap, "t4"),  // Tikitrap
            (EntryType.Trap, "t5"),  // Acid
            (EntryType.Trap, "t6"),  // Jump Pad
            (EntryType.Trap, "t7"),  // Landmine
            (EntryType.Trap, "t8"),  // Turret
            (EntryType.Trap, "t9"),  // Laser
            (EntryType.Trap, "t10"), // Crush Trap
            (EntryType.Trap, "t11"), // Ceiling Trap
            (EntryType.Trap, "t12"), // Spikeball
            (EntryType.Trap, "t13"), // Lava
        };

        public static (EntryType, string)[] BY_AREA = {
            (EntryType.Character, "c2"),  // Yellow
            (EntryType.Character, "c6"),  // Cyan
            (EntryType.Character, "c7"),  // Lime
            (EntryType.Character, "c4"),  // Purple
            (EntryType.Character, "c9"),  // Round Girl
            (EntryType.Character, "c12"), // Round Boy
            (EntryType.Character, "c11"), // Viking
            (EntryType.Character, "c5"),  // Black
            (EntryType.Character, "c1"),  // Meat Boy
            (EntryType.Character, "c8"),  // Magenta
            (EntryType.Character, "c14"), // Robot
            (EntryType.Character, "c10"), // Ninja
            (EntryType.Character, "c13"), // Carl
            (EntryType.Character, "c15"), // Golden Monk
            (EntryType.Character, "c3"),  // Brown
            (EntryType.Character, "c0"),  // Yang

            // Mines
            (EntryType.Monster, "m0"),  // Snake
            (EntryType.Monster, "m1"),  // Cobra
            (EntryType.Monster, "m2"),  // Bat
            (EntryType.Monster, "m3"),  // Spike
            (EntryType.Monster, "m4"),  // Hangspider
            (EntryType.Monster, "m5"),  // Giant Spider
            (EntryType.Item, "i8"),     // Paste
            (EntryType.Monster, "m6"),  // Skeleton
            (EntryType.Monster, "m7"),  // Scorpian
            (EntryType.Monster, "m8"),  // Caveman
            (EntryType.Monster, "m9"),  // Damsel
            (EntryType.Monster, "m10"), // Shopkeeper
            (EntryType.Monster, "m11"), // Tunnelman
            (EntryType.Trap, "t1"),     // Arrow Trap
            (EntryType.Trap, "t2"),     // TNT
            (EntryType.Trap, "t3"),     // Boulder
            (EntryType.Item, "i10"),    // Mattock
            (EntryType.Item, "i27"),    // Udjat

            // Jungle
            (EntryType.Monster, "m13"), // Tiki Man
            (EntryType.Item, "i11"),    // Boomerang
            (EntryType.Monster, "m14"), // Blue Frog
            (EntryType.Monster, "m15"), // Orange Frog
            (EntryType.Monster, "m16"), // Giant Frog
            (EntryType.Monster, "m17"), // Mantrap
            (EntryType.Monster, "m22"), // Snail
            (EntryType.Monster, "m23"), // Monkey
            (EntryType.Trap, "t4"),     // Tikitrap

            (EntryType.Monster, "m18"), // Piranha
            (EntryType.Monster, "m19"), // Old Bitey
            
            (EntryType.Monster, "m20"), // Bee
            (EntryType.Monster, "m21"), // Queen Bee
            (EntryType.Item, "i24"),    // Royal Jelly

            // Haunted Castle
            (EntryType.Monster, "m29"), // Ghost
            (EntryType.Monster, "m26"), // Green Knight
            (EntryType.Monster, "m27"), // Black Knight
            (EntryType.Item, "i23"),    // Shield

            // Worm
            (EntryType.Monster, "m31"), // Worm Egg
            (EntryType.Monster, "m32"), // Worm Baby
            (EntryType.Item, "i13"),    // Crysknife
            (EntryType.Trap, "t5"),     // Acid
            (EntryType.Monster, "m30"), // Bacterium

            // Ice Caves
            (EntryType.Monster, "m33"), // Yeti
            (EntryType.Monster, "m35"), // Mammoth
            (EntryType.Monster, "m37"), // UFO
            (EntryType.Monster, "m36"), // Alien
            (EntryType.Trap, "t6"),     // Jump Pad
            (EntryType.Trap, "t7"),     // Landmine
            (EntryType.Monster, "m34"), // Yeti King
            (EntryType.Item, "i9"),     // Compass
            (EntryType.Item, "i0"),     // Rope Pile
            (EntryType.Item, "i7"),     // Spike Shoes

            (EntryType.Monster, "m38"), // Alien Tank
            (EntryType.Monster, "m39"), // Alien Lord
            (EntryType.Monster, "m40"), // Alien Queen
            (EntryType.Item, "i17"),    // Plasma Cannon
            (EntryType.Trap, "t8"),     // Turret
            (EntryType.Trap, "t9"),     // Laser


            // Temple / CoG
            (EntryType.Monster, "m41"), // Hawkman
            (EntryType.Monster, "m42"), // Crocman
            (EntryType.Monster, "m43"), // Magmaman
            (EntryType.Monster, "m46"), // Anubis
            (EntryType.Monster, "m48"), // Olmec
            (EntryType.Trap, "t10"),    // Crush Trap
            (EntryType.Trap, "t11"),    // Ceiling Trap
            (EntryType.Trap, "t13"),    // Lava
            (EntryType.Monster, "m12"), // Scarab
            (EntryType.Monster, "m44"), // Scorpian Fly
            (EntryType.Monster, "m45"), // Mummy
            (EntryType.Monster, "m47"), // Abubis II

            // Hell
            (EntryType.Monster, "m25"), // Jiangshi
            (EntryType.Monster, "m28"), // Vampire
            (EntryType.Item, "i21"),    // Cape
            (EntryType.Monster, "m49"), // Vlad
            (EntryType.Item, "i32"),    // Vlad's Cape
            (EntryType.Item, "i33"),    // Vlad's Amulet
            (EntryType.Monster, "m50"), // Imp
            (EntryType.Monster, "m51"), // Blue Devil
            (EntryType.Monster, "m52"), // Succubus
            (EntryType.Trap, "t12"),    // Spikeball

            (EntryType.Item, "i28"),    // Ankh
            (EntryType.Item, "i29"),    // Hedjet
            (EntryType.Item, "i30"),    // Sceptre
            (EntryType.Item, "i31"),    // Book of the Dead
            (EntryType.Monster, "m53"), // Horse Head
            (EntryType.Monster, "m54"), // Ox Face
            (EntryType.Item, "i2"),     // Bomb Box
            (EntryType.Monster, "m55"), // Yama
            (EntryType.Trap, "t0"),     // Spikes


            (EntryType.Item, "i1"),     // Bomb Bag
            (EntryType.Item, "i3"),     // Specs
            (EntryType.Item, "i4"),     // Climbing Glove
            (EntryType.Item, "i5"),     // Pitcher's Mitt
            (EntryType.Item, "i6"),     // Spring Shoes
            (EntryType.Item, "i12"),    // Machete
            (EntryType.Item, "i14"),    // Webgun
            (EntryType.Item, "i15"),    // Shotgun
            (EntryType.Item, "i16"),    // Freezeray
            (EntryType.Item, "i18"),    // Camera
            (EntryType.Item, "i19"),    // Teleporter
            (EntryType.Item, "i20"),    // Parachute
            (EntryType.Item, "i22"),    // Jetpack
            (EntryType.Item, "i25"),    // Idol
            (EntryType.Item, "i26"),    // Kapala
            (EntryType.Monster, "m24"), // Golden Monkey
        };

    }
}

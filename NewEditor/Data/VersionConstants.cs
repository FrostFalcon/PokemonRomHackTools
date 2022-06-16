using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEditor.Data
{
    public static class VersionConstants
    {
        //File Data
        public const int BW2_FirstNarcPointerLocation = 0x3644C0;
        public const int BW2_LastNarc = 307;
        public const int BW2_NARCsToSkip = 3;
        public const int BW2_FileSizeLimit = 0x12000000;

        //Narc Data
        public const int BW2_TextNARCID = 2;
        public const int BW2_StoryTextNARCID = 3;
        public const int BW2_MapMatriciesNARCID = 9;
        public const int BW2_ZoneDataNARCID = 12;
        public const int BW2_PokemonDataNARCID = 16;
        public const int BW2_LevelUpMovesNARCID = 18;
        public const int BW2_EvolutionsNARCID = 19;
        public const int BW2_MoveDataNARCID = 21;
        public const int BW2_ScriptNARCID = 56;

        public const int BW2_PokemonNameTextFileID = 90;
        public const int BW2_AbilityNameTextFileID = 374;
        public const int BW2_TypeNameTextFileID = 398;
        public const int BW2_ItemNameTextFileID = 64;
        public const int BW2_MoveNameTextFileID = 403;
        public const int BW2_ZoneNameTextFileID = 109;

        public static List<string> BW2_TMNames = new List<string>()
        {
			"TM01 Hone Claws",
			"TM02 Dragon Claw",
			"TM03 Psyshock",
			"TM04 Calm Mind",
			"TM05 Roar",
			"TM06 Toxic",
			"TM07 Hail",
			"TM08 Bulk Up",
			"TM09 Venoshock",
			"TM10 Hidden Power",
			"TM11 Sunny Day",
			"TM12 Taunt",
			"TM13 Ice Beam",
			"TM14 Blizzard",
			"TM15 Hyper Beam",
			"TM16 Light Screen",
			"TM17 Protect",
			"TM18 Rain Dance",
			"TM19 Telekinesis",
			"TM20 Safeguard",
			"TM21 Frustration",
			"TM22 SolarBeam",
			"TM23 Smack Down",
			"TM24 Thunderbolt",
			"TM25 Thunder",
			"TM26 Earthquake",
			"TM27 Return",
			"TM28 Dig",
			"TM29 Psychic",
			"TM30 Shadow Ball",
			"TM31 Brick Break",
			"TM32 Double Team",
			"TM33 Reflect",
			"TM34 Sludge Wave",
			"TM35 Flamethrower",
			"TM36 Sludge Bomb",
			"TM37 Sandstorm",
			"TM38 Fire Blast",
			"TM39 Rock Tomb",
			"TM40 Aerial Ace",
			"TM41 Torment",
			"TM42 Facade",
			"TM43 Flame Charge",
			"TM44 Rest",
			"TM45 Attract",
			"TM46 Thief",
			"TM47 Low Sweep",
			"TM48 Round",
			"TM49 Echoed Voice",
			"TM50 Overheat",
			"TM51 Ally Switch",
			"TM52 Focus Blast",
			"TM53 Energy Ball",
			"TM54 False Swipe",
			"TM55 Scald",
			"TM56 Fling",
			"TM57 Charge Beam",
			"TM58 Sky Drop",
			"TM59 Incinerate",
			"TM60 Quash",
			"TM61 Will-O-Wisp",
			"TM62 Acrobatics",
			"TM63 Embargo",
			"TM64 Explosion",
			"TM65 Shadow Claw",
			"TM66 Payback",
			"TM67 Retaliate",
			"TM68 Giga Impact",
			"TM69 Rock Polish",
			"TM70 Flash",
			"TM71 Stone Edge",
			"TM72 Volt Switch",
			"TM73 Thunder Wave",
			"TM74 Gyro Ball",
			"TM75 Swords Dance",
			"TM76 Struggle Bug",
			"TM77 Psych Up",
			"TM78 Bulldoze",
			"TM79 Frost Breath",
			"TM80 Rock Slide",
			"TM81 X-Scissor",
			"TM82 Dragon Tail",
			"TM83 Work Up",
			"TM84 Poison Jab",
			"TM85 Dream Eater",
			"TM86 Grass Knot",
			"TM87 Swagger",
			"TM88 Pluck",
			"TM89 U-Turn",
			"TM90 Substitute",
			"TM91 Flash Cannon",
			"TM92 Trick Room",
			"TM93 Wild Charge",
			"TM94 Rock Smash",
			"TM95 Snarl",
			"HM01 Cut",
			"HM02 Fly",
			"HM03 Surf",
			"HM04 Strength",
			"HM05 Waterfall",
			"HM06 Dive"
		};

		public static List<string> BW2_EvolutionMethodNames = new List<string>()
		{
			"None",
			"Level up with high friendship",
			"Friendship during the day",
			"Friendship at night",
			"Level up",
			"Trade",
			"Trade with held item",
			"Trade for Karrablast / Shelmet",
			"Evolution stone / Held Item",
			"Level up with attack > defense",
			"Level up with attack = defense",
			"Level up with attack < defense",
			"Level up with personality < 5 (Silcoon)",
			"Level up with personality > 5 (Cascoon)",
			"Level up (Ninjask)",
			"Level up (Shedinja)",
			"Level up with high beauty",
			"Evolution stone (Male)",
			"Evolution stone (Female)",
			"Evolution stone (Day)",
			"Evolution stone (Night)",
			"Level up with a certain move",
			"Level up with a certain pokemon",
			"Level up (Male)",
			"Level up (Female)",
			"Level up in electric cave",
			"Level up near mossy rock",
			"Level up near ice rock"
		};
	}
}

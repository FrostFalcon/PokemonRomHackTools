using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Data
{
    public static class HexOffsets
    {
        public static int pokeDataLocation = 0;
        public static int pokeDataFirstEntry = 0x16B0;
        public static int pokeDataEntrySize = 76;
        public static int pokeDataTotalBytes = 0;

        public static int levelUpMovesLocation = 0;
        public static int levelUpMovesFirstEntry = 0x1660;
        public static int levelUpMovesTotalBytes = 0;
        public static int levelUpMovesHeaderAddress = 0x364568;

        public static int evolutionsLocation = 0;
        public static int evolutionsFirstEntry = 0x1688;
        public static int evolutionsTotalBytes = 0;
        public static int evolutionsHeaderAddress = 0x364570;

        public static int trDataLocation = 0;
        public static int trDataFirstEntry = 0x19A0;
        public static int trDataEntrySize = 20;
        public static int trDataTotalBytes = 0;

        public static int trPokeLocation = 0;
        public static int trPokeFirstEntry = 0x19AC;
        public static int trPokeTotalBytes = 0;
        public static int trPokeHeaderAddress = 0x3647B8;

        public static int encountersLocation = 0;
        public static int encountersFirstEntry = 0x46C;
        public static int encountersEntrySize = 232;
        public static int encountersTotalBytes = 0;

        public static int overworldsLocation = 0;
        public static int overworldsFirstEntry = 0x1374;
        public static int overworldsTotalBytes = 0;

        public static void RegisterNARCLocations(List<byte> romData, int startNarc = -1, int startPosition = 0x367000)
        {
            int narcID = startNarc;

            for (int i = startPosition; i < romData.Count; i++)
            {
                if (romData[i] == 0x4E && romData[i + 1] == 0x41 && romData[i + 2] == 0x52 && romData[i + 3] == 0x43)
                {
                    if (narcID == 16)
                    {
                        pokeDataLocation = i;
                        int j = 0;
                        while (!(romData[i + j] == 0xFF && romData[i + j + 1] == 0xFF && romData[i + j + 2] == 0xFF))
                        {
                            j++;
                        }
                        pokeDataTotalBytes = i + j - pokeDataLocation;
                    }

                    if (narcID == 18)
                    {
                        levelUpMovesLocation = i;
                        int j = 0;
                        while (!(romData[i + j] == 0xFF && romData[i + j + 1] == 0xFF && romData[i + j + 2] == 0xFF && romData[i + j + 3] == 0xFF && romData[i + j + 4] == 0xFF && romData[i + j + 5] == 0xFF))
                        {
                            j++;
                        }
                        levelUpMovesTotalBytes = i + j - levelUpMovesLocation;
                        levelUpMovesTotalBytes += 4;
                    }

                    if (narcID == 19)
                    {
                        evolutionsLocation = i;
                        int j = 0;
                        while (!(romData[i + j] == 0xFF && romData[i + j + 1] == 0xFF && romData[i + j + 2] == 0xFF && romData[i + j + 3] == 0xFF && romData[i + j + 4] == 0xFF && romData[i + j + 5] == 0xFF))
                        {
                            j++;
                        }
                        evolutionsTotalBytes = i + j - evolutionsLocation;
                        evolutionsTotalBytes += 4;
                    }

                    if (narcID == 91)
                    {
                        trDataLocation = i;
                        int j = 0;
                        while (!(romData[i + j] == 0xFF && romData[i + j + 1] == 0xFF && romData[i + j + 2] == 0xFF))
                        {
                            j++;
                        }
                        trDataTotalBytes = i + j - trDataLocation;
                    }

                    if (narcID == 92)
                    {
                        trPokeLocation = i;
                        int j = 0;
                        while (!(romData[i + j] == 0xFF && romData[i + j + 1] == 0xFF && romData[i + j + 2] == 0xFF))
                        {
                            j++;
                        }
                        trPokeTotalBytes = i + j - trPokeLocation;
                    }

                    if (narcID == 126)
                    {
                        overworldsLocation = i;
                        int j = 0;
                        while (!(romData[i + j] == 0xFF && romData[i + j + 1] == 0xFF && romData[i + j + 2] == 0xFF && romData[i + j + 3] == 0xFF && romData[i + j + 4] == 0xFF && romData[i + j + 5] == 0xFF))
                        {
                            j++;
                        }
                        overworldsTotalBytes = i + j - overworldsLocation;
                    }

                    if (narcID == 127)
                    {
                        encountersLocation = i;
                        int j = 0;
                        while (!(romData[i + j] == 0xFF && romData[i + j + 1] == 0xFF && romData[i + j + 2] == 0xFF))
                        {
                            j++;
                        }
                        encountersTotalBytes = i + j - encountersLocation;
                    }

                    narcID++;
                    if (narcID > 130) break;
                }
            }
        }
    }
}

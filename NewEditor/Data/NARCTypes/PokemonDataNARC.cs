using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewEditor.Forms;

namespace NewEditor.Data.NARCTypes
{
    public class PokemonDataNARC : NARC
    {
        public List<PokemonEntry> pokemon;

        public override void ReadData(FileStream fs)
        {
            base.ReadData(fs);

            //Find first file instance
            int pos = numFileEntries * 8;
            while (pos < byteData.Length)
            {
                pos++;
                if (pos >= byteData.Length) return;
                if (byteData[pos] == 'B' && byteData[pos + 1] == 'T' && byteData[pos + 2] == 'N' && byteData[pos + 3] == 'F') break;
            }
            int initialPosition = pos + 24;

            //Register data files
            pokemon = new List<PokemonEntry>();

            pos = pointerStartAddress;

            //Populate data types
            int nameID = 0;
            Dictionary<int, int> formNames = new Dictionary<int, int>();
            for (int i = 0; i < numFileEntries; i++)
            {
                int start = HelperFunctions.ReadInt(byteData, pos);
                int end = HelperFunctions.ReadInt(byteData, pos + 4);
                byte[] bytes = new byte[end - start];

                for (int j = 0; j < end - start; j++) bytes[j] = byteData[initialPosition + start + j];

                PokemonEntry p = new PokemonEntry(bytes) { nameID = nameID };
                pokemon.Add(p);

                //Assign name and handle alt forms
                if (formNames.ContainsKey(i)) p.nameID = formNames[i];

                if (p.numberOfForms > 1 && p.unknownsFrom27To31[2] != 0)
                {
                    for (int j = 0; j < p.numberOfForms - 1; j++) formNames.Add(HelperFunctions.ReadShort(p.unknownsFrom27To31, 1) + j, nameID);
                }
                nameID++;

                pos += 8;
            }
        }

        public override void WriteData(FileStream fs)
        {
            List<byte> newByteData = new List<byte>();
            List<byte> oldByteData = new List<byte>(byteData);

            newByteData.AddRange(oldByteData.GetRange(0, pointerStartAddress));

            //Find start of file instances
            int pos = 0;
            while (pos < byteData.Length)
            {
                pos++;
                if (pos >= byteData.Length) return;
                if (byteData[pos] == 'B' && byteData[pos + 1] == 'T' && byteData[pos + 2] == 'N' && byteData[pos + 3] == 'F') break;
            }

            newByteData.AddRange(oldByteData.GetRange(pos, 24));

            //Write Files
            int totalSize = 0;
            int pPos = pointerStartAddress;
            foreach (PokemonEntry p in pokemon)
            {
                newByteData.AddRange(p.bytes);
                newByteData.InsertRange(pPos, BitConverter.GetBytes(totalSize));
                pPos += 4;
                totalSize += p.bytes.Length;
                newByteData.InsertRange(pPos, BitConverter.GetBytes(totalSize));
                pPos += 4;
            }

            byteData = newByteData.ToArray();

            HelperFunctions.WriteInt(byteData, 8, byteData.Length);
            HelperFunctions.WriteInt(byteData, 24, pokemon.Count);

            base.WriteData(fs);
        }
    }

    public class PokemonEntry
    {
        public byte[] bytes;

        public LevelUpMoveset levelUpMoves;
        public EvolutionDataEntry evolutions;

        public int nameID;

        public byte baseHP;
        public byte baseAttack;
        public byte baseDefense;
        public byte baseSpAtt;
        public byte baseSpDef;
        public byte baseSpeed;

        public byte type1;
        public byte type2;
        
        public byte catchRate;
        public byte evolutionStage;

        public byte evYeildHP;
        public byte evYeildAttack;
        public byte evYeildDefense;
        public byte evYeildSpAtt;
        public byte evYeildSpDef;
        public byte evYeildSpeed;

        public short heldItem1;
        public short heldItem2;
        public short heldItem3;

        public byte[] unknownsFrom27To31;
        public byte[] unknownsFrom36To39;
        public byte[] unknownsAfter52;

        public byte genderRatio;
        public byte hatchCounter;
        public byte baseHappiness;
        public byte levelRate;
        public short xpYield;

        public byte eggGroup1;
        public byte eggGroup2;

        public byte ability1;
        public byte ability2;
        public byte ability3;

        public byte numberOfForms;
        public byte pokedexColor;

        public bool[] TMs;

        public PokemonEntry(byte[] bytes)
        {
            this.bytes = bytes;

            ReadData();
        }

        internal void ReadData()
        {
            baseHP = bytes[0];
            baseAttack = bytes[1];
            baseDefense = bytes[2];
            baseSpAtt = bytes[4];
            baseSpDef = bytes[5];
            baseSpeed = bytes[3];

            type1 = bytes[6];
            type2 = bytes[7];

            catchRate = bytes[8];
            evolutionStage = bytes[9];

            evYeildHP = (byte)(bytes[10] & 0b_11);
            evYeildAttack = (byte)(bytes[10] & 0b_1100);
            evYeildDefense = (byte)(bytes[10] & 0b_11_0000);
            evYeildSpAtt = (byte)(bytes[10] & 0b_1100_0000);
            evYeildSpDef = (byte)(bytes[11] & 0b_11);
            evYeildSpeed = (byte)(bytes[11] & 0b_1100);

            heldItem1 = (short)HelperFunctions.ReadShort(bytes, 12);
            heldItem2 = (short)HelperFunctions.ReadShort(bytes, 14);
            heldItem3 = (short)HelperFunctions.ReadShort(bytes, 16);

            genderRatio = bytes[18];

            hatchCounter = bytes[19];
            baseHappiness = bytes[20];
            levelRate = bytes[21];
            eggGroup1 = bytes[22];
            eggGroup2 = bytes[23];

            ability1 = bytes[24];
            ability2 = bytes[25];
            ability3 = bytes[26];

            unknownsFrom27To31 = new byte[5];
            for (int i = 0; i < 5; i++) unknownsFrom27To31[i] = bytes[27 + i];

            numberOfForms = bytes[32];
            pokedexColor = bytes[33];
            xpYield = (short)HelperFunctions.ReadShort(bytes, 34);

            unknownsFrom36To39 = new byte[4];
            for (int i = 0; i < 4; i++) unknownsFrom36To39[i] = bytes[36 + i];

            TMs = new bool[101];
            for (int i = 0; i < 101; i++)
            {
                int pos = 40 + i / 8;
                int bit = i % 8;
                TMs[i] = (bytes[pos] & (1 << bit)) != 0;
            }

            unknownsAfter52 = new byte[bytes.Length - 53];
            for (int i = 0; i < unknownsAfter52.Length; i++) unknownsAfter52[i] = bytes[53 + i];
        }

        internal void ApplyData()
        {
            bytes[0] = baseHP;
            bytes[1] = baseAttack;
            bytes[2] = baseDefense;
            bytes[4] = baseSpAtt;
            bytes[5] = baseSpDef;
            bytes[3] = baseSpeed;

            bytes[6] = type1;
            bytes[7] = type2;

            bytes[8] = catchRate;
            bytes[9] = evolutionStage;

            bytes[10] = (byte)(evYeildHP + (evYeildAttack << 2) + (evYeildDefense << 4) + (evYeildSpAtt << 6));
            bytes[11] = (byte)(evYeildSpDef + (evYeildSpeed << 2));

            HelperFunctions.WriteShort(bytes, 12, heldItem1);
            HelperFunctions.WriteShort(bytes, 14, heldItem2);
            HelperFunctions.WriteShort(bytes, 16, heldItem3);

            bytes[18] = genderRatio;

            bytes[19] = hatchCounter;
            bytes[20] = baseHappiness;
            bytes[21] = levelRate;
            bytes[22] = eggGroup1;
            bytes[23] = eggGroup2;

            bytes[24] = ability1;
            bytes[25] = ability2;
            bytes[26] = ability3;

            for (int i = 0; i < 5; i++) bytes[27 + i] = unknownsFrom27To31[i];

            bytes[32] = numberOfForms;
            bytes[33] = pokedexColor;
            HelperFunctions.WriteShort(bytes, 34, xpYield);

            for (int i = 0; i < 4; i++) bytes[36 + i] = unknownsFrom36To39[i];

            //Reset TM bytes
            for (int i = 40; i < 53; i++) bytes[i] = 0;
            //Add bits
            for (int i = 0; i < 101; i++)
            {
                int pos = 40 + i / 8;
                int bit = i % 8;

                if (TMs[i]) bytes[pos] += (byte)(1 << bit);
            }

            for (int i = 0; i < unknownsAfter52.Length; i++) bytes[53 + i] = unknownsAfter52[i];

            if (levelUpMoves != null) levelUpMoves.ApplyData();
            if (evolutions != null) evolutions.ApplyData();
        }

        public override string ToString()
        {
            return nameID < MainEditor.textNarc.textFiles[VersionConstants.BW2_PokemonNameTextFileID].text.Count ? MainEditor.textNarc.textFiles[VersionConstants.BW2_PokemonNameTextFileID].text[nameID] + " - " + nameID : "Name not found";
        }
    }
}

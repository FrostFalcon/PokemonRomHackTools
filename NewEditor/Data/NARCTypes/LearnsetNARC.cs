﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewEditor.Forms;

namespace NewEditor.Data.NARCTypes
{
    public class LearnsetNARC : NARC
    {
        public List<LevelUpMoveset> learnsets;

        PokemonDataNARC pokemonNarc => MainEditor.pokemonDataNarc;
        List<string> MoveNameText => MainEditor.textNarc.textFiles[VersionConstants.BW2_MoveNameTextFileID].text;

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
            learnsets = new List<LevelUpMoveset>();

            pos = pointerStartAddress;

            for (int i = 0; i < numFileEntries; i++)
            {
                int start = HelperFunctions.ReadInt(byteData, pos);
                int end = HelperFunctions.ReadInt(byteData, pos + 4);
                byte[] bytes = new byte[end - start];

                for (int j = 0; j < end - start; j++) bytes[j] = byteData[initialPosition + start + j];

                LevelUpMoveset moveset = new LevelUpMoveset(bytes);
                learnsets.Add(moveset);

                pos += 8;
            }

            //Assign movesets to pokemon
            for (int i = 0; i < pokemonNarc.pokemon.Count; i++)
            {
                if (i < learnsets.Count) pokemonNarc.pokemon[i].levelUpMoves = learnsets[i];
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
            foreach (LevelUpMoveset t in learnsets)
            {
                newByteData.AddRange(t.bytes);
                newByteData.InsertRange(pPos, BitConverter.GetBytes(totalSize));
                pPos += 4;
                totalSize += t.bytes.Length;
                newByteData.InsertRange(pPos, BitConverter.GetBytes(totalSize));
                pPos += 4;
            }

            byteData = newByteData.ToArray();

            HelperFunctions.WriteInt(byteData, 8, byteData.Length);
            HelperFunctions.WriteInt(byteData, 24, learnsets.Count);

            base.WriteData(fs);
        }
    }

    public class LevelUpMoveset
    {
        public byte[] bytes;

        public List<LevelUpMoveSlot> moves;

        public LevelUpMoveset(byte[] bytes)
        {
            this.bytes = bytes;
            ReadData();
        }

        public void ReadData()
        {
            moves = new List<LevelUpMoveSlot>();

            for (int i = 0; i < bytes.Length; i += 4)
            {
                moves.Add(new LevelUpMoveSlot((short)HelperFunctions.ReadShort(bytes, i), (short)HelperFunctions.ReadShort(bytes, i + 2)));
            }
            if (moves[moves.Count - 1].moveID == -1) moves.RemoveAt(moves.Count - 1);
        }

        public void ApplyData()
        {
            bytes = new byte[moves.Count * 4 + 4];

            for (int i = 0; i < moves.Count; i++)
            {
                HelperFunctions.WriteShort(bytes, i * 4, moves[i].moveID);
                HelperFunctions.WriteShort(bytes, i * 4 + 2, moves[i].level);
            }
            for (int i = bytes.Length - 4; i < bytes.Length; i++) bytes[i] = 0xFF;
        }
    }

    public struct LevelUpMoveSlot
    {
        public short moveID;
        public short level;

        public LevelUpMoveSlot(short moveID, short level)
        {
            this.moveID = moveID;
            this.level = level;
        }

        public override string ToString()
        {
            return MainEditor.textNarc.textFiles[VersionConstants.BW2_MoveNameTextFileID].text[moveID] + " at lv " + level;
        }
    }
}

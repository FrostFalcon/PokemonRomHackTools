﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewEditor.Forms;

namespace NewEditor.Data.NARCTypes
{
    public class ScriptNARC : NARC
    {
        public List<ScriptFile> scriptFiles;

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
            scriptFiles = new List<ScriptFile>();

            pos = pointerStartAddress;

            for (int i = 0; i < numFileEntries; i++)
            {
                int start = HelperFunctions.ReadInt(byteData, pos);
                int end = HelperFunctions.ReadInt(byteData, pos + 4);
                byte[] bytes = new byte[end - start];

                for (int j = 0; j < end - start; j++) bytes[j] = byteData[initialPosition + start + j];

                ScriptFile file = new ScriptFile(bytes);
                scriptFiles.Add(file);

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
            foreach (ScriptFile s in scriptFiles)
            {
                s.ApplyData();
                newByteData.AddRange(s.bytes);
                newByteData.InsertRange(pPos, BitConverter.GetBytes(totalSize));
                pPos += 4;
                totalSize += s.bytes.Length;
                newByteData.InsertRange(pPos, BitConverter.GetBytes(totalSize));
                pPos += 4;
            }

            byteData = newByteData.ToArray();

            HelperFunctions.WriteInt(byteData, 8, byteData.Length);
            HelperFunctions.WriteInt(byteData, 24, scriptFiles.Count);

            base.WriteData(fs);
        }
    }

    public class ScriptFile
    {
        public byte[] bytes;

        public bool valid = false;

        public List<ScriptSequence> sequences;

        public ScriptFile(byte[] bytes)
        {
            this.bytes = bytes;

            ReadData();
        }

        public void ReadData()
        {
            int pointerPos = 0;

            while (pointerPos < bytes.Length - 1)
            {
                if (HelperFunctions.ReadShort(bytes, pointerPos) == 0xFD13)
                {
                    valid = true;
                    break;
                }
                pointerPos++;
            }

            pointerPos = 0;

            if (valid)
            {
                sequences = new List<ScriptSequence>();

                while (pointerPos < bytes.Length - 1 && HelperFunctions.ReadShort(bytes, pointerPos) != 0xFD13)
                {
                    int pos = pointerPos + HelperFunctions.ReadInt(bytes, pointerPos) + 4;

                    int jumpPos = 0;

                    ScriptSequence sequence = new ScriptSequence();
                    while ((pos < bytes.Length - 1 && HelperFunctions.ReadShort(bytes, pos) != 0x0002) || pos < jumpPos)
                    {
                        ScriptCommand s = new ScriptCommand(bytes, pos);
                        sequence.commands.Add(s);
                        if (s.ByteLength == -1) break;
                        pos += s.ByteLength;

                        if (s.commandID == 0x1E) jumpPos = pos + s.parameters[0];
                        if (s.commandID == 0x1F) jumpPos = pos + s.parameters[1];
                    }
                    sequence.commands.Add(new ScriptCommand(new byte[] { 02, 00 }, 0));

                    sequences.Add(sequence);

                    pointerPos += 4;
                }
            }
        }

        public void ApplyData()
        {
            int pointerPos = 0;

            if (valid)
            {
                List<byte> newBytes = new List<byte>();

                for (int i = 0; i < sequences.Count; i++) newBytes.AddRange(new byte[] { 0, 0, 0, 0 });
                newBytes.Add(0x13);
                newBytes.Add(0xFD);

                foreach (ScriptSequence seq in sequences)
                {
                    newBytes.RemoveRange(pointerPos, 4);
                    newBytes.InsertRange(pointerPos, BitConverter.GetBytes(newBytes.Count - pointerPos));

                    foreach (ScriptCommand c in seq.commands)
                    {
                        newBytes.AddRange(BitConverter.GetBytes(c.commandID));

                        for (int p = 0; p < c.parameters.Length; p++)
                        {
                            if (CommandReference.commandList[c.commandID].parameterBytes[p] == 1) newBytes.Add((byte)c.parameters[p]);
                            if (CommandReference.commandList[c.commandID].parameterBytes[p] == 2) newBytes.AddRange(BitConverter.GetBytes((short)c.parameters[p]));
                            if (CommandReference.commandList[c.commandID].parameterBytes[p] == 4) newBytes.AddRange(BitConverter.GetBytes(c.parameters[p]));
                        }
                    }

                    pointerPos += 4;
                }

                while (newBytes.Count % 4 != 0) newBytes.Add(0);
            }
        }
    }

    public class ScriptSequence
    {
        public List<ScriptCommand> commands;

        public ScriptSequence() => commands = new List<ScriptCommand>();
    }

    public struct ScriptCommand
    {
        public short commandID;
        public int[] parameters;

        public int ByteLength
        {
            get
            {
                if (!CommandReference.commandList.ContainsKey(commandID)) return -1;
                int n = 2;
                for (int i = 0; i < CommandReference.commandList[commandID].parameterBytes.Count; i++) n += CommandReference.commandList[commandID].parameterBytes[i];
                return n;
            }
        }

        public ScriptCommand(byte[] bytes, int offset)
        {
            commandID = (short)HelperFunctions.ReadShort(bytes, offset);

            if (CommandReference.commandList.ContainsKey(commandID))
            {
                int pos = offset + 2;
                parameters = new int[CommandReference.commandList[commandID].numParameters];
                for (int i = 0; i < CommandReference.commandList[commandID].parameterBytes.Count; i++)
                {
                    if (CommandReference.commandList[commandID].parameterBytes[i] == 1) parameters[i] = bytes[pos];
                    if (CommandReference.commandList[commandID].parameterBytes[i] == 2) parameters[i] = HelperFunctions.ReadShort(bytes, pos);
                    if (CommandReference.commandList[commandID].parameterBytes[i] == 4) parameters[i] = HelperFunctions.ReadInt(bytes, pos);
                    pos += CommandReference.commandList[commandID].parameterBytes[i];
                }
            }
            else parameters = new int[0];
        }

        public override string ToString()
        {
            if (!CommandReference.commandList.ContainsKey(commandID)) return "Error";
            Command cmd = CommandReference.commandList[commandID];
            StringBuilder str = new StringBuilder(cmd.name);
            str.Append("(");
            for (int i = 0; i < parameters.Length; i++)
            {
                if (i != 0) str.Append(", ");
                str.Append(parameters[i] >= 0x4000 ? "0x" + parameters[i].ToString("X") : parameters[i].ToString());
            }
            str.Append(") - " + ByteLength + " bytes");
            return str.ToString();
        }
    }

    internal static class CommandReference
    {
        internal static Dictionary<int, Command> commandList = new Dictionary<int, Command>()
        {
            {0x0, new Command("0x0", 0)},
            {0x1, new Command("0x1", 0)},
            {0x2, new Command("End", 0)},
            {0x3, new Command("ReturnAfterDelay", 1, 2)},
            {0x4, new Command("CallRoutine", 1, 2)},
            {0x5, new Command("EndFunction", 0)},
            {0x6, new Command("Logic06", 1, 2)},
            {0x7, new Command("Logic07", 1, 2)},
            {0x8, new Command("CompareTo", 1, 2)},
            {0x9, new Command("StoreVar", 1, 2)},
            {0xA, new Command("ClearVar", 1, 2)},
            {0xB, new Command("0B", 1, 2)},
            {0xC, new Command("0C", 1, 2)},
            {0xD, new Command("0D", 1, 2)},
            {0xE, new Command("0E", 1, 2)},
            {0xF, new Command("0F", 1, 2)},
            {0x10, new Command("StoreFlag", 1, 2)},
            {0x11, new Command("Condition", 1, 2)},
            {0x12, new Command("0x12", 2, 2, 2)},
            {0x13, new Command("0x13", 2, 2, 2)},
            {0x14, new Command("0x14", 1, 2)},
            {0x15, new Command("0x15", 0)},
            {0x16, new Command("0x16", 1, 2)},
            {0x17, new Command("0x17", 1, 2)},
            {0x18, new Command("0x18", 0)},
            {0x19, new Command("Compare", 2, 2, 2)},
            {0x1A, new Command("0x1A", 0)},
            {0x1B, new Command("0x1B", 0)},
            {0x1C, new Command("CallStd", 1, 2)},
            {0x1D, new Command("ReturnStd", 0)},
            {0x1E, new Command("Jump", 1, 4)},
            {0x1F, new Command("If", 2, 1, 4)},
            {0x20, new Command("0x20", 0)},
            {0x21, new Command("0x21", 1, 2)},
            {0x22, new Command("0x22", 1, 2)},
            {0x23, new Command("SetFlag", 1, 2)},
            {0x24, new Command("ClearFlag", 1, 2)},
            {0x25, new Command("SetVarFlagStatus", 2, 2, 2)},
            {0x26, new Command("SetVar26", 2, 2, 2)},
            {0x27, new Command("SetVar27", 2, 2, 2)},
            {0x28, new Command("SetVarEqVal", 2, 2, 2)},
            {0x29, new Command("SetVar29", 2, 2, 2)},
            {0x2A, new Command("SetVar2A", 2, 2, 2)},
            {0x2B, new Command("SetVar2B", 1, 2)},
            {0x2C, new Command("0x2C", 0)},
            {0x2D, new Command("0x2D", 1, 2)},
            {0x2E, new Command("LockAll", 0)},
            {0x2F, new Command("UnlockAll", 0)},
            {0x30, new Command("WaitMoment", 0)},
            {0x31, new Command("0x31", 0)},
            {0x32, new Command("WaitButton", 0)},
            {0x33, new Command("MusicalMessage", 1, 2)},
            {0x34, new Command("EventGreyMessage", 2, 2, 2)},
            {0x35, new Command("CloseMusicalMessage", 0)},
            {0x36, new Command("CloseEventGreyMessage", 0)},
            {0x37, new Command("0x37", 0)},
            {0x38, new Command("BubbleMessage", 2, 2, 1)},
            {0x39, new Command("CloseBubbleMessage", 0)},
            {0x3A, new Command("ShowMessageAt", 4, 2, 2, 2, 2)},
            {0x3B, new Command("CloseShowMessageAt", 1, 2)},
            {0x3C, new Command("Message", 6, 1, 1, 2, 2, 2, 2)},
            {0x3D, new Command("Message2", 5, 1, 1, 2, 2, 2)},
            {0x3E, new Command("CloseMessageKeyPress", 0)},
            {0x3F, new Command("CloseMessageKeyPress2", 0)},
            {0x40, new Command("MoneyBox", 2, 2, 2)},
            {0x41, new Command("CloseMoneyBox", 0)},
            {0x42, new Command("UpdateMoneyBox", 0)},
            {0x43, new Command("BorderedMessage", 2, 2, 2)},
            {0x44, new Command("CloseBorderedMessage", 0)},
            {0x45, new Command("PaperMessage", 2, 2, 2)},
            {0x46, new Command("ClosePaperMessage", 0)},
            {0x47, new Command("YesNoBox", 1, 2)},
            {0x48, new Command("Error", 7, 1, 1, 2, 2, 2, 2, 2)},
            {0x49, new Command("DoubleMessage", 7, 1, 1, 2, 2, 2, 2, 2)},
            {0x4A, new Command("AngryMessage", 3, 2, 1, 2)},
            {0x4B, new Command("CloseAngryMessage", 0)},
            {0x4C, new Command("SetVarHero", 1, 1)},
            {0x4D, new Command("SetVarItem", 2, 1, 2)},
            {0x4E, new Command("0x4E", 4, 1, 2, 2, 1)},
            {0x4F, new Command("SetVarItem2", 2, 1, 2)},
            {0x50, new Command("SetVarItem3", 2, 1, 2)},
            {0x51, new Command("SetVarMove", 2, 1, 2)},
            {0x52, new Command("SetVarBag", 2, 1, 2)},
            {0x53, new Command("SetVarPartyPokemon", 2, 1, 2)},
            {0x54, new Command("SetVarPartyPokemon2", 2, 1, 2)},
            {0x55, new Command("SetVar????", 2, 1, 2)},
            {0x56, new Command("SetVarType", 2, 1, 2)},
            {0x57, new Command("SetVarPokèmon", 2, 1, 2)},
            {0x58, new Command("SetVarPokèmon2", 2, 1, 2)},
            {0x59, new Command("SetVarLocation", 2, 1, 2)},
            {0x5A, new Command("SetVarPokèmonNick", 2, 1, 2)},
            {0x5B, new Command("SetVar????", 2, 1, 2)},
            {0x5C, new Command("SetVarStoreVal5C", 3, 1, 2, 2)},
            {0x5D, new Command("SetVarMusicalInfo", 2, 2, 2)},
            {0x5E, new Command("SetVarNations", 2, 1, 2)},
            {0x5F, new Command("SetVarActivities", 2, 1, 2)},
            {0x60, new Command("SetVarPower", 2, 1, 2)},
            {0x61, new Command("SetVarTrainerType", 2, 1, 2)},
            {0x62, new Command("SetVarTrainerType2", 2, 1, 2)},
            {0x63, new Command("SetVarGeneralWord", 2, 1, 2)},
            {0x64, new Command("ApplyMovement", 2, 2, 4)},
            {0x65, new Command("WaitMovement", 0)},
            {0x66, new Command("StoreHeroPosition 0x66", 2, 2, 2)},
            {0x67, new Command("0x67", 1, 2)},
            {0x68, new Command("StoreHeroPosition", 2, 2, 2)},
            {0x69, new Command("StoreNPCPosition", 3, 2, 2, 2)},
            {0x6A, new Command("6A", 2, 2, 2)},
            {0x6B, new Command("AddNPC", 1, 2)},
            {0x6C, new Command("RemoveNPC", 1, 2)},
            {0x6D, new Command("SetOWPosition", 5, 2, 2, 2, 2, 2)},
            {0x6E, new Command("0x6E", 1, 2)},
            {0x6F, new Command("0x6F", 1, 2)},
            {0x70, new Command("0x70", 5, 2, 2, 2, 2, 2)},
            {0x71, new Command("0x71", 3, 2, 2, 2)},
            {0x72, new Command("0x72", 4, 2, 2, 2, 2)},
            {0x73, new Command("0x73", 2, 2, 2)},
            {0x74, new Command("FacePlayer", 0)},
            {0x75, new Command("Release", 1, 2)},
            {0x76, new Command("ReleaseAll", 0)},
            {0x77, new Command("Lock", 1, 2)},
            {0x78, new Command("0x78", 1, 2)},
            {0x79, new Command("0x79", 3, 2, 2, 2)},
            {0x7A, new Command("0x7A", 0)},
            {0x7B, new Command("MoveNpctoCoordinates", 4, 2, 2, 2, 2)},
            {0x7C, new Command("0x7C", 4, 2, 2, 2, 2)},
            {0x7D, new Command("0x7D", 4, 2, 2, 2, 2)},
            {0x7E, new Command("TeleportUpNPc", 1, 2)},
            {0x7F, new Command("0x7F", 2, 2, 2)},
            {0x80, new Command("0x80", 1, 2)},
            {0x81, new Command("0x81", 0)},
            {0x82, new Command("0x82", 2, 2, 2)},
            {0x83, new Command("SetVar0x83", 1, 2)},
            {0x84, new Command("SetVar0x83", 1, 2)},
            {0x85, new Command("SingleTrainerBattle", 3, 2, 2, 2)},
            {0x86, new Command("DoubleTrainerBattle", 4, 2, 2, 2, 2)},
            {0x87, new Command("0x87", 3, 2, 2, 2)},
            {0x88, new Command("0x88", 3, 2, 2, 2)},
            {0x89, new Command("0x89", 0)},
            {0x8A, new Command("0x8A", 2, 2, 2)},
            {0x8B, new Command("PlayTrainerMusic", 1, 2)},
            {0x8C, new Command("EndBattle", 0)},
            {0x8D, new Command("StoreBattleResult", 1, 2)},
            {0x8E, new Command("DisableTrainer", 0)},
            {0x8F, new Command("0x8F", 0)},
            {0x90, new Command("dvar90", 2, 2, 2)},
            {0x91, new Command("0x91", 0)},
            {0x92, new Command("dvar92", 2, 2, 2)},
            {0x93, new Command("dvar93", 2, 2, 2)},
            {0x94, new Command("TrainerBattle", 4, 2, 2, 2, 2)},
            {0x95, new Command("DeactiveTrainerId", 1, 2)},
            {0x96, new Command("0x96", 1, 2)},
            {0x97, new Command("StoreActiveTrainerId", 2, 2, 2)},
            {0x98, new Command("ChangeMusic", 1, 2)},
            {0x99, new Command("0x99", 0)},
            {0x9A, new Command("0x9A", 0)},
            {0x9B, new Command("0x9B", 0)},
            {0x9C, new Command("0x9C", 0)},
            {0x9D, new Command("0x9D", 0)},
            {0x9E, new Command("FadeToDefaultMusic", 0)},
            {0x9F, new Command("0x9F", 1, 2)},
            {0xA0, new Command("0xA0", 0)},
            {0xA1, new Command("0xA1", 0)},
            {0xA2, new Command("0xA2", 2, 2, 2)},
            {0xA3, new Command("0xA3", 1, 2)},
            {0xA4, new Command("0xA4", 1, 2)},
            {0xA5, new Command("0xA5", 2, 2, 2)},
            {0xA6, new Command("PlaySound", 1, 2)},
            {0xA7, new Command("WaitSoundA7", 0)},
            {0xA8, new Command("WaitSound", 0)},
            {0xA9, new Command("PlayFanfare", 1, 2)},
            {0xAA, new Command("WaitFanfare", 0)},
            {0xAB, new Command("Cry", 2, 2, 2)},
            {0xAC, new Command("WaitCry", 0)},
            {0xAD, new Command("0xAD", 0)},
            {0xAE, new Command("0xAE", 0)},
            {0xAF, new Command("SetTextScriptMessage", 3, 2, 2, 2)},
            {0xB0, new Command("CloseMulti", 0)},
            {0xB1, new Command("0xB1", 0)},
            {0xB2, new Command("Multi2", 6, 1, 1, 1, 1, 1, 2)},
            {0xB3, new Command("FadeScreen", 4, 2, 2, 2, 2)},
            {0xB4, new Command("ResetScreen", 3, 2, 2, 2)},
            {0xB5, new Command("Screen0xB5", 3, 2, 2, 2)},
            {0xB6, new Command("TakeItem", 3, 2, 2, 2)},
            {0xB7, new Command("CheckItemBagSpace", 3, 2, 2, 2)},
            {0xB8, new Command("CheckItemBagNumber", 3, 2, 2, 2)},
            {0xB9, new Command("StoreItemCount", 2, 2, 2)},
            {0xBA, new Command("0xBA", 4, 2, 2, 2, 2)},
            {0xBB, new Command("0xBB", 2, 2, 2)},
            {0xBC, new Command("0xBC", 1, 2)},
            {0xBD, new Command("0xBD", 0)},
            {0xBE, new Command("Warp", 3, 2, 2, 2)},
            {0xBF, new Command("TeleportWarp", 4, 2, 2, 2, 2)},
            {0xC0, new Command("0xC0", 0)},
            {0xC1, new Command("FallWarp", 3, 2, 2, 2)},
            {0xC2, new Command("FastWarp", 4, 2, 2, 2, 2)},
            {0xC3, new Command("UnionWarp", 0)},
            {0xC4, new Command("TeleportWarp", 5, 2, 2, 2, 2, 2)},
            {0xC5, new Command("SurfAnimation", 0)},
            {0xC6, new Command("SpecialAnimation", 1, 2)},
            {0xC7, new Command("SpecialAnimation2", 2, 2, 2)},
            {0xC8, new Command("CallAnimation", 2, 2, 2)},
            {0xC9, new Command("0xC9", 0)},
            {0xCA, new Command("0xCA", 0)},
            {0xCB, new Command("StoreRandomNumber", 2, 2, 2)},
            {0xCC, new Command("StoreVarItem", 1, 2)},
            {0xCD, new Command("StoreVar0xCD", 1, 2)},
            {0xCE, new Command("StoreVar0xCE", 1, 2)},
            {0xCF, new Command("StoreVar0xCF", 1, 2)},
            {0xD0, new Command("StoreDate", 2, 2, 2)},
            {0xD1, new Command("Store0xD1", 2, 2, 2)},
            {0xD2, new Command("Store0xD2", 1, 2)},
            {0xD3, new Command("Store0xD3", 1, 2)},
            {0xD4, new Command("StoreBirthDay", 2, 2, 2)},
            {0xD5, new Command("StoreBadge", 2, 2, 2)},
            {0xD6, new Command("SetBadge", 1, 2)},
            {0xD7, new Command("StoreBadgeNumber", 1, 2)},
            {0xD8, new Command("0xD8", 0)},
            {0xD9, new Command("0xD9", 0)},
            {0xDA, new Command("0xDA", 3, 2, 2, 2)},
            {0xDB, new Command("0xDB", 0)},
            {0xDC, new Command("0xDC", 0)},
            {0xDD, new Command("0xDD", 2, 2, 2)},
            {0xDE, new Command("SpeciesDisplayDE", 2, 2, 2)},
            {0xDF, new Command("0xDF", 0)},
            {0xE0, new Command("StoreVersion", 1, 2)},
            {0xE1, new Command("StoreHeroGender", 1, 2)},
            {0xE2, new Command("0xE2", 0)},
            {0xE3, new Command("0xE3", 0)},
            {0xE4, new Command("0xE4", 1, 2)},
            {0xE5, new Command("StoreE5", 1, 2)},
            {0xE6, new Command("0xE6", 0)},
            {0xE7, new Command("ActivateRelocator", 1, 2)},
            {0xE8, new Command("0xE8", 0)},
            {0xE9, new Command("0xE9", 0)},
            {0xEA, new Command("StoreEA", 1, 2)},
            {0xEB, new Command("StoreEB", 1, 2)},
            {0xEC, new Command("StoreEC", 0)},
            {0xED, new Command("StoreED", 0)},
            {0xEE, new Command("StoreEE", 1, 2)},
            {0xEF, new Command("StoreEF", 1, 2)},
            {0xF0, new Command("0xF0", 3, 2, 2, 2)},
            {0xF1, new Command("StoreF1", 1, 2)},
            {0xF2, new Command("0xF2", 2, 2, 2)},
            {0xF3, new Command("0xF3", 2, 2, 2)},
            {0xF4, new Command("0xF4", 2, 2, 2)},
            {0xF5, new Command("0xF5", 2, 2, 2)},
            {0xF6, new Command("0xF6", 2, 2, 2)},
            {0xF7, new Command("0xF7", 2, 2, 2)},
            {0xF8, new Command("0xF8", 2, 2, 2)},
            {0xF9, new Command("0xF9", 1, 2)},
            {0xFA, new Command("TakeMoney", 1, 2)},
            {0xFB, new Command("CheckMoney", 2, 2, 2)},
            {0xFC, new Command("StorePartyHappiness", 2, 2, 2)},
            {0xFD, new Command("0xFD", 3, 2, 2, 2)},
            {0xFE, new Command("StorePartySpecies", 1, 2)},
            {0xFF, new Command("0xFF", 2, 2, 2)},
            {0x100, new Command("0x100", 0)},
            {0x101, new Command("0x101", 2, 2, 2)},
            {0x102, new Command("StorePartyNotEgg", 2, 2, 2)},
            {0x103, new Command("StorePartyCountMore", 2, 2, 2)},
            {0x104, new Command("HealPokèmon", 0)},
            {0x105, new Command("0x105", 3, 2, 2, 2)},
            {0x106, new Command("0x106", 1, 2)},
            {0x107, new Command("OpenChoosePokemonMenu", 4, 2, 2, 2, 2)},
            {0x108, new Command("0x108", 2, 2, 2)},
            {0x109, new Command("0x109", 4, 2, 2, 2, 2)},
            {0x10A, new Command("0x10A", 3, 2, 2, 2)},
            {0x10B, new Command("0x10B", 3, 2, 2, 2)},
            {0x10C, new Command("GivePokèmon", 4, 2, 2, 2, 2)},
            {0x10D, new Command("StorePokemonPartyAt", 2, 2, 2)},
            {0x10E, new Command("GivePKM (0x10E)", 9, 2, 2, 2, 2, 2, 2, 2, 2, 2)},
            {0x10F, new Command("GiveEgg (0x10F)", 3, 2, 2, 2)},
            {0x110, new Command("StorePokèmonSex", 3, 2, 2, 2)},
            {0x111, new Command("0x111", 0)},
            {0x112, new Command("0x112", 0)},
            {0x113, new Command("0x113", 2, 2, 2)},
            {0x114, new Command("0x114", 2, 2, 2)},
            {0x115, new Command("StorePartyCanLearnMove", 3, 2, 2, 2)},
            {0x116, new Command("0x116", 2, 2, 2)},
            {0x117, new Command("VarValDualCompare117", 4, 2, 2, 2, 2)},
            {0x118, new Command("0x118", 3, 2, 2, 2)},
            {0x119, new Command("0x119", 0)},
            {0x11A, new Command("0x11A", 4, 2, 2, 2, 2)},
            {0x11B, new Command("StorePartyType", 3, 2, 2, 2)},
            {0x11C, new Command("0x11C", 3, 2, 2, 2)},
            {0x11D, new Command("SetFavorite", 1, 2)},
            {0x11E, new Command("0x11E", 1, 2)},
            {0x11F, new Command("0x11F", 3, 2, 1, 2)},
            {0x120, new Command("0x120", 2, 2, 2)},
            {0x121, new Command("0x121", 1, 2)},
            {0x122, new Command("0x122", 2, 2, 2)},
            {0x123, new Command("0x123", 0)},
            {0x124, new Command("0x124", 0)},
            {0x125, new Command("0x125", 0)},
            {0x126, new Command("0x126", 4, 2, 2, 2, 2)},
            {0x127, new Command("0x127", 4, 2, 2, 2, 2)},
            {0x128, new Command("0x128", 1, 2)},
            {0x129, new Command("0x129", 2, 2, 2)},
            {0x12A, new Command("0x12A", 1, 2)},
            {0x12B, new Command("0x12B", 3, 2, 2, 2)},
            {0x12C, new Command("0x12C", 1, 2)},
            {0x12D, new Command("0x12D", 4, 2, 2, 2, 2)},
            {0x12E, new Command("0x12E", 3, 2, 2, 2)},
            {0x12F, new Command("0x12F", 0)},
            {0x130, new Command("BootPCSound", 0)},
            {0x131, new Command("PC-131", 0)},
            {0x132, new Command("0x132", 1, 2)},
            {0x133, new Command("0x133", 0)},
            {0x134, new Command("0x134", 2, 2, 2)},
            {0x135, new Command("0x135", 0)},
            {0x136, new Command("0x136", 1, 1)},
            {0x137, new Command("0x137", 1, 2)},
            {0x138, new Command("0x138", 1, 1)},
            {0x139, new Command("0x139", 1, 1)},
            {0x13A, new Command("0x13A", 1, 1)},
            {0x13B, new Command("0x13B", 1, 2)},
            {0x13C, new Command("0x13C", 0)},
            {0x13D, new Command("0x13D", 0)},
            {0x13E, new Command("0x13E", 0)},
            {0x13F, new Command("StartCameraEvent", 0)},
            {0x140, new Command("StopCameraEvent", 0)},
            {0x141, new Command("LockCamera", 0)},
            {0x142, new Command("ReleaseCamera", 0)},
            {0x143, new Command("MoveCamera", 11, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2)},
            {0x144, new Command("0x144", 1, 2)},
            {0x145, new Command("EndCameraEvent", 0)},
            {0x146, new Command("0x146", 0)},
            {0x147, new Command("ResetCamera", 2, 2, 2)},
            {0x148, new Command("0x148", 8, 2, 2, 2, 2, 2, 2, 2, 2)},
            {0x149, new Command("0x149", 2, 2, 2)},
            {0x14A, new Command("0x14A", 0)},
            {0x14B, new Command("0x14B", 0)},
            {0x14C, new Command("0x14C", 0)},
            {0x14D, new Command("0x14D", 2, 2, 2)},
            {0x14E, new Command("0x14E", 3, 2, 2, 2)},
            {0x14F, new Command("0x14F", 2, 2, 2)},
            {0x150, new Command("0x150", 1, 2)},
            {0x151, new Command("0x151", 2, 2, 2)},
            {0x152, new Command("0x152", 0)},
            {0x153, new Command("0x153", 0)},
            {0x154, new Command("0x154", 2, 2, 2)},
            {0x155, new Command("0x155", 2, 2, 2)},
            {0x156, new Command("0x156", 1, 2)},
            {0x157, new Command("0x157", 0)},
            {0x158, new Command("0x158", 0)},
            {0x159, new Command("0x159", 1, 2)},
            {0x15A, new Command("0x15A", 4, 2, 2, 2, 2)},
            {0x15B, new Command("0x15B", 1, 1)},
            {0x15C, new Command("0x15C", 1, 1)},
            {0x15D, new Command("0x15D", 0)},
            {0x15E, new Command("0x15E", 0)},
            {0x15F, new Command("0x15F", 0)},
            {0x160, new Command("0x160", 0)},
            {0x161, new Command("0x161", 0)},
            {0x162, new Command("0x162", 0)},
            {0x163, new Command("0x163", 2, 2, 1)},
            {0x164, new Command("0x164", 1, 1)},
            {0x165, new Command("0x165", 3, 1, 2, 2)},
            {0x166, new Command("0x166", 3, 1, 2, 2)},
            {0x167, new Command("0x167", 4, 2, 2, 2, 2)},
            {0x168, new Command("0x168", 1, 2)},
            {0x169, new Command("0x169", 1, 2)},
            {0x16A, new Command("0x16A", 2, 2, 2)},
            {0x16B, new Command("PokèmonMenuMusicalFunctions", 4, 2, 2, 2, 2)},
            {0x16C, new Command("0x16C", 1, 2)},
            {0x16D, new Command("0x16D", 0)},
            {0x16E, new Command("0x16E", 0)},
            {0x16F, new Command("0x16F", 0)},
            {0x170, new Command("0x170", 0)},
            {0x171, new Command("0x171", 0)},
            {0x172, new Command("SetVar172", 1, 2)},
            {0x173, new Command("0x173", 0)},
            {0x174, new Command("StartWildBattle", 3, 2, 2, 2)},
            {0x175, new Command("EndWildBattle", 0)},
            {0x176, new Command("WildBattle1", 0)},
            {0x177, new Command("SetVarBattle177", 1, 2)},
            {0x178, new Command("BattleStoreResult", 1, 2)},
            {0x179, new Command("0x179", 0)},
            {0x17A, new Command("0x17A", 1, 2)},
            {0x17B, new Command("0x17B", 1, 2)},
            {0x17C, new Command("0x17C", 1, 2)},
            {0x17D, new Command("0x17D", 1, 2)},
            {0x17E, new Command("0x17E", 1, 2)},
            {0x17F, new Command("0x17F", 1, 2)},
            {0x180, new Command("0x180", 1, 2)},
            {0x181, new Command("0x181", 1, 2)},
            {0x182, new Command("0x182", 1, 2)},
            {0x183, new Command("0x183", 1, 2)},
            {0x184, new Command("0x184", 1, 2)},
            {0x185, new Command("0x185", 1, 2)},
            {0x186, new Command("0x186", 1, 2)},
            {0x187, new Command("0x187", 1, 2)},
            {0x188, new Command("0x188", 1, 2)},
            {0x189, new Command("0x189", 1, 2)},
            {0x18A, new Command("0x18A", 1, 2)},
            {0x18B, new Command("0x18B", 1, 2)},
            {0x18C, new Command("0x18C", 4, 2, 2, 2, 2)},
            {0x18D, new Command("0x18D", 1, 2)},
            {0x18E, new Command("0x18E", 1, 2)},
            {0x18F, new Command("0x18F", 1, 2)},
            {0x190, new Command("0x190", 1, 2)},
            {0x191, new Command("0x191", 1, 2)},
            {0x192, new Command("0x192", 1, 2)},
            {0x193, new Command("0x193", 0)},
            {0x194, new Command("0x194", 0)},
            {0x195, new Command("0x195", 0)},
            {0x196, new Command("0x196", 0)},
            {0x197, new Command("0x197", 0)},
            {0x198, new Command("0x198", 0)},
            {0x199, new Command("0x199", 1, 2)},
            {0x19A, new Command("0x19A", 0)},
            {0x19B, new Command("Animate19B", 1, 2)},
            {0x19C, new Command("0x19C", 1, 2)},
            {0x19D, new Command("0x19D", 0)},
            {0x19E, new Command("0x19E", 1, 2)},
            {0x19F, new Command("CallScreenAnimation", 1, 2)},
            {0x1A0, new Command("0x1A0", 0)},
            {0x1A1, new Command("Xtransciever1 (0x1A1)", 4, 2, 2, 2, 2)},
            {0x1A2, new Command("0x1A2", 0)},
            {0x1A3, new Command("FlashBlackInstant", 0)},
            {0x1A4, new Command("Xtransciever4 (0x1A4)", 0)},
            {0x1A5, new Command("Xtransciever5 (0x1A5)", 0)},
            {0x1A6, new Command("Xtransciever6 (0x1A6)", 3, 2, 2, 2)},
            {0x1A7, new Command("Xtransciever7 (0x1A7)", 0)},
            {0x1A8, new Command("0x1A8", 3, 2, 2, 2)},
            {0x1A9, new Command("0x1A9", 4, 2, 2, 2, 2)},
            {0x1AA, new Command("0x1AA", 4, 2, 2, 2, 2)},
            {0x1AB, new Command("FadeFromBlack", 0)},
            {0x1AC, new Command("FadeIntoBlack", 0)},
            {0x1AD, new Command("FadeFromWhite", 0)},
            {0x1AE, new Command("FadeIntoWhite", 0)},
            {0x1AF, new Command("0x1AF", 2, 2, 2)},
            {0x1B0, new Command("0x1B0", 0)},
            {0x1B1, new Command("E4StatueGoDown", 0)},
            {0x1B2, new Command("0x1B2", 0)},
            {0x1B3, new Command("0x1B3", 0)},
            {0x1B4, new Command("TradeNPCStart", 2, 2, 2)},
            {0x1B5, new Command("TradeNPCQualify", 3, 2, 2, 2)},
            {0x1B6, new Command("0x1B6", 0)},
            {0x1B7, new Command("0x1B7", 0)},
            {0x1B8, new Command("0x1B8", 0)},
            {0x1B9, new Command("0x1B9", 0)},
            {0x1BA, new Command("0x1BA", 2, 2, 2)},
            {0x1BB, new Command("0x1BB", 0)},
            {0x1BC, new Command("0x1BC", 0)},
            {0x1BD, new Command("0x1BD", 2, 2, 2)},
            {0x1BE, new Command("1BE", 2, 2, 2)},
            {0x1BF, new Command("CompareChosenPokemon", 3, 2, 2, 2)},
            {0x1C0, new Command("0x1C0", 0)},
            {0x1C1, new Command("0x1C1", 5, 2, 2, 2, 2, 2)},
            {0x1C2, new Command("StartEventBC", 0)},
            {0x1C3, new Command("EndEventBC", 0)},
            {0x1C4, new Command("StoreTrainerID", 2, 2, 2)},
            {0x1C5, new Command("0x1C5", 1, 2)},
            {0x1C6, new Command("StorePokemonCaughtWF", 3, 2, 2, 2)},
            {0x1C7, new Command("0x1C7", 1, 2)},
            {0x1C8, new Command("0x1C8", 0)},
            {0x1C9, new Command("StoreVarMessage", 2, 2, 2)},
            {0x1CA, new Command("0x1CA", 0)},
            {0x1CB, new Command("0x1CB", 5, 2, 2, 2, 2, 2)},
            {0x1CC, new Command("0x1CC", 1, 2)},
            {0x1CD, new Command("0x1CD", 1, 2)},
            {0x1CE, new Command("0x1CE", 2, 2, 2)},
            {0x1CF, new Command("0x1CF", 1, 2)},
            {0x1D0, new Command("0x1D0", 4, 2, 2, 2, 2)},
            {0x1D1, new Command("0x1D1", 2, 2, 2)},
            {0x1D2, new Command("0x1D2", 3, 2, 2, 2)},
            {0x1D3, new Command("0x1D3", 3, 2, 2, 2)},
            {0x1D4, new Command("0x1D4", 3, 2, 2, 2)},
            {0x1D5, new Command("0x1D5", 2, 2, 2)},
            {0x1D6, new Command("0x1D6", 2, 2, 2)},
            {0x1D7, new Command("0x1D7", 4, 2, 2, 2, 2)},
            {0x1D8, new Command("0x1D8", 4, 2, 2, 2, 2)},
            {0x1D9, new Command("0x1D9", 4, 2, 2, 2, 2)},
            {0x1DA, new Command("0x1DA", 1, 2)},
            {0x1DB, new Command("0x1DB", 1, 2)},
            {0x1DC, new Command("0x1DC", 1, 2)},
            {0x1DD, new Command("0x1DD", 3, 2, 2, 2)},
            {0x1DE, new Command("0x1DE", 2, 2, 2)},
            {0x1DF, new Command("0x1DF", 0)},
            {0x1E0, new Command("0x1E0", 4, 2, 2, 2, 2)},
            {0x1E1, new Command("0x1E3", 0)},
            {0x1E2, new Command("0x1E3", 0)},
            {0x1E3, new Command("0x1E3", 3, 2, 2, 2)},
            {0x1E4, new Command("0x1E4", 4, 2, 2, 2, 2)},
            {0x1E5, new Command("0x1E5", 0)},
            {0x1E6, new Command("0x1E6", 0)},
            {0x1E7, new Command("0x1E7", 0)},
            {0x1E8, new Command("0x1E8", 0)},
            {0x1E9, new Command("0x1E9", 0)},
            {0x1EA, new Command("0x1EA", 4, 2, 2, 2, 2)},
            {0x1EB, new Command("0x1EB", 0)},
            {0x1EC, new Command("SwitchOwPosition", 5, 2, 2, 2, 2, 2)},
            {0x1ED, new Command("0x1ED", 3, 2, 2, 2)},
            {0x1EE, new Command("0x1EE", 2, 2, 2)},
            {0x1EF, new Command("0x1EF", 2, 2, 2)},
            {0x1F0, new Command("0x1F0", 2, 2, 2)},
            {0x1F1, new Command("0x1F1", 0)},
            {0x1F2, new Command("0x1F2", 1, 2)},
            {0x1F3, new Command("0x1F3", 4, 2, 2, 2, 2)},
            {0x1F4, new Command("0x1F4", 2, 2, 2)},
            {0x1F5, new Command("0x1F5", 0)},
            {0x1F6, new Command("0x1F6", 4, 2, 2, 2, 2)},
            {0x1F7, new Command("0x1F7", 6, 2, 2, 2, 2, 2, 2)},
            {0x1F8, new Command("0x1F8", 2, 2, 2)},
            {0x1F9, new Command("0x1F9", 0)},
            {0x1FA, new Command("0x1FA", 0)},
            {0x1FB, new Command("0x1FB", 2, 2, 2)},
            {0x1FC, new Command("0x1FC", 2, 2, 2)},
            {0x1FD, new Command("0x1FD", 0)},
            {0x1FE, new Command("0x1FE", 0)},
            {0x1FF, new Command("0x1FF", 0)},
            {0x200, new Command("0x200", 1, 2)},
            {0x201, new Command("0x201", 0)},
            {0x202, new Command("0x202", 1, 2)},
            {0x203, new Command("0x203", 0)},
            {0x204, new Command("0x204", 0)},
            {0x205, new Command("0x205", 1, 2)},
            {0x206, new Command("0x206", 0)},
            {0x207, new Command("0x207", 2, 2, 2)},
            {0x208, new Command("0x208", 1, 2)},
            {0x209, new Command("0x209", 4, 2, 2, 2, 2)},
            {0x20A, new Command("0x20A", 4, 2, 2, 2, 2)},
            {0x20B, new Command("0x20B", 2, 2, 2)},
            {0x20C, new Command("StorePasswordClown", 4, 2, 2, 2, 2)},
            {0x20D, new Command("0x20D", 0)},
            {0x20E, new Command("0x20E", 2, 2, 2)},
            {0x20F, new Command("0x20F", 3, 2, 2, 2)},
            {0x210, new Command("0x210", 0)},
            {0x211, new Command("0x211", 0)},
            {0x212, new Command("0x212", 0)},
            {0x213, new Command("0x213", 0)},
            {0x214, new Command("0x214", 4, 2, 2, 2, 2)},
            {0x215, new Command("0x215", 2, 2, 2)},
            {0x216, new Command("0x216", 0)},
            {0x217, new Command("0x217", 1, 2)},
            {0x218, new Command("0x218", 2, 2, 2)},
            {0x219, new Command("0x219", 2, 2, 2)},
            {0x21A, new Command("0x21A", 2, 2, 2)},
            {0x21B, new Command("0x21B", 0)},
            {0x21C, new Command("0x21C", 2, 2, 2)},
            {0x21D, new Command("0x21D", 1, 2)},
            {0x21E, new Command("HipWaderPKMGet (0x21E)", 1, 2)},
            {0x21F, new Command("0x21F", 2, 2, 2)},
            {0x220, new Command("0x220", 1, 2)},
            {0x221, new Command("0x221", 2, 2, 2)},
            {0x222, new Command("0x222", 0)},
            {0x223, new Command("StoreHiddenPowerType", 2, 2, 2)},
            {0x224, new Command("0x224", 3, 2, 2, 2)},
            {0x225, new Command("0x225", 0)},
            {0x226, new Command("0x226", 1, 2)},
            {0x227, new Command("0x227", 2, 2, 2)},
            {0x228, new Command("0x228", 0)},
            {0x229, new Command("0x229", 2, 2, 2)},
            {0x22A, new Command("0x22A", 1, 2)},
            {0x22B, new Command("0x22B", 2, 2, 2)},
            {0x22C, new Command("0x22C", 2, 2, 2)},
            {0x22D, new Command("0x22D", 1, 2)},
            {0x22E, new Command("0x22E", 0)},
            {0x22F, new Command("0x22F", 2, 2, 2)},
            {0x230, new Command("0x230", 2, 2, 2)},
            {0x231, new Command("0x231", 1, 2)},
            {0x232, new Command("0x232", 0)},
            {0x233, new Command("0x233", 1, 2)},
            {0x234, new Command("0x234", 2, 2, 2)},
            {0x235, new Command("0x235", 0)},
            {0x236, new Command("0x236", 4, 2, 2, 2, 2)},
            {0x237, new Command("0x237", 2, 2, 2)},
            {0x238, new Command("0x238", 0)},
            {0x239, new Command("0x239", 1, 2)},
            {0x23A, new Command("0x23A", 2, 2, 2)},
            {0x23B, new Command("0x23B", 0)},
            {0x23C, new Command("0x23C", 0)},
            {0x23D, new Command("0x23D", 2, 2, 2)},
            {0x23E, new Command("0x23E", 3, 2, 2, 2)},
            {0x23F, new Command("Close23F", 0)},
            {0x240, new Command("0x240", 0)},
            {0x241, new Command("0x241", 0)},
            {0x242, new Command("0x242", 2, 2, 2)},
            {0x243, new Command("0x243", 0)},
            {0x244, new Command("0x244", 0)},
            {0x245, new Command("0x245", 1, 2)},
            {0x246, new Command("0x246", 1, 2)},
            {0x247, new Command("0x247", 5, 2, 2, 2, 2, 2)},
            {0x248, new Command("0x248", 2, 2, 2)},
            {0x249, new Command("0x249", 4, 2, 2, 2, 2)},
            {0x24A, new Command("0x24A", 2, 2, 2)},
            {0x24B, new Command("0x24B", 0)},
            {0x24C, new Command("0x24C", 1, 2)},
            {0x24D, new Command("0x24D", 0)},
            {0x24E, new Command("0x24E", 2, 2, 2)},
            {0x24F, new Command("0x24F", 6, 2, 2, 2, 2, 2, 2)},
            {0x250, new Command("0x250", 0)},
            {0x251, new Command("0x251", 1, 2)},
            {0x252, new Command("0x252", 1, 2)},
            {0x253, new Command("0x253", 1, 1)},
            {0x254, new Command("0x254", 1, 2)},
            {0x255, new Command("0x255", 0)},
            {0x256, new Command("0x256", 0)},
            {0x257, new Command("0x257", 0)},
            {0x258, new Command("0x258", 0)},
            {0x259, new Command("0x259", 0)},
            {0x25A, new Command("0x25A", 1, 2)},
            {0x25B, new Command("0x25B", 0)},
            {0x25C, new Command("0x25C", 6, 2, 2, 2, 2, 2, 2)},
            {0x25D, new Command("0x25D", 0)},
            {0x25E, new Command("0x25E", 0)},
            {0x25F, new Command("0x25F", 1, 2)},
            {0x260, new Command("0x260", 0)},
            {0x261, new Command("0x261", 0)},
            {0x262, new Command("0x262", 2, 2, 2)},
            {0x263, new Command("0x263", 1, 2)},
            {0x264, new Command("0x264", 0)},
            {0x265, new Command("0x265", 0)},
            {0x266, new Command("0x266", 1, 2)},
            {0x267, new Command("0x267", 0)},
            {0x268, new Command("0x268", 0)},
            {0x269, new Command("0x269", 0)},
            {0x26A, new Command("0x26A", 0)},
            {0x26B, new Command("0x26B", 0)},
            {0x26C, new Command("StoreMedals26C", 2, 1, 2)},
            {0x26D, new Command("StoreMedals26D", 2, 1, 2)},
            {0x26E, new Command("CountMedals26E", 2, 1, 2)},
            {0x26F, new Command("0x26F", 0)},
            {0x270, new Command("0x270", 0)},
            {0x271, new Command("0x271", 2, 2, 2)},
            {0x272, new Command("0x272", 2, 2, 2)},
            {0x273, new Command("0x273", 1, 2)},
            {0x274, new Command("0x274", 0)},
            {0x275, new Command("0x275", 3, 1, 2, 2)},
            {0x276, new Command("0x276", 2, 2, 2)},
            {0x277, new Command("0x277", 0)},
            {0x278, new Command("0x278", 0)},
            {0x279, new Command("0x279", 0)},
            {0x27A, new Command("0x27A", 0)},
            {0x27B, new Command("0x27B", 0)},
            {0x27C, new Command("0x27C", 0)},
            {0x27D, new Command("0x27D", 0)},
            {0x27E, new Command("0x27E", 0)},
            {0x27F, new Command("0x27F", 0)},
            {0x280, new Command("0x280", 0)},
            {0x281, new Command("0x281", 0)},
            {0x282, new Command("0x282", 0)},
            {0x283, new Command("0x283", 2, 1, 1)},
            {0x284, new Command("0x284", 2, 1, 1)},
            {0x285, new Command("0x285", 3, 2, 2, 2)},
            {0x286, new Command("0x286", 0)},
            {0x287, new Command("0x287", 3, 2, 2, 2)},
            {0x288, new Command("0x288", 3, 2, 2, 2)},
            {0x289, new Command("0x289", 1, 2)},
            {0x28A, new Command("0x28A", 0)},
            {0x28B, new Command("0x28B", 0)},
            {0x28C, new Command("0x28C", 0)},
            {0x28D, new Command("0x28D", 0)},
            {0x28E, new Command("0x28E", 0)},
            {0x28F, new Command("0x28F", 0)},
            {0x290, new Command("0x290", 1, 1)},
            {0x291, new Command("0x291", 0)},
            {0x292, new Command("0x292", 1, 1)},
            {0x293, new Command("0x293", 1, 1)},
            {0x294, new Command("0x294", 2, 1, 1)},
            {0x295, new Command("0x290", 0)},
            {0x296, new Command("0x296", 0)},
            {0x297, new Command("0x297", 1, 2)},
            {0x298, new Command("0x298", 0)},
            {0x299, new Command("0x299", 0)},
            {0x29A, new Command("0x29A", 2, 1, 2)},
            {0x29B, new Command("0x29B", 1, 1)},
            {0x29C, new Command("0x29C", 0)},
            {0x29D, new Command("0x29D", 0)},
            {0x29E, new Command("0x29E", 2, 2, 2)},
            {0x29F, new Command("0x29F", 1, 2)},
            {0x2A0, new Command("StoreHasMedal", 2, 2, 2)},
            {0x2A1, new Command("0x2A1", 1, 2)},
            {0x2A2, new Command("0x2A2", 0)},
            {0x2A3, new Command("0x2A3", 0)},
            {0x2A4, new Command("0x2A4", 0)},
            {0x2A5, new Command("0x2A5", 1, 2)},
            {0x2A6, new Command("0x2A6", 0)},
            {0x2A7, new Command("0x2A7", 1, 2)},
            {0x2A8, new Command("0x2A8", 0)},
            {0x2A9, new Command("0x2A9", 0)},
            {0x2AA, new Command("0x2AA", 0)},
            {0x2AB, new Command("0x2AB", 0)},
            {0x2AC, new Command("0x2AC", 0)},
            {0x2AD, new Command("0x2AD", 0)},
            {0x2AE, new Command("0x2AE", 0)},
            {0x2AF, new Command("StoreDifficulty", 1, 2)},
            {0x2B0, new Command("0x2B0", 0)},
            {0x2B1, new Command("0x2B1", 1, 2)},
            {0x2B2, new Command("0x2B2", 2, 2, 2)},
            {0x2B3, new Command("0x2B3", 2, 2, 2)},
            {0x2B4, new Command("0x2B4", 2, 2, 2)},
            {0x2B5, new Command("0x2B5", 2, 2, 2)},
            {0x2B6, new Command("0x2B6", 2, 2, 2)},
            {0x2B7, new Command("0x2B7", 1, 2)},
            {0x2B8, new Command("FollowMeStart (0x2B8)", 0)},
            {0x2B9, new Command("FollowMeEnd (0x2B9)", 0)},
            {0x2BA, new Command("FollowMeCopyStepsTo (0x2BA)", 1, 2)},
            {0x2BB, new Command("0x2BB", 0)},
            {0x2BC, new Command("0x2BC", 2, 2, 2)},
            {0x2BD, new Command("0x2BD", 1, 2)},
            {0x2BE, new Command("0x2BE", 5, 2, 2, 2, 2, 2)},
            {0x2BF, new Command("0x2BF", 0)},
            {0x2C0, new Command("0x2C0", 2, 2, 2)},
            {0x2C1, new Command("0x2C1", 0)},
            {0x2C2, new Command("0x2C2", 0)},
            {0x2C3, new Command("0x2C3", 2, 2, 2)},
            {0x2C4, new Command("0x2C4", 0)},
            {0x2C5, new Command("0x2C5", 1, 2)},
            {0x2C6, new Command("0x2C6", 0)},
            {0x2C7, new Command("0x2C7", 0)},
            {0x2C8, new Command("0x2C8", 0)},
            {0x2C9, new Command("0x2C9", 0)},
            {0x2CA, new Command("0x2CA", 0)},
            {0x2CB, new Command("0x2CB", 1, 2)},
            {0x2CC, new Command("0x2CC", 0)},
            {0x2CD, new Command("0x2CD", 0)},
            {0x2CE, new Command("0x2CE", 0)},
            {0x2CF, new Command("0x2CF", 4, 2, 2, 2, 2)},
            {0x2D0, new Command("***HABITATLISTENABLE***", 0)},
            {0x2D1, new Command("0x2D1", 1, 2)},
            {0x2D2, new Command("0x2D2", 0)},
            {0x2D3, new Command("0x2D3", 0)},
            {0x2D4, new Command("0x2D4", 1, 2)},
            {0x2D5, new Command("0x2D5", 2, 2, 2)},
            {0x2D6, new Command("0x2D6", 0)},
            {0x2D7, new Command("0x2D7", 2, 2, 2)},
            {0x2D8, new Command("0x2D8", 0)},
            {0x2D9, new Command("0x2D9", 2, 2, 2)},
            {0x2DA, new Command("0x2DA", 1, 2)},
            {0x2DB, new Command("0x2DB", 2, 2, 2)},
            {0x2DC, new Command("0x2DC", 3, 2, 2, 2)},
            {0x2DD, new Command("StoreUnityVisitors", 1, 2)},
            {0x2DE, new Command("0x2DE", 0)},
            {0x2DF, new Command("StoreMyActivities", 1, 2)},
            {0x2E0, new Command("0x2E0", 0)},
            {0x2E1, new Command("0x2E1", 0)},
            {0x2E2, new Command("0x2E2", 0)},
            {0x2E3, new Command("0x2E3", 0)},
            {0x2E4, new Command("0x2E4", 0)},
            {0x2E5, new Command("0x2E5", 0)},
            {0x2E6, new Command("0x2E6", 0)},
            {0x2E7, new Command("0x2E7", 0)},
            {0x2E8, new Command("0x2E8", 2, 2, 2)},
            {0x2E9, new Command("0x2E9", 0)},
            {0x2EA, new Command("0x2EA", 0)},
            {0x2EB, new Command("0x2EB", 0)},
            {0x2EC, new Command("0x2EC", 0)},
            {0x2ED, new Command("0x2ED", 2, 2, 2)},
            {0x2EE, new Command("Prop2EE", 2, 2, 2)},
            {0x2EF, new Command("0x2EF", 1, 2)},
            {0x2F0, new Command("0x2F0", 0)},
            {0x2F1, new Command("0x2F1", 1, 2)},
            {0x2F2, new Command("0x2F2", 0)},
            {0x2F3, new Command("0x2F3", 0)},
            {0x2F4, new Command("0x2F4", 0)},
            {0x2F5, new Command("0x2F5", 0)},
            {0x2F6, new Command("0x2F6", 0)},
            {0x2F7, new Command("0x2F7", 0)},
            {0x2F8, new Command("0x2F8", 0)},
            {0x2F9, new Command("0x2F9", 0)},
            {0x2FA, new Command("0x2FA", 0)},
            {0x2FB, new Command("0x2FB", 0)},
            {0x2FC, new Command("0x2FC", 0)},
            {0x2FD, new Command("0x2FD", 0)},
            {0x2FE, new Command("0x2FE", 0)},
            {0x2FF, new Command("0x2FF", 0)},
            {0x300, new Command("0x300", 0)},
            {0x301, new Command("0x301", 0)},
            {0x302, new Command("0x302", 0)},
            {0x303, new Command("0x303", 0)},
            {0x304, new Command("0x304", 0)},
            {0x305, new Command("0x305", 0)},
            {0x306, new Command("0x306", 0)},
            {0x307, new Command("0x307", 0)},
            {0x308, new Command("0x308", 0)},
            {0x309, new Command("0x309", 0)},
            {0x30A, new Command("0x30A", 0)},
            {0x30B, new Command("0x30B", 0)},
            {0x30C, new Command("0x30C", 0)},
            {0x30D, new Command("0x30D", 0)},
            {0x30E, new Command("0x30E", 0)},
            {0x30F, new Command("0x30F", 0)},
            {0x310, new Command("0x310", 0)},
            {0x311, new Command("0x311", 0)},
            {0x312, new Command("0x312", 0)},
            {0x313, new Command("0x313", 0)},
            {0x314, new Command("0x314", 0)},
            {0x315, new Command("0x315", 0)},
            {0x316, new Command("0x316", 0)},
            {0x317, new Command("0x317", 0)},
            {0x318, new Command("0x318", 0)},
            {0x319, new Command("0x319", 0)},
            {0x31A, new Command("0x31A", 0)},
            {0x31B, new Command("0x31B", 0)},
            {0x31C, new Command("0x31C", 0)},
            {0x31D, new Command("0x31D", 0)},
            {0x31E, new Command("0x31E", 0)},
            {0x31F, new Command("0x31F", 0)},
            {0x320, new Command("0x320", 0)},
            {0x321, new Command("0x321", 0)},
            {0x322, new Command("0x322", 0)},
            {0x323, new Command("0x323", 0)},
            {0x324, new Command("0x324", 0)},
            {0x325, new Command("0x325", 0)},
            {0x326, new Command("0x326", 0)},
            {0x327, new Command("0x327", 0)},
            {0x328, new Command("0x328", 0)},
            {0x329, new Command("0x329", 0)},
            {0x32A, new Command("0x32A", 0)},
            {0x32B, new Command("0x32B", 0)},
            {0x32C, new Command("0x32C", 0)},
            {0x32D, new Command("0x32D", 0)},
            {0x32E, new Command("0x32E", 0)},
            {0x32F, new Command("0x32F", 0)},
            {0x330, new Command("0x330", 0)},
            {0x331, new Command("0x331", 0)},
            {0x332, new Command("0x332", 0)},
            {0x333, new Command("0x333", 0)},
            {0x334, new Command("0x334", 0)},
            {0x335, new Command("0x335", 0)},
            {0x336, new Command("0x336", 0)},
            {0x337, new Command("0x337", 0)},
            {0x338, new Command("0x338", 0)},
            {0x339, new Command("0x339", 0)},
            {0x33A, new Command("0x33A", 0)},
            {0x33B, new Command("0x33B", 0)},
            {0x33C, new Command("0x33C", 0)},
            {0x33D, new Command("0x33D", 0)},
            {0x33E, new Command("0x33E", 0)},
            {0x33F, new Command("0x33F", 0)},
            {0x340, new Command("0x340", 0)},
            {0x341, new Command("0x341", 0)},
            {0x342, new Command("0x342", 0)},
            {0x343, new Command("0x343", 0)},
            {0x344, new Command("0x344", 0)},
            {0x345, new Command("0x345", 0)},
            {0x346, new Command("0x346", 0)},
            {0x347, new Command("0x347", 0)},
            {0x348, new Command("0x348", 0)},
            {0x349, new Command("0x349", 0)},
            {0x34A, new Command("0x34A", 0)},
            {0x34B, new Command("0x34B", 0)},
            {0x34C, new Command("0x34C", 0)},
            {0x34D, new Command("0x34D", 0)},
            {0x34E, new Command("0x34E", 0)},
            {0x34F, new Command("0x34F", 0)}
        };
    }

    internal struct Command
    {
        public string name;
        public int numParameters;
        public List<int> parameterBytes;

        public Command(string name, int numParameters, params int[] parameterBytes)
        {
            this.name = name;
            this.numParameters = numParameters;
            this.parameterBytes = new List<int>(parameterBytes);
        }
    }
}

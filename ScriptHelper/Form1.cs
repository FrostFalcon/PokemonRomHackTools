using ScriptHelper.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptHelper
{
    public partial class Form1 : Form
    {
        string loadedFilePath = "";
        List<byte> fileData;
        List<ScriptEntry> scripts;

        public Form1()
        {
            InitializeComponent();
        }

        private void LoadFile(object sender, EventArgs e)
        {
            OpenFileDialog prompt = new OpenFileDialog();

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                //Read all file data to the byte array
                loadedFilePath = prompt.FileName;
                FileStream fileStream = File.OpenRead(loadedFilePath);
                byte[] data = new byte[fileStream.Length];
                fileStream.Read(data, 0, (int)fileStream.Length);
                fileData = new List<byte>(data);
                fileStream.Close();

                RegisterScripts();
            }
        }

        private void SaveFile(object sender, EventArgs e)
        {
            SaveFileDialog prompt = new SaveFileDialog();

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                loadedFilePath = prompt.FileName;
                FileStream fileStream = File.OpenWrite(loadedFilePath);
                fileStream.Write(fileData.ToArray(), 0, fileData.Count);
                fileStream.Close();
            }
        }

        private void RegisterScripts()
        {
            scripts = new List<ScriptEntry>();
            int i = 0;
            while (i + 1 < fileData.Count && !(fileData[i] == 0x13 && fileData[i + 1] == 0xFD))
            {
                scripts.Add(new ScriptEntry() { pointerLocation = i, pointerValue = BitConverter.ToInt32(fileData.ToArray(), i) });
                i += 4;
            }
            if (fileData.Count - i < 4) return;

            scriptIDComboBox.Items.Clear();
            foreach (ScriptEntry s in scripts)
            {
                s.byteLocation = s.pointerValue + 4 * (scripts.IndexOf(s) + 1);
                i = s.pointerValue + 4 * (scripts.IndexOf(s) + 1);

                #region Command Definitions
                while (i < fileData.Count && !(fileData[i] == 0x02 && fileData[i + 1] == 0x00))
                {
                    CommandEntry com = new CommandEntry() { commandID = BitConverter.ToInt16(fileData.ToArray(), i)};
                    i += 2;
                    switch (com.commandID)
                    {
                        case 0x2:
                            com.Name = "End";
                            break;
                        case 0x03:
                            com.Name = "ReturnAfterDelay";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x04:
                            com.Name = "CallRoutine";
                            com.parameters.Add(ReadParameterInt32(ref i));                            //^temp fix for non-working movement detection
                            break;
                        case 0x05:
                            com.Name = "EndFunction";
                            break;
                        case 0x06:
                            com.Name = "Logic06";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x07:
                            com.Name = "Logic07";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x08:
                            com.Name = "CompareTo";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x09:
                            com.Name = "StoreVar";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x0A:
                            com.Name = "ClearVar";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x0B:
                            com.Name = "0B";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x0C:
                            com.Name = "0C";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x0D:
                            com.Name = "0D";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x0E:
                            com.Name = "0E";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x0F:
                            com.Name = "0F";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x10:
                            com.Name = "StoreFlag";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x11:
                            com.Name = "Condition";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x12:
                            com.Name = "0x12";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x13:
                            com.Name = "0x13";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x14:
                            com.Name = "0x14";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x16:
                            com.Name = "0x16";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x17:
                            com.Name = "0x17";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x19:
                            com.Name = "Compare";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1C:
                            com.Name = "CallStd";
                            com.parameters.Add(ReadParameterInt16(ref i));//Standard Function Id.
                            break;
                        case 0x1D:
                            com.Name = "ReturnStd";
                            break;
                        case 0x1E:
                            com.Name = "Jump";
                            com.parameters.Add(ReadParameterInt32(ref i) + i);
                            break;
                        case 0x1F:
                            com.Name = "If";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt32(ref i) + i);
                            break;
                        case 0x21:
                            com.Name = "0x21";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x22:
                            com.Name = "0x22";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x23:
                            com.Name = "SetFlag";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x24:
                            com.Name = "ClearFlag";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x25:
                            com.Name = "SetVarFlagStatus";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x26:
                            com.Name = "SetVar26";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x27:
                            com.Name = "SetVar27";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x28:
                            com.Name = "SetVarEqVal";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Var as container
                            com.parameters.Add(ReadParameterInt16(ref i)); //Value to store
                            break;
                        case 0x29:
                            com.Name = "SetVar29";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Var as container
                            com.parameters.Add(ReadParameterInt16(ref i)); //Value to store
                            break;
                        case 0x2A:
                            com.Name = "SetVar2A";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Var as container
                            com.parameters.Add(ReadParameterInt16(ref i)); //Value to store
                            break;
                        case 0x2B:
                            com.Name = "SetVar2B";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2D:
                            com.Name = "0x2D";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2E:
                            com.Name = "LockAll";
                            break;
                        case 0x2F:
                            com.Name = "UnlockAll";
                            break;
                        case 0x30:
                            com.Name = "WaitMoment";
                            break;
                        case 0x32:
                            com.Name = "WaitButton";
                            break;
                        case 0x33:
                            com.Name = "MusicalMessage";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Message Id
                            break;
                        case 0x34:
                            com.Name = "EventGreyMessage";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Message Id
                            com.parameters.Add(ReadParameterInt16(ref i)); //Bottom/Top View.
                            break;
                        case 0x35:
                            com.Name = "CloseMusicalMessage";
                            break;
                        case 0x36:
                            com.Name = "CloseEventGreyMessage";
                            break;
                        case 0x38:
                            com.Name = "BubbleMessage";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Message Id
                            com.parameters.Add(ReadParameterByte(ref i)); //Bottom/Top View.
                            break;
                        case 0x39:
                            com.Name = "CloseBubbleMessage";
                            break;
                        case 0x3A:
                            com.Name = "ShowMessageAt";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Message Id
                            com.parameters.Add(ReadParameterInt16(ref i)); //X coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); //Y coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); //Y coordinate
                            break;
                        case 0x3B:
                            com.Name = "CloseShowMessageAt";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x3C:
                            com.Name = "Message";
                            com.parameters.Add(ReadParameterByte(ref i)); //Costant
                            com.parameters.Add(ReadParameterByte(ref i)); //Costant
                            com.parameters.Add(ReadParameterInt16(ref i)); //Message Id
                            com.parameters.Add(ReadParameterInt16(ref i)); //NPC Id 
                            com.parameters.Add(ReadParameterInt16(ref i)); //Bottom/Top View.
                            com.parameters.Add(ReadParameterInt16(ref i)); //Message Type
                            break;
                        case 0x3D:
                            com.Name = "Message2";
                            com.parameters.Add(ReadParameterByte(ref i)); //Costant
                            com.parameters.Add(ReadParameterByte(ref i)); //Costant
                            com.parameters.Add(ReadParameterInt16(ref i)); //Message Id
                            com.parameters.Add(ReadParameterInt16(ref i)); //Bottom/Top View.
                            com.parameters.Add(ReadParameterInt16(ref i)); //Message Type
                            break;
                        case 0x3E:
                            com.Name = "CloseMessageKeyPress";
                            break;
                        case 0x3F:
                            com.Name = "CloseMessageKeyPress2";
                            break;
                        case 0x40:
                            com.Name = "MoneyBox";
                            com.parameters.Add(ReadParameterInt16(ref i)); //X coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); //Y coordinate
                            break;
                        case 0x41:
                            com.Name = "CloseMoneyBox";
                            break;
                        case 0x42:
                            com.Name = "UpdateMoneyBox";
                            break;
                        case 0x43:
                            com.Name = "BorderedMessage";
                            com.parameters.Add(ReadParameterInt16(ref i)); //MessageId
                            com.parameters.Add(ReadParameterInt16(ref i)); //Color
                            break;
                        case 0x44:
                            com.Name = "CloseBorderedMessage";
                            break;
                        case 0x45:
                            com.Name = "PaperMessage";
                            com.parameters.Add(ReadParameterInt16(ref i)); //MessageId
                            com.parameters.Add(ReadParameterInt16(ref i)); //Trans. Coordinate
                            break;
                        case 0x46:
                            com.Name = "ClosePaperMessage";
                            break;
                        case 0x47:
                            com.Name = "YesNoBox";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Variable: NO = 0, YES = 1
                            break;
                        case 0x48:
                            if (i + 0xC > fileData.Count) { com.Name = "Error"; break; }
                            com.Name = "Message3";
                            com.parameters.Add(ReadParameterByte(ref i)); //Costant
                            com.parameters.Add(ReadParameterByte(ref i)); //Costant
                            com.parameters.Add(ReadParameterInt16(ref i)); //Message Id
                            com.parameters.Add(ReadParameterInt16(ref i)); //NPC Id 
                            com.parameters.Add(ReadParameterInt16(ref i)); //Bottom/Top View.
                            com.parameters.Add(ReadParameterInt16(ref i)); //Message Type
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x49:
                            com.Name = "DoubleMessage";
                            com.parameters.Add(ReadParameterByte(ref i)); //Costant
                            com.parameters.Add(ReadParameterByte(ref i)); //Costant
                            com.parameters.Add(ReadParameterInt16(ref i)); //Id Message Black
                            com.parameters.Add(ReadParameterInt16(ref i)); //Id Message White
                            com.parameters.Add(ReadParameterInt16(ref i)); //NPC Id 
                            com.parameters.Add(ReadParameterInt16(ref i)); //Bottom/Top View.
                            com.parameters.Add(ReadParameterInt16(ref i)); //Message Type
                            break;
                        case 0x4A:
                            com.Name = "AngryMessage";
                            com.parameters.Add(ReadParameterInt16(ref i)); //MessageId
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i)); //Bottom/Top View.
                            break;
                        case 0x4B:
                            com.Name = "CloseAngryMessage";
                            break;
                        case 0x4C:
                            com.Name = "SetVarHero";
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x4D:
                            com.Name = "SetVarItem";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x4E:
                            com.Name = "0x4E";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x4F:
                            com.Name = "SetVarItem2";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x50:
                            com.Name = "SetVarItem3";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x51:
                            com.Name = "SetVarMove";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x52:
                            com.Name = "SetVarBag";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x53:
                            com.Name = "SetVarPartyPokemon";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x54:
                            com.Name = "SetVarPartyPokemon2";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x55:
                            com.Name = "SetVar????";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x56:
                            com.Name = "SetVarType";             //382
                            com.parameters.Add(ReadParameterByte(ref i));   //text variable to set
                            com.parameters.Add(ReadParameterInt16(ref i)); //type to set
                            break;
                        case 0x57:
                            com.Name = "SetVarPokèmon";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x58:
                            com.Name = "SetVarPokèmon2";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x59:
                            com.Name = "SetVarLocation";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x5A:
                            com.Name = "SetVarPokèmonNick";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x5B:
                            com.Name = "SetVar????";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x5C: // example 654
                            com.Name = "SetVarStoreVal5C";
                            com.parameters.Add(ReadParameterByte(ref i)); // 1 ?
                            com.parameters.Add(ReadParameterInt16(ref i)); // Container to store to
                            com.parameters.Add(ReadParameterInt16(ref i)); // Stat to Store [HP ATK DEF SPE SPA SPD, 0-5]
                            break;
                        case 0x5D:
                            com.Name = "SetVarMusicalInfo";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x5E:
                            com.Name = "SetVarNations";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x5F:
                            com.Name = "SetVarActivities";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x60:
                            com.Name = "SetVarPower";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x61:
                            com.Name = "SetVarTrainerType";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x62:
                            com.Name = "SetVarTrainerType2";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x63:
                            com.Name = "SetVarGeneralWord";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x64:
                            com.Name = "ApplyMovement";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt32(ref i) + i);
                            break;
                        case 0x65:
                            com.Name = "WaitMovement";
                            break;
                        case 0x66:
                            com.Name = "StoreHeroPosition 0x66";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x67:
                            com.Name = "0x67";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            uint variable = 0;
                            do
                            {
                                variable = (uint)ReadParameterInt16(ref i);
                                if (variable < 0x8000)
                                    i -= 2;
                                else
                                    com.parameters.Add((int)variable);
                            }
                            while (variable > 0x8000);

                            break;
                        case 0x68:
                            com.Name = "StoreHeroPosition";
                            com.parameters.Add(ReadParameterInt16(ref i)); // Variable as X container.
                            com.parameters.Add(ReadParameterInt16(ref i)); // Variable as Y container.
                            break;
                        case 0x69:
                            com.Name = "StoreNPCPosition";
                            com.parameters.Add(ReadParameterInt16(ref i)); // NPC Id
                            com.parameters.Add(ReadParameterInt16(ref i)); // Variable as X container.
                            com.parameters.Add(ReadParameterInt16(ref i)); // Variable as Y container.
                            break;
                        case 0x6A:
                            com.Name = "6A";
                            com.parameters.Add(ReadParameterInt16(ref i)); //NPC Id
                            com.parameters.Add(ReadParameterInt16(ref i)); //Flag
                            break;
                        case 0x6B:
                            com.Name = "AddNPC";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Npc Id
                            break;
                        case 0x6C:
                            com.Name = "RemoveNPC";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Npc Id
                            break;
                        case 0x6D:
                            com.Name = "SetOWPosition";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Npc Id
                            com.parameters.Add(ReadParameterInt16(ref i)); //X coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); //Y coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); //Z coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); //Face direction
                            break;
                        case 0x6E:
                            com.Name = "0x6E";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x6F:
                            com.Name = "0x6F";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x70:
                            com.Name = "0x70";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x71:
                            com.Name = "0x71";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x72:
                            com.Name = "0x72";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x73:
                            com.Name = "0x73";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x74:
                            com.Name = "FacePlayer";
                            break;
                        case 0x75:
                            com.Name = "Release";
                            com.parameters.Add(ReadParameterInt16(ref i)); //NPC Id
                            break;
                        case 0x76:
                            com.Name = "ReleaseAll";
                            break;
                        case 0x77:
                            com.Name = "Lock";
                            com.parameters.Add(ReadParameterInt16(ref i)); //NPC Id
                            break;
                        case 0x78:
                            com.Name = "0x78";
                            com.parameters.Add(ReadParameterInt16(ref i)); //variable
                            break;
                        case 0x79:
                            com.Name = "0x79";
                            com.parameters.Add(ReadParameterInt16(ref i)); //NPC Id
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x7B:
                            com.Name = "MoveNpctoCoordinates";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Npc Id
                            com.parameters.Add(ReadParameterInt16(ref i)); //X coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); //Y coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); //Z coordinate
                            break;
                        case 0x7C:
                            com.Name = "0x7C";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x7D:
                            com.Name = "0x7D";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x7E:
                            com.Name = "TeleportUpNPc";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Npc Id
                            break;
                        case 0x7F:
                            com.Name = "0x7F";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x80:
                            com.Name = "0x80";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x81:
                            com.Name = "0x81";
                            break;
                        case 0x82:
                            com.Name = "0x82";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x83:
                            com.Name = "SetVar0x83";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x84:
                            com.Name = "SetVar0x83";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x85:
                            com.Name = "SingleTrainerBattle";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Trainer Id
                            com.parameters.Add(ReadParameterInt16(ref i)); //2th Trainer Id (If 0x0 Single Battle)
                            com.parameters.Add(ReadParameterInt16(ref i)); //win loss logic (0 standard, 1 loss=>win)
                            break;
                        case 0x86:
                            com.Name = "DoubleTrainerBattle";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Ally
                            com.parameters.Add(ReadParameterInt16(ref i)); //Opp1 Trainer Id
                            com.parameters.Add(ReadParameterInt16(ref i)); //Opp2 Trainer Id
                            com.parameters.Add(ReadParameterInt16(ref i)); //win loss logic (0 standard, 1 loss=>win)
                            break;
                        case 0x87:
                            com.Name = "0x87";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x88:
                            com.Name = "0x88";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x8A:
                            com.Name = "0x8A";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x8B:
                            com.Name = "PlayTrainerMusic";
                            com.parameters.Add(ReadParameterInt16(ref i)); // music to play
                            break;
                        case 0x8C:
                            com.Name = "EndBattle";
                            break;
                        case 0x8D:
                            com.Name = "StoreBattleResult";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Variable as container.
                            break;
                        case 0x8E:
                            com.Name = "DisableTrainer";
                            break;
                        case 0x90:
                            com.Name = "dvar90";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x92:
                            com.Name = "dvar92";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x93:
                            com.Name = "dvar93";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x94:
                            com.Name = "TrainerBattle";
                            com.parameters.Add(ReadParameterInt16(ref i));//Trainer ID
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x95:
                            com.Name = "DeactiveTrainerId";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x96:
                            com.Name = "0x96";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Trainer ID
                            break;
                        case 0x97:
                            com.Name = "StoreActiveTrainerId";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Trainer ID
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x98:
                            com.Name = "ChangeMusic";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x9E:
                            com.Name = "FadeToDefaultMusic";
                            break;
                        case 0x9F:
                            com.Name = "0x9F";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xA2:
                            com.Name = "0xA2";
                            com.parameters.Add(ReadParameterInt16(ref i)); //sound?
                            com.parameters.Add(ReadParameterInt16(ref i)); //strain
                            break;
                        case 0xA3:
                            com.Name = "0xA3";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xA4:
                            com.Name = "0xA4";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xA5:
                            com.Name = "0xA5";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xA6:
                            com.Name = "PlaySound";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Sound Id
                            break;
                        case 0xA7:
                            com.Name = "WaitSoundA7";
                            break;
                        case 0xA8:
                            com.Name = "WaitSound";
                            break;
                        case 0xA9:
                            com.Name = "PlayFanfare";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Fanfare Id
                            break;
                        case 0xAA:
                            com.Name = "WaitFanfare";
                            break;
                        case 0xAB:
                            com.Name = "Cry";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Pokemon Index #
                            com.parameters.Add(ReadParameterInt16(ref i)); //0 ~ unknown
                            break;
                        case 0xAC:
                            com.Name = "WaitCry";
                            break;
                        case 0xAF:
                            com.Name = "SetTextScriptMessage";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Message Id
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xB0:
                            com.Name = "CloseMulti";
                            break;
                        case 0xB1:
                            com.Name = "0xB1";
                            break;
                        case 0xB2:
                            com.Name = "Multi2";            //88
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i)); //variable to store result in

                            break;
                        case 0xB3:
                            com.Name = "FadeScreen";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xB4:
                            com.Name = "ResetScreen";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xB5:
                            com.Name = "Screen0xB5";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xB6:
                            com.Name = "TakeItem";
                            com.parameters.Add(ReadParameterInt16(ref i)); // Item Index Number
                            com.parameters.Add(ReadParameterInt16(ref i)); // Quantity
                            com.parameters.Add(ReadParameterInt16(ref i)); // Return Result (0=added successfully | 1=full bag)
                            break;
                        case 0xB7:
                            com.Name = "CheckItemBagSpace";     //Store if it is possible to give an item.
                            com.parameters.Add(ReadParameterInt16(ref i)); // Item Index Number
                            com.parameters.Add(ReadParameterInt16(ref i)); // Quantity
                            com.parameters.Add(ReadParameterInt16(ref i)); // Return Result (0=not full | 1=full)
                            break;
                        case 0xB8:
                            com.Name = "CheckItemBagNumber";              //222
                            com.parameters.Add(ReadParameterInt16(ref i)); // Item #
                            com.parameters.Add(ReadParameterInt16(ref i)); // Minimum Quantity / Return X if has >=1
                            com.parameters.Add(ReadParameterInt16(ref i)); // Result Storage variable/container
                            break;
                        case 0xB9:
                            com.Name = "StoreItemCount";
                            com.parameters.Add(ReadParameterInt16(ref i)); // item #
                            com.parameters.Add(ReadParameterInt16(ref i)); // Return to storage
                            break;
                        case 0xBA:
                            com.Name = "0xBA";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xBB:
                            com.Name = "0xBB";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xBC:
                            com.Name = "0xBC";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xBE:
                            com.Name = "Warp";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Map Id
                            com.parameters.Add(ReadParameterInt16(ref i)); // X coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); // Y coordinate
                            break;
                        case 0xBF:
                            com.Name = "TeleportWarp";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Map Id
                            com.parameters.Add(ReadParameterInt16(ref i)); // X coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); // Y coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); // Z coordinate
                            break;
                        case 0xC1:
                            com.Name = "FallWarp";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Map Id
                            com.parameters.Add(ReadParameterInt16(ref i)); // X coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); // Y coordinate
                            break;
                        case 0xC2:
                            com.Name = "FastWarp";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Map Id
                            com.parameters.Add(ReadParameterInt16(ref i)); // X coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); // Y coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); // Hero's Facing
                            break;
                        case 0xC3:
                            com.Name = "UnionWarp"; // warp to union room
                            break;
                        case 0xC4:
                            com.Name = "TeleportWarp";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Map Id
                            com.parameters.Add(ReadParameterInt16(ref i)); // X coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); // Y coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); // Z coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); // Hero's Facing
                            break;
                        case 0xC5:
                            com.Name = "SurfAnimation";
                            break;
                        case 0xC6:
                            com.Name = "SpecialAnimation";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xC7:
                            com.Name = "SpecialAnimation2";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xC8:
                            com.Name = "CallAnimation";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Animation Id
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xCB:
                            com.Name = "StoreRandomNumber";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xCC:
                            com.Name = "StoreVarItem";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xCD:
                            com.Name = "StoreVar0xCD";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xCE:
                            com.Name = "StoreVar0xCE";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xCF:
                            com.Name = "StoreVar0xCF";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xD0:
                            com.Name = "StoreDate";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Month Return to var/cont
                            com.parameters.Add(ReadParameterInt16(ref i)); //Day Return to var/cont
                            break;
                        case 0xD1:
                            com.Name = "Store0xD1";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xD2:
                            com.Name = "Store0xD2";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xD3:
                            com.Name = "Store0xD3";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xD4:
                            com.Name = "StoreBirthDay";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xD5:
                            com.Name = "StoreBadge";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Variable to return
                            com.parameters.Add(ReadParameterInt16(ref i)); //Badge Id
                            break;
                        case 0xD6:
                            com.Name = "SetBadge";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Badge Id
                            break;
                        case 0xD7:
                            com.Name = "StoreBadgeNumber";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xDA:
                            com.Name = "0xDA";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xDB:
                            com.Name = "0xDB";
                            break;
                        case 0xDC:
                            com.Name = "0xDC";
                            break;
                        case 0xDD:
                            com.Name = "0xDD";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;

                        case 0xDE:
                            com.Name = "SpeciesDisplayDE"; // species display popup, Store
                            com.parameters.Add(ReadParameterInt16(ref i)); //0
                            com.parameters.Add(ReadParameterInt16(ref i)); //species
                            break;
                        case 0xE0:
                            com.Name = "StoreVersion";
                            com.parameters.Add(ReadParameterInt16(ref i)); //return result to this variable/cont
                            break;
                        case 0xE1:
                            com.Name = "StoreHeroGender";
                            com.parameters.Add(ReadParameterInt16(ref i)); //return result to this variable/cont
                            break;
                        case 0xE4:
                            com.Name = "0xE4";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xE5:
                            com.Name = "StoreE5";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xE7:
                            com.Name = "ActivateRelocator";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xEA:
                            com.Name = "StoreEA";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xEB:
                            com.Name = "StoreEB";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xEC:
                            com.Name = "StoreEC";
                            break;
                        case 0xED:
                            com.Name = "StoreED";
                            break;
                        case 0xEE:
                            com.Name = "StoreEE";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xEF:
                            com.Name = "StoreEF";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xF0:
                            com.Name = "0xF0";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xF1:
                            com.Name = "StoreF1";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xF2:
                            com.Name = "0xF2";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xF3:
                            com.Name = "0xF3";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xF4:
                            com.Name = "0xF4";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xF5:
                            com.Name = "0xF5";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xF6:
                            com.Name = "0xF6";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xF7:
                            com.Name = "0xF7";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xF8:
                            com.Name = "0xF8";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xF9:
                            com.Name = "0xF9";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xFA:
                            com.Name = "TakeMoney";         //66
                            com.parameters.Add(ReadParameterInt16(ref i)); //Removes this amount of money from the player's $.
                            break;
                        case 0xFB:
                            com.Name = "CheckMoney";            //66
                            com.parameters.Add(ReadParameterInt16(ref i)); //Result storage container (0=enough $|1=not enough $)
                            com.parameters.Add(ReadParameterInt16(ref i)); //Stores if current $ is >= [THIS ARGUMENT]
                            break;
                        case 0xFC:
                            com.Name = "StorePartyHappiness";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Happiness storage container
                            com.parameters.Add(ReadParameterInt16(ref i)); //Party member to Store
                            break;
                        case 0xFD:
                            com.Name = "0xFD";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0xFE:
                            com.Name = "StorePartySpecies";
                            if (i == fileData.Count) //this is temporary to catch movement related errors
                            {
                                break;
                            }
                            com.parameters.Add(ReadParameterInt16(ref i));    // Result Storage of Storeed species index #
                            if (i + 1 >= fileData.Count)
                            {
                                break;
                            }
                            else
                            {
                                com.parameters.Add(ReadParameterInt16(ref i)); // PKM to Store
                                break;
                            }
                        case 0xFF:
                            com.Name = "0xFF";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            var nextFF = ReadParameterInt16(ref i);
                            while (nextFF >= 0x4000) { com.parameters.Add(nextFF); nextFF = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x101:
                            com.Name = "0x101";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x102:
                            com.Name = "StorePartyNotEgg";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Result storage container
                            com.parameters.Add(ReadParameterInt16(ref i)); //Party member to Store
                            break;
                        case 0x103:
                            com.Name = "StorePartyCountMore";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Result Storage container
                            com.parameters.Add(ReadParameterInt16(ref i)); //Does the player have more than [VALUE]? Return 0 if true.
                            break;
                        case 0x104:
                            com.Name = "HealPokèmon";
                            break;
                        case 0x105:
                            com.Name = "0x105";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x106:
                            com.Name = "0x106";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Req
                            break;
                        case 0x107:
                            com.Name = "OpenChoosePokemonMenu";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Dialog Result Logic (1 if PKM Chosen) default 0
                            com.parameters.Add(ReadParameterInt16(ref i)); //    \->Variable Storage
                            com.parameters.Add(ReadParameterInt16(ref i)); //Pokemon Choice Variable Storage
                            com.parameters.Add(ReadParameterInt16(ref i)); //limits on choices? default 0
                            break;
                        case 0x108:
                            com.Name = "0x108";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x109:
                            com.Name = "0x109";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x10A:
                            com.Name = "0x10A";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x10B:
                            com.Name = "0x10B";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x10C:
                            com.Name = "GivePokèmon";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Id Pokèmon
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i)); //Item
                            com.parameters.Add(ReadParameterInt16(ref i)); //Level
                            break;
                        case 0x10D:
                            com.Name = "StorePokemonPartyAt";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Id Pokèmon Party
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x10E:
                            com.Name = "GivePKM (0x10E)";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Variable to return result to
                            com.parameters.Add(ReadParameterInt16(ref i)); //Egg Pokemon to try to
                            com.parameters.Add(ReadParameterInt16(ref i)); //Forme
                            com.parameters.Add(ReadParameterInt16(ref i)); //Level
                            com.parameters.Add(ReadParameterInt16(ref i)); //3
                            com.parameters.Add(ReadParameterInt16(ref i)); //2
                            com.parameters.Add(ReadParameterInt16(ref i)); //0
                            com.parameters.Add(ReadParameterInt16(ref i)); //0
                            com.parameters.Add(ReadParameterInt16(ref i)); //4
                            break;
                        case 0x10F:
                            com.Name = "GiveEgg (0x10F)";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Variable to return result to
                            com.parameters.Add(ReadParameterInt16(ref i)); //Egg Pokemon to try to
                            com.parameters.Add(ReadParameterInt16(ref i)); //Response if Party is Full (~0=true or FORME?)
                            break;
                        case 0x110:
                            com.Name = "StorePokèmonSex";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x113:
                            com.Name = "0x113";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x114:
                            com.Name = "0x114";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i)); //RV
                            break;
                        case 0x115:
                            com.Name = "StorePartyCanLearnMove";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Variable to return result to
                            com.parameters.Add(ReadParameterInt16(ref i)); //move to Store
                            com.parameters.Add(ReadParameterInt16(ref i)); //Party member to Store
                            break;
                        case 0x116:
                            com.Name = "0x116";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i)); //RV
                            break;
                        case 0x117:
                            com.Name = "VarValDualCompare117";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            com.parameters.Add(ReadParameterInt16(ref i)); //val
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            com.parameters.Add(ReadParameterInt16(ref i)); //val
                            break;
                        case 0x118:
                            com.Name = "0x118";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i)); //RV
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x11A:
                            com.Name = "0x11A";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x11B:
                            com.Name = "StorePartyType";
                            com.parameters.Add(ReadParameterInt16(ref i)); // Return Type 1
                            com.parameters.Add(ReadParameterInt16(ref i)); // Return Type 2
                            com.parameters.Add(ReadParameterInt16(ref i)); // Party member to Store
                            break;
                        case 0x11C:
                            com.Name = "0x11C";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x11D:
                            com.Name = "SetFavorite";           //82
                            com.parameters.Add(ReadParameterInt16(ref i)); //Party member to set as favorite Pokemon
                            break;
                        case 0x11E:
                            com.Name = "0x11E";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x11F:
                            com.Name = "0x11F";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x120:
                            com.Name = "0x120";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x121:
                            com.Name = "0x121";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x122:
                            com.Name = "0x122";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x126:
                            com.Name = "0x126";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x127:
                            com.Name = "0x127";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x128:
                            com.Name = "0x128";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x129:
                            com.Name = "0x129";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x12A:
                            com.Name = "0x12A";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x12B:
                            com.Name = "0x12B";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Req
                            com.parameters.Add(ReadParameterInt16(ref i)); //Req
                            com.parameters.Add(ReadParameterInt16(ref i)); //Req
                            break;
                        case 0x12C:
                            com.Name = "0x12C";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x12D:
                            com.Name = "0x12D";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i)); //0
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x12E:
                            com.Name = "0x12E";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x130:
                            com.Name = "BootPCSound";
                            break;
                        case 0x131:
                            com.Name = "PC-131";
                            break;
                        case 0x132:
                            com.Name = "0x132";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x134:
                            com.Name = "0x134";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x136:
                            com.Name = "0x136";
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x137:
                            com.Name = "0x137";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x138:
                            com.Name = "0x138";
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x139:
                            com.Name = "0x139";
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x13A:
                            com.Name = "0x13A";
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x13B:
                            com.Name = "0x13B";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x13C:
                            com.Name = "0x13C";
                            break;
                        case 0x13F:
                            com.Name = "StartCameraEvent";
                            break;
                        case 0x140:
                            com.Name = "StopCameraEvent";
                            break;
                        case 0x141:
                            com.Name = "LockCamera";
                            break;
                        case 0x142:
                            com.Name = "ReleaseCamera";
                            break;
                        case 0x143:
                            com.Name = "MoveCamera";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Elevation
                            com.parameters.Add(ReadParameterInt16(ref i)); //Rotation
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i)); //Zoom
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x144:
                            com.Name = "0x144";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Elevation
                            break;
                        case 0x145:
                            com.Name = "EndCameraEvent";
                            break;
                        case 0x147:
                            com.Name = "ResetCamera";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x148:
                            com.Name = "0x148";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x149:
                            com.Name = "0x149";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x14B:
                            com.Name = "0x14B";
                            break;
                        case 0x14A:
                            com.Name = "0x14A";
                            break;
                        case 0x14D:
                            com.Name = "0x14D";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x14E:
                            com.Name = "0x14E";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x14F:
                            com.Name = "0x14F";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x150:
                            com.Name = "0x150";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x151:
                            com.Name = "0x151";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x154:
                            com.Name = "0x154";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x155:
                            com.Name = "0x155";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x156:
                            com.Name = "0x156";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x159:
                            com.Name = "0x159";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x15A:
                            com.Name = "0x15A";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x15B:
                            com.Name = "0x15B";
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x15C:
                            com.Name = "0x15C";
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x163:
                            com.Name = "0x163";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x164:
                            com.Name = "0x164";
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x165:
                            com.Name = "0x165";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x166:
                            com.Name = "0x166";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x167:
                            com.Name = "0x167";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x168:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x169:
                            com.Name = "0x169";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x16A:
                            com.Name = "0x16A";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x16B:
                            com.Name = "PokèmonMenuMusicalFunctions";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x16C:
                            com.Name = "0x16C";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x16D:
                            com.Name = "0x16D";
                            break;
                        case 0x16E:
                            com.Name = "0x16E";
                            break;
                        case 0x172:
                            com.Name = "SetVar172";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Variable to Set
                            break;
                        case 0x174:
                            com.Name = "StartWildBattle";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Dex
                            com.parameters.Add(ReadParameterInt16(ref i)); //Level
                            com.parameters.Add(ReadParameterInt16(ref i)); //Unk
                            break;
                        case 0x175:
                            com.Name = "EndWildBattle";
                            break;
                        case 0x176:
                            com.Name = "WildBattle1";
                            break;
                        case 0x177:
                            com.Name = "SetVarBattle177";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x178:
                            com.Name = "BattleStoreResult";      // 364	0=captured, might output 1 & 2 for something else
                            com.parameters.Add(ReadParameterInt16(ref i)); //variable to store result to
                            break;
                        case 0x179:
                            com.Name = "0x179";
                            break;
                        case 0x17A:
                            com.Name = "0x17A";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x17B:
                            com.Name = "0x17B";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x17C:
                            com.Name = "0x17C";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x17D:
                            com.Name = "0x17D";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x17E:
                            com.Name = "0x17E";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x17F:
                            com.Name = "0x17F";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x180:
                            com.Name = "0x180";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x181:
                            com.Name = "0x181";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x182:
                            com.Name = "0x182";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x183:
                            com.Name = "0x183";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x184:
                            com.Name = "0x184";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x185:
                            com.Name = "0x185";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x186:
                            com.Name = "0x186";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x187:
                            com.Name = "0x187";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x188:
                            com.Name = "0x188";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x189:
                            com.Name = "0x189";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x18A:
                            com.Name = "0x18A";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x18B:
                            com.Name = "0x18B";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x18C:
                            com.Name = "0x18C";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x18D:
                            com.Name = "0x18D";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x18E:
                            com.Name = "0x18E";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x18F:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x190:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x191:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x192:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x193:
                            com.Name = "0x193";
                            break;
                        case 0x195:
                            com.Name = "0x195";
                            break;
                        case 0x199:
                            com.Name = "0x199";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x19B:
                            com.Name = "Animate19B";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x19C:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x19E:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x19F:
                            com.Name = "CallScreenAnimation";
                            com.parameters.Add(ReadParameterInt16(ref i)); //AnimationId
                            break;
                        case 0x1A1:
                            com.Name = "Xtransciever1 (0x1A1)";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i)); // container
                            break;
                        case 0x1A3:
                            com.Name = "FlashBlackInstant";
                            break;
                        case 0x1A4:
                            com.Name = "Xtransciever4 (0x1A4)";
                            break;
                        case 0x1A5:
                            com.Name = "Xtransciever5 (0x1A5)";
                            break;
                        case 0x1A6:
                            com.Name = "Xtransciever6 (0x1A6)";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1A7:
                            com.Name = "Xtransciever7 (0x1A7)";
                            break;
                        case 0x1A8:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1A9:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1AA:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1AB:
                            com.Name = "FadeFromBlack";
                            break;
                        case 0x1AC:
                            com.Name = "FadeIntoBlack";
                            break;
                        case 0x1AD:
                            com.Name = "FadeFromWhite";
                            break;
                        case 0x1AE:
                            com.Name = "FadeIntoWhite";
                            break;
                        case 0x1AF:
                            com.Name = "0x1AF";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1B1:
                            com.Name = "E4StatueGoDown";
                            break;
                        case 0x1B4:
                            com.Name = "TradeNPCStart";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Trade Entry to Trade For
                            com.parameters.Add(ReadParameterInt16(ref i)); //PKM Slot that gets traded away FOREVER!
                            break;
                        case 0x1B5:
                            com.Name = "TradeNPCQualify";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Logic Criterion (usually set to 0~true)
                            com.parameters.Add(ReadParameterInt16(ref i)); //Trade Criterion
                            com.parameters.Add(ReadParameterInt16(ref i)); //Offered PKM
                            break;
                        case 0x1BA:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1BD:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1BE:
                            com.Name = "1BE";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1BF:
                            com.Name = "CompareChosenPokemon";
                            com.parameters.Add(ReadParameterInt16(ref i));//RV = True if is equal
                            com.parameters.Add(ReadParameterInt16(ref i));//Id chosen Pokèmon
                            com.parameters.Add(ReadParameterInt16(ref i));//Id requested Pokèmon
                            break;
                        case 0x1C1:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1C2:
                            com.Name = "StartEventBC";
                            break;
                        case 0x1C3:
                            com.Name = "EndEventBC";
                            break;
                        case 0x1C4:
                            com.Name = "StoreTrainerID";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1C5:
                            com.Name = "0x1C5";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            var next1C5 = ReadParameterInt16(ref i);
                            while (next1C5 >= 0x4000) { com.parameters.Add(next1C5); next1C5 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x1C6:
                            com.Name = "StorePokemonCaughtWF";
                            com.parameters.Add(ReadParameterInt16(ref i)); //True if is Pokèmon searched
                            com.parameters.Add(ReadParameterInt16(ref i)); //True if is caught the same day
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1C7:
                            com.Name = "0x1C7";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1C9:
                            com.Name = "StoreVarMessage";
                            com.parameters.Add(ReadParameterInt16(ref i)); //Variable as Container
                            com.parameters.Add(ReadParameterInt16(ref i)); //Message Id
                            break;
                        case 0x1CB:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1CC:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1CD:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1CE:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1CF:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            var next1CF = ReadParameterInt16(ref i);
                            while (next1CF >= 0x4000) { com.parameters.Add(next1CF); next1CF = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x1D0:
                            com.Name = "0x1D0";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1D1:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1D2:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1D3:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1D4:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1D5:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1D6:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1D7:
                            com.Name = "0x1D7";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1D8:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1D9:
                            com.Name = "0x1D9";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1DA:
                            com.Name = "0x1DA";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            var next1DA = ReadParameterInt16(ref i);
                            while (next1DA >= 0x4000) { com.parameters.Add(next1DA); next1DA = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x1DB:
                            com.Name = "0x1DB";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1DC:
                            com.Name = "0x1DC";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1DD:
                            com.Name = "0x1DD";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1DE:
                            com.Name = "0x1DE";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1DF:
                            com.Name = "0x1DF";
                            break;
                        case 0x1E0:
                            com.Name = "0x1E0";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1E1:
                            com.Name = "0x1E3";
                            break;
                        case 0x1E2:
                            com.Name = "0x1E3";
                            break;
                        case 0x1E3:
                            com.Name = "0x1E3";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1E4:
                            com.Name = "0x1E4";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            var next1E4 = ReadParameterInt16(ref i);
                            while (next1E4 >= 0x4000) { com.parameters.Add(next1E4); next1E4 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x1E5:
                            com.Name = "0x1E5";
                            break;
                        case 0x1E9:
                            com.Name = "0x1E9";
                            var next1E9 = ReadParameterInt16(ref i);
                            while (next1E9 >= 0x4000) { com.parameters.Add(next1E9); next1E9 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x1EA:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1EC:
                            com.Name = "SwitchOwPosition";
                            com.parameters.Add(ReadParameterInt16(ref i)); //NPC Id
                            com.parameters.Add(ReadParameterInt16(ref i)); //NPC Id
                            com.parameters.Add(ReadParameterInt16(ref i)); //X Coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); //Y Coordinate
                            com.parameters.Add(ReadParameterInt16(ref i)); //Z Coordinate
                            break;
                        case 0x1ED:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1EE:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1EF:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1F0:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1F2:
                            com.Name = "0x1F2";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1F4:
                            com.Name = "0x1F4";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1F3:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1F6:
                            com.parameters.Add(ReadParameterInt16(ref i)); // 0
                            com.parameters.Add(ReadParameterInt16(ref i)); // 0
                            com.parameters.Add(ReadParameterInt16(ref i)); // 0
                            com.parameters.Add(ReadParameterInt16(ref i)); // var
                            break;
                        case 0x1F7:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1F8:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1FA:
                            var next1FA = ReadParameterInt16(ref i);
                            while (next1FA >= 0x4000) { com.parameters.Add(next1FA); next1FA = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x1FB:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x1FC:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            var next1FC = ReadParameterInt16(ref i);
                            while (next1FC >= 0x4000) { com.parameters.Add(next1FC); next1FC = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x200:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x202:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x205:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x207:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x208:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x209:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x20A:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x20B:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x20C:
                            com.Name = "StorePasswordClown";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i)); //True if inserted password is exact.
                            break;
                        case 0x20D:
                            com.Name = "0x20D";
                            break;
                        case 0x20E:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x20F:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x214:
                            com.Name = "0x214";
                            com.parameters.Add(ReadParameterInt16(ref i)); // species
                            com.parameters.Add(ReadParameterInt16(ref i)); //0
                            com.parameters.Add(ReadParameterInt16(ref i)); //0
                            com.parameters.Add(ReadParameterInt16(ref i)); //0
                            break;
                        case 0x215:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x217:
                            com.Name = "0x217";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x218:
                            com.Name = "0x218";
                            com.parameters.Add(ReadParameterInt16(ref i));//val
                            com.parameters.Add(ReadParameterInt16(ref i));//var
                            break;
                        case 0x219:
                            com.Name = "0x219";
                            com.parameters.Add(ReadParameterInt16(ref i));//var
                            com.parameters.Add(ReadParameterInt16(ref i));//val
                            break;
                        case 0x21A:
                            com.Name = "0x21A";
                            com.parameters.Add(ReadParameterInt16(ref i));//var
                            com.parameters.Add(ReadParameterInt16(ref i));//val
                            break;
                        case 0x21C:
                            com.Name = "0x21C";
                            com.parameters.Add(ReadParameterInt16(ref i));//var
                            com.parameters.Add(ReadParameterInt16(ref i));//val
                            break;
                        case 0x21D:
                            com.Name = "0x21D";
                            com.parameters.Add(ReadParameterInt16(ref i));//var
                            break;
                        case 0x21E:
                            com.Name = "HipWaderPKMGet (0x21E)";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x21F:
                            com.Name = "0x21F";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x220:
                            com.Name = "0x220";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            var next220 = ReadParameterInt16(ref i);
                            while (next220 >= 0x4000) { com.parameters.Add(next220); next220 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x221:
                            com.Name = "0x221";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));//var
                            break;
                        case 0x223:
                            com.Name = "StoreHiddenPowerType";          // ex 382
                            com.parameters.Add(ReadParameterInt16(ref i)); //Storage for result (0-17 move type)
                            com.parameters.Add(ReadParameterInt16(ref i)); //Party member to Store
                            break;
                        case 0x224:
                            com.Name = "0x224";
                            com.parameters.Add(ReadParameterInt16(ref i));//var
                            com.parameters.Add(ReadParameterInt16(ref i));//val
                            com.parameters.Add(ReadParameterInt16(ref i));//var
                            break;
                        case 0x226:
                            com.Name = "0x226";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x227:
                            com.Name = "0x227";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x229:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x22A:
                            com.Name = "0x22A";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            var next22A = ReadParameterInt16(ref i);
                            while (next22A >= 0x4000) { com.parameters.Add(next22A); next22A = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x22B:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x22C:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x22D:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            var next22D = ReadParameterInt16(ref i);
                            while (next22D >= 0x4000) { com.parameters.Add(next22D); next22D = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x22F:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x230:
                            com.Name = "0x230";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x231:
                            com.Name = "0x231";
                            com.parameters.Add(ReadParameterInt16(ref i)); // Var
                            var next231 = ReadParameterInt16(ref i);
                            while (next231 >= 0x4000) { com.parameters.Add(next231); next231 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x233:
                            com.Name = "0x233";
                            com.parameters.Add(ReadParameterInt16(ref i)); // Var
                            var next233 = ReadParameterInt16(ref i);
                            while (next233 >= 0x4000) { com.parameters.Add(next233); next233 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x234:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x236:
                            com.Name = "0x236";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            com.parameters.Add(ReadParameterInt16(ref i)); //val
                            com.parameters.Add(ReadParameterInt16(ref i)); //val
                            com.parameters.Add(ReadParameterInt16(ref i)); //val
                            break;
                        case 0x237:
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            com.parameters.Add(ReadParameterInt16(ref i)); //val
                            com.Name = "0x237";
                            break;
                        case 0x239:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x23A:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;

                        case 0x23D:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i)); //RV = Message Id
                            break;
                        case 0x23E:
                            com.Name = "0x23E";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x23F:
                            com.Name = "Close23F";
                            break;
                        case 0x242:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x245:
                            com.Name = "0x245";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x246:
                            com.Name = "0x246";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x247:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x248:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x249:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            var next249 = ReadParameterInt16(ref i);
                            while (next249 >= 0x4000) { com.parameters.Add(next249); next249 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x24C:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x24A:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x24E:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x24F:
                            com.Name = "0x24F";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x251:
                            com.Name = "0x251";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            var next251 = ReadParameterInt16(ref i);
                            while (next251 >= 0x4000) { com.parameters.Add(next251); next251 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x252:
                            com.Name = "0x252";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            var next252 = ReadParameterInt16(ref i);
                            while (next252 >= 0x4000) { com.parameters.Add(next252); next252 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x253:
                            com.Name = "0x253";
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x254:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x25A:
                            com.Name = "0x25A";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x25C:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x25F:
                            com.Name = "0x25F";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x262:
                            com.Name = "0x262";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x263:
                            com.Name = "0x263";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x266:
                            com.Name = "0x266";
                            com.parameters.Add(ReadParameterInt16(ref i)); // var
                            break;
                        case 0x26C:
                            com.Name = "StoreMedals26C";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x26D:
                            com.Name = "StoreMedals26D";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x26E:
                            com.Name = "CountMedals26E";
                            com.parameters.Add(ReadParameterByte(ref i)); // 3 For medals, command type?
                            com.parameters.Add(ReadParameterInt16(ref i)); // Variable to Store To
                            break;
                        case 0x271:
                            com.Name = "0x271";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x272:
                            com.Name = "0x272";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x273:
                            com.Name = "0x273";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x275:
                            com.Name = "0x275";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x276:
                            com.Name = "0x276";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x279:
                            com.Name = "0x279";
                            break;
                        case 0x283:
                            com.Name = "0x283";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x284:
                            com.Name = "0x284";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;

                        case 0x285:
                            com.Name = "0x285";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x287:
                            com.Name = "0x287";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x288:
                            com.Name = "0x288";
                            com.parameters.Add(ReadParameterInt16(ref i)); // value
                            com.parameters.Add(ReadParameterInt16(ref i)); // Variable
                            com.parameters.Add(ReadParameterInt16(ref i)); // Variable
                            break;
                        case 0x289:
                            com.Name = "0x289";
                            com.parameters.Add(ReadParameterInt16(ref i));    //might just be 3 tot
                            var next289 = ReadParameterInt16(ref i);
                            while (next289 >= 0x4000) { com.parameters.Add(next289); next289 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x28B:
                            com.Name = "0x28B";
                            break;
                        case 0x290:
                            com.Name = "0x290";
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x292:
                            com.Name = "0x292";
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x293:
                            com.Name = "0x293";
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x294:
                            com.Name = "0x294";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x295:
                            com.Name = "0x290";
                            break;
                        case 0x297:
                            com.Name = "0x297";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            var next297 = ReadParameterInt16(ref i);
                            while (next297 >= 0x4000) { com.parameters.Add(next297); next297 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x29A:
                            com.Name = "0x29A";
                            com.parameters.Add(ReadParameterByte(ref i)); // opt
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x29B:
                            com.Name = "0x29B";
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x29D:
                            com.Name = "0x29D";
                            break;
                        case 0x29E:
                            com.Name = "0x29E";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x29F:
                            com.Name = "0x29F";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2A0:
                            com.Name = "StoreHasMedal";
                            com.parameters.Add(ReadParameterInt16(ref i)); //
                            com.parameters.Add(ReadParameterInt16(ref i)); //
                            break;
                        case 0x2A1:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2A5:
                            com.Name = "0x2A5";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2A6:
                            com.Name = "0x2A6";
                            break;
                        case 0x2A7:
                            com.Name = "0x2A7";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2A8:
                            com.Name = "0x2A8";
                            break;
                        case 0x2A9:
                            com.Name = "0x2A9";
                            break;
                        case 0x2AF:
                            com.Name = "StoreDifficulty";
                            com.parameters.Add(ReadParameterInt16(ref i)); // Store result to var/cont of Easy 0 | Normal 1 | Hard 2
                            break;
                        case 0x2B1:
                            com.Name = "0x2B1";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2B2:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2B3:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i)); // var
                            break;
                        case 0x2B4:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i)); // var
                            break;
                        case 0x2B5:
                            com.Name = "0x2B5";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2B6:
                            com.Name = "0x2B6";
                            com.parameters.Add(ReadParameterInt16(ref i)); //
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x2B7:
                            com.Name = "0x2B7";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2B8:
                            com.Name = "FollowMeStart (0x2B8)"; // seen at the start of a follow me, maybe tracksteps
                            break;
                        case 0x2B9:
                            com.Name = "FollowMeEnd (0x2B9)"; // maybe endtracksteps
                            break;
                        case 0x2BA:
                            com.Name = "FollowMeCopyStepsTo (0x2BA)"; // copy steps taken with follow me to CONT
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2BC:
                            com.Name = "0x2BC";
                            com.parameters.Add(ReadParameterInt16(ref i));//var
                            com.parameters.Add(ReadParameterInt16(ref i));//var
                            break;
                        case 0x2BD:
                            com.Name = "0x2BD";
                            com.parameters.Add(ReadParameterInt16(ref i));//var
                            break;
                        case 0x2BE:
                            com.Name = "0x2BE";
                            com.parameters.Add(ReadParameterInt16(ref i)); //val
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x2C0:
                            com.Name = "0x2C0";
                            com.parameters.Add(ReadParameterInt16(ref i));//var
                            com.parameters.Add(ReadParameterInt16(ref i));//var
                            break;
                        case 0x2C3:
                            com.Name = "0x2C3";
                            com.parameters.Add(ReadParameterInt16(ref i));//var
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2C4:
                            com.Name = "0x2C4";
                            break;
                        case 0x2C5:
                            com.Name = "0x2C5";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2CB:
                            com.Name = "0x2CB";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2CF:
                            com.Name = "0x2CF";
                            com.parameters.Add(ReadParameterInt16(ref i)); //val
                            com.parameters.Add(ReadParameterInt16(ref i)); //val
                            com.parameters.Add(ReadParameterInt16(ref i)); //val
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x2D0:
                            com.Name = "***HABITATLISTENABLE***";
                            break;
                        case 0x2D1:
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2D4:
                            com.Name = "0x2D4";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2D5:
                            com.Name = "0x2D5";
                            com.parameters.Add(ReadParameterInt16(ref i)); // value
                            com.parameters.Add(ReadParameterInt16(ref i)); // variable
                            break;
                        case 0x2D7:
                            com.Name = "0x2D7";
                            com.parameters.Add(ReadParameterInt16(ref i)); // value
                            com.parameters.Add(ReadParameterInt16(ref i)); // variable
                            break;
                        case 0x2D9:
                            com.Name = "0x2D9";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2DA:
                            com.Name = "0x2DA";
                            com.parameters.Add(ReadParameterInt16(ref i)); // low value
                            break;
                        case 0x2DB:
                            com.Name = "0x2DB";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2DC:
                            com.Name = "0x2DC";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2DD:
                            com.Name = "StoreUnityVisitors";
                            com.parameters.Add(ReadParameterInt16(ref i)); // Variable to return to?
                            break;
                        case 0x2DF:
                            com.Name = "StoreMyActivities"; // activity
                            com.parameters.Add(ReadParameterInt16(ref i)); // Variable to return to?
                            break;
                        case 0x2E1:
                            com.Name = "0x2E1";
                            break;
                        case 0x2E8:
                            com.Name = "0x2E8";
                            com.parameters.Add(ReadParameterInt16(ref i)); // 
                            com.parameters.Add(ReadParameterInt16(ref i)); // 
                            break;
                        case 0x2ED:
                            com.Name = "0x2ED";
                            com.parameters.Add(ReadParameterInt16(ref i)); // 
                            com.parameters.Add(ReadParameterInt16(ref i)); // 
                            break;
                        case 0x2EE:
                            com.Name = "Prop2EE";
                            com.parameters.Add(ReadParameterInt16(ref i)); // value ~ prop# to give?
                            com.parameters.Add(ReadParameterInt16(ref i)); // variable/container
                            break;
                        case 0x2EF:
                            com.Name = "0x2EF";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x2F1:
                            com.Name = "0x2F1";
                            com.parameters.Add(ReadParameterInt16(ref i)); //
                            break;
                        case 0x2F2:
                            com.Name = "0x2F2";
                            break;

                        //commands set 2 (1000)

                        case 0x3E8:
                            com.Name = "0x3E8";
                            var next3E8 = ReadParameterInt16(ref i);
                            while (next3E8 < 2) { com.parameters.Add(next3E8); next3E8 = ReadParameterInt16(ref i); }
                            while (next3E8 <= 0xA && next3E8 > 1) { com.parameters.Add(next3E8); next3E8 = ReadParameterInt16(ref i); }
                            while (next3E8 >= 0x4000) { com.parameters.Add(next3E8); next3E8 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x3E9:
                            com.Name = "0x3E9";
                            var next3E9 = ReadParameterInt16(ref i);
                            while (next3E9 < 2) { com.parameters.Add(next3E9); next3E9 = ReadParameterInt16(ref i); }
                            while (next3E9 <= 0xA && next3E9 > 1) { com.parameters.Add(next3E9); next3E9 = ReadParameterInt16(ref i); }
                            while (next3E9 >= 0x4000) { com.parameters.Add(next3E9); next3E9 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x3EA:
                            com.Name = "0x3EA";
                            var next3EA = ReadParameterInt16(ref i);
                            while (next3EA < 2) { com.parameters.Add(next3EA); next3EA = ReadParameterInt16(ref i); }
                            while (next3EA <= 0xA && next3EA > 1) { com.parameters.Add(next3EA); next3EA = ReadParameterInt16(ref i); }
                            while (next3EA >= 0x4000) { com.parameters.Add(next3EA); next3EA = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x3EB:
                            com.Name = "0x3EB";
                            var next3EB = ReadParameterInt16(ref i);
                            while (next3EB < 2) { com.parameters.Add(next3EB); next3EB = ReadParameterInt16(ref i); }
                            while (next3EB <= 0xA && next3EB > 1) { com.parameters.Add(next3EB); next3EB = ReadParameterInt16(ref i); }
                            while (next3EB >= 0x4000) { com.parameters.Add(next3EB); next3EB = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x3EC:
                            com.Name = "0x3EC";
                            var next3EC = ReadParameterInt16(ref i);
                            while (next3EC < 2) { com.parameters.Add(next3EC); next3EC = ReadParameterInt16(ref i); }
                            while (next3EC <= 0xA && next3EC > 1) { com.parameters.Add(next3EC); next3EC = ReadParameterInt16(ref i); }
                            while (next3EC >= 0x4000) { com.parameters.Add(next3EC); next3EC = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x3ED:
                            com.Name = "0x3ED";
                            var next3ED = ReadParameterInt16(ref i);
                            while (next3ED <= 40) { com.parameters.Add(next3ED); next3ED = ReadParameterInt16(ref i); }
                            while (next3ED >= 0x4000) { com.parameters.Add(next3ED); next3ED = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x3EE:
                            com.Name = "0x3EE";
                            var next3EE = ReadParameterInt16(ref i);
                            while (next3EE <= 40) { com.parameters.Add(next3EE); next3EE = ReadParameterInt16(ref i); }
                            while (next3EE >= 0x4000) { com.parameters.Add(next3EE); next3EE = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x3EF:
                            com.Name = "0x3EF";
                            var next3EF = ReadParameterInt16(ref i);
                            while (next3EF <= 40) { com.parameters.Add(next3EF); next3EF = ReadParameterInt16(ref i); }
                            while (next3EF >= 0x4000) { com.parameters.Add(next3EF); next3EF = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x3F0:
                            com.Name = "0x3F0";
                            var next3F0 = ReadParameterInt16(ref i);
                            while (next3F0 <= 40) { com.parameters.Add(next3F0); next3F0 = ReadParameterInt16(ref i); }
                            while (next3F0 >= 0x4000) { com.parameters.Add(next3F0); next3F0 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x3F1:
                            com.Name = "0x3F1";
                            var next3F1 = ReadParameterInt16(ref i);
                            while (next3F1 <= 40) { com.parameters.Add(next3F1); next3F1 = ReadParameterInt16(ref i); }
                            while (next3F1 >= 0x4000) { com.parameters.Add(next3F1); next3F1 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x3F2:
                            com.Name = "0x3F2";
                            var next3F2 = ReadParameterInt16(ref i);
                            while (next3F2 <= 40) { com.parameters.Add(next3F2); next3F2 = ReadParameterInt16(ref i); }
                            while (next3F2 >= 0x4000) { com.parameters.Add(next3F2); next3F2 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x3F3:
                            com.Name = "0x3F3";
                            var next3F3 = ReadParameterInt16(ref i);
                            while (next3F3 <= 40) { com.parameters.Add(next3F3); next3F3 = ReadParameterInt16(ref i); }
                            while (next3F3 >= 0x4000) { com.parameters.Add(next3F3); next3F3 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x3F4:
                            com.Name = "0x3F4";
                            var next3F4 = ReadParameterInt16(ref i);
                            while (next3F4 <= 40) { com.parameters.Add(next3F4); next3F4 = ReadParameterInt16(ref i); }
                            while (next3F4 >= 0x4000) { com.parameters.Add(next3F4); next3F4 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x3F5:
                            com.Name = "0x3F5";
                            var next3F5 = ReadParameterInt16(ref i);
                            if (next3F5 < 2) { com.parameters.Add(next3F5); break; }
                            while (next3F5 <= 0xA && next3F5 > 1) { com.parameters.Add(next3F5); next3F5 = ReadParameterInt16(ref i); }
                            while (next3F5 >= 0x4000) { com.parameters.Add(next3F5); next3F5 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x3F6:
                            com.Name = "0x3F6";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x3F8:
                            com.Name = "0x3F8";
                            var next3F8 = ReadParameterInt16(ref i);
                            while (next3F8 <= 40) { com.parameters.Add(next3F8); next3F8 = ReadParameterInt16(ref i); }
                            while (next3F8 <= 0xA && next3F8 > 1) { com.parameters.Add(next3F8); next3F8 = ReadParameterInt16(ref i); }
                            while (next3F8 >= 0x4000) { com.parameters.Add(next3F8); next3F8 = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x3F9:
                            com.Name = "0x3F9";
                            break;
                        case 0x3FA:
                            com.Name = "0x3FA";
                            var next3FA = ReadParameterInt16(ref i);
                            while (next3FA <= 40) { com.parameters.Add(next3FA); next3FA = ReadParameterInt16(ref i); }
                            while (next3FA <= 0xA && next3FA > 1) { com.parameters.Add(next3FA); next3FA = ReadParameterInt16(ref i); }
                            while (next3FA >= 0x4000) { com.parameters.Add(next3FA); next3FA = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x3FB:         //iris?
                            com.parameters.Add(ReadParameterInt16(ref i)); //not sure if below is needed
                            var next3FB = ReadParameterInt16(ref i);
                            while (next3FB <= 40) { com.parameters.Add(next3FB); next3FB = ReadParameterInt16(ref i); }
                            while (next3FB <= 0xA && next3FB > 1) { com.parameters.Add(next3FB); next3FB = ReadParameterInt16(ref i); }
                            while (next3FB >= 0x4000) { com.parameters.Add(next3FB); next3FB = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x3FC:
                            com.Name = "0x3FC";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x3FD:
                            com.Name = "0x3FD";
                            break;
                        case 0x3FE:
                            com.Name = "0x3FE";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x3FF:
                            com.Name = "0x3FF";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            var next3FF = ReadParameterInt16(ref i);
                            while (next3FF >= 0x4000) { com.parameters.Add(next3FF); next3FF = ReadParameterInt16(ref i); }
                            i -= 2;
                            break;
                        case 0x401:
                            com.Name = "0x401";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x402:
                            com.Name = "0x402";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x403:
                            com.Name = "0x403";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x404:
                            com.Name = "0x404";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x406:
                            com.Name = "0x406";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x407:
                            com.Name = "0x407";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x40D:
                            com.Name = "0x40D";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            break;
                        case 0x40E:
                            com.Name = "0x40E";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x410:
                            com.Name = "0x410";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x411:
                            com.Name = "0x411";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x412:
                            com.Name = "0x412";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x414:
                            com.Name = "0x414";
                            com.parameters.Add(ReadParameterByte(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x415:
                            com.Name = "0x415";
                            com.parameters.Add(ReadParameterByte(ref i));
                            break;
                        case 0x416:
                            com.Name = "0x416";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x417:
                            com.Name = "0x417";
                            com.parameters.Add(ReadParameterInt16(ref i)); //val
                            break;
                        case 0x418:
                            com.Name = "0x418";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x419:
                            com.Name = "0x419";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x41A:
                            com.Name = "0x41A";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x41B:
                            com.Name = "0x40B";
                            com.parameters.Add(ReadParameterInt16(ref i));
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x41C:
                            com.Name = "0x41C";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x421:
                            com.Name = "0x420";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x41F:
                            com.Name = "0x41F";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x420:
                            com.Name = "0x420";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                        case 0x422:
                            com.Name = "0x422";
                            com.parameters.Add(ReadParameterInt16(ref i)); //var
                            break;
                    }

                    s.commands.Add(com);
                }
                s.commands.Add(new CommandEntry() { Name = "End" });
                #endregion

                s.bytes = fileData.GetRange(s.byteLocation, i - s.byteLocation + 2);

                scriptIDComboBox.Items.Add(scripts.IndexOf(s));
            }

            scriptIDComboBox.SelectedIndex = 0;
            scriptIDComboBox.Enabled = true;
        }

        private void ChangeScriptID(object sender, EventArgs e)
        {
            ScriptEntry script = scripts[scriptIDComboBox.SelectedIndex];

            rawHexTextBox.Text = "";
            foreach (byte b in script.bytes)
            {
                if (b < 0x10) rawHexTextBox.Text += "0";
                rawHexTextBox.Text += b.ToString("X");
            }
        }
        
        private void AddScript(object sender, EventArgs e)
        {
            foreach (ScriptEntry s in scripts)
            {
                s.pointerValue += 4;
                byte[] bytes = BitConverter.GetBytes(s.pointerValue);
                for (int i = 0; i < 4; i++) fileData[(scripts.IndexOf(s) * 4) + i] = bytes[i];
            }
            fileData.InsertRange(scripts.Count * 4, BitConverter.GetBytes(fileData.Count - 2 - (scripts.Count * 4)));

            fileData.InsertRange(fileData.Count - 2, new byte[] { 0x2E, 0x00, 0x2F, 0x00, 0x02, 0x00 });

            RegisterScripts();
        }

        private void rawHexTextBox_TextChanged(object sender, EventArgs e)
        {
            int cursorPos = rawHexTextBox.SelectionStart;
            StringBuilder text = new StringBuilder();
            foreach (char c in rawHexTextBox.Text) if (char.IsDigit(c) || (char.IsLetter(c) && char.ToUpper(c) <= 'F')) text.Append(char.ToUpper(c));

            rawHexTextBox.Text = text.ToString();
            rawHexTextBox.SelectionStart = cursorPos;

            StringBuilder text2 = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (i > 0 && i % 2 == 0) text2.Append("  ");
                text2.Append(text[i]);
            }
            if (text.Length % 2 == 1) text2.Append('0');
            formatedHexTextBox.Text = text2.ToString();
            HexStringToCommandList();
        }

        private void HexStringToCommandList()
        {
            List<int> currentCommandGroupByte = new List<int>();
            List<int> nextCommandGroupByte = new List<int>();
            List<ListBox> currentBoxes = new List<ListBox>();
            int iteration = 0;

            commandListPanel.Controls.Clear();
            int i = 0;
            ListBox activeBox = new ListBox() { Size = new Size(200, 150), Location = new Point(25, 80) };
            commandListPanel.Controls.Add(activeBox);
            currentCommandGroupByte.Add(0);
            currentBoxes.Add(activeBox);
            while (currentCommandGroupByte.Count != 0 && i < rawHexTextBox.Text.Length - 4)
            {
                int comID = BitConverter.ToInt16(StringToBytes(rawHexTextBox.Text.Substring(i, 4)), 0);
                if (!CommandReference.commandList.ContainsKey(comID))
                {
                    activeBox.Items.Add(comID.ToString("X"));
                    i += 4;
                    continue;
                }
                StringBuilder str = new StringBuilder(CommandReference.commandList[comID].name + "(");
                i += 4;
                for (int j = 0; j < CommandReference.commandList[comID].numParameters; j++)
                {
                    int num = 0;
                    if (i < rawHexTextBox.Text.Length) num = BitConverter.ToInt32(StringToBytes(rawHexTextBox.Text.Substring(i, Math.Min(CommandReference.commandList[comID].parameterBytes[j] * 2, rawHexTextBox.Text.Length - i))), 0);
                    if (j != 0) str.Append(", ");
                    if (num >= 0x8000) str.Append("0x" + num.ToString("X"));
                    else str.Append(num.ToString());
                    i += CommandReference.commandList[comID].parameterBytes[j] * 2;
                }
                str.Append(");");

                if ((comID == 0x1E || comID == 0x1F) && i < rawHexTextBox.Text.Length)
                {
                    int numJump = i + BitConverter.ToInt16(StringToBytes(rawHexTextBox.Text.Substring(i - 8, 8)), 0) * 2;
                    if (numJump - i >= 0)
                    {
                        nextCommandGroupByte.Add(numJump);
                        str.Append("\t//To box " + (nextCommandGroupByte.IndexOf(numJump) + 1));
                    }
                    else str.Append("\t//Back " + (numJump - i));
                }
                activeBox.Items.Add(str.ToString());

                if (comID == 0x02 || i >= rawHexTextBox.Text.Length - 3 || comID == 0x1E)
                {
                    currentCommandGroupByte.RemoveAt(0);
                    currentBoxes.RemoveAt(0);
                    if (currentCommandGroupByte.Count != 0)
                    {
                        //Next in current group
                        i = currentCommandGroupByte[0];
                        activeBox = currentBoxes[0];
                    }
                    else if (nextCommandGroupByte.Count != 0)
                    {
                        //Start next group
                        currentCommandGroupByte = new List<int>(nextCommandGroupByte);
                        nextCommandGroupByte = new List<int>();
                        i = currentCommandGroupByte[0];

                        for (int n = 0; n < currentCommandGroupByte.Count; n++)
                        {
                            ListBox newbox = new ListBox() { Size = new Size(200, 150), Location = new Point(activeBox.Location.X + 250, 80 + (200 * n)) };
                            currentBoxes.Add(newbox);
                            commandListPanel.Controls.Add(newbox);
                        }
                        activeBox = currentBoxes[0];
                    }
                }
            }
        }

        private byte[] StringToBytes(string str)
        {
            StringBuilder str2 = new StringBuilder();

            if (str.Length % 2 == 1) str += '0';
            for (int i = str.Length - 1; i >= 0; i -= 2)
            {
                str2.Append(str[i - 1]);
                str2.Append(str[i]);
            }

            int num = int.Parse(str2.ToString(), System.Globalization.NumberStyles.HexNumber);
            return BitConverter.GetBytes(num);
        }

        private int ReadParameterByte(ref int position)
        {
            if (position >= fileData.Count) return 0;
            int num = fileData[position];
            position += 1;
            return num;
        }
        private int ReadParameterInt16(ref int position)
        {
            if (position + 1 >= fileData.Count) return 0;
            int num = fileData[position] + fileData[position + 1] * 256;
            position += 2;
            return num;
        }
        private int ReadParameterInt32(ref int position)
        {
            if (position + 3 >= fileData.Count) return 0;
            int num = fileData[position] + fileData[position + 1] * 256 + fileData[position + 2] * 65536 + fileData[position + 3] * 16777216;
            position += 4;
            return num;
        }
    }

    public class ScriptEntry
    {
        public int pointerLocation;
        public int pointerValue;
        public int byteLocation;
        public List<CommandEntry> commands = new List<CommandEntry>();
        public List<byte> bytes = new List<byte>();
    }

    public class CommandEntry
    {
        public int commandID;
        public string Name;
        public List<int> parameters = new List<int>();

        public override string ToString()
        {
            string result = Name + "(";
            foreach (int i in parameters)
            {
                if (parameters.IndexOf(i) == 0) result += i;
                else result += ", " + i;
            }
            result += ")";

            return result;
        }
    }
}

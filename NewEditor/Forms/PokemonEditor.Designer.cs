﻿
namespace NewEditor.Forms
{
    partial class PokemonEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pokemonNameDropdown = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.pkLevelRateDropdown = new System.Windows.Forms.ComboBox();
            this.pokemonBSTText = new System.Windows.Forms.Label();
            this.applyPokemonButton = new System.Windows.Forms.Button();
            this.pokemonBaseSpeedNumberBox = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.pokemonBaseSpDefNumberBox = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.pokemonBaseSpAttNumberBox = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.pokemonBaseDefenseNumberBox = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.pokemonBaseAttackNumberBox = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pokemonBaseHpNumberBox = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pokemonTypeDropdown2 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.pokemonTypeDropdown1 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pokeAbilityDropdown3 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pokeAbilityDropdown2 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pokeAbilityDropdown1 = new System.Windows.Forms.ComboBox();
            this.baseStatsGroup = new System.Windows.Forms.GroupBox();
            this.miscStatsGroup = new System.Windows.Forms.GroupBox();
            this.xpYieldNumberBox = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.heldItem3Dropdown = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.heldItem1Dropdown = new System.Windows.Forms.ComboBox();
            this.heldItem2Dropdown = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pkGenderRatioDropdown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.levelUpMovesGroup = new System.Windows.Forms.GroupBox();
            this.learnsetApplyMoveButton = new System.Windows.Forms.Button();
            this.learnsetListBox = new System.Windows.Forms.ListBox();
            this.learnsetMoveDropdown = new System.Windows.Forms.ComboBox();
            this.addLearnsetMoveButton = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.removeLearnsetMoveButton = new System.Windows.Forms.Button();
            this.learnsetLevelNumberBox = new System.Windows.Forms.NumericUpDown();
            this.copyLearnsetButton = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.pasteLearnsetButton = new System.Windows.Forms.Button();
            this.tmMovesGroupBox = new System.Windows.Forms.GroupBox();
            this.tmMovesListBox = new System.Windows.Forms.CheckedListBox();
            this.evolutionsGroupBox = new System.Windows.Forms.GroupBox();
            this.evolutionApplyButton = new System.Windows.Forms.Button();
            this.evolutionIntoDropdown = new System.Windows.Forms.ComboBox();
            this.evolutionConditionDropdown = new System.Windows.Forms.ComboBox();
            this.evolutionMethodDropdown = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.evolutionsListBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pokemonBaseSpeedNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pokemonBaseSpDefNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pokemonBaseSpAttNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pokemonBaseDefenseNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pokemonBaseAttackNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pokemonBaseHpNumberBox)).BeginInit();
            this.baseStatsGroup.SuspendLayout();
            this.miscStatsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xpYieldNumberBox)).BeginInit();
            this.levelUpMovesGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.learnsetLevelNumberBox)).BeginInit();
            this.tmMovesGroupBox.SuspendLayout();
            this.evolutionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pokemonNameDropdown
            // 
            this.pokemonNameDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.pokemonNameDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.pokemonNameDropdown.FormattingEnabled = true;
            this.pokemonNameDropdown.Location = new System.Drawing.Point(20, 20);
            this.pokemonNameDropdown.Name = "pokemonNameDropdown";
            this.pokemonNameDropdown.Size = new System.Drawing.Size(143, 24);
            this.pokemonNameDropdown.TabIndex = 1;
            this.pokemonNameDropdown.SelectedIndexChanged += new System.EventHandler(this.LoadPokemonIntoEditor);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(15, 51);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(72, 16);
            this.label30.TabIndex = 71;
            this.label30.Text = "Level Rate:";
            // 
            // pkLevelRateDropdown
            // 
            this.pkLevelRateDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.pkLevelRateDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.pkLevelRateDropdown.FormattingEnabled = true;
            this.pkLevelRateDropdown.Location = new System.Drawing.Point(110, 48);
            this.pkLevelRateDropdown.Name = "pkLevelRateDropdown";
            this.pkLevelRateDropdown.Size = new System.Drawing.Size(140, 24);
            this.pkLevelRateDropdown.TabIndex = 70;
            // 
            // pokemonBSTText
            // 
            this.pokemonBSTText.AutoSize = true;
            this.pokemonBSTText.Location = new System.Drawing.Point(30, 280);
            this.pokemonBSTText.Name = "pokemonBSTText";
            this.pokemonBSTText.Size = new System.Drawing.Size(43, 16);
            this.pokemonBSTText.TabIndex = 69;
            this.pokemonBSTText.Text = "Total: ";
            // 
            // applyPokemonButton
            // 
            this.applyPokemonButton.Enabled = false;
            this.applyPokemonButton.Location = new System.Drawing.Point(20, 625);
            this.applyPokemonButton.Name = "applyPokemonButton";
            this.applyPokemonButton.Size = new System.Drawing.Size(120, 40);
            this.applyPokemonButton.TabIndex = 68;
            this.applyPokemonButton.Text = "Apply Pokemon";
            this.applyPokemonButton.UseVisualStyleBackColor = true;
            this.applyPokemonButton.Click += new System.EventHandler(this.ApplyPokemon);
            // 
            // pokemonBaseSpeedNumberBox
            // 
            this.pokemonBaseSpeedNumberBox.Location = new System.Drawing.Point(70, 241);
            this.pokemonBaseSpeedNumberBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.pokemonBaseSpeedNumberBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pokemonBaseSpeedNumberBox.Name = "pokemonBaseSpeedNumberBox";
            this.pokemonBaseSpeedNumberBox.Size = new System.Drawing.Size(50, 22);
            this.pokemonBaseSpeedNumberBox.TabIndex = 67;
            this.pokemonBaseSpeedNumberBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pokemonBaseSpeedNumberBox.ValueChanged += new System.EventHandler(this.UpdateBaseStatTotal);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(30, 245);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(35, 16);
            this.label19.TabIndex = 66;
            this.label19.Text = "Spe:";
            // 
            // pokemonBaseSpDefNumberBox
            // 
            this.pokemonBaseSpDefNumberBox.Location = new System.Drawing.Point(70, 206);
            this.pokemonBaseSpDefNumberBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.pokemonBaseSpDefNumberBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pokemonBaseSpDefNumberBox.Name = "pokemonBaseSpDefNumberBox";
            this.pokemonBaseSpDefNumberBox.Size = new System.Drawing.Size(50, 22);
            this.pokemonBaseSpDefNumberBox.TabIndex = 65;
            this.pokemonBaseSpDefNumberBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pokemonBaseSpDefNumberBox.ValueChanged += new System.EventHandler(this.UpdateBaseStatTotal);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(30, 210);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(37, 16);
            this.label18.TabIndex = 64;
            this.label18.Text = "SpD:";
            // 
            // pokemonBaseSpAttNumberBox
            // 
            this.pokemonBaseSpAttNumberBox.Location = new System.Drawing.Point(70, 171);
            this.pokemonBaseSpAttNumberBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.pokemonBaseSpAttNumberBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pokemonBaseSpAttNumberBox.Name = "pokemonBaseSpAttNumberBox";
            this.pokemonBaseSpAttNumberBox.Size = new System.Drawing.Size(50, 22);
            this.pokemonBaseSpAttNumberBox.TabIndex = 63;
            this.pokemonBaseSpAttNumberBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pokemonBaseSpAttNumberBox.ValueChanged += new System.EventHandler(this.UpdateBaseStatTotal);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(30, 175);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(37, 16);
            this.label17.TabIndex = 62;
            this.label17.Text = "SpA:";
            // 
            // pokemonBaseDefenseNumberBox
            // 
            this.pokemonBaseDefenseNumberBox.Location = new System.Drawing.Point(70, 136);
            this.pokemonBaseDefenseNumberBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.pokemonBaseDefenseNumberBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pokemonBaseDefenseNumberBox.Name = "pokemonBaseDefenseNumberBox";
            this.pokemonBaseDefenseNumberBox.Size = new System.Drawing.Size(50, 22);
            this.pokemonBaseDefenseNumberBox.TabIndex = 61;
            this.pokemonBaseDefenseNumberBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pokemonBaseDefenseNumberBox.ValueChanged += new System.EventHandler(this.UpdateBaseStatTotal);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(30, 140);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 16);
            this.label16.TabIndex = 60;
            this.label16.Text = "Def:";
            // 
            // pokemonBaseAttackNumberBox
            // 
            this.pokemonBaseAttackNumberBox.Location = new System.Drawing.Point(70, 101);
            this.pokemonBaseAttackNumberBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.pokemonBaseAttackNumberBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pokemonBaseAttackNumberBox.Name = "pokemonBaseAttackNumberBox";
            this.pokemonBaseAttackNumberBox.Size = new System.Drawing.Size(50, 22);
            this.pokemonBaseAttackNumberBox.TabIndex = 59;
            this.pokemonBaseAttackNumberBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pokemonBaseAttackNumberBox.ValueChanged += new System.EventHandler(this.UpdateBaseStatTotal);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(30, 105);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 16);
            this.label15.TabIndex = 58;
            this.label15.Text = "Att:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(30, 35);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 16);
            this.label14.TabIndex = 57;
            this.label14.Text = "Base Stats";
            // 
            // pokemonBaseHpNumberBox
            // 
            this.pokemonBaseHpNumberBox.Location = new System.Drawing.Point(70, 66);
            this.pokemonBaseHpNumberBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.pokemonBaseHpNumberBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pokemonBaseHpNumberBox.Name = "pokemonBaseHpNumberBox";
            this.pokemonBaseHpNumberBox.Size = new System.Drawing.Size(50, 22);
            this.pokemonBaseHpNumberBox.TabIndex = 56;
            this.pokemonBaseHpNumberBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pokemonBaseHpNumberBox.ValueChanged += new System.EventHandler(this.UpdateBaseStatTotal);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(30, 70);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(28, 16);
            this.label13.TabIndex = 55;
            this.label13.Text = "Hp:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(200, 105);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 16);
            this.label12.TabIndex = 54;
            this.label12.Text = "Type 2:";
            // 
            // pokemonTypeDropdown2
            // 
            this.pokemonTypeDropdown2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.pokemonTypeDropdown2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.pokemonTypeDropdown2.FormattingEnabled = true;
            this.pokemonTypeDropdown2.Location = new System.Drawing.Point(265, 101);
            this.pokemonTypeDropdown2.Name = "pokemonTypeDropdown2";
            this.pokemonTypeDropdown2.Size = new System.Drawing.Size(100, 24);
            this.pokemonTypeDropdown2.TabIndex = 53;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(200, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 16);
            this.label11.TabIndex = 52;
            this.label11.Text = "Type 1:";
            // 
            // pokemonTypeDropdown1
            // 
            this.pokemonTypeDropdown1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.pokemonTypeDropdown1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.pokemonTypeDropdown1.FormattingEnabled = true;
            this.pokemonTypeDropdown1.Location = new System.Drawing.Point(265, 66);
            this.pokemonTypeDropdown1.Name = "pokemonTypeDropdown1";
            this.pokemonTypeDropdown1.Size = new System.Drawing.Size(100, 24);
            this.pokemonTypeDropdown1.TabIndex = 51;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(200, 245);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 16);
            this.label10.TabIndex = 50;
            this.label10.Text = "Ability 3:";
            // 
            // pokeAbilityDropdown3
            // 
            this.pokeAbilityDropdown3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.pokeAbilityDropdown3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.pokeAbilityDropdown3.FormattingEnabled = true;
            this.pokeAbilityDropdown3.Location = new System.Drawing.Point(265, 241);
            this.pokeAbilityDropdown3.Name = "pokeAbilityDropdown3";
            this.pokeAbilityDropdown3.Size = new System.Drawing.Size(120, 24);
            this.pokeAbilityDropdown3.TabIndex = 49;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(200, 210);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 16);
            this.label9.TabIndex = 48;
            this.label9.Text = "Ability 2:";
            // 
            // pokeAbilityDropdown2
            // 
            this.pokeAbilityDropdown2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.pokeAbilityDropdown2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.pokeAbilityDropdown2.FormattingEnabled = true;
            this.pokeAbilityDropdown2.Location = new System.Drawing.Point(265, 206);
            this.pokeAbilityDropdown2.Name = "pokeAbilityDropdown2";
            this.pokeAbilityDropdown2.Size = new System.Drawing.Size(120, 24);
            this.pokeAbilityDropdown2.TabIndex = 47;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(200, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 16);
            this.label8.TabIndex = 46;
            this.label8.Text = "Ability 1:";
            // 
            // pokeAbilityDropdown1
            // 
            this.pokeAbilityDropdown1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.pokeAbilityDropdown1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.pokeAbilityDropdown1.FormattingEnabled = true;
            this.pokeAbilityDropdown1.Location = new System.Drawing.Point(265, 171);
            this.pokeAbilityDropdown1.Name = "pokeAbilityDropdown1";
            this.pokeAbilityDropdown1.Size = new System.Drawing.Size(120, 24);
            this.pokeAbilityDropdown1.TabIndex = 45;
            // 
            // baseStatsGroup
            // 
            this.baseStatsGroup.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.baseStatsGroup.Controls.Add(this.pokemonBaseHpNumberBox);
            this.baseStatsGroup.Controls.Add(this.label13);
            this.baseStatsGroup.Controls.Add(this.label14);
            this.baseStatsGroup.Controls.Add(this.pokemonBSTText);
            this.baseStatsGroup.Controls.Add(this.label10);
            this.baseStatsGroup.Controls.Add(this.label12);
            this.baseStatsGroup.Controls.Add(this.pokeAbilityDropdown3);
            this.baseStatsGroup.Controls.Add(this.label15);
            this.baseStatsGroup.Controls.Add(this.label9);
            this.baseStatsGroup.Controls.Add(this.pokemonTypeDropdown2);
            this.baseStatsGroup.Controls.Add(this.pokeAbilityDropdown2);
            this.baseStatsGroup.Controls.Add(this.pokemonBaseAttackNumberBox);
            this.baseStatsGroup.Controls.Add(this.label8);
            this.baseStatsGroup.Controls.Add(this.label11);
            this.baseStatsGroup.Controls.Add(this.pokeAbilityDropdown1);
            this.baseStatsGroup.Controls.Add(this.pokemonBaseSpeedNumberBox);
            this.baseStatsGroup.Controls.Add(this.pokemonTypeDropdown1);
            this.baseStatsGroup.Controls.Add(this.label16);
            this.baseStatsGroup.Controls.Add(this.label19);
            this.baseStatsGroup.Controls.Add(this.pokemonBaseDefenseNumberBox);
            this.baseStatsGroup.Controls.Add(this.pokemonBaseSpDefNumberBox);
            this.baseStatsGroup.Controls.Add(this.label17);
            this.baseStatsGroup.Controls.Add(this.label18);
            this.baseStatsGroup.Controls.Add(this.pokemonBaseSpAttNumberBox);
            this.baseStatsGroup.Enabled = false;
            this.baseStatsGroup.Location = new System.Drawing.Point(20, 60);
            this.baseStatsGroup.Name = "baseStatsGroup";
            this.baseStatsGroup.Size = new System.Drawing.Size(460, 340);
            this.baseStatsGroup.TabIndex = 72;
            this.baseStatsGroup.TabStop = false;
            this.baseStatsGroup.Text = "Base Data";
            // 
            // miscStatsGroup
            // 
            this.miscStatsGroup.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.miscStatsGroup.Controls.Add(this.xpYieldNumberBox);
            this.miscStatsGroup.Controls.Add(this.label6);
            this.miscStatsGroup.Controls.Add(this.label5);
            this.miscStatsGroup.Controls.Add(this.heldItem3Dropdown);
            this.miscStatsGroup.Controls.Add(this.label4);
            this.miscStatsGroup.Controls.Add(this.label3);
            this.miscStatsGroup.Controls.Add(this.heldItem1Dropdown);
            this.miscStatsGroup.Controls.Add(this.heldItem2Dropdown);
            this.miscStatsGroup.Controls.Add(this.label2);
            this.miscStatsGroup.Controls.Add(this.pkGenderRatioDropdown);
            this.miscStatsGroup.Controls.Add(this.label1);
            this.miscStatsGroup.Controls.Add(this.pkLevelRateDropdown);
            this.miscStatsGroup.Controls.Add(this.label30);
            this.miscStatsGroup.Enabled = false;
            this.miscStatsGroup.Location = new System.Drawing.Point(20, 410);
            this.miscStatsGroup.Name = "miscStatsGroup";
            this.miscStatsGroup.Size = new System.Drawing.Size(460, 200);
            this.miscStatsGroup.TabIndex = 73;
            this.miscStatsGroup.TabStop = false;
            this.miscStatsGroup.Text = "Misc Stats";
            // 
            // xpYieldNumberBox
            // 
            this.xpYieldNumberBox.Location = new System.Drawing.Point(110, 118);
            this.xpYieldNumberBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.xpYieldNumberBox.Name = "xpYieldNumberBox";
            this.xpYieldNumberBox.Size = new System.Drawing.Size(50, 22);
            this.xpYieldNumberBox.TabIndex = 70;
            this.xpYieldNumberBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 16);
            this.label6.TabIndex = 81;
            this.label6.Text = "Xp Yield:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(289, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 16);
            this.label5.TabIndex = 79;
            this.label5.Text = "1%";
            // 
            // heldItem3Dropdown
            // 
            this.heldItem3Dropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.heldItem3Dropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.heldItem3Dropdown.FormattingEnabled = true;
            this.heldItem3Dropdown.Location = new System.Drawing.Point(322, 117);
            this.heldItem3Dropdown.Name = "heldItem3Dropdown";
            this.heldItem3Dropdown.Size = new System.Drawing.Size(120, 24);
            this.heldItem3Dropdown.TabIndex = 78;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(289, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 16);
            this.label4.TabIndex = 77;
            this.label4.Text = "5%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(282, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 16);
            this.label3.TabIndex = 76;
            this.label3.Text = "50%";
            // 
            // heldItem1Dropdown
            // 
            this.heldItem1Dropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.heldItem1Dropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.heldItem1Dropdown.FormattingEnabled = true;
            this.heldItem1Dropdown.Location = new System.Drawing.Point(322, 48);
            this.heldItem1Dropdown.Name = "heldItem1Dropdown";
            this.heldItem1Dropdown.Size = new System.Drawing.Size(120, 24);
            this.heldItem1Dropdown.TabIndex = 70;
            // 
            // heldItem2Dropdown
            // 
            this.heldItem2Dropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.heldItem2Dropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.heldItem2Dropdown.FormattingEnabled = true;
            this.heldItem2Dropdown.Location = new System.Drawing.Point(322, 82);
            this.heldItem2Dropdown.Name = "heldItem2Dropdown";
            this.heldItem2Dropdown.Size = new System.Drawing.Size(120, 24);
            this.heldItem2Dropdown.TabIndex = 75;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(332, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 74;
            this.label2.Text = "Held Items";
            // 
            // pkGenderRatioDropdown
            // 
            this.pkGenderRatioDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.pkGenderRatioDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.pkGenderRatioDropdown.FormattingEnabled = true;
            this.pkGenderRatioDropdown.Location = new System.Drawing.Point(110, 82);
            this.pkGenderRatioDropdown.Name = "pkGenderRatioDropdown";
            this.pkGenderRatioDropdown.Size = new System.Drawing.Size(140, 24);
            this.pkGenderRatioDropdown.TabIndex = 72;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 73;
            this.label1.Text = "Gender Ratio:";
            // 
            // levelUpMovesGroup
            // 
            this.levelUpMovesGroup.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.levelUpMovesGroup.Controls.Add(this.learnsetApplyMoveButton);
            this.levelUpMovesGroup.Controls.Add(this.learnsetListBox);
            this.levelUpMovesGroup.Controls.Add(this.learnsetMoveDropdown);
            this.levelUpMovesGroup.Controls.Add(this.addLearnsetMoveButton);
            this.levelUpMovesGroup.Controls.Add(this.label21);
            this.levelUpMovesGroup.Controls.Add(this.removeLearnsetMoveButton);
            this.levelUpMovesGroup.Controls.Add(this.learnsetLevelNumberBox);
            this.levelUpMovesGroup.Controls.Add(this.copyLearnsetButton);
            this.levelUpMovesGroup.Controls.Add(this.label20);
            this.levelUpMovesGroup.Controls.Add(this.pasteLearnsetButton);
            this.levelUpMovesGroup.Enabled = false;
            this.levelUpMovesGroup.Location = new System.Drawing.Point(500, 60);
            this.levelUpMovesGroup.Name = "levelUpMovesGroup";
            this.levelUpMovesGroup.Size = new System.Drawing.Size(420, 340);
            this.levelUpMovesGroup.TabIndex = 70;
            this.levelUpMovesGroup.TabStop = false;
            this.levelUpMovesGroup.Text = "Level Up Moves";
            // 
            // learnsetApplyMoveButton
            // 
            this.learnsetApplyMoveButton.Location = new System.Drawing.Point(220, 222);
            this.learnsetApplyMoveButton.Name = "learnsetApplyMoveButton";
            this.learnsetApplyMoveButton.Size = new System.Drawing.Size(100, 25);
            this.learnsetApplyMoveButton.TabIndex = 83;
            this.learnsetApplyMoveButton.Text = "Apply Move";
            this.learnsetApplyMoveButton.UseVisualStyleBackColor = true;
            this.learnsetApplyMoveButton.Click += new System.EventHandler(this.ApplyMoveSlot);
            // 
            // learnsetListBox
            // 
            this.learnsetListBox.FormattingEnabled = true;
            this.learnsetListBox.ItemHeight = 16;
            this.learnsetListBox.Location = new System.Drawing.Point(37, 70);
            this.learnsetListBox.Name = "learnsetListBox";
            this.learnsetListBox.Size = new System.Drawing.Size(160, 212);
            this.learnsetListBox.TabIndex = 74;
            this.learnsetListBox.SelectedIndexChanged += new System.EventHandler(this.ChangeMoveSlot);
            // 
            // learnsetMoveDropdown
            // 
            this.learnsetMoveDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.learnsetMoveDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.learnsetMoveDropdown.FormattingEnabled = true;
            this.learnsetMoveDropdown.Location = new System.Drawing.Point(267, 136);
            this.learnsetMoveDropdown.Name = "learnsetMoveDropdown";
            this.learnsetMoveDropdown.Size = new System.Drawing.Size(120, 24);
            this.learnsetMoveDropdown.TabIndex = 82;
            // 
            // addLearnsetMoveButton
            // 
            this.addLearnsetMoveButton.Location = new System.Drawing.Point(37, 294);
            this.addLearnsetMoveButton.Name = "addLearnsetMoveButton";
            this.addLearnsetMoveButton.Size = new System.Drawing.Size(70, 25);
            this.addLearnsetMoveButton.TabIndex = 75;
            this.addLearnsetMoveButton.Text = "Add";
            this.addLearnsetMoveButton.UseVisualStyleBackColor = true;
            this.addLearnsetMoveButton.Click += new System.EventHandler(this.AddToLearnset);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(217, 180);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(41, 16);
            this.label21.TabIndex = 80;
            this.label21.Text = "Level:";
            // 
            // removeLearnsetMoveButton
            // 
            this.removeLearnsetMoveButton.Location = new System.Drawing.Point(127, 294);
            this.removeLearnsetMoveButton.Name = "removeLearnsetMoveButton";
            this.removeLearnsetMoveButton.Size = new System.Drawing.Size(70, 25);
            this.removeLearnsetMoveButton.TabIndex = 76;
            this.removeLearnsetMoveButton.Text = "Remove";
            this.removeLearnsetMoveButton.UseVisualStyleBackColor = true;
            this.removeLearnsetMoveButton.Click += new System.EventHandler(this.RemoveFromLearnset);
            // 
            // learnsetLevelNumberBox
            // 
            this.learnsetLevelNumberBox.Location = new System.Drawing.Point(267, 176);
            this.learnsetLevelNumberBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.learnsetLevelNumberBox.Name = "learnsetLevelNumberBox";
            this.learnsetLevelNumberBox.Size = new System.Drawing.Size(40, 22);
            this.learnsetLevelNumberBox.TabIndex = 81;
            this.learnsetLevelNumberBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // copyLearnsetButton
            // 
            this.copyLearnsetButton.Location = new System.Drawing.Point(37, 30);
            this.copyLearnsetButton.Name = "copyLearnsetButton";
            this.copyLearnsetButton.Size = new System.Drawing.Size(70, 25);
            this.copyLearnsetButton.TabIndex = 77;
            this.copyLearnsetButton.Text = "Copy";
            this.copyLearnsetButton.UseVisualStyleBackColor = true;
            this.copyLearnsetButton.Click += new System.EventHandler(this.CopyLearnset);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(217, 140);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(42, 16);
            this.label20.TabIndex = 79;
            this.label20.Text = "Move:";
            // 
            // pasteLearnsetButton
            // 
            this.pasteLearnsetButton.Enabled = false;
            this.pasteLearnsetButton.Location = new System.Drawing.Point(128, 30);
            this.pasteLearnsetButton.Name = "pasteLearnsetButton";
            this.pasteLearnsetButton.Size = new System.Drawing.Size(70, 25);
            this.pasteLearnsetButton.TabIndex = 78;
            this.pasteLearnsetButton.Text = "Paste";
            this.pasteLearnsetButton.UseVisualStyleBackColor = true;
            this.pasteLearnsetButton.Click += new System.EventHandler(this.PasteLearnset);
            // 
            // tmMovesGroupBox
            // 
            this.tmMovesGroupBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tmMovesGroupBox.Controls.Add(this.tmMovesListBox);
            this.tmMovesGroupBox.Enabled = false;
            this.tmMovesGroupBox.Location = new System.Drawing.Point(940, 60);
            this.tmMovesGroupBox.Name = "tmMovesGroupBox";
            this.tmMovesGroupBox.Size = new System.Drawing.Size(220, 340);
            this.tmMovesGroupBox.TabIndex = 84;
            this.tmMovesGroupBox.TabStop = false;
            this.tmMovesGroupBox.Text = "TM Moves";
            // 
            // tmMovesListBox
            // 
            this.tmMovesListBox.CheckOnClick = true;
            this.tmMovesListBox.FormattingEnabled = true;
            this.tmMovesListBox.Location = new System.Drawing.Point(5, 20);
            this.tmMovesListBox.Name = "tmMovesListBox";
            this.tmMovesListBox.Size = new System.Drawing.Size(210, 310);
            this.tmMovesListBox.TabIndex = 0;
            // 
            // evolutionsGroupBox
            // 
            this.evolutionsGroupBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.evolutionsGroupBox.Controls.Add(this.evolutionApplyButton);
            this.evolutionsGroupBox.Controls.Add(this.evolutionIntoDropdown);
            this.evolutionsGroupBox.Controls.Add(this.evolutionConditionDropdown);
            this.evolutionsGroupBox.Controls.Add(this.evolutionMethodDropdown);
            this.evolutionsGroupBox.Controls.Add(this.label29);
            this.evolutionsGroupBox.Controls.Add(this.label28);
            this.evolutionsGroupBox.Controls.Add(this.label27);
            this.evolutionsGroupBox.Controls.Add(this.evolutionsListBox);
            this.evolutionsGroupBox.Location = new System.Drawing.Point(500, 410);
            this.evolutionsGroupBox.Name = "evolutionsGroupBox";
            this.evolutionsGroupBox.Size = new System.Drawing.Size(660, 200);
            this.evolutionsGroupBox.TabIndex = 85;
            this.evolutionsGroupBox.TabStop = false;
            this.evolutionsGroupBox.Text = "Evolutions";
            // 
            // evolutionApplyButton
            // 
            this.evolutionApplyButton.Location = new System.Drawing.Point(20, 160);
            this.evolutionApplyButton.Name = "evolutionApplyButton";
            this.evolutionApplyButton.Size = new System.Drawing.Size(80, 25);
            this.evolutionApplyButton.TabIndex = 51;
            this.evolutionApplyButton.Text = "Apply";
            this.evolutionApplyButton.UseVisualStyleBackColor = true;
            this.evolutionApplyButton.Click += new System.EventHandler(this.ApplyEvolution);
            // 
            // evolutionIntoDropdown
            // 
            this.evolutionIntoDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.evolutionIntoDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.evolutionIntoDropdown.FormattingEnabled = true;
            this.evolutionIntoDropdown.Location = new System.Drawing.Point(440, 116);
            this.evolutionIntoDropdown.Name = "evolutionIntoDropdown";
            this.evolutionIntoDropdown.Size = new System.Drawing.Size(140, 24);
            this.evolutionIntoDropdown.TabIndex = 50;
            // 
            // evolutionConditionDropdown
            // 
            this.evolutionConditionDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.evolutionConditionDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.evolutionConditionDropdown.FormattingEnabled = true;
            this.evolutionConditionDropdown.Location = new System.Drawing.Point(440, 76);
            this.evolutionConditionDropdown.Name = "evolutionConditionDropdown";
            this.evolutionConditionDropdown.Size = new System.Drawing.Size(140, 24);
            this.evolutionConditionDropdown.TabIndex = 49;
            // 
            // evolutionMethodDropdown
            // 
            this.evolutionMethodDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.evolutionMethodDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.evolutionMethodDropdown.FormattingEnabled = true;
            this.evolutionMethodDropdown.Location = new System.Drawing.Point(440, 36);
            this.evolutionMethodDropdown.Name = "evolutionMethodDropdown";
            this.evolutionMethodDropdown.Size = new System.Drawing.Size(200, 24);
            this.evolutionMethodDropdown.TabIndex = 48;
            this.evolutionMethodDropdown.SelectedIndexChanged += new System.EventHandler(this.ChangeEvolutionMethod);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(350, 120);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(33, 16);
            this.label29.TabIndex = 47;
            this.label29.Text = "Into:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(350, 80);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(66, 16);
            this.label28.TabIndex = 46;
            this.label28.Text = "Condition:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(350, 40);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(55, 16);
            this.label27.TabIndex = 45;
            this.label27.Text = "Method:";
            // 
            // evolutionsListBox
            // 
            this.evolutionsListBox.FormattingEnabled = true;
            this.evolutionsListBox.ItemHeight = 16;
            this.evolutionsListBox.Location = new System.Drawing.Point(20, 30);
            this.evolutionsListBox.Name = "evolutionsListBox";
            this.evolutionsListBox.Size = new System.Drawing.Size(300, 116);
            this.evolutionsListBox.TabIndex = 44;
            this.evolutionsListBox.SelectedIndexChanged += new System.EventHandler(this.LoadEvolutionMethod);
            // 
            // PokemonEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 681);
            this.Controls.Add(this.evolutionsGroupBox);
            this.Controls.Add(this.tmMovesGroupBox);
            this.Controls.Add(this.levelUpMovesGroup);
            this.Controls.Add(this.miscStatsGroup);
            this.Controls.Add(this.baseStatsGroup);
            this.Controls.Add(this.applyPokemonButton);
            this.Controls.Add(this.pokemonNameDropdown);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PokemonEditor";
            this.Text = "Pokemon Editor";
            ((System.ComponentModel.ISupportInitialize)(this.pokemonBaseSpeedNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pokemonBaseSpDefNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pokemonBaseSpAttNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pokemonBaseDefenseNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pokemonBaseAttackNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pokemonBaseHpNumberBox)).EndInit();
            this.baseStatsGroup.ResumeLayout(false);
            this.baseStatsGroup.PerformLayout();
            this.miscStatsGroup.ResumeLayout(false);
            this.miscStatsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xpYieldNumberBox)).EndInit();
            this.levelUpMovesGroup.ResumeLayout(false);
            this.levelUpMovesGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.learnsetLevelNumberBox)).EndInit();
            this.tmMovesGroupBox.ResumeLayout(false);
            this.evolutionsGroupBox.ResumeLayout(false);
            this.evolutionsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox pokemonNameDropdown;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ComboBox pkLevelRateDropdown;
        private System.Windows.Forms.Label pokemonBSTText;
        private System.Windows.Forms.Button applyPokemonButton;
        private System.Windows.Forms.NumericUpDown pokemonBaseSpeedNumberBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown pokemonBaseSpDefNumberBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown pokemonBaseSpAttNumberBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown pokemonBaseDefenseNumberBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown pokemonBaseAttackNumberBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown pokemonBaseHpNumberBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox pokemonTypeDropdown2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox pokemonTypeDropdown1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox pokeAbilityDropdown3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox pokeAbilityDropdown2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox pokeAbilityDropdown1;
        private System.Windows.Forms.GroupBox baseStatsGroup;
        private System.Windows.Forms.GroupBox miscStatsGroup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox heldItem3Dropdown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox heldItem2Dropdown;
        private System.Windows.Forms.ComboBox heldItem1Dropdown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox pkGenderRatioDropdown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox levelUpMovesGroup;
        private System.Windows.Forms.Button learnsetApplyMoveButton;
        private System.Windows.Forms.ListBox learnsetListBox;
        private System.Windows.Forms.ComboBox learnsetMoveDropdown;
        private System.Windows.Forms.Button addLearnsetMoveButton;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button removeLearnsetMoveButton;
        private System.Windows.Forms.NumericUpDown learnsetLevelNumberBox;
        private System.Windows.Forms.Button copyLearnsetButton;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button pasteLearnsetButton;
        private System.Windows.Forms.NumericUpDown xpYieldNumberBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox tmMovesGroupBox;
        private System.Windows.Forms.CheckedListBox tmMovesListBox;
        private System.Windows.Forms.GroupBox evolutionsGroupBox;
        private System.Windows.Forms.Button evolutionApplyButton;
        private System.Windows.Forms.ComboBox evolutionIntoDropdown;
        private System.Windows.Forms.ComboBox evolutionConditionDropdown;
        private System.Windows.Forms.ComboBox evolutionMethodDropdown;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ListBox evolutionsListBox;
    }
}
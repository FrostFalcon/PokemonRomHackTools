
namespace NewEditor.Forms
{
    partial class OverworldEditor
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolTip mapTypeTooltip;
            this.mapTypeNumberBox = new System.Windows.Forms.NumericUpDown();
            this.scriptFileNumberBox = new System.Windows.Forms.NumericUpDown();
            this.textFileNumberBox = new System.Windows.Forms.NumericUpDown();
            this.encounterFileNumberBox = new System.Windows.Forms.NumericUpDown();
            this.mapIDNumberBox = new System.Windows.Forms.NumericUpDown();
            this.parentMapIDNumberBox = new System.Windows.Forms.NumericUpDown();
            this.zoneIdDropdown = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.mapNameDropdown = new System.Windows.Forms.ComboBox();
            this.applyZoneButton = new System.Windows.Forms.Button();
            this.openTextFileButton = new System.Windows.Forms.Button();
            this.mapMatrixNumberBox = new System.Windows.Forms.NumericUpDown();
            this.openScriptFileButton = new System.Windows.Forms.Button();
            mapTypeTooltip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.mapTypeNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scriptFileNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textFileNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.encounterFileNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapIDNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.parentMapIDNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapMatrixNumberBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mapTypeNumberBox
            // 
            this.mapTypeNumberBox.Enabled = false;
            this.mapTypeNumberBox.Location = new System.Drawing.Point(100, 86);
            this.mapTypeNumberBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.mapTypeNumberBox.Name = "mapTypeNumberBox";
            this.mapTypeNumberBox.Size = new System.Drawing.Size(58, 22);
            this.mapTypeNumberBox.TabIndex = 58;
            mapTypeTooltip.SetToolTip(this.mapTypeNumberBox, "Not entirely sure how this works. I think 0 is for outside and 16 is for inside,\r" +
        "\nbut aside from that, probably don\'t touch this.");
            this.mapTypeNumberBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // scriptFileNumberBox
            // 
            this.scriptFileNumberBox.Enabled = false;
            this.scriptFileNumberBox.Location = new System.Drawing.Point(100, 146);
            this.scriptFileNumberBox.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.scriptFileNumberBox.Name = "scriptFileNumberBox";
            this.scriptFileNumberBox.Size = new System.Drawing.Size(58, 22);
            this.scriptFileNumberBox.TabIndex = 62;
            this.scriptFileNumberBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textFileNumberBox
            // 
            this.textFileNumberBox.Enabled = false;
            this.textFileNumberBox.Location = new System.Drawing.Point(100, 176);
            this.textFileNumberBox.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.textFileNumberBox.Name = "textFileNumberBox";
            this.textFileNumberBox.Size = new System.Drawing.Size(58, 22);
            this.textFileNumberBox.TabIndex = 64;
            this.textFileNumberBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // encounterFileNumberBox
            // 
            this.encounterFileNumberBox.Enabled = false;
            this.encounterFileNumberBox.Location = new System.Drawing.Point(100, 206);
            this.encounterFileNumberBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.encounterFileNumberBox.Name = "encounterFileNumberBox";
            this.encounterFileNumberBox.Size = new System.Drawing.Size(58, 22);
            this.encounterFileNumberBox.TabIndex = 66;
            this.encounterFileNumberBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // mapIDNumberBox
            // 
            this.mapIDNumberBox.Enabled = false;
            this.mapIDNumberBox.Location = new System.Drawing.Point(100, 236);
            this.mapIDNumberBox.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.mapIDNumberBox.Name = "mapIDNumberBox";
            this.mapIDNumberBox.Size = new System.Drawing.Size(58, 22);
            this.mapIDNumberBox.TabIndex = 68;
            this.mapIDNumberBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // parentMapIDNumberBox
            // 
            this.parentMapIDNumberBox.Enabled = false;
            this.parentMapIDNumberBox.Location = new System.Drawing.Point(100, 266);
            this.parentMapIDNumberBox.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.parentMapIDNumberBox.Name = "parentMapIDNumberBox";
            this.parentMapIDNumberBox.Size = new System.Drawing.Size(58, 22);
            this.parentMapIDNumberBox.TabIndex = 70;
            this.parentMapIDNumberBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // zoneIdDropdown
            // 
            this.zoneIdDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.zoneIdDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.zoneIdDropdown.FormattingEnabled = true;
            this.zoneIdDropdown.Location = new System.Drawing.Point(20, 20);
            this.zoneIdDropdown.Name = "zoneIdDropdown";
            this.zoneIdDropdown.Size = new System.Drawing.Size(166, 24);
            this.zoneIdDropdown.TabIndex = 2;
            this.zoneIdDropdown.SelectedIndexChanged += new System.EventHandler(this.LoadZoneIntoEditor);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 90);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 16);
            this.label13.TabIndex = 57;
            this.label13.Text = "Map Type:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 59;
            this.label1.Text = "Map Matrix:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 61;
            this.label2.Text = "Script File:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 16);
            this.label3.TabIndex = 63;
            this.label3.Text = "Text File:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 16);
            this.label4.TabIndex = 65;
            this.label4.Text = "Encounters:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 16);
            this.label5.TabIndex = 67;
            this.label5.Text = "Map ID:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 270);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 16);
            this.label6.TabIndex = 69;
            this.label6.Text = "Parent Map:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(240, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 16);
            this.label7.TabIndex = 71;
            this.label7.Text = "Name:";
            // 
            // mapNameDropdown
            // 
            this.mapNameDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.mapNameDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.mapNameDropdown.Enabled = false;
            this.mapNameDropdown.FormattingEnabled = true;
            this.mapNameDropdown.Location = new System.Drawing.Point(300, 20);
            this.mapNameDropdown.Name = "mapNameDropdown";
            this.mapNameDropdown.Size = new System.Drawing.Size(166, 24);
            this.mapNameDropdown.TabIndex = 72;
            // 
            // applyZoneButton
            // 
            this.applyZoneButton.Enabled = false;
            this.applyZoneButton.Location = new System.Drawing.Point(30, 300);
            this.applyZoneButton.Name = "applyZoneButton";
            this.applyZoneButton.Size = new System.Drawing.Size(120, 40);
            this.applyZoneButton.TabIndex = 73;
            this.applyZoneButton.Text = "Apply Area";
            this.applyZoneButton.UseVisualStyleBackColor = true;
            this.applyZoneButton.Click += new System.EventHandler(this.ApplyZoneData);
            // 
            // openTextFileButton
            // 
            this.openTextFileButton.Location = new System.Drawing.Point(180, 175);
            this.openTextFileButton.Name = "openTextFileButton";
            this.openTextFileButton.Size = new System.Drawing.Size(75, 24);
            this.openTextFileButton.TabIndex = 74;
            this.openTextFileButton.Text = "Open";
            this.openTextFileButton.UseVisualStyleBackColor = true;
            this.openTextFileButton.Click += new System.EventHandler(this.openTextFileButton_Click);
            // 
            // mapMatrixNumberBox
            // 
            this.mapMatrixNumberBox.Enabled = false;
            this.mapMatrixNumberBox.Location = new System.Drawing.Point(100, 116);
            this.mapMatrixNumberBox.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.mapMatrixNumberBox.Name = "mapMatrixNumberBox";
            this.mapMatrixNumberBox.Size = new System.Drawing.Size(58, 22);
            this.mapMatrixNumberBox.TabIndex = 0;
            this.mapMatrixNumberBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // openScriptFileButton
            // 
            this.openScriptFileButton.Location = new System.Drawing.Point(180, 144);
            this.openScriptFileButton.Name = "openScriptFileButton";
            this.openScriptFileButton.Size = new System.Drawing.Size(75, 24);
            this.openScriptFileButton.TabIndex = 75;
            this.openScriptFileButton.Text = "Open";
            this.openScriptFileButton.UseVisualStyleBackColor = true;
            this.openScriptFileButton.Click += new System.EventHandler(this.openScriptFileButton_Click);
            // 
            // OverworldEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 361);
            this.Controls.Add(this.openScriptFileButton);
            this.Controls.Add(this.mapMatrixNumberBox);
            this.Controls.Add(this.openTextFileButton);
            this.Controls.Add(this.applyZoneButton);
            this.Controls.Add(this.mapNameDropdown);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.parentMapIDNumberBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.mapIDNumberBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.encounterFileNumberBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textFileNumberBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.scriptFileNumberBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mapTypeNumberBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.zoneIdDropdown);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "OverworldEditor";
            this.Text = "OverworldEditor";
            ((System.ComponentModel.ISupportInitialize)(this.mapTypeNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scriptFileNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textFileNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.encounterFileNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapIDNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.parentMapIDNumberBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapMatrixNumberBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox zoneIdDropdown;
        private System.Windows.Forms.NumericUpDown mapTypeNumberBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown scriptFileNumberBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown textFileNumberBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown encounterFileNumberBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown mapIDNumberBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown parentMapIDNumberBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox mapNameDropdown;
        private System.Windows.Forms.Button applyZoneButton;
        private System.Windows.Forms.Button openTextFileButton;
        private System.Windows.Forms.NumericUpDown mapMatrixNumberBox;
        private System.Windows.Forms.Button openScriptFileButton;
    }
}
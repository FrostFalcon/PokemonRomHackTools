
namespace NewEditor.Forms
{
    partial class MainEditor
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
            this.openRomButton = new System.Windows.Forms.Button();
            this.romTypeText = new System.Windows.Forms.Label();
            this.romNameText = new System.Windows.Forms.Label();
            this.saveRomButton = new System.Windows.Forms.Button();
            this.openTextViewerButton = new System.Windows.Forms.Button();
            this.openPokemonEditorButton = new System.Windows.Forms.Button();
            this.openOverworldEditorButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.openTypeSwapEditorButton = new System.Windows.Forms.Button();
            this.openMoveEditorButton = new System.Windows.Forms.Button();
            this.openScriptEditorButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openRomButton
            // 
            this.openRomButton.Font = new System.Drawing.Font("Arial", 9.75F);
            this.openRomButton.Location = new System.Drawing.Point(1072, 12);
            this.openRomButton.Name = "openRomButton";
            this.openRomButton.Size = new System.Drawing.Size(100, 40);
            this.openRomButton.TabIndex = 1;
            this.openRomButton.Text = "Open Rom";
            this.openRomButton.UseVisualStyleBackColor = true;
            this.openRomButton.Click += new System.EventHandler(this.OpenRom);
            // 
            // romTypeText
            // 
            this.romTypeText.AutoSize = true;
            this.romTypeText.Font = new System.Drawing.Font("Arial", 9.75F);
            this.romTypeText.Location = new System.Drawing.Point(12, 38);
            this.romTypeText.Name = "romTypeText";
            this.romTypeText.Size = new System.Drawing.Size(74, 16);
            this.romTypeText.TabIndex = 4;
            this.romTypeText.Text = "Rom Type: ";
            // 
            // romNameText
            // 
            this.romNameText.AutoSize = true;
            this.romNameText.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.romNameText.Location = new System.Drawing.Point(12, 12);
            this.romNameText.MaximumSize = new System.Drawing.Size(600, 20);
            this.romNameText.Name = "romNameText";
            this.romNameText.Size = new System.Drawing.Size(43, 16);
            this.romNameText.TabIndex = 3;
            this.romNameText.Text = "Rom: ";
            // 
            // saveRomButton
            // 
            this.saveRomButton.Enabled = false;
            this.saveRomButton.Font = new System.Drawing.Font("Arial", 9.75F);
            this.saveRomButton.Location = new System.Drawing.Point(1072, 58);
            this.saveRomButton.Name = "saveRomButton";
            this.saveRomButton.Size = new System.Drawing.Size(100, 40);
            this.saveRomButton.TabIndex = 6;
            this.saveRomButton.Text = "Save Rom";
            this.saveRomButton.UseVisualStyleBackColor = true;
            this.saveRomButton.Click += new System.EventHandler(this.SaveRom);
            // 
            // openTextViewerButton
            // 
            this.openTextViewerButton.Location = new System.Drawing.Point(20, 550);
            this.openTextViewerButton.Name = "openTextViewerButton";
            this.openTextViewerButton.Size = new System.Drawing.Size(120, 32);
            this.openTextViewerButton.TabIndex = 7;
            this.openTextViewerButton.Text = "Text Viewer";
            this.openTextViewerButton.UseVisualStyleBackColor = true;
            this.openTextViewerButton.Click += new System.EventHandler(OpenTextViewer);
            // 
            // openPokemonEditorButton
            // 
            this.openPokemonEditorButton.Location = new System.Drawing.Point(20, 510);
            this.openPokemonEditorButton.Name = "openPokemonEditorButton";
            this.openPokemonEditorButton.Size = new System.Drawing.Size(120, 32);
            this.openPokemonEditorButton.TabIndex = 8;
            this.openPokemonEditorButton.Text = "Pokemon Editor";
            this.openPokemonEditorButton.UseVisualStyleBackColor = true;
            this.openPokemonEditorButton.Click += new System.EventHandler(OpenPokemonEditor);
            // 
            // openOverworldEditorButton
            // 
            this.openOverworldEditorButton.Location = new System.Drawing.Point(160, 550);
            this.openOverworldEditorButton.Name = "openOverworldEditorButton";
            this.openOverworldEditorButton.Size = new System.Drawing.Size(120, 32);
            this.openOverworldEditorButton.TabIndex = 9;
            this.openOverworldEditorButton.Text = "Overworld Editor";
            this.openOverworldEditorButton.UseVisualStyleBackColor = true;
            this.openOverworldEditorButton.Click += new System.EventHandler(OpenOverworldEditor);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Controls.Add(this.openTypeSwapEditorButton);
            this.groupBox1.Location = new System.Drawing.Point(1000, 280);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(160, 300);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Custom Game Modes";
            // 
            // openTypeSwapEditorButton
            // 
            this.openTypeSwapEditorButton.Location = new System.Drawing.Point(30, 30);
            this.openTypeSwapEditorButton.Name = "openTypeSwapEditorButton";
            this.openTypeSwapEditorButton.Size = new System.Drawing.Size(100, 32);
            this.openTypeSwapEditorButton.TabIndex = 11;
            this.openTypeSwapEditorButton.Text = "Type Swap";
            this.openTypeSwapEditorButton.UseVisualStyleBackColor = true;
            this.openTypeSwapEditorButton.Click += new System.EventHandler(this.OpenTypeSwapEditor);
            // 
            // openMoveEditorButton
            // 
            this.openMoveEditorButton.Location = new System.Drawing.Point(20, 470);
            this.openMoveEditorButton.Name = "openMoveEditorButton";
            this.openMoveEditorButton.Size = new System.Drawing.Size(120, 32);
            this.openMoveEditorButton.TabIndex = 11;
            this.openMoveEditorButton.Text = "Move Editor";
            this.openMoveEditorButton.UseVisualStyleBackColor = true;
            this.openMoveEditorButton.Click += new System.EventHandler(this.OpenMoveEditor);
            // 
            // openScriptEditorButton
            // 
            this.openScriptEditorButton.Location = new System.Drawing.Point(160, 510);
            this.openScriptEditorButton.Name = "openScriptEditorButton";
            this.openScriptEditorButton.Size = new System.Drawing.Size(120, 32);
            this.openScriptEditorButton.TabIndex = 12;
            this.openScriptEditorButton.Text = "Script Editor";
            this.openScriptEditorButton.UseVisualStyleBackColor = true;
            this.openScriptEditorButton.Click += new System.EventHandler(OpenScriptEditor);
            // 
            // MainEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 601);
            this.Controls.Add(this.openScriptEditorButton);
            this.Controls.Add(this.openMoveEditorButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.openOverworldEditorButton);
            this.Controls.Add(this.openPokemonEditorButton);
            this.Controls.Add(this.openTextViewerButton);
            this.Controls.Add(this.saveRomButton);
            this.Controls.Add(this.romTypeText);
            this.Controls.Add(this.romNameText);
            this.Controls.Add(this.openRomButton);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainEditor";
            this.Text = "New Editor";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openRomButton;
        private System.Windows.Forms.Label romTypeText;
        private System.Windows.Forms.Label romNameText;
        private System.Windows.Forms.Button saveRomButton;
        private System.Windows.Forms.Button openTextViewerButton;
        private System.Windows.Forms.Button openPokemonEditorButton;
        private System.Windows.Forms.Button openOverworldEditorButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button openTypeSwapEditorButton;
        private System.Windows.Forms.Button openMoveEditorButton;
        private System.Windows.Forms.Button openScriptEditorButton;
    }
}



namespace ChangeLogGenerator
{
    partial class ChangeLogGenerator
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
            this.includePokemonCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.includeEncountersCheckBox = new System.Windows.Forms.CheckBox();
            this.includeTrainerTextBox = new System.Windows.Forms.CheckBox();
            this.selectOriginalRomButton = new System.Windows.Forms.Button();
            this.originalRomPathText = new System.Windows.Forms.Label();
            this.newRomPathText = new System.Windows.Forms.Label();
            this.selectNewRomButton = new System.Windows.Forms.Button();
            this.generateChangeLogButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // includePokemonCheckBox
            // 
            this.includePokemonCheckBox.AutoSize = true;
            this.includePokemonCheckBox.Checked = true;
            this.includePokemonCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.includePokemonCheckBox.Location = new System.Drawing.Point(25, 50);
            this.includePokemonCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.includePokemonCheckBox.Name = "includePokemonCheckBox";
            this.includePokemonCheckBox.Size = new System.Drawing.Size(82, 20);
            this.includePokemonCheckBox.TabIndex = 0;
            this.includePokemonCheckBox.Text = "Pokemon";
            this.includePokemonCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Include:";
            // 
            // includeEncountersCheckBox
            // 
            this.includeEncountersCheckBox.AutoSize = true;
            this.includeEncountersCheckBox.Checked = true;
            this.includeEncountersCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.includeEncountersCheckBox.Location = new System.Drawing.Point(25, 78);
            this.includeEncountersCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.includeEncountersCheckBox.Name = "includeEncountersCheckBox";
            this.includeEncountersCheckBox.Size = new System.Drawing.Size(93, 20);
            this.includeEncountersCheckBox.TabIndex = 2;
            this.includeEncountersCheckBox.Text = "Encounters";
            this.includeEncountersCheckBox.UseVisualStyleBackColor = true;
            // 
            // includeTrainerTextBox
            // 
            this.includeTrainerTextBox.AutoSize = true;
            this.includeTrainerTextBox.Checked = true;
            this.includeTrainerTextBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.includeTrainerTextBox.Location = new System.Drawing.Point(25, 106);
            this.includeTrainerTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.includeTrainerTextBox.Name = "includeTrainerTextBox";
            this.includeTrainerTextBox.Size = new System.Drawing.Size(73, 20);
            this.includeTrainerTextBox.TabIndex = 3;
            this.includeTrainerTextBox.Text = "Trainers";
            this.includeTrainerTextBox.UseVisualStyleBackColor = true;
            // 
            // selectOriginalRomButton
            // 
            this.selectOriginalRomButton.Location = new System.Drawing.Point(200, 45);
            this.selectOriginalRomButton.Name = "selectOriginalRomButton";
            this.selectOriginalRomButton.Size = new System.Drawing.Size(100, 30);
            this.selectOriginalRomButton.TabIndex = 4;
            this.selectOriginalRomButton.Text = "Original Rom";
            this.selectOriginalRomButton.UseVisualStyleBackColor = true;
            this.selectOriginalRomButton.Click += new System.EventHandler(this.selectOriginalRomButton_Click);
            // 
            // originalRomPathText
            // 
            this.originalRomPathText.AutoSize = true;
            this.originalRomPathText.Location = new System.Drawing.Point(306, 50);
            this.originalRomPathText.Name = "originalRomPathText";
            this.originalRomPathText.Size = new System.Drawing.Size(40, 16);
            this.originalRomPathText.TabIndex = 5;
            this.originalRomPathText.Text = "-none";
            // 
            // newRomPathText
            // 
            this.newRomPathText.AutoSize = true;
            this.newRomPathText.Location = new System.Drawing.Point(306, 132);
            this.newRomPathText.Name = "newRomPathText";
            this.newRomPathText.Size = new System.Drawing.Size(40, 16);
            this.newRomPathText.TabIndex = 7;
            this.newRomPathText.Text = "-none";
            // 
            // selectNewRomButton
            // 
            this.selectNewRomButton.Location = new System.Drawing.Point(200, 125);
            this.selectNewRomButton.Name = "selectNewRomButton";
            this.selectNewRomButton.Size = new System.Drawing.Size(100, 30);
            this.selectNewRomButton.TabIndex = 6;
            this.selectNewRomButton.Text = "New Rom";
            this.selectNewRomButton.UseVisualStyleBackColor = true;
            this.selectNewRomButton.Click += new System.EventHandler(this.selectNewRomButton_Click);
            // 
            // generateChangeLogButton
            // 
            this.generateChangeLogButton.Enabled = false;
            this.generateChangeLogButton.Location = new System.Drawing.Point(450, 75);
            this.generateChangeLogButton.Name = "generateChangeLogButton";
            this.generateChangeLogButton.Size = new System.Drawing.Size(120, 50);
            this.generateChangeLogButton.TabIndex = 8;
            this.generateChangeLogButton.Text = "Generate";
            this.generateChangeLogButton.UseVisualStyleBackColor = true;
            this.generateChangeLogButton.Click += new System.EventHandler(this.generateChangeLogButton_Click);
            // 
            // ChangeLogGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 211);
            this.Controls.Add(this.generateChangeLogButton);
            this.Controls.Add(this.newRomPathText);
            this.Controls.Add(this.selectNewRomButton);
            this.Controls.Add(this.originalRomPathText);
            this.Controls.Add(this.selectOriginalRomButton);
            this.Controls.Add(this.includeTrainerTextBox);
            this.Controls.Add(this.includeEncountersCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.includePokemonCheckBox);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ChangeLogGenerator";
            this.Text = "Change Log Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox includePokemonCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox includeEncountersCheckBox;
        private System.Windows.Forms.CheckBox includeTrainerTextBox;
        private System.Windows.Forms.Button selectOriginalRomButton;
        private System.Windows.Forms.Label originalRomPathText;
        private System.Windows.Forms.Label newRomPathText;
        private System.Windows.Forms.Button selectNewRomButton;
        private System.Windows.Forms.Button generateChangeLogButton;
    }
}


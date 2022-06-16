
namespace NewEditor.Forms
{
    partial class ScriptEditor
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
            this.scriptFileDropdown = new System.Windows.Forms.ComboBox();
            this.sequenceIDNumberBox = new System.Windows.Forms.NumericUpDown();
            this.commandTypeDropdown = new System.Windows.Forms.ComboBox();
            this.commandsListBox = new System.Windows.Forms.ListBox();
            this.sequenceCountLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sequenceIDNumberBox)).BeginInit();
            this.SuspendLayout();
            // 
            // scriptFileDropdown
            // 
            this.scriptFileDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.scriptFileDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.scriptFileDropdown.FormattingEnabled = true;
            this.scriptFileDropdown.Location = new System.Drawing.Point(14, 16);
            this.scriptFileDropdown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.scriptFileDropdown.Name = "scriptFileDropdown";
            this.scriptFileDropdown.Size = new System.Drawing.Size(193, 24);
            this.scriptFileDropdown.TabIndex = 3;
            this.scriptFileDropdown.SelectedIndexChanged += new System.EventHandler(this.LoadScriptFile);
            // 
            // sequenceIDNumberBox
            // 
            this.sequenceIDNumberBox.Location = new System.Drawing.Point(14, 48);
            this.sequenceIDNumberBox.Name = "sequenceIDNumberBox";
            this.sequenceIDNumberBox.Size = new System.Drawing.Size(60, 22);
            this.sequenceIDNumberBox.TabIndex = 4;
            this.sequenceIDNumberBox.ValueChanged += new System.EventHandler(this.LoadSequence);
            // 
            // commandTypeDropdown
            // 
            this.commandTypeDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.commandTypeDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.commandTypeDropdown.FormattingEnabled = true;
            this.commandTypeDropdown.Location = new System.Drawing.Point(350, 222);
            this.commandTypeDropdown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.commandTypeDropdown.Name = "commandTypeDropdown";
            this.commandTypeDropdown.Size = new System.Drawing.Size(193, 24);
            this.commandTypeDropdown.TabIndex = 5;
            // 
            // commandsListBox
            // 
            this.commandsListBox.FormattingEnabled = true;
            this.commandsListBox.ItemHeight = 16;
            this.commandsListBox.Location = new System.Drawing.Point(14, 222);
            this.commandsListBox.Name = "commandsListBox";
            this.commandsListBox.Size = new System.Drawing.Size(330, 212);
            this.commandsListBox.TabIndex = 6;
            this.commandsListBox.SelectedIndexChanged += new System.EventHandler(this.ChangeSelectedListboxCommand);
            this.commandsListBox.DoubleClick += new System.EventHandler(this.DoubleClickListBox);
            // 
            // sequenceCountLabel
            // 
            this.sequenceCountLabel.AutoSize = true;
            this.sequenceCountLabel.Location = new System.Drawing.Point(80, 52);
            this.sequenceCountLabel.Name = "sequenceCountLabel";
            this.sequenceCountLabel.Size = new System.Drawing.Size(23, 16);
            this.sequenceCountLabel.TabIndex = 7;
            this.sequenceCountLabel.Text = "/ 0";
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(14, 502);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(120, 40);
            this.saveButton.TabIndex = 69;
            this.saveButton.Text = "Save Script File";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveScriptFile);
            // 
            // ScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 554);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.sequenceCountLabel);
            this.Controls.Add(this.commandsListBox);
            this.Controls.Add(this.commandTypeDropdown);
            this.Controls.Add(this.sequenceIDNumberBox);
            this.Controls.Add(this.scriptFileDropdown);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ScriptEditor";
            this.Text = "ScriptEditor";
            ((System.ComponentModel.ISupportInitialize)(this.sequenceIDNumberBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown sequenceIDNumberBox;
        private System.Windows.Forms.ComboBox commandTypeDropdown;
        private System.Windows.Forms.ListBox commandsListBox;
        private System.Windows.Forms.Label sequenceCountLabel;
        private System.Windows.Forms.Button saveButton;
        public System.Windows.Forms.ComboBox scriptFileDropdown;
    }
}

namespace NewEditor.Forms
{
    partial class TextViewer
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
            this.textBoxDisplay = new System.Windows.Forms.RichTextBox();
            this.fileNumComboBox = new System.Windows.Forms.ComboBox();
            this.storyTextRadioButton = new System.Windows.Forms.RadioButton();
            this.miscTextRadioButton = new System.Windows.Forms.RadioButton();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxDisplay
            // 
            this.textBoxDisplay.Location = new System.Drawing.Point(12, 73);
            this.textBoxDisplay.Name = "textBoxDisplay";
            this.textBoxDisplay.ReadOnly = true;
            this.textBoxDisplay.Size = new System.Drawing.Size(920, 356);
            this.textBoxDisplay.TabIndex = 0;
            this.textBoxDisplay.Text = "";
            this.textBoxDisplay.WordWrap = false;
            // 
            // fileNumComboBox
            // 
            this.fileNumComboBox.FormattingEnabled = true;
            this.fileNumComboBox.Location = new System.Drawing.Point(12, 20);
            this.fileNumComboBox.Name = "fileNumComboBox";
            this.fileNumComboBox.Size = new System.Drawing.Size(69, 24);
            this.fileNumComboBox.TabIndex = 1;
            this.fileNumComboBox.SelectedIndexChanged += new System.EventHandler(this.LoadTextbox);
            // 
            // storyTextRadioButton
            // 
            this.storyTextRadioButton.AutoSize = true;
            this.storyTextRadioButton.Location = new System.Drawing.Point(840, 20);
            this.storyTextRadioButton.Name = "storyTextRadioButton";
            this.storyTextRadioButton.Size = new System.Drawing.Size(85, 20);
            this.storyTextRadioButton.TabIndex = 2;
            this.storyTextRadioButton.Text = "Story Text";
            this.storyTextRadioButton.UseVisualStyleBackColor = true;
            // 
            // miscTextRadioButton
            // 
            this.miscTextRadioButton.AutoSize = true;
            this.miscTextRadioButton.Checked = true;
            this.miscTextRadioButton.Location = new System.Drawing.Point(740, 20);
            this.miscTextRadioButton.Name = "miscTextRadioButton";
            this.miscTextRadioButton.Size = new System.Drawing.Size(82, 20);
            this.miscTextRadioButton.TabIndex = 3;
            this.miscTextRadioButton.TabStop = true;
            this.miscTextRadioButton.Text = "Misc Text";
            this.miscTextRadioButton.UseVisualStyleBackColor = true;
            this.miscTextRadioButton.CheckedChanged += new System.EventHandler(this.ChangeNarc);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(225, 20);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(150, 22);
            this.searchTextBox.TabIndex = 4;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(144, 19);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 24);
            this.searchButton.TabIndex = 5;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.FilterFiles);
            // 
            // TextViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 441);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.miscTextRadioButton);
            this.Controls.Add(this.storyTextRadioButton);
            this.Controls.Add(this.fileNumComboBox);
            this.Controls.Add(this.textBoxDisplay);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TextViewer";
            this.Text = "TextViewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox textBoxDisplay;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchButton;
        public System.Windows.Forms.ComboBox fileNumComboBox;
        public System.Windows.Forms.RadioButton storyTextRadioButton;
        public System.Windows.Forms.RadioButton miscTextRadioButton;
    }
}
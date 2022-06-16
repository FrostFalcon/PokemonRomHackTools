
namespace MapRandomizer
{
    partial class MapRandomizer
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
            this.romNameText = new System.Windows.Forms.Label();
            this.openRomButton = new System.Windows.Forms.Button();
            this.randomizeRomButton = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // romNameText
            // 
            this.romNameText.AutoSize = true;
            this.romNameText.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.romNameText.Location = new System.Drawing.Point(150, 95);
            this.romNameText.Name = "romNameText";
            this.romNameText.Size = new System.Drawing.Size(39, 15);
            this.romNameText.TabIndex = 7;
            this.romNameText.Text = "-none";
            // 
            // openRomButton
            // 
            this.openRomButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openRomButton.Location = new System.Drawing.Point(40, 90);
            this.openRomButton.Name = "openRomButton";
            this.openRomButton.Size = new System.Drawing.Size(100, 30);
            this.openRomButton.TabIndex = 6;
            this.openRomButton.Text = "Open Rom";
            this.openRomButton.UseVisualStyleBackColor = true;
            this.openRomButton.Click += new System.EventHandler(this.openRomButton_Click);
            // 
            // randomizeRomButton
            // 
            this.randomizeRomButton.Enabled = false;
            this.randomizeRomButton.Location = new System.Drawing.Point(300, 80);
            this.randomizeRomButton.Name = "randomizeRomButton";
            this.randomizeRomButton.Size = new System.Drawing.Size(120, 50);
            this.randomizeRomButton.TabIndex = 9;
            this.randomizeRomButton.Text = "Randomize";
            this.randomizeRomButton.UseVisualStyleBackColor = true;
            this.randomizeRomButton.Click += new System.EventHandler(this.randomizeRomButton_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(169, 157);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(174, 95);
            this.listBox1.TabIndex = 10;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(19, 169);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // MapRandomizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 264);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.randomizeRomButton);
            this.Controls.Add(this.romNameText);
            this.Controls.Add(this.openRomButton);
            this.Name = "MapRandomizer";
            this.Text = "Map Randomizer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label romNameText;
        private System.Windows.Forms.Button openRomButton;
        private System.Windows.Forms.Button randomizeRomButton;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}


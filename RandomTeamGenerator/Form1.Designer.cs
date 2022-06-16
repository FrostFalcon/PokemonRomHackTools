
namespace RandomTeamGenerator
{
    partial class Form1
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
            this.generateButton = new System.Windows.Forms.Button();
            this.starterText = new System.Windows.Forms.Label();
            this.pk1Text = new System.Windows.Forms.Label();
            this.pk3Text = new System.Windows.Forms.Label();
            this.pk5Text = new System.Windows.Forms.Label();
            this.pk2Text = new System.Windows.Forms.Label();
            this.pk4Text = new System.Windows.Forms.Label();
            this.pk6Text = new System.Windows.Forms.Label();
            this.b2Button = new System.Windows.Forms.RadioButton();
            this.w2Button = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(187, 30);
            this.generateButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(133, 62);
            this.generateButton.TabIndex = 0;
            this.generateButton.Text = "Generate";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // starterText
            // 
            this.starterText.Location = new System.Drawing.Point(173, 121);
            this.starterText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.starterText.Name = "starterText";
            this.starterText.Size = new System.Drawing.Size(160, 20);
            this.starterText.TabIndex = 1;
            this.starterText.Text = "---";
            this.starterText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pk1Text
            // 
            this.pk1Text.Location = new System.Drawing.Point(82, 167);
            this.pk1Text.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.pk1Text.Name = "pk1Text";
            this.pk1Text.Size = new System.Drawing.Size(160, 20);
            this.pk1Text.TabIndex = 2;
            this.pk1Text.Text = "---";
            this.pk1Text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pk3Text
            // 
            this.pk3Text.Location = new System.Drawing.Point(82, 203);
            this.pk3Text.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.pk3Text.Name = "pk3Text";
            this.pk3Text.Size = new System.Drawing.Size(160, 20);
            this.pk3Text.TabIndex = 3;
            this.pk3Text.Text = "---";
            this.pk3Text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pk5Text
            // 
            this.pk5Text.Location = new System.Drawing.Point(82, 237);
            this.pk5Text.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.pk5Text.Name = "pk5Text";
            this.pk5Text.Size = new System.Drawing.Size(160, 20);
            this.pk5Text.TabIndex = 4;
            this.pk5Text.Text = "---";
            this.pk5Text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pk2Text
            // 
            this.pk2Text.Location = new System.Drawing.Point(277, 167);
            this.pk2Text.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.pk2Text.Name = "pk2Text";
            this.pk2Text.Size = new System.Drawing.Size(160, 20);
            this.pk2Text.TabIndex = 5;
            this.pk2Text.Text = "---";
            this.pk2Text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pk4Text
            // 
            this.pk4Text.Location = new System.Drawing.Point(277, 203);
            this.pk4Text.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.pk4Text.Name = "pk4Text";
            this.pk4Text.Size = new System.Drawing.Size(160, 20);
            this.pk4Text.TabIndex = 6;
            this.pk4Text.Text = "---";
            this.pk4Text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pk6Text
            // 
            this.pk6Text.Location = new System.Drawing.Point(277, 237);
            this.pk6Text.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.pk6Text.Name = "pk6Text";
            this.pk6Text.Size = new System.Drawing.Size(160, 20);
            this.pk6Text.TabIndex = 7;
            this.pk6Text.Text = "---";
            this.pk6Text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // b2Button
            // 
            this.b2Button.AutoSize = true;
            this.b2Button.Checked = true;
            this.b2Button.Location = new System.Drawing.Point(364, 30);
            this.b2Button.Name = "b2Button";
            this.b2Button.Size = new System.Drawing.Size(71, 24);
            this.b2Button.TabIndex = 8;
            this.b2Button.TabStop = true;
            this.b2Button.Text = "Black 2";
            this.b2Button.UseVisualStyleBackColor = true;
            // 
            // w2Button
            // 
            this.w2Button.AutoSize = true;
            this.w2Button.Location = new System.Drawing.Point(364, 60);
            this.w2Button.Name = "w2Button";
            this.w2Button.Size = new System.Drawing.Size(71, 24);
            this.w2Button.TabIndex = 9;
            this.w2Button.Text = "White 2";
            this.w2Button.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 351);
            this.Controls.Add(this.w2Button);
            this.Controls.Add(this.b2Button);
            this.Controls.Add(this.pk6Text);
            this.Controls.Add(this.pk4Text);
            this.Controls.Add(this.pk2Text);
            this.Controls.Add(this.pk5Text);
            this.Controls.Add(this.pk3Text);
            this.Controls.Add(this.pk1Text);
            this.Controls.Add(this.starterText);
            this.Controls.Add(this.generateButton);
            this.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Random Team Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Label starterText;
        private System.Windows.Forms.Label pk1Text;
        private System.Windows.Forms.Label pk3Text;
        private System.Windows.Forms.Label pk5Text;
        private System.Windows.Forms.Label pk2Text;
        private System.Windows.Forms.Label pk4Text;
        private System.Windows.Forms.Label pk6Text;
        private System.Windows.Forms.RadioButton b2Button;
        private System.Windows.Forms.RadioButton w2Button;
    }
}


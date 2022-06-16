
namespace NewEditor.Forms
{
    partial class TypeSwapEditor
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numCyclesBox = new System.Windows.Forms.NumericUpDown();
            this.cycleButton = new System.Windows.Forms.Button();
            this.normalButton = new System.Windows.Forms.Button();
            this.oppositesButton = new System.Windows.Forms.Button();
            this.randomButton = new System.Windows.Forms.Button();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCyclesBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Controls.Add(this.numCyclesBox);
            this.groupBox1.Controls.Add(this.cycleButton);
            this.groupBox1.Controls.Add(this.normalButton);
            this.groupBox1.Controls.Add(this.oppositesButton);
            this.groupBox1.Controls.Add(this.randomButton);
            this.groupBox1.Location = new System.Drawing.Point(20, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(540, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Presets";
            // 
            // numCyclesBox
            // 
            this.numCyclesBox.Location = new System.Drawing.Point(370, 35);
            this.numCyclesBox.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numCyclesBox.Name = "numCyclesBox";
            this.numCyclesBox.Size = new System.Drawing.Size(40, 22);
            this.numCyclesBox.TabIndex = 5;
            // 
            // cycleButton
            // 
            this.cycleButton.Location = new System.Drawing.Point(420, 30);
            this.cycleButton.Name = "cycleButton";
            this.cycleButton.Size = new System.Drawing.Size(80, 30);
            this.cycleButton.TabIndex = 4;
            this.cycleButton.Text = "Cycle";
            this.cycleButton.UseVisualStyleBackColor = true;
            this.cycleButton.Click += new System.EventHandler(this.CycleElements);
            // 
            // normalButton
            // 
            this.normalButton.Location = new System.Drawing.Point(220, 30);
            this.normalButton.Name = "normalButton";
            this.normalButton.Size = new System.Drawing.Size(80, 30);
            this.normalButton.TabIndex = 2;
            this.normalButton.Text = "Normalize";
            this.normalButton.UseVisualStyleBackColor = true;
            this.normalButton.Click += new System.EventHandler(this.NormalizeElements);
            // 
            // oppositesButton
            // 
            this.oppositesButton.Location = new System.Drawing.Point(120, 30);
            this.oppositesButton.Name = "oppositesButton";
            this.oppositesButton.Size = new System.Drawing.Size(80, 30);
            this.oppositesButton.TabIndex = 1;
            this.oppositesButton.Text = "Opposites";
            this.oppositesButton.UseVisualStyleBackColor = true;
            this.oppositesButton.Click += new System.EventHandler(this.OppositeElements);
            // 
            // randomButton
            // 
            this.randomButton.Location = new System.Drawing.Point(20, 30);
            this.randomButton.Name = "randomButton";
            this.randomButton.Size = new System.Drawing.Size(80, 30);
            this.randomButton.TabIndex = 0;
            this.randomButton.Text = "Random";
            this.randomButton.UseVisualStyleBackColor = true;
            this.randomButton.Click += new System.EventHandler(this.RandomizeElements);
            // 
            // ApplyButton
            // 
            this.ApplyButton.Location = new System.Drawing.Point(460, 520);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(100, 30);
            this.ApplyButton.TabIndex = 6;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyTypeSwap);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(308, 485);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 32);
            this.label1.TabIndex = 7;
            this.label1.Text = "Once applied, changes cannot be undone.\r\nMake sure you back up your files.\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TypeSwapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TypeSwapEditor";
            this.Text = "TypeSwapEditor";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numCyclesBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numCyclesBox;
        private System.Windows.Forms.Button cycleButton;
        private System.Windows.Forms.Button normalButton;
        private System.Windows.Forms.Button oppositesButton;
        private System.Windows.Forms.Button randomButton;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Label label1;
    }
}
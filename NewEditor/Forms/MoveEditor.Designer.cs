
namespace NewEditor.Forms
{
    partial class MoveEditor
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
            this.moveNameDropdown = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // moveNameDropdown
            // 
            this.moveNameDropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.moveNameDropdown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.moveNameDropdown.FormattingEnabled = true;
            this.moveNameDropdown.Location = new System.Drawing.Point(23, 25);
            this.moveNameDropdown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.moveNameDropdown.Name = "moveNameDropdown";
            this.moveNameDropdown.Size = new System.Drawing.Size(166, 24);
            this.moveNameDropdown.TabIndex = 2;
            // 
            // MoveEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 554);
            this.Controls.Add(this.moveNameDropdown);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MoveEditor";
            this.Text = "MoveEditor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox moveNameDropdown;
    }
}
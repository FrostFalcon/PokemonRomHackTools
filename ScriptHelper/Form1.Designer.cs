
namespace ScriptHelper
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
            this.components = new System.ComponentModel.Container();
            this.openScriptButton = new System.Windows.Forms.Button();
            this.saveScriptButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.scriptIDComboBox = new System.Windows.Forms.ComboBox();
            this.addScriptButton = new System.Windows.Forms.Button();
            this.removeScriptButton = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.formatedHexTextBox = new System.Windows.Forms.RichTextBox();
            this.rawHexTextBox = new System.Windows.Forms.RichTextBox();
            this.commandListPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // openScriptButton
            // 
            this.openScriptButton.Location = new System.Drawing.Point(1043, 30);
            this.openScriptButton.Margin = new System.Windows.Forms.Padding(4);
            this.openScriptButton.Name = "openScriptButton";
            this.openScriptButton.Size = new System.Drawing.Size(107, 37);
            this.openScriptButton.TabIndex = 0;
            this.openScriptButton.Text = "Open Script";
            this.openScriptButton.UseVisualStyleBackColor = true;
            this.openScriptButton.Click += new System.EventHandler(this.LoadFile);
            // 
            // saveScriptButton
            // 
            this.saveScriptButton.Location = new System.Drawing.Point(1043, 79);
            this.saveScriptButton.Margin = new System.Windows.Forms.Padding(4);
            this.saveScriptButton.Name = "saveScriptButton";
            this.saveScriptButton.Size = new System.Drawing.Size(107, 37);
            this.saveScriptButton.TabIndex = 1;
            this.saveScriptButton.Text = "Save Script";
            this.saveScriptButton.UseVisualStyleBackColor = true;
            this.saveScriptButton.Click += new System.EventHandler(this.SaveFile);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 103);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Script:";
            // 
            // scriptIDComboBox
            // 
            this.scriptIDComboBox.Enabled = false;
            this.scriptIDComboBox.FormattingEnabled = true;
            this.scriptIDComboBox.Location = new System.Drawing.Point(80, 98);
            this.scriptIDComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.scriptIDComboBox.Name = "scriptIDComboBox";
            this.scriptIDComboBox.Size = new System.Drawing.Size(79, 24);
            this.scriptIDComboBox.TabIndex = 4;
            this.scriptIDComboBox.SelectedIndexChanged += new System.EventHandler(this.ChangeScriptID);
            // 
            // addScriptButton
            // 
            this.addScriptButton.Location = new System.Drawing.Point(30, 200);
            this.addScriptButton.Name = "addScriptButton";
            this.addScriptButton.Size = new System.Drawing.Size(80, 40);
            this.addScriptButton.TabIndex = 6;
            this.addScriptButton.Text = "Add Script";
            this.addScriptButton.UseVisualStyleBackColor = true;
            this.addScriptButton.Click += new System.EventHandler(this.AddScript);
            // 
            // removeScriptButton
            // 
            this.removeScriptButton.Location = new System.Drawing.Point(30, 250);
            this.removeScriptButton.Name = "removeScriptButton";
            this.removeScriptButton.Size = new System.Drawing.Size(80, 40);
            this.removeScriptButton.TabIndex = 7;
            this.removeScriptButton.Text = "Remove Script";
            this.removeScriptButton.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // formatedHexTextBox
            // 
            this.formatedHexTextBox.Location = new System.Drawing.Point(250, 250);
            this.formatedHexTextBox.Name = "formatedHexTextBox";
            this.formatedHexTextBox.ReadOnly = true;
            this.formatedHexTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.formatedHexTextBox.Size = new System.Drawing.Size(250, 200);
            this.formatedHexTextBox.TabIndex = 10;
            this.formatedHexTextBox.Text = "";
            // 
            // rawHexTextBox
            // 
            this.rawHexTextBox.Location = new System.Drawing.Point(250, 120);
            this.rawHexTextBox.Name = "rawHexTextBox";
            this.rawHexTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rawHexTextBox.Size = new System.Drawing.Size(250, 80);
            this.rawHexTextBox.TabIndex = 11;
            this.rawHexTextBox.Text = "";
            this.rawHexTextBox.TextChanged += new System.EventHandler(this.rawHexTextBox_TextChanged);
            // 
            // commandListPanel
            // 
            this.commandListPanel.AutoScroll = true;
            this.commandListPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.commandListPanel.Location = new System.Drawing.Point(550, 200);
            this.commandListPanel.Name = "commandListPanel";
            this.commandListPanel.Size = new System.Drawing.Size(600, 400);
            this.commandListPanel.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 611);
            this.Controls.Add(this.commandListPanel);
            this.Controls.Add(this.rawHexTextBox);
            this.Controls.Add(this.formatedHexTextBox);
            this.Controls.Add(this.removeScriptButton);
            this.Controls.Add(this.addScriptButton);
            this.Controls.Add(this.scriptIDComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveScriptButton);
            this.Controls.Add(this.openScriptButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Script Helper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openScriptButton;
        private System.Windows.Forms.Button saveScriptButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox scriptIDComboBox;
        private System.Windows.Forms.Button addScriptButton;
        private System.Windows.Forms.Button removeScriptButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.RichTextBox formatedHexTextBox;
        private System.Windows.Forms.RichTextBox rawHexTextBox;
        private System.Windows.Forms.Panel commandListPanel;
    }
}


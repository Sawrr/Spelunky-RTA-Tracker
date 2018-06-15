namespace AchievementsTracker
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bgColorDialog = new System.Windows.Forms.ColorDialog();
            this.bgColorText = new System.Windows.Forms.TextBox();
            this.bgColorPicker = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.hotkeyBox = new System.Windows.Forms.TextBox();
            this.textColorPicker = new System.Windows.Forms.Button();
            this.textColorText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textColorDialog = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(151, 308);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Background Color";
            // 
            // bgColorDialog
            // 
            this.bgColorDialog.Color = System.Drawing.Color.White;
            // 
            // bgColorText
            // 
            this.bgColorText.Location = new System.Drawing.Point(45, 78);
            this.bgColorText.Name = "bgColorText";
            this.bgColorText.ReadOnly = true;
            this.bgColorText.Size = new System.Drawing.Size(85, 20);
            this.bgColorText.TabIndex = 2;
            // 
            // bgColorPicker
            // 
            this.bgColorPicker.Location = new System.Drawing.Point(151, 70);
            this.bgColorPicker.Name = "bgColorPicker";
            this.bgColorPicker.Size = new System.Drawing.Size(35, 35);
            this.bgColorPicker.TabIndex = 3;
            this.bgColorPicker.UseVisualStyleBackColor = true;
            this.bgColorPicker.Click += new System.EventHandler(this.bgColorPicker_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(12, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Reset Hotkey";
            // 
            // hotkeyBox
            // 
            this.hotkeyBox.Location = new System.Drawing.Point(45, 247);
            this.hotkeyBox.Name = "hotkeyBox";
            this.hotkeyBox.ReadOnly = true;
            this.hotkeyBox.Size = new System.Drawing.Size(141, 20);
            this.hotkeyBox.TabIndex = 5;
            this.hotkeyBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hotkeyBox_KeyDown);
            // 
            // textColorPicker
            // 
            this.textColorPicker.Location = new System.Drawing.Point(151, 154);
            this.textColorPicker.Name = "textColorPicker";
            this.textColorPicker.Size = new System.Drawing.Size(35, 35);
            this.textColorPicker.TabIndex = 8;
            this.textColorPicker.UseVisualStyleBackColor = true;
            this.textColorPicker.Click += new System.EventHandler(this.textColorPicker_Click);
            // 
            // textColorText
            // 
            this.textColorText.Location = new System.Drawing.Point(45, 162);
            this.textColorText.Name = "textColorText";
            this.textColorText.ReadOnly = true;
            this.textColorText.Size = new System.Drawing.Size(85, 20);
            this.textColorText.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(12, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Text Color";
            // 
            // textColorDialog
            // 
            this.textColorDialog.Color = System.Drawing.Color.White;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(247, 360);
            this.Controls.Add(this.textColorPicker);
            this.Controls.Add(this.textColorText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hotkeyBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bgColorPicker);
            this.Controls.Add(this.bgColorText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColorDialog bgColorDialog;
        private System.Windows.Forms.TextBox bgColorText;
        private System.Windows.Forms.Button bgColorPicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox hotkeyBox;
        private System.Windows.Forms.Button textColorPicker;
        private System.Windows.Forms.TextBox textColorText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColorDialog textColorDialog;
    }
}
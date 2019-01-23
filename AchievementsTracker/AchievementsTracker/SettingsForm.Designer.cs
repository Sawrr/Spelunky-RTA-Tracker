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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.freshSaveBox = new System.Windows.Forms.TextBox();
            this.gameSaveBox = new System.Windows.Forms.TextBox();
            this.freshSaveButton = new System.Windows.Forms.Button();
            this.gameSaveButton = new System.Windows.Forms.Button();
            this.clearFresh = new System.Windows.Forms.Button();
            this.clearGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(190, 479);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(12, 301);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Fresh Save";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.Location = new System.Drawing.Point(12, 394);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Game Save";
            // 
            // freshSaveBox
            // 
            this.freshSaveBox.Enabled = false;
            this.freshSaveBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.freshSaveBox.Location = new System.Drawing.Point(16, 341);
            this.freshSaveBox.Name = "freshSaveBox";
            this.freshSaveBox.ReadOnly = true;
            this.freshSaveBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.freshSaveBox.Size = new System.Drawing.Size(249, 20);
            this.freshSaveBox.TabIndex = 11;
            // 
            // gameSaveBox
            // 
            this.gameSaveBox.Enabled = false;
            this.gameSaveBox.Location = new System.Drawing.Point(16, 432);
            this.gameSaveBox.Name = "gameSaveBox";
            this.gameSaveBox.ReadOnly = true;
            this.gameSaveBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gameSaveBox.Size = new System.Drawing.Size(249, 20);
            this.gameSaveBox.TabIndex = 12;
            // 
            // freshSaveButton
            // 
            this.freshSaveButton.ForeColor = System.Drawing.Color.Black;
            this.freshSaveButton.Location = new System.Drawing.Point(113, 300);
            this.freshSaveButton.Name = "freshSaveButton";
            this.freshSaveButton.Size = new System.Drawing.Size(73, 24);
            this.freshSaveButton.TabIndex = 14;
            this.freshSaveButton.Text = "Browse...";
            this.freshSaveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.freshSaveButton.UseVisualStyleBackColor = true;
            this.freshSaveButton.Click += new System.EventHandler(this.freshSaveButton_Click);
            // 
            // gameSaveButton
            // 
            this.gameSaveButton.ForeColor = System.Drawing.Color.Black;
            this.gameSaveButton.Location = new System.Drawing.Point(113, 393);
            this.gameSaveButton.Name = "gameSaveButton";
            this.gameSaveButton.Size = new System.Drawing.Size(73, 24);
            this.gameSaveButton.TabIndex = 15;
            this.gameSaveButton.Text = "Browse...";
            this.gameSaveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.gameSaveButton.UseVisualStyleBackColor = true;
            this.gameSaveButton.Click += new System.EventHandler(this.gameSaveButton_Click);
            // 
            // clearFresh
            // 
            this.clearFresh.ForeColor = System.Drawing.Color.Black;
            this.clearFresh.Location = new System.Drawing.Point(192, 301);
            this.clearFresh.Name = "clearFresh";
            this.clearFresh.Size = new System.Drawing.Size(61, 24);
            this.clearFresh.TabIndex = 16;
            this.clearFresh.Text = "Clear";
            this.clearFresh.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.clearFresh.UseVisualStyleBackColor = true;
            this.clearFresh.Click += new System.EventHandler(this.clearFresh_Click);
            // 
            // clearGame
            // 
            this.clearGame.ForeColor = System.Drawing.Color.Black;
            this.clearGame.Location = new System.Drawing.Point(192, 394);
            this.clearGame.Name = "clearGame";
            this.clearGame.Size = new System.Drawing.Size(61, 24);
            this.clearGame.TabIndex = 17;
            this.clearGame.Text = "Clear";
            this.clearGame.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.clearGame.UseVisualStyleBackColor = true;
            this.clearGame.Click += new System.EventHandler(this.clearGame_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(277, 523);
            this.Controls.Add(this.clearGame);
            this.Controls.Add(this.clearFresh);
            this.Controls.Add(this.gameSaveButton);
            this.Controls.Add(this.freshSaveButton);
            this.Controls.Add(this.gameSaveBox);
            this.Controls.Add(this.freshSaveBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
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
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColorDialog bgColorDialog;
        private System.Windows.Forms.Button bgColorPicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button textColorPicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColorDialog textColorDialog;
        public System.Windows.Forms.TextBox hotkeyBox;
        public System.Windows.Forms.TextBox textColorText;
        public System.Windows.Forms.TextBox bgColorText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox freshSaveBox;
        public System.Windows.Forms.TextBox gameSaveBox;
        private System.Windows.Forms.Button freshSaveButton;
        private System.Windows.Forms.Button gameSaveButton;
        private System.Windows.Forms.Button clearFresh;
        private System.Windows.Forms.Button clearGame;
    }
}
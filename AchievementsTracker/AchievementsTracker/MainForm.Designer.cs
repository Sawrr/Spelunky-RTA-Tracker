namespace AchievementsTracker
{
    partial class MainForm
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
            this.runningLabel = new System.Windows.Forms.Label();
            this.damselCount = new System.Windows.Forms.Label();
            this.shoppieCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // runningLabel
            // 
            this.runningLabel.AutoSize = true;
            this.runningLabel.Location = new System.Drawing.Point(86, 31);
            this.runningLabel.Name = "runningLabel";
            this.runningLabel.Size = new System.Drawing.Size(0, 13);
            this.runningLabel.TabIndex = 0;
            // 
            // damselCount
            // 
            this.damselCount.AutoSize = true;
            this.damselCount.Location = new System.Drawing.Point(406, 186);
            this.damselCount.Name = "damselCount";
            this.damselCount.Size = new System.Drawing.Size(13, 13);
            this.damselCount.TabIndex = 1;
            this.damselCount.Text = "0";
            // 
            // shoppieCount
            // 
            this.shoppieCount.AutoSize = true;
            this.shoppieCount.Location = new System.Drawing.Point(406, 226);
            this.shoppieCount.Name = "shoppieCount";
            this.shoppieCount.Size = new System.Drawing.Size(13, 13);
            this.shoppieCount.TabIndex = 2;
            this.shoppieCount.Text = "0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.shoppieCount);
            this.Controls.Add(this.damselCount);
            this.Controls.Add(this.runningLabel);
            this.Name = "MainForm";
            this.Text = "Achievements Tracker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label runningLabel;
        private System.Windows.Forms.Label damselCount;
        private System.Windows.Forms.Label shoppieCount;
    }
}


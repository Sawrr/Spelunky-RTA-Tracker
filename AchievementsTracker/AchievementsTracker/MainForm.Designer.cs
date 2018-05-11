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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // runningLabel
            // 
            this.runningLabel.AutoSize = true;
            this.runningLabel.Location = new System.Drawing.Point(99, 36);
            this.runningLabel.Name = "runningLabel";
            this.runningLabel.Size = new System.Drawing.Size(0, 15);
            this.runningLabel.TabIndex = 0;
            // 
            // damselCount
            // 
            this.damselCount.AutoSize = true;
            this.damselCount.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.damselCount.Location = new System.Drawing.Point(256, 127);
            this.damselCount.Name = "damselCount";
            this.damselCount.Size = new System.Drawing.Size(19, 20);
            this.damselCount.TabIndex = 1;
            this.damselCount.Text = "0";
            // 
            // shoppieCount
            // 
            this.shoppieCount.AutoSize = true;
            this.shoppieCount.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shoppieCount.Location = new System.Drawing.Point(256, 199);
            this.shoppieCount.Name = "shoppieCount";
            this.shoppieCount.Size = new System.Drawing.Size(19, 20);
            this.shoppieCount.TabIndex = 2;
            this.shoppieCount.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Casanova";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(48, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Public Enemy";
            // 
            // timer
            // 
            this.timer.AutoSize = true;
            this.timer.Font = new System.Drawing.Font("MS Reference Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timer.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.timer.Location = new System.Drawing.Point(2, 391);
            this.timer.Name = "timer";
            this.timer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.timer.Size = new System.Drawing.Size(337, 67);
            this.timer.TabIndex = 6;
            this.timer.Text = "         0.00";
            this.timer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(342, 482);
            this.Controls.Add(this.timer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shoppieCount);
            this.Controls.Add(this.damselCount);
            this.Controls.Add(this.runningLabel);
            this.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Achievements Tracker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label runningLabel;
        private System.Windows.Forms.Label damselCount;
        private System.Windows.Forms.Label shoppieCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label timer;
    }
}


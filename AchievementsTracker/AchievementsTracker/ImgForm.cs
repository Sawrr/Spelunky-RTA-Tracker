using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AchievementsTracker
{
    public partial class ImgForm : Form
    {

        int imageSize = 40;
        int rows = 8;
        bool inverted = false;
        int[] CHAR_ORDER = { 2, 6, 7, 4, 9, 12, 11, 5, 1, 8, 14, 10, 13, 15, 3, 0 };

        public ImgForm()
        {
            InitializeComponent();

            Reset();
        }

        public int GetImageSize()
        {
            return imageSize;
        }

        public int GetRows()
        {
            return rows;
        }

        public bool GetInverted()
        {
            return inverted;
        }

        public void SetImageSize(int size)
        {
            imageSize = size;
        }

        public void SetRows(int r)
        {
            rows = r;
        }

        public void SetInverted(bool inv)
        {
            inverted = inv;
        }

        public void Reset()
        {
            ArrangeUnlockables();

            foreach (Control c in Controls)
            {
                c.Show();
            }
        }

        public void ArrangeUnlockables()
        {
            for (int i = 0; i < 120; i++)
            {
                // Get the right image box
                Control picBox;
                if (i < 16)
                {
                    picBox = Controls.Find("c" + CHAR_ORDER[i], false)[0];
                }
                else if (i >= 16 && i < 72)
                {
                    picBox = Controls.Find("m" + (i - 16), false)[0];
                }
                else if (i >= 72 && i < 106)
                {
                    picBox = Controls.Find("i" + (i - 72), false)[0];
                }
                else
                {
                    picBox = Controls.Find("t" + (i - 106), false)[0];
                }

                // Position it accordingly
                int xIdx;
                int yIdx;
                if (!inverted)
                {
                    xIdx = (i / rows);
                    yIdx = (i % rows);
                } else
                {
                    xIdx = (i % rows);
                    yIdx = (i / rows);
                }
                int x = imageSize * xIdx;
                int y = imageSize * yIdx;
                picBox.Location = new Point(x, y);
                picBox.Size = new Size(imageSize, imageSize);
            }

            if (inverted)
            {
                ClientSize = new Size(imageSize * rows, imageSize * (120 / rows));
            } else
            {
                ClientSize = new Size(imageSize * (120 / rows), imageSize * rows);
            }
        }

        public void UpdateCharacters(byte[] chars)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateCharacters(chars)));
                return;
            }
            
            // Hide any characters that are unlocked
            for (int i = 0; i < 16; i++)
            {
                if (chars[4 * i] == 1)
                {
                    string controlName = "c" + i;
                    Control picBox = Controls.Find(controlName, false)[0];
                    picBox.Hide();
                }
            }
        }

        public void UpdateMonsters(byte[] mons)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateMonsters(mons)));
                return;
            }

            // Hide any monsters that are unlocked
            for (int i = 0; i < 56; i++)
            {
                if (mons[4 * i] == 1)
                {
                    string controlName = "m" + i;
                    Control picBox = Controls.Find(controlName, false)[0];
                    picBox.Hide();
                }
            }
        }

        public void UpdateItems(byte[] items)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateItems(items)));
                return;
            }

            // Hide any items that are unlocked
            for (int i = 0; i < 34; i++)
            {
                if (items[4 * i] == 1)
                {
                    string controlName = "i" + i;
                    Control picBox = Controls.Find(controlName, false)[0];
                    picBox.Hide();
                }
            }
        }

        public void UpdateTraps(byte[] traps)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateTraps(traps)));
                return;
            }

            // Hide any traps that are unlocked
            for (int i = 0; i < 14; i++)
            {
                if (traps[4 * i] == 1)
                {
                    string controlName = "t" + i;
                    Control picBox = Controls.Find(controlName, false)[0];
                    picBox.Hide();
                }
            }
        }

        // X button hides instead of closing
        // Credit: https://stackoverflow.com/questions/2021681/hide-form-instead-of-closing-when-close-button-clicked
        private void ImgForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}

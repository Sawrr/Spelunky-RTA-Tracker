using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace AchievementsTracker
{
    public partial class ImgForm : Form
    {

        int imageSize = 40;
        int rows = 8;
        bool inverted = false;
        bool groupByArea = false;
        Category category = Category.AA;

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

        public void changeCategory(Category cat)
        {
            category = cat;
        }

        public void SetGroupByArea(bool param)
        {
            groupByArea = param;
        }

        public void Reset()
        {
            foreach (Control c in Controls)
            {
                c.Show();
            }
            ArrangeUnlockables();

        }

        public void ArrangeUnlockables()
        {
            Debug.WriteLine("Category: " + category.ToString());

  
            int pos = 0;

            var imgOrder = ImgOrder.DEFAULT;
            if (groupByArea)
            {
                imgOrder = ImgOrder.BY_AREA;
            }

            foreach ((EntryType entryType, string name) in imgOrder)
            {
                // Get the right image box
                Control picBox;
                
                if (entryType == EntryType.Character)
                {

                    picBox = Controls.Find(name, false)[0];
                    if (category != Category.AA && category != Category.AC)
                    {
                        picBox.Hide();
                        continue;
                    }
                    picBox.Show();
                }
                else if (entryType == EntryType.Monster)
                {
                    picBox = Controls.Find(name, false)[0];
                    if (category != Category.AA && category != Category.AJE)
                    {
                        picBox.Hide();
                        continue;
                    }
                    picBox.Show();
                }
                else if (entryType == EntryType.Item)
                {
                    picBox = Controls.Find(name, false)[0];
                    if (category != Category.AA && category != Category.AJE)
                    {
                        picBox.Hide();
                        continue;
                    }
                    picBox.Show();
                }
                else
                {
                    picBox = Controls.Find(name, false)[0];
                    if (category != Category.AA && category != Category.AJE)
                    {
                        picBox.Hide();
                        continue;
                    }
                    picBox.Show();
                }


                // Position it accordingly
                int xIdx;
                int yIdx;
                if (!inverted)
                {
                    xIdx = (pos / rows);
                    yIdx = (pos % rows);
                } else
                {
                    xIdx = (pos % rows);
                    yIdx = (pos / rows);
                }
                int x = imageSize * xIdx;
                int y = imageSize * yIdx;
                picBox.Location = new Point(x, y);
                picBox.Size = new Size(imageSize, imageSize);

                pos++;
            }

            // Handle remainder
            int remainder = 0;
            if ((pos-1) % rows != 0)
            {
                remainder = 1;
            }

            if (inverted)
            {
                ClientSize = new Size(imageSize * rows, imageSize * ((pos-1) / rows + remainder));
            } else
            {
                ClientSize = new Size(imageSize * ((pos-1) / rows + remainder), imageSize * rows);
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

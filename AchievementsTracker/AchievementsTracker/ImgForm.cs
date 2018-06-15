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
        public ImgForm()
        {
            InitializeComponent();

            Reset();
        }

        public void Reset()
        {
            foreach(Control c in Controls)
            {
                c.Show();
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
    }
}

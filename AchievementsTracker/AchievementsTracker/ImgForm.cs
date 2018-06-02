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
        }

        public void UpdateCharacters(byte[] chars)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateCharacters(chars)));
                return;
            }
            
            // TODO
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AchievementsTracker.Program;

namespace AchievementsTracker
{
    public partial class SettingsForm : Form
    {
        TrayApplicationContext context;
        MainForm form;

        Keys hotkey;
        int modifiers;

        public SettingsForm(TrayApplicationContext context, MainForm form)
        {
            this.context = context;
            this.form = form;

            InitializeComponent();

            bgColorText.Text = HexConverter(BackColor);
            textColorText.Text = HexConverter(ForeColor);

            bgColorDialog.Color = BackColor;
            textColorDialog.Color = ForeColor;
        }

        private void hotkeyBox_KeyDown(object sender, KeyEventArgs e)
        {
            hotkey = e.KeyCode;
            modifiers = 0;
            if (e.Alt) modifiers += 1;
            if (e.Control) modifiers += 2;
            if (e.Shift) modifiers += 4;

            if (e.Modifiers == Keys.None)
            {
                hotkeyBox.Text = e.KeyCode.ToString();
            }
            else
            {
                string mods = e.Modifiers.ToString();
                string key = e.KeyCode.ToString();
                if (isKeyModifier(e.KeyCode))
                {
                    hotkeyBox.Text = "";
                }
                else
                {
                    hotkeyBox.Text = mods + " + " + key;
                }
            }
        }

        private bool isKeyModifier(Keys k)
        {
            return k == Keys.ControlKey || k == Keys.ShiftKey || k == Keys.Alt || k == Keys.Menu;
        }

        void Save()
        {
            form.SetResetHotKey(modifiers, hotkey);
            context.SetBackgroundColor(bgColorDialog.Color);
            context.SetTextColor(textColorDialog.Color);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Save();
            Hide();
        }

        private void bgColorPicker_Click(object sender, EventArgs e)
        {
            DialogResult result = bgColorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                bgColorText.Text = HexConverter(bgColorDialog.Color);
                bgColorPicker.BackColor = bgColorDialog.Color;
            }
        }

        private void textColorPicker_Click(object sender, EventArgs e)
        {
            DialogResult result = textColorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                textColorText.Text = HexConverter(textColorDialog.Color);
                textColorPicker.BackColor = textColorDialog.Color;
            }
        }

        // Credit: https://stackoverflow.com/questions/2395438/convert-system-drawing-color-to-rgb-and-hex-value
        private static String HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        // X button hides instead of closing
        // Credit: https://stackoverflow.com/questions/2021681/hide-form-instead-of-closing-when-close-button-clicked
        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("YEAH");
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}

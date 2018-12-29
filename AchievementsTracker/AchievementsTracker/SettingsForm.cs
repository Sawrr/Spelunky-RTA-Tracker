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
        }

        public void SetBackgroundColor(Color color)
        {
            bgColorText.Text = HexConverter(color);
            bgColorDialog.Color = color;
        }

        public void SetTextColor(Color color)
        {
            textColorText.Text = HexConverter(color);
            textColorDialog.Color = color;
        }

        public void SetHotkey(int mods, Keys key)
        {
            modifiers = mods;
            hotkey = key;

            if (mods == 0)
            {
                hotkeyBox.Text = key.ToString();
            }
            else
            {
                string modsStr = "";
                if (mods / 4 == 1)
                    modsStr += "Shift + ";
                if ((mods % 4) / 2 == 1)
                    modsStr += "Ctrl + ";
                if ((mods % 4) % 2 == 1)
                    modsStr += "Alt + ";
                hotkeyBox.Text = modsStr + key.ToString();
            }
        }

        private void hotkeyBox_KeyDown(object sender, KeyEventArgs e)
        {
            hotkey = e.KeyCode;
            modifiers = 0;
            if (e.Alt) modifiers += 1;
            if (e.Control) modifiers += 2;
            if (e.Shift) modifiers += 4;

            // Disallow control, shift, alt, and menu
            if (isKeyModifier(hotkey))
            {
                modifiers = 0;
                hotkey = Keys.None;
            }

            SetHotkey(modifiers, hotkey);
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

            context.SaveSettings(bgColorDialog.Color, textColorDialog.Color, hotkey, modifiers);
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
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}

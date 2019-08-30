using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ScreenRecoder.App
{
    [DefaultEvent("KeysChanged")]
    public class HotKeySelecter : TextBox
    {
        private List<Keys> oldKeys = new List<Keys>();
        private List<Keys> newKeys = new List<Keys>();
        private List<Keys> downedKeys = new List<Keys>();
        private bool isNew = false;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                oldKeys.Clear();
                newKeys.Clear();
                downedKeys.Clear();
                UpdateKeyDisplay();
            }
            else
            {
                if (isNew)
                {
                    oldKeys.Clear();
                    foreach (Keys k in downedKeys)
                        oldKeys.Add(k);

                    downedKeys.Clear();
                    isNew = false;
                }
                if (!downedKeys.Contains(e.KeyCode) && downedKeys.Count < 3)
                {
                    newKeys.Add(e.KeyCode);
                    downedKeys.Add(e.KeyCode);
                    UpdateKeyDisplay();
                }
            }
            base.OnKeyDown(e);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }
        protected override void OnGotFocus(EventArgs e)
        {
            newKeys.Clear();
            base.OnGotFocus(e);
            isNew = true;
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (newKeys.Count == 0)
            {
                foreach (Keys k in oldKeys)
                    downedKeys.Add(k);
                UpdateKeyDisplay();
            }
            else KeysChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler KeysChanged;

        public static bool IsEmepty(Keys[] keys)
        {
            int kc = 0;
            for (int i = 0; i < keys.Length; i++)
                if (keys[i] != Keys.None)
                    kc++;
            return kc == 0;
        }
        public static string KeyDisplay(Keys[] keys)
        {
            string s = "";
            for (int i = 0; i < keys.Length; i++)
            {
                if (i > 0) s += " + " + KeyDisplay(keys[i]);
                else s += KeyDisplay(keys[i]);
            }
            return s;
        }
        public bool IsEmepty()
        {
            return downedKeys.Count == 0;
        }
        public void SetKeys(Keys[] keys)
        {
            downedKeys.Clear();
            for (int i = 0; i < keys.Length; i++)
                if (keys[i] != Keys.None)
                    downedKeys.Add(keys[i]);
            UpdateKeyDisplay();
        }
        public void ClearKeys()
        {
            downedKeys.Clear();
            UpdateKeyDisplay();
        }
        public Keys[] GetKeys()
        {
            return downedKeys.ToArray();
        }
        public void GetKeys(Keys[] keys)
        {
            downedKeys.CopyTo(keys);
        }
        public static Keys ModKeyRealloc(Keys k)
        {
            if (k == Keys.Control || k == Keys.ControlKey || k == Keys.RControlKey)
                return Keys.Control;
            if (k == Keys.Alt || k == Keys.Menu || k == Keys.LMenu || k == Keys.RMenu)
                return Keys.Alt;
            if (k == Keys.LShiftKey || k == Keys.RShiftKey || k == Keys.ShiftKey || k == Keys.Shift)
                return Keys.Shift;
            return k;
        }
        public static string KeyDisplay(Keys k)
        {
            if (k == Keys.Control || k == Keys.ControlKey || k == Keys.RControlKey)
                return "Ctrl";
            if (k == Keys.Alt || k == Keys.Menu || k == Keys.LMenu || k == Keys.RMenu)
                return "Alt";
            if (k == Keys.LShiftKey || k == Keys.RShiftKey || k == Keys.ShiftKey)
                return "Shift";
            return k.ToString();
        }

        private void UpdateKeyDisplay()
        {
            string s = "";
            for (int i = 0; i < downedKeys.Count; i++)
            {
                if (i > 0) s += " + " + KeyDisplay(downedKeys[i]);
                else s += KeyDisplay(downedKeys[i]);
            }
            Text = s;
        }
    }
}

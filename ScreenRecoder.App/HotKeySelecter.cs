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
            if (isNew)
            {
                oldKeys.Clear();
                foreach (Keys k in downedKeys)
                    oldKeys.Add(k);

                downedKeys.Clear();
                isNew = false;
            }
            if (!downedKeys.Contains(e.KeyCode))
            {
                newKeys.Add(e.KeyCode);
                downedKeys.Add(e.KeyCode);
                UpdateKeyDisplay();
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
        public void GetKeys(Keys[]keys)
        {
            downedKeys.CopyTo(keys);
        }
        private void UpdateKeyDisplay()
        {
            string s = "";
            for (int i = 0; i < downedKeys.Count; i++)
            {
                if (i > 0) s += " + " + downedKeys[i].ToString();
                else s += downedKeys[i].ToString();
            }
            Text = s;
        }
    }
}

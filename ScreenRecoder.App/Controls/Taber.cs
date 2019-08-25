using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenRecoder.App.Controls
{
    class Taber
    {
        public Taber()
        {
            tabs = new List<TabStg>();
        }

        private class TabStg
        {
            public Control Tab;
            public TabButton Button;
        }
        private List<TabStg> tabs;

        public void AddTab(Control tabCotrol, TabButton tabButton)
        {
            TabStg ts =null;
            if (!HasTab(tabCotrol, out ts))
            {
                ts = new TabStg();
                ts.Tab = tabCotrol;
                ts.Button = tabButton;
                ts.Button.Click += (object o, EventArgs e) =>  { SwitchTab(ts.Tab); };
                tabs.Add(ts);
            }
        }
        private bool HasTab(Control tabCotrol, out TabStg ts)
        {
            foreach (TabStg t in tabs)
            {
                if (t.Tab == tabCotrol)
                {
                    ts = t;
                    return true;
                }
            }
            ts = null;
            return false;
        }
        public void RemoveTab(Control tabCotrol)
        {
            TabStg target = null;
            if (HasTab(tabCotrol, out target))
            {
                tabs.Remove(target);
            }
        }
        public void SwitchTab(Control tabCotrol)
        {
            foreach (TabStg t in tabs)
            {
                if (t.Tab == tabCotrol)
                {
                    if(!t.Button.Active) t.Button.SetActive(true);
                    if (!t.Tab.Visible) t.Tab.Show();
                }
                else
                {
                    if (t.Button.Active) t.Button.SetActive(false);
                    if (t.Tab.Visible) t.Tab.Hide();
                }
            }
        }
    }
}

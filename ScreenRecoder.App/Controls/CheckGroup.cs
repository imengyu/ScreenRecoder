using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenRecoder.App.Controls
{
    class CheckGroup
    {
        private List<TabButton> tabs = new List<TabButton>();

        public void Add(TabButton b)
        {
            tabs.Add(b);
        }
        public void Remove(TabButton b)
        {
            tabs.Remove(b);
        }
        public void Check(TabButton b)
        {
            foreach(TabButton t in tabs)
            {
                if(t == b)
                {
                    if(!t.Active)
                    {
                        t.Active = true;
                        t.Invalidate();
                    }
                }
                else
                {
                    if (t.Active)
                    {
                        t.Active = false;
                        t.Invalidate();
                    }
                }
            }
        }
        public void UnCheck(TabButton t)
        {
            if (t.Active)
            {
                t.Active = false;
                t.Invalidate();
            }
        }
        public bool Checked(TabButton b)
        {
            foreach (TabButton t in tabs)
            {
                if (t == b)
                {
                    return t.Active;
                }
            }
            return false;
        }
    }
}

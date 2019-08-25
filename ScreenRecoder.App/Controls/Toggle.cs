using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenRecoder.App.Controls
{
    [DefaultEvent("CheckedChanged")]
    public partial class Toggle : UserControl
    {
        public Toggle()
        {
            InitializeComponent();
        }

        private bool _Checked = false;

        public event EventHandler CheckedChanged;

        public bool Checked
        {
            
            get { return _Checked; }
            set
            {
                _Checked = value;
                imageButtonChecked.Visible = _Checked;
                imageButtonUnChecked.Visible = !_Checked;
                CheckedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void imageButtonUnChecked_Click(object sender, EventArgs e)
        {
            Checked = true;
        }
        private void imageButtonChecked_Click(object sender, EventArgs e)
        {
            Checked = false;
        }
        private void Toggle_Click(object sender, EventArgs e)
        {
            Checked = !Checked;
        }
    }
}

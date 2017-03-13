using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCBS.GUI.UserControl
{
    public partial class ObservedForm : Form
    {
        public event EventHandler UserActivity;

        public ObservedForm()
        {
            InitializeComponent();

            /*KeyPreview = true;

            FormClosed += ObservedForm_FormClosed;
            MouseMove += ObservedForm_MouseMove;
            KeyDown += ObservedForm_KeyDown;*/
        }

        protected virtual void OnUserActivity(EventArgs e)
        {
            UserActivity?.Invoke(this, e);
        }

        private void ObservedForm_MouseMove(object sender, MouseEventArgs e)
        {
            OnUserActivity(e);
        }

        private void ObservedForm_KeyDown(object sender, KeyEventArgs e)
        {
            OnUserActivity(e);
        }

        private void ObservedForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormClosed -= ObservedForm_FormClosed;
            MouseMove -= ObservedForm_MouseMove;
            KeyDown -= ObservedForm_KeyDown;
        }
    }
}

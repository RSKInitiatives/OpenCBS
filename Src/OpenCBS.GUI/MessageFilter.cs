using OpenCBS.GUI.UserControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCBS.GUI
{
    /*public class MessageFilter : IMessageFilter
    {
        private int WM_LBUTTONDOWN = 0x0201;
        private int WM_KEYDOWN = 0x0100;
        private int WM_RBUTTONDOWN = 0x0204;
        private int WM_MBUTTONDOWN = 0x0207;
        private int WM_MOUSEWHEEL = 0x020A;
        private int WM_MOUSEMOVE = 0x0200;

        private SweetBaseForm FParent;

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEMOVE || m.Msg == WM_KEYDOWN || m.Msg == WM_LBUTTONDOWN || m.Msg == WM_MOUSEWHEEL || m.Msg == WM_RBUTTONDOWN || m.Msg == WM_MBUTTONDOWN)
            {
                //Reset the timer of form1
                FParent.timerIdle.Stop();
                FParent.timerIdle.Start();
            }
            return false;
        }
    }
    */

    public class MessageFilter : IMessageFilter
    {
        private SweetBaseForm FParent;

        public MessageFilter(SweetBaseForm RefParent)
        {
            FParent = RefParent;
        }

        public bool PreFilterMessage(ref Message m)
        {
            bool ret = true;

            //Check for mouse movements and / or clicks
            bool mouse = (m.Msg >= 0x200 & m.Msg <= 0x20d) | (m.Msg >= 0xa0 & m.Msg <= 0xad);

            //Check for keyboard button presses
            bool kbd = (m.Msg >= 0x100 & m.Msg <= 0x109);

            //if any of these events occur
            if (mouse | kbd)
            {
                FParent.OnUserActivity(new EventArgs() {  });
                ret = false;
            }
            else
            {
                ret = false;
            }

            return ret;

        }

    }
}

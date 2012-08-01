using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeMachine
{
    public partial class TimeMachineControl : UserControl
    {
        protected Database.Connection db = null;
        public bool isBlueScreen = false;


        public static UInt64 id0(Object o)
        {
            UInt64 ret = 0;
            try
            {
                ret = Convert.ToUInt64(o);
            }
            catch (FormatException)
            {
            }
            catch (InvalidCastException)
            {
            }
            catch (OverflowException)
            {
            }

            return ret;
        }


        public TimeMachineControl()
        {
            InitializeComponent();
        }

        public void setConnection(Database.Connection c)
        {
            db = c;
        }

        public virtual void onAppear()
        {
        }

        public virtual void onDisappear()
        {
        }

        public virtual void update()
        {
        }

        private void TimeMachineControl_Resize(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BarCode
{
    public class ReaderControl
    {
        public BarCode BarCodeObject = new BarCode();
        private Thread thread = null;

        public ReaderControl()
        {
        }

        ~ReaderControl()
        {
            Stop();
        }

        public void Start()
        {
            if (thread != null) return;
            thread = new Thread(new ThreadStart(BarCodeObject.Start));
            thread.Start();
            Thread.Sleep(100);
        }

        public void Stop()
        {
            if (thread == null) return;
            BarCodeObject.Continue = false;
            thread.Join();
            thread = null;
        }

        public bool IsStarted()
        {
            return thread.IsAlive;
        }

        public void Reload()
        {
            Stop();
            Start();
        }
    }
}

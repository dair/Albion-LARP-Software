/* ***********************************************************************
 * (C) 2008-2012 Vladimir Lebedev-Schmidthof <vladimir@schmidthof.com>
 * Made for Albion Games (http://albiongames.org)
 * 
 * 
 *            DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
 *                    Version 2, December 2004

 * Copyright (C) 2004 Sam Hocevar <sam@hocevar.net>
 * 
 * Everyone is permitted to copy and distribute verbatim or modified
 * copies of this license document, and changing it is allowed as long
 * as the name is changed.

 *           DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
 *   TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION

 *  0. You just DO WHAT THE FUCK YOU WANT TO.
 * *********************************************************************** */

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
            lock (BarCodeObject)
            {
                BarCodeObject.Continue = false;
            }
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

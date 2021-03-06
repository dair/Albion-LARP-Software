﻿/* ***********************************************************************
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VKTest
{
    public class VerticalProgressBar : ProgressBar
    {
        protected static Random random = new Random();

        protected abstract class Function
        {
            public abstract bool HasNextValue();
            public abstract int NextValue();
            public abstract Function NextFunction();
        };

        protected class Line : Function
        {
            int Value;
            public Line(int val)
            {
                Value = val;
            }

            public override bool HasNextValue()
            {
                return true;
            }

            public override int NextValue()
            {
                return Value;
            }

            public override Function NextFunction()
            {
                return null;
            }
        }

        protected class Sinus: Function
        {
            int Value;
            int Amplitude;
            int x;

            public Sinus(int val, int amp)
            {
                Value = val;
                Amplitude = amp;
                x = 0;
            }

            public override bool HasNextValue()
            {
                return x < 10;
            }

            public override int NextValue()
            {
                int ret = Value + Convert.ToInt16(Amplitude * Math.Sin(Math.PI * x / 10));
                x++;
                return ret;
            }

            public override Function NextFunction()
            {
                int mlt = Math.Sign(Amplitude);
                int newAmp = (random.Next(6) + 2) * (-mlt);
                return new Sinus(Value, newAmp);
            }
        }

        protected class Moving : Function
        {
            double Start, Finish;
            int x = 0;
            Sinus sinus = null;

            public Moving(int start, int finish)
            {
                Start = start;
                Finish = finish;
                int mlt = Math.Sign(start - finish);
                sinus = new Sinus(0, (random.Next(6) + 2) * mlt);
            }

            public override bool HasNextValue()
            {
                return x < 10;
            }

            public override int NextValue()
            {
                double val = Start + (Finish - Start) * x / 10.0 + sinus.NextValue();
                x++;
                return Convert.ToInt16(val);
            }

            public override Function NextFunction()
            {
                int mlg = Math.Sign(Finish - Start);
                return new Sinus(Convert.ToInt16(Finish), mlg * random.Next(6) + 2);
            }
        }

        Timer timer = new Timer();
        bool shaking = false;
        private int realValue = 0;
        Function function = null;

        public bool Shaking
        {
            set
            {
                if (value != shaking)
                {
                    shaking = value;
                    timer.Enabled = shaking;
                }
            }

            get
            {
                return shaking;
            }
        }


        public VerticalProgressBar()
        {
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 100;
            shaking = false;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (function == null)
            {
                function = new Sinus(realValue, (random.Next(6) + 2));
            }

            if (function.HasNextValue())
            {
                int v = function.NextValue();
                System.Console.WriteLine("Value: " + Convert.ToString(v) + ", realValue = " + Convert.ToString(realValue));
                if (v < Minimum)
                    v = Minimum;
                if (v > Maximum)
                    v = Maximum;
                Value = v;
            }
            else
            {
                function = function.NextFunction();
                timer_Tick(sender, e);
            }
        }

        public int RealValue
        {
            set
            {
                function = new Moving(realValue, value);
                realValue = value;
            }

            get
            {
                return realValue;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x04;
                return cp;
            }
        }


    }
}

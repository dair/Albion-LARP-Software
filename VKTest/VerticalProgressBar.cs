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
                int newAmp = (random.Next(4) + 2) * mlt;
                return new Sinus(Value, newAmp);
            }
        }

        protected class Moving : Function
        {
            double Start, Finish;
            int x = 0;

            public Moving(int start, int finish)
            {
                Start = start;
                Finish = finish;
            }

            public override bool HasNextValue()
            {
                return x < 15;
            }

            public override int NextValue()
            {
                double val = Start + (Finish - Start) * x / 15.0;
                x++;
                return Convert.ToInt16(val);
            }

            public override Function NextFunction()
            {
                int mlg = Math.Sign(Finish - Start);
                return new Sinus(Convert.ToInt16(Finish), mlg * random.Next(4) + 2);
            }
        }

        Timer timer = new Timer();
        bool shaking = false;
        Function function = null;

        public bool Shaking
        {
            set
            {
                shaking = value;
                timer.Enabled = shaking;
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
                function = new Sinus(realValue, 5);
            }

            if (function.HasNextValue())
            {
                Value = function.NextValue();
            }
            else
            {
                function = function.NextFunction();
                timer_Tick(sender, e);
            }
        }

        public int realValue;

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

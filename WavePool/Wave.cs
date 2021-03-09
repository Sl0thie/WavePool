using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavePool
{
    public class Wave
    {
        public double H;//Wave Height.
        public double T;//Wave Period.
        public double d;//Depth to bottom.

        public double L;//Wave Length.
        public double L2;// double Wave Length.
        public double halfH;//Half of the wave height.
        public double k;//wave number.
        public double c;//wave celerity (speed).
        public double f;//Wave frequency.
        public double offset;// distance to offset along fetch.

        public int framesTotal;
        public double frameSpace;
        public double frameSpeed;
        public double milliSpeed;
        public float[] data;

        public double Reflection;
        public bool Reflected = false;
        public bool Reversed = false;
        public int Type;

        public bool Selected = false;

        private int _Milli;
        public int Milli
        {
            get { return _Milli; }
            set { _Milli = value; }
        }

        public int reflectMilli;

        public Wave(double H, double T, double d, bool Reversed, double Reflection, int Type, int Milli, bool Selected)
        {
            _Milli = Milli + (int)(T * 1000);

            this.H = H;
            this.T = T;
            this.d = d;
            this.Reversed = Reversed;
            this.Reflection = Reflection;
            this.Type = Type;

            if (Reversed)
            {
                CalculateReverse();
            }
            else
            {
                Calculate();
            }

            this.Selected = Selected;

            Debug.WriteLine("New Wave: T:" + T + " d:" + d + " L:" + L  + " c:" + c + " k:" + k + " f:" + f + " reversed:" + Reversed + " reflection:" + Reflection + " Data:" + data.Length + " FrameSpeed:" + frameSpeed + " FrameSpace:" + frameSpace + " FramesTotal:" + framesTotal);
            Debug.WriteLine("Milli:" + Milli + " MilliSpeed:" + milliSpeed + " offset:" + offset);
        }

        private void Calculate()
        {
            L = Op.WaveLengthExact(T, d);
            offset = -L;
            halfH = H / 2f;
            k = Op.π2 / L;
            f = Op.π2 / T;
            c = L / T;
            frameSpeed = c / 60f;
            milliSpeed = c / 1000f;
            frameSpace = (double)L / (double)Op.Scale;
            framesTotal = (int)frameSpace + 1;

            if (framesTotal > 0)
            {
                data = new float[framesTotal];
                for (int i = 0; i < framesTotal; i++)
                {
                    data[i] = (float)((double)halfH * Math.Sin(k * i * Op.Scale));
                }
            }
            else
            {
                Debug.WriteLine("No Frames:");
            }

            reflectMilli = _Milli + (int)((Op.FetchLength - (L * 2) - 10) / milliSpeed);

        }

        private void CalculateReverse()
        {
            L = Op.WaveLengthExact(T, d);
            offset = Op.FetchLength;
            halfH = H / 2f;
            k = Op.π2 / L;
            f = Op.π2 / T;
            c = L / T;
            milliSpeed = -c / 1000f;
            frameSpeed = c / 60f;
            frameSpace = (double)L / (double)Op.Scale;
            framesTotal = (int)frameSpace + 1;

            if (framesTotal > 0)
            {
                data = new float[framesTotal];
                for (int i = 0; i < framesTotal; i++)
                {
                    data[framesTotal - (i + 1)] = (float)((double)halfH * Math.Sin(k * i * Op.Scale));
                }
            }
            else
            {
                Debug.WriteLine("No Frames:");
            }
            c = -c;
            frameSpeed = -frameSpeed;
        }
    }
}


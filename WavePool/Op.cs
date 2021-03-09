using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavePool
{
    public static class Op
    {
        public static int TotalMilli = 0;
        public static double timeSpeed = 1;
        
        public static double nextHeight = 25;
        public static double nextPeriod = 9;
        public static double nextPhase = 0;
        public static double nextReflection = 1;
        public static double nextDepth = 1000;


        public static bool ShowTopForward = false;
        public static bool ShowTopReverse = false;
        public static bool ShowTopText = true;

        public static bool ShowBottomForward = false;
        public static bool ShowBottomReverse = false;
        public static bool ShowBottomText = true;


        public static bool showForward = false;
        public static bool showReverse = false;

        public static bool showForward1 = false;
        public static bool showReverse1 = false;

        public static bool showText = true;
        public static bool showText1 = true;







        public static double FetchLength;
        public static double Scale = 1;
        public const double g = 9.80665;
        public const double π = 3.1415927410125732421875;
        public const double π2 = π * 2;

        private static double LastWaveLength = 1;
        
        


        public static double WaveLengthExact(double T, double h)
        {
            int count = 0;
            double nextL = 0f;
            double diff = 1f;
            while (diff >= 0.01)
            {
                nextL = ((g * T * T) / (2f * π)) * Math.Tanh(((2f * π) / LastWaveLength) * h);
                if (nextL == double.NaN)
                {
                    Debug.WriteLine("Wavelength is NAN");
                }
                diff = Math.Abs(nextL - LastWaveLength);
                //Debug.WriteLine("Wavelength Loop: " + count + " "  + diff + " " + nextL + " " + LastWaveLength);
                LastWaveLength = nextL;
                count++;
            }

            Debug.WriteLine("Wavelength Loop: " + diff + " " + nextL + " " + LastWaveLength + " " + T + " " + h);
            return nextL;
        }

        public static double WaveLength2(bool isDeepWater, double T, double h)
        {

            if (isDeepWater)
            {
                return ((g * Math.Pow(T, 2)) / (2f * π));
            }
            else
            {
                return ((g * T * T) / (2f * π)) * Math.Pow(Math.Tanh(Math.Pow((((2f * π) / T) * Math.Sqrt(h / g)), (3f / 2f))), (2f / 3f));
            }
        }

        public static double WaveLength(double T, double h)
        {
            return ((g * T * T) / (2f * π)) * Math.Pow(Math.Tanh(Math.Pow((((2f * π) / T) * Math.Sqrt(h / g)), (3f / 2f))), (2f / 3f));
        }
    }
    
}

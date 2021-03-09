using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;



namespace WavePool
{

    public sealed partial class MenuBottom : Page
    {
        public static MenuBottom Current;
        private bool On = false;

        public MenuBottom()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Current = this;
            On = true;

            dud1.Background = new SolidColorBrush(Color.FromArgb(255, 32, 32, 32));
            dud2.Background = new SolidColorBrush(Color.FromArgb(255, 32, 32, 32));
            dud3.Background = new SolidColorBrush(Color.FromArgb(255, 32, 32, 32));         
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            SineWave2.Current.AddWave(Op.nextHeight, Op.nextPeriod, Op.nextDepth, false, Op.nextReflection, 0);
        }

        private void dud1_Click(object sender, RoutedEventArgs e)
        {
            if (SineWave2.Current.multiWave)
            {
                SineWave2.Current.multiWave = false;
                dud1.Background = new SolidColorBrush(Color.FromArgb(255, 32,32,32));
            }
            else
            {
                SineWave2.Current.multiWave = true;
                SineWave2.Current.multiTime = Op.TotalMilli;
                SineWave2.Current.multiH = Op.nextHeight;
                SineWave2.Current.multiT = Op.nextPeriod;
                SineWave2.Current.multiPhase = Op.nextPhase;
                SineWave2.Current.multiReflection = Op.nextReflection;
                SineWave2.Current.multiDepth = Op.nextDepth;

                dud1.Background = new SolidColorBrush(Color.FromArgb(255, 72, 72, 72));
            }
        }

        private void dud2_Click(object sender, RoutedEventArgs e)
        {
            if (SineWave2.Current.multiWave2)
            {
                SineWave2.Current.multiWave2 = false;
                dud2.Background = new SolidColorBrush(Color.FromArgb(255, 32, 32, 32));
            }
            else
            {
                SineWave2.Current.multiWave2 = true;
                SineWave2.Current.multiTime2 = Op.TotalMilli;
                SineWave2.Current.multiH2 = Op.nextHeight;
                SineWave2.Current.multiT2 = Op.nextPeriod;
                SineWave2.Current.multiPhase2 = Op.nextPhase;
                SineWave2.Current.multiReflection2 = Op.nextReflection;
                SineWave2.Current.multiDepth2 = Op.nextDepth;

                dud2.Background = new SolidColorBrush(Color.FromArgb(255, 72, 72, 72));
            }
        }

        private void dud3_Click(object sender, RoutedEventArgs e)
        {
            if (SineWave2.Current.multiWave3)
            {
                SineWave2.Current.multiWave3 = false;
                dud3.Background = new SolidColorBrush(Color.FromArgb(255, 32, 32, 32));
            }
            else
            {
                SineWave2.Current.multiWave3 = true;
                SineWave2.Current.multiTime3 = Op.TotalMilli;
                SineWave2.Current.multiH3 = Op.nextHeight;
                SineWave2.Current.multiT3 = Op.nextPeriod;
                SineWave2.Current.multiPhase3 = Op.nextPhase;
                SineWave2.Current.multiReflection3 = Op.nextReflection;
                SineWave2.Current.multiDepth3 = Op.nextDepth;

                dud3.Background = new SolidColorBrush(Color.FromArgb(255, 72, 72, 72));
            }
        }

        private void timeSpeed_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {

            if (On)
            {
                switch (timeSpeed.Value)
                {
                    case 0:
                        Op.timeSpeed = 0;
                        timeSpeedValue.Text = " x 0";
                        break;

                    case 1:
                        Op.timeSpeed = 0.015625;
                        timeSpeedValue.Text = " x 1/64";
                        break;

                    case 2:
                        Op.timeSpeed = 0.03125;
                        timeSpeedValue.Text = " x 1/32";
                        break;

                    case 3:
                        Op.timeSpeed = 0.0625;
                        timeSpeedValue.Text = " x 1/16";
                        break;

                    case 4:
                        Op.timeSpeed = 0.125;
                        timeSpeedValue.Text = " x 1/8";
                        break;

                    case 5:
                        Op.timeSpeed = 0.25;
                        timeSpeedValue.Text = " x 1/4";
                        break;

                    case 6:
                        Op.timeSpeed = 0.5;
                        timeSpeedValue.Text = " x 1/2";
                        break;

                    case 7:
                        Op.timeSpeed = 1;
                        timeSpeedValue.Text = " x 1";
                        break;

                    case 8:
                        Op.timeSpeed = 2;
                        timeSpeedValue.Text = " x 2";
                        break;

                    case 9:
                        Op.timeSpeed = 3;
                        timeSpeedValue.Text = " x 3";
                        break;

                    case 10:
                        Op.timeSpeed = 4;
                        timeSpeedValue.Text = " x 4";
                        break;

                    case 11:
                        Op.timeSpeed = 5;
                        timeSpeedValue.Text = " x 5";
                        break;

                    case 12:
                        Op.timeSpeed = 6;
                        timeSpeedValue.Text = " x 6";
                        break;

                    case 13:
                        Op.timeSpeed = 7;
                        timeSpeedValue.Text = " x 7";
                        break;

                    case 14:
                        Op.timeSpeed = 8;
                        timeSpeedValue.Text = " x 8";
                        break;

                    case 15:
                        Op.timeSpeed = 9;
                        timeSpeedValue.Text = " x 9";
                        break;

                    case 16:
                        Op.timeSpeed = 10;
                        timeSpeedValue.Text = " x 10";
                        break;

                    case 17:
                        Op.timeSpeed = 20;
                        timeSpeedValue.Text = " x 20";
                        break;

                    case 18:
                        Op.timeSpeed = 30;
                        timeSpeedValue.Text = " x 30";
                        break;

                    case 19:
                        Op.timeSpeed = 40;
                        timeSpeedValue.Text = " x 40";
                        break;

                    case 20:
                        Op.timeSpeed = 50;
                        timeSpeedValue.Text = " x 50";
                        break;
                }
            }        
        }

        private void depth_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (On)
            {
                depthValue.Text = depth.Value + "m";
                Op.nextDepth = depth.Value;
            }
        }

        private void Reflection_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (On)
            {
                ReflectionValue.Text = Reflection.Value.ToString("#0.0") + " %";
                //_Reflect = (float)Reflection.Value / 100;
                Op.nextReflection = (float)Reflection.Value / 100;
            }
        }

        private void ShowForward1_Click(object sender, RoutedEventArgs e)
        {
            if (Op.showForward1)
            {
                Op.showForward1 = false;
                ShowForward1.Background = new SolidColorBrush(Color.FromArgb(255, 32, 32, 32));
            }
            else
            {
                Op.showForward1 = true;
                ShowForward1.Background = new SolidColorBrush(Color.FromArgb(255, 72, 72, 72));
            }
        }

        private void ShowReverse1_Click(object sender, RoutedEventArgs e)
        {
            if (Op.showReverse1)
            {
                Op.showReverse1 = false;
                ShowReverse1.Background = new SolidColorBrush(Color.FromArgb(255, 32, 32, 32));
            }
            else
            {
                Op.showReverse1 = true;
                ShowReverse1.Background = new SolidColorBrush(Color.FromArgb(255, 72, 72, 72));
            }
        }

        private void ShowText_Click(object sender, RoutedEventArgs e)
        {
            if (Op.showText1)
            {
                Op.showText1 = false;
                ShowText.Background = new SolidColorBrush(Color.FromArgb(255, 32, 32, 32));
            }
            else
            {
                Op.showText1 = true;
                ShowText.Background = new SolidColorBrush(Color.FromArgb(255, 72, 72, 72));
            }
        }
    }
}

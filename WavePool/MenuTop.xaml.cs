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
    public sealed partial class MenuTop : Page
    {
        public static MenuTop Current;
        private bool On = false;

        public MenuTop()
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
            
            WaveHeightValue.Text = WaveHeight.Value.ToString("#0.0") + " m";
            PeroidValue.Text = Peroid.Value.ToString("#0") + " s";
            PhaseValue.Text = Phase.Value.ToString("#0.00");
            //ReflectionValue.Text = Reflection.Value.ToString("#0.0") + " %";
        }

        private void WaveHeight_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (On)
            {
                WaveHeightValue.Text = WaveHeight.Value.ToString("#0.0") + " m";
                //_H = (float)WaveHeight.Value;
                Op.nextHeight = (float)WaveHeight.Value;
            }
        }

        private void Peroid_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (On)
            {
                PeroidValue.Text = Peroid.Value.ToString("#0") + " s";
                //_T = (float)Peroid.Value;
                Op.nextPeriod = (float)Peroid.Value;
            }
        }

        private void Phase_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (On)
            {
                PhaseValue.Text = Phase.Value.ToString("#0.00");
                Op.nextPhase = (float)Phase.Value;
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            SineWave.Current.AddWave(Op.nextHeight, Op.nextPeriod, Op.nextDepth, false, Op.nextReflection,0);
        }

        private void dud_Click(object sender, RoutedEventArgs e)
        {
            if (SineWave.Current.multiWave)
            {
                SineWave.Current.multiWave = false;
                dud1.Background = new SolidColorBrush(Color.FromArgb(255, 32, 32, 32));
            }
            else
            {
                SineWave.Current.multiWave = true;
                //SineWave.Current.multiTime = Op.TotalMilli + (int)(Op.nextPeriod * 1000);

                SineWave.Current.multiTime = Op.TotalMilli ;

                SineWave.Current.multiH = Op.nextHeight;
                SineWave.Current.multiT = Op.nextPeriod;
                SineWave.Current.multiPhase = Op.nextPhase;
                SineWave.Current.multiReflection = Op.nextReflection;
                SineWave.Current.multiDepth = Op.nextDepth;


                dud1.Background = new SolidColorBrush(Color.FromArgb(255, 72,72,72));
            }
        }

        private void dud2_Click(object sender, RoutedEventArgs e)
        {
            if (SineWave.Current.multiWave2)
            {
                SineWave.Current.multiWave2 = false;
                dud2.Background = new SolidColorBrush(Color.FromArgb(255, 32, 32, 32));
            }
            else
            {
                SineWave.Current.multiWave2 = true;
                SineWave.Current.multiTime2 = Op.TotalMilli;
                SineWave.Current.multiH2 = Op.nextHeight;
                SineWave.Current.multiT2 = Op.nextPeriod;
                SineWave.Current.multiPhase2 = Op.nextPhase;
                SineWave.Current.multiReflection2 = Op.nextReflection;
                SineWave.Current.multiDepth2 = Op.nextDepth;

                dud2.Background = new SolidColorBrush(Color.FromArgb(255, 72, 72, 72));
            }
        }

        private void dud3_Click(object sender, RoutedEventArgs e)
        {
            if (SineWave.Current.multiWave3)
            {
                SineWave.Current.multiWave3 = false;
                dud3.Background = new SolidColorBrush(Color.FromArgb(255, 32, 32, 32));
            }
            else
            {
                SineWave.Current.multiWave3 = true;
                SineWave.Current.multiTime3 = Op.TotalMilli ;
                SineWave.Current.multiH3 = Op.nextHeight;
                SineWave.Current.multiT3 = Op.nextPeriod;
                SineWave.Current.multiPhase3 = Op.nextPhase;
                SineWave.Current.multiReflection3 = Op.nextReflection;
                SineWave.Current.multiDepth3 = Op.nextDepth;

                dud3.Background = new SolidColorBrush(Color.FromArgb(255, 72, 72, 72));
            }
        }

        private void ShowTopForward_Click(object sender, RoutedEventArgs e)
        {
            if (Op.showForward)
            {
                Op.showForward = false;
                ShowTopForward.Background = new SolidColorBrush(Color.FromArgb(255, 32,32,32));
            }
            else
            {
                Op.showForward = true;
                ShowTopForward.Background = new SolidColorBrush(Color.FromArgb(255, 72, 72, 72));
            }
        }

        private void ShowTopReverse_Click(object sender, RoutedEventArgs e)
        {
            if (Op.showReverse)
            {
                Op.showReverse = false;
                ShowTopReverse.Background = new SolidColorBrush(Color.FromArgb(255, 32, 32, 32));
            }
            else
            {
                Op.showReverse = true;
                ShowTopReverse.Background = new SolidColorBrush(Color.FromArgb(255, 72, 72, 72));
            }
        }

        private void ShowTopText_Click(object sender, RoutedEventArgs e)
        {
            if (Op.showText)
            {
                Op.showText = false;
                ShowTopText.Background = new SolidColorBrush(Color.FromArgb(255, 32,32,32));
            }
            else
            {
                Op.showText = true;
                ShowTopText.Background = new SolidColorBrush(Color.FromArgb(255, 72, 72, 72));
            }
        }

        private async void ShowHelp_ClickAsync(object sender, RoutedEventArgs e)
        {
            var uriHelpGeneral = new Uri(@"http://www.dallasadams.net/dallasadams/Software/WavePool/Help/Default.aspx");
            var success = await Windows.System.Launcher.LaunchUriAsync(uriHelpGeneral, new Windows.System.LauncherOptions() { DisplayApplicationPicker = false });
        }

    }
}

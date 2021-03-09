using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

namespace WavePool
{

    public sealed partial class MainPage : Page
    {

        public static MainPage Current;
        public bool On = false;


        public MainPage()
        {
            this.InitializeComponent();          
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Current = this;

            //Debug.WriteLine("Page_SizeChanged " + this.ActualWidth + " " + this.ActualWidth);


            if (!On)
            {
                try
                {
                    MenuTop.Navigate(typeof(MenuTop));
                    DisplayTop.Navigate(typeof(SineWave));
                    DisplayBottom.Navigate(typeof(SineWave2));
                    MenuBottom.Navigate(typeof(MenuBottom));
                    On = true;
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
            }


        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            Debug.WriteLine("Page Size " + this.ActualWidth + " " + this.ActualHeight);
            
            
        }
    }
}

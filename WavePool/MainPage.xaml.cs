namespace WavePool
{
    using System;
    using System.Diagnostics;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

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
                    _ = MenuTop.Navigate(typeof(MenuTop));
                    _ = DisplayTop.Navigate(typeof(SineWave));
                    _ = DisplayBottom.Navigate(typeof(SineWave2));
                    _ = MenuBottom.Navigate(typeof(MenuBottom));
                    On = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            Debug.WriteLine("Page Size " + this.ActualWidth + " " + this.ActualHeight);

        }
    }
}

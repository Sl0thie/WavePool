namespace WavePool
{
    using System;
    using System.Collections.Concurrent;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using System.Numerics;
    using Windows.UI;
    using System.Diagnostics;
    using Microsoft.Graphics.Canvas.Text;

    public sealed partial class SineWave : Page
    {
        public static SineWave Current;
        private bool On = false;

        private Brush BrushBackground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
        private Color colorBackground = Color.FromArgb(255, 0, 0, 0);
        private Color colorDot = Color.FromArgb(255, 64, 128, 255);
        private Color colorSeaFloor = Color.FromArgb(255, 64, 64, 64);
        private Color colorText = Color.FromArgb(255, 128, 128, 128);
        private Color colorTextSelected = Color.FromArgb(255, 64, 255, 64);
        private Color colorMainLine = Color.FromArgb(255, 32, 39, 255);
        private Color colorMainReverse = Color.FromArgb(255, 164, 0, 164);
        private Color colorMainForward = Color.FromArgb(196, 255, 128, 0);
        private Color colorSelectedLine = Color.FromArgb(255, 0, 255, 0);

        private Wave SelectedWave;
        private Wave SelectedWaveReverse;
        private bool IsSelected = false;
        private bool IsSelectedReverse = false;
        private int SelectType;
        public static float fixedDeltaTime = 1f / 60f;
        public ConcurrentDictionary<int, Wave> Waves;

        public bool multiWave = false;
        public int multiTime = 0;
        public double multiT;
        public double multiH;
        public double multiPhase;
        public double multiReflection;
        public double multiDepth;

        public bool multiWave2 = false;
        public int multiTime2 = 0;
        public double multiT2;
        public double multiH2;
        public double multiPhase2;
        public double multiReflection2;
        public double multiDepth2;

        public bool multiWave3 = false;
        public int multiTime3 = 0;
        public double multiT3;
        public double multiH3;
        public double multiPhase3;
        public double multiReflection3;
        public double multiDepth3;

        public int xOffset = 20;
        public int xSize;
        //public int xSizeOff;
        private float yOffset;
        public float[] FetchValues;
        public float[] FetchValuesForward;
        public float[] FetchValuesReversed;

        public int WaveCount = 0;
        public int nextWaveIndex = 0;
        public double depth = 1000;
        public float ScaleLineXHeight;

        private DateTime lastFrame = DateTime.Now;
        private double diffFrame;

        private float Text1X;
        private float Text2X;
        private float Text3X;
        private float Text4X;

        private string Text1Str = "";
        private string Text2Str = "";
        private string Text3Str = "";
        private string Text4Str = "";

        private CanvasTextFormat txtFormat = new CanvasTextFormat() { FontSize = 10 };

        public SineWave()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Current = this;
            On = true;
            Waves = new ConcurrentDictionary<int, Wave>();

            xSize = (int)(this.ActualWidth - (xOffset * 2));
            Op.FetchLength = xSize;
            //xSizeOff = xSize - 3;
            yOffset = (int)(this.ActualHeight / 2);
            FetchValues = new float[xSize];
            FetchValuesForward = new float[xSize];
            FetchValuesReversed = new float[xSize];
            ScaleLineXHeight = (float)(this.ActualHeight - 1);
            Debug.WriteLine("Page_Loaded " + xSize);

            Text1X = (xSize / 4 * 0) + xOffset;
            Text2X = (xSize / 4 * 1) + xOffset;
            Text3X = (xSize / 4 * 2) + xOffset;
            Text4X = (xSize / 4 * 3) + xOffset;
        }

        public void AddWave(double H, double T, double depth, bool Reverse, double Reflection, int Type)
        {
            try
            {
                if (Waves.TryAdd(nextWaveIndex, new Wave(H, T, depth, false, Reflection, Type, Op.TotalMilli, false)))
                {
                    nextWaveIndex++;
                    WaveCount++;
                }
            }
            catch (Exception ex) { Debug.WriteLine("AddWave: " + ex.Message); }
        }

        private void Canvas_DrawAnimated(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {

            #region Manage Time

            FetchValues = new float[xSize];
            FetchValuesForward = new float[xSize];
            FetchValuesReversed = new float[xSize];

            diffFrame = DateTime.Now.Subtract(lastFrame).Milliseconds;
            lastFrame = DateTime.Now;
            Op.TotalMilli += (int)(diffFrame * Op.timeSpeed);

            #endregion
            #region Add continuous waves
            if (multiWave)
            {
                if (multiTime <= Op.TotalMilli)
                {
                    Wave newWave = new Wave(multiH, multiT, multiDepth, false, multiReflection, 1, multiTime, false);
                    if (Waves.TryAdd(nextWaveIndex, newWave))
                    {
                        Text2Str = "H:" + newWave.H.ToString("0.0") + "   T:" + newWave.T + "   h:" + newWave.d.ToString("0.0") + "   c:" + newWave.c.ToString("0.00") + "   L:" + newWave.L.ToString("0.00");
                        nextWaveIndex++;
                        WaveCount++;
                    }
                    multiTime = multiTime + (int)(multiT * 1000) + (int)(multiT * 1000 * multiPhase);
                }
            }
            else
            {
                Text2Str = "";
            }

            if (multiWave2)
            {
                //Text3Str = "H:" + multiH2 + "  T:" + multiT2 + "  h:" + multiDepth2;
                if (multiTime2 <= Op.TotalMilli)
                {
                    Wave newWave = new Wave(multiH2, multiT2, multiDepth2, false, multiReflection2, 2, multiTime2, false);
                    if (Waves.TryAdd(nextWaveIndex, newWave))
                    {
                        Text3Str = "H:" + newWave.H.ToString("0.0") + "   T:" + newWave.T + "   h:" + newWave.d.ToString("0.0") + "   c:" + newWave.c.ToString("0.00") + "   L:" + newWave.L.ToString("0.00");
                        nextWaveIndex++;
                        WaveCount++;
                    }
                    multiTime2 = multiTime2 + (int)(multiT2 * 1000) + (int)(multiT2 * 1000 * multiPhase2);
                }
            }
            else
            {
                Text3Str = "";
            }

            if (multiWave3)
            {
                //Text4Str = "H:" + multiH3 + "  T:" + multiT3 + "  h:" + multiDepth3;
                if (multiTime3 <= Op.TotalMilli)
                {
                    Wave newWave = new Wave(multiH3, multiT3, multiDepth3, false, multiReflection3, 3, multiTime3, false);
                    if (Waves.TryAdd(nextWaveIndex, newWave))
                    {
                        Text4Str = "H:" + newWave.H.ToString("0.0") + "   T:" + newWave.T + "   h:" + newWave.d.ToString("0.0") + "   c:" + newWave.c.ToString("0.00") + "   L:" + newWave.L.ToString("0.00");
                        nextWaveIndex++;
                        WaveCount++;
                    }
                    multiTime3 = multiTime3 + (int)(multiT3 * 1000) + (int)(multiT3 * 1000 * multiPhase3);
                }
            }
            else
            {
                Text4Str = "";
            }

            #endregion     
            #region Move Waves/Sink/Reflection
            foreach (var nextWave in Waves)
            {
                //Update offset.               
                if (!nextWave.Value.Reversed)
                {
                    nextWave.Value.offset = nextWave.Value.milliSpeed * (Op.TotalMilli - nextWave.Value.Milli);

                    //Check for reflection.
                    if (!nextWave.Value.Reflected)
                    {
                        //if (nextWave.Value.offset + nextWave.Value.L2 >= xSize)
                        if (nextWave.Value.reflectMilli <= Op.TotalMilli)
                        {
                            if (nextWave.Value.Reflection != 0)
                            {
                                Wave newWave = new Wave(nextWave.Value.H * nextWave.Value.Reflection, nextWave.Value.T, nextWave.Value.d, true, nextWave.Value.Reflection, nextWave.Value.Type, Op.TotalMilli, nextWave.Value.Selected);

                                if (nextWave.Value.Selected && (nextWave.Value == SelectedWave))
                                {
                                    newWave.Selected = true;
                                    SelectedWaveReverse = newWave;
                                    IsSelectedReverse = true;
                                }

                                if (Waves.TryAdd(nextWaveIndex, newWave))
                                {
                                    nextWaveIndex++;
                                    WaveCount++;
                                    nextWave.Value.Reflected = true;
                                }
                            }
                            else
                            {
                                nextWave.Value.Reflected = true;
                            }
                        }

                        //Check if wave needs to be removed.
                        if (nextWave.Value.offset - nextWave.Value.L > xSize)
                        {
                            if (nextWave.Value.Selected)
                            {
                                SelectedWave = null;
                                IsSelected = false;
                            }

                            //Start delete.
                            Wave sink;
                            if (!Waves.TryRemove(nextWave.Key, out sink)) { Debug.WriteLine("Can't Remove"); WaveCount--; } else { Debug.WriteLine("Removed H:" + sink.H + " T:" + sink.T); }
                        }

                        //Check if wave needs to be removed.
                        if (nextWave.Value.offset < -nextWave.Value.L)
                        {
                            if (nextWave.Value.Selected)
                            {
                                SelectedWaveReverse = null;
                                IsSelectedReverse = false;
                            }

                            //Start delete.
                            Wave sink;
                            if (!Waves.TryRemove(nextWave.Key, out sink)) { Debug.WriteLine("Can't Remove"); WaveCount--; } else { Debug.WriteLine("Removed H:" + sink.H + " T:" + sink.T); }
                        }
                    }
                }
                else
                {
                    nextWave.Value.offset = xSize + (nextWave.Value.milliSpeed * (Op.TotalMilli - nextWave.Value.Milli));

                    //Check if wave needs to be removed for reverse.
                    if (nextWave.Value.offset + nextWave.Value.L < -10)
                    {
                        //Start delete.
                        Wave sink;
                        if (!Waves.TryRemove(nextWave.Key, out sink)) { Debug.WriteLine("Can't Remove"); WaveCount--; } else { Debug.WriteLine("Removed H:" + sink.H + " T:" + sink.T); }
                    }
                }
            }

            #endregion
            #region Build Lines

            foreach (var nextItem in Waves)
            {
                //nextItem.Value.offset += (nextItem.Value.c * diffFrame);
                try
                {
                    int offset = (int)nextItem.Value.offset;
                    for (int i = 0; i < nextItem.Value.data.Length; i++)
                    {
                        try
                        {
                            if ((i + offset >= 0) && (i + offset < FetchValues.Length))
                            {
                                FetchValues[i + offset] += nextItem.Value.data[i];
                            }
                        }
                        catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }

                        if (nextItem.Value.Reversed)
                        {
                            try
                            {
                                if ((i + offset >= 0) && (i + offset < FetchValuesReversed.Length))
                                {
                                    FetchValuesReversed[i + offset] += nextItem.Value.data[i];
                                }
                            }
                            catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
                        }
                        else
                        {
                            try
                            {
                                if ((i + offset >= 0) && (i + offset < FetchValuesForward.Length))
                                {
                                    FetchValuesForward[i + offset] += nextItem.Value.data[i];
                                }
                            }
                            catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }

                        }
                    }
                }
                catch { }

            }

            #endregion
            #region Draw Image

            args.DrawingSession.Clear(colorBackground);
            args.DrawingSession.Antialiasing = Microsoft.Graphics.Canvas.CanvasAntialiasing.Antialiased;

            //Reverse.
            if (Op.showReverse)
            {
                try
                {
                    for (int i = 0; i < xSize - 2; i++)
                    {
                        args.DrawingSession.DrawLine(xOffset + i, yOffset - FetchValuesReversed[i], xOffset + i + 1, yOffset - FetchValuesReversed[i + 1], colorMainReverse, 1.6f);
                    }
                }
                catch (Exception ex) { Debug.WriteLine("ERR2 " + ex.Message); }
            }

            //Forward.
            if (Op.showForward)
            {
                try
                {
                    for (int i = 0; i < xSize - 2; i++)
                    {
                        args.DrawingSession.DrawLine(xOffset + i, yOffset - FetchValuesForward[i], xOffset + i + 1, yOffset - FetchValuesForward[i + 1], colorMainForward, 1.6f);
                    }
                }
                catch (Exception ex) { Debug.WriteLine("ERR2 " + ex.Message); }
            }

            //Major line.
            try
            {
                for (int i = 0; i < xSize - 2; i++)
                {
                    args.DrawingSession.DrawLine(xOffset + i, yOffset - FetchValues[i], xOffset + i + 1, yOffset - FetchValues[i + 1], colorMainLine, 2.1f);
                }
            }
            catch (Exception ex) { Debug.WriteLine("ERR2 " + ex.Message); }

            try
            {
                for (int i = 0; i < xSize - 2; i = i + 20)
                {
                    args.DrawingSession.DrawLine(xOffset + i, ScaleLineXHeight, xOffset + i, ScaleLineXHeight - 4, colorSeaFloor, 1.1f);
                }
                args.DrawingSession.DrawLine(xOffset, ScaleLineXHeight, xOffset + xSize, ScaleLineXHeight, colorSeaFloor, 1.1f);
            }
            catch (Exception ex) { Debug.WriteLine("ERR3 " + ex.Message); }

            //Text.
            if (Op.showText)
            {
                try
                {
                    args.DrawingSession.DrawText(Text1Str, Text1X, 2, colorText, txtFormat);
                    args.DrawingSession.DrawText(Text2Str, Text2X, 2, colorText, txtFormat);
                    args.DrawingSession.DrawText(Text3Str, Text3X, 2, colorText, txtFormat);
                    args.DrawingSession.DrawText(Text4Str, Text4X, 2, colorText, txtFormat);
                }
                catch (Exception ex) { Debug.WriteLine("ERR2 " + ex.Message); }
            }

            if (IsSelected)
            {
                try
                {
                    int selStart = (int)SelectedWave.offset;
                    if (selStart < 0) { selStart = 0; }

                    int selFinish = (int)(SelectedWave.offset + SelectedWave.L);
                    if (selFinish > xSize - 2) { selFinish = xSize - 2; }

                    for (int i = selStart; i < selFinish; i++)
                    {
                        args.DrawingSession.DrawLine(xOffset + i, yOffset - FetchValues[i], xOffset + i + 1, yOffset - FetchValues[i + 1], colorSelectedLine, 1.6f);
                    }

                    if (Op.showText)
                    {
                        switch (SelectType)
                        {
                            case 0:
                                args.DrawingSession.DrawText(Text1Str, Text1X, 2, colorTextSelected, txtFormat);
                                break;
                            case 1:
                                args.DrawingSession.DrawText(Text2Str, Text2X, 2, colorTextSelected, txtFormat);
                                break;
                            case 2:
                                args.DrawingSession.DrawText(Text3Str, Text3X, 2, colorTextSelected, txtFormat);
                                break;
                            case 3:
                                args.DrawingSession.DrawText(Text4Str, Text4X, 2, colorTextSelected, txtFormat);
                                break;
                        }
                    }

                }
                catch (Exception ex) { Debug.WriteLine("ERR3 " + ex.Message); }
            }

            if (IsSelectedReverse)
            {
                try
                {
                    int selStart = (int)SelectedWaveReverse.offset;
                    if (selStart < 0) { selStart = 0; }

                    int selFinish = (int)(SelectedWaveReverse.offset + SelectedWaveReverse.L);
                    if (selFinish > xSize - 2) { selFinish = xSize - 2; }

                    for (int i = selStart; i < selFinish; i++)
                    {
                        args.DrawingSession.DrawLine(xOffset + i, yOffset - FetchValues[i], xOffset + i + 1, yOffset - FetchValues[i + 1], colorSelectedLine, 1.6f);
                    }

                    if (Op.showText)
                    {
                        switch (SelectType)
                        {
                            case 0:
                                args.DrawingSession.DrawText(Text1Str, Text1X, 2, colorTextSelected, txtFormat);
                                break;
                            case 1:
                                args.DrawingSession.DrawText(Text2Str, Text2X, 2, colorTextSelected, txtFormat);
                                break;
                            case 2:
                                args.DrawingSession.DrawText(Text3Str, Text3X, 2, colorTextSelected, txtFormat);
                                break;
                            case 3:
                                args.DrawingSession.DrawText(Text4Str, Text4X, 2, colorTextSelected, txtFormat);
                                break;
                        }
                    }

                }
                catch (Exception ex) { Debug.WriteLine("ERR3 " + ex.Message); }
            }

            #endregion
        }

        private void canvas_Tapped(object sender, TappedRoutedEventArgs e)
        {

            SelectedWave = null;
            SelectedWaveReverse = null;
            IsSelected = false;
            IsSelectedReverse = false;
            SelectType = -1;

            Vector2 MousePoint = new Vector2((float)e.GetPosition(canvas).X, (float)e.GetPosition(canvas).Y);
            //Debug.WriteLine("Tapped: " + MousePoint);

            foreach (var nextItem in Waves)
            {

                if ((nextItem.Value.offset < MousePoint.X) && (nextItem.Value.offset + nextItem.Value.L > MousePoint.X))
                {
                    if (nextItem.Value.Selected)
                    {
                        SelectedWave = null;
                        nextItem.Value.Selected = false;
                        IsSelected = false;
                    }
                    else
                    {
                        SelectedWave = nextItem.Value;
                        nextItem.Value.Selected = true;
                        IsSelected = true;
                        SelectType = nextItem.Value.Type;
                    }

                    //Debug.WriteLine("Tapped: " + MousePoint);
                    //Debug.WriteLine("Wave: " + nextItem.Value.offset + " " + (nextItem.Value.offset + nextItem.Value.L));
                }
            }
        }
    }
}

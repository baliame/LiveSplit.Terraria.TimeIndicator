using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LiveSplit.UI.Components
{
    public class TerrariaTimeIndicator : IComponent
    {
        public TerrariaTimeIndicatorSettings Settings { get; set; }
        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public float SkyObjectT { get; private set; }
        public Color BackgroundColor { get; private set; }
        public bool IsNightTime { get; private set;  }

        public float VerticalHeight { get { return Settings.ComponentHeight; } }
        public float MinimumWidth { get; private set; }
        public float HorizontalWidth { get { return Settings.ComponentWidth; } }
        public float MinimumHeight { get; private set; }
        public float PaddingTop { get { return 0f; } }
        public float PaddingLeft { get { return 0f; } }
        public float PaddingBottom { get { return 0f; } }
        public float PaddingRight { get { return 0f; } }

        public string ComponentName
        {
            get { return "Terraria Time Indicator"; }
        }

        public IDictionary<string, Action> ContextMenuControls
        {
            get { return null; }
        }

        public void Dispose() { }

        public void Update(IInvalidator invalidator, Model.LiveSplitState state, float width, float height, LayoutMode mode) {
            TimeSpan? tq = state.CurrentTime[state.CurrentTimingMethod];
            if (tq.HasValue)
            {
                TimeSpan t = tq.Value;

                int H = ((int)(t.TotalMinutes + 8.25));
                int M = ((int)t.TotalSeconds + 495) - H * 60;
                H = H % 24;
                M = M % 60;
                SetTime(H, M);
            }

            if (invalidator != null)
            {
                invalidator.Invalidate(0, 0, width, height);
            }
        }

        public Control GetSettingsControl(LayoutMode mode)
        {
            return Settings;
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            var brush = new SolidBrush(BackgroundColor);
            g.FillRectangle(brush, 0, 0, width, VerticalHeight);

            float r = width < VerticalHeight ? width / 4 : VerticalHeight / 4;
            float cx = width * SkyObjectT;
            float cy = VerticalHeight / 2;
            var skybrush = new SolidBrush(IsNightTime ? Color.LightGray : Color.DarkGoldenrod);
            g.FillEllipse(skybrush, cx - r, cy - r, 2 * r, 2 * r);
            if (IsNightTime)
            {
                for (float x = 20, y = 10; x < width; x += 40, y += 10)
                {
                    y = y % VerticalHeight;
                    g.FillEllipse(skybrush, x - 1, y - 1, 3, 3);
                }
            }
        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            var brush = new SolidBrush(BackgroundColor);
            g.FillRectangle(brush, 0, 0, HorizontalWidth, height);

            float r = HorizontalWidth < height ? HorizontalWidth / 4 : height / 4;
            float cx = HorizontalWidth / 2;
            float cy = height * SkyObjectT;
            var skybrush = new SolidBrush(IsNightTime ? Color.LightGray : Color.DarkGoldenrod);
            g.FillEllipse(skybrush, cx - r, cy - r, 2 * r, 2 * r);
            if (IsNightTime)
            {
                for (float y = 5, x = 5; y < height; x += 5, y += 5)
                {
                    x = x % HorizontalWidth;
                    g.FillEllipse(skybrush, x - 1, y - 1, 3, 3);
                }
            }
        }

        public void SetSettings(System.Xml.XmlNode settings)
        {
            Settings.SetSettings(settings);
        }

        public System.Xml.XmlNode GetSettings(System.Xml.XmlDocument document)
        {
            return Settings.GetSettings(document);
        }

        private bool IsNight()
        {
            return !((Hour > 4 && Hour < 19) || (Hour == 4 && Minute >= 30) || (Hour == 19 && Minute < 30));
        }

        private float CalcSkyObjectT()
        {
            if (IsNightTime)
            {
                int NMinStart = 19 * 60 + 30;
                int NMinEnd = 24 * 60 + 4 * 60 + 30;
                int NMinCurr = Hour * 60 + Minute + (Hour <= 4 ? 24 * 60 : 0);
                return (float)(NMinCurr - NMinStart) / (float)(NMinEnd - NMinStart);
            } else {
                int NMinStart = 4 * 60 + 30;
                int NMinEnd = 19 * 60 + 30;
                int NMinCurr = Hour * 60 + Minute;
                return (float)(NMinCurr - NMinStart) / (float)(NMinEnd - NMinStart);
            }
        }

        private Color CalcBackgroundColor()
        {
            if (IsNightTime)
            {
                return Color.Black;
            } else
            {
                if (SkyObjectT >= 0.25 && SkyObjectT <= 0.75)
                {
                    return Color.SkyBlue;
                } else
                {
                    float Grad = 0;
                    if (SkyObjectT < 0.25)
                    {
                        Grad = SkyObjectT * 4;
                    } else
                    {
                        Grad = (1 - SkyObjectT) * 4;
                    }
                    int R = (int)(Color.SkyBlue.R * Grad);
                    int G = (int)(Color.SkyBlue.G * Grad);
                    int B = (int)(Color.SkyBlue.B * Grad);
                    return Color.FromArgb(255, R, G, B);
                }
            }
        }

        public TerrariaTimeIndicator()
        {
            MinimumWidth = 32;
            MinimumHeight = 16;
            Settings = new TerrariaTimeIndicatorSettings();
            Reset();
        }

        public void Reset()
        {
            SetTime(8, 15);
        }

        public void EnchantedSundial()
        {
            SetTime(4, 30);
        }

        private void SetTime(int H, int M)
        {
            Hour = H;
            Minute = M;
            IsNightTime = IsNight();
            SkyObjectT = CalcSkyObjectT();
            BackgroundColor = CalcBackgroundColor();
        }

    }
}

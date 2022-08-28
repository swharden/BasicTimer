using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace BasicTimer
{
    internal class TimerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public readonly TimeTracker Timer = new();
        public string Text => Timer.ToString();

        public bool CountDown { get => !Timer.CountingUpward; set => Timer.CountingUpward = !value; }

        public bool BeepOnRollover { get; set; } = false;

        public void Tick()
        {
            double progressBefore = ProgressFraction;
            Timer.UpdateTime();
            double progressAfter = ProgressFraction;
            bool rolledOver = CountDown ? progressBefore < progressAfter : progressBefore > progressAfter;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProgressWidth)));

            if (rolledOver & BeepOnRollover)
            {
                SystemSounds.Beep.Play();
            }
        }

        public Version Version => Assembly.GetExecutingAssembly().GetName().Version!;
        public string VersionString => $"Basic Timer {Version.Major}.{Version.Minor}";

        private double _windowHeight = 60;
        public double WindowHeight
        {
            get => _windowHeight;
            set
            {
                _windowHeight = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WindowHeight)));
            }
        }

        private Brush _progressForegroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#006699"));
        public Brush ProgressForegroundBrush
        {
            get => _progressForegroundBrush;
            set
            {
                _progressForegroundBrush = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProgressForegroundBrush)));
            }
        }

        private Brush _progressBackgroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#003366"));
        public Brush ProgressBackgroundBrush
        {
            get => _progressBackgroundBrush;
            set
            {
                _progressBackgroundBrush = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProgressBackgroundBrush)));
            }
        }

        private int _fontSize = 36;
        public int FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontSize)));
            }
        }

        private int _progressWidthSeconds = 10;
        public int ProgressWidthSeconds
        {
            get => _progressWidthSeconds;
            set
            {
                _progressWidthSeconds = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProgressWidthMax)));
            }
        }

        private double _progressWidthMax;
        public double ProgressWidthMax
        {
            get => _progressWidthMax;
            set
            {
                _progressWidthMax = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProgressWidthMax)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProgressWidth)));
            }
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }

        public double ProgressFraction
        {
            get
            {
                double width = ProgressWidthSeconds;
                double sec = Timer.TimeOnClock.TotalSeconds;

                if (sec == 0)
                    return 0;

                return (sec < 0)
                    ? (width - Math.Abs(sec) % width) / width
                    : (sec % width) / width;
            }
        }
        public double ProgressWidth => ProgressFraction * ProgressWidthMax;

        public void SetColor(Brush progressBackground, Brush progressForeground)
        {
            ProgressBackgroundBrush = progressBackground;
            ProgressForegroundBrush = progressForeground;
            // TODO: set font color
        }

        public void SetColor(string progressBackground, string progressForeground)
        {
            ProgressBackgroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(progressBackground));
            ProgressForegroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(progressForeground));
            // TODO: set font color
        }
    }
}

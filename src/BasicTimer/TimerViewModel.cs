using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BasicTimer
{
    internal class TimerViewModel : INotifyPropertyChanged
    {
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version!;
        public string VersionString => $"Stupid Timer {Version.Major}.{Version.Minor}";

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
            set {
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

        public string Text => GetStopwatchText();

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

        public double ProgressWidth
        {
            get
            {
                if (ProgressWidthSeconds == 0)
                    return 0;

                double progressFraction = (Stopwatch.Elapsed.TotalSeconds % ProgressWidthSeconds) / ProgressWidthSeconds;

                return progressFraction * ProgressWidthMax;
            }

        }

        private readonly System.Diagnostics.Stopwatch Stopwatch = new();

        private string GetStopwatchText()
        {
            TimeSpan ts = Stopwatch.Elapsed;
            StringBuilder sb = new();
            sb.Append($"{ts.Hours:00}:");
            sb.Append($"{ts.Minutes:00}:");
            sb.Append($"{ts.Seconds:00}.");
            sb.Append($"{ts.Milliseconds / 10:00}");
            return sb.ToString();
        }

        public void Restart() => Stopwatch.Restart();

        public void Stop() => Stopwatch.Stop();

        public void Start() => Stopwatch.Start();

        public void Tick()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProgressWidth)));
        }

        public void Tick(object? sender, EventArgs e) => Tick();

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

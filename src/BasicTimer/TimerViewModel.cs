﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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

        public void Tick(object? sender, EventArgs e)
        {
            Timer.UpdateTime();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProgressWidth)));
        }

        public Version Version => Assembly.GetExecutingAssembly().GetName().Version!;
        public string VersionString => $"Basic Timer {Version.Major}.{Version.Minor}";

        public bool EnableKeyboardShortcuts { get; set; } = true;

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

        public double ProgressWidth
        {
            get
            {
                if (ProgressWidthSeconds == 0)
                    return 0;

                double progressFraction = (Timer.Elapsed.TotalSeconds % ProgressWidthSeconds) / ProgressWidthSeconds;

                return progressFraction * ProgressWidthMax;
            }

        }

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

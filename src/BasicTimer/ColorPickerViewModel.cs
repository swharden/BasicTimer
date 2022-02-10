using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BasicTimer
{
    internal class ColorPickerViewModel : INotifyPropertyChanged
    {
        public Brush ForegroundBrush { get; set; } = Brushes.Gray;

        public Brush BackgroundBrush { get; set; } = Brushes.Red;

        private string _backgroundColorString = "#003366";

        private string _foregroundColorString = "#006699";

        public string BackgroundColorString
        {
            get => _backgroundColorString;
            set
            {
                _backgroundColorString = value;

                try
                {
                    BackgroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(value));
                }
                catch (FormatException)
                {
                    System.Diagnostics.Debug.WriteLine($"Invalid color: {value}");
                    return;
                }
                System.Diagnostics.Debug.WriteLine($"Valid color: {value}");

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BackgroundColorString)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BackgroundBrush)));
            }
        }


        public string ForegroundColorString
        {
            get => _foregroundColorString;
            set
            {
                _foregroundColorString = value;

                try
                {
                    ForegroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(value));
                }
                catch (FormatException)
                {
                    System.Diagnostics.Debug.WriteLine($"Invalid color: {value}");
                    return;
                }
                System.Diagnostics.Debug.WriteLine($"Valid color: {value}");

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ForegroundColorString)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ForegroundBrush)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

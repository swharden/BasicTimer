using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BasicTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TimerViewModel VM = new();
        private DispatcherTimer RedrawTimer = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = VM;

            RedrawTimer.Interval = TimeSpan.FromMilliseconds(10);
            RedrawTimer.Tick += VM.Tick;
            RedrawTimer.Start();

            VM.Start();
        }

        private void MainCanvas_SizeChanged(object sender, SizeChangedEventArgs e) => VM.ProgressWidthMax = MainCanvas.ActualWidth;
        private void MenuItem_Restart_Click(object sender, RoutedEventArgs e) => VM.Restart();
        private void MenuItem_Stop_Click(object sender, RoutedEventArgs e) => VM.Stop();
        private void MenuItem_Start_Click(object sender, RoutedEventArgs e) => VM.Start();
        private void MenuItem_Copy_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(VM.Text);
        private void MenuItem_ExitApp_Click(object sender, RoutedEventArgs e) => Close();
        private void MenuItem_CloseMenu_Click(object sender, RoutedEventArgs e) { }

        private void MenuItem_FontSize_Click(object sender, RoutedEventArgs e) =>
            VM.FontSize = int.Parse(((MenuItem)sender).Tag.ToString()!);

        private void MenuItem_ProgressUnitSize_Click(object sender, RoutedEventArgs e) =>
            VM.ProgressWidthSeconds = int.Parse(((MenuItem)sender).Tag.ToString()!);

        private void MenuItem_ToggleTitleBar_Click(object sender, RoutedEventArgs e)
        {
            if (WindowStyle == WindowStyle.None)
            {
                WindowStyle = WindowStyle.SingleBorderWindow;
                ResizeMode = ResizeMode.CanResizeWithGrip;
                VM.WindowHeight = ActualHeight + SystemParameters.WindowCaptionHeight * 2;
            }
            else
            {
                WindowStyle = WindowStyle.None;
                ResizeMode = ResizeMode.NoResize;
                VM.WindowHeight = ActualHeight - SystemParameters.WindowCaptionHeight * 2;
            }
        }

        private void MenuItem_ToggleAlwaysOnTop_Click(object sender, RoutedEventArgs e) =>
            Topmost = ((MenuItem)sender).IsChecked;

        private void MenuItem_Version_Click(object sender, RoutedEventArgs e) =>
            System.Diagnostics.Process.Start("explorer", "https://github.com/swharden/BasicTimer");

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void MenuItem_SetBackgroundColor_Click(object sender, RoutedEventArgs e)
        {
        }

        private void MenuItem_SetProgressColor_Click(object sender, RoutedEventArgs e)
        {
            var win = new ColorPickerWindow();
            win.ShowDialog();
            VM.ProgressBackgroundBrush = win.BackgroundBrush;
            VM.ProgressForegroundBrush = win.ForegroundBrush;
            System.Diagnostics.Debug.WriteLine($"{VM.ProgressBackgroundBrush} {VM.ProgressForegroundBrush}");
        }
    }
}

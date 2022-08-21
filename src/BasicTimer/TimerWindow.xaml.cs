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
using System.Windows.Shell;
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
            RedrawTimer.Tick += RedrawTimer_Tick; ;
            RedrawTimer.Start();

            TaskbarItemInfo = new();
        }

        private void RedrawTimer_Tick(object sender, EventArgs e)
        {
            VM.Tick();
            TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Normal;
            TaskbarItemInfo.ProgressValue = VM.ProgressFraction;
        }

        private void MainCanvas_SizeChanged(object sender, SizeChangedEventArgs e) => VM.ProgressWidthMax = MainCanvas.ActualWidth;
        private void MenuItem_Restart_Click(object sender, RoutedEventArgs e) => VM.Timer.Restart();
        private void MenuItem_Stop_Click(object sender, RoutedEventArgs e) => VM.Timer.Stop();
        private void MenuItem_Start_Click(object sender, RoutedEventArgs e) => VM.Timer.Start();
        private void MenuItem_Pause_Click(object sender, RoutedEventArgs e) => VM.Timer.Pause();
        private void MenuItem_Reset_Click(object sender, RoutedEventArgs e) => VM.Timer.Reset();
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
            System.Diagnostics.Process.Start("explorer", "https://swharden.github.io/BasicTimer/");

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void MenuItem_SelectColors_Click(object sender, RoutedEventArgs e)
        {
            var win = new ColorPickerWindow();
            win.ShowDialog();
            VM.SetColor(win.BackgroundBrush, win.ForegroundBrush);
        }

        private void MenuItem_Palette_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            VM.SetColor(menuItem.Background, menuItem.Foreground);
        }

        private void MenuItem_ShowTitle_Click(object sender, RoutedEventArgs e)
        {
            var win = new SetTitleWindow(VM.Title);
            win.ShowDialog();
            VM.Title = win.NewTitle;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!VM.EnableKeyboardShortcuts)
                return;

            switch (e.Key)
            {
                case Key.P:
                case Key.Space:
                    VM.Timer.Pause();
                    return;
                case Key.R:
                    VM.Timer.Restart();
                    return;
                case Key.S:
                    VM.Timer.Reset();
                    return;
            }
        }

        private void MenuItem_SetTime_Click(object sender, RoutedEventArgs e)
        {
            var win = new SetTimeWindow(VM.Timer.Minutes, VM.Timer.Seconds);
            win.ShowDialog();
            if (win.TotalSeconds is not null)
                VM.Timer.Set(win.TotalSeconds.Value);
        }
    }
}

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
using System.Windows.Shapes;

namespace BasicTimer
{
    /// <summary>
    /// Interaction logic for SetTitleWindow.xaml
    /// </summary>
    public partial class SetTitleWindow : Window
    {
        public string NewTitle;

        public SetTitleWindow(string originalTitle)
        {
            InitializeComponent();
            NewTitle = originalTitle;
            TitleTextbox.Text = NewTitle;
            TitleTextbox.Focus();
            TitleTextbox.SelectAll();

        }

        private void SaveAndClose()
        {
            NewTitle = TitleTextbox.Text;
            Close();
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e) => SaveAndClose();

        private void Button_Cancel_Click(object sender, RoutedEventArgs e) => Close();

        private void TitleTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveAndClose();
            }
            else if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}

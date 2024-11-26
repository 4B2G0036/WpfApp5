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

namespace WpfApp5
{
    /// <summary>
    /// myDocumentViewer.xaml 的互動邏輯
    /// </summary>
    public partial class myDocumentViewer : Window
    {
        Color fontColor = Colors.Black;
        public myDocumentViewer()
        {
            InitializeComponent();
            fontColorPicker.SelectedColor = fontColor;

            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                fontFamilyComboBox.Items.Add(fontFamily.Source);
            }
            fontFamilyComboBox.SelectedItem = "Arial";

            fontSizeComboBox.ItemsSource = new List<string> { "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72" };
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myDocumentViewer myDocumentViewer = new myDocumentViewer();
            myDocumentViewer.Show();
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}

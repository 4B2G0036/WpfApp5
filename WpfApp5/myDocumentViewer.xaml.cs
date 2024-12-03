using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
            fontFamilyComboBox.SelectedIndex = 13;

            fontSizeComboBox.ItemsSource = new List<string> { "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72" };
            fontSizeComboBox.SelectedIndex = 3;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myDocumentViewer myDocumentViewer = new myDocumentViewer();
            myDocumentViewer.Show();
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            OpenFileDialog.DefaultExt = ".rtf";
            OpenFileDialog.AddExtension = true;

            if (OpenFileDialog.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(OpenFileDialog.FileName, FileMode.Open);
                TextRange range = new TextRange(rtbEditer.Document.ContentStart, rtbEditer.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
                fileStream.Close();
            }
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            saveFileDialog.DefaultExt = ".rtf";
            saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == true) 
            {
                FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create);
                TextRange range = new TextRange(rtbEditer.Document.ContentStart, rtbEditer.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
                fileStream.Close();
            }
        }

        private void fontColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            fontColor = (Color)e.NewValue;
            SolidColorBrush fontBrush = new SolidColorBrush(fontColor);
            if (rtbEditer != null && rtbEditer.Selection != null)
            {
                rtbEditer.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, fontBrush);
            }
        }

        private void fontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontFamilyComboBox_SelectionChanged != null)
            {
                rtbEditer.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, fontFamilyComboBox.SelectedItem);
            }
        }

        private void fontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontSizeComboBox.SelectedItem != null)
            {
                rtbEditer.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSizeComboBox.SelectedItem.ToString());
            }
        }

        private void rtbEditer_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var property_bold = rtbEditer.Selection.GetPropertyValue(TextElement.FontWeightProperty);
            boldButton.IsChecked = (property_bold != DependencyProperty.UnsetValue) && (property_bold.Equals(FontWeights.Bold));

            var property_italic = rtbEditer.Selection.GetPropertyValue(TextElement.FontStyleProperty);
            italicButton.IsChecked = (property_italic != DependencyProperty.UnsetValue) && (property_italic.Equals(FontStyles.Italic));
            
            var property_underline = rtbEditer.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            underlineButton.IsChecked = (property_underline != DependencyProperty.UnsetValue) && (property_underline.Equals(TextDecorations.Underline));

            var property_fontFamily = rtbEditer.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
                fontFamilyComboBox.SelectedItem = property_fontFamily.ToString();


            var property_fontSize = rtbEditer.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            fontSizeComboBox.SelectedItem = property_fontSize;
            

            var property_fontColor = rtbEditer.Selection.GetPropertyValue(TextElement.ForegroundProperty);
                fontColorPicker.SelectedColor = ((SolidColorBrush)property_fontColor).Color;
        }

        private void trashButton_Click(object sender, RoutedEventArgs e)
        {
            rtbEditer.Document.Blocks.Clear();
        }
    }
}

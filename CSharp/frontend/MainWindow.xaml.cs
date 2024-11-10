using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Frontend.Models;
using Frontend.Services;

namespace Frontend
{
    public partial class MainWindow : Window
    {
        private readonly ResumeService _resumeService;

        public MainWindow()
        {
            InitializeComponent();
            _resumeService = new ResumeService(App.HttpClient);

            SetPlaceholder(SexBox, "Sex");
            SetPlaceholder(ExperienceBox, "Experience");
            SetPlaceholder(LanguageBox, "Language Proficiency");
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var query = new ResumeQuery
            {
                Sex = SexBox.Text == (string)SexBox.Tag ? null : SexBox.Text,
                Experience = ExperienceBox.Text == (string)ExperienceBox.Tag ? null : ExperienceBox.Text,
                LanguageProficiency = LanguageBox.Text == (string)LanguageBox.Tag ? null : LanguageBox.Text
            };

            try
            {
                var resumes = await _resumeService.GetResumesAsync(query);
                var resumeListWindow = new ResumeListWindow(resumes);
                resumeListWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving resumes: {ex.Message}");
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Text == (string)textBox.Tag)
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = new SolidColorBrush(Color.FromArgb(128, 136, 136, 136));
                textBox.Text = (string)textBox.Tag;
            }
        }

        private void SetPlaceholder(TextBox textBox, string placeholderText)
        {
            textBox.Tag = placeholderText;
            textBox.Text = placeholderText;
            textBox.Foreground = new SolidColorBrush(Color.FromArgb(128, 136, 136, 136));
        }
    }
}
